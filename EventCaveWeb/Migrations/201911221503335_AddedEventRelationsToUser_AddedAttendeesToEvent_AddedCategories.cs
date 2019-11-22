namespace EventCaveWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedEventRelationsToUser_AddedAttendeesToEvent_AddedCategories : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 15, storeType: "nvarchar"),
                        Description = c.String(nullable: false, maxLength: 50, storeType: "nvarchar"),
                        Image = c.String(nullable: false, unicode: false),
                        Event_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Events", t => t.Event_Id)
                .Index(t => t.Event_Id);
            
            AddColumn("dbo.Events", "ApplicationUser_Id", c => c.String(maxLength: 128, storeType: "nvarchar"));
            AddColumn("dbo.Events", "Host_Id", c => c.String(nullable: false, maxLength: 128, storeType: "nvarchar"));
            AddColumn("dbo.AspNetUsers", "Event_Id", c => c.Int());
            CreateIndex("dbo.Events", "ApplicationUser_Id");
            CreateIndex("dbo.Events", "Host_Id");
            CreateIndex("dbo.AspNetUsers", "Event_Id");
            AddForeignKey("dbo.Events", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.AspNetUsers", "Event_Id", "dbo.Events", "Id");
            AddForeignKey("dbo.Events", "Host_Id", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Events", "Host_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Categories", "Event_Id", "dbo.Events");
            DropForeignKey("dbo.AspNetUsers", "Event_Id", "dbo.Events");
            DropForeignKey("dbo.Events", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.AspNetUsers", new[] { "Event_Id" });
            DropIndex("dbo.Events", new[] { "Host_Id" });
            DropIndex("dbo.Events", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.Categories", new[] { "Event_Id" });
            DropColumn("dbo.AspNetUsers", "Event_Id");
            DropColumn("dbo.Events", "Host_Id");
            DropColumn("dbo.Events", "ApplicationUser_Id");
            DropTable("dbo.Categories");
        }
    }
}
