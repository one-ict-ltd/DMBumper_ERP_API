using Microsoft.EntityFrameworkCore;
using ONEERP.Areas.Auth.Models;
using ONEERP.Areas.MasterData.Models;
using ONEERP.Data;
using ONEERP.ERPServices.MasterData.Interfaces;
using ONEERP.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ONEERP.ERPServices.MasterData
{
    public class CmnSettingService : ICmnSettingService
    {
        private readonly ERPDbContext _context;

        public CmnSettingService(ERPDbContext context)
        {
            _context = context;
        }

        #region Approval Type           

        public async Task<JsonViewModel> GetMenuWiseTransactionDateUnlockList(int? id, int masterId)
        {
            var result = await _context.jsonViewModels.FromSql($"CmnSpGetMenuWiseTransactionDateUnlockJSON {id},{masterId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<JsonViewModel> GetMenuListForTransactionDateUnlock(int? id)
        {
            var result = await _context.jsonViewModels.FromSql($"CmnSpGetMenuListForTransactionDateUnlock {id}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<int> SaveMenuWiseTransactionDateUnlock(int? id, MenuWiseTransactionDateUnlockViewModel item)
        {
            var s = $"CmnSpSetMenuWiseTransactionDateUnlock {id},{item.unlockId},{item.employeeId},{item.menuName},{item.backDays},{item.forwardDays},{item.uptoDate}";
            //foreach (var item in models)
            //{
            var result = await _context.saveUpdateValueViewModels.FromSql($"CmnSpSetMenuWiseTransactionDateUnlock {id},{item.unlockId},{item.employeeId},{item.menuName},{item.backDays},{item.forwardDays},{item.uptoDate}").AsNoTracking().FirstOrDefaultAsync();
            //if (result.isSuccess <= 0) return result.isSuccess;
            //}
            //return 1;
            return result.isSuccess;
        }
        public async Task<int> DeleteMenuWiseTransactionDateUnlock(int? id, int masterId)
        {
            var result = await _context.saveUpdateValueViewModels.FromSql($"CmnSpDeleteMenuWiseTransactionDateUnlock {id},{masterId}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }

        #endregion

    }
}
