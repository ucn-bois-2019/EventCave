namespace Core.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class AddedTickets : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Tickets",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Subject = c.String(unicode: false),
                    Message = c.String(unicode: false),
                    Response = c.String(unicode: false),
                    Resolved = c.Boolean(nullable: false),
                    SubmittedAt = c.DateTime(nullable: false, precision: 0),
                    Sender_Id = c.String(maxLength: 128, storeType: "nvarchar"),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Sender_Id)
                .Index(t => t.Sender_Id);

        }

        public override void Down()
        {
            DropForeignKey("dbo.Tickets", "Sender_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Tickets", new[] { "Sender_Id" });
            DropTable("dbo.Tickets");
        }
    }
}
