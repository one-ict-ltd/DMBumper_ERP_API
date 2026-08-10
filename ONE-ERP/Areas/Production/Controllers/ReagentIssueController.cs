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
    public class ReagentIssueController : Controller
    {
        object jwts; ApplicationUser user;
        private IUserInfoes userInfoes;
        private IReagentIssueService _reagentIssueService;
        public ReagentIssueController(IUserInfoes userInfoes, IReagentIssueService reagentIssueService)
        {
            this.userInfoes = userInfoes;
            this._reagentIssueService = reagentIssueService;
        }

        [HttpGet("GetMaxReagentIssueNumber")]
        public async Task<IActionResult> GetMaxReagentIssueNumber(DateTime reagentIssueDate)
        {
            #region Common
            if (Authentication().Result == false) return new OkObjectResult(jwts);
            #endregion

            var datajson = await _reagentIssueService.GetMaxReagentIssueNumber(reagentIssueDate);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }


        [HttpGet("GetReagentRequisitionNumberforIssue")]
        public async Task<IActionResult> GetRequisitionNumberforIssue()
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            var datajson = await _reagentIssueService.GetReagentRequisitionNumberforIssue((int)user.employeeId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpGet("GetReagentRequisitionByIdToIssue")]
        public async Task<IActionResult> GetReagentRequisitionByIdToIssue(int reagentReqId)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            var datajson = await _reagentIssueService.GetReagentRequisitionByIdToIssue((int)user.employeeId, reagentReqId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpPost("SaveReagentIssueMaster")]
        public async Task<IActionResult> SaveReagentIssueMaster([FromBody] ReagentIssueViewModel model)
        {
            int result = 0;
            int flag = model.reagentIssueMasterId;
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            if (model == null)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Reagent Issues has not created.", false);
                return new OkObjectResult(jwt);
            }

            int issueId = await _reagentIssueService.SaveReagentIssueMaster((int)user.employeeId, model);

            if (issueId == 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Reagent Issuess has not created.", false);
                return new OkObjectResult(jwt);
            }

            result = await _reagentIssueService.SaveReagentIssueDetails((int)user.employeeId, model.lstDetailsViewModel, issueId);


            if (result != 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Reagent Issue Details has created successfully.", true);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Reagent Issue Details has not created.", false);
                return new OkObjectResult(jwt);
            }
        }

        [HttpGet("getReagentIssueListByDate")]
        public async Task<IActionResult> GetReagentIssueListByDate(DateTime fromDate, DateTime toDate, int? issueId, string typeOfIssue)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            var datajson = await _reagentIssueService.GetReagentIssueListByDate((int)user.employeeId, fromDate, toDate, issueId, typeOfIssue);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpGet("getReagentIssueDetailsByMasterId")]
        public async Task<IActionResult> GetReagentIssueDetailsByMasterId(int? issueId)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            var datajson = await _reagentIssueService.GetReagentIssueDetailsByMasterId(issueId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpPost("DeleteReagentIssueMasterById")]
        public async Task<IActionResult> DeleteReagentIssueMasterById([FromBody] int issueId)
        {

            if (Authentication().Result == false) return new OkObjectResult(jwts);

            if (issueId <= 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Reagent Requisition Master has not deleted.", false);
                return new OkObjectResult(jwt);
            }
            string result = await _reagentIssueService.DeleteReagentIssueById(user.employeeId.ToString(), issueId);

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
