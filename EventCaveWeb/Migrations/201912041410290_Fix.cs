namespace EventCaveWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Fix : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Categories", "Image", c => c.String(nullable: false));
            AlterColumn("dbo.Events", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Events", "Images", c => c.String());
            AlterColumn("dbo.Events", "Location", c => c.String(nullable: false));
            AlterColumn("dbo.Events", "Datetime", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Events", "Description", c => c.String());
            AlterColumn("dbo.Events", "CreatedAt", c => c.DateTime(nullable: false));
            AlterColumn("dbo.AspNetUsers", "FirstName", c => c.String());
            AlterColumn("dbo.AspNetUsers", "LastName", c => c.String());
            AlterColumn("dbo.AspNetUsers", "Picture", c => c.String());
            AlterColumn("dbo.AspNetUsers", "Bio", c => c.String());
            AlterColumn("dbo.AspNetUsers", "RegisteredAt", c => c.DateTime(nullable: false));
            AlterColumn("dbo.AspNetUsers", "PasswordHash", c => c.String());
            AlterColumn("dbo.AspNetUsers", "SecurityStamp", c => c.String());
            AlterColumn("dbo.AspNetUsers", "PhoneNumber", c => c.String());
            AlterColumn("dbo.AspNetUsers", "LockoutEndDateUtc", c => c.DateTime());
            AlterColumn("dbo.AspNetUserClaims", "ClaimType", c => c.String());
            AlterColumn("dbo.AspNetUserClaims", "ClaimValue", c => c.String());
            AlterColumn("dbo.Tickets", "Subject", c => c.String());
            AlterColumn("dbo.Tickets", "Message", c => c.String());
            AlterColumn("dbo.Tickets", "Response", c => c.String());
            AlterColumn("dbo.Tickets", "SubmittedAt", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Tickets", "SubmittedAt", c => c.DateTime(nullable: false, precision: 0));
            AlterColumn("dbo.Tickets", "Response", c => c.String(unicode: false));
            AlterColumn("dbo.Tickets", "Message", c => c.String(unicode: false));
            AlterColumn("dbo.Tickets", "Subject", c => c.String(unicode: false));
            AlterColumn("dbo.AspNetUserClaims", "ClaimValue", c => c.String(unicode: false));
            AlterColumn("dbo.AspNetUserClaims", "ClaimType", c => c.String(unicode: false));
            AlterColumn("dbo.AspNetUsers", "LockoutEndDateUtc", c => c.DateTime(precision: 0));
            AlterColumn("dbo.AspNetUsers", "PhoneNumber", c => c.String(unicode: false));
            AlterColumn("dbo.AspNetUsers", "SecurityStamp", c => c.String(unicode: false));
            AlterColumn("dbo.AspNetUsers", "PasswordHash", c => c.String(unicode: false));
            AlterColumn("dbo.AspNetUsers", "RegisteredAt", c => c.DateTime(nullable: false, precision: 0));
            AlterColumn("dbo.AspNetUsers", "Bio", c => c.String(unicode: false));
            AlterColumn("dbo.AspNetUsers", "Picture", c => c.String(unicode: false));
            AlterColumn("dbo.AspNetUsers", "LastName", c => c.String(unicode: false));
            AlterColumn("dbo.AspNetUsers", "FirstName", c => c.String(unicode: false));
            AlterColumn("dbo.Events", "CreatedAt", c => c.DateTime(nullable: false, precision: 0));
            AlterColumn("dbo.Events", "Description", c => c.String(unicode: false));
            AlterColumn("dbo.Events", "Datetime", c => c.DateTime(nullable: false, precision: 0));
            AlterColumn("dbo.Events", "Location", c => c.String(nullable: false, unicode: false));
            AlterColumn("dbo.Events", "Images", c => c.String(unicode: false));
            AlterColumn("dbo.Events", "Name", c => c.String(nullable: false, unicode: false));
            AlterColumn("dbo.Categories", "Image", c => c.String(nullable: false, unicode: false));
        }
    }
}
