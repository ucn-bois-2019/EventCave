using System;
using System.Collections.Generic;

namespace Core.Models
{
    public class User
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Bio { get; set; }
        public DateTime RegisteredAt { get; set; }
        public ICollection<Event> EventsEnrolledIn { get; set; }
        public ICollection<Event> EventsHosted { get; set; }
    }
}