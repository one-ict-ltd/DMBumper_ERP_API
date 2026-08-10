using ONEERP.Data.Entity.Common;
using ONEERP.Data.Entity.Sales;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.Inventory
{
    public class InvDestructionNoteReceiveDetail : NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int destructionNoteRecvDetailId { get; set; }
        public int? destructionNoteReceiveId { get; set; }
        public InvDestructionNoteReceiveMaster destructionNoteReceive { get; set; }
        public int damageExpireProductReturnDetailId { get; set; }
        public InvDamageExpireProductReturnDetail damageExpireProductReturnDetail { get; set; }
        public int? MiscellaneousItemDetailId { get; set; }
        //public SalMiscellaneousItemDetailsDepot MiscellaneousItemDetail { get; set; } include SalMiscellaneousItemDetailsDepot tbl and expireReturnDetailsId from SalSalesProductExpireReturnDetails tbl
        public int? productSpecificationId { get; set; }
        public InvProductWiseSpecification productSpecification { get; set; }
        public decimal? qty { get; set; }
    }
}
