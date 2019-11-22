namespace EventCaveWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CategoriesAdded : DbMigration
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
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Categories");
        }
    }
}
