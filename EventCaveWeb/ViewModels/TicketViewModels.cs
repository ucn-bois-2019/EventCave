using EventCaveWeb.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EventCaveWeb.ViewModels
{
    public class CreateUpdateTicketViewModel
    {
        [Required]
        public string Subject { get; set; }
        [Required]
        public string Message { get; set; }
    }

    public class DetailTicketViewModel
    {
        [Required]
        public string Subject { get; set; }
        [Required]
        public string Message { get; set; }
        [Required]
        public string Response { get; set; }
        [Required]
        public bool Resolved { get; set; }
        [Required]
        public DateTime SubmittedAt { get; set; }
    }

    public class TicketListingViewModel
    {
        public IEnumerable<Ticket> ResolvedTickets { get; set; }
        public IEnumerable<Ticket> PendingTickets { get; set; }
    }
}