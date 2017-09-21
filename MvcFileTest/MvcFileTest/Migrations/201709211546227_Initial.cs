namespace MvcFileTest.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.TblEmployee", "FirstName", c => c.String(nullable: false));
            AlterColumn("dbo.TblEmployee", "LastName", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TblEmployee", "LastName", c => c.String());
            AlterColumn("dbo.TblEmployee", "FirstName", c => c.String());
        }
    }
}
