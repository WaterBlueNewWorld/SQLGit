using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;
using SQLGit.GenericRepository;
using SQLGit.Patterns.Repositories.GenericRepository;
using SQLGit.Patterns;
using SQLGit.Patterns.Models.Connection;

namespace SQLGit.Controllers
{
    
    public class ConnectionController : Controller
    {
        private IGenericRepository<ConnectionModel> db = null;
        static string sqlconn;
        private static DBContext _db;

        public ConnectionController()
        {
            sqlconn = "Data Source=172.30.20.165;Initial Catalog=SaasIntegral;Persist Security Info=True;User ID=saas;Password=VclDev2020.;";
            _db = new DBContext(sqlconn);
        }

        public ConnectionController(IGenericRepository<ConnectionController> repository)
        {
            
        }
        // GET
        public ActionResult Index()
        {
            GenericRepository<ConnectionModel> conn = new GenericRepository<ConnectionModel>(_db);
            var busc = _db.ConnectionModels.FirstOrDefault(x => x.IdConnection == 1);
            var model = _db.ConnectionModels.OrderBy(x => x.IdConnection);
            return View(model);
        }

        /*public ActionResult ComboBoxDB()
        {
            
        }*/
    }
}