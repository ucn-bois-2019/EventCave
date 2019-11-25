using EventCaveWeb.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EventCaveWeb.ViewModels
{
    public class CreateUpdateEventViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Location { get; set; }
        [Required]
        public DateTime Datetime { get; set; }
        [Required]
        public bool Public { get; set; }
        [Required]
        public int Limit { get; set; }
    }

    public class EventDetailViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public DateTime Datetime { get; set; }
        public bool Public { get; set; }
        public int Limit { get; set; }
        public ApplicationUser Host { get; set; }
        public int AttendeeCount { get; set; }
        public int SpacesLeft { get; set; }
        public ICollection<Category> Categories { get; set; }
    }
}