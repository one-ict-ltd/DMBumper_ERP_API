using ONEERP.Data.Entity.Inventory;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.FieldForceTracking
{
    public class CmnTerritoryWiseMonthlyPromoItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TerritoryWiseMonthlyPromoItemID { get; set; }

        public string TerritoryCode { get; set; }

        public int? promoItemSecId { get; set; }
        public InvProductWiseSpecification promoItemSec { get; set; }

        public string promoItemFor { get; set; }

        public string employeeCode { get; set; }

        public decimal? Qty { get; set; }
        public decimal? rcvQty { get; set; }

        public int? month { get; set; }
        public int? workingDay { get; set; }
        public int? year { get; set; }
        public int? status { get; set; }
        public string remarks { get; set; }

        public bool? isActive { get; set; }
        [DefaultValue(0)]
        public bool? isDelete { get; set; }
        public DateTime? createdAt { get; set; }
        public DateTime? updatedAt { get; set; }
        [MaxLength(250)]
        public string createdBy { get; set; }
        [MaxLength(250)]
        public string updatedBy { get; set; }
    }
}
