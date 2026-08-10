using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;


namespace ONEERP.Data.Entity.Purchase
{
    public class PurImpOtherCharge:NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ImpOtherChargeId { get; set; }
        public int? ImpLCInfoMasterId { get; set; }
        public PurImpLCInfoMaster ImpLCInfoMaster { get; set; }
        public decimal? CustomsDutyOthersCharge{ get; set; }
        public DateTime? CustomsDutyOthersChargeDate { get; set; }
        public decimal? ClearingCNFCharge { get; set; }
        public DateTime? ClearingCNFChargeDate { get; set; }
        public decimal? LoadingUnloadingCharge { get; set; }
        public DateTime? LoadingUnloadingChargeDate { get; set; }
        public decimal? CarringCharge { get; set; }
        public DateTime? CarringChargeDate { get; set; }
        public decimal? OthersCharge { get; set; }
        public decimal? OthersCharge2 { get; set; }
        public string remarks { get; set; }
    }
}
