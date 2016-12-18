using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using PhotoGallery.Models;

namespace PhotoGallery.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Details(ApplicationUser user)
        {
            var model = new UserDetailsViewModel();
            model.Images = GetImagesByUser(user);

            return View(model);
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