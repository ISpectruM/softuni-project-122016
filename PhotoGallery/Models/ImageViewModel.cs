﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhotoGallery.Models
{
    public class ImageViewModel: ApplicationUser
    {
        public IList<Image> Images { get; set; }
    }
}