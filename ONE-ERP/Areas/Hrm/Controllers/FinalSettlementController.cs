using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using Newtonsoft.Json;
using ONEERP.Data.Entity;
using ONEERP.ERPService.AuthService.Interfaces;
using ONEERP.Helpers;
using ONEERP.ERPServices.Hrm.EmployeesInfo.Interfaces;
using ONEERP.Areas.Hrm.Models;

namespace ONEERP.Areas.Hrm.Controllers
{
    [Route("api/[controller]")]
    public class FinalSettlementController : Controller
    {
        object jwts;
        ApplicationUser user;
        private IHrmMasterService hrmMasterService;
        private IUserInfoes userInfoes;
        private TokenAuthenticator authenticator;
        public FinalSettlementController(IUserInfoes userInfoes, IHrmMasterService hrmMasterService)
        {
            this.userInfoes = userInfoes;
            this.hrmMasterService = hrmMasterService;
            authenticator = new TokenAuthenticator(userInfoes);
            jwts = new object();
            user = new ApplicationUser();
        }
        [HttpGet("GetPayableList")]
        public async Task<IActionResult> GetPayableList()
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);
            var datajson = await hrmMasterService.GetPayableList(user.employeeId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }
        [HttpGet("GetReceivableList")]
        public async Task<IActionResult> GetReceivableList()
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);
            var datajson = await hrmMasterService.GetReceivableList(user.employeeId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }
        [HttpGet("GetMarketOutstanding")]
        public async Task<IActionResult> GetMarketOutstanding(DateTime? fDate, DateTime? tDate, string employeeNo)
        {
            //var AuthModel = await authenticator.AuthenticationStatus(Request.Headers["auth_token"]);
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            var datajson = await hrmMasterService.GetMarketOutstanding((int)user.employeeId, fDate, tDate, employeeNo);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }
        [HttpGet("GetEmployeeInfoForFinalSettlement")]
        public async Task<IActionResult> GetEmployeeInfoForFinalSettlement(int employeeId)
        {
            //var AuthModel = await authenticator.AuthenticationStatus(Request.Headers["auth_token"]);
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            var datajson = await hrmMasterService.GetEmployeeInfoForFinalSettlement(employeeId, (int)user.employeeId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }
        [HttpGet("GetEmployeeFinalSettlementbyId")]
        public async Task<IActionResult> GetEmployeeFinalSettlementbyId(int finalSettlementMasterId)
        {
            
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            var datajson = await hrmMasterService.GetEmployeeFinalSettlementbyId((int)user.employeeId, finalSettlementMasterId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }
        [HttpGet("GetfinalSettlementDataForApproval")]
        public async Task<IActionResult> GetfinalSettlementDataForApproval()
        {

            if (Authentication().Result == false) return new OkObjectResult(jwts);

            var datajson = await hrmMasterService.GetfinalSettlementDataForApproval((int)user.employeeId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }
        [HttpGet("GetEmployeeFinalSettlementDetailsById")]
        public async Task<IActionResult> GetEmployeeFinalSettlementDetailsById(int finalSettlementMasterId)
        {

            if (Authentication().Result == false) return new OkObjectResult(jwts);

            var datajson = await hrmMasterService.GetEmployeeFinalSettlementDetailsById((int)user.employeeId, finalSettlementMasterId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }
        [HttpGet("GetEmployeeFinalSettlementSignatoryById")]
        public async Task<IActionResult> GetEmployeeFinalSettlementSignatoryById(int finalSettlementMasterId)
        {

            if (Authentication().Result == false) return new OkObjectResult(jwts);

            var datajson = await hrmMasterService.GetEmployeeFinalSettlementSignatoryById((int)user.employeeId, finalSettlementMasterId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }
        [HttpPost("SaveEmployeeFinalSettlement")]
        public async Task<IActionResult> SaveEmployeeFinalSettlement([FromBody] HrmFinalSettlementViewModel model)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);
            int finalSettlementMasterId = await hrmMasterService.SaveEmployeeFinalSettlement(user.employeeId, model);

            await hrmMasterService.SaveEmployeeFinalSettlementDetails(user.employeeId, finalSettlementMasterId, model.finalSettlementDetails);
            await hrmMasterService.SaveEmployeeFinalSettlementSignatory(user.employeeId, finalSettlementMasterId, model.SignatoryList);
            if (finalSettlementMasterId == 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Final Settlement Master has not created.", false);
            return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Final Settlement Master has created.", true);
                return new OkObjectResult(jwt);
            }
        }
        [HttpPost("DeleteEmployeeFinalSettlement")]
        public async Task<IActionResult> DeleteEmployeeFinalSettlement([FromBody] int finalSettlementMasterId)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            if (finalSettlementMasterId <= 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Employee final Settlement has not deleted.", false);
                return new OkObjectResult(jwt);
            }
            bool result = await hrmMasterService.DeleteEmployeeFinalSettlement((int)user.employeeId, finalSettlementMasterId);

            if (result == true)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Employee final Settlement has deleted successfully.", true);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Employee final Settlement has not deleted.", false);
                return new OkObjectResult(jwt);
            }
        }
        [HttpPost("DeleteSignatoryListById")]
        public async Task<IActionResult> DeleteSignatoryListById([FromBody] int signatoryId)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            if (signatoryId <= 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Signatory has not deleted.", false);
                return new OkObjectResult(jwt);
            }
            bool result = await hrmMasterService.DeleteSignatoryListById((int)user.employeeId, signatoryId);

            if (result == true)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Signatory has deleted successfully.", true);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Signatory has not deleted.", false);
                return new OkObjectResult(jwt);
            }
        }
        [HttpPost("SaveEmployeeFinalSettlementApproval")]
        public async Task<IActionResult> SaveEmployeeFinalSettlementApproval([FromBody] HrmFinalSettlementSignatoryApprovalViewModel model)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            if (model.finalSettlementApprovalModel.Count() == 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Final Settlement Not Found.", false);
                return new OkObjectResult(jwt);
            }
            int result = await hrmMasterService.SaveEmployeeFinalSettlementApproval((int)user.employeeId, model.approvalStatus, model.finalSettlementApprovalModel);

            if (result > 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Final Settlement has Approved successfully.", true);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Final Settlement has not Approved.", false);
                return new OkObjectResult(jwt);
            }
        }
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
    }
}
