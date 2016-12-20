using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
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

        //GET: Image/Details
        public ActionResult Details(int? id)
        {
            using (var db = new ApplicationDbContext())
            {
                var model = new ImageViewModel();
                var image = db.Images.First(i => i.Id == id);
                model.Image = image;

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
        [Authorize]
        public ActionResult Upload(ImageViewModel model, HttpPostedFileBase photo, HttpPostedFileBase avatar)
        {
            using (var db = new ApplicationDbContext())
            {
                model.Galleries = GetAllGalleries(db);

                var fileForUpload = photo ?? avatar;
                if (fileForUpload != null && fileForUpload.ContentLength > 0)
                {
                    if (!IsFileValid(fileForUpload.ContentType))
                    {
                        ViewBag.Message = "Only JPEG, JPG, PNG or GIF files are allowed!";
                        return View(model);
                    }
                    else if (!IsFileSizeValid(fileForUpload.ContentLength))
                    {
                        ViewBag.Message = "The file size must be up to 1MB!";
                        return View(model);
                    }

                    try
                    {
                        var image = new Image();

                        var fileName = Path.GetFileName(fileForUpload.FileName);
                        fileName = fileName.Replace(" ", "_");

                        //Upload photo to galleries
                        if (photo != null)
                        {
                            var path = Path.Combine(Server.MapPath("~/Images"), fileName);
                            fileForUpload.SaveAs(path);

                            var authorId = GetAuthorId(db);

                            image.AuthorId = authorId;
                            image.Title = model.Title;
                            image.GalleryId = model.GalleryId;
                            image.Path = fileName;

                            db.Images.Add(image);
                            db.SaveChanges();
                        }

                        //Upload avatar image
                        else if (avatar != null)
                        {
                            var path = Path.Combine(Server.MapPath("~/Images/Avatar"), fileName);
                            fileForUpload.SaveAs(path);

                            var currentUserId = User.Identity.GetUserId();
                            var user = db.Users
                                .First(u => u.Id == currentUserId);
                            user.Avatar = fileName;
                            db.Entry(user).State = EntityState.Modified;
                            db.SaveChanges();

                            ViewBag.Message = "File uploaded successfully";

                            return RedirectToAction("Details", "User");
                        }
                        ViewBag.Message = "File uploaded successfully";

                        return View(model);

                    }
                    catch (Exception ex)
                    {

                        ViewBag.Message = "ERROR:" + ex.Message.ToString();
                        return View(model);
                    }
                }
                else
                {
                    ViewBag.Message = "You have not specified a file.";
                    return View(model);
                }
            }
        }

        //Get votes
        public ActionResult Vote(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            using (var db = new ApplicationDbContext())
                {
                    var model = new ImageViewModel();
                    var image = db.Images.First(i => i.Id == id);

                    var imgVotes = db.Images.First(i => i.Id == id).Vote;
                    imgVotes++;
                    image.Vote = imgVotes;

                    model.Image = image;
                    
                    db.Entry(image).State = EntityState.Modified;
                    db.SaveChanges();

                    return View(model);
                }
        }

        private ICollection<Gallery> GetAllGalleries(ApplicationDbContext db)
        {
            var galleries = db.Galleries
                .OrderBy(g => g.Name)
                .ToList();
            return galleries;
        }

        private string GetAuthorId(ApplicationDbContext db)
        {
            var authorId = db.Users
                .Where(a => a.UserName == this.User.Identity.Name)
                .First()
                .Id;
            return authorId;
        }

        private bool IsFileValid(string contentType)
        {
            return contentType.Equals("image/jpeg") || contentType.Equals("image/jpg") ||
                   contentType.Equals("image/png") || contentType.Equals("image/gif");
        }

        private bool IsFileSizeValid(int fileSize)
        {
            return ((fileSize / 1024) / 1024) < 3;
        }
    }
}