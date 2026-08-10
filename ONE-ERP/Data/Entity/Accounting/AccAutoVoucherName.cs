using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.Accounting
{
    public class AccAutoVoucherName :NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int autoVoucherNameId { get; set; }
        [MaxLength(250)]
        public string autoVoucherName { get; set; }
        [MaxLength(250)]
        public string shortName { get; set; }
    }
}
