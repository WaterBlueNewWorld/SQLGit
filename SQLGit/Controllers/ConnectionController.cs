using System.Linq;
using System.Web.Mvc;
using SQLGit.Patterns.Repositories.GenericRepository;
using SQLGit.Patterns.Models;
using SQLGit.Patterns.Models.Connection;

namespace SQLGit.Controllers
{
    
    public class ConnectionController : Controller
    {
        private IGenericRepository<ConnectionModel> repo = null;
        static string _sqlconn;
        private static DBContext db;

        public ConnectionController(string ip)
        {
            _sqlconn = db.ConnectionModels.Where(x => x.DatabaseIp == ip).ToString();
            db = new DBContext(_sqlconn);
        }

        public ConnectionController(IGenericRepository<ConnectionController> repository)
        {
            
        }
        // GET
        public ActionResult Index()
        {
            GenericRepository<ConnectionModel> conn = new GenericRepository<ConnectionModel>(db);
            var busc = db.ConnectionModels.FirstOrDefault(x => x.IdConnection == 1);
            var model = db.ConnectionModels.OrderBy(x => x.IdConnection);
            return View(model);
        }

        /*public ActionResult ComboBoxDB()
        {
            
        }*/
    }
}