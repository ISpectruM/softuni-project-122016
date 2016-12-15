using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PhotoGallery.Models;

namespace PhotoGallery.Controllers
{
    public class ImageController : Controller
    {
        // GET: Image
        public ActionResult List()
        {
            using (var db = new ApplicationDbContext())
            {
                var images = db.Images
                    .Include(a=>a.Author)
                    .ToList();
            }
            return View();
        }

    }
}