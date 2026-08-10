using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ONEERP.Areas.Hrm.Controllers;
using ONEERP.Areas.Hrm.Models;
using ONEERP.Data;
using ONEERP.Data.Entity;
using ONEERP.ERPServices.Hrm.EmployeesInfo.Interfaces;
using ONEERP.Models;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using ONEERP.Areas.Auth.Models;

namespace ONEERP.ERPServices.Hrm.EmployeesInfo
{
    public class HrmMasterService : IHrmMasterService
    {
        private readonly ERPDbContext _context;        

        public HrmMasterService(ERPDbContext context)
        {
            _context = context;            
        }

        #region Employee Status
        public async Task<bool> SaveEmployeeStatus(string id, EmployeeStatusViewModel employeeViewModel)
        {
            try
            {
                var result = await _context.saveUpdateViewModels.FromSql($"CmnSpSetEmployeeStatus {id},{employeeViewModel.employeeStatusId},{employeeViewModel.statusName},{employeeViewModel.statusShortName},{employeeViewModel.isActive}").AsNoTracking().FirstOrDefaultAsync();
                return result.isSuccess;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<JsonViewModel> GetEmployeeStatusById(int employeeStatusId)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"CmnSpGetEmplyeeStatus {employeeStatusId}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<bool> DeleteEmployeeStatusById(string id,int employeeStatusId)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"CmnSpDeleteEmployeeStatus {id},{employeeStatusId}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }
        #endregion

        #region Activity Type
        public async Task<bool> SaveActivityType(string id, ActivityTypeViewModel employeeViewModel)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"CmnSpSetActivityType {id},{employeeViewModel.activityTypeId},{employeeViewModel.activityTypeName},{employeeViewModel.isActive}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }

        public async Task<JsonViewModel> GetActivityTypeById(int activityTypeId)
        {
            try
            {

                var result = await _context.jsonViewModels.FromSql($"CmnSpGetActivityType {activityTypeId}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (System.Exception ex)
            {

                throw ex;
            }
        }

        public async Task<bool> DeleteActivityTypeById(string id, int activityTypeId)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"CmnSpDeleteActivityType {id},{activityTypeId}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }
        #endregion

        #region Department
        public async Task<bool> SaveDepartment(string id, DepartmentViewModel depViewModel)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"CmnSpSetDepartment {id},{depViewModel.departmentId},{depViewModel.deptCode},{depViewModel.deptName},{depViewModel.shortName},{depViewModel.startDate},{depViewModel.isActive}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }

        public async Task<JsonViewModel> GetDepartmentById(int departmentId)
        {
            try
            {

                var result = await _context.jsonViewModels.FromSql($"CmnSpGetDepartment {departmentId}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (System.Exception ex)
            {

                throw ex;
            }
        }

        public async Task<bool> DeleteDepartmentById(string id, int activityTypeId)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"CmnSpDeleteDepartment {id},{activityTypeId}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }
        #endregion

        #region Designation
        public async Task<bool> SaveDesignation(string id, DesignationViewModel depViewModel)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"CmnSpSetDesignation {id},{depViewModel.designationId},{depViewModel.designationCode},{depViewModel.designationName},{depViewModel.shortName},{depViewModel.isActive}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }

        public async Task<JsonViewModel> GetDesignationById(int designationId)
        {
            try
            {

                var result = await _context.jsonViewModels.FromSql($"CmnSpGetDesignation {designationId}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (System.Exception ex)
            {

                throw ex;
            }
        }
        public async Task<JsonViewModel> GetDesignationBySalarySlabId(int? employeeId, int? salarySlabId)
        {
            try
            {

                var result = await _context.jsonViewModels.FromSql($"SalarySpGetDesignationBySalarySlabId {employeeId},{salarySlabId}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (System.Exception ex)
            {

                throw ex;
            }
        }
        public async Task<JsonViewModel> GetEmployeeDesignationByEmployeeId(int? employeeId, int? empId)
        {
            try
            {

                var result = await _context.jsonViewModels.FromSql($"HrmSpGetDesignationByEmployeeId {employeeId},{empId}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (System.Exception ex)
            {

                throw ex;
            }
        }

        public async Task<bool> DeleteDesignationById(string id, int DesignationId)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"CmnSpDeleteDesignation {id},{DesignationId}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }
        #endregion

        #region Employee Type
        public async Task<bool> SaveEmployeeType(string id, EmployeeTypeViewModel depViewModel)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"CmnSpSetEmployeeType {id},{depViewModel.employeeTypeId},{depViewModel.empType},{depViewModel.shortName},{depViewModel.isActive}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }

        public async Task<JsonViewModel> GetEmployeeTypeById(int employeeTypeId)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"CmnSpGetEmployeeType {employeeTypeId}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> DeleteEmployeeTypeById(string id, int employeeTypeId)
        {
            try
            {
                var result = await _context.saveUpdateViewModels.FromSql($"CmnSpDeleteEmployeeType {id},{employeeTypeId}").AsNoTracking().FirstOrDefaultAsync();
                return result.isSuccess;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Employee Relation
        public async Task<bool> SaveEmployeeRelation(string id, EmployeeRelationViewModel depViewModel)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"CmnSpSetEmployeeRelation {id},{depViewModel.relationId},{depViewModel.relationName},{depViewModel.relationShortName},{depViewModel.isActive}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }

        public async Task<JsonViewModel> GetEmployeeRelationById(int relationId)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"CmnSpGetEmployeerRelation {relationId}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> DeleteEmployeeRelationById(string id, int relationId)
        {
            try
            {
                var result = await _context.saveUpdateViewModels.FromSql($"CmnSpDeleteEmployeeRelation {id},{relationId}").AsNoTracking().FirstOrDefaultAsync();
                return result.isSuccess;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Employee Religion------------
        public async Task<bool> SaveEmployeeReligion(string id, EmployeeReligionViewModel depViewModel)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"CmnSpSetEmployeeReligion {id},{depViewModel.religionId},{depViewModel.name},{depViewModel.shortName},{depViewModel.isActive}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }

        public async Task<JsonViewModel> GetEmployeeReligionById(int religionId)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"CmnSpGetEmployeerReligion {religionId}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> DeleteEmployeeReligionById(string id, int religionId)
        {
            try
            {
                var result = await _context.saveUpdateViewModels.FromSql($"CmnSpDeleteEmployeeReligion {id},{religionId}").AsNoTracking().FirstOrDefaultAsync();
                return result.isSuccess;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Address Type

        public async Task<JsonViewModel> GetAddressType()
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"CmnSpGetAddressType").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Division---------
        public async Task<bool> SaveDivision(string id, DivisionViewModel depViewModel)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"CmnSpSetDivision {id},{depViewModel.divisionsId},{depViewModel.divisionCode},{depViewModel.divisionName},{depViewModel.shortName},{depViewModel.countryId},{depViewModel.isActive}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }
        public async Task<JsonViewModel> GetDivisionById(int countryId)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"CmnSpGetDivision {countryId}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        public async Task<bool> DeleteDivisionById(string id, int divisionsId)
        {
            try
            {
                var result = await _context.saveUpdateViewModels.FromSql($"CmnSpDeleteDivision {id},{divisionsId}").AsNoTracking().FirstOrDefaultAsync();
                return result.isSuccess;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region District---------
        public async Task<bool> SaveDistrict(string id, DistrictViewModel depViewModel)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"CmnSpSetDistrict {id},{depViewModel.districtsId},{depViewModel.districtCode},{depViewModel.districtName},{depViewModel.shortName},{depViewModel.divisionsId},{depViewModel.isActive}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }
        public async Task<JsonViewModel> GetDistrictById(int divisionId)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"CmnSpGetDistrict {divisionId}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        public async Task<bool> DeleteDistrictById(string id, int districtsId)
        {
            try
            {
                var result = await _context.saveUpdateViewModels.FromSql($"CmnSpDeleteDistrict {id},{districtsId}").AsNoTracking().FirstOrDefaultAsync();
                return result.isSuccess;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Thanas  -----
        public async Task<bool> SaveThanas(string id, ThanasViewModel thanasViewModel)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"CmnSpSetThanas {id},{thanasViewModel.thanasId},{thanasViewModel.thanaCode},{thanasViewModel.thanaName},{thanasViewModel.shortName},{thanasViewModel.districtsId},{thanasViewModel.isActive}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }
        public async Task<JsonViewModel> GetThanasById(int districtsId)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"CmnSpGetThanas {districtsId}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        public async Task<bool> DeleteThanasById(string id, int thanasId)
        {
            try
            {
                var result = await _context.saveUpdateViewModels.FromSql($"CmnSpDeleteThanas {id},{thanasId}").AsNoTracking().FirstOrDefaultAsync();
                return result.isSuccess;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Municipility Location ----
        public async Task<bool> SaveMunicipilityLocation(string id, MunicipilityLocationViewModel municipilityViewModel)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"CmnSpSetMunicipilityLocation {id},{municipilityViewModel.MunicipilityLocationId},{municipilityViewModel.locationName},{municipilityViewModel.shortName},{municipilityViewModel.isActive}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }

        public async Task<JsonViewModel> GetMunicipilityLocationById(int MunicipilityLocationId)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"CmnSpGetMunicipilityLocation {MunicipilityLocationId}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> DeleteMunicipilityLocationById(string id, int MunicipilityLocationId)
        {
            try
            {
                var result = await _context.saveUpdateViewModels.FromSql($"CmnSpDeleteMunicipilityLocation {id},{MunicipilityLocationId}").AsNoTracking().FirstOrDefaultAsync();
                return result.isSuccess;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Gender ----
        public async Task<bool> SaveGender(string id, GenderViewModel genderViewModel)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"CmnSpSetGender {id},{genderViewModel.genderId},{genderViewModel.Name},{genderViewModel.shortName},{genderViewModel.isActive}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }

        public async Task<JsonViewModel> GetGenderById(int genderId)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"CmnSpGetGender {genderId}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> DeleteGenderById(string id, int genderId)
        {
            try
            {
                var result = await _context.saveUpdateViewModels.FromSql($"CmnSpDeleteGender {id},{genderId}").AsNoTracking().FirstOrDefaultAsync();
                return result.isSuccess;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Blood Group ----
        public async Task<bool> SaveBloodGroup(string id, BloodGroupViewModel bloodGroupViewModel)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"CmnSpSetBloodGroup {id},{bloodGroupViewModel.bloodGroupId},{bloodGroupViewModel.Name},{bloodGroupViewModel.shortName},{bloodGroupViewModel.isActive}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }

        public async Task<JsonViewModel> GetBloodGroupById(int bloodGroupId)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"CmnSpGetBloodGroup {bloodGroupId}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> DeleteBloodGroupById(string id, int bloodGroupId)
        {
            try
            {
                var result = await _context.saveUpdateViewModels.FromSql($"CmnSpDeleteBloodGroup {id},{bloodGroupId}").AsNoTracking().FirstOrDefaultAsync();
                return result.isSuccess;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Unique Identity -----
        public async Task<bool> SaveUniqueIdentity(string id, UniqueIdentityViewModel uniqueIdentityIdViewModel)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"CmnSpSetUniqueIdentity {id},{uniqueIdentityIdViewModel.uniqueIdentityId},{uniqueIdentityIdViewModel.Name},{uniqueIdentityIdViewModel.shortName},{uniqueIdentityIdViewModel.isActive}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }

        public async Task<JsonViewModel> GetUniqueIdentityById(int uniqueIdentityId)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"CmnSpGetUniqueIdentity {uniqueIdentityId}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> DeleteUniqueIdentityById(string id, int bloodGroupId)
        {
            try
            {
                var result = await _context.saveUpdateViewModels.FromSql($"CmnSpDeleteUniqueIdentity {id},{bloodGroupId}").AsNoTracking().FirstOrDefaultAsync();
                return result.isSuccess;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Training Type

        public async Task<bool> SaveTrainingType(string userId, TrainingTypeViewModel model)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"HrmSpSetTrainingType {userId},{model.trainingTypeId},{model.name},{model.shortName},{model.nameBn},{model.isActive}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }
        public async Task<JsonViewModel> GetTrainingTypeById(int? trainingTypeId)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"HrmSpGetTrainingType {trainingTypeId}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        public async Task<bool> DeleteTrainingTypeById(string userId, int trainingTypeId)
        {
            try
            {
                var result = await _context.saveUpdateViewModels.FromSql($"HrmSpDeleteTrainingType {userId},{trainingTypeId}").AsNoTracking().FirstOrDefaultAsync();
                return result.isSuccess;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion Training Type

        #region Training

        public async Task<bool> SaveTraining(string userId, TrainingViewModel model)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"HrmSpSetTraining {userId},{model.isActive},{model.isActive},{model.isActive},{model.isActive}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }
        public async Task<JsonViewModel> GetTrainingById(int? trainingId)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"HrmSpGetTraining {trainingId}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        public async Task<bool> DeleteTrainingById(string userId, int trainingId)
        {
            try
            {
                var result = await _context.saveUpdateViewModels.FromSql($"HrmSpDeleteTraining {userId},{trainingId}").AsNoTracking().FirstOrDefaultAsync();
                return result.isSuccess;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region LevelOfEducation

        public async Task<bool> SaveLevelOfEducation(string userId, LevelOfEducationViewModel model)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"HrmSpSetLevelOfEducation {userId},{model.levelOfEducationId},{model.levelOfEducationName},{model.isActive}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }
        public async Task<JsonViewModel> GetLevelOfEducationById(int? LevelOfEducationId)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"HrmSpGetLevelOfEducation {LevelOfEducationId}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        public async Task<bool> DeleteLevelOfEducationById(string userId, int LevelOfEducationId)
        {
            try
            {
                var result = await _context.saveUpdateViewModels.FromSql($"HrmSpDeleteLevelOfEducation {userId},{LevelOfEducationId}").AsNoTracking().FirstOrDefaultAsync();
                return result.isSuccess;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Degree

        public async Task<bool> SaveDegree(string userId, DegreeViewModel model)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"HrmSpSetDegree {userId},{model.degreeId},{model.name},{model.shortName},{model.levelOfEducationId},{model.isActive}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }
        public async Task<JsonViewModel> GetDegreeById(int? degreeId)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"HrmSpGetDegree {degreeId}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        public async Task<bool> DeleteDegreeById(string userId, int degreeId)
        {
            try
            {
                var result = await _context.saveUpdateViewModels.FromSql($"HrmSpDeleteDegree {userId},{degreeId}").AsNoTracking().FirstOrDefaultAsync();
                return result.isSuccess;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region EducationalSubject

        public async Task<bool> SaveEducationalSubject(string userId, EducationalSubjectViewModel model)
        {
            try
            {
                var result = await _context.saveUpdateViewModels.FromSql($"HrmSpSetEducationalSubject {userId},{model.subjectId},{model.name},{model.isActive}").AsNoTracking().FirstOrDefaultAsync();
                return result.isSuccess;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<JsonViewModel> GetEducationalSubjectById(int? subjectId)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"HrmSpGetEducationalSubject {subjectId}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        public async Task<bool> DeleteEducationalSubjectById(string userId, int subjectId)
        {
            try
            {
                var result = await _context.saveUpdateViewModels.FromSql($"HrmSpDeleteEducationalSubject {userId},{subjectId}").AsNoTracking().FirstOrDefaultAsync();
                return result.isSuccess;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region DegreeSubject

        public async Task<bool> SaveDegreeSubject(string userId, DegreeSubjectViewModel model)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"HrmSpSetDegreeSubject {userId},{model.degreeSubjectId},{model.degreeId},{model.subjectId},{model.isActive}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }
        public async Task<JsonViewModel> GetDegreeSubjectById(int? degreeSubjectId)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"HrmSpGetDegreeSubject {degreeSubjectId}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        public async Task<bool> DeleteDegreeSubjectById(string userId, int degreeSubjectId)
        {
            try
            {
                var result = await _context.saveUpdateViewModels.FromSql($"HrmSpDeleteDegreeSubject {userId},{degreeSubjectId}").AsNoTracking().FirstOrDefaultAsync();
                return result.isSuccess;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Salary Location

        public async Task<bool> SaveSalaryLocation(string id, SalaryLocationViewModel salaryLocationView)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"CmnSpSetSalaryLocation {id},{salaryLocationView.salaryLocationId},{salaryLocationView.Name},{salaryLocationView.shortName},{salaryLocationView.shortOrder},{salaryLocationView.isActive}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }

        public async Task<JsonViewModel> GetSalaryLocation(int salaryLocationId)
        {
            try
            {

                var result = await _context.jsonViewModels.FromSql($"CmnSpGetSalaryLocation {salaryLocationId}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (System.Exception ex)
            {

                throw ex;
            }
        }

        public async Task<bool> DeleteSalaryLocationById(string id, int? salaryLocationId)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"CmnSpDeleteSalaryLocation {id},{salaryLocationId}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }

        #endregion

        #region Final Settlement
        public async Task<JsonViewModel> GetPayableList(int? userId)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"HrmSpGetFinalSettlementPayableList {userId}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (System.Exception ex)
            {

                throw ex;
            }
        }
        public async Task<JsonViewModel> GetReceivableList(int? userId)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"HrmSpGetFinalSettlementReceivableList {userId}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (System.Exception ex)
            {

                throw ex;
            }
        }
        public async Task<JsonViewModel> GetEmployeeInfoForFinalSettlement(int employeeId, int userId)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"HrmSpGetEmployeeNameForFinalSettlement {employeeId},{userId}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<JsonViewModel> GetMarketOutstanding(int userId, DateTime? fDate, DateTime? tDate, string employeeNo)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"HrmSpGetMarketOutstanding {userId},{fDate},{tDate},{employeeNo}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<int> SaveEmployeeFinalSettlement(int? UserId, HrmFinalSettlementViewModel model)
        {
            try
            {
                var result = await _context.saveUpdateValueViewModels.FromSql($"HrmSpSetFinalSettlementMaster {UserId},{model.finalSettlementMasterId},{model.employeeId},{model.grossSalary},{model.basicSalary},{model.lastMonthSalary},{model.lMSalaryStatus},{model.mcInstallmentNo},{model.mcInstallmentAmmount},{model.employmentType},{model.pFEligibility},{model.resignationDate},{model.pFContributionDuration},{model.noticeProvided},{model.pFAmount},{model.LWD},{model.aLBalance},{model.noticeShortfall},{model.lengthOfService},{model.resignationEffectiveDate},{model.serviceBenefitDuration}").AsNoTracking().FirstOrDefaultAsync();
                return result.isSuccess;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<int> SaveEmployeeFinalSettlementDetails(int? UserId, int finalSettlementMasterId, List<HrmFinalSettlementDetailViewModel> finalSettlementDetails)
        {
            var result = 0;
            try
            {
                foreach (var data in finalSettlementDetails)
                {
                    var res = await _context.saveUpdateValueViewModels.FromSql($"HrmSpSetFinalSettlementDetail {UserId},{data.finalSettlementDetailsId},{finalSettlementMasterId},{data.finalSettlementHeadId},{data.monthOrParticulars},{data.days},{data.amount}").AsNoTracking().FirstOrDefaultAsync();
                    result = res.isSuccess;
                }
            }
            catch (Exception ex)
            {
                result = 0;
            }
            return result;
        }
        public async Task<int> SaveEmployeeFinalSettlementSignatory(int? UserId, int finalSettlementMasterId, List<HrmFinalSettlementSignatoryViewModel> SignatoryList)
        {
            var result = 0;
            try
            {
                foreach (var data in SignatoryList)
                {
                    if (data.sortOrder == 1)
                    {
                        data.status = 1;
                    }
                    var res = await _context.saveUpdateValueViewModels.FromSql($"HrmSpSetFinalSettlementSignatory {UserId},{data.signatoryId},{finalSettlementMasterId},{data.finalSettlementHeadId},{data.signatoryType},{data.sortOrder},{data.employeeId},{data.status},{data.remarks},{data.isApprove}").AsNoTracking().FirstOrDefaultAsync();
                    result = res.isSuccess;
                }
            }
            catch (Exception ex)
            {
                result = 0;
            }
            return result;
        }
        public async Task<JsonViewModel> GetEmployeeFinalSettlementbyId(int employeeId, int finalSettlementMasterId)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"HrmSpGetEmployeeFinalSettlementbyId {employeeId},{finalSettlementMasterId}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<JsonViewModel> GetEmployeeFinalSettlementDetailsById(int employeeId, int finalSettlementMasterId)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"HrmSpGetEmployeeFinalSettlementDetailsById {employeeId},{finalSettlementMasterId}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<JsonViewModel> GetEmployeeFinalSettlementSignatoryById(int employeeId, int finalSettlementMasterId)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"HrmSpGetEmployeeFinalSettlementSignatoryById {employeeId},{finalSettlementMasterId}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<bool> DeleteEmployeeFinalSettlement(int employeeId, int finalSettlementMasterId)
        {
            try
            {
                var result = await _context.saveUpdateViewModels.FromSql($"HrmSpdeleteEmployeeFinalSettlement {employeeId},{finalSettlementMasterId}").AsNoTracking().FirstOrDefaultAsync();
                return result.isSuccess;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<bool> DeleteSignatoryListById(int employeeId, int signatoryId)
        {
            try
            {
                var result = await _context.saveUpdateViewModels.FromSql($"HrmSpDeleteSignatoryListById {employeeId},{signatoryId}").AsNoTracking().FirstOrDefaultAsync();
                return result.isSuccess;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<JsonViewModel> GetfinalSettlementDataForApproval(int employeeId)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"HrmSpGetEmployeefinalSettlementDataForApproval {employeeId}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<int> SaveEmployeeFinalSettlementApproval(int userId, int approvalStatus, List<HrmSignatoryViewModel> models)
        {

            var result = 0;
            try
            {
                foreach (var data in models)
                {
                    var res = await _context.saveUpdateValueViewModels.FromSql($"HrmSpSetEmployeeFinalSettlementApproval {userId},{data.signatoryId},{data.finalSettlementMasterId},{approvalStatus},{data.remarks},{data.isApprove},{data.isSelect}").AsNoTracking().FirstOrDefaultAsync();
                    result = res.isSuccess;
                }
            }
            catch (Exception ex)
            {
                result = 0;
            }
            return result;
        }
        #endregion

        #region Company Bank

        public async Task<JsonViewModel> getCompanyBank(int companyBankId)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"CmnSpGetCompanyBank {companyBankId}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public async Task<JsonViewModel> getSalaryDepot(int salaryDepotId)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"CmnSpGetSalaryDepot {salaryDepotId}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        public async Task<int> SetSlabDesignation(string userId, IEnumerable<SalarySlabDesignationViewModel> models)
        {
            var result = new SaveUpdateValueViewModel();
            try
            {
                foreach (var model in models)
                {
                    result = await _context.saveUpdateValueViewModels.FromSql($"SalarySpSetSlabDesignation {userId},{model.slabDesignationId},{model.salarySlabId},{model.designationId}").AsNoTracking().FirstOrDefaultAsync();
                }
                return result.isSuccess;
            }
            catch (System.Exception ex)
            {
                return 0;
            }
        }
        public async Task<JsonViewModel> GetSlabDesignationById(int? userId, int? slabDesignationId)
        {
            try
            {

                var result = await _context.jsonViewModels.FromSql($"HrmSpGetSlabDesignationById {userId},{slabDesignationId}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (System.Exception ex)
            {

                throw ex;
            }
        }
        public async Task<bool> DeleteSlabDesignation(string id, int slabDesignation)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"HrmSpDeleteSlabDesignation {id},{slabDesignation}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }
    }
}
