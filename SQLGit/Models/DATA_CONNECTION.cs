using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLGit.Models
{
    [Table("DATA_CONNECTION")]
    public class DATA_CONNECTION
    {
        [Key]
        [Column("ID_Connections")]
        public int ID_Connections { get; set;  }
        
        [Column("VanityName")]
        public string VanityName { get; set;  }
        
        [Column("DatabaseIp")]
        public string DatabaseIp { get; set;  }
        
        [Column("DBUser")]
        public string DBUser { get; set;  }
        
        [Column("DBPassword")]
        public string DBPassword { get; set;  }
    }
}