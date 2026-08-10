using ONEERP.Data.Entity.Common;
using ONEERP.Data.Entity.Inventory;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.Purchase
{
    public class PurPurchaseRequisition:NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int purchaseReqId { get; set; }
        [MaxLength(50)]
        public string purReqNo { get; set; }
        public int? productReqId { get; set; }
        public PurProductRequisition productRequisition { get; set; }
        public DateTime? purchaseReqDate { get; set; }
        public int? fromWarehouseId { get; set; }
        public CmnStore fstore { get; set; }
        public int? toWarehouseId { get; set; }
        public CmnStore tstore { get; set; }
        public int? approvalStatus { get; set; }
        [MaxLength(250)]
        public string purpose { get; set; }
        [DefaultValue(0)]
        public bool? isUrgency { get; set; }

        public int? isHO { get; set; } // 1 for Head Office 

        public int? approval_TypeId { get; set; }
        public CmnApprovalType approval_Type { get; set; } 
        public int? productTypeId { get; set; }
        public InvProductType productType { get; set; } 
    }
}
