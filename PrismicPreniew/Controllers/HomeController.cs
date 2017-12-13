using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using prismic;
using prismic.fragments;
using System.Threading.Tasks;
using PrismicPreniew.Views.Models;

namespace PrismicPreniew.Controllers
{
    public class HomeController : Controller
    {
        public async Task<ActionResult> Index()
        {
            var key = "mycourusel";
            var cache = new DefaultCache();
            var imageList = cache.Get(key)?.ToObject<List<IndexViewModel>>();
            if (imageList == null)
            {
                var endpoint = "https://preview.prismic.io/api";
                var api = await Api.Get(endpoint);
                var document = await api.GetByUID("carousel", "mycouruselru");
                var docs = document.GetGroup("carousel.carouselimages").GroupDocs;

                var docIds = docs.Select(doc => ((DocumentLink)doc.Fragments["carouselimage"]).Id.ToString()).ToList();
                var result = await (api.GetByIDs(docIds, lang:"RU").Submit());

                var images = result.Results.Select(r =>
                {
                    return new IndexViewModel
                    {
                        Title = r.GetStructuredText("carouselimage.imagetitle").getTitle()?.Text,
                        OrdinalNumber = r.GetNumber("carouselimage.ordinalnumber")?.Value,
                        Description = r.GetStructuredText("carouselimage.imagedescription")?.AsHtml(new Resolver()).Replace("<p>", "").Replace("</p>", ""),
                        ImageUrl = r.GetImageView("carouselimage.image", "main").Url
                    };
                }).ToList();

                images[0].Active = true;
                cache.Set(key, 240000, Newtonsoft.Json.Linq.JToken.FromObject(images));
                imageList = cache.Get(key).ToObject<List<IndexViewModel>>();
            }

            return View(imageList);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }

    internal class Resolver : DocumentLinkResolver
    {
        public override string Resolve(DocumentLink link)
        {
            return "";
        }
    }

}