using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Core.Models
{
    public class Category
    {
        public int Id { get; set; }

        [MaxLength(15)]
        [Required]
        public string Name { get; set; }

        [MaxLength(50)]
        [Required]
        public string Description { get; set; }

        [Required]
        public string Image { get; set; }
        public virtual ICollection<Event> Events { get; set; }
    }

    public class CategoryDto
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        public string Image { get; set; }
    }
}