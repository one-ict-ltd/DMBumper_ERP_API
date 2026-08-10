using System;
using System.Collections.Generic;

namespace ONEERP.Areas.Inventory.Models
{
    public class TerritoryPromoStockMasterModel
    {
        public int? doctorScheduleId { get; set; }
        public int? chemistScheduleId { get; set; }
        public int? territoryStockMasterId { get; set; }
        public int? distributionMasterId { get; set; } 
        public DateTime? promoStockDate { get; set; }
        public string promoStockNo { get; set; }
        public int? promoStockTypeId { get; set; }    
        public string remarks { get; set; }
        public string territoryCode { get; set; }  
        public string stockFor { get; set; }  
        public List<TerritoryPromoStockDetailsModel> promoItemsListModel { get; set; }
    }

    public class TerritoryPromoStockDetailsModel
    {
        public int? territoryStockDetailId { get; set; } 
        public int? productWiseSpecificationId { get; set; } 
        public int? packetingDetailId { get; set; }
        public decimal? receivedQty { get; set; }
        public decimal? stockOutQty { get; set; }
    }
}
