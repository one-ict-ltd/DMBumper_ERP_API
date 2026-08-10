using ONEERP.Data.Entity.Common;
using ONEERP.Data.Entity.Inventory;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ONEERP.Data.Entity.Production
{
    public class PrdProductionPlan : NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int productionPlanId { get; set; }
        [MaxLength(20)]
        public string planNo { get; set; }
        public DateTime? planDate { get; set; }
        public int? productWiseSpecificationId { get; set; }
        public InvProductWiseSpecification productWiseSpecification { get; set; }
        public string batchNo { get; set; }
        public decimal? batchWeight { get; set; }
        public int? batchTypeId { get; set; }
        public PrdBatchType batchType { get; set; }

        public int? bomMasterId { get; set; }
        public PrdBomMaster bomMaster { get; set; }

        public string chargeNo { get; set; }
        public decimal? batchRatio { get; set; }
        public decimal? stdBatchSize { get; set; }
        public int? batchStatusId { get; set; }
        public int? thirdPartyStatusId { get; set; }
        public int? packingTypeId { get; set; }
        public int? flagStatus { get; set; }
        public int? isTransfer { get; set; }
        public decimal? finalOutputQty { get; set; }
        public DateTime manufacturingDate { get; set; }
        public DateTime ExpireDate { get; set; }
        public int? approvalStatus { get; set; }
        public string remarksForPlan { get; set; }
    }
    public class PrdProductionPlanProcess : NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int prdPlanProcessId { get; set; }
        public int? productionPlanId { get; set; }
        public PrdProductionPlan productionPlan { get; set; }

        public int? processHeadId { get; set; }
        public PrdProcessHead processHead { get; set; }
        public int? prdGroupAssignId { get; set; }
        public PrdProductGroupAssign prdGroupAssign { get; set; }
        public int? phGroupMasterId { get; set; }
        public PrdProcessHeadGroupMaster phGroupMaster { get; set; }
        public int? phGroupDetailId { get; set; }
        public PrdProcessHeadGroupDetails phGroupDetail { get; set; }
        public int? productWiseSpecificationId { get; set; }
        public InvProductWiseSpecification productWiseSpecification { get; set; }
        public string processStatus { get; set; }
        public DateTime? startTime { get; set; }
        public DateTime? endTime { get; set; }
        public decimal? totalOutput { get; set; }
        public int? hasQC { get; set; }
        public int? qcApproval { get; set; }
        public string processNote { get; set; }
        public int? processCompleteStatus { get; set; }
    }
    public class PrdProductionPlanMachine : NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int prdPlanMachineId { get; set; }
        public int? prdPlanProcessId { get; set; }
        public PrdProductionPlanProcess prdPlanProcess { get; set; }
        public int? productionPlanId { get; set; }
        public PrdProductionPlan productionPlan { get; set; }
        public int? machineInfoId { get; set; }
        public PrdMachineInfo machineInfo { get; set; }
        public int? uomId { get; set; }
        public InvProductUOM uom { get; set; }

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


    public class PrdProductionPlanProcessLog : NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public int prdPlanProcessId { get; set; }
        public PrdProductionPlanProcess prdPlanProcess { get; set; }
        public int? productionPlanId { get; set; }
        public PrdProductionPlan productionPlan { get; set; }

        public int? processHeadId { get; set; }
        public PrdProcessHead processHead { get; set; }
        public int? prdGroupAssignId { get; set; }
        public PrdProductGroupAssign prdGroupAssign { get; set; }
        public int? phGroupMasterId { get; set; }
        public PrdProcessHeadGroupMaster phGroupMaster { get; set; }
        public int? phGroupDetailId { get; set; }
        public PrdProcessHeadGroupDetails phGroupDetail { get; set; }
        public int? productWiseSpecificationId { get; set; }
        public InvProductWiseSpecification productWiseSpecification { get; set; }
        public string processStatus { get; set; }
        public DateTime? startTime { get; set; }
        public DateTime? endTime { get; set; }
        public decimal? totalOutput { get; set; }
        public int? hasQC { get; set; }
        public int? qcApproval { get; set; }
        public string processNote { get; set; }
        public int? processCompleteStatus { get; set; }
    }
}
