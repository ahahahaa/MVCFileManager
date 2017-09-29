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
    [Authorize]
    public class HomeController : Controller
    {
        private string _uploadsFolder = HostingEnvironment.MapPath("~/App_Data/Document/");
        private string fileTime = DateTime.Now.Year.ToString()+DateTime.Now.Month.ToString()+DateTime.Now.Day.ToString()+
                                  DateTime.Now.Hour.ToString()+DateTime.Now.Minute.ToString()+DateTime.Now.Second.ToString()+
                                  DateTime.Now.Millisecond.ToString();

        
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
            flvm.UserName = User.Identity.Name;
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

        public ActionResult SaveFile()
        {
            return View("CreateFile", new FileViewModel());
        }

        //
        //Post: /Home/SaveFile

        [HttpPost]
        public ActionResult SaveFile(string fileName, HttpPostedFileBase uploadFile)
        {
            if (uploadFile != null && fileName != null)
            {
                FileDB file = new FileDB();
                file.FileName = fileName;
                file.Creater = User.Identity.Name;
                file.FilePath = Path.Combine(_uploadsFolder, uploadFile.FileName);
                uploadFile.SaveAs(file.FilePath);

                FileBusinessLayer fbl = new FileBusinessLayer();
                fbl.SaveFile(file, "upload");

                return RedirectToAction("Index");
            }
            else
            {
                FileViewModel fvm = new FileViewModel();
                fvm.FileName = fileName;
                return View("CreateFile", fvm);
            }
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
            FileViewModel fvm = new FileViewModel();
            fvm.FileId = file.FileId;
            fvm.FileName = file.FileName;
            fvm.Version = file.Version + 1;
            if (file.FilePath != "")
            {
                fvm.FileContent = System.IO.File.ReadAllText(file.FilePath);
            }
            else
            {
                fvm.FileContent = "File content is empty. Please check the file path!";
            }

            return View("Edit", fvm);
        }

        //
        //Post: /Home/Edit


        [HttpPost]
        // user Ajax communication return json
        public ActionResult Edit()
        {
            //datas
            int FileId = int.Parse(Request["FileId"]);
            System.Diagnostics.Debug.Write(FileId);
            string FileName = Request["FileName"];
            int FileVersion = int.Parse(Request["FileVersion"]);
            string FileContent = Request["FileContent"];
            //save file
            FileDB file = new FileDB();
            file.FileId = FileId;
            file.FileName = FileName;
            file.FilePath = Path.Combine(_uploadsFolder, fileTime + "_" + FileVersion + "_" + FileName);
            file.Creater = User.Identity.Name;
            file.Version = FileVersion;

            FileStream fs = new FileStream(file.FilePath, FileMode.Create);
            StreamWriter sw = new StreamWriter(fs);
            //开始写入
            sw.Write(FileContent);
            //清空缓冲区
            sw.Flush();
            //关闭流
            sw.Close();
            fs.Close();

            FileBusinessLayer fbl = new FileBusinessLayer();
            fbl.SaveFile(file, "modify");

            var result = new { err = false, message = "no err" };
            return Json(result);
            
                /*
            catch
            {
                var result = new { err = true, message = "save file err" };
                return Json(result);
            }
                 * */
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
        [HttpPost]
        public ActionResult Delete(FormCollection fcNotUsed, int id)
        {
            FileBusinessLayer fbl = new FileBusinessLayer();
            FileDB file = fbl.GetFile(id);
            
            if (file == null)
            {
                return HttpNotFound();
            }
            
            fbl.SaveFile(file, "mark delete");
            return RedirectToAction("Index");
        }

        //
        //Post: /Home/ConfirmDelete
        [HttpPost]
        public ActionResult ConfirmDelete(FormCollection fcNotUsed, int id)
        {
            FileBusinessLayer fbl = new FileBusinessLayer();
            FileDB file = fbl.GetFile(id);

            if (file == null)
            {
                return HttpNotFound();
            }

            //System.IO.File.Delete(file.FilePath);
            fbl.SaveFile(file, "delete");
            return RedirectToAction("Index");
            
        }
    
    
        //
        //Get: /Home/History
        public ActionResult History(int id)
        {
            //user SQL to filter  override
            FileListViewModel flvm = new FileListViewModel();
            FileBusinessLayer fbl = new FileBusinessLayer();
            List<FileDB> files = fbl.GetFiles();
            List<FileViewModel> evmlist = new List<FileViewModel>();

            FileDB filenow = fbl.GetFile(id);
            while (filenow != null)
            {
                FileViewModel fvm = new FileViewModel();
                System.Diagnostics.Debug.Write(filenow.FileId);
                fvm.FileId = filenow.FileId;
                fvm.FileName = filenow.FileName;
                fvm.Creater = filenow.Creater;
                fvm.UploadTime = filenow.UploadTime;
                fvm.Version = filenow.Version;
                fvm.FileContent = filenow.FilePath;
                evmlist.Add(fvm);
                System.Diagnostics.Debug.Write(filenow.FormerId);
                filenow = filenow.FormerId;
            }

            flvm.FileList = evmlist;
            flvm.UserName = User.Identity.Name;
            return View("Historys", flvm);
        }

        //
        //Post: /Home/VersionCompare
        [HttpPost]
        public ActionResult VersionCompare()
        {
            return View("Versions");
        }

    }
}
