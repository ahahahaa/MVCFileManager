namespace MvcFileManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update : DbMigration
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
                        isDelete = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.FileId);
            
            DropTable("dbo.UserProfileDB");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.UserProfileDB",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                        isUpload = c.Boolean(nullable: false),
                        isSearch = c.Boolean(nullable: false),
                        isModify = c.Boolean(nullable: false),
                        isDeleteUser = c.Boolean(nullable: false),
                        isAdmin = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.UserId);
            
            DropTable("dbo.TblFileDB");
        }
    }
}
