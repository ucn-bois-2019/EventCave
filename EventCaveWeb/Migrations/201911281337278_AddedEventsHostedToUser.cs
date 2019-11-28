namespace EventCaveWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedEventsHostedToUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Events", "ApplicationUser_Id1", c => c.String(maxLength: 128, storeType: "nvarchar"));
            CreateIndex("dbo.Events", "ApplicationUser_Id1");
            AddForeignKey("dbo.Events", "ApplicationUser_Id1", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Events", "ApplicationUser_Id1", "dbo.AspNetUsers");
            DropIndex("dbo.Events", new[] { "ApplicationUser_Id1" });
            DropColumn("dbo.Events", "ApplicationUser_Id1");
        }
    }
}
