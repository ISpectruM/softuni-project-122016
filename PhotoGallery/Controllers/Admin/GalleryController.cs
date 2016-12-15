using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PhotoGallery.Models;

namespace PhotoGallery.Controllers.Admin
{
    [Authorize(Roles = "Admin")]
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

        //GET: Gallery/Create
        public ActionResult Create()
        {
            return View();
        }

        //POST: Gallery/Create
        [HttpPost]
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


    }

}