using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ONEERP.Areas.MasterData.Models;
using ONEERP.ERPService.AuthService.Interfaces;
using ONEERP.ERPServices.MasterData.Interfaces;
using ONEERP.Helpers;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using ONEERP.Data.Entity;

namespace ONEERP.Areas.MasterData.Controllers
{
    [Route("api/[controller]")]

    public class ApprovalMatrixController : Controller
    {
        object jwts;
        ApplicationUser user;
        private readonly IUserInfoes userInfoes;
        private readonly IApprovalMatrixService approvalMatrixService;

        public ApprovalMatrixController(IUserInfoes userInfoes, IApprovalMatrixService approvalMatrixService)
        { 
            this.userInfoes = userInfoes;
            this.approvalMatrixService = approvalMatrixService;
            jwts = new object();
            user = new ApplicationUser();
        }


        #region Approval Type   

        [HttpGet("GetApprovalTypeById")]
        public async Task<IActionResult> GetApprovalTypeById(int approvalTypeId)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            var datajson = await approvalMatrixService.GetApprovalTypeById(approvalTypeId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpPost("SaveApprovalType")]
        public async Task<IActionResult> SaveApprovalType([FromBody] ApprovalTypeViewModel model)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            int res = await approvalMatrixService.SaveApprovalType(user.employeeId.ToString(), model);

            if (res > 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Approval Type has created successfully.", true);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Approval Type has not created.", false);
                return new OkObjectResult(jwt);
            }
        }

        [HttpPost("DeleteApprovalTypeId")]
        public async Task<IActionResult> DeleteApprovalTypeId([FromBody] int approvalTypeId)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);
            bool result = await approvalMatrixService.DeleteApprovalTypeByTypeId(user.employeeId.ToString(), approvalTypeId);

            if (result == true)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Approval Type has deleted successfully.", true);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Approval Type has not deleted.", false);
                return new OkObjectResult(jwt);
            }
        }

        #endregion

        #region Approver Type   

        [HttpPost("SaveApproverType")]
        public async Task<IActionResult> SaveApproverType([FromBody] ApproverTypeViewModel model)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            int res = await approvalMatrixService.SaveApproverType(user.employeeId.ToString(), model);
            if (res > 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Approver Type has created successfully.", true);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Approver Type has not created.", false);
                return new OkObjectResult(jwt);
            }
        }

        [HttpGet("GetApproverTypeById")]
        public async Task<IActionResult> GetApproverTypeById(int approverTypeId, int approvalTypeId)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            var datajson = await approvalMatrixService.GetApproverTypeById(approverTypeId, approvalTypeId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }


        [HttpGet("GetApproverTypeId")]
        public async Task<IActionResult> GetApproverTypeId(int approverTypeId)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            var datajson = await approvalMatrixService.GetApproverType(approverTypeId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpPost("DeleteApproverTypeId")]
        public async Task<IActionResult> DeleteApproverTypeId([FromBody] int approverTypeId)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            bool result = await approvalMatrixService.DeleteApproverTypeId(user.employeeId.ToString(), approverTypeId);

            if (result == true)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Approval Type has deleted successfully.", true);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Approval Type has not deleted.", false);
                return new OkObjectResult(jwt);
            }
        }

        #endregion


        #region Approval Matrix

        [HttpPost("SaveApprovalMatrix")]
        public async Task<IActionResult> SaveApprovalMatrix([FromBody] ApprovalMatrixViewModel model)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            int result = await approvalMatrixService.SaveApprovalMatrix(user.employeeId.ToString(), model.lstDetails, (int)model.approvalTypeId);

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

        [HttpGet("GetApprovalMatrix")]
        public async Task<IActionResult> GetApprovalMatrix(int approvalTypeId)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);
            var datajson = await approvalMatrixService.GetApprovalMatrix(approvalTypeId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpGet("GetApprovalMatrixByTypeId")]
        public async Task<IActionResult> GetApprovalMatrixByTypeId(int approvalTypeId)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);
            var datajson = await approvalMatrixService.GetApprovalMatrixByTypeId(approvalTypeId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpPost("DeleteApprovalMatrixByTypeId")]
        public async Task<IActionResult> DeleteApprovalMatrixByTypeId([FromBody] int approvalTypeId)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            if (approvalTypeId <= 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "ApprovalMatrix has not deleted.", false);
                return new OkObjectResult(jwt);
            }
            bool result = await approvalMatrixService.DeleteApprovalMatrixByTypeId(user.employeeId.ToString(), approvalTypeId);

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



        #endregion
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
