using ONEERP.Data.Entity.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ONEERP.Data.Entity.Accounting
{
    public class AccPartyBank : NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int partyBankId { get; set; }
        public int? partyId { get; set; }
        public AccParty party { get; set; }
        public int? bankId { get; set; }
        public CmnBank bank { get; set; }
        [MaxLength(200)]
        public string bankAccName { get; set; }       
        [MaxLength(100)]
        public string bankAccNo { get; set; }
        [MaxLength(200)]
        public string bankBranchName { get; set; }

    }
}
