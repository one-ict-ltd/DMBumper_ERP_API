using ONEERP.Data.Entity.Inventory;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.Production
{
    public class PrdRequisitionDetails:NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int requisitionDetailId { get; set; }

        public int? requisitionMasterId { get; set; }
        public PrdRequisitionMaster requisitionMaster { get; set; }

        public int productWiseSpecificationId { get; set; }
        public InvProductWiseSpecification productWiseSpecification { get; set; }

        public decimal? qty { get; set; } 

    }
}
