
using ONEERP.Areas.Accounting.Models;
using ONEERP.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ONEERP.ERPServices.Accounting.Transaction.Interfaces
{
    public interface ICostCentreAllocationService
    {
        Task<bool> SaveCostCentreAllocation(string Id, List<CostCentreAllocationViewModel> costCentreAllocationViewModels);
        Task<IEnumerable<CostCentreAllocationListViewModel>> GetCostCentreAllocationList();
        Task<IEnumerable<CostCentreAllocationListViewModel>> GetCostCentreAllocationListbyVoucherMasterId(int voucherMasterId);
        Task<IEnumerable<CostCentreAllocationListViewModel>> GetCostCentreAllocationListbyVoucherMasterDetailId(int voucherMasterId, int voucherDetailId);
        Task<JsonViewModel> GetCostCentreAllocationbyVoucherMasterIdJson(int voucherMasterId);
        Task<bool> DeleteCostCentreAllocationById(string Id, int costCentreAllocationId);


    }
}
