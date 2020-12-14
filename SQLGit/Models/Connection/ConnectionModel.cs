using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SQLGit.Models.Connection
{
    [Table("DATA_CONNECTION_DB")]
    public class ConnectionModel
    {
        [Key]
        [Column("id_connection")]
        public int IdConnection { get; set; }
        [Column("vanity_name")]
        public string VanityName { get; set; }
        [Column("db_uname")]
        public string User { get; set; }
        [Column("db_upass")]
        public string Pass { get; set; }
        
    }
}