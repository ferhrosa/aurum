using Aurum.Modelo;
using Aurum.Modelo.Enums;
using Aurum.Negocio;
using Aurum.Negocio.Extensoes;
using System.Linq;
using System.Web.Mvc;

namespace Aurum.Web.Controllers
{
    public class CadastrosController : Controller
    {
        //
        // GET: /Cadastros/Carteiras
        public ActionResult Carteiras()
        {
            ViewBag.ListaTipos = from t in System.Enum.GetValues(typeof(TipoCarteira)).Cast<TipoCarteira>()
                                 select new SelectListItem()
                                 {
                                     Value = ((int)t).ToString(),
                                     Text = t.GetDescription()
                                 };

            var carteiraNegocios = new CarteiraNegocio(Contexto.InstanciaWeb);

            ViewBag.ListaCarteiras = carteiraNegocios.ListarSomenteAtivas();

            return View();
        }

        //
        // GET: /Cadastros/Categorias
        public ActionResult Categorias()
        {
            return View();
        }

        //
        // GET: /Cadastros/Objetivos
        public ActionResult Objetivos()
        {
            var carteiraNegocios = new CarteiraNegocio();

            ViewBag.ListaCarteiras = carteiraNegocios.ListarSomenteAtivas();

            return View();
        }
    }
}