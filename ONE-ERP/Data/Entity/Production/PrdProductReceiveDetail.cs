using ONEERP.Data.Entity.Inventory;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.Production
{
    public class PrdProductReceiveDetail:NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int productReceiveDetailId { get; set; }

        public int? ProductReceiveMasterId { get; set; }
        public PrdProductReceiveMaster ProductReceiveMaster { get; set; }

        public int? ProductIssueDetailId { get; set; }
        public PrdProductIssueDetail ProductIssueDetail { get; set; }
        public decimal? potency { get; set; }
        public string grnNo { get; set; }
        public decimal? qty { get; set; }
    }
}
