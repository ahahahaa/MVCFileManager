using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MvcFileManager.Models;
using MvcFileManager.Data_Access_Layer;
using System.Data;

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

            switch (action)
            {
                case "upload":
                    file.Creater = "admin";
                    file.UploadTime = DateTime.Now;
                    file.ModifiedTime = DateTime.Now;
                    file.Version = 1;
                    file.isDelete = false;
                    file.FormerId = null;

                    fmDAL.Entry(file).State = EntityState.Added;
                    break;
            
                case "delete":
                    int id = file.FileId;
                    FileDB record = fmDAL.Files.Find(id);

                    record.isDelete = true;
                    record.FilePath = null;
                    record.ModifiedTime = DateTime.Now;

                    fmDAL.Entry(record).State = EntityState.Modified;
                    break;

                case "modify":
                    FileDB parentFile = fmDAL.Files.Find(file.FileId);
                    
                    file.Creater = parentFile.Creater;
                    file.UploadTime = parentFile.UploadTime;
                    file.ModifiedTime = DateTime.Now;
                    if(file.Version == 0)
                    {
                        file.Version = parentFile.Version + 1;
                    }
                    file.isDelete = false;
                    file.FormerId = parentFile;

                    fmDAL.Entry(file).State = EntityState.Added;

                    parentFile.isDelete = true;
                    fmDAL.Entry(parentFile).State = EntityState.Modified;

                    break;

            }
            fmDAL.SaveChanges();
            return file;
        }

    }
}