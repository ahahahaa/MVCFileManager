namespace MvcFileManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifyUploadTime : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.TblFile", "UploadTime", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TblFile", "UploadTime", c => c.String());
        }
    }
}
