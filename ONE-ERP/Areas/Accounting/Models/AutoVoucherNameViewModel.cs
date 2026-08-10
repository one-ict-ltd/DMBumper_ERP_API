using System.Collections.Generic;

namespace ONEERP.Areas.Accounting.Models
{
    public class AutoVoucherNameViewModel
    {
        public int? autoVoucherNameId { get; set; }                     
        public string autoVoucherName { get; set; }
        public string shortName { get; set; }           
        public bool? isActive { get; set; }        
    }
}
