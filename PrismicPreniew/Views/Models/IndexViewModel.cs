﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PrismicPreview.Views.Models
{
    public class IndexViewModel
    {
        public string Url { get; set; }
        public decimal? OrdinalNumber { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool Active { get; set; }
        public string ExternalLink { get; set; }
    }
}