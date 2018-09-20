using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Aurum.Web.Areas.Template.Controllers
{
    public class MovimentacoesController : Controller
    {
        // GET: Template/Movimentacoes/Resumo
        public ActionResult Resumo()
        {
            return View();
        }
    }
}