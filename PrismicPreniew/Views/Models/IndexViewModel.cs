using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PrismicPreniew.Views.Models
{
    public class IndexViewModel
    {
        public string Url { get; set; }
        public decimal OrdinalNumber { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool Active { get; set; }
    }
}