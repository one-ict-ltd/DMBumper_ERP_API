using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.Production
{
    public class PrdProductReceiveFromReturnMaster : NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductReceiveFromReturnMasterId { get; set; } 
        public DateTime ProductReceiveFromReturnDate { get; set; }

        public string TypeofReceive { get; set; }

        public int? ProductReturnMasterId { get; set; }
        public PrdProductReturnMaster ProductReturnMaster { get; set; }

        public int? status { get; set; }
        public string remarks { get; set; }
        public int? bomForId { get; set; }
    }
}
