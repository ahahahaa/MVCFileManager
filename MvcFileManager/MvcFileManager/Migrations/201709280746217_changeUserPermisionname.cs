namespace MvcFileManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeUserPermisionname : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserProfileDB", "isUploadPM", c => c.Boolean(nullable: false));
            AddColumn("dbo.UserProfileDB", "isSearchPM", c => c.Boolean(nullable: false));
            AddColumn("dbo.UserProfileDB", "isModifyPM", c => c.Boolean(nullable: false));
            AddColumn("dbo.UserProfileDB", "isDeletePM", c => c.Boolean(nullable: false));
            DropColumn("dbo.UserProfileDB", "isUpload");
            DropColumn("dbo.UserProfileDB", "isSearch");
            DropColumn("dbo.UserProfileDB", "isModify");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserProfileDB", "isModify", c => c.Boolean(nullable: false));
            AddColumn("dbo.UserProfileDB", "isSearch", c => c.Boolean(nullable: false));
            AddColumn("dbo.UserProfileDB", "isUpload", c => c.Boolean(nullable: false));
            DropColumn("dbo.UserProfileDB", "isDeletePM");
            DropColumn("dbo.UserProfileDB", "isModifyPM");
            DropColumn("dbo.UserProfileDB", "isSearchPM");
            DropColumn("dbo.UserProfileDB", "isUploadPM");
        }
    }
}
