using Core.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace Core.Database
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