namespace EventCaveWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class AddedEventsToCategory : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("Categories", "Event_Id", "Events");
            DropIndex("dbo.Categories", new[] { "Event_Id" });
            CreateTable(
                "dbo.EventCategories",
                c => new
                {
                    Event_Id = c.Int(nullable: false),
                    Category_Id = c.Int(nullable: false),
                })
                .PrimaryKey(t => new { t.Event_Id, t.Category_Id })
                .ForeignKey("dbo.Events", t => t.Event_Id, cascadeDelete: true)
                .ForeignKey("dbo.Categories", t => t.Category_Id, cascadeDelete: true)
                .Index(t => t.Event_Id)
                .Index(t => t.Category_Id);

            DropColumn("dbo.Categories", "Event_Id");
        }

        public override void Down()
        {
            AddColumn("dbo.Categories", "Event_Id", c => c.Int());
            DropForeignKey("dbo.EventCategories", "Category_Id", "dbo.Categories");
            DropForeignKey("dbo.EventCategories", "Event_Id", "dbo.Events");
            DropIndex("dbo.EventCategories", new[] { "Category_Id" });
            DropIndex("dbo.EventCategories", new[] { "Event_Id" });
            DropTable("dbo.EventCategories");
            CreateIndex("dbo.Categories", "Event_Id");
            AddForeignKey("dbo.Categories", "Event_Id", "dbo.Events", "Id");
        }
    }
}
