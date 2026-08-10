using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.Sales.Models
{
    public class ValidateCustomerDuesStatusViewModel
    {
        public string hasDeed { get; set; }
        public decimal? creditLimit { get; set; }
        public decimal? duesAmount { get; set; }
        public string creditLimitCrossed { get; set; }
        public string overDuesStatus { get; set; }
        public int? overDueDays { get; set; }
        public decimal? PendingOrderAmount { get; set; }
        public bool? hasOrderValidationCheck { get; set; }
    }
}
