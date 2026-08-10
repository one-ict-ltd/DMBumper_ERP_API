using ONEERP.Data.Entity.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.Purchase
{
    public class PurPurchaseOrderReceive:NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int poReceiveId { get; set; }
        public string purOrderRecvNo { get; set; }
        public int? purchaseOrderId { get; set; }
        public PurPurchaseOrder purchaseOrder { get; set; }
        public DateTime? purchaseOrderRecvDate { get; set; }
        public int? toWarehouseId { get; set; }
        public CmnStore store { get; set; }
        public int? approvalStatus { get; set; }
        public string receiveStatus { get; set; }
    }
}
