using System.Web.Mvc;
using DevExpress;
using System.Linq;
using Microsoft.Ajax.Utilities;
using SQLGit.Models;

namespace SQLGit.Controllers
{
    public class ScreenController : Controller
    {
      
        
        // GET: Screen
        public ActionResult Index()
        {
            //db = new DBcontext("Data Source=172.30.20.114;Initial Catalog=BODIES_300_20201103_C_V533_R0;Persist Security Info=True;User ID=saas;Password=VclDev2020.");
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