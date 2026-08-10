using ONEERP.Data.Entity.Purchase;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.Inventory
{
    public class InvBatchWiseSerialNo : NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int batchWiseSerialNoId { get; set; }
        
        public int? productWiseSpecificationId { get; set; }
        public InvProductWiseSpecification productWiseSpecification { get; set; }

        public string batchNo { get; set; }
        public string serialNo { get; set; }
        public bool? isChecked { get; set; }

    }
}
