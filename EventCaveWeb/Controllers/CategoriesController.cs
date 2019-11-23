using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EventCaveWeb.Database;
using EventCaveWeb.Models;
using EventCaveWeb.ViewModels;
using Microsoft.AspNet.Identity.Owin;

namespace EventCaveWeb.Controllers
{
    public class CategoriesController : Controller
    {
        private  DatabaseContext db = new DatabaseContext();

        [Route("Categories")]
        [HttpGet]
        [Authorize]
        [AllowAnonymous]
        public ActionResult GetCategories(Category category)
        {
            var categories = db.Categories.ToList();
            return View(categories);
        }

    
    }
}
