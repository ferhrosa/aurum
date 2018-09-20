using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.IO;

namespace Aurum.Web
{
    /// <summary>
    /// Summary description for Paginas
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    [System.Web.Script.Services.ScriptService]
    public class Pagina : System.Web.Services.WebService
    {
        [WebMethod]
        public string Carregar(string url)
        {
            var partes = url.Split('#');

            if ( partes.Length <= 1 )
            {
                return CarregarPagina("Principal");
            }

            var parametros = partes[1];



            return url;
        }

        private string CarregarPagina(string nomePagina)
        {
            //File.ReadAllText(
        }
    }
}
