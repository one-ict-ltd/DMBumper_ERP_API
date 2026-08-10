using ONEERP.Data.Entity.Accounting;
using ONEERP.Data.Entity.Common;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ONEERP.Data.Entity.Purchase
{
    public class PurImpGRNMaster:NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ImpgrnMasterId { get; set; }
        [MaxLength(30)]
        public string grnNo { get; set; }
        public DateTime? grnDate { get; set; }
        public int? PurImpPreLCInfoMasterId { get; set; }
        public PurImpPreLCInfoMaster PurImpPreLCInfoMaster { get; set; }
        public string RMRNo { get; set; }
        public string MRRNo { get; set; }
        public string TruckNo { get; set; }
        public string DriverName { get; set; }
        public string CFAgentName { get; set; }
        public string mobileNo { get; set; }
        public DateTime? factoryReceivedDate { get; set; }
        public int? grnStatus { get; set; }
        public string rejectedGRNNo { get; set; }
        public string remarks { get; set; }
        public int? storeId { get; set; }

    }
}
