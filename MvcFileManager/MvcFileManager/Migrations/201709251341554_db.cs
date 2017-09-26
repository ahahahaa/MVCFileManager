namespace MvcFileManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class db : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserProfileDB",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                        isUpload = c.Boolean(nullable: true, defaultValue: false),
                        isSearch = c.Boolean(nullable: true, defaultValue: false),
                        isModify = c.Boolean(nullable: true, defaultValue: false),
                        isDeleteUser = c.Boolean(nullable: true, defaultValue: false),
                        isAdmin = c.Boolean(nullable: false, defaultValue: false),
                    })
                .PrimaryKey(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.UserProfileDB");
        }
    }
}
