using Microsoft.EntityFrameworkCore;
using ONEERP.Areas.Accounting.Models;
using ONEERP.Areas.Auth.Models;
using ONEERP.Data;
using ONEERP.ERPServices.Accounting.Transaction.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ONEERP.ERPServices.Accounting.Transaction
{
    public class VoucherApprovalLogService : IVoucherApprovalLogService
    {
        private readonly ERPDbContext _context;

        public VoucherApprovalLogService(ERPDbContext context)
        {
            _context = context;
        }
        public async Task<int> SaveVoucherApprovalLog(string Id, VoucherApprovalLogViewModel voucherApprovalLogViewModel)
        {
            var result = await _context.saveUpdateValueViewModels.FromSql($"AccSpSetVoucherApprovalLog {Id},{voucherApprovalLogViewModel.voucherAppLogId},{voucherApprovalLogViewModel.voucherMasterId},{voucherApprovalLogViewModel.remarks},{voucherApprovalLogViewModel.isPosted},{voucherApprovalLogViewModel.isActive}").AsNoTracking().FirstOrDefaultAsync(); ;
           
           
            return result.isSuccess;
        }
        public async Task<IEnumerable<VoucherApprovalLogListViewModel>> GetVoucherApprovalLogList()
        {
            var result = await _context.voucherApprovalLogListViewModels.FromSql($"AccSpGetVoucherApprovalLog {0}").AsNoTracking().ToListAsync();
            return result;
        }

        public async Task<IEnumerable<VoucherApprovalLogListViewModel>> GetVoucherApprovalLogbyVoucherMasterId(int voucherMasterId)
        {
            var result = await _context.voucherApprovalLogListViewModels.FromSql($"AccSpGetVoucherApprovalLog {voucherMasterId}").AsNoTracking().ToListAsync();
            return result;
        }
       
    }
}
