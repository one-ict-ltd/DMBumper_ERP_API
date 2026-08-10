using Microsoft.EntityFrameworkCore;
using ONEERP.Areas.Accounting.Models;

using ONEERP.Data;
using ONEERP.ERPServices.Accounting.MasterData.Interfaces;
using ONEERP.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ONEERP.ERPServices.Accounting.MasterData
{
    public class FundSourceService: IFundSourceService
    {
        private readonly ERPDbContext _context;

        public FundSourceService(ERPDbContext context)
        {
            _context = context;
        }
        public async Task<bool> SaveFundSource(string Id, FundSourceViewModel fundSourceViewModel)
        {

            var result = await _context.saveUpdateViewModels.FromSql($"AccSpSetFundSource {Id},{fundSourceViewModel.fundSourceId},{fundSourceViewModel.fundSourceName},{fundSourceViewModel.aliasName},{fundSourceViewModel.companyId},{fundSourceViewModel.sbuId},{fundSourceViewModel.isActive}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }
        public async Task<IEnumerable<FundSourceListViewModel>> GetFundSourceList()
        {
            var result = await _context.fundSourceListViewModels.FromSql($"AccSpGetFundSource {0},{0},{0}").AsNoTracking().ToListAsync();
            return result;
        }

        public async Task<FundSourceListViewModel> GetFundSourceById(int id)
        {
            var result = await _context.fundSourceListViewModels.FromSql($"AccSpGetFundSource {id},{0},{0}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> GetFundSourceByIdJson(int id,int companyId,int sbuId)
        {
            var result = await _context.jsonViewModels.FromSql($"AccSpGetFundSourceJson {id},{companyId},{sbuId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<IEnumerable<FundSourceListViewModel>> GetFundSourceListbyCompanyId(int companyId)
        {
            var result = await _context.fundSourceListViewModels.FromSql($"AccSpGetFundSource {0},{companyId},{0}").AsNoTracking().ToListAsync();
            return result;
        }
        public async Task<IEnumerable<FundSourceListViewModel>> GetFundSourceListbySbuId(int sbuId)
        {
            var result = await _context.fundSourceListViewModels.FromSql($"AccSpGetFundSource {0},{0},{sbuId}").AsNoTracking().ToListAsync();
            return result;
        }

        public async Task<JsonViewModel> GetDuplicateFundSource(int fundSourceId, string fundSourceName)
        {
            var result = await _context.jsonViewModels.FromSql($"AccSpGetDuplicateFundSource {fundSourceId},{fundSourceName}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<bool> DeleteFundSourceById(string Id, int fundSourceId)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"AccSpDeleteFundSource {Id},{fundSourceId}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }
    }
}
