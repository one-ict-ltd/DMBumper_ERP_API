using Microsoft.EntityFrameworkCore;
using ONEERP.Areas.Auth.Models;
using ONEERP.Areas.Purchase.Models;
using ONEERP.Data;
using ONEERP.ERPServices.Purchase.Interfaces;
using ONEERP.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ONEERP.ERPServices.Purchase
{
    public class GRNService : IGRNService
    {
        private readonly ERPDbContext _context;
        public GRNService(ERPDbContext context)
        {
            _context = context;
        }
        public async Task<JsonViewModel> getGRNForQA(int? userId)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"PurSpGetGRNInfoForQAJSON {userId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
            }
            catch (System.Exception ex)
            {

                throw ex;
            }
        }
        public async Task<JsonViewModel> getGRNForRetest(int? userId)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"PurSpGetGRNForRetestJSON {userId}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        public async Task<JsonViewModel> getGRNImportForQA(int? userId)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"PurSpGetGRNImportInfoForQAJSON {userId}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (System.Exception ex)
            {

                throw ex;
            }
        }
        public async Task<JsonViewModel> getGrnDetailsForQA(int? grnMasterId,string InitialOrRetest)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"PurSpGetGRNDetailsForQAJSON {grnMasterId},{InitialOrRetest}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (System.Exception ex)
            {

                throw ex;
            }
        }
        public async Task<JsonViewModel> getGrnDetailsForRetest(int? grnMasterId, string grnType)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"PurSpGetGrnDetailsForRetestJSON {grnMasterId},{grnType}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (System.Exception ex)
            {

                throw ex;
            }
        }
        public async Task<JsonViewModel> getGrnImportDetailsForQA(int? ImpgrnMasterId, string InitialOrRetest)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"PurSpGetGRNImportDetailsForQAJSON {ImpgrnMasterId},{InitialOrRetest}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (System.Exception ex)
            {

                throw ex;
            }
        }
        public async Task<int> UpdateGRNQaForApproval(int? userId, int? approvalStatus, List<grnlist> models, DateTime? RetestDate,string InitialOrRetest)
        {
            try
            {
                var result = new SaveUpdateValueViewModel();
            foreach (var model in models)
            {
                result = await _context.saveUpdateValueViewModels.FromSql($"PurSpSetGRNForQAApproval {userId}, {model.grnMasterId},{model.grnDetailsId},{approvalStatus},{model.isSelect},{model.potency},{model.approvedQty},{RetestDate},{model.QCRefNo},{InitialOrRetest},{model.grnLogMasterId}").AsNoTracking().FirstOrDefaultAsync();
            }
            return result.isSuccess;
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        public async Task<int> UpdateGRNQaMasterForApproval(int? userId, int? approvalStatus, List<grnlist> models)
        {
            try
            {
                var result = new SaveUpdateValueViewModel();
               
                    result = await _context.saveUpdateValueViewModels.FromSql($"PurSpSetGRNMasterForQAApproval {userId}, {models[0].grnMasterId},{approvalStatus}").AsNoTracking().FirstOrDefaultAsync();
                return result.isSuccess;
            }
            catch (System.Exception ex)
            {

                throw ex;
            }
        }
        public async Task<int> UpdateGRNImportQaMasterForApproval(int? userId, int? approvalStatus, List<grnImportqalist> models)
        {
            try
            {
                var result = new SaveUpdateValueViewModel();

                result = await _context.saveUpdateValueViewModels.FromSql($"PurSpSetGRNImportMasterForQAApproval {userId}, {models[0].ImpgrnMasterId},{approvalStatus}").AsNoTracking().FirstOrDefaultAsync();
                return result.isSuccess;
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        public async Task<int> UpdateGRNImportQaForApproval(int? userId, int? approvalStatus, List<grnImportqalist> models, DateTime? RetestDate, string InitialOrRetest)
        {
            try
            {
                var result = new SaveUpdateValueViewModel();
                foreach (var model in models)
                {
                    result = await _context.saveUpdateValueViewModels.FromSql($"PurSpSetGRNImportForQAApproval {userId}, {model.ImpgrnMasterId},{model.grnDetailsId},{approvalStatus},{model.isSelect},{model.potency},{model.approvedQty},{RetestDate},{model.QCRefNo},{InitialOrRetest},{model.grnLogMasterId}").AsNoTracking().FirstOrDefaultAsync();
                }
                return result.isSuccess;
            }
            catch (System.Exception ex)
            {

                throw ex;
            }
        }
        public async Task<int> SaveGrnLogtbl(int? userId, PurGrnLogViewModel model)
        {
            try
            {
                var result = new SaveUpdateValueViewModel();

                result = await _context.saveUpdateValueViewModels.FromSql($"PurSpSetGrnLogtbl {userId}, {model.grnLogMasterId},{model.grnDetailsId},{model.RetestDate},{model.TestReqQty},{model.NoOfPackForRetest},{model.grnType}").AsNoTracking().FirstOrDefaultAsync();
                return result.isSuccess;
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
    }
}
