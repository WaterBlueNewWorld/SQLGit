using System.Web.Mvc;
using DevExpress;
using System.Linq;
using Microsoft.Ajax.Utilities;

namespace SQLGit.Controllers
{
    public class ScreenController : Controller
    {
       
        // GET: Screen
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult TablaDistinta()
        {
            return PartialView("_TablaDistinta");
        }
        public ActionResult EsquemaOrigen()
        {
            return PartialView("_EsquemaOrigen");
        }
        public ActionResult EsquemaDestino()
        {
            return PartialView("_EsquemaDestino");
        }
        public ActionResult ServidorOrigenBox()
        {
            return PartialView("ServidorOrigenBox");
        }
        public ActionResult ServidorDestinoBox()
        {
            return PartialView("ServidorDestinoBox");
        }
        public ActionResult BaseOrigenBox()
        {
            return PartialView("BaseOrigenBox");
        }
        public ActionResult BaseDestinoBox()
        {
            return PartialView("BaseDestinoBox");
        }
    }
}