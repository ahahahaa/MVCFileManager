using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MvcFileManager.Models;
using MvcFileManager.Data_Access_Layer;

namespace MvcFileManager.Business_Layer
{
    public class FileBusinessLayer
    {

        public List<FileDB> GetFiles()
        {
            FileManagerDAL fmDAL = new FileManagerDAL();
            return fmDAL.Files.ToList();
        }

        public FileDB SaveFile(FileDB file)
        {
            FileManagerDAL fmDAL = new FileManagerDAL();
            fmDAL.Files.Add(file);
            fmDAL.SaveChanges();
            return file;
        }

    }
}