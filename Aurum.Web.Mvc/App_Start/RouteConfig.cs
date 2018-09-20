using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace Aurum.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");


            #region Api

            // Rota para API de dados.
            routes.MapHttpRoute(
                name: "ApiDados",
                routeTemplate: "Dados/{controller}/{codigo}",
                defaults: new { codigo = RouteParameter.Optional }
            );

            // Rota para API de páginas.
            routes.MapHttpRoute(
                name: "ApiPaginas",
                routeTemplate: "Paginas/{controller}/{action}/{codigo}",
                defaults: new { controller = "Base", action = "Principal", codigo = RouteParameter.Optional }
            );

            #endregion Api


            #region Páginas / Templates

            // Rota para carregar Views de páginas base e temmplates.
            routes.MapRoute(
                name: "Padrao",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Base", action = "Principal", id = UrlParameter.Optional }
            );

            #endregion Páginas / Templates


            //routes.MapRoute(
            //    name: "Scripts",
            //    url: "Scripts/{build}/{action}",
            //    defaults: new { controller = "Base", action = "Principal", id = UrlParameter.Optional }
            //);
        }
    }
}