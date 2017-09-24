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

            FileListViewModel elvm = new FileListViewModel();
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

            elvm.FileList = evmlist;
            elvm.UserName = "Admin";

            return View("Index", elvm);
        }

        //public ActionResult GetUploadLink()
        //{
        //    return PartialView("GetUploadLink");
        //}

        public ActionResult Upload()
        {
            return View("CreateFile", new CreateFileViewModel());
        }


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

                        file.Creater = "Admin";
                        file.UploadTime = DateTime.Now;
                        file.ModifiedTime = DateTime.Now;
                        file.Version = 1;
                        file.isDelete = false;

                        FileBusinessLayer fbl = new FileBusinessLayer();
                        fbl.SaveFile(file);

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

                    default:
                        return RedirectToAction("CreateFile", new CreateFileViewModel());
                }
            }
            return Content("Upload Error! File is empty");
        }


        public ActionResult Details(int id)
        {
            FileBusinessLayer fbl = new FileBusinessLayer();
            FileDB file = fbl.GetFile(id);

            if (file == null)
            {
                return JavaScript("Error" + id);
            }

            DisplayFileViewModel pfvm = new DisplayFileViewModel();

            pfvm.FileName = file.FileName;
            pfvm.Creater = file.Creater;
            pfvm.Version = file.Version;
            pfvm.UploadTime = file.UploadTime;
            pfvm.ModifiedTime = file.ModifiedTime;
            if (file.FilePath != "")
            {
                pfvm.FileContent = System.IO.File.ReadAllText(file.FilePath);
            }
            else 
            {
                pfvm.FileContent = "File content is empty. Please check the file path!";
            }
            return View("Details", pfvm);

        }


        public ActionResult Edit(int id = 0)
        {
            FileBusinessLayer fbl = new FileBusinessLayer();
            FileDB file = fbl.GetFile(id);

            return View("Edit", new CreateFileViewModel());
        }


        public ActionResult Delete(FormCollection fcNotUsed, int id)
        {
            FileBusinessLayer fbl = new FileBusinessLayer();
            FileDB file = fbl.GetFile(id);
            
            if (file == null)
            {
                return HttpNotFound();
            }
            
            file.ModifiedTime = DateTime.Now;
            file.isDelete = true;

            fbl.SaveFile(file);
 
            return RedirectToAction("Index");
        }

        //public ActionResult About()
        //{
        //    ViewBag.Message = "Your app description page.";

        //    return View();
        //}

        //public ActionResult Contact()
        //{
        //    ViewBag.Message = "Your contact page.";

        //    return View();
        //}
    }
}
