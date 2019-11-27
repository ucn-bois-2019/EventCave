namespace EventCaveWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedImagesToEventAndUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Events", "Images", c => c.String(unicode: false));
            AddColumn("dbo.AspNetUsers", "Picture", c => c.String(unicode: false));
            DropColumn("dbo.Events", "Public");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Events", "Public", c => c.Boolean(nullable: false));
            DropColumn("dbo.AspNetUsers", "Picture");
            DropColumn("dbo.Events", "Images");
        }
    }
}
