using System;
using System.Collections.Generic;

namespace ONEERP.Areas.Accounting.Models
{
    public class VisaWorkOrderViewModel
    {
        public int? visaId { get; set; }
        public int? visaWorkOrderId { get; set; }
        public string workOrderNo { get; set; }
        //public int? tradeId { get; set; }
        //public string trade { get; set; }
        public int? countryId { get; set; }
        public string countryName { get; set; }
        public int? cityId { get; set; }
        public string cityName { get; set; }
        public int? companyId { get; set; }
        public string companyName { get; set; }
        public string issueDate { get; set; }
        public string expireDate { get; set; }
        public int? visaGroupQuantity { get; set; }
        public int? visaQuantity { get; set; }
        public int? visaAssigned { get; set; }
        public int? visaUnassigned { get; set; }
        //public int? agentId { get; set; }
        //public string agentName { get; set; }
        //public string employerName { get; set; }         
        public decimal? purchaseAmount { get; set; }
        

        public List<VisaGroupViewModel> lstVisaGroup { get; set; }

    }
}
