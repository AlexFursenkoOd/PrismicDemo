using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using prismic;
using prismic.fragments;
using System.Threading.Tasks;
using PrismicPreview.Views.Models;
using PrismicPreview.Helpers;

namespace PrismicPreview.Controllers
{
    public class HomeController : Controller
    {
        static DefaultCache cache = new DefaultCache();

        public async Task<ActionResult> Index(string culture = "")
        {
            var key = "mycourusel" + "_" + culture;
            var imageList = cache.Get(key)?.ToObject<List<IndexViewModel>>();
            if (imageList == null)
            {
                var api = await PrismicPreviewApi.GetApi();
                var document = await api.GetByUID("carousel", key);
                var docs = document.GetGroup("carousel.carouselimages").GroupDocs;

                var docIds = docs.Select(doc => ((DocumentLink)doc.Fragments.Values.First()).Id).ToList();
                var result = await (api.GetByIDs(docIds).Submit());

                var images = result.Results.Select(r => r.ToIndexViewModel())
                    .OrderBy(i => i.OrdinalNumber).ToList();
                images[0].Active = true;

                cache.Set(key, 240000, Newtonsoft.Json.Linq.JToken.FromObject(images));
                imageList = cache.Get(key).ToObject<List<IndexViewModel>>();
            }

            return View(imageList);
        }
        public async Task<ActionResult> About(string culture = "")
        {
            var key = "e390bee5-52df-49b6-8aa0-ab5d3c97d1c8" + "_" + culture;
            var imageList = cache.Get(key)?.ToObject<List<IndexViewModel>>();
            if (imageList == null)
            {
                var api = await PrismicPreviewApi.GetApi();

                var document = await api.GetByUID("carousel", key);
                var docs = document.GetGroup("carousel.carouselimages").GroupDocs;

                var docIds = docs.Select(doc => ((DocumentLink)doc.Fragments.Values.First()).Id).ToList();
                var result = await (api.GetByIDs(docIds).Submit());

                var images = result.Results.Select(r => r.ToIndexViewModel())
                    .OrderBy(i => i.OrdinalNumber).ToList();
                images[0].Active = true;

                cache.Set(key, 240000, Newtonsoft.Json.Linq.JToken.FromObject(images));
                imageList = cache.Get(key).ToObject<List<IndexViewModel>>();
            }
            return View(imageList);
        }

        public async Task<ActionResult> ContentBlock(string culture = "")
        {
            ViewBag.Message = "Your contact page.";
            var key = "block1" + "_" + culture;
            var contentBlock = cache.Get(key)?.ToObject<ContentBlockViewModel>();
            if (contentBlock == null)
            {
                var api = await PrismicPreviewApi.GetApi();

                var document = await api.GetByUID("contentblock", key);
                var viewModel = document.ToContentBlockViewModel();

                cache.Set(key, 240000, Newtonsoft.Json.Linq.JToken.FromObject(viewModel));
                contentBlock = cache.Get(key).ToObject<ContentBlockViewModel>();
            }
            return View(contentBlock);
        }
    }
}