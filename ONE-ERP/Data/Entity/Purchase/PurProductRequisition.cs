using ONEERP.Data.Entity.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.Purchase
{
    public class PurProductRequisition : NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int prodReqId { get; set; }
        [MaxLength(50)]
        public string prodReqNo { get; set; }
        public DateTime? prodReqDate { get; set; }
        public int? fromWarehouseId { get; set; }
        public CmnStore fstore {get;set;}
        public int? toWarehouseId { get;set; }
        public CmnStore tstore { get; set; }
        [MaxLength(250)]
        public string purpose { get;set; }
        public bool? isUrgency { get; set; }
        public int? approvalStatus { get; set; }
    }
}
