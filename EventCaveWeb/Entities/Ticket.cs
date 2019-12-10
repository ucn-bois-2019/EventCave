using System;

namespace EventCaveWeb.Entities
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

    public class TicketDto
    {
        public int Id { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public string Response { get; set; }
        public bool Resolved { get; set; }
        public DateTime SubmittedAt { get; set; }
        public string SenderId { get; set; }
        public string SenderUsername { get; set; }
    }

    public class TicketResolveDto
    {
        public string Response { get; set; }
    }
}