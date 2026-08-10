using ONEERP.Data.Entity.Accounting;
using ONEERP.Data.Entity.Common;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ONEERP.Data.Entity.Purchase
{
    public class PurGRNMaster : NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int grnMasterId { get; set; }
        [MaxLength(30)]
        public string grnNo { get; set; }
        public DateTime? grnDate { get; set; }
        public int? purchaseOrderId { get; set; }
        public PurPurchaseOrder purchaseOrder { get; set; }
        public string inhouseChallanNo { get; set; }
        public string factoryReceiveSINo { get; set; }
        public string supplierChallanNo { get; set; }
        public DateTime? supplierChallanDate { get; set; }
        public DateTime? factoryReceivedDate { get; set; }
        public int? grnStatus { get; set; }
        public string remarks { get; set; }
        public string rejectedGRNNo { get; set; }
        public int? storeId { get; set; }



    }
}
