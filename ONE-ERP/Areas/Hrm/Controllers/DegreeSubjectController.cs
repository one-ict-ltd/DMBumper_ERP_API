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
    public class DegreeSubjectController : Controller
    {
        public object jwts;
        ApplicationUser user;
        private IUserInfoes userInfoes;
        private IHrmMasterService hrmMasterService;
        public DegreeSubjectController(IUserInfoes _userInfoes, IHrmMasterService employeeInfoService)
        {
            userInfoes = _userInfoes;
            hrmMasterService = employeeInfoService;
            jwts = new object();
            user = new ApplicationUser();
        }

        #region DegreeSubject

        [HttpPost("SaveDegreeSubject")]
        public async Task<IActionResult> SaveDegreeSubject([FromBody] DegreeSubjectViewModel model)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            bool result = await hrmMasterService.SaveDegreeSubject(user.employeeId.ToString(), model);
            if (result == true)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "DegreeSubject created successfully.", true);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "DegreeSubject has not created successfully.", false);
                return new OkObjectResult(jwt);
            }
        }
        
        [HttpGet("GetDegreeSubjectById")]
        public async Task<IActionResult> GetDegreeSubjectById(int? degreeSubjectId)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            var datajson = await hrmMasterService.GetDegreeSubjectById(degreeSubjectId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }
        
        [HttpPost("DeleteDegreeSubjectById")]
        public async Task<IActionResult> DeleteDegreeSubjectById([FromBody] int degreeSubjectId)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            bool result = await hrmMasterService.DeleteDegreeSubjectById(user.employeeId.ToString(), degreeSubjectId);
            if (result == true)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "DegreeSubject has deleted successfully.", true);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "DegreeSubject has not deleted.", false);
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
