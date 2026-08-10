using ONEERP.Data.Entity.Common;
using ONEERP.Data.Entity.FieldForceTracking;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ONEERP.Data.Entity.Accounting
{
    public class AccParty:NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int partyId { get; set; }
        public int? visaPartyId { get; set; }
        public int? companyId { get; set; }
        public CmnCompany company { get; set; }
        public int? sbuId { get; set; }
        public CmnSpecialBranchUnit sbu { get; set; }
        public int? partyTypeId { get; set; }
        public AccPartyType partyType { get; set; }
        public int? companyCategoryId { get; set; }
        public CmnCompanyCategory companyCategory { get; set; }
        public int? marketId { get; set; }
        public CmnMarket market { get; set; } 
        [MaxLength(100)]
        public string partyCode { get; set; }
        [MaxLength(250)]
        public string partyName { get; set; }
        public string tradeLicense { get; set; }
        public string drugLicense { get; set; }
        [MaxLength(250)]
        public string officeName { get; set; }
        [MaxLength(250)]
        public string ownerName { get; set; }
        [MaxLength(250)]
        public string fatherName { get; set; }
        [MaxLength(250)]
        public string motherName { get; set; }        
        public DateTime? birthDate { get; set; }
        [MaxLength(30)]
        public string nid { get; set; }
        [MaxLength(6)]
        public string gender { get; set; }
        [MaxLength(100)]
        public string aliasName { get; set; }        
        public string addressLine { get; set; }
        [MaxLength(250)]
        public string contactNumber { get; set; }
        [MaxLength(250)]
        public string contactPerson { get; set; }
        [MaxLength(250)]
        public string email { get; set; }
        public DateTime? businessStartDate { get; set; }
        public decimal? creditLimit { get; set; }
        [MaxLength(300)]
        public string creditLimitWord { get; set; }
        public bool? isApproved { get; set; }
        public bool? isHold { get; set; }
        public int? isScheduled { get; set; }
        public bool? isConvertedToLedgers { get; set; }
        public string territoryCode { get; set; }
        public int? creditDays { get; set; }

        public string chemberLocation { get; set; }
        [MaxLength(100)]
        public string ConversionCode { get; set; }
        public int ChemistCodeApprovalId { get; set; }
    }
}
