
using ONEERP.Areas.Accounting.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ONEERP.ERPServices.Accounting.Transaction.Interfaces
{
    public interface IVoucherApprovalLogService
    {
        Task<int> SaveVoucherApprovalLog(string Id, VoucherApprovalLogViewModel voucherApprovalLogViewModel);
        Task<IEnumerable<VoucherApprovalLogListViewModel>> GetVoucherApprovalLogList();
        Task<IEnumerable<VoucherApprovalLogListViewModel>> GetVoucherApprovalLogbyVoucherMasterId(int voucherMasterId);


    }
}
