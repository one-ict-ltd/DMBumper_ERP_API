using System;
using System.Collections.Generic;

namespace ONEERP.Areas.Accounting.Models
{
    public class VisaSalesViewModel
    {
        public int? visaSaleId { get; set; }
        public int? candidateId { get; set; }
        public string candidateName { get; set; }
        public string candidateCode { get; set; }
        public string candidateStatus { get; set; }
        public string passportNo { get; set; }
        public int? agentId { get; set; }
        public string agentName { get; set; }
        public int? companyId { get; set; }
        public string companyName { get; set; }
        public int? groupId { get; set; }
        public string groupName { get; set; }
        public int? tradeId { get; set; }
        public string tradeName { get; set; }
        public int? countryId { get; set; }
        public string countryName { get; set; }
        public int? cityId { get; set; }
        public string cityName { get; set; }
        public int? workOrderId { get; set; }
        public string workOrderNo { get; set; }
        public string visaNo { get; set; }        
        public string sponsorId { get; set; }
        public string contact { get; set; }
        public string reference { get; set; }
        public string assignRemarks { get; set; }       
        public string unAssignRemarks { get; set; }
        public decimal? salesAmount { get; set; }
        public decimal? agentCommission { get; set; }
        public decimal? additionalCharge { get; set; }
        public decimal? specialDiscount { get; set; }
        public DateTime? salesDate { get; set; }
        public decimal? netAmount { get; set; }
    }
}
