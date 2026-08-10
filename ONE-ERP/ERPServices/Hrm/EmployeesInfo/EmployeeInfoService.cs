using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ONEERP.Areas.Auth.Models;
using ONEERP.Areas.Hrm.Models;
using ONEERP.Data;
using ONEERP.Data.Entity;
using ONEERP.Data.Entity.FieldForceTracking;
using ONEERP.ERPServices.Hrm.EmployeesInfo.Interfaces;
using ONEERP.Helpers;
using ONEERP.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Threading.Tasks;

namespace ONEERP.ERPServices.Hrm.EmployeesInfo
{
    public class EmployeeInfoService : IEmployeeInfoService
    {
        private readonly ERPDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public EmployeeInfoService(ERPDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        #region Employee For User Create & GET DON'T CHANGE THIS service

        //DONT CHANGE THIS service

        public async Task<bool> SaveEmployeeForCreateUser(string id, EmployeeViewModel employeeViewModel)
        {
            try
            {

                var result = await _context.saveUpdateViewModels.FromSql($"CmnSpSetEmployeeForCreateUser {id},{employeeViewModel.employeeId},{employeeViewModel.companyId},{employeeViewModel.fullName},{employeeViewModel.emailId},{employeeViewModel.isActive}").AsNoTracking().FirstOrDefaultAsync();
                return result.isSuccess;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<JsonViewModel> GetEmployeeById(int companyId, int employeeId)
        {
            var result = await _context.jsonViewModels.FromSql($"CmnSpGetEmployee {companyId},{employeeId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        #endregion

        #region Employee Info

        public async Task<JsonViewModel> GetMaxEmployeeNo(int companyId)
        {
            var result = await _context.jsonViewModels.FromSql($"HrmSpGetMaxEmployeeNo {companyId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<bool> SaveEmployeeBasicInfo(string id, EmployeeInformationViewModel employeeViewModel)
        {
            try
            {
                var result = await _context.saveUpdateViewModels.FromSql($"CmnSpSetEmployee {id},{employeeViewModel.employeeId},{employeeViewModel.employeeNo},{employeeViewModel.employeeTypeId},{employeeViewModel.firstName},{employeeViewModel.middleName},{employeeViewModel.lastName},{employeeViewModel.fullName},{employeeViewModel.emailId},{employeeViewModel.skypeId},{employeeViewModel.facebookId},{employeeViewModel.whatsApp},{employeeViewModel.viber},{employeeViewModel.linkedIN},{employeeViewModel.fathersName},{employeeViewModel.mothersName},{employeeViewModel.employeeStatusId},{employeeViewModel.bloodGroupId},{employeeViewModel.religionId},{employeeViewModel.mobileNo},{employeeViewModel.phoneNo},{employeeViewModel.uniqueIdentityId},{employeeViewModel.height},{employeeViewModel.DOB},{employeeViewModel.passportNO},{employeeViewModel.NID},{employeeViewModel.officeId},{employeeViewModel.genderId},{employeeViewModel.effectiveDate},{employeeViewModel.companyId},{employeeViewModel.joiningDate},{employeeViewModel.maritalStatus},{employeeViewModel.drivingLicense},{employeeViewModel.tinNo},{employeeViewModel.sbuId},{employeeViewModel.currentDesignation},{employeeViewModel.currentDepartment},{employeeViewModel.nationality},{employeeViewModel.isSalaryActive},{employeeViewModel.haveVehicle},{employeeViewModel.zoneId},{employeeViewModel.depoId},{employeeViewModel.regionId},{employeeViewModel.areaId},{employeeViewModel.territoryId},{employeeViewModel.postingLocation},{employeeViewModel.salaryLocation},{employeeViewModel.isTopManagement},{employeeViewModel.heldUpDate},{employeeViewModel.binNo},{employeeViewModel.companyBankId},{employeeViewModel.salaryDepotId},{employeeViewModel.probationPeriodId},{employeeViewModel.confirmationDate},{employeeViewModel.separationTypeId},{employeeViewModel.separationEffectiveDate},{employeeViewModel.salaryGradeId},{employeeViewModel.salarySlabId},{employeeViewModel.deviceNo}").AsNoTracking().FirstOrDefaultAsync();
                return result.isSuccess;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<dynamic> SaveDemoEmployeeBasicInfo(string id, EmployeeInformationViewModel employeeViewModel)
        {
            try
            {
                var user = new ApplicationUser { UserName = employeeViewModel.emailId, isActive = 1, Email = employeeViewModel.emailId, employeeId = 0, PassExpiredAt = DateTime.Now };
                var userInfo = await _userManager.FindByNameAsync(user.UserName);

                if (userInfo != null)
                {
                    _ = await _userManager.RemovePasswordAsync(userInfo);
                    var resetResult = await _userManager.AddPasswordAsync(userInfo, "OneErp@123");
                    if (resetResult.Succeeded)
                    {
                        var robj = new
                        {
                            UserName = employeeViewModel.emailId,
                            Password = "OneErp@123",
                            DemoLink = "http://103.106.236.93:9205/#/auth/login"
                        };
                        //string jsonString = JsonConvert.SerializeObject(obj);
                        return robj;
                    }
                }
                else
                {
                    var result = await _context.saveUpdateValueViewModels.FromSql($"CmnSpSetDemoEmployee {id},{employeeViewModel.employeeId},{employeeViewModel.employeeNo},{employeeViewModel.employeeTypeId},{employeeViewModel.firstName},{employeeViewModel.middleName},{employeeViewModel.lastName},{employeeViewModel.fullName},{employeeViewModel.emailId},{employeeViewModel.skypeId},{employeeViewModel.facebookId},{employeeViewModel.whatsApp},{employeeViewModel.viber},{employeeViewModel.linkedIN},{employeeViewModel.fathersName},{employeeViewModel.mothersName},{employeeViewModel.employeeStatusId},{employeeViewModel.bloodGroupId},{employeeViewModel.religionId},{employeeViewModel.mobileNo},{employeeViewModel.phoneNo},{employeeViewModel.uniqueIdentityId},{employeeViewModel.height},{employeeViewModel.DOB},{employeeViewModel.passportNO},{employeeViewModel.NID},{employeeViewModel.officeId},{employeeViewModel.genderId},{employeeViewModel.effectiveDate},{employeeViewModel.companyId},{employeeViewModel.joiningDate},{employeeViewModel.maritalStatus},{employeeViewModel.drivingLicense},{employeeViewModel.tinNo},{employeeViewModel.sbuId},{employeeViewModel.currentDesignation},{employeeViewModel.currentDepartment},{employeeViewModel.nationality},{employeeViewModel.isSalaryActive},{employeeViewModel.haveVehicle},{employeeViewModel.zoneId},{employeeViewModel.depoId},{employeeViewModel.regionId},{employeeViewModel.areaId},{employeeViewModel.territoryId},{employeeViewModel.postingLocation},{employeeViewModel.salaryLocation},{employeeViewModel.isTopManagement},{employeeViewModel.heldUpDate},{employeeViewModel.binNo},{employeeViewModel.companyBankId},{employeeViewModel.salaryDepotId},{employeeViewModel.probationPeriodId},{employeeViewModel.confirmationDate},{employeeViewModel.separationTypeId},{employeeViewModel.separationEffectiveDate},{employeeViewModel.salaryGradeId},{employeeViewModel.salarySlabId}").AsNoTracking().FirstOrDefaultAsync();

                    user.employeeId = result.isSuccess;

                    // onek kisu korete hobe
                    if (result.isSuccess > 0)
                    {
                        _ = await _context.saveUpdateViewModels.FromSql($"CmnSpSetUserWiseCompany {id},{0},{result.isSuccess},{1},{0},{1}").AsNoTracking().FirstOrDefaultAsync();
                        _ = await _userManager.CreateAsync(user, "OneErp@123");
                        _ = await _context.saveUpdateValueViewModels.FromSql($"CmnSpSetUserPermissionGroupV2 {id},{0},{1},{result.isSuccess},{1},{1}").AsNoTracking().FirstOrDefaultAsync();

                    }

                    var obj = new
                    {
                        UserName = employeeViewModel.emailId,
                        Password = "OneErp@123",
                        DemoLink = "http://103.106.236.93:9205/#/auth/login"
                    };
                    //string jsonString = JsonConvert.SerializeObject(obj);
                    return obj;
                }
                return new
                {
                    UserName = employeeViewModel.emailId,
                    Password = "",
                    DemoLink = "",
                    Message = "User Create Failed"
                };

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public async Task<bool> SaveEmployeeOtherExpense(string id, EmployeeOtherExpenseViewModel employeeViewModel)
        {
            try
            {
                var result = await _context.saveUpdateViewModels.FromSql($"CmnSpSetEmployeeOtherExpense {id},{employeeViewModel.otherExpenseId},{employeeViewModel.employeeId},{employeeViewModel.fiscalYearId},{employeeViewModel.monthName},{employeeViewModel.amount},{employeeViewModel.remarks}").AsNoTracking().FirstOrDefaultAsync();
                return result.isSuccess;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<bool> UpdateSalesLimit(string id, string territoryCode)
        {
            try
            {
                var result = await _context.saveUpdateViewModels.FromSql($"SpSetUpdateSalesLimit {id},{territoryCode}").AsNoTracking().FirstOrDefaultAsync();
                return result.isSuccess;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> UpdatePostingLocation(string id, UpdatePostingViewModel model)
        {
            try
            {
                var result = await _context.saveUpdateViewModels.FromSql($"SpSetUpdatePostingLocation {id},{model.EmployeeId},{model.ZoneId},{model.DepoId},{model.RegionId},{model.AreaId},{model.TerritoryId},{model.PostingLocation}").AsNoTracking().FirstOrDefaultAsync();
                return result.isSuccess;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> UpdateEmployeeFirebaseToken(string id, EmployeeFireBaseViewModel employeeViewModel)
        {
            try
            {
                var result = await _context.saveUpdateViewModels.FromSql($"CmnSpUpdateEmployeeFirebaseToken {id},{Convert.ToInt32(id)},{employeeViewModel.firebaseToken}").AsNoTracking().FirstOrDefaultAsync();
                return result.isSuccess;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> SaveEmployeeMessageInfo(string id, CmnMessageInfo model)
        {
            try
            {
                var result = await _context.saveUpdateViewModels.FromSql($"CmnSpSetEmployeeMessageInfo {id},{model.messageInfoID},{Convert.ToInt32(id)},{model.toEmployeeId},{model.msgTitle},{model.message},{model.isRead},{model.date}").AsNoTracking().FirstOrDefaultAsync();
                return result.isSuccess;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public async Task<JsonViewModel> GetMessageInfoById(int employeeId)
        {
            var result = await _context.jsonViewModels.FromSql($"CmnSpGetMessageInfoById {employeeId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<JsonViewModel> GetEmployeeBasicInfoById(int employeeId)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"CmnSpGetEmployeeInfo {employeeId}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {

                throw;
            }

        }
        public async Task<JsonViewModel> GetEmployeeBasicInfoByCompanyId(int? userId, int employeeId)
        {
            var result = await _context.jsonViewModels.FromSql($"CmnSpGetEmployeeBasicInfoByCompanyId {userId},{employeeId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> GetEmployeeBasicInfoByIdNew(int employeeId)
        {
            var result = await _context.jsonViewModels.FromSql($"CmnSpGetEmployeeInfoNew {employeeId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> GetEmployeeBasicInfoByIdOptimized(int employeeId)
        {
            try
            {
                var result = await _context.employeeInfoViewModelcs
                .FromSql($"EXEC CmnSpGetEmployeeInfoNewUpdate {employeeId}")
                .AsNoTracking()
                .ToListAsync();

                var jsonResult = JsonConvert.SerializeObject(result, Formatting.Indented);

                return new JsonViewModel { data = jsonResult };
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        public async Task<JsonViewModel> GetEmployeeBasicInfoByIdForESS(int employeeId)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"CmnSpGetEmployeeInfoByIDForESS {employeeId}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {

                throw;
            }

        }
        public async Task<JsonViewModel> GetLeaveSummaryForESSJson(int employeeId, int year)
        {
            var result = await _context.jsonViewModels.FromSql($"HrmLeaveSummaryForESSJson {employeeId},{year}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> GetCelebtationForESSJson(int employeeId)
        {
            var result = await _context.jsonViewModels.FromSql($"HrmCelebtationForESSJson {employeeId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<JsonViewModel> GetDispatcher(int? userId)
        {
            var result = await _context.jsonViewModels.FromSql($"SalSpGetDispatcher {userId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<bool> DeleteEmployeeById(string id, int employeeId)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"CmnSpDeleteEmployee {id},{employeeId}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }

        public async Task<bool> DeleteEmployeeOtherExpense(string id, int otherExpenseId)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"CmnSpDeleteEmployeeOtherExpense {id},{otherExpenseId}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }

        public async Task<JsonViewModel> GetDuplicateEmployeeNo(int employeeId, string employeeNo)
        {
            var result = await _context.jsonViewModels.FromSql($"HrmSpGetDuplicateEmployeeNo {employeeId},{employeeNo}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<JsonViewModel> getDuplicateTerritoty(int employeeId, string PostingLocation, string Code)
        {
            var result = await _context.jsonViewModels.FromSql($"HrmSpGetDuplicateEmployeeInTerritoty {employeeId},{PostingLocation},{Code}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }


        public async Task<JsonViewModel> GetGetEmployeeInfoLoadById(int employeeId, int userId)
        {
            var result = await _context.jsonViewModels.FromSql($"CmnSpGetEmployeeInfoLoadById {employeeId},{userId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> GetEmployeeInfoWhoHasLeaveById(int employeeId, int userId)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"CmnSpGetEmployeeInfoWhoHasLeaveById {employeeId},{userId}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {
                return new JsonViewModel { data = "ss" };
            }
        }
        public async Task<JsonViewModel> GetEmployeeInfoLoadByIdOptimized(int employeeId, int userId)
        {
            try
            {
                var result = await _context.employeeListViewModelDropdown
                    .FromSql($"EXEC CmnSpGetEmployeeInfoDropdownLoadById {employeeId},{userId}")
                    .AsNoTracking()
                    .ToListAsync();

                var jsonResult = JsonConvert.SerializeObject(result, Formatting.Indented);

                return new JsonViewModel { data = jsonResult };
            }
            catch (Exception ex)
            {
                return new JsonViewModel { data = "ss" };
            }
        }
        public async Task<JsonViewModel> GetEmployeeInfoLoadByIdOptimizedForPaySlip(int employeeId, int userId)
        {
            try
            {
                var result = await _context.employeeListViewModelDropdown
                    .FromSql($"EXEC CmnSpGetEmployeeInfoDropdownLoadByIdForPaySlip {employeeId},{userId}")
                    .AsNoTracking()
                    .ToListAsync();

                var jsonResult = JsonConvert.SerializeObject(result, Formatting.Indented);

                return new JsonViewModel { data = jsonResult };
            }
            catch (Exception ex)
            {
                return new JsonViewModel { data = "ss" };
            }
        }

        public async Task<JsonViewModel> GetGetEmployeeInfoByPosting(int employeeId, int userId)
        {
            var result = await _context.jsonViewModels.FromSql($"CmnSpGetEmployeeInfoWithPostingLocation {employeeId},{userId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> GetEmployeeOtherExpense(int employeeId, int otherExpenseId)
        {
            var result = await _context.jsonViewModels.FromSql($"HrmSpGetEmployeeOtherExpense {employeeId},{otherExpenseId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<JsonViewModel> GetEmployeeInfoLoadByIdAndCompany(int companyId, int employeeId)
        {
            var result = await _context.jsonViewModels.FromSql($"CmnSpGetEmployeeInfoLoadByIdAndCompany {companyId},{employeeId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> GetLoanInformation(int loanId, int employeeId, int userId)
        {
            var result = await _context.jsonViewModels.FromSql($"GetLoanInformation {loanId},{employeeId},{userId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> GetLoanCategoryJson()
        {
            var result = await _context.jsonViewModels.FromSql($"HrmSpGetLoanCategoryJson").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<JsonViewModel> GetEmployeeWithLoan(int loanCategoryId)
        {
            var result = await _context.jsonViewModels.FromSql($"HrmSpGetEmployeeWithLoan {loanCategoryId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<JsonViewModel> GetEmployeeLoanDetails(int loanId)
        {
            var result = await _context.jsonViewModels.FromSql($"HrmSpGetEmployeeLoanDetails {loanId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<bool> CancelLoan(int loanId, int userId)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"HrmSpUpdateCancelLoan {loanId}, {userId}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }

        public async Task<JsonViewModel> GetInterestTypeJson()
        {
            var result = await _context.jsonViewModels.FromSql($"HrmSpGetInterestTypeJson").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<int> SaveLoanInfo(string id, LoanInfoViewModel employeeViewModel)
        {
            try
            {
                var result = await _context.saveUpdateValueViewModels.FromSql($"SaveLoanInfo {id},{employeeViewModel.loadId},{employeeViewModel.employeeId},{employeeViewModel.LoanCategoryId},{employeeViewModel.interestTypeId},{employeeViewModel.applicationNo},{employeeViewModel.applicationDate},{employeeViewModel.issueDate},{employeeViewModel.registrationNo},{employeeViewModel.engineNo},{employeeViewModel.interestRate},{employeeViewModel.NumOfInstallment},{employeeViewModel.AmountOfInstallment},{employeeViewModel.loanAmount},{employeeViewModel.salaryCalulationTypeId},{employeeViewModel.purchaseAmount},{employeeViewModel.purchaseDate},{employeeViewModel.isClose}").AsNoTracking().FirstOrDefaultAsync();

                int loanEntryId = result.isSuccess;

                //Details

                DateTime payDate = (DateTime)employeeViewModel.issueDate;
                decimal? opening = employeeViewModel.loanAmount;
                decimal? closing = employeeViewModel.loanAmount;
                decimal? interest = employeeViewModel.interestRate;
                if (employeeViewModel.salaryCalulationTypeId == 2)
                {
                    interest = employeeViewModel.AmountOfInstallment * (employeeViewModel.interestRate / 100);
                }
                decimal? amountTobepaid = employeeViewModel.AmountOfInstallment + interest;
                decimal? cumulativePrincipal = 0;
                decimal? cumulativeInterest = 0;
                decimal? principalAmount = employeeViewModel.AmountOfInstallment;
                for (int i = 0; i < employeeViewModel.NumOfInstallment; i++)
                {
                    if (i == 0)
                    {
                        payDate = payDate.AddDays(-payDate.Day + 1).AddMonths(0);
                    }
                    else
                    {
                        payDate = payDate.AddMonths(1);
                    }
                    if (closing < employeeViewModel.AmountOfInstallment)
                    {
                        opening = closing;
                        cumulativePrincipal = cumulativePrincipal + closing;
                        amountTobepaid = closing;
                        principalAmount = closing;
                        cumulativeInterest = cumulativeInterest + interest;
                        closing = closing - closing;
                    }
                    else
                    {
                        opening = closing;
                        cumulativePrincipal = cumulativePrincipal + amountTobepaid;
                        cumulativeInterest = cumulativeInterest + interest;
                        closing = closing - amountTobepaid;
                    }

                    await _context.saveUpdateValueViewModels.FromSql($"SaveLoanLogHistory {id},{0},{loanEntryId},{employeeViewModel.employeeId},{payDate},{null},{0},{opening},{principalAmount},{interest},{amountTobepaid},{cumulativePrincipal},{cumulativeInterest},{closing}").AsNoTracking().FirstOrDefaultAsync();

                }

                return loanEntryId;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<EmployeeInfoUploadVerifyViewModel>> GetEmployeeInfoUploadDataVerify(List<EmployeeInfoUploadVerifyViewModel> models)
        {
            JsonViewModel jr = new JsonViewModel();
            string[] arrStatus = { };
            var statusMessage = "";
            bool flag = true;
            var status1 = "";
            var status2 = "";
            var status3 = "";
            var status4 = "";
            var status5 = "";
            foreach (var m in models)
            {
                try
                {
                    var result = await _context.jsonViewModels.FromSql($"EmployeeInfoUploadDataVerify {m.employeeNo},{m.regionCode},{m.areaCode},{m.territoryCode},{m.depotCode},{m.joiningdate},{m.postingType},{m.salaryLocation},{m.salaryDepot}").AsNoTracking().FirstOrDefaultAsync();
                    //[{"status":"0:Invalid employee code; Invalid salary head"}]
                    string[] res = result.data.Replace("[{\"status\":\"", "").Replace("\"}]", "").Split(":");

                    // int.TryParse(res[0], out int eid);
                    //  int.TryParse(res[1], out int shid);

                    //m.joiningDate = 
                    m.regionName = res[2];
                    m.areaName = res[4];
                    m.territoryName = res[6];
                    m.depotName = res[8];
                    var status = res[9];
                    if (status != "")
                    {
                        arrStatus = status.Split(";");
                        if (arrStatus.Length >= 0)
                        {
                            if (arrStatus.Length >= 1)
                            {
                                status1 = arrStatus[0];
                                statusMessage = status1;
                                m.status = statusMessage;
                            }

                            if (arrStatus.Length >= 2)
                            {
                                status2 = arrStatus[1];
                                statusMessage = "";
                                statusMessage = status1 + ";" + status2;
                                m.status = statusMessage;
                            }
                            if (arrStatus.Length >= 3)
                            {
                                status3 = arrStatus[2];
                                statusMessage = "";
                                statusMessage = status1 + ";" + status2 + ";" + status3;
                                m.status = statusMessage;


                            }
                            if (arrStatus.Length >= 4)
                            {
                                status4 = arrStatus[3];
                                statusMessage = "";
                                statusMessage = status1 + ";" + status2 + ";" + status3 + ";" + status4;
                                m.status = statusMessage;

                            }
                            if (arrStatus.Length >= 5)
                            {
                                status5 = arrStatus[4];
                                statusMessage = "";
                                statusMessage = status1 + ";" + status2 + ";" + status3 + ";" + status4 + ";" + status5;
                                m.status = statusMessage;

                            }
                        }
                    }

                    else
                    {
                        m.status = "OK";
                    }

                }
                catch (System.Exception ex)
                {
                    throw ex;
                }
            }
            return models;
        }

        //public async Task<List<EmployeeInfoUploadVerifyViewModel>> GetEmployeeInfoUploadDataVerify(List<EmployeeInfoUploadVerifyViewModel> models)
        //{
        //    JsonViewModel jr = new JsonViewModel();
        //    string[] arrStatus = { };
        //    var statusMessage = "";
        //    bool flag = true;
        //    var status1 = "";
        //    var status2 = "";
        //    var status3 = "";
        //    var status4 = "";
        //    var status5 = "";

        //    foreach (var m in models)
        //    {
        //        try
        //        {
        //            var result = await _context.jsonViewModels.FromSql($"EmployeeInfoUploadDataVerify {m.employeeNo},{m.regionCode},{m.areaCode},{m.territoryCode},{m.depotCode},{m.joiningdate},{m.postingType},{m.salaryLocation},{m.salaryDepot}")
        //                .AsNoTracking()
        //                .FirstOrDefaultAsync();

        //            //[{"status":"0:Invalid employee code; Invalid salary head"}]
        //            string[] res = result.data.Replace("[{\"status\":\"", "").Replace("\"}]", "").Split(":");

        //            m.regionName = res[2];
        //            m.areaName = res[4];
        //            m.territoryName = res[6];
        //            m.depotName = res[8];
        //            m.salaryLocation = res[10];   // Mapping the `salaryLocation` field
        //            m.salaryDepot = res[11];       // Mapping the `salaryDepot` field

        //            var status = res[12]; // Adjust index to fetch status message correctly
        //            if (status != "")
        //            {
        //                arrStatus = status.Split(";");
        //                if (arrStatus.Length >= 0)
        //                {
        //                    if (arrStatus.Length >= 1)
        //                    {
        //                        status1 = arrStatus[0];
        //                        statusMessage = status1;
        //                        m.status = statusMessage;
        //                    }

        //                    if (arrStatus.Length >= 2)
        //                    {
        //                        status2 = arrStatus[1];
        //                        statusMessage = "";
        //                        statusMessage = status1 + ";" + status2;
        //                        m.status = statusMessage;
        //                    }
        //                    if (arrStatus.Length >= 3)
        //                    {
        //                        status3 = arrStatus[2];
        //                        statusMessage = "";
        //                        statusMessage = status1 + ";" + status2 + ";" + status3;
        //                        m.status = statusMessage;
        //                    }
        //                    if (arrStatus.Length >= 4)
        //                    {
        //                        status4 = arrStatus[3];
        //                        statusMessage = "";
        //                        statusMessage = status1 + ";" + status2 + ";" + status3 + ";" + status4;
        //                        m.status = statusMessage;
        //                    }
        //                    if (arrStatus.Length >= 5)
        //                    {
        //                        status5 = arrStatus[4];
        //                        statusMessage = "";
        //                        statusMessage = status1 + ";" + status2 + ";" + status3 + ";" + status4 + ";" + status5;
        //                        m.status = statusMessage;
        //                    }
        //                }
        //            }
        //            else
        //            {
        //                m.status = "OK";
        //            }
        //        }
        //        catch (System.Exception ex)
        //        {
        //            throw ex;
        //        }
        //    }
        //    return models;
        //}


        public async Task<int> SaveEmployeeInfoFromExcel(string userId, List<EmployeeInfoUploadVerifyViewModel> models)
        {
            var result = new SaveUpdateValueViewModel();
            try
            {
                foreach (var model in models)
                {
                    DateTime joiningDate = DateTime.Parse(model.joiningdate);
                    result = await _context.saveUpdateValueViewModels.FromSql($"SetSPSaveEmployeeInfoFromExcel {userId},{model.employeeId},{model.employeeNo},{model.employeeName},{model.designation},{model.department},{model.contractNumber},{joiningDate},{model.regionCode},{model.areaCode},{model.territoryCode},{model.depotCode},{model.postingType},{model.salaryLocation},{model.salaryDepot}").AsNoTracking().FirstOrDefaultAsync();
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            return result.isSuccess;
        }

        public async Task<List<InactiveEmployeeInfoUploadVerifyViewModel>> GetInactiveEmployeeInfoUploadDataVerify(List<InactiveEmployeeInfoUploadVerifyViewModel> models)
        {
            JsonViewModel jr = new JsonViewModel();



            foreach (var m in models)
            {
                try
                {
                    var result = await _context.jsonViewModels.FromSql($"InactiveEmployeeInfoUploadDataVerify  {m.employeeNo},{m.inActiveDate}").AsNoTracking().FirstOrDefaultAsync();
                    //[{"status":"0:Invalid employee code; Invalid salary head"}]
                    string[] res = result.data.Replace("[{\"status\":\"", "").Replace("\"}]", "").Split(":");
                    if (res[0].Length <= 0)
                    {
                        m.status = "Employee not Exits";

                    }
                    else if (res[1] != "")
                    {
                        m.status = "Invalid Inactive Date Format";
                    }
                    else
                    {
                        m.status = "OK";
                    }

                }
                catch (System.Exception ex)
                {
                    throw ex;
                }
            }
            return models;
        }
        public async Task<JsonViewModel> GetPayrollEmployeeById(int companyId, int employeeId)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"CmnSpGetPayrollEmployee {companyId},{employeeId}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (Exception)
            {

                throw;
            }

        }
        public async Task<int> SaveInactiveEmployeeInfoFromExcel(string userId, List<InactiveEmployeeInfoUploadVerifyViewModel> models)
        {
            var result = new SaveUpdateValueViewModel();
            try
            {
                foreach (var model in models)
                {
                    DateTime inactiveDate = DateTime.Parse(model.inActiveDate);
                    result = await _context.saveUpdateValueViewModels.FromSql($"SetSPSaveInactiveEmployeeInfoFromExcel {userId},{model.employeeNo},{inactiveDate}").AsNoTracking().FirstOrDefaultAsync();
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            return result.isSuccess;
        }

        #endregion


        #region Employee Transfer Info
        public async Task<bool> SaveEmployeeTransfer(string id, EmployeeTransferViewModel model)
        {
            try
            {

                var result = await _context.saveUpdateViewModels.FromSql($"CmnSpSetEmployeeTransfer {id},{model.employeeTransferId},{model.employeeId},{model.HrmSalaryLocationId},{model.HrmNewSalaryLocationId},{model.grossSalary},{model.transferDate},{model.status},{model.remarks}").AsNoTracking().FirstOrDefaultAsync();
                return result.isSuccess;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<JsonViewModel> GetEmployeeTransferById(int employeeTransferId)
        {
            var result = await _context.jsonViewModels.FromSql($"CmnSpGetEmployeeTransfer {employeeTransferId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<bool> DeleteEmployeeTransfer(string id, int employeeTransferId)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"CmnSpDeleteTransfer {id},{employeeTransferId}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }
        #endregion

        #region Employee Promotion
        public async Task<bool> SaveEmployeePromotion(string id, EmployeePromotionViewModel model)
        {
            try
            {
                var result = await _context.saveUpdateViewModels.FromSql($"CmnSpSetEmployeePromotion {id},{model.employeePromotionId},{model.employeeId},{model.HrmSalaryLocationId},{model.HrmNewSalaryLocationId},{model.previousDesignation},{model.currentDesignation},{model.previousDepartment},{model.currentDepartment},{model.PreviousGrossSalary},{model.NewGrossSalary},{model.incrementSalary},{model.promotionDate},{model.status},{model.remarks},{model.type},{model.prevSalarySlabId},{model.NewSalarySlabId}").AsNoTracking().FirstOrDefaultAsync();
                return result.isSuccess;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<JsonViewModel> GetEmployeePromotionById(int employeePromotionId)
        {
            var result = await _context.jsonViewModels.FromSql($"CmnSpGetPromotion {employeePromotionId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> GetEmployeeConfirmationById(int employeePromotionId)
        {
            var result = await _context.jsonViewModels.FromSql($"CmnSpGetConfirmation {employeePromotionId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<bool> deleteEmployeePromotion(string id, int employeePromotionId)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"CmnSpDeletePromotion {id},{employeePromotionId}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }


        #endregion


    }
}
