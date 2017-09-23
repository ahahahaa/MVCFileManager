using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using MvcFileManager.Models;

namespace MvcFileManager.Data_Access_Layer
{
    public class FileManagerDAL : DbContext
    {
        //public FileManagerDAL()
        //    : base("FileDB")
        //{ }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FileDB>().ToTable("TblFileDB");
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<FileDB> Files { get; set; }

    }
}