using ONEERP.Areas.Salary.Models;
using ONEERP.Models;
using System.Threading.Tasks;

namespace ONEERP.ERPServices.Salary.Interfaces
{
    public interface ISalaryReportService
    {
        #region Salary Report        
        Task<JsonViewModel> RptMonthlySalarySheet(int salaryPeriodId);
        Task<JsonViewModel> HrmPFReportJson(string employeeId);
        Task<JsonViewModel> RptMonthlySalarySheetJson(int companyId, int sbuId, int salaryPeriodId, string reportFormat, string reportType, int? salaryLocation);
        Task<JsonViewModel> SalarySpRptBonusSheetJson(int companyId, int sbuId, int salaryPeriodId, string reportFormat, string reportType, int? salaryLocation);
        #endregion
        Task<JsonViewModel> GetSalarySheetHeadWise(int? userId, int employeeId, int salaryHeadId);
        Task<JsonViewModel> RptMonthlySalaryMobileBill(int employeeId, int salaryPeriodId, int? salaryLocation);
    }
}
