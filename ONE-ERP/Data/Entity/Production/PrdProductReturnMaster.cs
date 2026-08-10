using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.Production
{
    public class PrdProductReturnMaster : NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int productReturnMasterId { get; set; }

        public string returnNo { get; set; }
        public DateTime returnDate { get; set; }

        public string TypeofReturn { get; set; }

        public int? ProductIssueMasterId { get; set; }
        public PrdProductIssueMaster ProductIssueMaster { get; set; }

        public int? status { get; set; }
        public string remarks { get; set; }
        public int? bomForId { get; set; }
    }
}
