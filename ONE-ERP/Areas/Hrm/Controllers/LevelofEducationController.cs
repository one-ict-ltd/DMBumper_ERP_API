using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ONEERP.Areas.Hrm.Models;
using ONEERP.Data.Entity;
using ONEERP.ERPService.AuthService.Interfaces;
using ONEERP.ERPServices.Hrm.EmployeesInfo.Interfaces;
using ONEERP.Helpers;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.Hrm.Controllers
{
    [Route("api/[controller]")]
    public class LevelOfEducationController : Controller
    {
        public object jwts;
        ApplicationUser user;
        private IUserInfoes userInfoes;
        private IHrmMasterService hrmMasterService;
        public LevelOfEducationController(IUserInfoes _userInfoes, IHrmMasterService employeeInfoService)
        {
            userInfoes = _userInfoes;
            hrmMasterService = employeeInfoService;
            jwts = new object();
            user = new ApplicationUser();
        }

        #region Level of education

        [HttpPost("SaveLevelOfEducation")]
        public async Task<IActionResult> SaveLevelOfEducation([FromBody] LevelOfEducationViewModel model)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            bool result = await hrmMasterService.SaveLevelOfEducation(user.employeeId.ToString(), model);
            if (result == true)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Level of Education created successfully.", true);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Level of education has not created successfully.", false);
                return new OkObjectResult(jwt);
            }
        }
        
        [HttpGet("GetLevelOfEducationById")]
        public async Task<IActionResult> GetLevelOfEducationById(int? LevelOfEducationId)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            var datajson = await hrmMasterService.GetLevelOfEducationById(LevelOfEducationId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }
        
        [HttpPost("DeleteLevelOfEducationById")]
        public async Task<IActionResult> DeleteLevelOfEducationById([FromBody] int LevelOfEducationId)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            bool result = await hrmMasterService.DeleteLevelOfEducationById(user.employeeId.ToString(), LevelOfEducationId);
            if (result == true)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Level of education has deleted successfully.", true);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Level of education has not deleted.", false);
                return new OkObjectResult(jwt);
            }
        }



        #endregion

        

        #region Authentication

        public async Task<bool> Authentication()
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
