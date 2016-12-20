using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PhotoGallery.Models
{
    public class Image
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(255)]
        public string Title { get; set; }

        [ForeignKey("Author")]
        public string AuthorId { get; set; }
        public virtual ApplicationUser Author { get; set; }

        [ForeignKey("Gallery")]
        public int GalleryId { get; set; }
        public virtual Gallery Gallery { get; set; }

        public string Path { get; set; }

        public int Vote { get; set; }
    }
}