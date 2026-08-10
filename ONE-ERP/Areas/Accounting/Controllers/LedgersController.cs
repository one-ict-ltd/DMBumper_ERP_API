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
    [Route("api/[controller]")]
    //[Authorize]
    //[Area("Accounting")]

    public class LedgersController : Controller
    {
        private IUserInfoes userInfoes;
        private ILedgersService ledgersService;
        private IAccountGroupService accountGroupService;

        public LedgersController(IUserInfoes userInfoes, ILedgersService ledgersService, IAccountGroupService accountGroupService)
        {
            this.userInfoes = userInfoes;
            this.ledgersService = ledgersService;
            this.accountGroupService = accountGroupService;
        }

        //[Route("api/Ledgers/GetLedgersList/")]
        //[HttpGet]
        //[AllowAnonymous]
        //public async Task<IActionResult> GetLedgersList()
        //{
        //    return Json(await ledgersService.GetLedgersList());
        //}

        [HttpPost("setledgers")]
        public async Task<IActionResult> setledgers([FromBody] LedgersViewModel model)
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

            if (model.accountName == null)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Ledger has not created successfully.", false);
                return new OkObjectResult(jwt);
            }
            //var ledgerdata = await ledgersService.GetLedgersList();
            //string maxcode = "";
            //if (model.ledgerId == 0)
            //{
            //    maxcode = ledgerdata.Where(x => x.accountGroupId == model.accountGroupId && x.isActive == true).OrderByDescending(x => x.ledgerId).Select(x => x.accountCode).FirstOrDefault();
            //    if (maxcode == null)
            //    {
            //        maxcode = "000";
            //    }
            //    else
            //    {
            //        maxcode = maxcode.Substring(3, maxcode.Length - 3);
            //    }
            //    var accountgroup = await accountGroupService.GetAccountGroupById((int)model.accountGroupId);
            //    model.accountCode = accountgroup.groupCode.ToString() + "" + (Convert.ToInt32(maxcode) + 1).ToString("000");
            //}
            //else
            //{
            //    maxcode = ledgerdata.Where(x => x.accountGroupId == model.accountGroupId && x.ledgerId != model.ledgerId && x.isActive == true).OrderByDescending(x => Convert.ToInt32(x.accountCode)).Select(x => x.accountCode).FirstOrDefault();
            //    if (maxcode == null)
            //    {
            //        maxcode = "000";
            //    }
            //    else
            //    {
            //        maxcode = maxcode.Substring(3, maxcode.Length - 3);
            //    }
            //    var accountgroup = await accountGroupService.GetAccountGroupById((int)model.accountGroupId);
            //    model.accountCode = accountgroup.groupCode.ToString() + "" + (Convert.ToInt32(maxcode) + 1).ToString("000");
            //}

            bool result = await ledgersService.SaveLedgers(user.employeeId.ToString(), model);
            if (result == true)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Ledger has created successfully.", true);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Ledger has not created successfully.", false);
                return new OkObjectResult(jwt);
            }
        }

        [HttpGet("getledger")]
        public async Task<IActionResult> getledger(int ledgerId, int accountGroupId, int groupNatureId, int companyId, int sbuId, int ledgerTypeId)
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

            var datajson = await ledgersService.GetLedgersByIdJsonwithemp(ledgerId, accountGroupId, groupNatureId, companyId, sbuId, ledgerTypeId,(int) user.employeeId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpGet("getDuplicateLedger")]
        public async Task<IActionResult> getDuplicateLedger(int ledgerId, string accountName)
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

            var datajson = await ledgersService.GetDuplicateLedger(ledgerId, accountName);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpGet("getCOA")]
        public async Task<IActionResult> getCOA(int? companyId = 0, int? groupNatureId = 0, int? accountGroupId = 0, int? accountSubGroupId = 0)
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

            var datajson = await ledgersService.GetCOAJson(companyId, groupNatureId, accountGroupId, accountSubGroupId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpPost("deleteledger")]
        public async Task<IActionResult> deleteledger([FromBody] LedgersViewModel model)
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

            if (model.ledgerId <= 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Ledger has not deleted.", false);
                return new OkObjectResult(jwt);
            }
            bool result = await ledgersService.DeleteLedgersById(user.employeeId.ToString(), (int)model.ledgerId);
            if (result == true)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Ledger has deleted successfully.", true);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Ledger has not deleted.", false);
                return new OkObjectResult(jwt);
            }
        }

        [HttpGet("GetLedgersForVoucherCreate")]
        public async Task<IActionResult> GetLedgersForVoucherCreate(int companyId, int sbuId)
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

            var datajson = await ledgersService.GetLedgersForVoucherCreateWithemp(companyId, sbuId,(int) user.employeeId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpGet("GetAutoLedgerCode")]
        public async Task<IActionResult> GetAutoLedgerCode(int accountGroupId)
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

            var datajson = await ledgersService.GetAutoLedgerCode(accountGroupId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }
    }
}
