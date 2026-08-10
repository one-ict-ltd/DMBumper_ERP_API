using ONEERP.Data.Entity.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.Inventory
{
    public class InvDestructionNoteReceiveMaster:NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
        public int destructionNoteReceiveId { get; set; }
        public int damageExpireProductReturnMasterId { get; set; }
        public InvDamageExpireProductReturnMaster damageExpireProductReturnMaster { get; set; }
        public string destructionNoteReceiveNo { get; set; }
        public DateTime? destructionNoteReceiveDate { get; set; }
        public int? miscellaneousTypeId { get; set; } //1 =Damage 6 =Expire
        public string remarks { get; set; }
        public int? isApproved { get; set; }
        public string MarketOrDepo { get; set; }
    }
}
