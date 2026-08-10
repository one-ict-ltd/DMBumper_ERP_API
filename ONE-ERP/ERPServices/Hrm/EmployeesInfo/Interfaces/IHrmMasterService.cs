using ONEERP.Areas.Hrm.Controllers;
using ONEERP.Areas.Hrm.Models;
using ONEERP.Models;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;

namespace ONEERP.ERPServices.Hrm.EmployeesInfo.Interfaces
{
    public interface IHrmMasterService
    {
        #region Employee Status

        Task<bool> SaveEmployeeStatus(string id, EmployeeStatusViewModel employeeViewModel);
        Task<JsonViewModel> GetEmployeeStatusById(int employeeStatusId);
        Task<bool> DeleteEmployeeStatusById(string id, int employeeId);

        #endregion

        #region Activity Type -------------
        Task<bool> SaveActivityType(string id, ActivityTypeViewModel employeeViewModel);
        Task<JsonViewModel> GetActivityTypeById(int activityTypeId);
        Task<bool> DeleteActivityTypeById(string id, int activityTypeId);

        #endregion

        #region Department ----------------
        Task<bool> SaveDepartment(string id, DepartmentViewModel employeeViewModel);
        Task<JsonViewModel> GetDepartmentById(int departmentId);
        Task<bool> DeleteDepartmentById(string id, int activityTypeId);

        #endregion

        #region Designation ----------------

        Task<bool> SaveDesignation(string id, DesignationViewModel employeeViewModel);
        Task<JsonViewModel> GetDesignationById(int designationId);
        Task<bool> DeleteDesignationById(string id, int DesignationId);
        Task<JsonViewModel> GetDesignationBySalarySlabId(int? employeeId, int? salarySlabId);
        Task<JsonViewModel> GetEmployeeDesignationByEmployeeId(int? employeeId, int? empId);
        Task<int> SetSlabDesignation(string userId, IEnumerable<SalarySlabDesignationViewModel> models);
        Task<JsonViewModel> GetSlabDesignationById(int? userId, int? slabDesignationId);
        Task<bool> DeleteSlabDesignation(string id, int slabDesignationId);

        #endregion

        #region Employee Type----------------

        Task<bool> SaveEmployeeType(string id, EmployeeTypeViewModel employeeViewModel);
        Task<JsonViewModel> GetEmployeeTypeById(int designationId);
        Task<bool> DeleteEmployeeTypeById(string id, int employeeTypeId);

        #endregion

        #region Employee Relation----------

        Task<bool> SaveEmployeeRelation(string id, EmployeeRelationViewModel employeeViewModel);
        Task<JsonViewModel> GetEmployeeRelationById(int relationId);
        Task<bool> DeleteEmployeeRelationById(string id, int relationId);

        #endregion

        #region Employee Religion--------

        Task<bool> SaveEmployeeReligion(string id, EmployeeReligionViewModel employeeViewModel);
        Task<JsonViewModel> GetEmployeeReligionById(int relationId);
        Task<bool> DeleteEmployeeReligionById(string id, int religionId);

        #endregion

        #region Address Type
        Task<JsonViewModel> GetAddressType();

        #endregion

        #region Division---------------------
        Task<bool> SaveDivision(string id, DivisionViewModel DivisionViewModel);
        Task<JsonViewModel> GetDivisionById(int countryId);
        Task<bool> DeleteDivisionById(string id, int divisionsId);
        #endregion

        #region District -------------
        Task<bool> SaveDistrict(string id, DistrictViewModel DivisionViewModel);
        Task<JsonViewModel> GetDistrictById(int divisionId);
        Task<bool> DeleteDistrictById(string id, int districtsId);
        #endregion

        #region Thanas -------------
        Task<bool> SaveThanas(string id, ThanasViewModel DivisionViewModel);
        Task<JsonViewModel> GetThanasById(int districtsId);
        Task<bool> DeleteThanasById(string id, int thanasId);
        #endregion

        #region Employee Religion--------

        Task<bool> SaveMunicipilityLocation(string id, MunicipilityLocationViewModel employeeViewModel);
        Task<JsonViewModel> GetMunicipilityLocationById(int MunicipilityLocationId);
        Task<bool> DeleteMunicipilityLocationById(string id, int MunicipilityLocationId);

        #endregion

        #region Gender ------

        Task<bool> SaveGender(string id, GenderViewModel GenderViewModel);
        Task<JsonViewModel> GetGenderById(int genderId);
        Task<bool> DeleteGenderById(string id, int genderId);

        #endregion

