using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ONEERP.Areas.Production.Models;
using ONEERP.Data.Entity;
using ONEERP.ERPService.AuthService.Interfaces;
using ONEERP.ERPServices.Production.Interfaces;
using ONEERP.Helpers;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.Production.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductionIssueController : ControllerBase
    {
        object jwts;
        ApplicationUser user;
        private readonly IUserInfoes userInfoes;
        private readonly IProductionIssueAndReceived service;
        public ProductionIssueController(IUserInfoes _userInfoes, IProductionIssueAndReceived productionIssueService)
        {
            userInfoes = _userInfoes;
            jwts = new object();
            user = new ApplicationUser();
            service = productionIssueService;
        }

        [HttpPost("SaveIssueMaster")]
        public async Task<IActionResult> SaveIssueMaster([FromBody] ProductionIssueViewModel model)
        {
            int result = 0;
            int flag = model.productIssueMasterId;
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            if (model == null)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "RM Issues has not created.", false);
                return new OkObjectResult(jwt);
            }

            int issueId = await service.SaveIssueMaster(user.employeeId.ToString(), model);

            if (issueId == 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "RM Issuess has not created.", false);
                return new OkObjectResult(jwt);
            }
            if (flag == 0)
            {
                 result = await service.SaveIssueDetails(user.employeeId.ToString(), model.lstDetailsViewModel, issueId);
            }

           

            ///int result = await service.CreateAutoJournalForBOM(user.employeeId.ToString(), model);
            ///
          

            if (result != 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Production Issue Details has created successfully.", true);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Production Issue Details has not created.", false);
                return new OkObjectResult(jwt);
            }
        }

        [HttpGet("GetIssueMasterById")]
        public async Task<IActionResult> GetIssueMasterById(int? issueId, string typeOfIssue)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            var datajson = await service.GetIssueById(issueId, typeOfIssue);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpPost("DeleteIssueMasterById")]
        public async Task<IActionResult> DeleteIssueMasterById([FromBody] int issueId)
        {

            if (Authentication().Result == false) return new OkObjectResult(jwts);

            if (issueId <= 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "RM Requisition Master has not deleted.", false);
                return new OkObjectResult(jwt);
            }
            string result = await service.DeleteIssueById(user.employeeId.ToString(), issueId);

            if (result != null && result=="success")
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "RM Requisition Master has deleted successfully.", true);
                return new OkObjectResult(jwt);
            }
            if (result != null )
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, result, false);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "RM Requisition Master has not deleted.", false);
                return new OkObjectResult(jwt);
            }
        }



        [HttpGet("GetIssueDetailsByMasterId")]
        public async Task<IActionResult> GetIssueDetailsByMasterId(int? issueId)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            var datajson = await service.GetIssueDetailsByMasterId(issueId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpGet("GetIssueMasterByIdDate")]
        public async Task<IActionResult> GetIssueMasterByIdDate(DateTime fromDate, DateTime toDate, int? issueId,string typeOfIssue)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            var datajson = await service.GetIssueByIdDate((int)user.employeeId ,fromDate, toDate, issueId, typeOfIssue);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
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
