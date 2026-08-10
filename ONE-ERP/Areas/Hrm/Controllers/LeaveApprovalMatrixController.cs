using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ONEERP.Areas.Hrm.Models;
using ONEERP.ERPService.AuthService.Interfaces;
using ONEERP.ERPServices.Hrm.EmployeesInfo.Interfaces;
using ONEERP.ERPServices.Hrm.Leave.Interfaces;
using ONEERP.Helpers;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.Hrm.Controllers
{
    [Route("api/[controller]")]
    public class LeaveApprovalMatrixController : Controller
    {
        private IUserInfoes userInfoes;
        private ILeaveApprovalMatrixService leaveApprovalMatrixService;
        private IHrmMasterService hrmMasterService;

        public LeaveApprovalMatrixController(IUserInfoes userInfoes, ILeaveApprovalMatrixService leaveApprovalMatrixService, IHrmMasterService hrmMasterService)
        {
            this.userInfoes = userInfoes;
            this.leaveApprovalMatrixService = leaveApprovalMatrixService;
            this.hrmMasterService = hrmMasterService;
        }

        [HttpPost("SaveApprovalMatrix")]
        public async Task<IActionResult> SaveLeaveApprovalMatrix([FromBody] LeaveApprovalMatrixViewModel model)
        {
            try
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

                if (model.lstDetails.Count() == 0)
                {
                    var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "ApprovalMatrix has not created.", false);
                    return new OkObjectResult(jwt);
                }

                int result = 0;
                if (model.departmentId == null) model.departmentId = 0;
                if (model.employeeId == null) model.employeeId = 0;

                result = await leaveApprovalMatrixService.SaveApprovalMatrix(user.employeeId.ToString(), model.lstDetails, (int)model.employeeId, (int)model.departmentId);


                if (result != 0)
                {
                    var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "ApprovalMatrix has created successfully.", true);
                    return new OkObjectResult(jwt);
                }
                else
                {
                    var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "ApprovalMatrix has not created.", false);
                    return new OkObjectResult(jwt);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
           
        }

        [HttpGet("GetApprovalMatrix")]
        public async Task<IActionResult> GetApprovalMatrix(int employeeId)
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

            var datajson = await leaveApprovalMatrixService.GetLeaveApprovalMatrixByEmployeeIdJson(employeeId,user.employeeId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpPost("DeleteApprovalMatrixByemployeeId")]
        public async Task<IActionResult> DeleteApprovalMatrixByemployeeId([FromBody] int employee)
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

            if (employee <= 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "ApprovalMatrix has not deleted.", false);
                return new OkObjectResult(jwt);
            }
            bool result = await leaveApprovalMatrixService.DeleteApprovalMatrixByTypeId(user.employeeId.ToString(), employee);

            if (result == true)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "ApprovalMatrix has deleted successfully.", true);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "ApprovalMatrix has not deleted.", false);
                return new OkObjectResult(jwt);
            }
        }

    }
}
