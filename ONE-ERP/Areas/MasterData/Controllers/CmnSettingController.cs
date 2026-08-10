using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ONEERP.Areas.MasterData.Models;
using ONEERP.Data.Entity;
using ONEERP.ERPService.AuthService.Interfaces;
using ONEERP.ERPServices.MasterData.Interfaces;
using ONEERP.Helpers;

namespace ONEERP.Areas.MasterData.Controllers
{
    [Route("api/[controller]")]
    public class CmnSettingController : Controller
    {
        object jwts;
        ApplicationUser user;
        private readonly IUserInfoes userInfoes;
        private readonly ICmnSettingService service;
        public CmnSettingController(IUserInfoes _userInfoes, ICmnSettingService _service)
        {
            userInfoes = _userInfoes;
            service = _service;
            jwts = new object();
            user = new ApplicationUser();
        }

        #region MenuWiseTransactionDateUnlock

        [HttpGet("GetMenuWiseTransactionDateUnlockList")]
        public async Task<IActionResult> GetMenuWiseTransactionDateUnlockList(int masterId)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);
            var datajson = await service.GetMenuWiseTransactionDateUnlockList(user.employeeId, masterId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }
        
        [HttpGet("GetMenuListForTransactionDateUnlock")]
        public async Task<IActionResult> GetMenuListForTransactionDateUnlock(int masterId)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);
            var datajson = await service.GetMenuListForTransactionDateUnlock(user.employeeId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }
        
        [HttpPost("SaveMenuWiseTransactionDateUnlock")]
        public async Task<IActionResult> SaveMenuWiseTransactionDateUnlock([FromBody] MenuWiseTransactionDateUnlockViewModel model)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);
            var res = await service.SaveMenuWiseTransactionDateUnlock(user.employeeId, model);
            if (res > 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Saved successfully.", true);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Data saved process failed!", false);
                return new OkObjectResult(jwt);
            }
        }
        [HttpPost("DeleteMenuWiseTransactionDateUnlock")]
        public async Task<IActionResult> DeleteMenuWiseTransactionDateUnlock([FromBody] int masterId)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);
            var res = await service.DeleteMenuWiseTransactionDateUnlock(user.employeeId, masterId);
            if (res > 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Deleted successfully.", true);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Data delete process failed!", false);
                return new OkObjectResult(jwt);
            }
        }

        #endregion

        #region Auth
        async Task<bool> Authentication()
        {
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
        }
        #endregion
    }
}