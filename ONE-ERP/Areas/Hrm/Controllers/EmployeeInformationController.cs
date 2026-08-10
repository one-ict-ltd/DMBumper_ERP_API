using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ONEERP.Areas.Hrm.Models;
using ONEERP.Areas.Salary.Models;
using ONEERP.Data.Entity;
using ONEERP.Data.Entity.FieldForceTracking;
using ONEERP.ERPService.AuthService.Interfaces;
using ONEERP.ERPServices.FieldForceTracking.Interfaces;
using ONEERP.ERPServices.Hrm.EmployeesInfo.Interfaces;
using ONEERP.Helpers;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.Hrm.Controllers
{
    [Route("api/[controller]")]
    public class EmployeeInformationController : Controller
    {
        object jwts;
        ApplicationUser user;

        private IUserInfoes userInfoes;
        private IEmployeeInfoService employeeInfoService;
        private readonly IEmployeeService employeeService;

        public EmployeeInformationController(IUserInfoes userInfoes, IEmployeeInfoService employeeInfoService, IEmployeeService employeeService)
        {
            this.userInfoes = userInfoes;
            this.employeeInfoService = employeeInfoService;
            this.employeeService = employeeService;
        }

        #region Employee For User Create & GET DON'T CHANGE THIS service

        [HttpPost("setEmployeeForCreateUser")]
        public async Task<IActionResult> setEmployeeForCreateUser([FromBody] EmployeeViewModel model)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);
            if (model.employeeId == null)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Employee has not created successfully.", false);
                return new OkObjectResult(jwt);
            }

            bool result = await employeeInfoService.SaveEmployeeForCreateUser(user.employeeId.ToString(), model);
            if (result == true)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Employee created successfully.", true);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Employee has not created successfully.", false);
                return new OkObjectResult(jwt);
            }
        }

        [HttpGet("getEmployee")]
        public async Task<IActionResult> getEmployee(int companyId, int employeeId)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            var datajson = await employeeInfoService.GetEmployeeById(companyId, employeeId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        #endregion DONT CHANGE THIS API


        #region Employee Info     

        [HttpGet("GetMaxEmployeeNo")]
        public async Task<IActionResult> GetMaxEmployeeNo(int companyId)
        {

            if (Authentication().Result == false) return new OkObjectResult(jwts);

            var datajson = await employeeInfoService.GetMaxEmployeeNo(companyId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpPost("SaveEmployeeBasicInfo")]
        public async Task<IActionResult> SaveEmployeeBasicInfo([FromBody] EmployeeInformationViewModel model)
        {
            if (!Authentication().Result) return new OkObjectResult(jwts);

            if (model.fullName == "" || model.fullName == null)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Employee has not created successfully.", false);
                return new OkObjectResult(jwt);
            }

            bool result = await employeeInfoService.SaveEmployeeBasicInfo(user.employeeId.ToString(), model);
            if (result)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Employee created successfully.", true);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Employee has not created successfully.", false);
                return new OkObjectResult(jwt);
            }
        }

        [HttpPost("UpdateSalesLimit")]
        public async Task<IActionResult> UpdateSalesLimit([FromBody] string territoryCode)
        {
            #region Common
            var uid = Request.Headers["auth_token"];
            if (uid.Count() == 0)
            {
                bool status = false;
                string actionresult = "Invalid Token.";
                var jwts = await Tokens.changePasswordJwt(status, actionresult, new JsonSerializerSettings { Formatting = Formatting.Indented });
                return new OkObjectResult(jwts);
            }
            var stream = uid;
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(stream);

            var tokenS = handler.ReadToken(stream) as JwtSecurityToken;
            var jti = tokenS.Claims.First(claim => claim.Type == "Id").Value;
            var user = await userInfoes.GetUserBasicInfoesbyId(jti);

            var employee = await employeeService.GetEmployeeLoadViewModels();
            employee = employee.Where(x => x.employeeNo == user.UserName).ToList();

            if (employee?.FirstOrDefault()?.Token != uid && employee.Count() != 0)
            {
                bool status = false;
                string actionresult = "Invalid Token.";
                var jwts = await Tokens.changePasswordJwt(status, actionresult, new JsonSerializerSettings { Formatting = Formatting.Indented });
                return new OkObjectResult(jwts);
            }
            #endregion
            bool result = await employeeInfoService.UpdateSalesLimit(user.employeeId.ToString(), territoryCode);
            if (result)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Sales Limit Updated.", true);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Sales Limit not Updated.", false);
                return new OkObjectResult(jwt);
            }
        }

        [HttpPost("UpdatePostingLocation")]
        public async Task<IActionResult> UpdatePostingLocation([FromBody] UpdatePostingViewModel updatePostingViewModel)
        {
            #region Common
            var uid = Request.Headers["auth_token"];
            if (uid.Count() == 0)
            {
                bool status = false;
                string actionresult = "Invalid Token.";
                var jwts = await Tokens.changePasswordJwt(status, actionresult, new JsonSerializerSettings { Formatting = Formatting.Indented });
                return new OkObjectResult(jwts);
            }
            var stream = uid;
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(stream);

            var tokenS = handler.ReadToken(stream) as JwtSecurityToken;
            var jti = tokenS.Claims.First(claim => claim.Type == "Id").Value;
            var user = await userInfoes.GetUserBasicInfoesbyId(jti);

            var employee = await employeeService.GetEmployeeLoadViewModels();
            employee = employee.Where(x => x.employeeNo == user.UserName).ToList();

            if (employee?.FirstOrDefault()?.Token != uid && employee.Count() != 0)
            {
                bool status = false;
                string actionresult = "Invalid Token.";
                var jwts = await Tokens.changePasswordJwt(status, actionresult, new JsonSerializerSettings { Formatting = Formatting.Indented });
                return new OkObjectResult(jwts);
            }
            #endregion
            bool result = await employeeInfoService.UpdatePostingLocation(user.employeeId.ToString(), updatePostingViewModel);
            if (result)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Information Updated.", true);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Information not Updated.", false);
                return new OkObjectResult(jwt);
            }
        }

        [HttpPost("UpdateEmployeeFirebaseInfo")]
        public async Task<IActionResult> UpdateEmployeeFirebaseInfo([FromBody] EmployeeFireBaseViewModel model)
        {
            var uid = Request.Headers["auth_token"];
            if (uid.Count() == 0)
            {
                bool status = false;
                string actionresult = "Invalid Token.";
                var jwts = await Tokens.changePasswordJwt(status, actionresult, new JsonSerializerSettings { Formatting = Formatting.Indented });
                return new OkObjectResult(jwts);
            }
            var stream = uid;
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(stream);

            var tokenS = handler.ReadToken(stream) as JwtSecurityToken;
            var jti = tokenS.Claims.First(claim => claim.Type == "Id").Value;
            var user = await userInfoes.GetUserBasicInfoesbyId(jti);

            var employee = await employeeService.GetEmployeeLoadViewModels();
            employee = employee.Where(x => x.employeeNo == user.UserName).ToList();

            if (employee?.FirstOrDefault()?.Token != uid && employee.Count() != 0)
            {
                bool status = false;
                string actionresult = "Invalid Token.";
                var jwts = await Tokens.changePasswordJwt(status, actionresult, new JsonSerializerSettings { Formatting = Formatting.Indented });
                return new OkObjectResult(jwts);
            }

            if (model.employeeId == null || model.employeeId == 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Employee Firebase Token has not updated successfully.", false);
                return new OkObjectResult(jwt);
            }

            bool result = await employeeInfoService.UpdateEmployeeFirebaseToken(user.employeeId.ToString(), model);
            if (result == true)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Employee Firebase Token has updated successfully.", true);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Employee Firebase Token has not updated successfully.", false);
                return new OkObjectResult(jwt);
            }
        }

        [HttpPost("SaveEmployeeMessageInfo")]
        public async Task<IActionResult> SaveEmployeeMessageInfo([FromBody] CmnMessageInfo model)
        {
            var uid = Request.Headers["auth_token"];
            if (uid.Count() == 0)
            {
                bool status = false;
                string actionresult = "Invalid Token.";
                var jwts = await Tokens.changePasswordJwt(status, actionresult, new JsonSerializerSettings { Formatting = Formatting.Indented });
                return new OkObjectResult(jwts);
            }
            var stream = uid;
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(stream);

            var tokenS = handler.ReadToken(stream) as JwtSecurityToken;
            var jti = tokenS.Claims.First(claim => claim.Type == "Id").Value;
            var user = await userInfoes.GetUserBasicInfoesbyId(jti);

            var employee = await employeeService.GetEmployeeLoadViewModels();
            employee = employee.Where(x => x.employeeNo == user.UserName).ToList();

            if (employee?.FirstOrDefault()?.Token != uid && employee.Count() != 0)
            {
                bool status = false;
                string actionresult = "Invalid Token.";
                var jwts = await Tokens.changePasswordJwt(status, actionresult, new JsonSerializerSettings { Formatting = Formatting.Indented });
                return new OkObjectResult(jwts);
            }

            if (model.toEmployeeId == null || model.toEmployeeId == 0)
            {
                string actionresult = "Employee Firebase Token has not updated successfully.";
                var jwts = await Tokens.changePasswordJwt(false, actionresult, new JsonSerializerSettings { Formatting = Formatting.Indented });
                return new OkObjectResult(jwts);
            }

            bool result = await employeeInfoService.SaveEmployeeMessageInfo(user.employeeId.ToString(), model);
            if (result == true)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Employee Firebase Token has updated successfully.", true);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Employee Firebase Token has not updated successfully.", false);
                return new OkObjectResult(jwt);
            }
        }

        [HttpGet("GetMessageInfoById")]
        public async Task<IActionResult> GetMessageInfoById(int employeeId)
        {
            var uid = Request.Headers["auth_token"];
            if (uid.Count() == 0)
            {
                bool status = false;
                string actionresult = "Invalid Token.";
                var jwts = await Tokens.changePasswordJwt(status, actionresult, new JsonSerializerSettings { Formatting = Formatting.Indented });
                return new OkObjectResult(jwts);
            }
            var stream = uid;
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(stream);

            var tokenS = handler.ReadToken(stream) as JwtSecurityToken;
            var jti = tokenS.Claims.First(claim => claim.Type == "Id").Value;
            var user = await userInfoes.GetUserBasicInfoesbyId(jti);

            var employee = await employeeService.GetEmployeeLoadViewModels();
            employee = employee.Where(x => x.employeeNo == user.UserName).ToList();

            if (employee?.FirstOrDefault()?.Token != uid && employee.Count() != 0)
            {
                bool status = false;
                string actionresult = "Invalid Token.";
                var jwts = await Tokens.changePasswordJwt(status, actionresult, new JsonSerializerSettings { Formatting = Formatting.Indented });
                return new OkObjectResult(jwts);
            }

            var datajson = await employeeInfoService.GetMessageInfoById((int)user.employeeId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpGet("GetEmployeeBasicInfoById")]
        public async Task<IActionResult> GetEmployeeBasicInfoById(int employeeId)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            var datajson = await employeeInfoService.GetEmployeeBasicInfoById(employeeId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }
        
        [HttpGet("GetEmployeeBasicInfoByCompanyId")]
        public async Task<IActionResult> GetEmployeeBasicInfoByCompanyId(int employeeId)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            var datajson = await employeeInfoService.GetEmployeeBasicInfoByCompanyId(user.employeeId, employeeId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpGet("GetEmployeeBasicInfoByIdNew")]
        public async Task<IActionResult> GetEmployeeBasicInfoByIdNew()
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            var datajson = await employeeInfoService.GetEmployeeBasicInfoByIdNew((int)user.employeeId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }
        [HttpGet("GetEmployeeBasicInfoByIdOptimized")]
        public async Task<IActionResult> GetEmployeeBasicInfoByIdOptimized()
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            var datajson = await employeeInfoService.GetEmployeeBasicInfoByIdOptimized((int)user.employeeId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }
        [HttpGet("GetEmployeeBasicInfoByIdForESS")]
        public async Task<IActionResult> GetEmployeeBasicInfoByIdForESS()
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            var datajson = await employeeInfoService.GetEmployeeBasicInfoByIdForESS((int)user.employeeId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }
        
        [HttpGet("GetLeaveSummaryForESSJson")]
        public async Task<IActionResult> GetLeaveSummaryForESSJson()
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            var datajson = await employeeInfoService.GetLeaveSummaryForESSJson((int)user.employeeId, DateTime.Now.Year);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }
        
        [HttpGet("GetCelebtationForESSJson")]
        public async Task<IActionResult> GetCelebtationForESSJson()
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            var datajson = await employeeInfoService.GetCelebtationForESSJson((int)user.employeeId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpGet("GetDispatcher")]
        public async Task<IActionResult> GetDispatcher()
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            var datajson = await employeeInfoService.GetDispatcher(user.employeeId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }


        [HttpPost("SaveEmployeeOtherExpense")]
        public async Task<IActionResult> SaveEmployeeOtherExpense([FromBody] EmployeeOtherExpenseViewModel model)
        {
            if (!Authentication().Result) return new OkObjectResult(jwts);

            if (model.employeeId == 0 || model.employeeId == null)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Employee has not selected successfully.", false);
                return new OkObjectResult(jwt);
            }

            bool result = await employeeInfoService.SaveEmployeeOtherExpense(user.employeeId.ToString(), model);
            if (result)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Employee other expense created successfully.", true);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Employee  other expense  has not created successfully.", false);
                return new OkObjectResult(jwt);
            }
        }

        [HttpPost("DeleteEmployeeOtherExpense")]
        public async Task<IActionResult> DeleteEmployeeOtherExpense([FromBody] int otherExpenseId)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            if (otherExpenseId <= 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Employee expense has not deleted.", false);
                return new OkObjectResult(jwt);
            }
            bool result = await employeeInfoService.DeleteEmployeeOtherExpense(user.employeeId.ToString(), otherExpenseId);

            if (result == true)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Employee Expense has deleted successfully.", true);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Employee Expense has not deleted.", false);
                return new OkObjectResult(jwt);
            }
        }

       
        [HttpGet("GetEmployeeInfoLoadById")]
        public async Task<IActionResult> GetEmployeeInfoLoadById(int employeeId)
        {
            //var AuthModel = await authenticator.AuthenticationStatus(Request.Headers["auth_token"]);
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            var datajson = await employeeInfoService.GetGetEmployeeInfoLoadById(employeeId,(int)user.employeeId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpGet("GetEmployeeInfoWhoHasLeaveById")]
        public async Task<IActionResult> GetEmployeeInfoWhoHasLeaveById(int employeeId)
        {
            //var AuthModel = await authenticator.AuthenticationStatus(Request.Headers["auth_token"]);
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            var datajson = await employeeInfoService.GetEmployeeInfoWhoHasLeaveById(employeeId, (int)user.employeeId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpGet("GetEmployeeInfoLoadByIdOptimized")]
        public async Task<IActionResult> GetEmployeeInfoLoadByIdOptimized(int employeeId)
        {
            //var AuthModel = await authenticator.AuthenticationStatus(Request.Headers["auth_token"]);
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            var datajson = await employeeInfoService.GetEmployeeInfoLoadByIdOptimized(employeeId, (int)user.employeeId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }
        [HttpGet("GetEmployeeInfoLoadByIdOptimizedForPaySlip")]
        public async Task<IActionResult> GetEmployeeInfoLoadByIdOptimizedForPaySlip(int employeeId)
        {
            //var AuthModel = await authenticator.AuthenticationStatus(Request.Headers["auth_token"]);
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            var datajson = await employeeInfoService.GetEmployeeInfoLoadByIdOptimizedForPaySlip(employeeId, (int)user.employeeId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }


        [HttpGet("GetEmployeeInfoByPosting")]
        public async Task<IActionResult> GetEmployeeInfoByPosting(int employeeId)
        {
            //var AuthModel = await authenticator.AuthenticationStatus(Request.Headers["auth_token"]);
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            var datajson = await employeeInfoService.GetGetEmployeeInfoByPosting(employeeId, (int)user.employeeId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }
        [HttpGet("GetEmployeeOtherExpense")]
        public async Task<IActionResult> GetEmployeeOtherExpense(int otherExpenseId)
        {
            //var AuthModel = await authenticator.AuthenticationStatus(Request.Headers["auth_token"]);
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            var datajson = await employeeInfoService.GetEmployeeOtherExpense((int)user.employeeId, otherExpenseId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpGet("GetEmployeeInfoLoadByIdAndCompany")]
        public async Task<IActionResult> GetEmployeeInfoLoadByIdAndCompany(int companyId,int employeeId)
        {
            //var AuthModel = await authenticator.AuthenticationStatus(Request.Headers["auth_token"]);
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            var datajson = await employeeInfoService.GetEmployeeInfoLoadByIdAndCompany(companyId,employeeId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpGet("GetLoanInformation")]
        public async Task<IActionResult> GetLoanInformation(int loanId, int employeeId)
        {
            //var AuthModel = await authenticator.AuthenticationStatus(Request.Headers["auth_token"]);
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            var datajson = await employeeInfoService.GetLoanInformation(loanId, employeeId,(int)user.employeeId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpGet("GetLoanCategory")]
        public async Task<IActionResult> GetLoanCategory()
        {
            //var AuthModel = await authenticator.AuthenticationStatus(Request.Headers["auth_token"]);
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            var datajson = await employeeInfoService.GetLoanCategoryJson();
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpGet("GetEmployeeWithLoan")]
        public async Task<IActionResult> GetEmployeeWithLoan(int loanCategoryId)
        {
            //var AuthModel = await authenticator.AuthenticationStatus(Request.Headers["auth_token"]);
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            var datajson = await employeeInfoService.GetEmployeeWithLoan(loanCategoryId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpGet("GetEmployeeLoanDetails")]
        public async Task<IActionResult> GetEmployeeLoanDetails(int loanId)
        {
            //var AuthModel = await authenticator.AuthenticationStatus(Request.Headers["auth_token"]);
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            var datajson = await employeeInfoService.GetEmployeeLoanDetails(loanId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpGet("CancelLoan")]
        public async Task<IActionResult> CancelLoan(int loanId)
        {
            //var AuthModel = await authenticator.AuthenticationStatus(Request.Headers["auth_token"]);
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            bool result = await employeeInfoService.CancelLoan(loanId, (int)user.employeeId);
            if (result == true)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Loan has updated successfully.", true);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Loan has not updated successfully.", false);
                return new OkObjectResult(jwt);
            }
        }

        [HttpGet("GetInterestType")]
        public async Task<IActionResult> GetInterestType()
        {
            //var AuthModel = await authenticator.AuthenticationStatus(Request.Headers["auth_token"]);
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            var datajson = await employeeInfoService.GetInterestTypeJson();
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpPost("SaveLoanInfo")]
        public async Task<IActionResult> SaveLoanInfo([FromBody] LoanInfoViewModel model)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            if (model.employeeId == 0 || model.employeeId == null)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Loan has not created successfully.", false);
                return new OkObjectResult(jwt);
            }

            int result = await employeeInfoService.SaveLoanInfo(user.employeeId.ToString(), model);
            if (result > 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Loan created successfully.", true);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Loan has not created successfully.", false);
                return new OkObjectResult(jwt);
            }
        }


        [HttpPost("deleteEmployee")]
        public async Task<IActionResult> deleteEmployee([FromBody] int employeeId)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            if (employeeId <= 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Employee has not deleted.", false);
                return new OkObjectResult(jwt);
            }
            bool result = await employeeInfoService.DeleteEmployeeById(user.employeeId.ToString(), employeeId);

            if (result == true)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Employee has deleted successfully.", true);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Employee has not deleted.", false);
                return new OkObjectResult(jwt);
            }
        }

        [HttpGet("GetDuplicateEmployeeNo")]
        public async Task<IActionResult> GetDuplicateEmployeeNo(int employeeId, string employeeNo)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            var datajson = await employeeInfoService.GetDuplicateEmployeeNo(employeeId, employeeNo);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpGet("getDuplicateTerritoty")]
        public async Task<IActionResult> getDuplicateTerritoty(int employeeId, string PostingLocation,string Code)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            var datajson = await employeeInfoService.getDuplicateTerritoty(employeeId, PostingLocation, Code);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }
        #endregion


        #region Employee Transfer     

        [HttpPost("SaveEmployeeTransfer")]
        public async Task<IActionResult> SaveEmployeeTransfer([FromBody] EmployeeTransferViewModel model)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            //if (model.fullName == "" || model.fullName == null)
            //{
            //    var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Employee has not created successfully.", false);
            //    return new OkObjectResult(jwt);
            //}

            bool result = await employeeInfoService.SaveEmployeeTransfer(user.employeeId.ToString(), model);
            if (result == true)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Transfer created successfully.", true);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Transfer has not created successfully.", false);
                return new OkObjectResult(jwt);
            }
        }

        [HttpGet("GetEmployeeTransferById")]
        public async Task<IActionResult> GetEmployeeTransferById(int employeeTransferId)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            var datajson = await employeeInfoService.GetEmployeeTransferById(employeeTransferId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpPost("deleteEmployeeTransfer")]
        public async Task<IActionResult> deleteEmployeeTransfer([FromBody] int employeeTransferId)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            if (employeeTransferId <= 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Employee has not deleted.", false);
                return new OkObjectResult(jwt);
            }
            bool result = await employeeInfoService.DeleteEmployeeTransfer(user.employeeId.ToString(), employeeTransferId);

            if (result == true)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Employee has deleted successfully.", true);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Employee has not deleted.", false);
                return new OkObjectResult(jwt);
            }
        }

        #endregion

        #region Employee Promotion     

        [HttpPost("SaveEmployeePromotion")]
        public async Task<IActionResult> SaveEmployeePromotion([FromBody] EmployeePromotionViewModel model)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);


            bool result = await employeeInfoService.SaveEmployeePromotion(user.employeeId.ToString(), model);



            if (result == true)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Promotion created successfully.", true);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Promotion has not created successfully.", false);
                return new OkObjectResult(jwt);
            }
        }

        [HttpGet("GetEmployeePromotionById")]
        public async Task<IActionResult> GetEmployeePromotionById(int employeePromotionId)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            var datajson = await employeeInfoService.GetEmployeePromotionById(employeePromotionId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpGet("GetEmployeeConfirmationById")]
        public async Task<IActionResult> GetEmployeeConfirmationById(int employeePromotionId)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            var datajson = await employeeInfoService.GetEmployeeConfirmationById(employeePromotionId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpPost("deleteEmployeePromotion")]
        public async Task<IActionResult> deleteEmployeePromotion([FromBody] int employeePromotionId)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            if (employeePromotionId <= 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Employee has not deleted.", false);
                return new OkObjectResult(jwt);
            }
            bool result = await employeeInfoService.deleteEmployeePromotion(user.employeeId.ToString(), employeePromotionId);

            if (result == true)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Employee has deleted successfully.", true);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Employee has not deleted.", false);
                return new OkObjectResult(jwt);
            }
        }

        #endregion



        #region Token Check
        async Task<bool> Authentication()
        {
            #region common
            var uid = Request.Headers["auth_token"];
            if (uid.Count() == 0)
            {
                bool status = false;
                string actionresult = "Invalid Token.";
                jwts = await Tokens.changePasswordJwt(status, actionresult, new JsonSerializerSettings { Formatting = Formatting.Indented });
                return false;
            }

            var stream = uid;
            var handler = new JwtSecurityTokenHandler();
            var tokenS = handler.ReadToken(stream) as JwtSecurityToken;
            var jti = tokenS.Claims.First(claim => claim.Type == "Id").Value;
            user = await userInfoes.GetUserBasicInfoesbyId(jti);

            if (user.token != uid && user != null)
            {
                bool status = false;
                string actionresult = "Invalid Token.";
                jwts = Tokens.changePasswordJwt(status, actionresult, new JsonSerializerSettings { Formatting = Formatting.Indented });
                return false;
            }
            return true;
            #endregion
        }

        #endregion


        [HttpPost("GetEmployeeInfoUploadDataVerify")]
        public async Task<IActionResult> GetEmployeeInfoUploadDataVerify([FromBody] List<EmployeeInfoUploadVerifyViewModel> models)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            var obj = await employeeInfoService.GetEmployeeInfoUploadDataVerify(models);

            var jwt = await Tokens.ObjToJson(obj);
            return new OkObjectResult(jwt);
        }

        [HttpGet("GetPayrollEmployeeById")]
        public async Task<IActionResult> GetPayrollEmployeeById(int companyId, int employeeId)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            var datajson = await employeeInfoService.GetPayrollEmployeeById(companyId, employeeId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpPost("SaveEmployeeInfoFromExcelFile")]
        public async Task<IActionResult> SaveEmployeeInfoFromExcelFile([FromBody] List<EmployeeInfoUploadVerifyViewModel> models)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            if (models.Count == 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Employee Info is empty", false);
                return new OkObjectResult(jwt);
            }
            var result = await employeeInfoService.SaveEmployeeInfoFromExcel(user.employeeId.ToString(), models);
            if (result == 1)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Employee Info has saved successfully.", true);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Employee Info has not updated.", false);
                return new OkObjectResult(jwt);
            }
        }



        [HttpPost("GetInactiveEmployeeInfoUploadDataVerify")]
        public async Task<IActionResult> GetInactiveEmployeeInfoUploadDataVerify([FromBody] List<InactiveEmployeeInfoUploadVerifyViewModel> models)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            var obj = await employeeInfoService.GetInactiveEmployeeInfoUploadDataVerify(models);

            var jwt = await Tokens.ObjToJson(obj);
            return new OkObjectResult(jwt);
        }



        [HttpPost("SaveInactiveEmployeeInfoFromExcelFile")]
        public async Task<IActionResult> SaveInactiveEmployeeInfoFromExcelFile([FromBody] List<InactiveEmployeeInfoUploadVerifyViewModel> models)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            if (models.Count == 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Employee Info is empty", false);
                return new OkObjectResult(jwt);
            }
            var result = await employeeInfoService.SaveInactiveEmployeeInfoFromExcel(user.employeeId.ToString(), models);
            if (result == 1)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Employee Info has saved successfully.", true);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Employee Info has not updated.", false);
                return new OkObjectResult(jwt);
            }
        }

        [HttpPost("SaveDemoEmployeeBasicInfo")]
        public async Task<IActionResult> SaveDemoEmployeeBasicInfo([FromBody] EmployeeInformationViewModel model)
        {
            if (!Authentication().Result) return new OkObjectResult(jwts);

            if (model.fullName == "" || model.fullName == null)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Employee has not created successfully.", false);
                return new OkObjectResult(jwt);
            }

            var result = await employeeInfoService.SaveDemoEmployeeBasicInfo(user.employeeId.ToString(), model);
            var response = await Tokens.ObjToJson(result);
            return new OkObjectResult(response);
        }
    }
}
