using System.Linq;
using System.Web;
using System.Web.Mvc;
using EventCaveWeb.Database;
using EventCaveWeb.Entities;
using Microsoft.AspNet.Identity.Owin;

namespace EventCaveWeb.Controllers
{
    [RoutePrefix("Categories")]
    public class CategoriesController : Controller
    {
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Index(Category category)
        {
            DatabaseContext db = HttpContext.GetOwinContext().Get<DatabaseContext>();
            var categories = db.Categories.ToList();
            return View(categories);
        }
    }
}
