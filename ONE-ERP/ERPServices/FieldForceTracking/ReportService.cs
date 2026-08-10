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


namespace ONEERP.ERPServices.FieldForceTracking
{
    public class ReportService : IReportService
    {
        private readonly ERPDbContext _context;

        public ReportService(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<JsonViewModel> GetSalesReport(string ZONE_CODE, string DEPOT_CODE, string REGION_COE, string AREA_CODE, string TERRITORY_CODE, string EmpId, DateTime FDate, DateTime TDate, int StoreId, int SalesInvoiceId)
        {
            var result = await _context.jsonViewModels.FromSql($"FFTSpGetSalesReport {ZONE_CODE},{DEPOT_CODE},{REGION_COE},{AREA_CODE},{TERRITORY_CODE},{EmpId},{FDate},{TDate},{StoreId},{SalesInvoiceId}").AsNoTracking().FirstOrDefaultAsync();
            return result;            
        }
        public async Task<JsonViewModel> GetEmp_DoctorPromotionalItemReportData(string ZONE_CODE, string DEPOT_CODE, string REGION_COE, string AREA_CODE, string TERRITORY_CODE, string EmpId, string DoctorId, DateTime FDate, DateTime TDate)
        {
            var result = await _context.jsonViewModels.FromSql($"FftSpGetDoctorPromotionalItemReportJson {ZONE_CODE},{DEPOT_CODE},{REGION_COE},{AREA_CODE},{TERRITORY_CODE},{EmpId},{DoctorId},{FDate},{TDate}").AsNoTracking().FirstOrDefaultAsync();
            return result;            
        }
        public async Task<JsonViewModel> GetAM_MIOAttendenceReport()
        {
            var result = await _context.jsonViewModels.FromSql($"AM_MIOWiseDailyAppsLoginReportJson").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> GetMonthlyEmployeeAttendanceReportForFFM(int? userId, DateTime fDate, DateTime tDate, string zoneCode, string depotCode, string regionCode, string areaCode, string territoryCode, string empCode)
        {
            var result = await _context.jsonViewModels.FromSql($"FftSpGetMonthlyEmployeeAttendanceReportForFFM {userId},{fDate},{tDate},{zoneCode},{depotCode},{regionCode},{areaCode},{territoryCode},{empCode}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
    }
}
