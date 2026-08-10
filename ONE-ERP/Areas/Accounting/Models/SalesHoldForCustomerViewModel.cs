using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.Accounting.Models
{
    public class SalesHoldForCustomerViewModel
    {

        public List<SalesHoldForCustomerlist> lstAccPartyViewModel { get; set; }
    }
    public class SalesHoldForCustomerlist
    {
        public int? partyId { get; set; }
        public bool? isHold { get; set; }
    }
}
