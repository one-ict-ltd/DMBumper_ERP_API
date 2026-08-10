
using ONEERP.Areas.Accounting.Models;
using ONEERP.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ONEERP.ERPServices.Accounting.MasterData.Interfaces
{
    public interface IVoucherStatusService
    {
        Task<bool> SaveVoucherStatus(string Id, VoucherStatusViewModel voucherStatusViewModel);
        Task<IEnumerable<VoucherStatusListViewModel>> GetVoucherStatus();
        Task<VoucherStatusListViewModel> GetVoucherStausById(int id);
        Task<JsonViewModel> GetVoucherStausByIdJson(int id);
        Task<bool> DeleteVoucherStatusById(string Id, int voucherStausId);
     
    }
}
