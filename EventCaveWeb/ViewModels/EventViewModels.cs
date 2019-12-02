using EventCaveWeb.Models;
using EventCaveWeb.Utils;
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
        public string Images { get; set; }
        public string Description { get; set; }
        [Required]
        public string Location { get; set; }
        [Required]
        public DateTime Datetime { get; set; }
        public int Limit { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<int> SelectedCategoryIds { get; set; }
    }

    public class EventDetailViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public DateTime Datetime { get; set; }
        public int Limit { get; set; }
        public ApplicationUser Host { get; set; }
        public int AttendeeCount { get; set; }
        public int SpacesLeft { get; set; }
        public ICollection<Category> Categories { get; set; }
        public IEnumerable<ImgurImage> Images { get; set; }
        public bool Going { get; set; }
        public ApplicationUser AuthenticatedUser { get; set; }
        
    }
}