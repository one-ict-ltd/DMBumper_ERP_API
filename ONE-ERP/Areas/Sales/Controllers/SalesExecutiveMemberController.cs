using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ONEERP.Areas.Sales.Models;
using ONEERP.Data.Entity;
using ONEERP.ERPService.AuthService.Interfaces;
using ONEERP.ERPServices.Sales.Interfaces;
using ONEERP.Helpers;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using System.Linq;


namespace ONEERP.Areas.Sales.Controllers
{
    [Route("api/[controller]")]
    public class SalesExecutiveMemberController : Controller
    {
        object jwts;
        ApplicationUser user;
        private IUserInfoes userInfoes;
        private readonly ISalesExecutiveMemberService service;
        public SalesExecutiveMemberController(IUserInfoes _userInfoes, ISalesExecutiveMemberService _service)
        {
            this.userInfoes = _userInfoes;
            this.service = _service;
        }

        [HttpPost("saveExecutiveMember")]
        public async Task<IActionResult> SaveExecutiveWiseProduct([FromBody] List<SalExecutiveTeamViewModel> model)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);
            if (model == null)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Executive Team Member not created.", false);
                return new OkObjectResult(jwt);
            }
            int result = 0;
            result = await service.SaveExecutiveMember(user.employeeId, model);
            if (result == 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Executive Team Member not created.", false);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Executive Team Member created.", true);
                return new OkObjectResult(jwt);
            }
        }

        [HttpGet("GetExecutiveMember")]
        public async Task<IActionResult> GetExecutiveWiseProduct(int? executiveTeamId)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            var datajson = await service.GetExecutiveMember(executiveTeamId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpGet("DeleteExecutiveMember")]
        public async Task<IActionResult> DeleteExecutiveMember(int executiveTeamId)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            bool result = await service.DeleteExecutiveMember(user.employeeId, executiveTeamId);

            if (result == true)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Executive Team Member has deleted successfully.", true);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Executive Team Member has not deleted.", false);
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
