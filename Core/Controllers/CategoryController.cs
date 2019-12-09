using Core.Database;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Controllers
{
    public class CategoryController
    {
        public ICollection<Category> GetAll()
        {
            ICollection<Category> events = new List<Category>();

            using (var db = new DatabaseContext())
            {
                events = (ICollection<Category>)db.Categories;
            }

            return events;
        }
    }
}
