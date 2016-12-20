using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhotoGallery.Models
{
    public class ImageViewModel
    {
        public int? Id { get; set; }
        public string Title { get; set; }
        public int GalleryId { get; set; }
        public ICollection<Image> Images { get; set; }
        public ICollection<Gallery> Galleries { get; set; }
        public HttpPostedFileBase Photo { get; set; }
        public Image Image { get; set; }
        public Gallery Gallery { get; set; }

    }
}