namespace MvcFileManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TblFileDB",
                c => new
                    {
                        FileId = c.Int(nullable: false, identity: true),
                        FileName = c.String(nullable: false),
                        Creater = c.String(nullable: false),
                        UploadTime = c.DateTime(nullable: false),
                        ModifiedTime = c.DateTime(nullable: false),
                        Version = c.Int(nullable: false),
                        FilePath = c.String(),
                        isDelete = c.Boolean(nullable: false, defaultValue: false),
                        FormerId_FileId = c.Int(),
                    })
                .PrimaryKey(t => t.FileId)
                .ForeignKey("dbo.TblFileDB", t => t.FormerId_FileId)
                .Index(t => t.FormerId_FileId);
            
            CreateTable(
                "dbo.UserProfileDB",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        UserName = c.String(nullable: false),
                        isUploadPM = c.Boolean(nullable: true, defaultValue: false),
                        isSearchPM = c.Boolean(nullable: true, defaultValue: true),
                        isModifyPM = c.Boolean(nullable: true, defaultValue: false),
                        isDeletePM = c.Boolean(nullable: true, defaultValue: false),
                        isDeleteUser = c.Boolean(nullable: false, defaultValue: false),
                        isAdmin = c.Boolean(nullable: false, defaultValue: false),
                    })
                .PrimaryKey(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.TblFileDB", new[] { "FormerId_FileId" });
            DropForeignKey("dbo.TblFileDB", "FormerId_FileId", "dbo.TblFileDB");
            DropTable("dbo.UserProfileDB");
            DropTable("dbo.TblFileDB");
        }
    }
}
