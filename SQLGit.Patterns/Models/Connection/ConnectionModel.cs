using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SQLGit.Patterns.Models.Connection
{
    [Table("DATA_CONNECTION")]
    public class ConnectionModel
    {
        [Key]
        [Column("ID_Connections")]
        public int ID_Connections { get; set; }
        
        [Column("VanityName")]
        public string VanityName { get; set; }
        
        [Column("DatabaseIp")]
        public string DatabaseIp { get; set; }
        
        [Column("DBUser")]
        public string DBUser { get; set; }
        
        [Column("DBPassword")]
        public string DBPassword { get; set; }
        
    }
}