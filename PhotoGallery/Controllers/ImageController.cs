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
        // GET: Image/List
        public ActionResult List()
        {
            using (var db = new ApplicationDbContext())
            {
               var model = new ImageViewModel(); 
               var images = db.Images
                    .Include(a=>a.Author)
                    .ToList();

                model.Images = images;
                return View(model);
            }
        }

        //GET: Image/Upload
        public ActionResult Upload()
        {
            
        }

    }
}