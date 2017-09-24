using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MvcFileManager.Models;
using MvcFileManager.Data_Access_Layer;
using System.Data.Entity;

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

        public FileDB SaveFile(FileDB file)
        {
            int id = file.FileId;

            FileManagerDAL fmDAL = new FileManagerDAL();
            FileDB record = fmDAL.Files.Find(id);

            if (record == null || id == null)
            {
                fmDAL.Files.Add(file);
            }
            else 
            {
                //fmDAL.Files.Attach(file);
                //DbContext.Entry(file).State = System.Data.EntityState.Modified;

                record.FileName = file.FileName;
                record.FilePath = file.FilePath;
                record.isDelete = file.isDelete;
                record.ModifiedTime = file.ModifiedTime;
                record.UploadTime = file.UploadTime;
                record.Version = file.Version;

            }
            fmDAL.SaveChanges();
            return file;
        }

    }
}