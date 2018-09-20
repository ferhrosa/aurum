using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.Routing;

namespace Aurum.Web
{
    public class Global : HttpApplication
    {

        #region "Eventos" padrões de uma aplicação web
        
        /// <summary>
        /// Método executado no evento de inicialização do aplicativo web.
        /// </summary>
        protected void Application_Start(object sender, EventArgs e)
        {
            RegistrarRotas(RouteTable.Routes);
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }

        #endregion "Eventos" padrões de uma aplicação web


        private void RegistrarRotas(RouteCollection rotas)
        {
            // Informa que é possível criar rotas para arquivos existentes no projeto (para sobrescrever a "rota" padrão).
            rotas.RouteExistingFiles = true;

            rotas.MapPageRoute("Erro-Raiz", "{pagina}.aspx", "~/Erro.aspx");
            //rotas.MapPageRoute("Erro-Paginas", "Paginas/{pagina}", "~/Erro.aspx");

            rotas.MapPageRoute("Base", "", "~/Base.aspx");

            
        }


    }
}