using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Aurum.Negocio.Extensoes;
using Aurum.Negocio;

namespace Aurum.Web.Controllers
{
    public class MovimentacaoController : Controller
    {
        // GET: Movimentacao
        public ActionResult Principal()
        {
            return RedirectToAction("Resumo");
        }

        // GET: Movimentacao/Resumo
        public ActionResult Resumo()
        {
            return RedirectToRoute("MovimentacaoResumoMesAno", new { mesAno = DateTime.Now.MesAno() });
        }

        // GET: Movimentacao/Resumo/201405
        [Route("Movimentacao/Resumo/{mesAno:int}", Name = "MovimentacaoResumoMesAno")]
        public ActionResult Resumo(int mesAno)
        {
            var mesAnoData = mesAno.MesAno();

            if (!mesAnoData.HasValue)
                throw new HttpException(400, "Mês/ano inválido");

            ViewBag.MesAno = mesAno;
            ViewBag.MesAnoData = mesAnoData.Value;

            var carteiraNegocios = new CarteiraNegocio();
            var categoriaNegocio = new CategoriaNegocio();
            var objetivoNegocio = new ObjetivoNegocio();

            ViewBag.ListaCarteiras = carteiraNegocios.Listar();
            ViewBag.ListaCategorias = categoriaNegocio.Listar();
            ViewBag.ListaObjetivos = objetivoNegocio.Listar();

            return View();
        }

        // GET: Movimentacao/Cadastro
        [Route("Movimentacao/Cadastro")]
        public ActionResult Cadastro()
        {
            return RedirectToRoute("MovimentacaoCadastroMesAno", new { mesAno = DateTime.Now.MesAno() });
        }

        // GET: Movimentacao/Cadastro/201405
        [Route("Movimentacao/Cadastro/{mesAno:int}", Name = "MovimentacaoCadastroMesAno")]
        public ActionResult Cadastro(int mesAno)
        {
            var mesAnoData = mesAno.MesAno();

            if (!mesAnoData.HasValue)
                throw new HttpException(400, "Mês/ano inválido");

            ViewBag.MesAno = mesAno;
            ViewBag.MesAnoData = mesAnoData.Value;

            var carteiraNegocios = new CarteiraNegocio();
            var categoriaNegocio = new CategoriaNegocio();
            var objetivoNegocio = new ObjetivoNegocio();

            ViewBag.ListaCarteiras = carteiraNegocios.Listar();
            ViewBag.ListaCategorias = categoriaNegocio.Listar();
            ViewBag.ListaObjetivos = objetivoNegocio.Listar();

            return View();
        }
    }
}