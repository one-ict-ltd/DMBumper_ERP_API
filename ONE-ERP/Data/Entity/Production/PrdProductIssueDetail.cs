using ONEERP.Data.Entity.Inventory;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.Production
{
    public class PrdProductIssueDetail:NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int productIssueDetailId { get; set; }

        public int? productIssueMasterId { get; set; }
        public PrdProductIssueMaster productIssueMaster { get; set; }

        public int? prdRequisitionDetailId { get; set; }
        public PrdRequisitionDetails prdRequisitionDetail { get; set; }

        public decimal? qty { get; set; }
        public string batchNumber { get; set; }
    }
}
