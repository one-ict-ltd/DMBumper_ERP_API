using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ONEERP.Areas.Production.Models;
using ONEERP.Areas.Purchase.Models;
using ONEERP.Data.Entity;
using ONEERP.ERPService.AuthService.Interfaces;
using ONEERP.ERPServices.Production.Interfaces;
using ONEERP.ERPServices.Purchase;
using ONEERP.ERPServices.Purchase.Interfaces;
using ONEERP.Helpers;
using ONEERP.Models;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.Production.Controllers
{
    [Route("api/[controller]")]
    public class ReagentReceiveController : Controller
    {
        object jwts; ApplicationUser user;
        private IUserInfoes userInfoes;
        private IReagentReceiveService _reagentReceiveService;
        public ReagentReceiveController(IUserInfoes userInfoes, IReagentReceiveService reagentReceiveService)
        {
            this.userInfoes = userInfoes;
            this._reagentReceiveService = reagentReceiveService;
        }

        [HttpGet("GetMaxReagentReceiveNumber")]
        public async Task<IActionResult> GetMaxReagentIssueNumber(DateTime receiveDate)
        {
            #region Common
            if (Authentication().Result == false) return new OkObjectResult(jwts);
            #endregion

            var datajson = await _reagentReceiveService.GetMaxReagentReceiveNumber(receiveDate);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }


        [HttpGet("GetReagentIssueNumberForReceive")]
        public async Task<IActionResult> GetReagentIssueNumberForReceive()
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            var datajson = await _reagentReceiveService.GetReagentIssueNumberForReceive((int)user.employeeId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpGet("GetReagentIssueDetailsByMasterIdForReceive")]
        public async Task<IActionResult> GetReagentIssueDetailsByMasterIdForReceive(int issueId)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            var datajson = await _reagentReceiveService.GetReagentIssueDetailsByMasterIdForReceive(issueId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpPost("SaveReagentReceiveMaster")]
        public async Task<IActionResult> SaveReagentReceiveMaster([FromBody] ReagentReceiveViewModel model)
        {
            int result = 0;
            int flag = model.reagentReceiveMasterId;
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            if (model == null)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Reagent Receive has not created.", false);
                return new OkObjectResult(jwt);
            }

            int receiveId = await _reagentReceiveService.SaveReagentReceiveMaster((int)user.employeeId, model);

            if (receiveId == 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Reagent Receive has not created.", false);
                return new OkObjectResult(jwt);
            }

            result = await _reagentReceiveService.SaveReagentReceiveDetails(user.employeeId.ToString(), model.lstDetailsViewModel, receiveId);

            if (result != 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Reagent Receive Details has created successfully.", true);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Reagent Receive Details has not created.", false);
                return new OkObjectResult(jwt);
            }
        }

        [HttpGet("getReagentReceiveListByDate")]
        public async Task<IActionResult> GetReagentReceiveListByDate(DateTime fromDate, DateTime toDate, int? receiveId)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);
            
            var datajson = await _reagentReceiveService.GetReagentReceiveListByDate((int)user.employeeId, fromDate, toDate, receiveId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpGet("getReagentReceiveDetailsByMasterId")]
        public async Task<IActionResult> GetReagentReceiveDetailsByMasterId(int? receiveId)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);
            
            var datajson = await _reagentReceiveService.GetReagentReceiveDetailsByMasterId(receiveId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpPost("DeleteReagentReceiveById")]
        public async Task<IActionResult> DeleteReagentReceiveById([FromBody] int issueId)
        {

            if (Authentication().Result == false) return new OkObjectResult(jwts);

            if (issueId <= 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Reagent Requisition Master has not deleted.", false);
                return new OkObjectResult(jwt);
            }
            string result = await _reagentReceiveService.DeleteReagentReceiveById(user.employeeId.ToString(), issueId);

            if (result != null && result == "success")
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Reagent Requisition Master has deleted successfully.", true);
                return new OkObjectResult(jwt);
            }
            if (result != null)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, result, false);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Reagent Requisition Master has not deleted.", false);
                return new OkObjectResult(jwt);
            }
        }

        async Task<bool> Authentication()
        {
            #region common
            var uid = Request.Headers["auth_token"];
            if (!uid.Any())
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

            if (user == null || user.token != uid)
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
