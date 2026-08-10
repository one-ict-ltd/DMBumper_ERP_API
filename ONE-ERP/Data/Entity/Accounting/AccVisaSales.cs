using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ONEERP.Data.Entity.Accounting
{
    public class AccVisaSales:NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int visaSaleId { get; set; }
        public int? candidateId { get; set; }
        [MaxLength(250)]
        public string candidateName { get; set; }
        [MaxLength(250)]
        public string candidateCode { get; set; }
        [MaxLength(450)]
        public string candidateStatus { get; set; }
        [MaxLength(250)]
        public string passportNo { get; set; }
        public int? agentId { get; set; }
        [MaxLength(250)]
        public string agentName { get; set; }
        public int? companyId { get; set; }
        [MaxLength(250)]
        public string companyName { get; set; }
        public int? groupId { get; set; }
        [MaxLength(250)]
        public string groupName { get; set; }
        public int? tradeId { get; set; }
        [MaxLength(250)]
        public string tradeName { get; set; }
        public int? countryId { get; set; }
        [MaxLength(250)]
        public string countryName { get; set; }
        public int? cityId { get; set; }
        [MaxLength(250)]
        public string cityName { get; set; }
        public int? workOrderId { get; set; }        
        [MaxLength(250)]
        public string workOrderNo { get; set; }
        [MaxLength(250)]
        public string visaNo { get; set; }
        [MaxLength(250)]
        public string sponsorId { get; set; }
        [MaxLength(250)]
        public string contact { get; set; }
        [MaxLength(250)]
        public string reference { get; set; }
        public string assignRemarks { get; set; }
        public string unAssignRemarks { get; set; }
        public decimal? salesAmount { get; set; }
        public decimal? agentCommission { get; set; }
        public decimal? additionalCharge { get; set; }       
        public decimal? specialDiscount { get; set; }
        public bool? isProcessed { get; set; }
        public DateTime? salesDate { get; set; }
        public decimal? netAmount { get; set; }
    }
}
