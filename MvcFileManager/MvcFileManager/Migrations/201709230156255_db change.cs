namespace MvcFileManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dbchange : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.TblFile", "FileName", c => c.String(nullable: false));
            AlterColumn("dbo.TblFile", "Author", c => c.String(nullable: false));
            AlterColumn("dbo.TblFile", "ModifiedTime", c => c.DateTime(nullable: false));
            AlterColumn("dbo.TblFile", "Version", c => c.Int(nullable: false));
            AlterColumn("dbo.TblFile", "FilePath", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TblFile", "FilePath", c => c.String());
            AlterColumn("dbo.TblFile", "Version", c => c.String());
            AlterColumn("dbo.TblFile", "ModifiedTime", c => c.String());
            AlterColumn("dbo.TblFile", "Author", c => c.String());
            AlterColumn("dbo.TblFile", "FileName", c => c.String());
        }
    }
}
