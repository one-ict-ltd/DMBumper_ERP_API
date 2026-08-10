using System;
using System.Collections.Generic;

namespace ONEERP.Areas.Accounting.Models
{
    public class PartyObservationViewModel
    {
        public int partyId { get; set; }
        public int partyTypeId { get; set; }
        public int companyCategoryId { get; set; }
        public string partyName { get; set; }
        public string ownerName { get; set; }
        public string nid { get; set; }
        public string addressLine { get; set; }
        public string contactNumber { get; set; }
        public string contactPerson { get; set; }
        public string email { get; set; }
        public string MarketCode { get; set; }
        public string MarketName { get; set; }
        public decimal creditLimit { get; set; }
        public int isApproved { get; set; }
        public string territoryId { get; set; }
        public int? accPartyId { get; set; }
        public string chemberLocation { get; set; }


    }
}
