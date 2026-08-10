using Microsoft.EntityFrameworkCore;
using ONEERP.Areas.FieldForceTracking.Models;
using ONEERP.Areas.Schedule.Models;
using ONEERP.Data;
using ONEERP.ERPServices.FieldForceTracking.Interfaces;
using ONEERP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace ONEERP.ERPServices.Schedule
{
    public class DashboardService : IDashboardService
    {
        private readonly ERPDbContext _context;

        public DashboardService(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<JsonViewModel> GetSalesVsCollectionChartData(int Totaldays, string ZoneCode, string DepotCode, string RegionCode, string AreaCode, string TerritoryCode, string EmpCode, DateTime FDate)
        {
            var result = await _context.jsonViewModels.FromSql($"FftSpGetSalesVsCollection {Totaldays}, {ZoneCode}, {DepotCode}, {RegionCode}, {AreaCode}, {TerritoryCode}, {EmpCode}, {FDate}").AsNoTracking().FirstOrDefaultAsync();
            return result;            
        }
    }
}
