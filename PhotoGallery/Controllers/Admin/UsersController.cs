using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using PhotoGallery.Models;

namespace PhotoGallery.Controllers.Admin
{
    [Authorize]
    public class UsersController : Controller
    {
        //Get index according to logged user/admin
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = User.Identity;
                ViewBag.Name = user.Name;

                if (IsAdminUser())
                {
                    return View("AdminPage");
                }

                return RedirectToAction("UserPage");
            }
            return RedirectToAction("Index", "Home");
        }

        //Get: User/UserPage
        public ActionResult UserPage(int?id)
        {
            if (User.Identity.IsAuthenticated)
            {
                return View();
            }
            return RedirectToAction("Index", "Home");
        }

        //Get: Users/List
        [Authorize(Roles = "Admin")]
        public ActionResult List()
        {
            var context = new ApplicationDbContext();
            var users = context.Users.ToList();

            return View(users);
        }

        private bool IsAdminUser()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = User.Identity;
                ApplicationDbContext context = new ApplicationDbContext();
                var userManager  = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

                var userRole = userManager.GetRoles(user.GetUserId());

                if (userRole[0] == "Admin")
                {
                    return true;
                }
                return false;
            }
            return false;
        }
    }
}