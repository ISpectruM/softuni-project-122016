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
        [Authorize]
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

                    if (photo != null && photo.ContentLength > 0)
                    {
                        if (!IsFileValid(photo.ContentType))
                        {
                            ViewBag.Message = "Only JPEG, JPG, PNG or GIF files are allowed!";
                            return View("Upload");
                        }
                        else if (!IsFileSizeValid(photo.ContentLength))
                        {
                            ViewBag.Message = "The file size must be up to 1MB!";
                            return View("Upload");
                        }

                        try
                        {
                            var fileName = Path.GetFileName(photo.FileName);
                            fileName = fileName.Replace(" ", "_");
                            var path = Path.Combine(Server.MapPath("~/Images"), fileName);
                            photo.SaveAs(path);

                            var authorId = db.Users
                            .Where(a => a.UserName == this.User.Identity.Name)
                            .First()
                            .Id;

                            image.AuthorId = authorId;
                            image.Title = model.Title;
                            image.GalleryId = model.GalleryId;
                            image.Path = fileName;

                            db.Images.Add(image);
                            db.SaveChanges();

                            ViewBag.Message = "File uploaded successfully";
                        }
                        catch (Exception ex)
                        {

                            return ViewBag.Message = "ERROR:" + ex.Message.ToString();
                        }
                    }
                    else
                    {
                        return ViewBag.Message = "You have not specified a file.";
                    }
                }
            }
            return RedirectToAction("Upload");
        }

        private bool IsFileValid(string contentType)
        {
            return contentType.Equals("image/jpeg") || contentType.Equals("image/jpg") ||
                   contentType.Equals("image/png") || contentType.Equals("image/gif");
        }

        private bool IsFileSizeValid(int fileSize)
        {
            return ((fileSize/1024)/1024) < 3;
        }
    }
}