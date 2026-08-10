using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.Production.Models
{
    public class ProductionPlanApprovalViewModel
    {
        public int? ApprovalStatus { get; set; }
        public List<ProductionPlanForApprovalViewModel> lstPlanDetailsViewModel { get; set; }
    }

    public class ProductionPlanForApprovalViewModel
    {
        public int? productionPlanId { get; set; }
        public DateTime? planDate { get; set; }
        public int? productWiseSpecificationId { get; set; }
        public string batchNo { get; set; }
        public decimal? batchWeight { get; set; }
        public int? batchTypeId { get; set; }
        public int? bomMasterId { get; set; }
        public string chargeNo { get; set; }
        public decimal? batchRatio { get; set; }
        public decimal? stdBatchSize { get; set; }
        public int? batchStatusId { get; set; }
        public int? thirdPartyStatusId { get; set; }
        public int? packingTypeId { get; set; }
        public int? flagStatus { get; set; }
        public DateTime manufacturingDate { get; set; }
        public DateTime ExpireDate { get; set; }

        public bool? isSelect { get; set; }
    }
}
