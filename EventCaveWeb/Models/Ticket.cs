using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventCaveWeb.Models
{
    public class Ticket
    {
        public int Id { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public string Response { get; set; }
        public bool Resolved { get; set; }
        public DateTime SubmittedAt { get; set; }
        public virtual ApplicationUser Sender { get; set; }
    }
}