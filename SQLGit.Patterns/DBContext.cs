using System.Data.Entity;
using SQLGit.Patterns.Models.Connection;

namespace SQLGit.Patterns
{
    public class DBContext : DbContext
    {
        public DBContext(string sqlConnection) : base(sqlConnection)
        {
            
        }

        public DbSet<ConnectionModel> ConnectionModels { get; set; }
    }
}