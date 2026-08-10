using ONEERP.Data.Entity.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.Inventory
{
    public class InvStockReceive : NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int stockReceiveId { get; set; }
        [MaxLength(50)]
        public string stockReceiveNo { get; set; }
        public int? prodTrnfrId { get; set; }
        public InvProductTransfer productTransfer { get; set; }
        public DateTime? stockReceiveDate { get; set; }
        public int? SbuId { get; set; }
        public CmnSpecialBranchUnit sbu { get;set;}
        public string purpose { get; set; }
        public string purchaseOrderNo { get; set; }
        public string challanNo { get; set; }
        public string lcNo { get; set; }
        public string supplierName { get; set; }
    }
}
