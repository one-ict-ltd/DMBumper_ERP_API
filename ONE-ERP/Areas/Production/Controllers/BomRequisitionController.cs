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
    public class BomRequisitionController : ControllerBase
    {
        object jwts;
        ApplicationUser user;
        private readonly IUserInfoes userInfoes;
        private readonly IBomRequisitionService service;
        public BomRequisitionController(IUserInfoes _userInfoes,IBomRequisitionService bomRequisitionService)
        {
            userInfoes = _userInfoes;
            jwts = new object();
            user = new ApplicationUser();
            service = bomRequisitionService;
        }

        [HttpGet("GetMaxRMRequisitionMasterNumber")]
        public async Task<IActionResult> GetMaxRMRequisitionMasterNumber(DateTime bomDate)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            var datajson = await service.GetMaxRMRequisitionMasterNumber(bomDate);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpGet("GetMaxPMRequisitionMasterNumber")]
        public async Task<IActionResult> GetMaxPMRequisitionMasterNumber(DateTime bomDate)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            var datajson = await service.GetMaxPMRequisitionMasterNumber(bomDate);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpGet("GetProductSpecificationByBomIdFromBomDetails")]
        public async Task<IActionResult> GetProductSpecificationByBomIdFromBomDetails(int bomId,int bomForId)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            var datajson = await service.GetProductSpecificatinDataByIdFromBomDetails(bomId, bomForId,user.employeeId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpPost("SaveRMRequisitionMaster")]
        public async Task<IActionResult> SaveRMRequisitionMaster([FromBody] RmRequisitionViewModel model)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            if (model == null)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "RM Requisition has not created.", false);
                return new OkObjectResult(jwt);
            }

            int bomId = await service.SaveRMRequisitionMaster(user.employeeId.ToString(), model);

            if (bomId == 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "RM Requisition has not created.", false);
                return new OkObjectResult(jwt);
            }


            int result = await service.SaveRMRequisitionDetails(user.employeeId.ToString(), model.lstDetailsViewModel, bomId);

            ///int result = await service.CreateAutoJournalForBOM(user.employeeId.ToString(), model);

            if (result != 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "RM Requisition Details has created successfully.", true);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "RM Requisition Details has not created.", false);
                return new OkObjectResult(jwt);
            }
        }

        [HttpGet("GetRMRequisitionMasterById")]
        public async Task<IActionResult> GetRMRequisitionMasterById(int? requisitionId)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            var datajson = await service.GetRMRequisitionById(requisitionId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }
        [HttpGet("GetRMRequisitionMasterByIdWithDate")]
        public async Task<IActionResult> GetRMRequisitionMasterByIdWithDate(DateTime fromDate, DateTime toDate, int? requisitionId)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            var datajson = await service.GetRMRequisitionByIdWithDate(fromDate, toDate, requisitionId, user.employeeId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }
        [HttpPost("DeleteRMRequisitionMasterById")]
        public async Task<IActionResult> DeleteRMRequisitionMasterById([FromBody] int requisitionId)
        {

            if (Authentication().Result == false) return new OkObjectResult(jwts);

            if (requisitionId <= 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "RM Requisition Master has not deleted.", false);
                return new OkObjectResult(jwt);
            }
            string result = await service.DeleteRMRequisitionById(user.employeeId.ToString(), requisitionId);

            if (result !=null && result=="success")
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "RM Requisition Master has deleted successfully.", true);
                return new OkObjectResult(jwt);
            }
            if (result != null)
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



        [HttpGet("GetRMRequisitionDetailsByMasterId")]
        public async Task<IActionResult> GetRMRequisitionDetailsByMasterId(int? requisitionId)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            var datajson = await service.GetRMRequisitionDetailsByMasterId(requisitionId, user.employeeId);
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

        [HttpGet("GetRequisitionNumberforIssue")]
        public async Task<IActionResult> GetRequisitionNumberforIssue(int type)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            var datajson = await service.GetRequisitionNoForIssue(type,(int)user.employeeId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }



        #region product Issue
        [HttpGet("GetMaxIssueMasterNumber")]
        public async Task<IActionResult> GetMaxIssueMasterNumber(DateTime bomDate, int type)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            var datajson = await service.GetMaxIssueMasterNumber(bomDate,type);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        #endregion
       
    }
}
