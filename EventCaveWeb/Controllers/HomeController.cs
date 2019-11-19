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
            return View();
        }
    }
}