namespace MvcFileManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Adddeleteanddetail : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.TblFileDB", "FilePath", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TblFileDB", "FilePath", c => c.String(nullable: false));
        }
    }
}
