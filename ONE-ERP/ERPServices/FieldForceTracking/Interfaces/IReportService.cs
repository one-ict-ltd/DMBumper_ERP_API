using ONEERP.Models;
using System;
using System.Threading.Tasks;


namespace ONEERP.ERPServices.FieldForceTracking.Interfaces
{
    public interface IReportService
    {
        Task<JsonViewModel> GetSalesReport(string ZONE_CODE, string DEPOT_CODE, string REGION_COE, string AREA_CODE, string TERRITORY_CODE, string EmpId, DateTime FDate, DateTime TDate, int StoreId, int SalesInvoiceId);
        Task<JsonViewModel> GetEmp_DoctorPromotionalItemReportData(string ZONE_CODE, string DEPOT_CODE, string REGION_COE, string AREA_CODE, string TERRITORY_CODE, string EmpId, string DoctorId, DateTime FDate, DateTime TDate);
        Task<JsonViewModel> GetAM_MIOAttendenceReport();

        Task<JsonViewModel> GetMonthlyEmployeeAttendanceReportForFFM(int? userId,DateTime fDate, DateTime tDate, string zoneCode, string depotCode, string regionCode, string areaCode, string territoryCode, string empCode);
    }
}
