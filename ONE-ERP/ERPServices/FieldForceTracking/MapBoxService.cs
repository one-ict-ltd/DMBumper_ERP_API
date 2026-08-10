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
    public class MapBoxService : IMapBoxService
    {
        private readonly ERPDbContext _context;

        public MapBoxService(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<JsonViewModel> GetLatitudeandLongitudebyPerm()
        {
            try
            {
            var result = await _context.jsonViewModels.FromSql($"getCurrentLocationD").AsNoTracking().FirstOrDefaultAsync();
            return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<JsonViewModel> GetLocationMIO(string Type, string ZoneCode, string DepotCode, string RegionCode, string AreaCode, string TerritoryCode, string EmpCode, DateTime Date)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"getCurrentLocationMIO {ZoneCode},{DepotCode},{RegionCode},{AreaCode},{TerritoryCode},{EmpCode},{Date}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

    }
}
