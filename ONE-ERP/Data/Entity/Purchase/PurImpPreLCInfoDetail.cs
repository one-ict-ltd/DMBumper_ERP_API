using ONEERP.Data.Entity.Inventory;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.Purchase
{
    public class PurImpPreLCInfoDetail:NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ImpPreLCInfoDetailId { get; set; }
        public int? ImpPreLCInfoMasterId { get; set; }
        public PurImpPreLCInfoMaster ImpPreLCInfoMaster { get; set; }
        public int? productWiseSpecificationId { get; set; }
        public InvProductWiseSpecification productWiseSpecification { get; set; }
        public decimal? unitPrice { get; set; }
        public decimal? totalPrice { get; set; }
        public string BLNo { get; set; }
        public DateTime? BLDate { get; set; }
        public string HSCode { get; set; }
        public decimal? BLRate { get; set; }
        public decimal? BLValue { get; set; }

        public int? csDetailId { get; set; }
        public PurCSDetail csDetail { get; set; }
    }
}
