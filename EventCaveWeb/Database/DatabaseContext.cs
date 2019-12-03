using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using EventCaveWeb.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using MySql.Data.EntityFramework;

namespace EventCaveWeb.Database
{
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
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