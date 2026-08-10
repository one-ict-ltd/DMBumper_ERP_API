using System;
using System.Collections.Generic;

namespace ONEERP.Areas.Inventory.Models
{
    public class DamageGoodsViewModel
    {
        public int damageGoodsId { get; set; }
        public string damageGoodsNo { get; set; }
        public DateTime? receiveDate { get; set; }
        public int? companyId { get; set; }
        public int? sbuId { get; set; }
        public int? storeId { get; set; }
        public int? stockTypeId { get; set; } = 2;
        public int? stockCategoryId { get; set; } = 9;
        public bool? isActive { get; set; }
        public List<DamageGoodsDetailsViewModel> lstDetailsViewModel { get; set; }
    }
    public class DamageGoodsDetailsViewModel
    {
        public int damageGoodsDetailsId { get; set; }
        public int? damageGoodsId { get; set; }
        public int? productWiseSpecificationId { get; set; }
        public int? barcodeDetailsId { get; set; }
        public decimal? damageQty { get; set; }
        public int? stockTypeId { get; set; } = 2;
        public string remarks { get; set; }
        public bool? isActive { get; set; } = true;
        public bool? isSelect { get; set; } = true;
    }
}
