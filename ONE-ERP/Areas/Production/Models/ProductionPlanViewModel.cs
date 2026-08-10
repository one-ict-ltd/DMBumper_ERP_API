using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.Production.Models
{
    public class ProductionPlanViewModel
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
        public string remarksForPlan { get; set; }
    }

    public class ProductionPlanProcessViewModel
    {
        public int prdPlanProcessId { get; set; }
        public int? productionPlanId { get; set; }
        public int? processHeadId { get; set; }
        public int? prdGroupAssignId { get; set; }
        public int? phGroupMasterId { get; set; }
        public int? phGroupDetailId { get; set; }
        public int? productWiseSpecificationId { get; set; }
        public string processStatus { get; set; }
        public DateTime? startTime { get; set; }
        public DateTime? endTime { get; set; }
        /*
            get { return startTime; }
            set
            {
                startTime = Convert.ToDateTime(value.ToString().Replace("T", " "));
            }
         */
        public decimal? totalOutput { get; set; }
        public int? hasQC { get; set; }
        public int? qcApproval { get; set; }
        public string processNote { get; set; }
        public int? processCompleteStatus { get; set; }
        public bool? isSelect { get; set; }
    }

    public class ProductionPlanMachineViewModel
    {
        public int prdPlanMachineId { get; set; }
        public int? prdPlanProcessId { get; set; }
        public int? productionPlanId { get; set; }
        public int? machineInfoId { get; set; }
        public int? uomId { get; set; }
        public DateTime? startingTime { get; set; }
        public DateTime? endingTime { get; set; }
        public decimal? lostTimeHH { get; set; }
        public decimal? lostTimeMM { get; set; }
        public decimal? uploadQty { get; set; }
        public decimal? outputQty { get; set; }
        public string remarks { get; set; }
        public decimal? machineOutput { get; set; }
        public decimal? involvePerson { get; set; }
    }

}
