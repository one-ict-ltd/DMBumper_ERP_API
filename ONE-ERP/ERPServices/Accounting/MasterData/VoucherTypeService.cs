using Microsoft.EntityFrameworkCore;
using ONEERP.Areas.Accounting.Models;
using ONEERP.Areas.Auth.Models;
using ONEERP.Data;
using ONEERP.ERPServices.Accounting.MasterData.Interfaces;
using ONEERP.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ONEERP.ERPServices.Accounting.MasterData
{
    public class VoucherTypeService : IVoucherTypeService
    {
        private readonly ERPDbContext _context;

        public VoucherTypeService(ERPDbContext context)
        {
            _context = context;
        }

        #region Voucher Type

        public async Task<bool> SaveVoucherType(string Id, VoucherTypeViewModel voucherTypeViewModel)
        {

            var result = await _context.saveUpdateViewModels.FromSql($"AccSpSetVoucherType {Id},{voucherTypeViewModel.voucherTypeId},{voucherTypeViewModel.voucherTypeName},{voucherTypeViewModel.aliasName},{voucherTypeViewModel.isActive}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }
        public async Task<IEnumerable<VoucherTypeListViewModel>> GetVoucherType()
        {
            var result = await _context.voucherTypeListViewModels.FromSql($"AccSpGetVoucherType {0}").AsNoTracking().ToListAsync();
            return result;
        }

        public async Task<VoucherTypeListViewModel> GetVoucherTypeById(int id)
        {
            var result = await _context.voucherTypeListViewModels.FromSql($"AccSpGetVoucherType {id}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> GetVoucherTypeByIdJson(int id)
        {
            var result = await _context.jsonViewModels.FromSql($"AccSpGetVoucherTypeJson {id}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<bool> DeleteVoucherTypeById(string Id, int voucherTypeId)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"AccSpDeleteVoucherType {Id},{voucherTypeId}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }

        #endregion

        #region Auto Voucher Name
        public async Task<int> SaveAutoVoucherName(string id, AutoVoucherNameViewModel model)
        {
            var result = await _context.saveUpdateValueViewModels.FromSql($"AccSpSetAutoVoucherName {id},{model.autoVoucherNameId},{model.autoVoucherName},{model.shortName},{model.isActive}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }

        public async Task<JsonViewModel> GetAutoVoucherNameById(int autoVoucherNameId)
        {
            var result = await _context.jsonViewModels.FromSql($"AccSpGetAutoVoucherNameJson {autoVoucherNameId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<bool> DeleteAutoVoucherNameById(string id, int autoVoucherNameId)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"AccSpDeleteAutoVoucherName {id},{autoVoucherNameId}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }

        #endregion

        #region Auto Voucher Master
        public async Task<int> SaveAutoVoucherMaster(string id, AutoVoucherMasterViewModel model)
        {
            var result = await _context.saveUpdateValueViewModels.FromSql($"AccSpSetAutoVoucherMaster {id},{model.autoVoucherMasterId},{model.autoVoucherNameId},{model.voucherTypeId},{model.companyId},{model.sbuId},{model.description},{model.isActive}").AsNoTracking().FirstOrDefaultAsync();

            return result.isSuccess;
        }

        public async Task<JsonViewModel> GetAutoVoucherMasterById(int companyId, int sbuId, int autoVoucherMasterId)
        {
            var result = await _context.jsonViewModels.FromSql($"AccSpGetAutoVoucherMasterJson {companyId},{sbuId},{autoVoucherMasterId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<bool> DeleteAutoVoucherMasterById(string id, int autoVoucherMasterId)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"AccSpDeleteAutoVoucherMaster {id},{autoVoucherMasterId}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }

        #endregion

        #region Auto Voucher Detail
        public async Task<int> SaveAutoVoucherDetail(string id, List<AutoVoucherDetailViewModel> autoVoucherDetailViewModels, int autoVoucherMasterId)
        {
            await _context.saveUpdateViewModels.FromSql($"AccSpDeleteAutoVoucherDetail {id},{autoVoucherMasterId},{0}").AsNoTracking().FirstOrDefaultAsync();
            var result = new SaveUpdateValueViewModel();
            foreach (AutoVoucherDetailViewModel model in autoVoucherDetailViewModels)
            {
                result = await _context.saveUpdateValueViewModels.FromSql($"AccSpSetAutoVoucherDetail {id},{0},{autoVoucherMasterId},{model.transactionModeId},{model.ledgerId},{model.isActive}").AsNoTracking().FirstOrDefaultAsync();
            }
            return result.isSuccess;
        }

        public async Task<JsonViewModel> GetAutoVoucherDetailByMasterId(int autoVoucherMasterId)
        {
            var result = await _context.jsonViewModels.FromSql($"AccSpGetAutoVoucherDetailJson {autoVoucherMasterId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<bool> DeleteAutoVoucherDetailById(string id, int autoVoucherDetailId)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"AccSpDeleteAutoVoucherDetail {id},{0},{autoVoucherDetailId}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }

        #endregion
    }
}
