using ONEERP.Data.Entity.Sales;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.Sales.Models
{
    public class MiscellaneousItemDetailsViewModel
    {
        public int? miscellaneousItemDetailsId { get; set; }
        public int? miscellaneousItemId { get; set; }
        public int? productSpecificationId { get; set; }
        public decimal? ctnQty { get; set; }
        public decimal? looseQty { get; set; }
        public decimal? price { get; set; }
        public string remarks { get; set; }
        public string batchNo { get; set; }
        public DateTime? mgfDate { get; set; }
        public DateTime? expireDate { get; set; }
    }
    public class MiscellaneousItemFileViewModel
    {
        public int? miscellaneousItemFileId { get; set; }
        public int? miscellaneousItemId { get; set; }
        public string docInfo { get; set; }
        public string imageFile { get; set; }
        public string filePath { get; set; }
    }
}
