using ONEERP.Data.Entity.Inventory;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.Production
{
    public class PrdReagentIssueDetail : NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int reagentIssueDetailId { get; set; }
        public int? reagentIssueMasterId { get; set; }
        public PrdReagentIssueMaster reagentIssueMaster { get; set; }
        public int? reagentReqDetailId { get; set; }
        public PrdReagentReqDetails reagentReqDetail { get; set; }
        public decimal? qty { get; set; }
        public string batchNumber { get; set; }
    }
}
