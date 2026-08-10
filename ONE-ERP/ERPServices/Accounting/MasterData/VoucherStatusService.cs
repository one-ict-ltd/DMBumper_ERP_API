using Microsoft.EntityFrameworkCore;
using ONEERP.Areas.Accounting.Models;

using ONEERP.Data;
using ONEERP.ERPServices.Accounting.MasterData.Interfaces;
using ONEERP.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ONEERP.ERPServices.Accounting.MasterData
{
    public class VoucherStatusService : IVoucherStatusService
    {
        private readonly ERPDbContext _context;

        public VoucherStatusService(ERPDbContext context)
        {
            _context = context;
        }
        public async Task<bool> SaveVoucherStatus(string Id, VoucherStatusViewModel voucherStatusViewModel)
        {

            var result = await _context.saveUpdateViewModels.FromSql($"AccSpSetVoucherStatus {Id},{voucherStatusViewModel.voucherStatusId},{voucherStatusViewModel.statusName},{voucherStatusViewModel.isActive}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }
        public async Task<IEnumerable<VoucherStatusListViewModel>> GetVoucherStatus()
        {
            var result = await _context.voucherStatusListViewModels.FromSql($"AccSpGetVoucherStatus {0}").AsNoTracking().ToListAsync();
            return result;
        }

        public async Task<VoucherStatusListViewModel> GetVoucherStausById(int id)
        {
            var result = await _context.voucherStatusListViewModels.FromSql($"AccSpGetVoucherStatus {id}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> GetVoucherStausByIdJson(int id)
        {
            var result = await _context.jsonViewModels.FromSql($"AccSpGetVoucherStatusJson {id}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<bool> DeleteVoucherStatusById(string Id, int voucherStausId)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"AccSpDeleteVoucherStatus {Id},{voucherStausId}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }
    }
}
