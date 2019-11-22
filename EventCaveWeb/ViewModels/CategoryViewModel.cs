using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EventCaveWeb.ViewModels
{
    public class CategoryViewModel
    {
        [MaxLength(15)]
        [Required]
        public string Name { get; set; }

        [MaxLength(50)]
        [Required]
        public string Description { get; set; }

        [Required]
        public string Image { get; set; }
    }
}