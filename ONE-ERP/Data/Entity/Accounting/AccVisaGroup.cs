using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ONEERP.Data.Entity.Accounting
{
    public class AccVisaGroup : NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int? visaId { get; set; }
        public AccVisaWorkOrder visa { get; set; }
        public int? visaGroupId { get; set; }
        public int? visaWorkOrderId { get; set; }
        [MaxLength(250)]
        public string groupTitle { get; set; }
        [MaxLength(250)]
        public string visaNumber { get; set; }
        [MaxLength(250)]
        public string type { get; set; }
        public int? visaAssigned { get; set; }
        public int? visaUnassigned { get; set; }
        public int? totalVisa { get; set; }
        public int? tradeId { get; set; }
        [MaxLength(250)]
        public string trade { get; set; }
        public decimal? salary { get; set; }
        public int? licenseId { get; set; }
        [MaxLength(250)]
        public string license { get; set; }
        [MaxLength(250)]
        public string sponsorId { get; set; }
        public decimal? purchaseRate { get; set; }
        public decimal? purchaseAmount { get; set; }
        public decimal? serviceCharge { get; set; }
        public decimal? agentCommission { get; set; }
        public decimal? otherCharge { get; set; }
        public decimal? hadia { get; set; }
        public DateTime? purchaseDate { get; set; }
        public int? purchaseVisa { get; set; }
    }
}
