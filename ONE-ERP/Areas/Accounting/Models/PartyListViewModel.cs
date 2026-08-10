using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.Accounting.Models
{
    public class PartyListViewModel
    {
        public int? partyId { get; set; }
        public string partyCode { get; set; }
        public string partyName { get; set; }
        public string aliasName { get; set; }
        public int? companyId { get; set; }
        public int? sbuId { get; set; }
        public bool? isActive { get; set; }
        public string status { get; set; }
    }
}
