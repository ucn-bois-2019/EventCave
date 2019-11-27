using System;
using System.Collections.Generic;

namespace Core.Models
{
    public class Event
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public DateTime Datetime { get; set; }
        public bool Public { get; set; }
        public int Limit { get; set; }
        public DateTime CreatedAt { get; set; }
        public virtual User Host { get; set; }
        public virtual ICollection<User> Attendees { get; set; }
        public virtual ICollection<Category> Categories { get; set; }
    }
}