using ONEERP.Data.Entity.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ONEERP.Data.Entity.Accounting
{
    public class AccPartyObservation: NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int partyId { get; set; }
        public int? partyTypeId { get; set; }
        public AccPartyType partyType { get; set; }
        public int? companyCategoryId { get; set; }
        public CmnCompanyCategory companyCategory { get; set; }
        [MaxLength(250)]
        public string partyName { get; set; }
        [MaxLength(250)]
        public string ownerName { get; set; }
        [MaxLength(30)]
        public string nid { get; set; }
        public string addressLine { get; set; }
        [MaxLength(250)]
        public string contactNumber { get; set; }
        [MaxLength(250)]
        public string contactPerson { get; set; }
        [MaxLength(250)]
        public string email { get; set; }
        public decimal? creditLimit { get; set; }
        public int? isApproved { get; set; }
        public string territoryCode { get; set; }

        public int? accPartyId { get; set; } //if exist for update 
        public int? marketId { get; set; } //if exist for update 
        public string chemberLocation { get; set; }
    }
}
