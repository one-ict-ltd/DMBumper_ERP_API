using Microsoft.EntityFrameworkCore;
using ONEERP.Areas.Auth.Models;
using ONEERP.Areas.Production.Models;
using ONEERP.Data;
using ONEERP.ERPServices.Production.Interfaces;
using ONEERP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.ERPServices.Production
{
    public class ProductionProcessService : IProductionProcessService
    {
        private readonly ERPDbContext _context;
        public ProductionProcessService( ERPDbContext context)
        {
            _context = context;
        }

        public async Task<bool> DeleteProductionProcessHead(string UserId, int headId)
        {
            try
            {
                var result = await _context.saveUpdateViewModels.FromSql($"PrdSpDeleteProccessHead {UserId},{headId}").AsNoTracking().FirstOrDefaultAsync();
                return result.isSuccess;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<JsonViewModel> GetProductionProductionProcessHeadById(int headId)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"PrdSpGetProductionProcessViewModelJsonData {headId}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> SaveProductionProcessHead(string UserId, ProductionProcessHeadViewModel model)
        {
            try
            {
                var result= await _context.saveUpdateValueViewModels.FromSql($"PrdSpSaveProductionPorcess {UserId},{model.processHeadId},{model.headName},{model.headCode},{model.description},{model.shortName},{model.shortOrder},{model.isQA},{model.isActive}").AsNoTracking().FirstOrDefaultAsync();
                return result.isSuccess;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task<JsonViewModel> GetMachineInfoById(int? UserId, int machineId)
        
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"PrdSpGetMachineInfoJsonData {machineId},{UserId}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> SaveMachineInfo(string UserId, MachineInfoViewModel model)
        {
            try
            {
                var result= await _context.saveUpdateValueViewModels.FromSql($"PrdSpSaveMachineInfo {UserId},{model.machineInfoId},{model.machineName},{model.machineCode},{model.originCountry},{model.purchaseDate},{model.startDate},{model.purchaseAmount},{model.status},{model.remarks}").AsNoTracking().FirstOrDefaultAsync();
                return result.isSuccess;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> DeleteMachineInfo(string UserId, int machineId)
        {
            try
            {
                var result = await _context.saveUpdateViewModels.FromSql($"PrdSpDeleteMachinerInfo {UserId},{machineId}").AsNoTracking().FirstOrDefaultAsync();
                return result.isSuccess;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        #region ProcessGroup
        public async Task<int> SaveProductionProcessGroup(string UserId, ProcessHeadGroupViewModel model)
        {
            try
            {
                var result = await _context.saveUpdateValueViewModels.FromSql($"PrdSpSaveProductionPorcessGroup {UserId},{model.phGroupMasterId},{model.productionTypeId},{model.groupName}").AsNoTracking().FirstOrDefaultAsync();
                return result.isSuccess;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<bool> DeleteProductionProcessGroupById(string UserId, int phGroupMasterId)
        {
            try
            {
                var result = await _context.saveUpdateViewModels.FromSql($"PrdSpDeleteProccessHeadGroup {UserId},{phGroupMasterId}").AsNoTracking().FirstOrDefaultAsync();
                return result.isSuccess;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<JsonViewModel> GetProductionProcessGroupById(int userId,int phGroupMasterId)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"PrdSpGetProductionProcessGroupById {userId},{phGroupMasterId}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


     

        #endregion

        #region ProcessGroup Details

        public async Task<int> SaveProcessGroupDetails(string userId, List<ProcessHeadGroupDetailsViewModel> details, int phGroupMasterId)
        {
            var result = new SaveUpdateValueViewModel();
            foreach (var model in details)
            {
                result = await _context.saveUpdateValueViewModels.FromSql($"PrdSpSetProcessHeadGroupDetails {userId}, {model.phGroupDetailId}, {phGroupMasterId}, {model.processHeadId}, {model.headOrder},{model.isQA},{model.hasQC}").AsNoTracking().FirstOrDefaultAsync();
            }
            return result.isSuccess;
        }
        public async Task<bool> DeleteProcessGroupDetailsById(string userId, int phGroupDetailId)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"PrdSpDeleteProccessHeadGroupDetails {userId}, {phGroupDetailId}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }
        public async Task<JsonViewModel> GetProcessGroupDetailsById(int phGroupMasterId)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"PrdSpGetProcessGroupDetailsByMasterId {phGroupMasterId}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Group Wise Assign

        public async Task<JsonViewModel> GetProductGroupAssignById(int prdGroupAssignId)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"PrdSpGetProductGroupAssignJsonData {prdGroupAssignId}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<int> SaveProductGroupAssign(string userId, List<ProductGroupWiseItems> details, int? phGroupMasterId, int? prdGroupAssignId)
        {
            var result = new SaveUpdateValueViewModel();
            foreach (var model in details.Where(x=> x.isSelect))
            {
                result = await _context.saveUpdateValueViewModels.FromSql($"PrdSpSetProductGroupAssign {userId}, {prdGroupAssignId}, {model.productWiseSpecificationId}, {phGroupMasterId}").AsNoTracking().FirstOrDefaultAsync();
            }
            return result.isSuccess;
        }
        public async Task<JsonViewModel> GetGroupWiseProductSpecs(int? productionTypeId)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"PrdSpGetGroupWiseProductSpecs {productionTypeId}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public async Task<bool> DeleteProductGroupAssignByGroupMasterId(string userId, int? phGroupMasterId)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"PrdSpDeleteProductGroupAssign {userId}, {phGroupMasterId}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }
        #endregion

        #region Group Wise Assign

        public async Task<JsonViewModel> GetProductionPlanProcessById(int? userId, int? productionPlanId, int? productionTypeId, int? productWiseSpecificationId)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"PrdSpGetProductionPlanProcessById {userId},{productionPlanId}, {productionTypeId}, {productWiseSpecificationId}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<JsonViewModel> GetProductionPlanMachineById(int? userId, int? prdPlanProcessId)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"PrdSpGetProductionPlanMachineById {userId},{prdPlanProcessId}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<JsonViewModel> GetBatchWiseProcesses(int? productWiseSpecificationId, int? productionTypeId)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"PrdSpGetBatchWiseProcesses {productWiseSpecificationId},{productionTypeId}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> SaveProductionProcess(string UserId, List<ProductionPlanProcessViewModel> models)
        {
            var result = new SaveUpdateValueViewModel();
            try
            {
                foreach (var m in models)
                {
                    //if (m.isSelect == true)// 
                    //{
                        result = await _context.saveUpdateValueViewModels.FromSql($"PrdSpSetProductionPlanProcess {UserId},{m.prdPlanProcessId},{m.productionPlanId},{m.processHeadId},{m.prdGroupAssignId},{m.phGroupMasterId},{m.phGroupDetailId},{m.productWiseSpecificationId},{m.processStatus},{m.startTime},{m.endTime},{m.totalOutput},{m.hasQC},{m.qcApproval},{m.processNote},{m.processCompleteStatus},{m.isSelect}").AsNoTracking().FirstOrDefaultAsync();
                   // }
                }
            }
            catch (Exception ex)
            {
                result.isSuccess = 0;
            }
            return result.isSuccess;
        }

        public async Task<int> SaveProductionMachine(string UserId, List<ProductionPlanMachineViewModel> models)
        {
            var result = new SaveUpdateValueViewModel();
            try
            {
                foreach (var m in models)
                {
                    result = await _context.saveUpdateValueViewModels.FromSql($"PrdSpSetProductionPlanMachine {UserId},{m.prdPlanMachineId},{m.prdPlanProcessId},{m.productionPlanId},{m.machineInfoId},{m.uomId},{m.startingTime},{m.endingTime},{m.lostTimeHH},{m.lostTimeMM},{m.uploadQty},{m.outputQty},{m.remarks},{m.machineOutput},{m.involvePerson}").AsNoTracking().FirstOrDefaultAsync();
                }
            }
            catch (Exception ex)
            {
                result.isSuccess = 0;
            }
            return result.isSuccess;
        }
        
        public async Task<int> SetProcessTransfer(int? userId, int? productionPlanId, decimal? outputQty)
        {
            var result = new SaveUpdateValueViewModel();
            try
            {               
                    result = await _context.saveUpdateValueViewModels.FromSql($"PrdSpSetProcessTransfer {userId},{productionPlanId},{outputQty}").AsNoTracking().FirstOrDefaultAsync();                
            }
            catch (Exception ex)
            {
                result.isSuccess = 0;
            }
            return result.isSuccess;
        }

        public async Task<string> DeleteProductionProcessById(string UserId, int productionPlanProcessId)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"PrdSpDeleteProccessById {UserId},{productionPlanProcessId}").AsNoTracking().FirstOrDefaultAsync();
                return result.data;
            }
            catch (Exception ex)
            {
                return "Something went wrong!";
            }
        }

        #endregion

        public async Task<int> SaveProductionQA(int? UserId, ProductionQaMasterViewModel model)
        {
            try
            {
                var result = await _context.saveUpdateValueViewModels.FromSql($"PrdSpSetProductionQaMaster {UserId},{model.productionQaId},{model.QCDate},{model.prdPlanProcessId},{model.productionPlanId},{model.approvalStatus},{model.remarks}").AsNoTracking().FirstOrDefaultAsync();
                return result.isSuccess;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> SaveProductionQADetail(int? UserId, int productionQaId, List<ProductionQaDetailsViewModel> model)
        {
            var result = 0;
            try
            {
                foreach (var data in model)
                {
                    var res = await _context.saveUpdateValueViewModels.FromSql($"PrdSpSetProductionQaDetail {UserId},{data.productionQaDetailsId},{productionQaId},{data.testName},{data.value},{data.result},{data.description},{data.TestParameterId}").AsNoTracking().FirstOrDefaultAsync();
                    result = res.isSuccess;
                }
            }
            catch (Exception ex)
            {
                result = 0;
            }
            return result;
        }
        public async Task<JsonViewModel> GetProductionQAById(int productionQaId)

        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"PrdSpGetProductionQA {productionQaId}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<JsonViewModel> GetProductionQAByIdDate(int? userId, DateTime fromDate, DateTime toDate, int productionQaId)

        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"PrdSpGetProductionQAWithDate {fromDate}, {toDate},{productionQaId},{userId}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
