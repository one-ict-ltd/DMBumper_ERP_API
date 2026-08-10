using ONEERP.Areas.Hrm.Models;
using ONEERP.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ONEERP.ERPServices.Hrm.EmployeesInfo.Interfaces
{
    public interface IEmployeeRelatedOtherInfoService
    {
        #region Employee Address       
        Task<bool> SaveEmployeeAddress(string userId, EmployeeAddressViewModel model);
        Task<JsonViewModel> GetEmployeeAddressById(int employeeAddressId, int employeeId);
        Task<JsonViewModel> GetDuplicateEmployeeAddress(int employeeAddressId, int employeeId, int addressTypeId);
        Task<bool> DeleteEmployeeAddressById(string userId, int employeeAddressId);

        #endregion

        #region Employee Job Description    

        Task<bool> SaveEmployeeJobDescription(string userId, List<EmployeeJobDescriptionViewModel> jobDescriptionList);
        Task<JsonViewModel> GetEmployeeJobDescriptionById(int employeeJobDescriptionId, int employeeId);
        Task<bool> DeleteEmployeeJobDescriptionById(string userId, int employeeJobDescriptionId);
        #endregion

        #region Employee Related Others Info
        Task<JsonViewModel> GetAllLevelOfEducation();
        Task<JsonViewModel> GetDegreeByLevelOfEducationId(int levelOfEducationId);
        Task<JsonViewModel> GetResultTypes();
        Task<JsonViewModel> GetMejorById(int degreeId);
        Task<bool> SaveEmployeeEducation(string userId, EmployeeEducationViewModel model);
        Task<JsonViewModel> GetEmployeeEducationById(int educationalQualificationId, int employeeId);
        Task<bool> DeleteEmployeeEducationById(string userId, int educationalQualificationId);
        Task<JsonViewModel> GetEmployeeAllRelations();
        Task<JsonViewModel> GetEmployeeFamilyInfoById(int employeeFamilyInfoId, int employeeId);
        Task<bool> SaveEmployeeFamillyInfo(string userId, EmployeeFamilyInfoViewModel model);
        Task<bool> DeleteEmployeeFamilyInfoById(string userId, int familyInfoId);
        Task<JsonViewModel> GetEmployeeEmergencyContactById(int familyInfoId, int employeeId);
        Task<JsonViewModel> GetEmployeeReferenceById(int familyInfoId, int employeeId);
        Task<bool> SetHrmEmployeeAttachment(string user, EmployeeAttachmentUploadViewModel model);
        Task<JsonViewModel> GetEmployeeExperienceById(int employeeExperinceId, int employeeId);
        Task<bool> SaveEmployeeExperience(string userId, EmployeeExperienceViewModel model);
        Task<bool> DeleteEmployeeExperienceById(string userId, int empExperienceId);
        #endregion
    }
}
