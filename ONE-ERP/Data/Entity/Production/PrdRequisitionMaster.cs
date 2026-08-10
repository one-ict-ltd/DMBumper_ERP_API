using ONEERP.Data.Entity.Inventory;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.Production
{
    public class PrdRequisitionMaster:NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int requisitionMasterId { get; set; }

        public string ReqNo { get; set; }
        public DateTime ReqDate { get; set; }

        public string TypeofRequisition { get; set; }

        public int productWiseSpecificationId { get; set; }
        public InvProductWiseSpecification productWiseSpecification { get; set; }

        public decimal? reqQty { get; set; }


        public int? productionPlanId { get; set; }
        public int? status { get; set; }
        public string remarks { get; set; }

        public int? bomForId { get; set; }

    }
}
