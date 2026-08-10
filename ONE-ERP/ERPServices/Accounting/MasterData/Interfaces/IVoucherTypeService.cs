
using ONEERP.Areas.Accounting.Models;
using ONEERP.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ONEERP.ERPServices.Accounting.MasterData.Interfaces
{
    public interface IVoucherTypeService
    {
        #region  Voucher Type
        Task<bool> SaveVoucherType(string Id, VoucherTypeViewModel voucherTypeViewModel);
        Task<IEnumerable<VoucherTypeListViewModel>> GetVoucherType();
        Task<VoucherTypeListViewModel> GetVoucherTypeById(int id);
        Task<bool> DeleteVoucherTypeById(string Id, int voucherTypeId);
        Task<JsonViewModel> GetVoucherTypeByIdJson(int id);

        #endregion

        #region  Auto Voucher Name

        Task<int> SaveAutoVoucherName(string id, AutoVoucherNameViewModel model);
        Task<JsonViewModel> GetAutoVoucherNameById(int autoVoucherNameId);
        Task<bool> DeleteAutoVoucherNameById(string id, int autoVoucherNameId);

        #endregion

        #region  Auto Voucher Master

        Task<int> SaveAutoVoucherMaster(string id, AutoVoucherMasterViewModel model);
        Task<JsonViewModel> GetAutoVoucherMasterById(int companyId, int sbuId, int autoVoucherMasterId);
        Task<bool> DeleteAutoVoucherMasterById(string id, int autoVoucherMasterId);

        #endregion

        #region Auto Voucher Detail

        Task<int> SaveAutoVoucherDetail(string id, List<AutoVoucherDetailViewModel> autoVoucherDetailViewModels, int autoVoucherMasterId);
        Task<JsonViewModel> GetAutoVoucherDetailByMasterId(int autoVoucherMasterId);
        Task<bool> DeleteAutoVoucherDetailById(string id, int autoVoucherDetailId);

        #endregion

    }
}
