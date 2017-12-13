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
        static DefaultCache cache = new DefaultCache();

        public async Task<ActionResult> Index()
        {
            var key = "mycourusel";
            var imageList = cache.Get(key)?.ToObject<List<IndexViewModel>>();
            if (imageList == null)
            {
                var endpoint = "https://preview.prismic.io/api";
                var api = await Api.Get(endpoint);
                var document = await api.GetByUID("carousel", "mycouruselru");
                var docs = document.GetGroup("carousel.carouselimages").GroupDocs;

                var docIds = docs.Select(doc => ((DocumentLink)doc.Fragments.Values.First()).Id).ToList();
                var result = await (api.GetByIDs(docIds).Submit());

                var images = result.Results.Select(r => new IndexViewModel
                         {
                             Url = r.GetImageView("carouselimage.image", "main")?.Url,
                             Title = r.GetText("carouselimage.imagetitle"),
                             OrdinalNumber = r.GetNumber("carouselimage.ordinalnumber")?.Value ?? 0,
                             Description = r.GetText("carouselimage.imagedescription"),
                             ExternalLink = ((WebLink)r.GetLink("carouselimage.externallink"))?.Url
                         }).OrderBy(i => i.OrdinalNumber).ToList();
                images[0].Active = true;
                cache.Set(key, 240000, Newtonsoft.Json.Linq.JToken.FromObject(images));
                imageList = cache.Get(key).ToObject<List<IndexViewModel>>();
            }


            return View(imageList);
        }
        public async Task<ActionResult> About()
        {
            var key = "e390bee5-52df-49b6-8aa0-ab5d3c97d1c8";
            //cache.Set(key, 1, Newtonsoft.Json.Linq.JToken.FromObject(""));
            var imageList = cache.Get(key)?.ToObject<List<IndexViewModel>>();
            if (imageList == null)
            {
                var endpoint = "https://preview.prismic.io/api";
                var api = await Api.Get(endpoint);
                var document = await api.GetByUID("carousel", key);
                var docs = document.GetGroup("carousel.carouselimages").GroupDocs;

                var docIds = docs.Select(doc => ((DocumentLink)doc.Fragments.Values.First()).Id).ToList();
                var result = await (api.GetByIDs(docIds).Submit());

                var images = result.Results.Select(r => new IndexViewModel
                {
                    Url = r.GetImageView("carouselimage.image", "main")?.Url,
                    Title = r.GetText("carouselimage.imagetitle"),
                    OrdinalNumber = r.GetNumber("carouselimage.ordinalnumber")?.Value ?? 0,
                    Description = r.GetText("carouselimage.imagedescription"),
                    ExternalLink = ((WebLink)r.GetLink("carouselimage.externallink"))?.Url
                }).OrderBy(i => i.OrdinalNumber).ToList();
                images[0].Active = true;
                cache.Set(key, 240000, Newtonsoft.Json.Linq.JToken.FromObject(images));
                imageList = cache.Get(key).ToObject<List<IndexViewModel>>();
            }
            return View(imageList);
        }

        public async Task<ActionResult> ContentBlock()
        {
            ViewBag.Message = "Your contact page.";
            var key = "block1ru";
            var contentBlock = cache.Get(key)?.ToObject<ContentBlockViewModel>();
            if (contentBlock == null)
            {
                var endpoint = "https://preview.prismic.io/api";
                var api = await Api.Get(endpoint);
                var document = await api.GetByUID("contentblock", key);
                var viewModel = new ContentBlockViewModel
                {
                    BlockTitle = document.GetText("contentblock.blocktitle"),
                    Content = document.GetText("contentblock.content")
                };

                cache.Set(key, 240000, Newtonsoft.Json.Linq.JToken.FromObject(viewModel));
                contentBlock = cache.Get(key).ToObject<ContentBlockViewModel>();
            }
            return View(contentBlock);
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