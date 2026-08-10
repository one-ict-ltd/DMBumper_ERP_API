using Microsoft.EntityFrameworkCore;
using ONEERP.Areas.Salary.Models;
using ONEERP.Data;
using ONEERP.ERPServices.Salary.Interfaces;
using ONEERP.Models;
using System.Threading.Tasks;

namespace ONEERP.ERPServices.Salary
{
    public class SalaryReportService : ISalaryReportService
    {
        private readonly ERPDbContext _context;

        public SalaryReportService(ERPDbContext context)
        {
            _context = context;
        }


        #region Salary Report


        public async Task<JsonViewModel> RptMonthlySalarySheet(int salaryPeriodId)
        {
            var result = await _context.jsonViewModels.FromSql($"SalarySpRptMonthlySalarySheet {salaryPeriodId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        
        public async Task<JsonViewModel> HrmPFReportJson(string employeeId)
        {
            var result = await _context.jsonViewModels.FromSql($"HrmPFReportJson {employeeId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<JsonViewModel> RptMonthlySalarySheetJson(int companyId, int sbuId, int salaryPeriodId, string reportFormat, string reportType, int? salaryLocation)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"SalarySpRptMonthlySalarySheetJson {companyId},{sbuId},{salaryPeriodId},{reportType},0,0,{salaryLocation}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (System.Exception ex)
            {

                throw ex;
            }
        }
        public async Task<JsonViewModel> SalarySpRptBonusSheetJson(int companyId, int sbuId, int salaryPeriodId, string reportFormat, string reportType, int? salaryLocation)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"SalarySpRptBonusSheetJson {companyId},{sbuId},{salaryPeriodId},{salaryLocation}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (System.Exception ex)
            {

                throw ex;
            }
        }

        public async Task<JsonViewModel> RptMonthlySalaryMobileBill(int employeeId, int salaryPeriodId, int? salaryLocation)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"SalarySpRptMobileBillSheetJson {employeeId},{salaryPeriodId},{salaryLocation}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (System.Exception ex)
            {

                throw ex;
            }
        }

        #endregion
        public async Task<JsonViewModel> GetSalarySheetHeadWise(int? userId, int employeeId, int salaryHeadId)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"HrmIndividualHeadWiseSalaryReportJson {userId},{employeeId},{salaryHeadId}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (System.Exception ex)
            {

                throw ex;
            }
        }
    }
}
