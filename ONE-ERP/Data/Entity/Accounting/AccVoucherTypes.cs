using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.Accounting
{
    public class AccVoucherTypes:NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? voucherTypeId { get; set; }
        [MaxLength(250)]
        public string voucherTypeName { get; set; }
        [MaxLength(100)]
        public string aliasName { get; set; }
    }
}
