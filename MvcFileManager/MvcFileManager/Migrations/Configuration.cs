namespace MvcFileManager.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using MvcFileManager.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<MvcFileManager.Data_Access_Layer.FileManagerDAL>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(MvcFileManager.Data_Access_Layer.FileManagerDAL context)
        {
            //  This method will be called after migrating to the latest version.
            //context.UserProfiles.AddOrUpdate(
            //    u => u.UserId, 
            //    new UserProfile()
            //    { 
            //        UserId = 1,
            //        UserName = "Admin",
            //        isAdmin = true,
            //        isDeleteUser = false,
            //        isUploadPM = true,
            //        isModifyPM = true,
            //        isSearchPM = true,
            //        isDeletePM = true
            //    }
            //);

        }
    }
}
