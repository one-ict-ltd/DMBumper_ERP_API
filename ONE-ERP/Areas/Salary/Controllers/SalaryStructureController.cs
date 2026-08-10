using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ONEERP.Areas.Salary.Models;
using ONEERP.Data.Entity;
using ONEERP.ERPService.AuthService.Interfaces;
using ONEERP.ERPServices.Salary.Interfaces;
using ONEERP.Helpers;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ONEERP.Areas.Salary.Controllers
{
    [Route("api/[controller]")]
    public class SalaryStructureController : Controller
    {
        object jwts;
        ApplicationUser user;
        private IUserInfoes userInfoes;
        private ISalaryMasterService salaryMasterService;
        private ISalaryStructureService salaryStructureService;
        public SalaryStructureController(IUserInfoes userInfoes, ISalaryMasterService salaryMasterService, ISalaryStructureService salaryStructureService)
        {
            jwts = new object();
            user = new ApplicationUser();

            this.userInfoes = userInfoes;
            this.salaryMasterService = salaryMasterService;
            this.salaryStructureService = salaryStructureService;
        }

        #region Salary Structure

        [HttpPost("SaveSalaryEmployeeStructure")]
        public async Task<IActionResult> SaveSalaryEmployeeStructure([FromBody] SalaryEmployeeStructureViewModel model)
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
            var tokenS = handler.ReadToken(stream) as JwtSecurityToken;
            var jti = tokenS.Claims.First(claim => claim.Type == "Id").Value;
            var user = await userInfoes.GetUserBasicInfoesbyId(jti);
            if (user.token != uid && user != null)
            {
                bool status = false;
                string actionresult = "Invalid Token.";
                var jwts = await Tokens.changePasswordJwt(status, actionresult, new JsonSerializerSettings { Formatting = Formatting.Indented });
                return new OkObjectResult(jwts);
            }

            if (model.employeeId == null || model.employeeId == 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Salary structure has not created successfully.", false);
                return new OkObjectResult(jwt);
            }
            bool deleteStructure = await salaryStructureService.DeleteSalaryEmployeeStructureByEmpId(user.employeeId.ToString(), (int)model.employeeId);
            int result = 0;
            var salaryHeadViewModels = await salaryMasterService.GetSalaryHeadListById(0);
            foreach (SalaryHeadViewModel head in salaryHeadViewModels)
            {
                decimal structureAmount = 0;
                var percentList = await salaryMasterService.GetSalaryGradePercentListById(0, model.salaryGradeId, head.salaryHeadId);
                var percentInfo = percentList.FirstOrDefault();
                if (percentInfo != null)
                {
                    if (percentInfo.salaryCalulationTypeId == 2) // percentage
                    {
                        structureAmount = model.slabAmount * (Convert.ToDecimal(percentInfo.percentAmount) / 100);
                    }
                    else //if (percentInfo.salaryCalulationTypeId == 1 || percentInfo.salaryCalulationTypeId == 3)// Fixed or Manual
                    {
                        structureAmount = Convert.ToDecimal(percentInfo.percentAmount);
                    }
                }
                result = await salaryStructureService.SaveSalaryEmployeeStructure(user.employeeId.ToString(), model, structureAmount, head.salaryHeadId);
            }

            result = await salaryStructureService.SaveSalaryBankCashStructure(user.employeeId.ToString(), model);

            if (result != 0)
            {
                await salaryStructureService.UpdateEmployeeDesignationAndDepartment(user.employeeId.ToString(), model);
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Salary structure has created successfully.", true);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Salary structure has not created successfully.", false);
                return new OkObjectResult(jwt);
            }
        }

        [HttpGet("GetSalaryAllEmployeeStructure")]
        public async Task<IActionResult> GetSalaryAllEmployeeStructure()
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
            var tokenS = handler.ReadToken(stream) as JwtSecurityToken;
            var jti = tokenS.Claims.First(claim => claim.Type == "Id").Value;
            var user = await userInfoes.GetUserBasicInfoesbyId(jti);

            if (user.token != uid && user != null)
            {
                bool status = false;
                string actionresult = "Invalid Token.";
                var jwts = await Tokens.changePasswordJwt(status, actionresult, new JsonSerializerSettings { Formatting = Formatting.Indented });
                return new OkObjectResult(jwts);

            }
            var datajson = await salaryStructureService.GetSalaryAllEmployeeStructure((int)user.employeeId);
            var jwt = await Tokens.getDataWithStatus(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpGet("GetSalaryEmployeeStructureByEmpId")]
        public async Task<IActionResult> GetSalaryEmployeeStructureByEmpId(int employeeId, string salaryHeadType)
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
            var tokenS = handler.ReadToken(stream) as JwtSecurityToken;
            var jti = tokenS.Claims.First(claim => claim.Type == "Id").Value;
            var user = await userInfoes.GetUserBasicInfoesbyId(jti);

            if (user.token != uid && user != null)
            {
                bool status = false;
                string actionresult = "Invalid Token.";
                var jwts = await Tokens.changePasswordJwt(status, actionresult, new JsonSerializerSettings { Formatting = Formatting.Indented });
                return new OkObjectResult(jwts);

            }
            var datajson = await salaryStructureService.GetSalaryEmployeeStructureByEmpId(employeeId, salaryHeadType);
            var jwt = await Tokens.getDataWithStatus(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpGet("GetDuplicateSalaryEmployeeStructure")]
        public async Task<IActionResult> GetDuplicateSalaryEmployeeStructure(int employeeId)
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
            var tokenS = handler.ReadToken(stream) as JwtSecurityToken;
            var jti = tokenS.Claims.First(claim => claim.Type == "Id").Value;
            var user = await userInfoes.GetUserBasicInfoesbyId(jti);

            if (user.token != uid && user != null)
            {
                bool status = false;
                string actionresult = "Invalid Token.";
                var jwts = await Tokens.changePasswordJwt(status, actionresult, new JsonSerializerSettings { Formatting = Formatting.Indented });
                return new OkObjectResult(jwts);
            }

            var datajson = await salaryStructureService.GetDuplicateSalaryEmployeeStructure(employeeId);
            var jwt = await Tokens.getDataWithStatus(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpPost("DeleteSalaryEmployeeStructureByEmpId")]
        public async Task<IActionResult> DeleteSalaryEmployeeStructureByEmpId([FromBody] int employeeId)
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
            var tokenS = handler.ReadToken(stream) as JwtSecurityToken;
            var jti = tokenS.Claims.First(claim => claim.Type == "Id").Value;
            var user = await userInfoes.GetUserBasicInfoesbyId(jti);
            if (user.token != uid && user != null)
            {
                bool status = false;
                string actionresult = "Invalid Token.";
                var jwts = await Tokens.changePasswordJwt(status, actionresult, new JsonSerializerSettings { Formatting = Formatting.Indented });
                return new OkObjectResult(jwts);
            }

            if (employeeId == 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Salary structure has not deleted.", false);
                return new OkObjectResult(jwt);
            }
            bool result = await salaryStructureService.DeleteSalaryEmployeeStructureByEmpId(user.employeeId.ToString(), employeeId);
            if (result == true)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Salary structure has deleted successfully.", true);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Salary structure has not deleted.", false);
                return new OkObjectResult(jwt);
            }
        }

        [HttpPost("UpdateSalaryEmployeeStructure")]
        public async Task<IActionResult> UpdateSalaryEmployeeStructure([FromBody] SalaryEmployeeStructureViewModel model)
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
            var tokenS = handler.ReadToken(stream) as JwtSecurityToken;
            var jti = tokenS.Claims.First(claim => claim.Type == "Id").Value;
            var user = await userInfoes.GetUserBasicInfoesbyId(jti);
            if (user.token != uid && user != null)
            {
                bool status = false;
                string actionresult = "Invalid Token.";
                var jwts = await Tokens.changePasswordJwt(status, actionresult, new JsonSerializerSettings { Formatting = Formatting.Indented });
                return new OkObjectResult(jwts);
            }

            if (model.employeeStructureId == 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Salary structure has not updated.", false);
                return new OkObjectResult(jwt);
            }
            bool result = await salaryStructureService.UpdateSalaryEmployeeStructure(user.employeeId.ToString(), model.employeeStructureId, model.structureAmount, model.isActive);
            if (result == true)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Salary structure has updated successfully.", true);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Salary structure has not updated.", false);
                return new OkObjectResult(jwt);
            }
        }

        [HttpPost("SaveSalaryEmployeeFixedHeadStructure")]
        public async Task<IActionResult> SaveSalaryEmployeeFixedHeadStructure([FromBody] List<SalaryEmployeeFixedHeadStructureViewModel> models)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            if (models.Count == 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Employee salary fixed head structure is empty", false);
                return new OkObjectResult(jwt);
            }
            var result = await salaryStructureService.SaveSalaryEmployeeFixedHeadStructure(user.employeeId.ToString(), models);
            if (result == 1)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Employee salary fixed head structure has saved successfully.", true);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Employee salary fixed head structure has not updated.", false);
                return new OkObjectResult(jwt);
            }
        }

        
        [HttpPost("SaveEmployeeSalaryStructureUpload")]
        public async Task<IActionResult> SaveEmployeeSalaryStructureUpload([FromBody] List<SalaryEmployeeStructureVerifyViewModel> models)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            if (models.Count == 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Employee salary Structure Upload  listis empty", false);
                return new OkObjectResult(jwt);
            }
            var result = await salaryStructureService.SaveEmployeeSalaryStructureUpload(user.employeeId.ToString(), models);
            if (result == 1)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Employee salary Structure  has saved successfully.", true);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Employee salary Structure Upload  has not updated.", false);
                return new OkObjectResult(jwt);
            }
        }

        [HttpPost("SaveBatchWiseSerialNoUpload")]
        public async Task<IActionResult> SaveBatchWiseSerialNoUpload([FromBody] List<BatchWiseSerialNoVerifyViewModel> models)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            if (models.Count == 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Batch Wise Serial No Upload list empty", false);
                return new OkObjectResult(jwt);
            }
            var result = await salaryStructureService.SaveBatchWiseSerialNoUpload(user.employeeId.ToString(), models);
            if (result == 1)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Batch Wise Serial No Upload has saved successfully.", true);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Batch Wise Serial No Upload has not updated.", false);
                return new OkObjectResult(jwt);
            }
        }

        [AllowAnonymous]
        [HttpGet("CheckBatchWiseSerialNo")]
        public async Task<IActionResult> CheckBatchWiseSerialNo(string serialNo)
        {
           
            var datajson = await salaryStructureService.CheckBatchWiseSerialNo(serialNo);

            if (datajson.data != "[]")
            {
                var message = "Congratulations!  " +
              "This code is valid. You have purchased an ORIGINAL One Pharma product. " +
              "Thank you for confirming with One Pharma. " +
              "One Pharma wishes you good health.";
                var jwt = await Tokens.getDataWithStatusAndMessage(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented }, message);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.getDataWithStatusAndMessage(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented }, "This code is Invalid.");
                return new OkObjectResult(jwt);
            }
        }

        [HttpPost("SaveEmployeeMobileBill")]
        public async Task<IActionResult> SaveEmployeeMobileBill([FromBody] List<MobileBillVerifyViewModel> models)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            if (models.Count == 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Mobile Bill List is empty", false);
                return new OkObjectResult(jwt);
            }
            var result = await salaryStructureService.SaveEmployeeMobileBill(user.employeeId.ToString(), models);
            if (result == 1)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Employee Mobile Bill has saved successfully.", true);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Employee Mobile Bill has not updated.", false);
                return new OkObjectResult(jwt);
            }
        }

        [HttpPost("UploadEmployeeSalaryStructure")]
        public async Task<IActionResult> UploadEmployeeSalaryStructure([FromBody] List<SalaryEmployeeStructureVerifyViewModel> models)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            if (models.Count == 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Mobile Bill List is empty", false);
                return new OkObjectResult(jwt);
            }
            var result = await salaryStructureService.UploadEmployeeSalaryStructure(user.employeeId.ToString(), models);
            if (result == 1)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Employee Mobile Bill has saved successfully.", true);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Employee Mobile Bill has not updated.", false);
                return new OkObjectResult(jwt);
            }
        }

        [HttpPost("DeleteSalaryEmployeeFixedHeadStructure")]
        public async Task<IActionResult> DeleteSalaryEmployeeFixedHeadStructure([FromBody] int empFixedHeadStructureId)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            if (empFixedHeadStructureId == 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Salary fixed head structure has not deleted.", false);
                return new OkObjectResult(jwt);
            }
            bool result = await salaryStructureService.DeleteSalaryEmployeeFixedHeadStructure(user.employeeId.ToString(), empFixedHeadStructureId);
            if (result == true)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Salary fixed head structure has deleted successfully.", true);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Salary fixed head structure has not deleted.", false);
                return new OkObjectResult(jwt);
            }
        }

        [HttpGet("GetSalaryEmployeeFixedHeadStructureById")]
        public async Task<IActionResult> GetSalaryEmployeeFixedHeadStructureById(int empFixedHeadStructureId,int salaryPeriodIdId)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);
            var uid = Request.Headers["auth_token"];
            var stream = uid;
            var handler = new JwtSecurityTokenHandler();
            var tokenS = handler.ReadToken(stream) as JwtSecurityToken;
            var jti = tokenS.Claims.First(claim => claim.Type == "Id").Value;
            var user = await userInfoes.GetUserBasicInfoesbyId(jti);
            var datajson = await salaryStructureService.GetSalaryEmployeeFixedHeadStructureById(empFixedHeadStructureId, salaryPeriodIdId,(int)user.employeeId);
            var jwt = await Tokens.GetJwt(datajson.data);
            return new OkObjectResult(jwt);
        }

        [HttpGet("GetEmployeeMobileBillById")]
        public async Task<IActionResult> GetEmployeeMobileBillById(int salaryPeriodIdId)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);
            var uid = Request.Headers["auth_token"];
            var stream = uid;
            var handler = new JwtSecurityTokenHandler();
            var tokenS = handler.ReadToken(stream) as JwtSecurityToken;
            var jti = tokenS.Claims.First(claim => claim.Type == "Id").Value;
            var user = await userInfoes.GetUserBasicInfoesbyId(jti);
            var datajson = await salaryStructureService.GetEmployeeMobileBillById(salaryPeriodIdId,(int)user.employeeId);
            var jwt = await Tokens.GetJwt(datajson.data);
            return new OkObjectResult(jwt);
        }

        [HttpGet("GetSalaryHeadByType")]
        public async Task<IActionResult> GetSalaryHeadByType(string salaryHeadType)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            var datajson = await salaryStructureService.GetSalaryHeadByType(salaryHeadType);
            var jwt = await Tokens.GetJwt(datajson.data);
            return new OkObjectResult(jwt);
        }
        [HttpGet("GetSalaryFixedHeadByEmpId")]
        public async Task<IActionResult> GetSalaryFixedHeadByEmpId(int employeeId)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            var datajson = await salaryStructureService.GetSalaryFixedHeadByEmpId(employeeId);
            var jwt = await Tokens.GetJwt(datajson.data);
            return new OkObjectResult(jwt);
        }

        [HttpPost("GetEmployeeSalaryFixedHeadUploadDataVerify")]
        public async Task<IActionResult> GetEmployeeSalaryFixedHeadUploadDataVerify([FromBody] List<SalaryEmployeeFixedHeadStructureVerifyViewModel> models)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            var obj = await salaryStructureService.GetEmployeeSalaryFixedHeadUploadDataVerify(models);

            var jwt = await Tokens.ObjToJson(obj);
            return new OkObjectResult(jwt);
        }

        [HttpPost("GetEmployeeSalaryStructureUploadDataVerify")]
        public async Task<IActionResult> GetEmployeeSalaryStructureUploadDataVerify([FromBody] List<SalaryEmployeeStructureVerifyViewModel> models)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            var obj = await salaryStructureService.GetEmployeeSalaryStructureUploadDataVerify(models);

            var jwt = await Tokens.ObjToJson(obj);
            return new OkObjectResult(jwt);
        }

        [HttpPost("GetBatchWiseSerialNoUploadDataVerify")]
        public async Task<IActionResult> GetBatchWiseSerialNoUploadDataVerify([FromBody] List<BatchWiseSerialNoVerifyViewModel> models)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            var obj = await salaryStructureService.GetBatchWiseSerialNoUploadDataVerify(models);

            var jwt = await Tokens.ObjToJson(obj);
            return new OkObjectResult(jwt);
        }

        [HttpPost("GetMobileBillUploadDataVerify")]
        public async Task<IActionResult> GetMobileBillUploadDataVerify([FromBody] List<MobileBillVerifyViewModel> models)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            var obj = await salaryStructureService.GetMobileBillUploadDataVerify(models);

            var jwt = await Tokens.ObjToJson(obj);
            return new OkObjectResult(jwt);
        }

        [HttpPost("GetVoucherUploadDataVerify")]
        public async Task<IActionResult> GetVoucherUploadDataVerify([FromBody] List<VoucherUploadVerifyViewModel> models)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            var obj = await salaryStructureService.GetVoucherUploadDataVerify(models);

            var jwt = await Tokens.ObjToJson(obj);
            return new OkObjectResult(jwt);
        }

        #endregion

        #region Salary Process

        [HttpPost("ProcessEmployeesSalary")]
        public async Task<IActionResult> ProcessEmployeesSalary([FromBody] SalaryEmployeeProcessViewModel model)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);
            
            if (model.salaryPeriodId == 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Salary has not processed.", false);
                return new OkObjectResult(jwt);
            }

            bool result = await salaryStructureService.ProcessEmployeesSalary(user.employeeId.ToString(), model.salaryPeriodId);
            if (result == true)
            {
                IPAddress remoteIpAddress = Request.HttpContext.Connection.RemoteIpAddress;
                string ipAddress = "";
                if (remoteIpAddress != null)
                {
                    if (remoteIpAddress.AddressFamily == System.Net.Sockets.AddressFamily.InterNetworkV6)
                    {
                        remoteIpAddress = System.Net.Dns.GetHostEntry(remoteIpAddress).AddressList.First(x => x.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork);
                    }
                    ipAddress = remoteIpAddress.ToString();
                }
                model.ipAddress = ipAddress;
                model.processName = "Salary";
                await salaryStructureService.SaveSalaryProcessLog(user.employeeId.ToString(), model);

                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Salary has processed successfully.", true);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Salary has not processed.", false);
                return new OkObjectResult(jwt);
            }
        }


        [HttpGet("GetSalaryMasterByPeriodId")]
        public async Task<IActionResult> GetSalaryMasterByPeriodId(int salaryPeriodId, string salaryDepotName)
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
            var tokenS = handler.ReadToken(stream) as JwtSecurityToken;
            var jti = tokenS.Claims.First(claim => claim.Type == "Id").Value;
            var user = await userInfoes.GetUserBasicInfoesbyId(jti);

            if (user.token != uid && user != null)
            {
                bool status = false;
                string actionresult = "Invalid Token.";
                var jwts = await Tokens.changePasswordJwt(status, actionresult, new JsonSerializerSettings { Formatting = Formatting.Indented });
                return new OkObjectResult(jwts);

            }
            var datajson = await salaryStructureService.GetSalaryMasterByPeriodId(salaryPeriodId,(int)user.employeeId, salaryDepotName);
            var jwt = await Tokens.getDataWithStatus(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        #endregion


        #region Target Upload

        [HttpGet("GetMiosalestargetmasterById")]
        public async Task<IActionResult> GetMiosalestargetmasterById(int targetMasterId)
        {
            if (!await Authentication()) return new OkObjectResult(jwts);

            var datajson = await salaryStructureService.GetMiosalestargetmasterById(targetMasterId);
            var jwt = await Tokens.GetJwt(datajson.data);
            return new OkObjectResult(jwt);
        }



        [HttpPost("DeleteMioItemWiseSalesTarget")]
        public async Task<IActionResult> DeleteMioItemWiseSalesTarget([FromBody] int targetId)
        {
            if (!await Authentication()) return new OkObjectResult(jwts);

            if (targetId == 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "MIO Sales Target has not deleted.", false);
                return new OkObjectResult(jwt);
            }
            bool result = await salaryStructureService.DeleteMioItemWiseSalesTarget(user.employeeId.ToString(), targetId);
            if (result)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "MIO Sales Target has deleted successfully.", true);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "MIO Sales Target has not deleted.", false);
                return new OkObjectResult(jwt);
            }
        }

        [HttpPost("SaveMioItemWiseSalesTarget")]
        public async Task<IActionResult> SaveMioItemWiseSalesTarget([FromBody] MioSalesTargetMasterViewModel models)
        {
            if (!await Authentication()) return new OkObjectResult(jwts);

            if (models.lstMaster.Count == 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "MIO Sales Target is empty", false);
                return new OkObjectResult(jwt);
            }
            var result = await salaryStructureService.SaveMioItemWiseSalesTarget(user.employeeId.ToString(), models);
            if (result > 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "MIO Sales Target has saved successfully.", true);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "MIO Sales Target has not updated.", false);
                return new OkObjectResult(jwt);
            }
        }

        [HttpPost("GetMioSalesTargetUploadDataVerify")]
        public async Task<IActionResult> GetMioSalesTargetUploadDataVerify([FromBody] List<MioSalesTargetViewModel> models)
        {
            if (!await Authentication()) return new OkObjectResult(jwts);

            var obj = await salaryStructureService.GetMioSalesTargetUploadDataVerify(models);

            var jwt = await Tokens.ObjToJson(obj);
            return new OkObjectResult(jwt);
        }

        [HttpGet("GetMioSalesTargetMasterWithDetailsById")]
        public async Task<IActionResult> GetMioSalesTargetMasterWithDetailsById(int targetMasterId)
        {
            if (!await Authentication()) return new OkObjectResult(jwts);

            var obj = await salaryStructureService.GetMiosalestargetmasterwithdetailsById(targetMasterId);

            var jwt = await Tokens.GetJwt(obj.data);
            return new OkObjectResult(jwt);
        }
        #endregion Target Upload

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
        #region Increment Upload
        [HttpPost("GetEmployeeIncrementUploadDataVerify")]
        public async Task<IActionResult> GetEmployeeIncrementUploadDataVerify([FromBody] List<SalaryEmployeeIncrementVerifyViewModel> models)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            var obj = await salaryStructureService.GetEmployeeIncrementUploadDataVerify(models);

            var jwt = await Tokens.ObjToJson(obj);
            return new OkObjectResult(jwt);
        }

        [HttpPost("SaveEmployeeSalaryIncrementUpload")]
        public async Task<IActionResult> SaveEmployeeSalaryIncrementUpload([FromBody] List<SalaryEmployeeIncrementVerifyViewModel> models)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            if (models.Count == 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Employee salary Increment Upload  listis empty", false);
                return new OkObjectResult(jwt);
            }
            var result = await salaryStructureService.SaveEmployeeSalaryIncrementUpload(user.employeeId.ToString(), models);
            if (result == 1)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Employee salary Increment  has saved successfully.", true);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Employee salary Increment Upload  has not updated.", false);
                return new OkObjectResult(jwt);
            }
        }
        #endregion
    }
}
