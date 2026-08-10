using ONEERP.Data.Entity.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.Inventory
{
    public class InvProductTransfer:NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int prodTrnfrId { get; set; }
        [MaxLength(30)]
        public string prodTrnNo { get; set; }
        public int? productReqId { get; set; }
        public DateTime? prodTrnDate { get; set; }
        public int? fromWarehouseId { get; set; }
        public int? toWarehouseId { get; set; }
        public int? fromSbuId { get; set; }
        public CmnSpecialBranchUnit fromSbu { get; set; }
        public int? toSbuId { get; set; }
       public CmnSpecialBranchUnit toSbu { get; set; }
        public string purpose { get; set; }
        [DefaultValue(0)]
        public bool? isUrgency { get; set; }
        public int? approvalStatus { get; set; }
        public string driverName { get; set; }
        public string vehicleNo { get; set; }
        public string transferType { get; set; }
    }
}
