namespace MvcFileTest.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.TblEmployee", "Salary", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TblEmployee", "Salary", c => c.Int(nullable: false));
        }
    }
}
