using Microsoft.EntityFrameworkCore;
using ONEERP.Areas.Accounting.Models;

using ONEERP.Data;
using ONEERP.ERPServices.Accounting.MasterData.Interfaces;
using ONEERP.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ONEERP.ERPServices.Accounting.MasterData
{
    public class TransactionModeService : ITransactionModeService
    {
        private readonly ERPDbContext _context;

        public TransactionModeService(ERPDbContext context)
        {
            _context = context;
        }
        public async Task<bool> SaveTransactionMode(string Id, TransactionModeViewModel transactionModeViewModel)
        {

            var result = await _context.saveUpdateViewModels.FromSql($"AccSpSetTransactionMode {Id},{transactionModeViewModel.transactionModeId},{transactionModeViewModel.modeName},{transactionModeViewModel.sortOrder},{transactionModeViewModel.isActive}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }
        public async Task<IEnumerable<TransactionModeListViewModel>> GetTransactionMode()
        {
            var result = await _context.transactionModeListViewModels.FromSql($"AccSpGetTransactionMode {0}").AsNoTracking().ToListAsync();
            return result;
        }

        public async Task<TransactionModeListViewModel> GetTransactionModeById(int id)
        {
            var result = await _context.transactionModeListViewModels.FromSql($"AccSpGetTransactionMode {id}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> GetTransactionModeByIdJson(int id)
        {
            var result = await _context.jsonViewModels.FromSql($"AccSpGetTransactionModeJson {id}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<bool> DeleteTransactionModeById(string Id, int transactionModeId)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"AccSpDeleteTransactionMode {Id},{transactionModeId}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }
    }
}
