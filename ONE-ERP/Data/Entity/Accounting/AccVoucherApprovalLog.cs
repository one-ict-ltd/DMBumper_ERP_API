using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.Accounting
{
    public class AccVoucherApprovalLog:NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int voucherAppLogId { get; set; }
        public int? voucherMasterId { get; set; }
        public AccVoucherMasters voucherMaster { get; set; }
        public string remarks { get; set; }
        public int? voucherStatusId { get; set; }
        public AccVoucherStatus voucherStatus { get; set; }
    }
}
