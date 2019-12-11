using EventCaveWeb.Database;
using EventCaveWeb.Entities;
using Microsoft.AspNet.Identity.Owin;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace EventCaveWeb.Controllers.Api
{
    [Authorize]
    public class CategoriesController : ApiController
    {
        [Route("Api/Categories")]
        [HttpGet]
        public IQueryable<CategoryDto> List()
        {
            DatabaseContext db = HttpContext.Current.GetOwinContext().Get<DatabaseContext>();
            IQueryable<CategoryDto> categories = db.Categories.Select(c =>
                new CategoryDto()
                {
                    Id = c.Id,
                    Name = c.Name,
                    Description = c.Description,
                    Image = c.Image
                });
            return categories;
        }

        [Route("Api/Categories/{id}")]
        [HttpGet]
        public IHttpActionResult Single(int id)
        {
            DatabaseContext db = HttpContext.Current.GetOwinContext().Get<DatabaseContext>();
            CategoryDto categoryDto = db.Categories.Where(c => c.Id == id).Select(c =>
                new CategoryDto()
                {
                    Id = c.Id,
                    Name = c.Name,
                    Description = c.Description,
                    Image = c.Image
                }).FirstOrDefault();
            return Ok(categoryDto);
        }

        [Route("Api/Categories")]
        [HttpPost]
        public IHttpActionResult Post([FromBody] CategoryDto categoryDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            DatabaseContext db = HttpContext.Current.GetOwinContext().Get<DatabaseContext>();

            Category category = new Category()
            {
                Name = categoryDto.Name,
                Description = categoryDto.Description,
                Image = categoryDto.Image
            };

            db.Categories.Add(category);
            db.SaveChanges();

            return Ok(
                new
                {
                    Status = 200,
                    Message = "Category was successfully created."
                }
            );
        }

        [Route("Api/Categories/{id}")]
        [HttpPut]
        public IHttpActionResult Put(int id, [FromBody] CategoryDto categoryDto)
        {
            Category category = null;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            using (DatabaseContext db = HttpContext.Current.GetOwinContext().Get<DatabaseContext>())
            {
                category = db.Categories.Find(id);

                if (category == null)
                {
                    return BadRequest("Category not found.");
                }

                category.Name = categoryDto.Name;
                category.Description = categoryDto.Description;
                category.Image = categoryDto.Image;

                db.SaveChanges();
            }

            return Ok(
                new
                {
                    Status = 200,
                    Message = "Category was successfully updated."
                }
            );
        }

        [Route("Api/Categories/{id}")]
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            Category category;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            using (DatabaseContext db = HttpContext.Current.GetOwinContext().Get<DatabaseContext>())
            {
                category = db.Categories.Find(id);

                if (category == null)
                {
                    return BadRequest(string.Format("Category ID {0} not found.", id));
                }

                db.Categories.Remove(category);
                db.SaveChanges();
            }

            return Ok(
                new
                {
                    Status = 200,
                    Message = "Category was successfully deleted."
                }
            );
        }
    }
}