        #region Blood Group ------
        Task<bool> SaveBloodGroup(string id, BloodGroupViewModel GenderViewModel);
        Task<JsonViewModel> GetBloodGroupById(int bloodGroupId);
        Task<bool> DeleteBloodGroupById(string id, int bloodGroupId);
        #endregion

        #region Unique Identity ------
        Task<bool> SaveUniqueIdentity(string id, UniqueIdentityViewModel GenderViewModel);
        Task<JsonViewModel> GetUniqueIdentityById(int uniqueIdentityId);
        Task<bool> DeleteUniqueIdentityById(string id, int uniqueIdentityId);
        #endregion

        #region TrainingType

        Task<bool> SaveTrainingType(string userId, TrainingTypeViewModel model);
        Task<JsonViewModel> GetTrainingTypeById(int? trainingTypeId);
        Task<bool> DeleteTrainingTypeById(string userId, int trainingTypeId);

        #endregion

        #region Training  Not Done yet

        Task<bool> SaveTraining(string userId, TrainingViewModel model);
        Task<JsonViewModel> GetTrainingById(int? trainingId);
        Task<bool> DeleteTrainingById(string userId, int trainingId);

        #endregion

        #region LevelOfEducation

        Task<bool> SaveLevelOfEducation(string userId, LevelOfEducationViewModel model);
        Task<JsonViewModel> GetLevelOfEducationById(int? LevelOfEducationId);
        Task<bool> DeleteLevelOfEducationById(string userId, int LevelOfEducationId);

        #endregion

        #region Degree

        Task<bool> SaveDegree(string userId, DegreeViewModel model);
        Task<JsonViewModel> GetDegreeById(int? degreeId);
        Task<bool> DeleteDegreeById(string userId, int degreeId);

        #endregion

        #region EducationalSubject

        Task<bool> SaveEducationalSubject(string userId, EducationalSubjectViewModel model);
        Task<JsonViewModel> GetEducationalSubjectById(int? subjectId);
        Task<bool> DeleteEducationalSubjectById(string userId, int subjectId);

        #endregion

        #region Degree

        Task<bool> SaveDegreeSubject(string userId, DegreeSubjectViewModel model);
        Task<JsonViewModel> GetDegreeSubjectById(int? degreeSubjectId);
        Task<bool> DeleteDegreeSubjectById(string userId, int degreeSubjectId);

        #endregion

        #region Salary Location

        Task<bool> SaveSalaryLocation(string id, SalaryLocationViewModel employeeViewModel);
        Task<JsonViewModel> GetSalaryLocation(int salaryLocationId);
        Task<bool> DeleteSalaryLocationById(string id, int? activityTypeId);

        #endregion

        #region Final Settlement
        Task<JsonViewModel> GetPayableList(int? userId);
        Task<JsonViewModel> GetReceivableList(int? userId);
        Task<JsonViewModel> GetEmployeeInfoForFinalSettlement(int employeeId, int userId);
        Task<JsonViewModel> GetMarketOutstanding(int userId, DateTime? fDate, DateTime? tDate, string employeeNo);
        Task<int> SaveEmployeeFinalSettlement(int? UserId, HrmFinalSettlementViewModel model);
        Task<int> SaveEmployeeFinalSettlementDetails(int? UserId, int finalSettlementMasterId, List<HrmFinalSettlementDetailViewModel> finalSettlementDetails);
        Task<int> SaveEmployeeFinalSettlementSignatory(int? UserId, int finalSettlementMasterId, List<HrmFinalSettlementSignatoryViewModel> SignatoryList);
        Task<JsonViewModel> GetEmployeeFinalSettlementbyId(int employeeId,int finalSettlementMasterId);
        Task<JsonViewModel> GetEmployeeFinalSettlementDetailsById(int employeeId, int finalSettlementMasterId);
        Task<JsonViewModel> GetEmployeeFinalSettlementSignatoryById(int employeeId, int finalSettlementMasterId);
        Task<bool> DeleteEmployeeFinalSettlement(int employeeId, int finalSettlementMasterId);
        Task<bool> DeleteSignatoryListById(int employeeId, int signatoryId);
        Task<JsonViewModel> GetfinalSettlementDataForApproval(int employeeId);
        Task<int> SaveEmployeeFinalSettlementApproval(int userId, int approvalStatus, List<HrmSignatoryViewModel> models);
        #endregion

        #region Company Bank
        Task<JsonViewModel> getCompanyBank(int companyBankId);
        Task<JsonViewModel> getSalaryDepot(int salaryDepotId);
        #endregion
    }
}
