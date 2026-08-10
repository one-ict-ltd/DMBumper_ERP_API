using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.Accounting
{
    public class AccCurrency:NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int currencyId { get; set; }
        [MaxLength(250)]
        public string currencyName { get; set; }
        [MaxLength(100)]
        public string aliasName { get; set; }
        public decimal? conversionRate { get; set; }
    }
}
