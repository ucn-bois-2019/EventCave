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

        /* [Route("Api/Categories")]
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

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        } */
    }
}