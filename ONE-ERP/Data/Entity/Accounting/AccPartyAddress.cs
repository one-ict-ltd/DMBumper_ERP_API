using ONEERP.Data.Entity.Common;
using ONEERP.Data.Entity.HrmMaster;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ONEERP.Data.Entity.Accounting
{
    public class AccPartyAddress : NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int partyAddressId { get; set; }
        public int? partyId { get; set; }
        public AccParty party { get; set; }
        public int? addressTypeId { get; set; }
        public HrmAddressType hrmAddressType { get; set; }
        [MaxLength(10)]
        public string addressType { get; set; }        
        public string houseStreet { get; set; }
        [MaxLength(250)]
        public string postOffice { get; set; }
        [MaxLength(250)]
        public string policeStation { get; set; }
        [MaxLength(250)]
        public string thana { get; set; } 
        [MaxLength(250)]
        public string district { get; set; }
        [MaxLength(250)]
        public string division { get; set; }
       

    }
}
