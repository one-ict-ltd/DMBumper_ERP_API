using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.Accounting.Models
{
    public class CurrencyViewModel
    {
        public int? currencyId { get; set; }
        public string currencyName { get; set; }
        public string aliasName { get; set; }
        public decimal? conversionRate { get; set; }
        public bool? isActive { get; set; }

    }
}
