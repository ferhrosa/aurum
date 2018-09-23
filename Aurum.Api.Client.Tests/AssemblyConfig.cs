using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Owin;
using System.Web.Http;
using Microsoft.Owin.Hosting;

namespace Aurum.Api.Client.Tests
{
    [TestClass]
    public class AssemblyConfig
    {

        private static IDisposable webApp;

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


        [AssemblyInitialize]
        public static void InicializarApi(TestContext context)
        {
            Console.WriteLine("Inicializando a API...");

            webApp = WebApp.Start<Startup>(url: ApiClient.baseUrl);
        }


        [AssemblyCleanup]
        public static void FinalizarApi()
        {
            webApp.Dispose();

            Console.WriteLine("API finalizada.");
        }

    }
}
