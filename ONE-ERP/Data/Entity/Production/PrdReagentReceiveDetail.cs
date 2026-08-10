using ONEERP.Data.Entity.Inventory;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.Production
{
    public class PrdReagentReceiveDetail : NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int reagentReceiveDetailId { get; set; }
        public int? reagentReceiveMasterId { get; set; }
        public PrdReagentReceiveMaster PrdReagentReceiveMaster { get; set; }
        public int? reagentIssueDetailId { get; set; }
        public PrdReagentIssueDetail ProductIssueDetail { get; set; }
        public decimal? potency { get; set; }
        public string grnNo { get; set; }
        public decimal? qty { get; set; }
    }
}
