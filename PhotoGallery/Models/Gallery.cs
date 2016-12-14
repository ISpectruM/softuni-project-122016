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
        [Key]
        public int Id { get; set; }

        [Required]
        [Index(IsUnique=true)]
        public string Name { get; set; }
    }
}