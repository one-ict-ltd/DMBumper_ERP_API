using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.Production
{
    public class PrdReagentReceiveMaster : NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int reagentReceiveMasterId { get; set; }
        public string receiveNo { get; set; }
        public DateTime receiveDate { get; set; }
        public string TypeofReceive { get; set; }
        public int? reagentIssueMasterId { get; set; }
        public PrdReagentIssueMaster ReagentIssueMaster { get; set; }
        public decimal? receiveQty { get; set; }
        public int? status { get; set; }
        public string remarks { get; set; }
        public int? bomForId { get; set; }
    }
}
