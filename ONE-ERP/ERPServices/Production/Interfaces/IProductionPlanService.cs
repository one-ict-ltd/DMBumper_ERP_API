using ONEERP.Areas.Production.Models;
using ONEERP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.ERPServices.Production.Interfaces
{
    public interface IProductionPlanService
    {
        Task<int> SaveProductionPlan(string UserId, ProductionPlanViewModel model);
        Task<string> DeleteProductionPlan(string UserId, int? planId);
        Task<string> DeleteProductionProcessQaById(int? UserId, int? productionQaId);
        Task<JsonViewModel> GetProductionPlanById(int? planId);
        Task<JsonViewModel> GetProductionPlanByIdWithDate(DateTime fromDate, DateTime toDate, int? planId, int? userId);
        Task<JsonViewModel> GetProductionPlanForRequisition(int? planId);
        Task<JsonViewModel> GetProductionPlanForProdProcess(int? planId);
        Task<JsonViewModel> GetProductionPlanBatch(int? planId, int? UserId);
        Task<JsonViewModel> GetProductionProcessBatch(int? prdPlanProcessId,int? UserId);
        Task<JsonViewModel> GetBatchTypeById(int? batchTypeId);
        Task<JsonViewModel> CheckDuplicatedBatchNo(int? planId,string batchNo);
        Task<JsonViewModel> GetProductionPlanForStockIn(int? planId);
        Task<JsonViewModel> GetProductionPlanForRequisitionWithType(int? planId, string type);
        Task<JsonViewModel> GetCheckManufacturingAndPackingProcessComplete(int? planId);
        Task<JsonViewModel> GetTransferNoteById(int? UserId, int? productTransferId, DateTime? fDate, DateTime? tDate);
        Task<JsonViewModel> GetProductionPlanWithType(int? userId, int? planId, string type);
        Task<JsonViewModel> GetProductionPlanByIdForApproval(int? userId, int? planId);
        Task<int> UpdateProductionPlanForApproval(string userId, int? approvalStatus, List<ProductionPlanForApprovalViewModel> lstPlanDetailsViewModel);

        Task<string> DeleteProcessMachineById(int? UserId, int? prdPlanMachineId);
        Task<JsonViewModel> GetAllQcQaParameterList(int? userId);
        Task<JsonViewModel> GetPredefineParameterFormat(int? userId, int productionPlanId);

        #region transfer Note
        Task<JsonViewModel> GetMaxTransferNoteNumber(DateTime transferDate);
        Task<JsonViewModel> GetTransferedProductionProcessBatch(int? prdPlanProcessId, int? userId);
        Task<int> SaveTransferNote(string UserId, TransferNoteViewModel model);
        Task<bool> DeleteTransferNoteById(int? UserId, int? productTransferId);
        Task<int> UpdateTransferNote(int? UserId, List<BatchReleaseListViewModel> TransferDetailsList);

        Task<JsonViewModel> GetTransferNoteByIdForBatch(int? UserId, int? productTransferId);
        Task<JsonViewModel> GetTransferNoteListForStockIn(int? UserId, int? productTransferId);
        #endregion

    }
}
