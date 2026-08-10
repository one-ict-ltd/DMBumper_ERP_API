using ONEERP.Data.Entity.Inventory;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.Purchase.Models
{
    public class PreLcDetailsViewModel
    {      
        public int? ImpPreLCInfoDetailId { get; set; }
        public int? ImpPreLCInfoMasterId { get; set; }
        public int? productWiseSpecificationId { get; set; }
        public decimal? unitPrice { get; set; }
        public decimal? totalPrice { get; set; }
        public string blNo { get; set; }
        public DateTime? blDate { get; set; }
        public string hsCode { get; set; }
        public decimal? blRate { get; set; }
        public decimal? blValue { get; set; }
        public int? csDetailId { get; set; }
    }
}


