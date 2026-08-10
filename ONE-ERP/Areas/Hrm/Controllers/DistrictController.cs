using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ONEERP.Areas.Hrm.Models;
using ONEERP.ERPService.AuthService.Interfaces;
using ONEERP.ERPServices.Hrm.EmployeesInfo.Interfaces;
using ONEERP.Helpers;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.Hrm.Controllers
{
    [Route("api/[controller]")]
    public class DistrictController : Controller
    {
        private IUserInfoes userInfoes;
        private IHrmMasterService hrmMasterService;

        public DistrictController(IUserInfoes userInfoes, IHrmMasterService employeeInfoService)
        {
            this.userInfoes = userInfoes;
            this.hrmMasterService = employeeInfoService;
        }

        #region District --------

        [HttpPost("setDistrict")]
        public async Task<IActionResult> SetDistrict([FromBody] DistrictViewModel model)
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

            bool result = await hrmMasterService.SaveDistrict(user.employeeId.ToString(), model);
            if (result == true)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Employee created successfully.", true);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Employee has not created successfully.", false);
                return new OkObjectResult(jwt);
            }
        }

        [HttpGet("getDistrict")]
        public async Task<IActionResult> GetDistrict(int divisionId)
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

            var datajson = await hrmMasterService.GetDistrictById(divisionId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpPost("deleteDistrict")]
        public async Task<IActionResult> deleteDistrict([FromBody] DistrictViewModel model)
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
            bool result = await hrmMasterService.DeleteDistrictById(user.employeeId.ToString(), (int)model.districtsId);
            if (result == true)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Employee has deleted successfully.", true);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Employee has not deleted.", false);
                return new OkObjectResult(jwt);
            }
        }

        #endregion



    }
}
