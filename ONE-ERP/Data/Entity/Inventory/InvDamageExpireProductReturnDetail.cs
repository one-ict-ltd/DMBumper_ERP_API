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
    public class InvDamageExpireProductReturnDetail:NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int damageExpireProductReturnDetailId { get; set; }
        public int? damageExpireProductReturnMasterId { get; set; }
        public InvDamageExpireProductReturnMaster damageExpireProductReturnMaster { get; set; }
        public int? MiscellaneousItemDetailId { get; set; } // include SalMiscellaneousItemDetailsDepot tbl and expireReturnDetailsId from SalSalesProductExpireReturnDetails tbl
        //public SalMiscellaneousItemDetailsDepot MiscellaneousItemDetail { get; set; }
        public int? productSpecificationId { get; set; }
        public InvProductWiseSpecification productSpecification { get; set; }
        public decimal? qty { get; set; }
    }
}
