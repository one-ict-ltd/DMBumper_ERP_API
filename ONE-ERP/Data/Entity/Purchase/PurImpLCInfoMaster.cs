using ONEERP.Data.Entity.Common;
using ONEERP.Data.Entity.Inventory;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;


namespace ONEERP.Data.Entity.Purchase
{
    public class PurImpLCInfoMaster:NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ImpLCInfoMasterId { get; set; }
        public int? ImpPreLCInfoMasterId { get; set; }
        public PurImpPreLCInfoMaster ImpPreLCInfoMaster { get; set; }

        public string LCNo { get; set; }
        public string LCANo { get; set; }
        public DateTime? LCOpenDate { get; set; }
        public int? BankId { get; set; }
        public CmnBank Bank { get; set; }
        public int AdviseBankId { get; set; }
        public PurImpAdviceBank AdviseBank { get; set; }

        public DateTime? ValidityShipDate { get; set; }
        public string LCNegotiation { get; set; }
        public DateTime? ExpectedShiptDate { get; set; }
        public DateTime? LCExpireDate { get; set; }
        public int? loadingPortInfoId { get; set; }
        public PurImpPortInfo loadingPortInfo { get; set; }
        public int? destinationPortInfoId { get; set; }
        public PurImpPortInfo destinationPortInfo { get; set; }
        public int? CountryId { get; set; }
        public CmnOriginCountry Country { get; set; }
        public decimal? FreigtAmount { get; set; }
        public decimal? TotalLCAmount { get; set; }
        public string RemindShiptDay { get; set; }
        public DateTime? shiptRemindDate { get; set; }
    }
}
