using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Aurum.Web.Controllers
{
    public class InicioController : Controller
    {
        // GET: [raiz]
        public ActionResult Principal()
        {
            return View();
        }
    }
}