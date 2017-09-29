namespace MvcFileManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deleteco : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.TblFileDB", "FormerIdID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TblFileDB", "FormerIdID", c => c.Int(nullable: false));
        }
    }
}
