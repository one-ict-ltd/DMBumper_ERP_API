using System;
using System.Collections.Generic;
namespace ONEERP.Areas.Production.Models
{
    public class ProductionQaMasterViewModel
    {
        public int productionQaId { get; set; }
        public int? productionPlanId { get; set; }
        public DateTime QCDate { get; set; }
        public int? prdPlanProcessId { get; set; }
        public string remarks { get; set; }
        public string approvalStatus { get; set; }
        public List<ProductionQaDetailsViewModel> QCprocessList { get; set; }
    }
    public class ProductionQaDetailsViewModel
    {
        public int productionQaDetailsId { get; set; }
        public int productionQaId { get; set; }
        public int? TestParameterId { get; set; }
        public string testName { get; set; }
        public decimal? value { get; set; }
        public string result { get; set; }
        public string description { get; set; }

    }
}
