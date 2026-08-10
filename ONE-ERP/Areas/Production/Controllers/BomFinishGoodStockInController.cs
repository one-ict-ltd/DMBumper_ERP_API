using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ONEERP.Areas.Production.Models;
using ONEERP.Data.Entity;
using ONEERP.ERPService.AuthService.Interfaces;
using ONEERP.ERPServices.Production.Interfaces;
using ONEERP.Helpers;

namespace ONEERP.Areas.Production.Controllers
{
    [Route("api/[controller]")]
    public class BomFinishGoodStockInController : Controller
    {
        object jwts;
        ApplicationUser user;
        private readonly IUserInfoes userInfoes;
        private readonly IBomFinishGoodStockInService service;
        public BomFinishGoodStockInController(IUserInfoes _userInfoes, IBomFinishGoodStockInService _service)
        {
            userInfoes = _userInfoes;
            service = _service;
            jwts = new object();
            user = new ApplicationUser();
        }

        #region Master

        [HttpPost("SaveBomFinishGoodStockIn")]
        public async Task<IActionResult> SaveBomFinishGoodStockIn([FromBody] BomFinishGoodStockInMasterViewModel model)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            if (model == null)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "BOM Finish Good Stock In has not created.", false);
                return new OkObjectResult(jwt);
            }

            int bomStockInId = await service.SaveBomFinishGoodStockInMaster(user.employeeId.ToString(), model);

            if (bomStockInId == 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "BOM Finish Good Stock In has not created.", false);
                return new OkObjectResult(jwt);
            }


            int result = await service.SaveBomFinishGoodStockInDetails(user.employeeId.ToString(), model.lstDetailsViewModel, bomStockInId);

            ///int result = await service.CreateAutoJournalForBOM(user.employeeId.ToString(), model);

            if (result != 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "BOM Finish Good Stock In Details has created successfully.", true);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "BOM Finish Good Stock In Details has not created.", false);
                return new OkObjectResult(jwt);
            }
        }

        [HttpPost("DeleteBomFinishGoodStockInMasterById")]
        public async Task<IActionResult> DeleteBomFinishGoodStockInMasterById([FromBody] int bomStockInId)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            if (bomStockInId <= 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "BOM Finish Good Stock In has not deleted.", false);
                return new OkObjectResult(jwt);
            }
            bool result = await service.DeleteBomFinishGoodStockInMasterById(user.employeeId.ToString(), bomStockInId);

            if (result == true)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "BOM Finish Good Stock In has deleted successfully.", true);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "BOM Finish Good Stock In has not deleted.", false);
                return new OkObjectResult(jwt);
            }
        }

        [HttpGet("GetBomFinishGoodStockInMasterById")]
        public async Task<IActionResult> GetBomFinishGoodStockInMasterById(int? bomStockInId)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            var datajson = await service.GetBomFinishGoodStockInMasterById(bomStockInId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpGet("GetMaxBomFinishGoodStockInNumber")]
        public async Task<IActionResult> GetMaxBomFinishGoodStockInNumber(DateTime bomDate)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            var datajson = await service.GetMaxBomFinishGoodStockInNumber(bomDate);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpGet("GetBomFinishGoodProductSpec")]
        public async Task<IActionResult> GetBomFinishGoodProductSpec(int productId)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            var datajson = await service.GetBomFinishGoodProductSpec(productId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }
        #endregion

        #region Details

        [HttpGet("GetBomFinishGoodStockInDetailsByMasterId")]
        public async Task<IActionResult> GetBomFinishGoodStockInDetailsByMasterId(int? bomStockInId)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            var datajson = await service.GetBomFinishGoodStockInDetailsByMasterId(bomStockInId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpPost("DeleteBomFinishGoodStockInDetailsById")]
        public async Task<IActionResult> DeleteBomFinishGoodStockInDetailsById([FromBody] int bomStockInDetailsId)
        {

            if (Authentication().Result == false) return new OkObjectResult(jwts);

            if (bomStockInDetailsId <= 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "BOM Finish Good Stock In Details has not deleted.", false);
                return new OkObjectResult(jwt);
            }
            bool result = await service.DeleteBomFinishGoodStockInDetailsById(user.employeeId.ToString(), bomStockInDetailsId);

            if (result == true)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "BOM Finish Good Stock In Details has deleted successfully.", true);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "BOM Finish Good Stock In Details has not deleted.", false);
                return new OkObjectResult(jwt);
            }
        }

        #endregion

        #region BOM Report

        [HttpGet("GetBomFinishGoodStockInReportDataById")]
        public async Task<IActionResult> GetBomFinishGoodStockInReportDataById(int? bomStockInId)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            var datajson = await service.GetBomFinishGoodStockInReportDataById(bomStockInId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
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