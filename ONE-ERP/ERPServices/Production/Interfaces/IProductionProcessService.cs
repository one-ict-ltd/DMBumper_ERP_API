using ONEERP.Areas.Production.Models;
using ONEERP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.ERPServices.Production.Interfaces
{
    public interface IProductionProcessService
    {
        Task<int> SaveProductionProcessHead(string UserId, ProductionProcessHeadViewModel model);
        Task<bool> DeleteProductionProcessHead(string UserId,  int headId);
        Task<string> DeleteProductionProcessById(string UserId, int productionPlanProcessId);
        Task<JsonViewModel> GetProductionProductionProcessHeadById(int headId);


        #region machine info
        Task<int> SaveMachineInfo(string UserId, MachineInfoViewModel model);
        Task<bool> DeleteMachineInfo(string UserId, int machineId);
        Task<JsonViewModel> GetMachineInfoById(int? UserId, int machineId);

        #endregion

        #region ProcessGroup
        Task<int> SaveProductionProcessGroup(string UserId, ProcessHeadGroupViewModel model);
        Task<bool> DeleteProductionProcessGroupById(string UserId, int headId);
        Task<JsonViewModel> GetProductionProcessGroupById(int userId, int headId);
      
        #endregion


        #region ProcessGroup Details

        Task<int> SaveProcessGroupDetails(string userId, List<ProcessHeadGroupDetailsViewModel> model, int phGroupMasterId);
        Task<JsonViewModel> GetProcessGroupDetailsById(int phGroupMasterId);
        Task<bool> DeleteProcessGroupDetailsById(string userId, int phGroupDetailId);

        #endregion

        #region Group Wise Assign

        Task<int> SaveProductGroupAssign(string userId, List<ProductGroupWiseItems> model, int? phGroupMasterId, int? prdGroupAssignId);
        Task<JsonViewModel> GetProductGroupAssignById(int prdGroupAssignId);
         Task<JsonViewModel> GetGroupWiseProductSpecs(int? phGroupMasterId);
        //Task<JsonViewModel> GetGroupWiseProductSpecs(int? productWiseSpecificationId, int? productionTypeId);
        Task<bool> DeleteProductGroupAssignByGroupMasterId(string userId, int? phGroupMasterId);
        #endregion

        #region ProductionProcess

        Task<JsonViewModel> GetProductionPlanProcessById(int? userId, int? productionPlanId, int? productionTypeId, int? productWiseSpecificationId);
        Task<JsonViewModel> GetProductionPlanMachineById(int? userId, int? prdPlanProcessId);
        Task<JsonViewModel> GetBatchWiseProcesses(int? productWiseSpecificationId, int? productionTypeId);
        Task<int> SaveProductionProcess(string UserId, List<ProductionPlanProcessViewModel> model);
        Task<int> SaveProductionMachine(string UserId, List<ProductionPlanMachineViewModel> models);
        Task<int> SetProcessTransfer(int? userId, int? productionPlanId, decimal? outputQty);

        #endregion
        Task<int> SaveProductionQA(int? UserId, ProductionQaMasterViewModel model);
        Task<int> SaveProductionQADetail(int? UserId, int productionQaId, List<ProductionQaDetailsViewModel> model);
        Task<JsonViewModel> GetProductionQAById(int productionQaId);
        Task<JsonViewModel> GetProductionQAByIdDate(int? userId, DateTime fromDate, DateTime toDate, int productionQaId);
    }
}
