
using ONEERP.Areas.Accounting.Models;
using ONEERP.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ONEERP.ERPServices.Accounting.Transaction.Interfaces
{
    public interface IVoucherMasterService
    {
        Task<int> SaveVoucherMaster(string Id, VoucherMasterViewModel voucherMasterViewModel);
        Task<IEnumerable<VoucherMasterListViewModel>> GetVoucherMasterList();
        Task<IEnumerable<VoucherMasterListViewModel>> GetVoucherMasterListbyVoucherMasterId(int voucherMasterId);
        Task<IEnumerable<VoucherMasterListViewModel>> GetVoucherMasterListbyVoucherDate(DateTime voucherDate);
        Task<IEnumerable<VoucherMasterListViewModel>> GetVoucherMasterListbyVoucherTypeId(int voucherTypeId);
        Task<IEnumerable<VoucherMasterListViewModel>> GetVoucherMasterListbyPostStatus(int isPosted);
        Task<JsonViewModel> GetVoucherMasterListbyVoucherMasterIdJson(int voucherMasterId,int voucherTypeId);
        Task<JsonViewModel> GetUploadedVoucherListJson(int userId,int voucherTypeId);
        Task<bool> DeleteVoucherMasterById(string Id, int voucherMasterId);
        Task<JsonViewModel> GetVoucherNoJson(int voucherType, DateTime voucherDate,int IsCheque);
        Task<JsonViewModel> GetBalanceAmountByLedgerJson(int ledgerId, int? partyId);
        Task<JsonViewModel> CheckLockFiscalYear(string voucherDate);
        Task<JsonViewModel> GetVoucherMasterListbyVoucherMasterForPostingIdJson(int employeeId, int voucherMasterId, int voucherTypeId, int isPost);
        Task<int> UpdateVoucherMaster(string Id, int ispost, VoucherPostingViewModel voucherPostingViewModel);
        Task<JsonViewModel> GetVoucherMasterListbyVoucherMasterIdDateJson(int voucherMasterId, int voucherTypeId, DateTime fromDate, DateTime toDate, int employeeId);
        Task<JsonViewModel> GetVoucherEditDeleteCheckJson(int voucherMasterId, int employeeId);
        Task<int> SaveVoucherMasterExcel(string Id, VoucherMasterViewModelExcel voucherMasterViewModel);
        Task<VoucherMasterViewModel> ConvertVoucherExcelToVoucherMaster(VoucherMasterViewModelExcel voucherMasterViewModel);

        Task<JsonViewModel> GetVoucherMasterListbyVoucherMasterForPostingIdFactoryJson(int voucherMasterId, int voucherTypeId, int isPost);
    }
}
