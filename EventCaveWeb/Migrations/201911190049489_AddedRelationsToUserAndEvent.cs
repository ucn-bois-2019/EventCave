namespace EventCaveWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedRelationsToUserAndEvent : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Events", "Host_Id", c => c.String(nullable: false, maxLength: 128, storeType: "nvarchar"));
            CreateIndex("dbo.Events", "Host_Id");
            AddForeignKey("dbo.Events", "Host_Id", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Events", "Host_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Events", new[] { "Host_Id" });
            DropColumn("dbo.Events", "Host_Id");
        }
    }
}
