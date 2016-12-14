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
    [Authorize(Roles = "Admin")]
    public class UsersController : Controller
    {
        //Get: Index
        public ActionResult AdminPage()
        {
            return View();
        }

        //Get: Users/List
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