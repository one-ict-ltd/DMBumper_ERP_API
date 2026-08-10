using ONEERP.Data.Entity.Accounting;
using ONEERP.Data.Entity.Common;
using ONEERP.Data.Entity.HRM;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ONEERP.Data.Entity.Purchase
{
    public class PurPurchaseOrder:NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int purchaseOrderId { get; set; }
        [MaxLength(30)]
        public string purOrderNo { get; set; }
        public int? purchaseReqId { get; set; }
        public PurPurchaseRequisition purchaseReq { get; set; }
        public DateTime? purchaseOrderDate { get; set; }
        public int? fromWarehouseId { get; set; }
        public CmnStore fstore { get; set; }
        public int? toWarehouseId { get; set; }
        public CmnStore tstore { get; set; }
        public int? purchaseOrderFromId { get; set; }

        public int? supplierId { get; set; }
        public AccParty supplier { get; set; }
        public int? approvalStatus { get; set; }
        public string purpose { get; set; }
        [DefaultValue(0)]
        public bool? IsUrgency { get; set; }

        public decimal? grossAmount { get; set; }
        public decimal? totalVat { get; set; }
        public decimal? totalAit { get; set; }
        public decimal? totalDiscount { get; set; }
        public decimal? freightCharge { get; set; }        
        public decimal? netAmount { get; set; }
        public int? transactionTypeId { get; set; }
        public CmnTransactionType transactionType { get; set; }

        public int? purchaseFromId { get; set; }
        public int? csMasterId { get; set; }
        public PurCSMaster csMaster { get; set; }
        public int? requisitionFinalizeMasterId { get; set; }
        public PurRequisitionFinalizeMaster requisitionFinalizeMaster { get; set; }
        
        [MaxLength(100)]
        public string lcNo { get; set; }
        [MaxLength(100)]
        public string refNo { get; set; }

        public int? purchaseOrderSignatoryId { get; set; }
        public HrmEmployee purchaseOrderSignatory { get; set; }

        //public int? purchaseOrderFromId { get; set; }
        //public HrmEmployee purchaseOrderFrom { get; set; }

    }
}
