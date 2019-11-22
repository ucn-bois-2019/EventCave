using EventCaveWeb.Models;
using System;
using System.Data.Entity.Migrations;
using System.Linq;

namespace EventCaveWeb.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<EventCaveWeb.Database.DatabaseContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(EventCaveWeb.Database.DatabaseContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
            var users = from user in context.Users
                        where user.UserName == "testuser"
                        select user;
            ApplicationUser TestUser = users.First();

            context.Categories.AddOrUpdate(x => x.Id,
                new Category() { Name = "City tour", Description = "Explore the city.", Image = "https://i.imgur.com/7GNgVgp.jpg" },
                new Category() { Name = "Drinks", Description = "The night is yours.", Image = "https://i.imgur.com/fvmSyjD.jpg" },
                new Category() { Name = "Education", Description = "Meet students and professionals in the field.", Image = "https://i.imgur.com/VewhUEL.jpg" },
                new Category() { Name = "Gambling", Description = "Don't leave your purse home tonight.", Image = "https://i.imgur.com/IjC4N45.jpg" },
                new Category() { Name = "Employment", Description = "Find a job you'll love.", Image = "https://i.imgur.com/81C4w1F.png" }
                );

            var categories = from cat in context.Categories
                             select cat;

            context.Events.AddOrUpdate(x => x.Id,
                new Event() { Name = "City exploration", Description = "A guided tour of the city with the best places to hang out.", Location = "Aalborg, Denmark", Datetime = DateTime.Parse("10.1.2019"), CreatedAt = DateTime.Now, Public = true, Host = TestUser, Categories = categories.Where(x => x.Name.Equals("City tour")).ToList() },
                new Event() { Name = "Friday bar", Description = "A great place and time to meet new people and have a great time.", Location = "Aarhus, Denmark", Datetime = DateTime.Parse("10.3.2019"), CreatedAt = DateTime.Now, Public = true, Host = TestUser, Categories = categories.Where(x => x.Name.Equals("Drinks")).ToList() },
                new Event() { Name = "Job seminar", Description = "Need a job? Meet with the experts and companies from the area", Location = "London, England", Datetime = DateTime.Parse("7.11.2019"), CreatedAt = DateTime.Now, Public = true, Host = TestUser, Categories = categories.Where(x => x.Name.Equals("Employment")).ToList() },
                new Event() { Name = "Casino night", Description = "Explore the nightlife in the city's best clubs and casinos.", Location = "Aalborg, Denmark", Datetime = DateTime.Parse("8.2.2012"), CreatedAt = DateTime.Now, Public = true, Host = TestUser, Categories = categories.Where(x => x.Name.Equals("Gambling")).ToList() },
                new Event() { Name = "Sunset bbq", Description = "Enjoy a cold beer with a freshly-grilled sausage at the beach.", Location = "Skagen, Denmark", Datetime = DateTime.Parse("9.10.2019"), CreatedAt = DateTime.Now, Public = true, Host = TestUser, Categories = categories.Where(x => x.Name.Equals("Drinks")).ToList() }
                );
        }
    }
}
