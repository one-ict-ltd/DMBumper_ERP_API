using ONEERP.Data.Entity.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ONEERP.Data.Entity.Accounting
{
    public class AccPartyDistributor : NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int partyDistributorId { get; set; }
        public int? partyId { get; set; }
        public AccParty party { get; set; }        
        [MaxLength(300)]
        public string distributorName { get; set; }       
        [MaxLength(100)]
        public string distributorMobile { get; set; }       
        public string distributorAddress { get; set; }
        [MaxLength(300)]
        public string businessDuration { get; set; }
        [MaxLength(300)]
        public string yearlySales { get; set; }

    }
}
