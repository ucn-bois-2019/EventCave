using EventCaveWeb.Database;
using EventCaveWeb.Models;
using Microsoft.AspNet.Identity.Owin;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EventCaveWeb.Controllers
{
    [RoutePrefix("")]
    public class HomeController : Controller
    {
        [AllowAnonymous]
        [Route("")]
        public ActionResult Index()
        {
            DatabaseContext db = HttpContext.GetOwinContext().Get<DatabaseContext>();
            return View(db.Categories.Take(4));
        }
    }
}