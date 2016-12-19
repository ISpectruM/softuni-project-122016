using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using PhotoGallery.Models;

namespace PhotoGallery.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Details(string id)
        {
            var model = new UserDetailsViewModel();
            model.Images = GetImagesByUser(id);

            return View(model);
        }

        //Get images by user
        public IList<Image> GetImagesByUser(string id)
        {
            using (var db = new ApplicationDbContext())
            {
                var images = db.Images.Where(a => a.AuthorId == id).ToList();

                return images;
            }
        }


    }
}