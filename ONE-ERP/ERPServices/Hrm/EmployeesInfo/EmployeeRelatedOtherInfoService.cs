using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ONEERP.Areas.Auth.Models;
using ONEERP.Areas.Hrm.Models;
using ONEERP.Data;
using ONEERP.Data.Entity;
using ONEERP.ERPServices.Hrm.EmployeesInfo.Interfaces;
using ONEERP.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace ONEERP.ERPServices.Hrm.EmployeesInfo
{
    public class EmployeeRelatedOtherInfoService : IEmployeeRelatedOtherInfoService
    {
        private readonly ERPDbContext _context;       

        public EmployeeRelatedOtherInfoService(ERPDbContext context)
        {
            _context = context;           
        }

        #region Employee Address    

        public async Task<bool> SaveEmployeeAddress(string userId, EmployeeAddressViewModel model)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"HrmSpSetEmployeeAddress {userId},{model.employeeAddressId},{model.employeeId},{model.addressTypeId},{model.countryId},{model.divisionId},{model.districtId},{model.thanaId},{model.address},{model.isActive}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }
        public async Task<JsonViewModel> GetEmployeeAddressById(int employeeAddressId, int employeeId)
        {
            var result = await _context.jsonViewModels.FromSql($"HrmSpGetEmployeeAddress {employeeAddressId},{employeeId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> GetDuplicateEmployeeAddress(int employeeAddressId, int employeeId, int addressTypeId)
        {
            var result = await _context.jsonViewModels.FromSql($"HrmSpGetDuplicateEmployeeAddress {employeeAddressId},{employeeId},{addressTypeId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<bool> DeleteEmployeeAddressById(string userId, int employeeAddressId)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"HrmSpDeleteEmployeeAddress {userId},{employeeAddressId}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }

        #endregion

        #region Employee Job Description    

        public async Task<bool> SaveEmployeeJobDescription(string userId, List<EmployeeJobDescriptionViewModel> jobDescriptionList )
        {
            var result = new SaveUpdateViewModel();
            foreach (var item in jobDescriptionList)
            {
                 result = await _context.saveUpdateViewModels.FromSql($"HrmSpSetEmployeeJobDescription {userId},{item.employeeJobDescriptionId},{item.employeeId},{item.slNo},{item.jobDescription},{item.isActive}").AsNoTracking().FirstOrDefaultAsync();
            }
            return result.isSuccess;
        }
        public async Task<JsonViewModel> GetEmployeeJobDescriptionById(int employeeJobDescriptionId, int employeeId)
        {
            var result = await _context.jsonViewModels.FromSql($"HrmSpGetEmployeeJobDescription {employeeJobDescriptionId},{employeeId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<bool> DeleteEmployeeJobDescriptionById(string userId, int employeeJobDescriptionId)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"HrmSpDeleteEmployeeJobDescription {userId},{employeeJobDescriptionId}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }

        #endregion

        #region Employee Related Other Info
        public async Task<JsonViewModel> GetAllLevelOfEducation()
        {
            var result = await _context.jsonViewModels.FromSql($"HrmSpGetLevelOfEducation").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<JsonViewModel> GetDegreeByLevelOfEducationId(int levelOfEducationId)
        {
            var result = await _context.jsonViewModels.FromSql($"HrmSpGetEmployeeDegree {levelOfEducationId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<JsonViewModel> GetMejorById(int degreeId)
        {
            var result = await _context.jsonViewModels.FromSql($"HrmSpGetEmployeeDegreeSubject {degreeId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<JsonViewModel> GetResultTypes()
        {
            var result = await _context.jsonViewModels.FromSql($"HrmSpGetResultTypes").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<bool> SaveEmployeeEducation(string userId, EmployeeEducationViewModel model)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"HrmSpSetEmployeeEducation {userId},{model.educationalQualificationId},{model.employeeId},{model.institution},{model.resultId},{model.majorGroup},{model.grade},{model.passingYear},{model.degreeId},{model.degreesubjectId},{model.educationOrganizationId},{model.certificateUrl},{model.isActive}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }

        public async Task<JsonViewModel> GetEmployeeEducationById(int educationalQualificationId, int employeeId)
        {
            var result = await _context.jsonViewModels.FromSql($"HrmSpGetEmployeeEducation {educationalQualificationId},{employeeId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<bool> DeleteEmployeeEducationById(string userId, int educationalQualificationId)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"HrmSpDeleteEmployeeEducation {userId},{educationalQualificationId}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }

        public async Task<JsonViewModel> GetEmployeeAllRelations()
        {
            var result = await _context.jsonViewModels.FromSql($"CmnSpGetEmployeerRelation").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<JsonViewModel> GetEmployeeFamilyInfoById(int employeeFamilyInfoId, int employeeId)
        {
            var result = await _context.jsonViewModels.FromSql($"HrmSpGetEmployeeFamilyInfo {employeeFamilyInfoId},{employeeId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<bool> SaveEmployeeFamillyInfo(string userId, EmployeeFamilyInfoViewModel model)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"HrmSpSetEmployeeFamilyInfo {userId},{model.familyInfoId},{model.relationId},{model.employeeId},{model.name},{model.dob},{model.occupation},{model.mobile},{model.NID},{model.passport},{model.email},{model.remarks}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }

        public async Task<bool> DeleteEmployeeFamilyInfoById(string userId, int familyInfoId)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"HrmSpDeleteEmployeeFamilyInfo {userId},{familyInfoId}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }

        public async Task<JsonViewModel> GetEmployeeEmergencyContactById(int familyInfoId, int employeeId)
        {
            var result = await _context.jsonViewModels.FromSql($"HrmSpGetEmployeeEmergencyContact {familyInfoId},{employeeId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<JsonViewModel> GetEmployeeReferenceById(int familyInfoId, int employeeId)
        {
            var result = await _context.jsonViewModels.FromSql($"HrmSpGetEmployeeReferenceInfo {familyInfoId},{employeeId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }


        public async Task<bool> SetHrmEmployeeAttachment(string user, EmployeeAttachmentUploadViewModel model)
        {
            string[] res = model.tempImageUrl?.Split(',');
            if (res?.Length > 1)
            {
                Byte[] bytes = Convert.FromBase64String(res[1]);
                string servePath = ("./wwwroot/HrmEmployeeImages");
                if (!System.IO.Directory.Exists(servePath)) System.IO.Directory.CreateDirectory(servePath);
                string fileName = ($"{DateTime.Now.Ticks}.{model.extension}");
                string filePath = ($"{servePath}/{fileName}");
                File.WriteAllBytes(filePath, bytes);

                model.tempImageUrl = filePath.Remove(0, 9);
            }

            var result = await _context.saveUpdateViewModels.FromSql($"HrmSpSetEmployeeAttachment {user},{model.employeeAttachmentId},{model.employeeId},{model.tempImageUrl}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }
        public async Task<JsonViewModel> GetEmployeeExperienceById(int employeeExperienceId, int employeeId)
        {
            var result = await _context.jsonViewModels.FromSql($"[dbo].[HrmSpGetEmployeeExperience] {employeeExperienceId},{employeeId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<bool> SaveEmployeeExperience(string userId, EmployeeExperienceViewModel model)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"[HrmSpSetEmployeeExperience] {userId},{model.employeeExperienceId},{model.employeeId},{model.organization},{model.appointedDesignation},{model.designation},{model.department},{model.startDate},{model.endDate},{model.remarks}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }

        public async Task<bool> DeleteEmployeeExperienceById(string userId, int empExperienceId)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"[dbo].[HrmSpDeleteEmployeeExperience] {userId},{empExperienceId}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }
        #endregion
    }
}
