using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Web;
using System.Web.Mvc;
using System.Web.Hosting;
using MvcFileManager.Filters;
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
        //
        //GLOBAL VALUE:

        private string _uploadsFolder = HostingEnvironment.MapPath("~/App_Data/Document/");
        private string fileTime = DateTime.Now.Year.ToString()+DateTime.Now.Month.ToString()+DateTime.Now.Day.ToString()+
                                  DateTime.Now.Hour.ToString()+DateTime.Now.Minute.ToString()+DateTime.Now.Second.ToString()+
                                  DateTime.Now.Millisecond.ToString();

        //
        //GET: Home/Index

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
            flvm.Permission = Convert.ToString(Session["Permission"]);
            return View("Index", flvm);
        }

        //
        //GET: /Home/SearchIndex

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

        //
        //GET: /Home/GetUploadLink

        public ActionResult GetUploadLink()
        {
            if(Session["Permission"] != null && 
                (UserStatus)Session["Permission"] != UserStatus.NonAuthenticatedUser &&
                (UserStatus)Session["Permission"] != UserStatus.AuthenticatedVisitor)
            {
                return PartialView("GetUploadLink");
            }
            else
            {
                return new EmptyResult();
            }
        }

        //
        //GET: /Home/SaveFile

        [PermissionFilter]
        public ActionResult SaveFile()
        {
            return View("Upload", new FileViewModel());
        }

        //
        //POST: /Home/SaveFile

        [HttpPost]
        [PermissionFilter]
        public ActionResult SaveFile(string fileName, HttpPostedFileBase uploadFile)
        {
            if (uploadFile != null && fileName != null)
            {
                FileDB file = new FileDB();
                file.FileName = fileName;
                file.Creater = User.Identity.Name;
                file.FilePath = uploadFile.FileName;
                uploadFile.SaveAs(Path.Combine(_uploadsFolder,file.FilePath));

                FileBusinessLayer fbl = new FileBusinessLayer();
                fbl.SaveFile(file, "upload");
                return RedirectToAction("Index");
            }
            else
            {
                FileViewModel fvm = new FileViewModel();
                fvm.FileName = fileName;
                return View("Upload", fvm);
            }
        }

        //
        //GET: /Home/GetEditLink

        public ActionResult GetEditLink(int id)
        {
            if (Session["Permission"] != null &&
                (UserStatus)Session["Permission"] != UserStatus.NonAuthenticatedUser &&
                (UserStatus)Session["Permission"] != UserStatus.AuthenticatedVisitor)
            {
                ViewBag.Id = id;
                return PartialView("GetEditLink");
            }
            else
            {
                return new EmptyResult();
            }
        }

        //
        //GET: /Home/Edit

        [PermissionFilter]
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
                fvm.FileContent = System.IO.File.ReadAllText(Path.Combine(_uploadsFolder, file.FilePath));
            }
            else
            {
                fvm.FileContent = "File content is empty. Please check the file path!";
            }
            return View("Edit", fvm);
        }

        //
        //POST: /Home/Edit

        [HttpPost]
        [PermissionFilter]
        public ActionResult Edit()
        {
            int FileId = int.Parse(Request["FileId"]);
            System.Diagnostics.Debug.Write(FileId);
            string FileName = Request["FileName"];
            int FileVersion = int.Parse(Request["FileVersion"]);
            string FileContent = Request["FileContent"];

            FileDB file = new FileDB();
            file.FileId = FileId;
            file.FileName = FileName;
            file.FilePath = fileTime + "_" + FileVersion + "_" + FileName;
            file.Creater = User.Identity.Name;
            file.Version = FileVersion;

            FileStream fs = new FileStream(Path.Combine(_uploadsFolder,file.FilePath), FileMode.Create);
            StreamWriter sw = new StreamWriter(fs);
            sw.Write(FileContent);
            sw.Flush();
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
        //GET: /Home/Details

        [PermissionFilter]
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
                fvm.FileContent = System.IO.File.ReadAllText(Path.Combine(_uploadsFolder,file.FilePath));
            }
            else 
            {
                fvm.FileContent = "File content is empty. Please check the file path!";
            }
            return View("Details", fvm);
        }

        //
        //POST: /Home/Delete

        [HttpPost]
        [PermissionFilter]
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
        //POST: /Home/ConfirmDelete

        [HttpPost]
        [PermissionFilter]
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
        //GET: /Home/History

        [PermissionFilter]
        public ActionResult History(int id)
        {
            FileListViewModel flvm = new FileListViewModel();
            FileBusinessLayer fbl = new FileBusinessLayer();
            List<FileDB> files = fbl.GetFiles();
            List<FileViewModel> evmlist = new List<FileViewModel>();

            FileDB filenow = fbl.GetFile(id);
            while (filenow != null)
            {
                FileViewModel fvm = new FileViewModel();
                fvm.FileId = filenow.FileId;
                fvm.FileName = filenow.FileName;
                fvm.Creater = filenow.Creater;
                fvm.UploadTime = filenow.UploadTime;
                fvm.Version = filenow.Version;
                fvm.FileContent = filenow.FilePath;
                evmlist.Add(fvm);
                filenow = filenow.FormerId;
            }

            flvm.FileList = evmlist;
            flvm.Permission = Convert.ToString(Session["Permission"]);
            return View("Historys", flvm);
        }

        //
        //POST: /Home/VersionCompare

        [PermissionFilter]
        public ActionResult VersionCompare()
        {
            int v1 = int.Parse(Request["v1"]);
            int v2 = int.Parse(Request["v2"]);
            FileBusinessLayer fbl = new FileBusinessLayer();
            FileDB f1 = fbl.GetFile(v1);
            FileDB f2 = fbl.GetFile(v2);

            FileStream fs1 = new FileStream(Path.Combine(_uploadsFolder,f1.FilePath), FileMode.Open);
            FileStream fs2 = new FileStream(Path.Combine(_uploadsFolder,f2.FilePath), FileMode.Open);
            StreamReader sr1 = new StreamReader(fs1);
            StreamReader sr2 = new StreamReader(fs2);
            string content1=sr1.ReadLine();
            while (content1.Trim() == null || content1.Trim().Equals(""))
            {
                content1 = sr1.ReadLine();
            }

            string content2 = sr2.ReadLine();
            while (content2.Trim() == null || content2.Trim().Equals(""))
            {
                content2 = sr2.ReadLine();
            }

            string DiffContent = "\n";
            int line = 1;

            while(!sr1.EndOfStream || !sr2.EndOfStream)
            {
                System.Diagnostics.Debug.Write(content1);
                System.Diagnostics.Debug.Write(content2);

                if (sr1.EndOfStream)
                {
                    DiffContent += "In Line No." + line + ": " + "\n"
                        + "         Version" + f1.Version + ": " + "File End." + "\n"
                        + "         Version" + f2.Version + ": " + content2 + "\n\n";
                }
                else if (sr2.EndOfStream)
                {
                    DiffContent += "In Line No." + line + ": " + "\n"
                        + "         Version" + f1.Version + ": " + content1 + "\n"
                        + "         Version" + f2.Version + ": " + "File End." + "\n\n";
                }
                else if (!content1.Equals(content2))
                {
                    DiffContent += "In Line No." + line + ": " + "\n"
                        + "         Version" + f1.Version + ": " + content1 + "\n"
                        + "         Version" + f2.Version + ": " + content2 + "\n\n";
                }

                if (!sr1.EndOfStream) 
                {
                    content1 = sr1.ReadLine();
                }
                if (!sr2.EndOfStream) 
                {
                    content2 = sr2.ReadLine();
                }
                line++;
            }

            sr1.Close();
            sr2.Close();
            fs1.Close();
            fs2.Close();

            VersionViewModel vvm = new VersionViewModel();
            vvm.FirstVersion = f1;
            vvm.SecondVersion = f2;
            vvm.DiffContent = DiffContent;

            return View("Versions", vvm);
        }

    }
}
