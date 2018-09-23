using Microsoft.Owin.Hosting;
using Owin;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace Aurum.Api.SelfHost
{
    class Program
    {

        /// <see cref="https://www.asp.net/web-api/overview/hosting-aspnet-web-api/use-owin-to-self-host-web-api"/>
        private class Startup
        {
            // This code configures Web API. The Startup class is specified as a type
            // parameter in the WebApp.Start method.
            public void Configuration(IAppBuilder appBuilder)
            {
                // Configure Web API for self-host. 
                var config = new HttpConfiguration();
                WebApiConfig.Register(config);

                appBuilder.UseWebApi(config);
            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Inicializando a API...");

            var url = ConfigurationManager.AppSettings["Aurum.Api.Client.BaseUrl"];

            using (var webApp = WebApp.Start<Startup>(url))
            {
                Console.WriteLine($"API sendo executada na seguinte URL: {url}");

                Console.WriteLine("Pressione qualquer tecla para finalizar...");
                Console.ReadLine();
            }
        }

    }
}
