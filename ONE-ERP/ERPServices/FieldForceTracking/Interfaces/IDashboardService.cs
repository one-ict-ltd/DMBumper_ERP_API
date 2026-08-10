using ONEERP.Models;
using System;
using System.Threading.Tasks;


namespace ONEERP.ERPServices.FieldForceTracking.Interfaces
{
    public interface IDashboardService// 20-Jan-2022
    {
        Task<JsonViewModel> GetSalesVsCollectionChartData(int Totaldays, string ZoneCode, string DepotCode, string RegionCode, string AreaCode, string TerritoryCode, string EmpCode, DateTime FDate);
    }
}
