using System.Data.Entity;
using SQLGit.Models.Connection;

namespace SQLGit.Models
{
    public class DBContext : DbContext
    {
        public DBContext(string sqlConnection) : base(sqlConnection)
        {
            
        }

        public DbSet<ConnectionModel> ConnectionModels { get; set; }
    }
}