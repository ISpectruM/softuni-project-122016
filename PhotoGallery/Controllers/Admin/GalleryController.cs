using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using PhotoGallery.Models;

namespace PhotoGallery.Controllers.Admin
{
    public class GalleryController : Controller
    {
        // GET: Gallery
        public ActionResult List()
        {
            using (var db = new ApplicationDbContext())
            {
                var galleries  = db.Galleries
                    .Include(g=>g.Images)
                    .OrderBy(g => g.Name)
                    .ToList();
                
                return View(galleries);
            }
        }

        [Authorize(Roles = "Admin")]
        //GET: Gallery/Create
        public ActionResult Create()
        {
            return View();
        }

        //POST: Gallery/Create
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult CreateAction(Gallery gallery)
        {
            if (ModelState.IsValid)
            {
                using (var db = new ApplicationDbContext())
                {
                    db.Galleries.Add(gallery);
                    db.SaveChanges();
                }
            }

            return RedirectToAction("List");
        }

        //Render the galleri drop down menu
        public PartialViewResult GalleriesMenu()
        {
            using (var db = new ApplicationDbContext())
            {
                var model = new ImageViewModel();
                var galleries = db.Galleries
                    .Include(g => g.Images)
                    .OrderBy(g => g.Name)
                    .ToList();
                model.Galleries = galleries;

                return PartialView(model);
            }
        }


    }

}