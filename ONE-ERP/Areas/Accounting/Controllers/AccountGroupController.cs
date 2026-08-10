using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ONEERP.Areas.Accounting.Models;
using ONEERP.ERPService.AuthService.Interfaces;
using ONEERP.ERPServices.Accounting.MasterData.Interfaces;
using ONEERP.Helpers;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.Accounting.Controllers
{
    //[Authorize]
    //[Area("Accounting")]
    [Route("api/[controller]")]

    public class AccountGroupController : Controller
    {       
        private IUserInfoes userInfoes;       
        private IAccountGroupService accountGroupService;
        public AccountGroupController(IUserInfoes userInfoes, IAccountGroupService accountGroupService)
        {            
            this.userInfoes = userInfoes;           
            this.accountGroupService = accountGroupService;
        }
       
        //[Route("api/AccountGroup/GetAccountGroupList/")]
        //[HttpGet]
        //[AllowAnonymous]
        //public async Task<IActionResult> GetAccountGroupList()
        //{
        //    return Json(await accountGroupService.GetAccountGroupList());
        //}    
        

        [HttpPost("setaccountGroup")]
        public async Task<IActionResult> setaccountGroup([FromBody] AccountGroupViewModel model)
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
            
            if (model.groupName == null &&model.groupNatureId==0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented },"Account group has not created successfully.",false);
                return new OkObjectResult(jwt);
            }        
            

            var accountGroupdata = await accountGroupService.GetAccountGroupList();
            string maxcode = "";
            if (model.accountGroupId == 0)
            {
                maxcode = accountGroupdata.Where(x => x.groupNatureId == model.groupNatureId && x.isActive == true).OrderByDescending(x => x.accountGroupId).Select(x => x.groupCode).FirstOrDefault();

                if (maxcode == null)
                {
                    maxcode = "000";
                }
                else
                {
                    maxcode = maxcode.Substring(1, maxcode.Length - 1);
                }
                model.groupCode = model.groupNatureId.ToString() + "" + (Convert.ToInt32(maxcode) + 1).ToString("00");
            }
            else
            {
                maxcode = accountGroupdata.Where(x => x.groupNatureId == model.groupNatureId && x.accountGroupId != model.accountGroupId && x.isActive == true).OrderByDescending(x => Convert.ToInt32(x.groupCode)).Select(x => x.groupCode).FirstOrDefault();
                if (maxcode == null)
                {
                    maxcode = "000";
                }
                else
                {
                    maxcode = maxcode.Substring(1, maxcode.Length - 1);
                }
                model.groupCode = model.groupNatureId.ToString() + "" + (Convert.ToInt32(maxcode) + 1).ToString("00");
                //model.groupCode = accountGroupdata.Where(x => x.accountGroupId == model.accountGroupId).FirstOrDefault().groupCode;
            }

            bool result = await accountGroupService.SaveAccountGroup(user.employeeId.ToString(),model);         

            if (result == true)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Account group has created successfully.", true);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Account group has not created successfully.", false);
                return new OkObjectResult(jwt);
            }         
        }

       
        [HttpGet("getaccountGroup")]
        public async Task<IActionResult> getaccountGroup(int accountGroupId,int groupNatureId)
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
            var datajson = await accountGroupService.GetAccountGroupByIdNatureIdJson(accountGroupId,groupNatureId);          
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);            
        }

        [HttpGet("GetParentChildAccountGroup")]
        public async Task<IActionResult> GetParentChildAccountGroup(int groupNatureId)
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
            var datajson = await accountGroupService.GetParentChildAccountGroup(groupNatureId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }
        
        [HttpGet("GetAccountGroupSubGroup")]
        public async Task<IActionResult> GetAccountGroupSubGroup(int groupNatureId, int accountGroupId)
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
            var datajson = await accountGroupService.GetAccountGroupSubGroup(groupNatureId, accountGroupId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }
        
        [HttpGet("GetSubGroupByAccountGroupIds")]
        public async Task<IActionResult> GetSubGroupByAccountGroupIds(int groupNatureId, string accountGroupIds)
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
            var datajson = await accountGroupService.GetSubGroupByAccountGroupIds(groupNatureId, accountGroupIds);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpGet("AccSpGetAccountLedgerAccessByUser")]
        public async Task<IActionResult> AccSpGetAccountLedgerAccessByUser(int groupNatureId, int accountGroupId, int employeeId)
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
            var datajson = await accountGroupService.AccSpGetAccountLedgerAccessByUser(groupNatureId, accountGroupId, employeeId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpPost("saveAccountWiseLedger")]
        public async Task<IActionResult> saveAccountWiseLedger([FromBody] UserWiseLedgerViewModel model)
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

            bool result = await accountGroupService.SaveUserWiseLedger(user.employeeId.ToString(), model);

            if (result == true)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Account group has created successfully.", true);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Account group has not created successfully.", false);
                return new OkObjectResult(jwt);
            }
        }

        [HttpGet("AccSpGetAccountUserWiseLedger")]
        public async Task<IActionResult> AccSpGetAccountUserWiseLedger(int employeeId)
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
            var datajson = await accountGroupService.AccSpGetAccountUserWiseLedger(employeeId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }


        [HttpGet("getDuplicateAccountGroup")]
        public async Task<IActionResult> getDuplicateAccountGroup(int accountGroupId, string groupName)
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

            var datajson = await accountGroupService.GetDuplicateAccountGroup(accountGroupId, groupName);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpPost("deleteaccountGroup")]
        public async Task<IActionResult> deleteaccountGroup([FromBody] AccountGroupViewModel model)
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
            
            if (model.accountGroupId <= 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Account group has not deleted.", false);
                return new OkObjectResult(jwt);
            }
            bool result = await accountGroupService.DeleteAccountGroupById(user.employeeId.ToString(), (int)model.accountGroupId);
            if (result == true)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Account group has deleted successfully.", true);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Account group has not deleted.", false);
                return new OkObjectResult(jwt);
            }
        }


    }
}
