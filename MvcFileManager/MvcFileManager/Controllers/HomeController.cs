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

using System.Diagnostics;

namespace MvcFileManager.Controllers
{
    public class HomeController : Controller
    {

        private string _uploadsFolder = HostingEnvironment.MapPath("~/App_Data/Document/");

        [Authorize]
        public ActionResult Index()
        {
            //ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            FileListViewModel elvm = new FileListViewModel();
            FileBusinessLayer fbl = new FileBusinessLayer();
            List<FileDB> files = fbl.GetFiles();

            List<FileViewModel> evmlist = new List<FileViewModel>();

            foreach (FileDB file in files)
            {
                FileViewModel evm = new FileViewModel();
                evm.FileName = file.FileName;
                evm.Author = file.Author;
                evm.UploadTime = file.UploadTime;
                evm.Version = file.Version;
                evmlist.Add(evm);
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
        //public ActionResult SaveFile()
        {
            //HttpPostedFileBase UploadFile = Request.Files["UploadFile"];
            //System.Diagnostics.Debug.Write(Request.Params);
            //FileDB file = new FileDB();
            //String BtnSubmit = "Save File";
            //System.Diagnostics.Debug.Write(file.FileName);
            
            if (UploadFile != null)
            {
                switch (BtnSubmit)
                {
                    case "Save File":
                        //if (ModelState.IsValid)
                        //{
                            file.FilePath = Path.Combine(_uploadsFolder, UploadFile.FileName);
                            UploadFile.SaveAs(file.FilePath);
                            file.UploadTime = DateTime.Now;
                            file.ModifiedTime = DateTime.Now;
                            file.Version = 1;
                            file.isDelete = false;

                            FileBusinessLayer fbl = new FileBusinessLayer();
                            fbl.SaveFile(file);
                            return JavaScript("1");
                            //return RedirectToAction("Index");

                            //return Content(e.FirstName + "|" + e.LastName + "|" + e.Salary);
                        //}
                        //else
                        //{
                        //    //错误验证值的保留
                        //    CreateFileViewModel vm = new CreateFileViewModel();
                        //    vm.FileName = file.FileName;
                        //    vm.Author = file.Author;

                        //    return JavaScript("2");
                        //    //return View("CreateFile", vm);
                        //}
                    case "Cancel":
                        return JavaScript("3");
                        //return RedirectToAction("Index");
                    default:
                        return JavaScript("5");
                }
            }
            return JavaScript("4");
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
