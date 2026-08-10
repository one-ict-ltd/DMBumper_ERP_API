using ONEERP.Data.Entity.Inventory;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.FieldForceTracking
{
    public class CmnRxUploadProduct
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int rxUploadProductID { get; set; }

        public int? CmnRxUploadMasterId { get; set; }
        public CmnRxUploadMaster CmnRxUploadMaster { get; set; }

        public DateTime? date { get; set; }

        public int? InvProductWiseSpecificationId { get; set; }
        public InvProductWiseSpecification InvProductWiseSpecification { get; set; }
    }
}
