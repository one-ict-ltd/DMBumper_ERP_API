using System;
using System.Collections.Generic;

namespace ONEERP.Areas.Production.Models
{
    public class BomFinishGoodStockInMasterViewModel
    {
        public int? bomStockInId { get; set; }
        //public string stockInNo { get; set; }
        public int? companyId { get; set; }
        public int? sbuId { get; set; }
        public int? storeId { get; set; }
        public DateTime? stockInDate { get; set; }
        public int? isActive { get; set; }
        public List<BomFinishGoodStockInDetailsViewModel> lstDetailsViewModel { get; set; }
    }
}
