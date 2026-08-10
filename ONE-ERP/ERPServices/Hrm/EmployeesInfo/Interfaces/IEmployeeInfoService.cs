using ONEERP.Areas.Hrm.Models;
using ONEERP.Data.Entity.FieldForceTracking;
using ONEERP.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace ONEERP.ERPServices.Hrm.EmployeesInfo.Interfaces
{
    public interface IEmployeeInfoService
    {
        #region Employee For User Create & GET DON'T CHANGE THIS service

        //DONT CHANGE THIS service
        Task<bool> SaveEmployeeForCreateUser(string id, EmployeeViewModel employeeViewModel);
        Task<JsonViewModel> GetEmployeeById(int companyId, int employeeId);

        #endregion

        #region Employee Info    
        Task<JsonViewModel> GetMaxEmployeeNo(int companyId);
        Task<bool> SaveEmployeeBasicInfo(string id, EmployeeInformationViewModel employeeViewModel);
        Task<bool> UpdateSalesLimit(string id, string territoryCode);
        Task<bool> UpdatePostingLocation(string id, UpdatePostingViewModel updatePostingViewModel);
        Task<JsonViewModel> GetEmployeeBasicInfoById(int employeeId);
        Task<JsonViewModel> GetEmployeeBasicInfoByCompanyId(int? userId, int employeeId);
        Task<JsonViewModel> GetDispatcher(int? employeeId);
        Task<bool> DeleteEmployeeById(string id, int employeeId);
        Task<JsonViewModel> GetDuplicateEmployeeNo(int employeeId, string employeeNo);
        Task<bool> UpdateEmployeeFirebaseToken(string id, EmployeeFireBaseViewModel employeeViewModel);
        Task<bool> SaveEmployeeMessageInfo(string id, CmnMessageInfo model);
        Task<JsonViewModel> GetMessageInfoById(int employeeId);
        Task<bool> SaveEmployeeOtherExpense(string id, EmployeeOtherExpenseViewModel employeeViewModel);
        Task<JsonViewModel> GetEmployeeOtherExpense(int employeeId, int otherExpenseId);
        Task<bool> DeleteEmployeeOtherExpense(string id, int otherExpenseId);
        

        Task<JsonViewModel> GetGetEmployeeInfoLoadById(int employeeId, int userId);
        Task<JsonViewModel> GetEmployeeInfoWhoHasLeaveById(int employeeId, int userId);
        Task<JsonViewModel> GetEmployeeInfoLoadByIdOptimized(int employeeId, int userId);
        Task<JsonViewModel> GetEmployeeInfoLoadByIdOptimizedForPaySlip(int employeeId, int userId);


        Task<JsonViewModel> GetGetEmployeeInfoByPosting(int employeeId, int userId);
        Task<JsonViewModel> GetLoanCategoryJson();
        Task<JsonViewModel> GetEmployeeWithLoan(int loanCategoryId);
        Task<JsonViewModel> GetEmployeeLoanDetails(int loanId);
        Task<bool> CancelLoan(int loanId, int userId);
        Task<JsonViewModel> GetInterestTypeJson();
        Task<int> SaveLoanInfo(string id, LoanInfoViewModel employeeViewModel);
        Task<JsonViewModel> GetLoanInformation(int loanId, int employeeId,int userId);
        Task<JsonViewModel> GetEmployeeInfoLoadByIdAndCompany(int companyId, int employeeId);
        Task<JsonViewModel> getDuplicateTerritoty(int employeeId, string PostingLocation, string Code);

        #endregion
        Task<JsonViewModel> GetEmployeeBasicInfoByIdNew(int employeeId);
        Task<JsonViewModel> GetEmployeeBasicInfoByIdOptimized(int employeeId);

        Task<List<EmployeeInfoUploadVerifyViewModel>> GetEmployeeInfoUploadDataVerify(List<EmployeeInfoUploadVerifyViewModel> models);

        Task<int> SaveEmployeeInfoFromExcel(string userId, List<EmployeeInfoUploadVerifyViewModel> models);

        Task<JsonViewModel> GetPayrollEmployeeById(int companyId, int employeeId);

        Task<List<InactiveEmployeeInfoUploadVerifyViewModel>> GetInactiveEmployeeInfoUploadDataVerify(List<InactiveEmployeeInfoUploadVerifyViewModel> models);

        Task<int> SaveInactiveEmployeeInfoFromExcel(string userId, List<InactiveEmployeeInfoUploadVerifyViewModel> models);

        #region Employee Transfer    
        Task<bool> SaveEmployeeTransfer(string id, EmployeeTransferViewModel employeeTransferViewModel);
        Task<JsonViewModel> GetEmployeeTransferById(int employeeTransferId);
        Task<bool> DeleteEmployeeTransfer(string id, int employeeTransferId);

        #endregion

        #region Employee Promotion
        Task<bool> SaveEmployeePromotion(string id, EmployeePromotionViewModel employeePromotionViewModel);
        Task<JsonViewModel> GetEmployeePromotionById(int employeePromotionId);
        Task<JsonViewModel> GetEmployeeConfirmationById(int employeePromotionId);
        Task<bool> deleteEmployeePromotion(string id, int employeePromotionId);
        Task<JsonViewModel> GetEmployeeBasicInfoByIdForESS(int employeeId);
        Task<JsonViewModel> GetLeaveSummaryForESSJson(int employeeId, int year);
        Task<JsonViewModel> GetCelebtationForESSJson(int employeeId);

        #endregion

        #region external employee
        Task<dynamic> SaveDemoEmployeeBasicInfo(string id, EmployeeInformationViewModel employeeViewModel);
        
        #endregion

    }
}
