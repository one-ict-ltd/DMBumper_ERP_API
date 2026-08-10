using ONEERP.Areas.FieldForceTracking.Models;
using ONEERP.Areas.MasterData.Models;
using ONEERP.Data.Entity.FieldForceTracking;
using ONEERP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace ONEERP.ERPServices.FieldForceTracking.Interfaces
{
    public interface IMapBoxService
    {
        Task<JsonViewModel> GetLatitudeandLongitudebyPerm();
        Task<JsonViewModel> GetLocationMIO(string Type, string ZoneCode, string DepotCode, string RegionCode, string AreaCode, string TerritoryCode, string EmpCode, DateTime Date);

    }
}
