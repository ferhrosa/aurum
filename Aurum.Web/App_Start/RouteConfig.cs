using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Aurum.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            var urls = new string[]
            {
                "cadastros/conta/{id}",
                "{controller}/{action}/{id}"
            };

            foreach(var url in urls)
            {
                routes.MapRoute(
                    name: String.Format("Default-{0}", url),
                    url: url,
                    defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
                );
            }

        }
    }
}
