namespace MvcFileManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class controllerupdate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TblFileDB",
                c => new
                    {
                        FileId = c.Int(nullable: false, identity: true),
                        FileName = c.String(nullable: false),
                        Author = c.String(nullable: false),
                        UploadTime = c.DateTime(nullable: false),
                        ModifiedTime = c.DateTime(nullable: false),
                        Version = c.Int(nullable: false),
                        FilePath = c.String(nullable: false),
                        isDelete = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.FileId);
            
            DropTable("dbo.TblFile");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.TblFile",
                c => new
                    {
                        FileId = c.Int(nullable: false, identity: true),
                        FileName = c.String(nullable: false),
                        Author = c.String(nullable: false),
                        UploadTime = c.DateTime(nullable: false),
                        ModifiedTime = c.DateTime(nullable: false),
                        Version = c.Int(nullable: false),
                        FilePath = c.String(nullable: false),
                        FileContent = c.String(),
                        isDelete = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.FileId);
            
            DropTable("dbo.TblFileDB");
        }
    }
}
