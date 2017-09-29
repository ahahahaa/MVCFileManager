namespace MvcFileManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeIsSearch : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.UserProfileDB", "isSearchPM");
            AddColumn("dbo.UserProfileDB", "isSearchPM", c => c.Boolean(nullable: false, defaultValue: true));
        }
        
        public override void Down()
        {
        }
    }
}
