using System.IO;
using System.Reflection;
using System.Web.Mvc;

namespace Aurum.Web.Controllers
{
    public class BaseController : Controller
    {
        public ActionResult Principal()
        {
            ViewBag.ChaveScripts = new FileInfo(Assembly.GetExecutingAssembly().Location).LastWriteTime.ToString("yyyyMMddHHmmss");

            return View();
        }
    }
}
