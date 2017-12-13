using prismic;
using prismic.fragments;
using PrismicPreview.Views.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PrismicPreview.Helpers
{
    public static class PrismicHelper
    {
        public static IndexViewModel ToIndexViewModel(this Document document)
        {
            return new IndexViewModel
            {
                Url = document.GetImageView("carouselimage.image", "main")?.Url,
                Title = document.GetText("carouselimage.imagetitle"),
                OrdinalNumber = document.GetNumber("carouselimage.ordinalnumber")?.Value ?? 0,
                Description = document.GetText("carouselimage.imagedescription"),
                ExternalLink = ((WebLink)document.GetLink("carouselimage.externallink"))?.Url
            };
        }

        public static ContentBlockViewModel ToContentBlockViewModel(this Document document)
        {
            return new ContentBlockViewModel
            {
                BlockTitle = document.GetText("contentblock.blocktitle"),
                Content = document.GetText("contentblock.content")
            };
        }

        //public T Map<T>(Document documnt)
        //{
        //    var images = result.Results.Select(r => new IndexViewModel
        //    {
        //        Url = r.GetImageView("carouselimage.image", "main")?.Url,
        //        Title = r.GetText("carouselimage.imagetitle"),
        //        OrdinalNumber = r.GetNumber("carouselimage.ordinalnumber")?.Value ?? 0,
        //        Description = r.GetText("carouselimage.imagedescription"),
        //        ExternalLink = ((WebLink)r.GetLink("carouselimage.externallink"))?.Url
        //    }).OrderBy(i => i.OrdinalNumber).ToList();
        //}
    }
}