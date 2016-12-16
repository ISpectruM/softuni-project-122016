using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhotoGallery.Models
{
    public class UsersViewModel
    {
        public IList<ApplicationUser> Users { get; set; }

        public IList<Image> Images { get; set; }

        //public IList<Image> ImagesByUser { get; set; }


        public IList<Image> ImagesByUser(ApplicationUser user)
        {
            using (var db = new ApplicationDbContext())
            {
                var images = db.Images.Where(a => a.Author.UserName == user.UserName).ToList();

                return images;
            }
        }

    }
}