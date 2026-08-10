using ONEERP.Areas.Salary.Models;
using ONEERP.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ONEERP.ERPServices.Salary.Interfaces
{
    public interface ISalaryStructureService
    { 
        #region Salary Structure

        Task<int> SaveSalaryEmployeeStructure(string userId, SalaryEmployeeStructureViewModel model, decimal structureAmount,int salaryHeadId);
        Task<int> SaveSalaryBankCashStructure(string userId, SalaryEmployeeStructureViewModel model);
        Task<JsonViewModel> GetSalaryAllEmployeeStructure(int employeeId);
        Task<JsonViewModel> GetSalaryEmployeeStructureByEmpId(int employeeId, string salaryHeadType);
        Task<JsonViewModel> GetDuplicateSalaryEmployeeStructure(int employeeId);
        Task<bool> DeleteSalaryEmployeeStructureByEmpId(string userId, int employeeId);
        Task<bool> UpdateSalaryEmployeeStructure(string userId, int employeeStructureId, decimal? amount, bool? isActive);
        
        Task<int> SaveEmployeeSalaryStructureUpload(string userId, List<SalaryEmployeeStructureVerifyViewModel> model);

        Task<int> SaveSalaryEmployeeFixedHeadStructure(string userId, List<SalaryEmployeeFixedHeadStructureViewModel> model);
        Task<bool> DeleteSalaryEmployeeFixedHeadStructure(string userId, int empFixedHeadStructureId);
        Task<JsonViewModel> GetSalaryEmployeeFixedHeadStructureById(int empFixedHeadStructureId,int salaryPeriodIdId,int userId);
        Task<JsonViewModel> GetSalaryHeadByType(string salaryHeadType);
        Task<JsonViewModel> GetSalaryFixedHeadByEmpId(int employeeId);
        Task<List<SalaryEmployeeFixedHeadStructureVerifyViewModel>> GetEmployeeSalaryFixedHeadUploadDataVerify(List<SalaryEmployeeFixedHeadStructureVerifyViewModel> models);
        Task<List<VoucherUploadVerifyViewModel>> GetVoucherUploadDataVerify(List<VoucherUploadVerifyViewModel> models); 
        Task<List<MioSalesTargetViewModel>> GetMioSalesTargetUploadDataVerify(List<MioSalesTargetViewModel> models);
        Task<int> SaveMioItemWiseSalesTarget(string userId, MioSalesTargetMasterViewModel models);
        Task<JsonViewModel> GetMiosalestargetmasterById(int targetMasterId);
        Task<JsonViewModel> GetMiosalestargetmasterwithdetailsById(int targetMasterId);
        Task<bool> DeleteMioItemWiseSalesTarget(string userId, int targetId);
        Task<List<SalaryEmployeeStructureVerifyViewModel>> GetEmployeeSalaryStructureUploadDataVerify(List<SalaryEmployeeStructureVerifyViewModel> models);
        Task<int> UploadEmployeeSalaryStructure(string userId, List<SalaryEmployeeStructureVerifyViewModel> models);
        Task<List<BatchWiseSerialNoVerifyViewModel>> GetBatchWiseSerialNoUploadDataVerify(List<BatchWiseSerialNoVerifyViewModel> models);
        Task<int> SaveBatchWiseSerialNoUpload(string userId, List<BatchWiseSerialNoVerifyViewModel> models);
        Task<JsonViewModel> CheckBatchWiseSerialNo(string serialNo);
        #endregion

        #region Salary Process

        Task<bool> ProcessEmployeesSalary(string userId, int salaryPeriodId);
        Task<bool> SaveSalaryProcessLog(string userId, SalaryEmployeeProcessViewModel model);
        Task<JsonViewModel> GetSalaryMasterByPeriodId(int salaryPeriodId,int userId, string salaryDepotName);
        Task<List<MobileBillVerifyViewModel>> GetMobileBillUploadDataVerify(List<MobileBillVerifyViewModel> models);
        Task<int> SaveEmployeeMobileBill(string userId, List<MobileBillVerifyViewModel> models);
        Task<JsonViewModel> GetEmployeeMobileBillById(int salaryPeriodIdId, int userId);
        Task<int> UpdateEmployeeDesignationAndDepartment(string userId, SalaryEmployeeStructureViewModel model);
        #endregion

        #region Increment
        Task<List<SalaryEmployeeIncrementVerifyViewModel>> GetEmployeeIncrementUploadDataVerify(List<SalaryEmployeeIncrementVerifyViewModel> models);
        Task<int> SaveEmployeeSalaryIncrementUpload(string userId, List<SalaryEmployeeIncrementVerifyViewModel> models);
        #endregion
    }
}
