using System.Web.Mvc;

namespace EventCaveWeb.Controllers
{
    public class HomeController : Controller
    {
        [Route("")]
        public ActionResult Index()
        {
            return View();
        }
    }
}