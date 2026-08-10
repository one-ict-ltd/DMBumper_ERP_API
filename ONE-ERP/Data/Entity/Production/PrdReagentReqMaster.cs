using ONEERP.Data.Entity.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.Production
{
    public class PrdReagentReqMaster : NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int reagentReqId { get; set; }
        [MaxLength(50)]
        public string reagentReqNo { get; set; }
        public DateTime? reagentReqDate { get; set; }
        public int? fromWarehouseId { get; set; }
        public CmnStore fstore { get; set; }
        public int? toWarehouseId { get; set; }
        public CmnStore tstore { get; set; }
        [MaxLength(500)]
        public string purpose { get; set; }
        public bool? isUrgency { get; set; }
        public int? approvalStatus { get; set; }
    }
}
