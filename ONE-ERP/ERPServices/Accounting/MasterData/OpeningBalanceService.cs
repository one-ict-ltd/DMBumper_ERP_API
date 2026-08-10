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
    public class OpeningBalanceService : IOpeningBalanceService
    {
        private readonly ERPDbContext _context;

        public OpeningBalanceService(ERPDbContext context)
        {
            _context = context;
        }
        public async Task<bool> SaveOpeningBalance(string Id, OpeningBalanceViewModel openingBalanceViewModel)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"AccSpSetOpeningBalance {Id},{openingBalanceViewModel.openingBalanceId},{openingBalanceViewModel.ledgerId},{openingBalanceViewModel.partyId},{openingBalanceViewModel.transactionModeId},{openingBalanceViewModel.balanceUpTo},{openingBalanceViewModel.amount},{openingBalanceViewModel.companyId},{openingBalanceViewModel.sbuId},{openingBalanceViewModel.isActive},{openingBalanceViewModel.description}").AsNoTracking().FirstOrDefaultAsync();

            return result.isSuccess;
        }
        public async Task<IEnumerable<OpeningBalanceListViewModel>> GetOpeningBalanceList()
        {
            var result = await _context.openingBalanceListViewModels.FromSql($"AccSpGetOpeningBalance {0},{0},{0}").AsNoTracking().ToListAsync();
            return result;
        }

        public async Task<IEnumerable<OpeningBalanceListViewModel>> GetOpeningBalanceListbyLedgerId(int ledgerId)
        {
            var result = await _context.openingBalanceListViewModels.FromSql($"AccSpGetOpeningBalance {0},{ledgerId},{0}").AsNoTracking().ToListAsync();
            return result;
        }
        public async Task<IEnumerable<OpeningBalanceListViewModel>> GetOpeningBalanceListbyLedgerIdPartyId(int ledgerId,int partyId)
        {
            var result = await _context.openingBalanceListViewModels.FromSql($"AccSpGetOpeningBalance {0},{ledgerId},{partyId}").AsNoTracking().ToListAsync();
            return result;
        }
        public async Task<IEnumerable<OpeningBalanceListViewModel>> GetOpeningBalanceListbyPartyId(int partyId)
        {
            var result = await _context.openingBalanceListViewModels.FromSql($"AccSpGetOpeningBalance {0},{0},{partyId}").AsNoTracking().ToListAsync();
            return result;
        }
        public async Task<OpeningBalanceListViewModel> GetOpeningBalanceListbyId(int openingBalanceId)
        {
            var result = await _context.openingBalanceListViewModels.FromSql($"AccSpGetOpeningBalance {openingBalanceId},{0},{0}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        } 
        public async Task<JsonViewModel> GetOpeningBalanceListbyIdJson(int openingBalanceId,int ledgerId,int partyId)
        {
            var result = await _context.jsonViewModels.FromSql($"AccSpGetOpeningBalanceJson {openingBalanceId},{ledgerId},{partyId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> GetDuplicateOpeningBalance(int openingBalanceId, int ledgerId, int? partyId)
        {
            var result = await _context.jsonViewModels.FromSql($"AccSpGetDuplicateOpeningBalance {openingBalanceId},{ledgerId},{partyId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<bool> DeleteOpeningBalanceById(string Id, int openingBalanceId)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"AccSpDeleteOpeningBalance {Id},{openingBalanceId}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }
    }
}
