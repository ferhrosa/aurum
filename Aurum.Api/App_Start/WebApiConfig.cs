using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Aurum.Api
{
    public static class WebApiConfig
    {
        public const string RotaPadrao = "DefaultApi";

        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            //TODO: Verificar se posso deixar com * no origins mesmo.
            config.EnableCors(new EnableCorsAttribute(origins: "*", headers: "*", methods: "*"));
            //config.EnableCors(new EnableCorsAttribute(origins: "localhost:1337", headers: "*", methods: "*"));

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: RotaPadrao,
                routeTemplate: "{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
