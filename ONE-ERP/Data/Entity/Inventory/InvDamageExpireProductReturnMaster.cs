using ONEERP.Data.Entity.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.Inventory
{
    public class InvDamageExpireProductReturnMaster:NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
        public int damageExpireProductReturnMasterId { get; set; }
        public string damageExpireProductReturnNo { get; set; }
        public DateTime? damageExpireProductReturnDate { get; set; }
        public int? miscellaneousTypeId { get; set; } //1 =Damage 6 =Expire
        public string remarks { get; set; }
        public int? isApproved { get; set; }
        public string MarketOrDepo { get; set; }
    }
}
