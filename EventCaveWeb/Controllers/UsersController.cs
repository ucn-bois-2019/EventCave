using EventCaveWeb.Database;
using EventCaveWeb.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace EventCaveWeb.Controllers
{
    [RoutePrefix("users")]
    public class UsersController : Controller
    {

        [Route("{UserName}")]
        [HttpGet]
        [Authorize]
        [AllowAnonymous]
        public async Task<ActionResult> Profile(string UserName)
        {
            var _userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            ApplicationUser user = await _userManager.FindByNameAsync(UserName);
            return View(user);
        }
    }
}