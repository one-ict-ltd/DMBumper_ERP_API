using ONEERP.Areas.Production.Models;
using ONEERP.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ONEERP.ERPServices.Production.Interfaces
{
    public interface IBomMasterService
    {
        #region BomService Master

        Task<int> SaveBomMaster(int? userId, BomPendingMasterViewModel model);
        Task<int> SaveBomForApproval(int? userId, List<BomMasterModel> model);
        Task<bool> DeleteBomMasterById(int? userId, int pendingbomId);
        Task<JsonViewModel> GetBomMasterById(int? userId, int? pendingbomId);
        Task<JsonViewModel> GetApprovedBomMasterById(int? userId, int? bomId);
        Task<JsonViewModel> GetPendingBomMasterById(int? userId, int? pendingbomId);
        Task<JsonViewModel> GetMaxBomMasterNumber(DateTime date);
        Task<JsonViewModel> GetBomProductWiseSpecification(int productId, int? userId);
        Task<JsonViewModel> GetProductWiseSpecificationWsieBOM(int productId);
        Task<JsonViewModel> GetAllbomForList(int? bomForId);
        Task<JsonViewModel> GetBomTypeIdByName(string bomType);
        Task<JsonViewModel> GetBOMForListFromBOM(int? planId, string materialType);
        Task<JsonViewModel> GetRevisionNoFromBOM(int? productWiseSpecificationId, string materialsType);
        Task<JsonViewModel> GetBomMasterIsApproveOrNot(int? pendingbomId, string materialsType);
        Task<JsonViewModel> GetBomMasterIsExistOrNot(int? bomProductWiseSpecificationId, string materialsType);

        Task<JsonViewModel> GetLastGroupNameForBom(int? productWiseSpecificationId);
        #endregion

        #region BomService Details

        Task<int> SaveBomDetails(int? userId, List<BomPendingDetailsViewModel> model, int pendingbomId);
        Task<JsonViewModel> GetBomDetailsByMasterId(int? pendingbomId);
        Task<bool> DeleteBomDetailsByIdForApprovedBom(int? userId, int bomDetailsId);

        #endregion

        #region Reports

        Task<JsonViewModel> GetBomReportDataById(int? pendingbomId);

        #endregion

        #region Create Sales Auto Voucher  

        //Task<int> CreateAutoJournalForBom(string userId, BomViewModel model);

        #endregion

        #region BOM Approval Edit
        Task<JsonViewModel> GetApprovedBomReportDataById(int? bomId);
        Task<JsonViewModel> GetBomMasterByIdForApprovedBom(int? userId, int? bomId);
        Task<JsonViewModel> GetBomDetailsByMasterIdForApprovedBom(int? userId, int? bomId);
        Task<bool> DeleteBomDetailsById(int? userId, int pendingbomDetailsId);
        Task<int> SaveBomMasterFromApproval(int? userId, BomMasterViewModelForApproval model);
        Task<int> SaveBomDetailsFromApproval(int? userId, List<BomDetailsViewModelForApproval> models, int bomId);
        #endregion

        Task<JsonViewModel> GetAllActiveInActiveBomListJson(int? userId, int? productWiseSpecificationId);

        Task<int> SaveActiveInActiveBom(int? userId, List<BomlstMasterViewModel> model);
    }
}
