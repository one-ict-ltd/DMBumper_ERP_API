using ONEERP.Data.Entity.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.Accounting
{
    public class AccAutoVoucherMaster : NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int autoVoucherMasterId { get; set; }
        public int? autoVoucherNameId { get; set; }
        public AccAutoVoucherName autoVoucherName { get; set; }
        public int? voucherTypeId { get; set; }
        public AccVoucherTypes voucherType { get; set; }
        public int? companyId { get; set; }
        public CmnCompany company { get; set; }
        public int? sbuId { get; set; }
        public CmnSpecialBranchUnit sbu { get;set;}
        [MaxLength(350)]
        public string description { get; set; }
    }
}
