using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhotoGallery.Models
{
    public class UserDetailsViewModel
    {
        public ApplicationUser User { get; set; }

        public Image Image { get; set; }

        public IList<Image> Images { get; set; }

    }
}