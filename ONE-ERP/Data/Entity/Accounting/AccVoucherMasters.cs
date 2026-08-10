using ONEERP.Data.Entity.Common;
using ONEERP.Data.Entity.Purchase;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.Accounting
{
    public class AccVoucherMasters:NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int voucherMasterId { get; set; }
        public int? voucherTypeId { get; set; }
        public AccVoucherTypes voucherType { get; set; }
        public int? companyId { get; set; }
        public CmnCompany company { get; set; }
        public int? sbuId { get; set; }
        public CmnSpecialBranchUnit sbu { get; set; }
        public int? fundSourceId { get; set; }        
        [MaxLength(50)]
        public string voucherNo { get; set; }
        public DateTime? voucherDate { get; set; }
        [MaxLength(250)]
        public string refNo { get; set; } 
        public string remarks { get; set; }
        public string editRemarks { get; set; }
        public int? isPosted { get; set; }   
        public decimal? amount { get; set; }
        public int? departmentId { get; set; }        
        public int? visaWorkOrderId { get; set; }
        public int? visaSalesId { get; set; }
        public int? salesInvoiceId { get; set; }
        public int? billMasterId { get; set; }
        public PurBillMaster billMaster { get; set; }

    }
}
