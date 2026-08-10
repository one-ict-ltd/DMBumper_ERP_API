using ONEERP.Data.Entity.Common;
using ONEERP.Data.Entity.FieldForceTracking;
using ONEERP.Data.Entity.Inventory;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ONEERP.Data.Entity.PromoInventory
{
    public class PromoTerritoryStockMaster : NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int territoryStockMasterId { get; set; } 
        public DateTime? promoStockDate { get; set; }
        public string promoStockNo { get; set; }
        public int? promoStockTypeId { get; set; }
        public InvStockType promoStockType { get; set; }

        public int? distributionMasterId { get; set; }
        public DepotPromoDistributionMaster distributionMaster { get; set; }

        public int? transactionMasterId { get; set; }
        public string remarks { get; set; }
        public string territoryCode { get; set; } 
        public int? companyId { get; set; }
        public CmnCompany company { get; set; }
        public int? doctorScheduleId { get; set; }
        public CmnDoctorSchedules doctorSchedules { get; set; }

        public int? chemistScheduleId { get; set; }
        public CmnChemistSchedules chemistSchedule { get; set; }
        public string stockFor { get; set; }

    }
}
