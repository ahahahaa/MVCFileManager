namespace MvcFileManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeisSearchPM : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.UserProfileDB", "isSearchPM");
            AddColumn("dbo.UserProfileDB", "isSearchPM", c => c.Boolean(nullable: true,defaultValue:true));
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserProfileDB", "isSearchPM");
        }
    }
}
