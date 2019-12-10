using EventCaveWeb.Models;
using EventCaveWeb.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace EventCaveWeb.ViewModels
{
    public class CreateUpdateEventViewModel
    {
        public int Id { get; set; }

        [StringLength(15)]
        [Required]
        [Display(Name = "Title")]
        public string Name { get; set; }

        [StringLength(25)]
        public string Images { get; set; }

        [StringLength(10000)]
        public string Description { get; set; }

        [Required]
        public string Location { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime Datetime { get; set; }

        public int Limit { get; set; }

        [Required]
        public IEnumerable<Category> Categories { get; set; }

        [Display(Name = "Categories")]
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
        public bool Going { get; set; }
        public ApplicationUser Host { get; set; }
        public ImgurImage HostPicture { get; set; }
        public int AttendeeCount { get; set; }
        public int SpacesLeft { get; set; }
        public ICollection<Category> Categories { get; set; }
        public IEnumerable<ImgurImage> Images { get; set; }
    }
}