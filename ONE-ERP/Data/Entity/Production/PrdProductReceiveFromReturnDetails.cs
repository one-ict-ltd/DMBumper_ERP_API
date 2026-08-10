using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.Production
{
    public class PrdProductReceiveFromReturnDetails : NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductReceiveFromReturnDetailId { get; set; }
        public int productReturnDetailId { get; set; }
        public int? ProductReceiveFromReturnMasterId { get; set; }
        public int? ProductIssueDetailId { get; set; }
        public decimal? potency { get; set; }
        public string grnNo { get; set; }
        public decimal? receivedQty { get; set; }
        public int? grnDetailsId { get; set; }
    }
}
