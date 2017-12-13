using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace PrismicPreniew
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
    public static class Locales
    {
        public static List<string> AvailableLocales { get; set; } = new List<string>
        {
            "en",
            "ru"
        };
        //static Locales()
        //{
        //    try
        //    {
        //        // Retrieving all locales available for us at our space on Contentful to fill in the dropDown in Shared Layout
        //        AvailableLocales = Task.Run(() => Api._management.GetLocalesCollection()).Result;
        //    }
        //    catch (Exception e)
        //    {
        //    }
        //}
    }
}
