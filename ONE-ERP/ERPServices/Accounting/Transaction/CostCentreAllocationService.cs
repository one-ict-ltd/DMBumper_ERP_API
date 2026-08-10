using Microsoft.EntityFrameworkCore;
using ONEERP.Areas.Accounting.Models;
using ONEERP.Areas.Auth.Models;
using ONEERP.Data;
using ONEERP.ERPServices.Accounting.Transaction.Interfaces;
using ONEERP.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ONEERP.ERPServices.Accounting.Transaction
{
    public class CostCentreAllocationService : ICostCentreAllocationService
    {
        private readonly ERPDbContext _context;

        public CostCentreAllocationService(ERPDbContext context)
        {
            _context = context;
        }
        public async Task<bool> SaveCostCentreAllocation(string Id, List<CostCentreAllocationViewModel> costCentreAllocationViewModels)
        {
            var result = new SaveUpdateViewModel();
            foreach (CostCentreAllocationViewModel costCentreAllocationViewModel in costCentreAllocationViewModels)
            {
                result = await _context.saveUpdateViewModels.FromSql($"AccSpSetCostCentreAllocation {Id},{costCentreAllocationViewModel.costCentreAllocationId},{costCentreAllocationViewModel.costCentreId},{costCentreAllocationViewModel.voucherMasterId},{costCentreAllocationViewModel.voucherDetailId},{costCentreAllocationViewModel.amount},{costCentreAllocationViewModel.isActive}").AsNoTracking().FirstOrDefaultAsync();
            }
           
            return result.isSuccess;
        }
        public async Task<IEnumerable<CostCentreAllocationListViewModel>> GetCostCentreAllocationList()
        {
            var result = await _context.costCentreAllocationListViewModels.FromSql($"AccSpGetCostCentreAllocation {0},{0}").AsNoTracking().ToListAsync();
            return result;
        }
        public async Task<JsonViewModel> GetCostCentreAllocationbyVoucherMasterIdJson(int voucherMasterId)
        {
            var result = await _context.jsonViewModels.FromSql($"AccSpGetCostCentreAllocationJson {voucherMasterId},{0}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<IEnumerable<CostCentreAllocationListViewModel>> GetCostCentreAllocationListbyVoucherMasterId(int voucherMasterId)
        {
            var result = await _context.costCentreAllocationListViewModels.FromSql($"AccSpGetCostCentreAllocation {voucherMasterId},{0}").AsNoTracking().ToListAsync();
            return result;
        }
        public async Task<IEnumerable<CostCentreAllocationListViewModel>> GetCostCentreAllocationListbyVoucherMasterDetailId(int voucherMasterId,int voucherDetailId)
        {
            var result = await _context.costCentreAllocationListViewModels.FromSql($"AccSpGetCostCentreAllocation {voucherMasterId},{voucherDetailId}").AsNoTracking().ToListAsync();
            return result;
        }
        public async Task<bool> DeleteCostCentreAllocationById(string Id, int costCentreAllocationId)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"AccSpDeleteCostCentreAllocation {Id},{costCentreAllocationId}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }
    }
}
