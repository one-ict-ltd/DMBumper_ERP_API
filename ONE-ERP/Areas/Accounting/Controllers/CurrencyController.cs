using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ONEERP.Areas.Accounting.Models;
using ONEERP.ERPService.AuthService.Interfaces;
using ONEERP.ERPServices.Accounting.MasterData.Interfaces;
using ONEERP.ERPServices.AuthService.Interfaces;
using ONEERP.Helpers;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;



namespace ONEERP.Areas.Accounting.Controllers
{
    [Route("api/[controller]")]
    public class CurrencyController : Controller    {
       
        private IUserInfoes userInfoes;       
        private ICurrencyService currencyService;

        private readonly TokenAuthenticator _authenticator;
        private readonly IJwtFactoryService _jwtFactoryService;
        public CurrencyController( IUserInfoes userInfoes,  ICurrencyService currencyService 
           , IJwtFactoryService jwtFactoryService)
        {          
            this.userInfoes = userInfoes;
            _authenticator = new TokenAuthenticator(userInfoes);
            _jwtFactoryService = jwtFactoryService;
            this.currencyService = currencyService;
        }

        [HttpPost("setcurrency")]
        public async Task<IActionResult> setcurrency([FromBody] CurrencyViewModel model)
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
            
            if (model.currencyName == null)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented },"Currency has not created successfully.",false);
                return new OkObjectResult(jwt);
            }
            bool result = await currencyService.SaveCurrency(user.employeeId.ToString(),model);        

            if (result == true)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Currency has created successfully.", true);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Currency has not created successfully.", false);
                return new OkObjectResult(jwt);
            }
        }

        [HttpGet("getcurrency")]
        public async Task<IActionResult> getcurrency(int currencyId)
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
            
            var datajson = await currencyService.GetCurrencyByIdJson(currencyId);           
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);            
        }

        [HttpGet("getDuplicateCurrency")]
        public async Task<IActionResult> getDuplicateCurrency(int currencyId, string currencyName)
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

            var datajson = await currencyService.GetDuplicateCurrency(currencyId, currencyName);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpPost("deletecurrency")]
        public async Task<IActionResult> deletecurrency([FromBody] CurrencyViewModel model)
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
            
            if (model.currencyId <= 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Currency has not deleted.", false);
                return new OkObjectResult(jwt);
            }
            bool result = await currencyService.DeleteCurrencyById(user.employeeId.ToString(), (int)model.currencyId);

            if (result == true)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Currency has deleted successfully.", true);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Currency has not deleted.", false);
                return new OkObjectResult(jwt);
            }
        }

        [HttpGet("getAllActiveInActiveCurrency")]
        public async Task<IActionResult> getAllActiveInActiveCurrency(int currencyId)
        {

            #region common

            var (user, message, isAuthenticUser) = await _jwtFactoryService.AuthenticateRequest(Request.Headers["auth_token"]);
            if (!isAuthenticUser)
            {
                var jwts = await Tokens.changePasswordJwt(isAuthenticUser, message, new JsonSerializerSettings { Formatting = Formatting.Indented });
                return new OkObjectResult(jwts);
            }

            #endregion
            var datajson = await currencyService.GetAllActiveInActiveCurrency(currencyId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }


    }
}
