using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Web;
using System.Web.Mvc;
using System.Web.Hosting;
using MvcFileManager.Models;
using MvcFileManager.ViewModels;
using MvcFileManager.Business_Layer;
using MvcFileManager.Data_Access_Layer;

using System.Diagnostics;

namespace MvcFileManager.Controllers
{
    public class HomeController : Controller
    {
        private string _uploadsFolder = HostingEnvironment.MapPath("~/App_Data/Document/");

        [Authorize]
        public ActionResult Index()
        {
            FileListViewModel flvm = new FileListViewModel();
            FileBusinessLayer fbl = new FileBusinessLayer();
            List<FileDB> files = fbl.GetFiles();
            List<FileViewModel> evmlist = new List<FileViewModel>();

            foreach (FileDB file in files)
            {
                if (!file.isDelete)
                {
                    FileViewModel fvm = new FileViewModel();
                    fvm.FileId = file.FileId;
                    fvm.FileName = file.FileName;
                    fvm.Creater = file.Creater;
                    fvm.UploadTime = file.UploadTime;
                    fvm.Version = file.Version;
                    evmlist.Add(fvm);
                }
            }

            flvm.FileList = evmlist;
            flvm.UserName = "Admin";

            return View("Index", flvm);
        }

        //
        //Get: /Home/SearchIndex
        public ActionResult SearchIndex(string keyword)
        {
            FileBusinessLayer fbl = new FileBusinessLayer();
            var files = from f in fbl.GetFiles() select f;
            if (!String.IsNullOrEmpty(keyword))
            {
                files = files.Where(s => s.FileName.Contains(keyword));
            }
            return View("SearchIndex", files);
        
        }


        //public ActionResult GetUploadLink()
        //{
        //    return PartialView("GetUploadLink");
        //}

        //
        //Get: /Home/Upload

        public ActionResult Upload()
        {
            return View("CreateFile", new CreateFileViewModel());
        }

        //
        //Post: /Home/SaveFile

        [HttpPost]
        public ActionResult SaveFile(FileDB file, string BtnSubmit, HttpPostedFileBase UploadFile)
        {
            if (UploadFile != null)
            {
                switch (BtnSubmit)
                {
                    case "Save File":

                        file.FilePath = Path.Combine(_uploadsFolder, UploadFile.FileName);
                        UploadFile.SaveAs(file.FilePath);

                        FileBusinessLayer fbl = new FileBusinessLayer();

                        fbl.SaveFile(file, "upload");

                        return RedirectToAction("Index");

                        //if (ModelState.IsValid) { }
                        //else 
                        //{
                        //    CreateFileViewModel cfvm = new CreateFileViewModel();
                        //    cfvm.FileName = file.FileName;
                        //    return View("CreateFile", cfvm);
                        //}

                    case "Cancel":
                        return RedirectToAction("Index");

                }
            }
            return Content("Upload Error! File is empty");
        }

        //
        //Get: /Home/Edit

        [HttpGet]
        public ActionResult Edit(int id = 0)
        {
            FileBusinessLayer fbl = new FileBusinessLayer();
            FileDB file = fbl.GetFile(id);

            if (file == null)
            {
                return HttpNotFound();
            }
            CreateFileViewModel cfvm = new CreateFileViewModel();
            cfvm.FileId = file.FileId;
            cfvm.FileName = file.FileName;
            cfvm.Version = file.Version;

            return View("Edit", cfvm);
        }

        //
        //Post: /Home/Edit

        [HttpPost]
        public ActionResult Edit(FileDB file, string BtnSubmit, HttpPostedFileBase UploadFile)
        {
            if (UploadFile != null)
            {
                switch (BtnSubmit)
                {
                    case "Save":

                        file.FilePath = Path.Combine(_uploadsFolder, UploadFile.FileName);
                        UploadFile.SaveAs(file.FilePath);

                        FileBusinessLayer fbl = new FileBusinessLayer();
                        fbl.SaveFile(file, "modify");

                        return RedirectToAction("Index");

                    case "Cancel":
                        return RedirectToAction("Index");

                }
            }
            return RedirectToAction("Index");
        }

        //
        //Get: /Home/Details

        public ActionResult Details(int id)
        {
            FileBusinessLayer fbl = new FileBusinessLayer();
            FileDB file = fbl.GetFile(id);

            if (file == null)
            {
                return HttpNotFound();
            }

            FileViewModel fvm = new FileViewModel();
            fvm.FileId = file.FileId;
            fvm.FileName = file.FileName;
            fvm.Creater = file.Creater;
            fvm.Version = file.Version;
            fvm.UploadTime = file.UploadTime;
            fvm.ModifiedTime = file.ModifiedTime;
            if (file.FilePath != "")
            {
                fvm.FileContent = System.IO.File.ReadAllText(file.FilePath);
            }
            else 
            {
                fvm.FileContent = "File content is empty. Please check the file path!";
            }
            return View("Details", fvm);

        }


        //
        //Post: /Home/Delete

        public ActionResult Delete(FormCollection fcNotUsed, int id)
        {
            FileBusinessLayer fbl = new FileBusinessLayer();
            FileDB file = fbl.GetFile(id);
            
            if (file == null)
            {
                return HttpNotFound();
            }
            
            fbl.SaveFile(file, "delete");
            return RedirectToAction("Index");
        }

    }
}
