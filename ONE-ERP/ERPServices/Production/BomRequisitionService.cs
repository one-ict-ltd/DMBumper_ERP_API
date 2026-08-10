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
    public class BomRequisitionService:IBomRequisitionService
    {
        private readonly ERPDbContext _context;
        public BomRequisitionService(ERPDbContext context)
        {
            _context = context;
        }

        #region RMRequisition
        public async Task<JsonViewModel> GetMaxRMRequisitionMasterNumber(DateTime date)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"PrdSpGetMaxReqNoNumberJson {date.ToString("yyyy-MMM-dd")}").AsNoTracking().FirstOrDefaultAsync();
                return result;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public async Task<JsonViewModel> GetMaxPMRequisitionMasterNumber(DateTime date)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"PrdSpGetPMMaxReqNoNumberJson {date.ToString("yyyy-MMM-dd")}").AsNoTracking().FirstOrDefaultAsync();
                return result;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<JsonViewModel> GetProductSpecificatinDataByIdFromBomDetails(int? bomId,int? bomForId,int? userId)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"PrdSpGetBomDetailsforRMRequisition {bomId},{bomForId},{userId}").AsNoTracking().FirstOrDefaultAsync();
                return result;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<JsonViewModel> GetRMRequisitionById(int? requisitionId)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"PrdSpGetRMRequisitionJsonData {requisitionId}").AsNoTracking().FirstOrDefaultAsync();
                return result;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<JsonViewModel> GetRMRequisitionByIdWithDate(DateTime fromDate, DateTime toDate, int? requisitionId, int? userId)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"PrdSpGetRMRequisitionJsonDataWithDate {fromDate},{toDate},{requisitionId},{userId}").AsNoTracking().FirstOrDefaultAsync();
                return result;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<int> SaveRMRequisitionMaster(string Id, RmRequisitionViewModel model)
        {
            try
            {
                var result = await _context.saveUpdateValueViewModels.FromSql($"PrdSpSaveRMRequisitionMaster {Id}, {model.rmRequisitonId}, {model.bomId}, {model.reqNo}, {model.reqDate}, {model.bomMasterProductWiseSpecificationId}, {model.bomQty}, {model.remarks}, {model.status}, {model.type}, {model.productionPlanId},{model.bomForId}").AsNoTracking().FirstOrDefaultAsync();
                return result.isSuccess;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<string> DeleteRMRequisitionById(string Id, int requisitionId)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"PrdSpDeleteRMRequisitionMaster {Id}, {requisitionId}").AsNoTracking().FirstOrDefaultAsync();
                return result.data;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> SaveRMRequisitionDetails(string Id, List<RMRequisitionDetailsViewModel> model, int requisitionId)
        {
            var result = new SaveUpdateValueViewModel();
            foreach (var data in model)
            {
                result = await _context.saveUpdateValueViewModels.FromSql($"PrdSpSaveRMRequisitionDetails {Id}, {data.requisitionDetailId}, {requisitionId}, {data.productWiseSpecificationId}, {data.totalQty}").AsNoTracking().FirstOrDefaultAsync();
            }
            return result.isSuccess;
        }

        public async Task<JsonViewModel> GetRMRequisitionDetailsByMasterId(int? requisitionId, int? userId)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"PrdSpGetRMRequisitionDetailsJsonDataByMasterId {requisitionId},{userId}").AsNoTracking().FirstOrDefaultAsync();
                return result;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Task<bool> DeleteRMRequisitionDetailsById(string userId, int rmRequisitionDetailsId)
        {
            throw new NotImplementedException();
        }

        public async Task<JsonViewModel> GetRequisitionNoForIssue(int type,int userId)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"getRequisitionNoForIssue {type},{userId}").AsNoTracking().FirstOrDefaultAsync();
                return result;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }







        #endregion
        #region Product Issue

        public async Task<JsonViewModel> GetMaxIssueMasterNumber(DateTime date, int type)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"PrdSpGetMaxIssueNo {date.ToString("yyyy-MMM-dd")},{type}").AsNoTracking().FirstOrDefaultAsync();
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
