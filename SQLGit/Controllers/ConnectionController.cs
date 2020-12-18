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
            _sqlconn = "Data Source=172.30.20.114;Initial Catalog=BODIES_300_20201103_C_V533_R0;Persist Security Info=True;User ID=saas;Password=VclDev2020.";
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