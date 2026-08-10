using Microsoft.EntityFrameworkCore;
using ONEERP.Areas.Accounting.Models;

using ONEERP.Data;
using ONEERP.ERPServices.Accounting.MasterData.Interfaces;
using ONEERP.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ONEERP.ERPServices.Accounting.MasterData
{
    public class LedgerTypeService: ILedgerTypeService
    {
        private readonly ERPDbContext _context;

        public LedgerTypeService(ERPDbContext context)
        {
            _context = context;
        }
        public async Task<bool> SaveLedgerType(string Id, LedgerTypeViewModel ledgerTypeViewModel)
        {

            var result = await _context.saveUpdateViewModels.FromSql($"AccSpSetLedgerType {Id},{ledgerTypeViewModel.ledgerTypeId},{ledgerTypeViewModel.ledgerTypeName},{ledgerTypeViewModel.isActive}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }
        public async Task<IEnumerable<LedgerTypeListViewModel>> GetLedgerType()
        {
            var result = await _context.ledgerTypeListViewModels.FromSql($"AccSpGetLedgerType {0}").AsNoTracking().ToListAsync();
            return result;
        }

        public async Task<LedgerTypeListViewModel> GetLedgerTypeById(int id)
        {
            var result = await _context.ledgerTypeListViewModels.FromSql($"AccSpGetLedgerType {id}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        } 
        public async Task<JsonViewModel> GetLedgerTypeByIdJson(int id)
        {
            var result = await _context.jsonViewModels.FromSql($"AccSpGetLedgerTypeJson {id}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<bool> DeleteLedgerTypeById(string Id, int ledgerTypeId)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"AccSpDeleteLedgerType {Id},{ledgerTypeId}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }
    }
}
