using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using PhotoGallery.Models;

namespace PhotoGallery.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Details()
        {
            var id = User.Identity.GetUserId();

            var model = new UserDetailsViewModel();
            model.Images = GetImagesByUser(id);

            using (var db = new ApplicationDbContext())
            {
                var user = db.Users.First(u => u.Id == id);
                model.User = user;
            }

            return View(model);
        }

        //Get images by user
        public IList<Image> GetImagesByUser(string id)
        {
            using (var db = new ApplicationDbContext())
            {
                var images = db.Images
                    .Where(a => a.AuthorId == id)
                    .Include(i => i.Author)
                    .ToList();

                return images;
            }
        }

        ////Upload an avatar image
        //public ActionResult Avatar(string id)
        //{
            
        //}

    }
}