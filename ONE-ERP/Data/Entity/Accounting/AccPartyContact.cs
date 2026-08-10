using ONEERP.Data.Entity.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ONEERP.Data.Entity.Accounting
{
    public class AccPartyContact : NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int partyContactId { get; set; }
        public int? partyId { get; set; }
        public AccParty party { get; set; }
        [MaxLength(30)]
        public string mobileOne { get; set; }       
        [MaxLength(30)]
        public string mobileTwo { get; set; }
        [MaxLength(250)]
        public string emailAddress { get; set; }
        [MaxLength(250)]
        public string managerName { get; set; }
        [MaxLength(250)]
        public string managerContact { get; set; }
       

    }
}
