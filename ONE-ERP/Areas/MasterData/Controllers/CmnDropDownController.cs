using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ONEERP.Areas.MasterData.Models;
using ONEERP.ERPService.AuthService.Interfaces;
using ONEERP.ERPServices.AuthService.Interfaces;
using ONEERP.ERPServices.MasterData.Interfaces;
using ONEERP.Helpers;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.MasterData.Controllers
{
    [Route("api/[controller]")]

    public class CmnDropDownController : Controller
    {
        private IUserInfoes userInfoes;
        private ICmnDropDownService cmnDropDownService;
        private IJwtFactoryService _jwtFactoryService;

        public CmnDropDownController(IUserInfoes userInfoes, ICmnDropDownService cmnDropDownService, IJwtFactoryService jwtFactoryService)
        {
            this.userInfoes = userInfoes;
            this.cmnDropDownService = cmnDropDownService;
            _jwtFactoryService = jwtFactoryService;
        }

        #region DropDown Type

        [HttpPost("setCmnDropDownType")]
        public async Task<IActionResult> setCmnDropDownType([FromBody] CmnDropDownTypeViewModel model)
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
            if (model.dropDownType == null)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Drop down type has not created successfully.", false);
                return new OkObjectResult(jwt);
            }

            bool result = await cmnDropDownService.SaveCmnDropDownType(user.employeeId.ToString(), model);
            if (result == true)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Drop down type has created successfully.", true);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Drop down type has not created successfully.", false);
                return new OkObjectResult(jwt);
            }
        }

        [HttpGet("getCmnDropDownType")]
        public async Task<IActionResult> getCmnDropDownType(int dropDownTypeId)
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

            var datajson = await cmnDropDownService.GetCmnDropDownTypeByIdJson(dropDownTypeId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpPost("deleteCmnDropDownType")]
        public async Task<IActionResult> deleteCmnDropDownType([FromBody] CmnDropDownTypeViewModel model)
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

            if (model.dropDownTypeId <= 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Drop down type has not deleted.", false);
                return new OkObjectResult(jwt);
            }
            bool result = await cmnDropDownService.DeleteCmnDropDownTypeById(user.employeeId.ToString(), (int)model.dropDownTypeId);

            if (result == true)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Drop down type has deleted successfully.", true);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Drop down type has not deleted.", false);
                return new OkObjectResult(jwt);
            }
        }

        #endregion

        #region Drop Down

        [HttpPost("setCmnDropDown")]
        public async Task<IActionResult> setCmnDropDown([FromBody] CmnDropDownViewModel model)
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
            if (model.dropDownValue == null)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Drop down has not created successfully.", false);
                return new OkObjectResult(jwt);
            }

            bool result = await cmnDropDownService.SaveCmnDropDown(user.employeeId.ToString(), model);
            if (result == true)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Drop down has created successfully.", true);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Drop down has not created successfully.", false);
                return new OkObjectResult(jwt);
            }
        }

        [HttpGet("getCmnDropDown")]
        public async Task<IActionResult> getCmnDropDown(int dropDownId, string type)
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

            var datajson = await cmnDropDownService.GetCmnDropDownByIdJson(dropDownId, type);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpPost("deleteCmnDropDown")]
        public async Task<IActionResult> deleteCmnDropDown([FromBody] CmnDropDownViewModel model)
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

            if (model.dropDownId <= 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Drop down has not deleted.", false);
                return new OkObjectResult(jwt);
            }
            bool result = await cmnDropDownService.DeleteCmnDropDownTypeById(user.employeeId.ToString(), (int)model.dropDownId);

            if (result == true)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Drop down has deleted successfully.", true);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Drop down has not deleted.", false);
                return new OkObjectResult(jwt);
            }
        }

        #endregion

        #region Bank

        [HttpGet("GetCmnBank")]
        public async Task<IActionResult> GetCmnBank(int bankId, int bankTypeId)
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

            var datajson = await cmnDropDownService.GetCmnBankByIdJson(bankId, bankTypeId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpGet("GetCmnBankBranch")]
        public async Task<IActionResult> GetCmnBankBranch(int bankId)
        {
            var uid = Request.Headers["auth_token"];
            if (!uid.Any())
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

            if (user == null || user.token != uid)
            {
                bool status = false;
                string actionresult = "Invalid Token.";
                var jwts = await Tokens.changePasswordJwt(status, actionresult, new JsonSerializerSettings { Formatting = Formatting.Indented });
                return new OkObjectResult(jwts);
            }

            var datajson = await cmnDropDownService.GetCmnBankBranchByBankIdJson(bankId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        #endregion



        [HttpGet("GetCmnDepot")]
        public async Task<IActionResult> GetCmnDepot()
        {
            var uid = Request.Headers["auth_token"];
            if (!uid.Any())
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

            if (user == null || user.token != uid)
            {
                bool status = false;
                string actionresult = "Invalid Token.";
                var jwts = await Tokens.changePasswordJwt(status, actionresult, new JsonSerializerSettings { Formatting = Formatting.Indented });
                return new OkObjectResult(jwts);
            }
            var empId = user.employeeId ?? default;
            var datajson = await cmnDropDownService.GetCmnDepotByCodeJson(empId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpGet("getAllTerritoriesHH")]
        public async Task<IActionResult> GetAllTerritoriesHH()
        {
            var uid = Request.Headers["auth_token"];
            var fixedToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJNb3N0YWZhQGVtYWlsLmNvbSIsImp0aSI6IjUwMzhjNzQyLWVkNzAtNDRiNS1hMGI3LTBhMjRlNzFmYWJiYiIsImlhdCI6MTcyNzU4NjEzNSwiSWQiOiIwMjEwNzVEQy0xNjFBLTQ4QTUtODlBMy1FM0NEODQ1OTVFODkiLCJuYmYiOjE3Mjc1ODYxMzQsImV4cCI6MTcyNzU5MzMzNCwiaXNzIjoid2ViQXBpIiwiYXVkIjoiaHR0cDovL2xvY2FsaG9zdDo5MTE1LyJ9.cy7P0ws__zHO2ZuwuDnVmxggrJ1LYM9se81lx3cxCYM";// For Digital Gift Disbursement

            if (uid != fixedToken)
            {
                bool status = false;
                string actionresult = "Invalid Token.";
                var jwts = await Tokens.changePasswordJwt(status, actionresult, new JsonSerializerSettings { Formatting = Formatting.Indented });
                return new OkObjectResult(jwts);
            }
            //var empId = user.employeeId ?? default;
            var datajson = await cmnDropDownService.GetAllTerritoriesHH(1);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        #region Transaction Type

        [HttpGet("GetCmnTransactionType")]
        public async Task<IActionResult> GetCmnTransactionType(int transactionTypeId)
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

            var datajson = await cmnDropDownService.GetCmnTransactionType(transactionTypeId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        #endregion

        [HttpGet("GetCmnOriginCountries")]
        public async Task<IActionResult> GetCmnOriginCountries(int countryId)
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

            var datajson = await cmnDropDownService.GetCmnOriginCountries(countryId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        #region For multiselect drop down
        [HttpGet("GetRegionByZoneCodes")]
        public async Task<IActionResult> GetRegionByZoneCodes(string zoneCodes)
        {
            var (user, message, isAuthenticUser) = await _jwtFactoryService.AuthenticateRequest(Request.Headers["auth_token"]);
            if (!isAuthenticUser)
            {
                var jwts = await Tokens.changePasswordJwt(isAuthenticUser, message, new JsonSerializerSettings { Formatting = Formatting.Indented });
                return new OkObjectResult(jwts);
            }

            var datajson = await cmnDropDownService.GetRegionByZoneCodes(user.employeeId, zoneCodes);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpGet("GetAreaByRegionCodes")]
        public async Task<IActionResult> GetAreaByRegionCodes(string regionCodes)
        {
            var (user, message, isAuthenticUser) = await _jwtFactoryService.AuthenticateRequest(Request.Headers["auth_token"]);
            if (!isAuthenticUser)
            {
                var jwts = await Tokens.changePasswordJwt(isAuthenticUser, message, new JsonSerializerSettings { Formatting = Formatting.Indented });
                return new OkObjectResult(jwts);
            }

            var datajson = await cmnDropDownService.GetAreaByRegionCodes(user.employeeId, regionCodes);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpGet("GetTerritoryByAreaCodes")]
        public async Task<IActionResult> GetTerritoryByAreaCodes(string areaCodes)
        {
            var (user, message, isAuthenticUser) = await _jwtFactoryService.AuthenticateRequest(Request.Headers["auth_token"]);
            if (!isAuthenticUser)
            {
                var jwts = await Tokens.changePasswordJwt(isAuthenticUser, message, new JsonSerializerSettings { Formatting = Formatting.Indented });
                return new OkObjectResult(jwts);
            }

            var datajson = await cmnDropDownService.GetTerritoryByAreaCodes(user.employeeId, areaCodes);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }
        #endregion
    }
}
