using ONEERP.Data.Entity.Inventory;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.Production
{
    public class PrdProductReturnDetail : NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int productReturnDetailId { get; set; }

        public int? ProductReturnMasterId { get; set; }
        public PrdProductReturnMaster ProductReturnMaster { get; set; }

        public int? ProductIssueDetailId { get; set; }
        public PrdProductIssueDetail ProductIssueDetail { get; set; }
        public decimal? potency { get; set; }
        public string grnNo { get; set; }
        public decimal? returnQty { get; set; }
        public int? grnDetailsId { get; set; }
    }
}
