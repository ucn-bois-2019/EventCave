using System.Data.Entity;
using EventCaveWeb.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace EventCaveWeb.Database
{
    public class DatabaseContext : IdentityDbContext<ApplicationUser>
    {
        public virtual DbSet<Event> Events { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<UserEvent> UserEvents { get; set; }
        public virtual DbSet<Ticket> Tickets { get; set; }

        public DatabaseContext() : base("DefaultConnection")
        {
        }

        public static DatabaseContext Create()
        {
            return new DatabaseContext();
        }
    }
}