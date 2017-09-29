namespace MvcFileManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fk : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TblFileDB", "FormerIdID", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TblFileDB", "FormerIdID");
        }
    }
}
