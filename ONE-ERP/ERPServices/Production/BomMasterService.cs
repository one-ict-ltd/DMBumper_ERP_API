using Microsoft.EntityFrameworkCore;
using ONEERP.Areas.Auth.Models;
using ONEERP.Areas.Production.Models;
using ONEERP.Data;
using ONEERP.ERPServices.Production.Interfaces;
using ONEERP.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ONEERP.ERPServices.Production
{
    public class BomMasterService : IBomMasterService
    {
        private readonly ERPDbContext _context;
        public BomMasterService(ERPDbContext context)
        {
            _context = context;
        }

        #region BomService Master

        public async Task<int> SaveBomMaster(int? userId, BomPendingMasterViewModel model)
        {
            try
            {
                var result = await _context.saveUpdateValueViewModels.FromSql($"PrdSpSetBomMaster {userId}, {model.pendingbomId}, {model.bomNo},  {model.bomName}, {model.bomDescription}, {model.bomProductWiseSpecificationId}, {model.bomQty}, {model.bomTotalCost}, {model.isActive}, {model.bomDate},{model.materialsType},{model.bomType},{model.weightPerPack},{model.WeightPerPackUOM},{model.batchWeight},{model.batchWeightUOMId},{model.phGroupMasterId},{model.shelfLife},{model.packSizeForPM}").AsNoTracking().FirstOrDefaultAsync();
                return result.isSuccess;

            }
            catch (Exception ex)
            {
                throw;
            }

        }
        public async Task<int> SaveBomForApproval(int? userId, List<BomMasterModel> models)
        {
            try
            {
                var result = new SaveUpdateValueViewModel();

                foreach (var model in models)
                {
                     result = await _context.saveUpdateValueViewModels.FromSql($"PrdSpSetBomMasterForApproval {userId}, {model.pendingbomId}, {model.approvalStatus}").AsNoTracking().FirstOrDefaultAsync();
                  
                }
                return result.isSuccess;
            }
            catch (Exception ex)
            {
                throw;
            }

        }
        public async Task<bool> DeleteBomMasterById(int? userId, int pendingbomId)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"PrdSpDeleteBomMaster {userId}, {pendingbomId}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }

        public async Task<JsonViewModel> GetBomMasterById(int? userId, int? pendingbomId)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"PrdSpGetBomMasterListJson {pendingbomId},{userId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<JsonViewModel> GetApprovedBomMasterById(int? userId, int? bomId)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"PrdSpGetApprovedBomMasterListJson {bomId},{userId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<JsonViewModel> GetPendingBomMasterById(int? userId, int? pendingbomId)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"PrdSpGetPendingBomMasterListJson {pendingbomId},{userId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<JsonViewModel> GetMaxBomMasterNumber(DateTime date)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"PrdSpGetMaxBomNumberJson {date.ToString("yyyy-MMM-dd")}").AsNoTracking().FirstOrDefaultAsync();
                return result;

            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<JsonViewModel> GetBomProductWiseSpecification(int productId, int? userId)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"PrdSpGetBomProductWiseSpecification {productId},{userId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<JsonViewModel> GetProductWiseSpecificationWsieBOM(int productId)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"PrdSpGetProductWiseSpecificationWsieBOM {productId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion

        #region Sales Offer Details

        public async Task<int> SaveBomDetails(int? userId, List<BomPendingDetailsViewModel> models, int pendingbomId)
        {
            try
            {
                var result = new SaveUpdateValueViewModel();
            foreach (var model in models)
            {
                result = await _context.saveUpdateValueViewModels.FromSql($"PrdSpSetBomDetails {userId}, {model.pendingbomDetailsId}, {pendingbomId}, {model.bomDetailsProductWiseSpecificationId}, {model.qty}, {model.price}, {model.totalPrice}, {model.isActive}, {model.isSelect}, {model.wastage}, {model.totalQty},{model.assay},{model.potencyEffect},{model.bomForId}").AsNoTracking().FirstOrDefaultAsync();
            }
            return result.isSuccess;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<bool> DeleteBomDetailsById(int? userId, int pendingbomDetailsId)
        {
            try
            {
                var result = await _context.saveUpdateViewModels.FromSql($"PrdSpDeleteBomDetails {userId}, {pendingbomDetailsId}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<JsonViewModel> GetBomTypeIdByName(string bomType)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"PrdSpGetBomTypeIdByName {bomType}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<JsonViewModel> GetBOMForListFromBOM(int? planId, string materialType)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"PrdSpGetBOMForListFromBOM {planId},{materialType}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<JsonViewModel> GetBomDetailsByMasterId(int? pendingbomId)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"PrdSpGetBomDetailsListJson {pendingbomId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<JsonViewModel> GetAllbomForList(int? bomForId)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"PrdSpGetAllbomForList {bomForId}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<JsonViewModel> GetRevisionNoFromBOM(int? productWiseSpecificationId, string materialsType)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"PrdSpGetRevisionNoFromBOM {productWiseSpecificationId},{materialsType}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<JsonViewModel> GetBomMasterIsApproveOrNot(int? pendingbomId, string materialsType)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"PrdSpGetBomMasterIsApproveOrNot {pendingbomId},{materialsType}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<JsonViewModel> GetBomMasterIsExistOrNot(int? bomProductWiseSpecificationId, string materialsType)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"PrdSpGetBomMasterIsExistOrNot {bomProductWiseSpecificationId},{materialsType}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<JsonViewModel> GetLastGroupNameForBom(int? productWiseSpecificationId)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"PrdSpGetLastGroupNameForBom {productWiseSpecificationId}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion

        #region Reports

        public async Task<JsonViewModel> GetBomReportDataById(int? pendingbomId)
        {
            var result = await _context.jsonViewModels.FromSql($"PrdSpGetBomReportJson {pendingbomId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> GetApprovedBomReportDataById(int? bomId)
        {
            var result = await _context.jsonViewModels.FromSql($"PrdSpGetApprovedBomReportJson {bomId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        #endregion

        #region Create Sales Auto Voucher       

        //public async Task<int> CreateAutoJournalForBom(string userId, BomViewModel model)
        //{
        //    var result = await _context.saveUpdateValueViewModels.FromSql($"AccSpCreateBomJournal {userId},{model.grandTotal},{model.BomDate},{model.BomNo},{model.partyId}").AsNoTracking().FirstOrDefaultAsync();

        //    return result.isSuccess;
        //}

        #endregion

        #region BOM Approval Edit
        public async Task<JsonViewModel> GetBomMasterByIdForApprovedBom(int? userId, int? bomId)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"PrdSpGetBomMasterByIdForApprovedBom {bomId},{userId}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<JsonViewModel> GetBomDetailsByMasterIdForApprovedBom(int? userId, int? bomId)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"PrdSpGetBomDetailsByMasterIdForApprovedBom {bomId},{userId}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<bool> DeleteBomDetailsByIdForApprovedBom(int? userId, int bomDetailsId)
        {
            try
            {
                var result = await _context.saveUpdateViewModels.FromSql($"PrdSpDeleteBomDetailsByIdForApprovedBom {userId}, {bomDetailsId}").AsNoTracking().FirstOrDefaultAsync();
                return result.isSuccess;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<int> SaveBomMasterFromApproval(int? userId, BomMasterViewModelForApproval model)
        {
            try
            {
                var result = await _context.saveUpdateValueViewModels.FromSql($"PrdSpSaveBomMasterFromApproval {userId}, {model.bomId}, {model.pendingbomId}, {model.bomNo},  {model.bomName}, {model.bomDescription}, {model.bomProductWiseSpecificationId}, {model.bomQty}, {model.bomTotalCost}, {model.isActive}, {model.bomDate},{model.materialsType},{model.bomType},{model.weightPerPack},{model.WeightPerPackUOM},{model.batchWeight},{model.batchWeightUOMId},{model.phGroupMasterId},{model.shelfLife},{model.packSizeForPM}").AsNoTracking().FirstOrDefaultAsync();
                return result.isSuccess;

            }
            catch (Exception ex)
            {
                throw;
            }

        }
        public async Task<int> SaveBomDetailsFromApproval(int? userId, List<BomDetailsViewModelForApproval> models, int bomId)
        {
            try
            {
                var result = new SaveUpdateValueViewModel();
                foreach (var model in models)
                {
                    result = await _context.saveUpdateValueViewModels.FromSql($"PrdSpSaveBomDetailsFromApproval {userId}, {model.bomDetailsId} ,{model.pendingbomDetailsId}, {bomId}, {model.bomDetailsProductWiseSpecificationId}, {model.qty}, {model.price}, {model.totalPrice}, {model.isActive}, {model.isSelect}, {model.wastage}, {model.totalQty},{model.assay},{model.potencyEffect},{model.bomForId}").AsNoTracking().FirstOrDefaultAsync();
                }
                return result.isSuccess;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion
        public async Task<JsonViewModel> GetAllActiveInActiveBomListJson(int? userId, int? productWiseSpecificationId)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"PrdSpGetAllActiveInActiveBomListJson {productWiseSpecificationId},{userId}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<int> SaveActiveInActiveBom(int? userId, List<BomlstMasterViewModel> models)
        {
            try
            {
                var result = new SaveUpdateValueViewModel();
                foreach (var model in models)
                {
                        result = await _context.saveUpdateValueViewModels.FromSql($"PrdSpSaveActiveInActiveBom {userId}, {model.bomId} ,{model.isSelect}").AsNoTracking().FirstOrDefaultAsync();
                    
                }
                return result.isSuccess;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
