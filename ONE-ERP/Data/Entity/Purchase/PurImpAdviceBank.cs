using ONEERP.Data.Entity.Accounting;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.Purchase
{
    public class PurImpAdviceBank:NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ImpAdviceBankId { get; set; }
        public string bankName { get; set; }
        public string bankCode { get; set; }
        public string bankShortName { get; set; }
        public int? sortOrder { get; set; }
    }
}
