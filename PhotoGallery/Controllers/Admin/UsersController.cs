using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
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
        public ActionResult List(ApplicationUser user)
        {
            using (var db = new ApplicationDbContext())
            {
                var model = new UsersViewModel();
                //var imagesByUser = GetImagesByUser(user);
                var users = db.Users
                    .ToList();
                model.Users = users;
                //model.ImagesByUser = imagesByUser;

                return View(model);
            }
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

        //Get images by user
        public IList<Image> GetImagesByUser(ApplicationUser user)
        {
            using (var db = new ApplicationDbContext())
            {
                var images = db.Images.Where(a => a.Author.UserName == user.UserName).ToList();

                return images;
            }
        }


    }
}