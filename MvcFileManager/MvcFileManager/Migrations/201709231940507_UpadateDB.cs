namespace MvcFileManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpadateDB : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TblFileDB", "Creater", c => c.String(nullable: false));
            DropColumn("dbo.TblFileDB", "Author");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TblFileDB", "Author", c => c.String(nullable: false));
            DropColumn("dbo.TblFileDB", "Creater");
        }
    }
}
