using ONEERP.Data.Entity.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ONEERP.Data.Entity.Accounting
{
    public class AccPartyShareholder : NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int partyShareholderId { get; set; }
        public int? partyId { get; set; }
        public AccParty party { get; set; }        
        [MaxLength(300)]
        public string shareholderName { get; set; }       
        [MaxLength(100)]
        public string shareholderMobile { get; set; }       
        public string shareholderAddress { get; set; }

    }
}
