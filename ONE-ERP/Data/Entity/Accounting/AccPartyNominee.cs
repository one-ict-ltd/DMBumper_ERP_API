using ONEERP.Data.Entity.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ONEERP.Data.Entity.Accounting
{
    public class AccPartyNominee : NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int partyNomineeId { get; set; }
        public int? partyId { get; set; }
        public AccParty party { get; set; }        
        [MaxLength(300)]
        public string nomineeName { get; set; }
        [MaxLength(300)]
        public string guardianName { get; set; }
        [MaxLength(100)]
        public string nomineeMobile { get; set; }
        [MaxLength(100)]
        public string nomineeNID { get; set; }
        public string nomineeAddress { get; set; }
        [MaxLength(300)]
        public string relationWithNominee { get; set; }
        [MaxLength(300)]
        //public string yearlySales { get; set; }
        public string imagePath { get; set; }

    }
}
