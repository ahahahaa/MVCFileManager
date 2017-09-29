using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using MvcFileManager.Models;
using MvcFileManager.Data_Access_Layer;


namespace MvcFileManager.Business_Layer
{
    public class FileBusinessLayer
    {

        public FileDB GetFile(int id)
        {
            FileManagerDAL fmDAL = new FileManagerDAL();
            return fmDAL.Files.Find(id);
        }

        public List<FileDB> GetFiles()
        {
            FileManagerDAL fmDAL = new FileManagerDAL();
            return fmDAL.Files.ToList();
        }

        public FileDB SaveFile(FileDB file, string action)
        {
            FileManagerDAL fmDAL = new FileManagerDAL();
            FileDB target;
            if (file.FileId != 0)
            {
                target = fmDAL.Files.Find(file.FileId);
            }
            else
            {
                target = file;
            }

            switch (action)
            {
                case "upload":
                    {
                        target.UploadTime = DateTime.Now;
                        target.ModifiedTime = DateTime.Now;
                        target.Version = 1;
                        target.FormerId = null;

                        fmDAL.Entry(target).State = EntityState.Added;
                        break;
                    }
                case "mark delete":
                    {
                        target.isDelete = true;
                        //target.FilePath = null;
                        target.ModifiedTime = DateTime.Now;

                        fmDAL.Entry(target).State = EntityState.Modified;
                        break;
                    }
                case "delete":
                    {
                        target.FilePath = null;
                        target.ModifiedTime = DateTime.Now;
                        fmDAL.Entry(target).State = EntityState.Modified;
                        break;
                    }
                case "modify":
                    {
                        file.UploadTime = target.UploadTime;
                        file.ModifiedTime = DateTime.Now;
                        file.FormerId = target;

                        fmDAL.Entry(file).State = EntityState.Added;

                        target.isDelete = true;
                        fmDAL.Entry(target).State = EntityState.Modified;

                        break;
                    }
            }
            fmDAL.SaveChanges();
            return file;
        }

    }
}