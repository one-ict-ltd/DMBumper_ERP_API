using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.Purchase.Models
{
    // No need all of them

    public class CommonViewModel
    {
       
    }

    /*Create some common APIs for Purchase Module like 
     * 
     * Warehouse, 
     * Product name,
     * 
     * Product Req. No., 
     * Purchase Req. No., 
     * Purchase Order No., 
     * Purchase Order Receive No., 
     * Requisition By etc.
     */

    public class PurchaseOrderReceiveNumberViewModel
    {
        public int? PurchaseOrderReceiveId { get; set; }
        public string PurchaseOrderReceiveNumber { get; set; }
    }
    public class PurchaseOrderNumberViewModel
    {
        public int? PurchaseOrderId{ get; set; }
        public string PurchaseOrderNumber { get; set; }
    }
    public class PurchaseReqNumberViewModel
    {
        public int? PurchaseReqId{ get; set; }
        public string PurchaseReqNumber { get; set; }
    }
    public class ProductReqNumberViewModel
    {
        public int? ProductReqId{ get; set; }
        public string ProductReqNumber { get; set; }
    }
}
