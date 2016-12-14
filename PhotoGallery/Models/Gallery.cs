using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PhotoGallery.Models
{
    public class Gallery
    {
        private ICollection<Image> images;

        public Gallery()
        {
            this.images = new HashSet<Image>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [Index(IsUnique=true)]
        [MaxLength(20)]
        public string Name { get; set; }

        public virtual ICollection<Image> Images { get; set; }
    }
}