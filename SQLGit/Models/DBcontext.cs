using System.Data.Entity;
using SQLGit.Models;

namespace SQLGit.Models
{
    public class DBcontext : DbContext
    {
        public DBcontext(string sqlconn) : base(sqlconn)
        {
            
        }
        public DbSet<DATA_CONNECTION> DataConmections { get; set; }
    }
}