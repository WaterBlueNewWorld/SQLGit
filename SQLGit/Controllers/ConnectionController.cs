using System.Linq;
using System.Web.Mvc;
using SQLGit.Patterns.Repositories.GenericRepository;
using SQLGit.Patterns.Models;
using SQLGit.Patterns.Models.Connection;
using SQLGit.Patterns.UnitOfWork;

namespace SQLGit.Controllers
{
    
    public class ConnectionController : Controller
    {
        private GenericRepository<ConnectionModel> repo;
        private UnitOfWork<DBContext> _unitOfWork = new UnitOfWork<DBContext>();
        static string _sqlconn;
        private static DBContext db;

        public ConnectionController()
        {
            _sqlconn = "";
            //_sqlconn = db.ConnectionModels.Where(x => x.DatabaseIp == ip).ToString();
            db = new DBContext(_sqlconn);
        }
        // GET
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ComboBoxDB()
        {
            var model = db.ConnectionModel.OrderBy(x => x.DatabaseIp).ToList();
            return PartialView("_ComboBoxDB", model);
        }
    }
}