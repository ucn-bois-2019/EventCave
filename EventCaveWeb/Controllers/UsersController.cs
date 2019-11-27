using EventCaveWeb.Database;
using EventCaveWeb.Models;
using EventCaveWeb.Utils;
using EventCaveWeb.ViewModels;
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
    [RoutePrefix("Users")]
    public class UsersController : Controller
    {
        [Route("{username}")]
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> Profile(string username)
        {
            var _userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            ApplicationUser user = await _userManager.FindByNameAsync(username);
            DetailUserProfileViewModel detailUserProfileViewModel = new DetailUserProfileViewModel()
            {
                UserName = user.UserName,
                Picture = Imgur.Instance.GetImage(user.Picture),
                Bio = user.Bio,
                RegisteredAt = user.RegisteredAt
            };
            return View(detailUserProfileViewModel);
        }

        [Route("{username}/Edit")]
        [HttpGet]
        [Authorize]
        public async Task<ActionResult> Edit(string username)
        {
            if (username != User.Identity.Name)
            {
                return RedirectToAction("Index", "Home");
            }
            var _userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            ApplicationUser user = await _userManager.FindByNameAsync(username);
            EditUserProfileViewModel editUserProfileViewModel = new EditUserProfileViewModel()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Picture = user.Picture,
                Bio = user.Bio
            };
            return View(editUserProfileViewModel);
        }

        [Route("{username}/Edit")]
        [HttpPost]
        [Authorize]
        public async Task<ActionResult> Edit(string username, EditUserProfileViewModel editUserProfileViewModel)
        {
            var _userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            ApplicationUser user = await _userManager.FindByIdAsync(User.Identity.GetUserId());
            if (username != User.Identity.Name)
            {
                return RedirectToAction("Index", "Home");
            }
            if (ModelState.IsValid)
            {
                user.FirstName = editUserProfileViewModel.FirstName;
                user.LastName = editUserProfileViewModel.LastName;
                user.Email = editUserProfileViewModel.Email;
                user.Picture = editUserProfileViewModel.Picture;
                user.Bio = editUserProfileViewModel.Bio;
                _userManager.Update(user);
            }
            return RedirectToAction("Profile", "Users", new { username = user.UserName });
        }

        [Route("{username}/Edit/ChangePassword")]
        [HttpGet]
        [Authorize]
        public ActionResult ChangePassword(string username)
        {
            return View();
        }

        [Route("{username}/Edit/ChangePassword")]
        [HttpPost]
        [Authorize]
        public async Task<ActionResult> ChangePassword(string username, ChangeUserPasswordViewModel changeUserPasswordViewModel)
        {
            if (username != User.Identity.Name)
            {
                return RedirectToAction("Index", "Home");
            }
            var _userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            ApplicationUser user = await _userManager.FindByIdAsync(User.Identity.GetUserId());
            if (ModelState.IsValid)
            {
                await _userManager.ChangePasswordAsync(
                    User.Identity.GetUserId(), 
                    changeUserPasswordViewModel.OldPassword, 
                    changeUserPasswordViewModel.NewPassword
                );
            }
            return RedirectToAction("Profile", "Users", new { username = user.UserName });
        }
    }
}