using EventCaveWeb.Entities;
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

        public IEnumerable<int> SelectedCategoryIds { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<Event> RandomEvents { get; set; }
    }
}