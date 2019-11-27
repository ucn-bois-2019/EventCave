using EventCaveWeb.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EventCaveWeb.ViewModels
{
    public class HomeViewModel
    {
        [Display(Prompt = "What are you loooking for?")]
        public string Keyword { get; set; }
        [Display(Prompt = "Location")]
        public string Location { get; set; }
        [DataType(DataType.Date)]
        [Display(Prompt = "When?")]
        public DateTime DateTime { get; set; }
        public IQueryable<Category> Categories { get; set; }
        public ICollection<Event> RandomEvents { get; set; }
    }
}