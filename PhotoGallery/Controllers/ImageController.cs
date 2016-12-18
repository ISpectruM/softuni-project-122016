using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
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
                    .Include(a => a.Author)
                    .ToList();

                model.Images = images;
                return View(model);
            }
        }

        //GET: Image/Upload
        public ActionResult Upload()
        {
            using (var db = new ApplicationDbContext())
            {
                var model = new ImageViewModel();
                var galleries = db.Galleries
                    .OrderBy(g => g.Name)
                    .ToList();
                model.Galleries = galleries;
                return View(model);
            }
        }


        //POST: Image/Upload
        [HttpPost]
        public ActionResult Upload(ImageViewModel model, HttpPostedFileBase photo)
        {
            if (ModelState.IsValid)
            {
                using (var db = new ApplicationDbContext())
                {
                var image = new Image();

                    var authorId = db.Users
                                .Where(a => a.UserName == this.User.Identity.Name)
                                .First()
                                .Id;
                    image.AuthorId = authorId;
                    
                    //image upload
                    if (photo != null && photo.ContentLength > 0)
                    {
                        try
                        {
                                var fileName = Path.GetFileName(photo.FileName);
                                fileName = fileName.Replace(" ", "_");
                                var path = Path.Combine(Server.MapPath("~/Images"),fileName);
                                photo.SaveAs(path);

                                image.Path = path;

                                ViewBag.Message = "File uploaded successfully";
                        }
                        catch (Exception ex)
                        {

                            ViewBag.Message = "ERROR:" + ex.Message.ToString();
                        }
                    }
                    else
                    {
                        ViewBag.Message = "You have not specified a file.";
                    }

                    image.Title = model.Title;
                    image.GalleryId = model.GalleryId;

                    db.Images.Add(image);
                    db.SaveChanges();
                }
            }
            return RedirectToAction("List");
        }
    }
}