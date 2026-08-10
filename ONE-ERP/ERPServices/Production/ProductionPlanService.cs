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
    public class ProductionPlanService : IProductionPlanService
    {
        private readonly ERPDbContext _context;
        public ProductionPlanService( ERPDbContext context)
        {
            _context = context;
        }
        public async Task<string> DeleteProductionPlan(string UserId, int? planId)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"PrdSpDeleteProductionPlan {UserId},{planId}").AsNoTracking().FirstOrDefaultAsync();
                return result.data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<string> DeleteProductionProcessQaById(int? UserId, int? productionQaId)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"PrdSpDeleteProductionProcessQa {UserId},{productionQaId}").AsNoTracking().FirstOrDefaultAsync();
                return result.data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<JsonViewModel> GetProductionPlanById(int? planId)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"PrdSpGetPrdProductionPlanViewModelJsonData {planId}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public async Task<JsonViewModel> GetProductionPlanByIdWithDate(DateTime fromDate, DateTime toDate, int? planId, int? userId)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"PrdSpGetPrdProductionPlanViewModelJsonDataWithDate {fromDate}, {toDate},{planId},{userId}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> SaveProductionPlan(string UserId, ProductionPlanViewModel model)
        {
            try
            {
                var result= await _context.saveUpdateValueViewModels.FromSql($"PrdSpSetProductionPlan {UserId},{model.productionPlanId},{model.planDate},{model.productWiseSpecificationId},{model.batchNo},{model.batchWeight},{(model.batchTypeId==0?null:model.batchTypeId)},{(model.bomMasterId==0?null:model.bomMasterId)},{model.chargeNo},{model.batchRatio},{model.stdBatchSize},{model.batchStatusId},{model.thirdPartyStatusId},{model.packingTypeId},{model.flagStatus},{model.manufacturingDate},{model.ExpireDate},{model.remarksForPlan}").AsNoTracking().FirstOrDefaultAsync();
                return result.isSuccess;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        
        public async Task<JsonViewModel> GetBatchTypeById(int? batchTypeId)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"PrdSpGetPrdPrdBatchTypeViewModelJsonData {batchTypeId}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public async Task<JsonViewModel> CheckDuplicatedBatchNo(int? planId, string batchNo)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"PrdSpCheckDuplicatedBatchNo {planId},{batchNo}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public async Task<JsonViewModel> GetProductionPlanForRequisition(int? planId)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"PrdSpGetProductionPlanForRequisition {planId}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public async Task<JsonViewModel> GetProductionPlanForRequisitionWithType(int? planId,string type)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"PrdSpGetProductionPlanForRequisitionWithType {planId},{type}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<JsonViewModel> GetProductionPlanForProdProcess(int? planId)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"PrdSpGetProductionPlanForProdProcess {planId}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<JsonViewModel> GetProductionPlanBatch(int? planId, int? UserId)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"PrdSpGetProductionPlanBatch {planId},{UserId}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<JsonViewModel> GetProductionProcessBatch(int? prdPlanProcessId, int? UserId)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"PrdSpGetProductionProcessBatch {prdPlanProcessId},{UserId}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    
        public async Task<JsonViewModel> GetProductionPlanForStockIn(int? planId)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"PrdSpGetProductionPlanForStockIn {planId}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<JsonViewModel> GetCheckManufacturingAndPackingProcessComplete(int? planId)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"PrdSpGetCheckManufacturingAndPackingProcessComplete {planId}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<JsonViewModel> GetProductionPlanWithType(int? userId, int? planId, string type)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"PrdSpGetProductionPlanWithType {planId},{type},{userId}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<JsonViewModel> GetProductionPlanByIdForApproval(int? userId,int? planId)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"PrdSpGetProductionPlanByIdForApproval {userId},{planId}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> UpdateProductionPlanForApproval(string userId, int? approvalStatus, List<ProductionPlanForApprovalViewModel> lstPlanDetailsViewModel)
        {
            var result = new SaveUpdateValueViewModel();
            foreach (var model in lstPlanDetailsViewModel)
            {
                result = await _context.saveUpdateValueViewModels.FromSql($"PurSpSetProductionPlanForApproval {userId}, {model.productionPlanId},{approvalStatus},{model.isSelect}").AsNoTracking().FirstOrDefaultAsync();
            }
            return result.isSuccess;
        }

        public async Task<string> DeleteProcessMachineById(int? UserId, int? prdPlanMachineId)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"PrdSpDeleteProcessMachineById {UserId},{prdPlanMachineId}").AsNoTracking().FirstOrDefaultAsync();
                return result.data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<JsonViewModel> GetAllQcQaParameterList(int? userId)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"PrdSpGetAllQcQaParameterList {userId}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {
                JsonViewModel jv = new JsonViewModel();
                jv.data = "[]";
                return jv;
            }
        }

        
        public async Task<JsonViewModel> GetPredefineParameterFormat(int? userId, int productionPlanId)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"PrdSpGetPredefineParameterFormat {userId},{productionPlanId}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {
                JsonViewModel jv = new JsonViewModel();
                jv.data = "[]";
                return jv;
            }
        }



        #region transfer note
        public async Task<JsonViewModel> GetTransferedProductionProcessBatch(int? prdPlanProcessId, int? userId)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"PrdSpGetTransferedProductionProcessBatch {prdPlanProcessId},{userId}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<JsonViewModel> GetMaxTransferNoteNumber(DateTime date)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"PrdSpGetMaxTransferNoteNumber {date.ToString("yyyy-MMM-dd")}").AsNoTracking().FirstOrDefaultAsync();
                return result;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<int> SaveTransferNote(string UserId, TransferNoteViewModel model)
        {
            try
            {
                var result = await _context.saveUpdateValueViewModels.FromSql($"PrdSpSetTransferNote {UserId},{model.productTransferId},{model.productionPlanId},{model.transferDate},{model.productWiseSpecificationId},{model.batchNo},{model.batchWeight},{model.qtyPerShipper},{model.remarks},{model.transferIssuedBy},{model.noOfBox},{model.transferNoteNo},{model.equivalentWeight},{model.weightUOMId},{model.prdPlanProcessId},{model.transferQty},{model.manufacturingDate},{model.ExpireDate},{model.totalCommercialQty},{model.transfered},{model.remainQty},{model.isComplete},{model.batchTypeName},{model.SecndproductWiseSpecificationId},{model.sQtyPerPack},{model.sWeightPerPack},{model.MRP}").AsNoTracking().FirstOrDefaultAsync();
                return result.isSuccess;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<JsonViewModel> GetTransferNoteById(int? UserId, int? productTransferId, DateTime? fDate, DateTime? tDate)
        
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"PrdSpGetTransferNoteById {productTransferId}, {UserId}, {fDate}, {tDate}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<bool> DeleteTransferNoteById(int? UserId, int? productTransferId)
        {
            try
            {
                var result = await _context.saveUpdateViewModels.FromSql($"PrdSpDeleteTransferNoteById {UserId},{productTransferId}").AsNoTracking().FirstOrDefaultAsync();
                return result.isSuccess; 
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> UpdateTransferNote(int? UserId, List<BatchReleaseListViewModel> TransferDetailsList)
        {
            var result = new SaveUpdateValueViewModel();
            foreach (var model in TransferDetailsList)
            {
                result = await _context.saveUpdateValueViewModels.FromSql($"PurSpSetTransferNoteForBatchRelease {UserId}, {model.productTransferId}, {model.ReleaseRemarks},{model.isActive},{model.ReleaseDate}").AsNoTracking().FirstOrDefaultAsync();
            }
            return result.isSuccess;
        }
        public async Task<JsonViewModel> GetTransferNoteByIdForBatch(int? UserId, int? productTransferId)

        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"PrdSpGetTransferNoteByIdForBatch {productTransferId}, {UserId}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<JsonViewModel> GetTransferNoteListForStockIn(int? UserId, int? productTransferId)

        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"PrdSpGetTransferNoteListForStockIn {productTransferId}, {UserId}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

    }
}
