using System;

namespace ONEERP.Areas.Accounting.Models
{
    public class VisaGroupViewModel
    {
        public int? visa_group_id { get; set; }
        public int? visa_id { get; set; }
        public int? visaWorkOrderId { get; set; }
        public int? visa_WorkOrder_Id { get; set; }
        public string group_title { get; set; }       
        public string visa_number { get; set; }
        public string type { get; set; }
        public int? assigned_visas { get; set; }
        public int? unassigned_visas { get; set; }
        public int? total_visas { get; set; }
        public int? trade_id { get; set; }
        public string trade { get; set; }
        public decimal? salary { get; set; }
        public int? license_id { get; set; }
        public string license { get; set; }
        public string sponsor_id { get; set; }
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
