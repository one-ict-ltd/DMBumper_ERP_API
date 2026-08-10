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
    public class AutoStockInOutSettingService : IAutoStockInOutSettingService
    {
        private readonly ERPDbContext _context;

        public AutoStockInOutSettingService(ERPDbContext context)
        {
            _context = context;
        }

        #region Approval Type           

        public async Task<JsonViewModel> GetAutoStockInOutSettingStatusById(int id)
        {
            var result = await _context.jsonViewModels.FromSql($"CmnSpGetAutoStockInOutSettingStatusById {id}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        #endregion

    }
}
