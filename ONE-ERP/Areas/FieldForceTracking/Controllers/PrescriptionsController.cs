using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ONEERP.Areas.FieldForceTracking.Models;
using ONEERP.Areas.Hrm.Models;
using ONEERP.Data.Entity;
using ONEERP.ERPService.AuthService.Interfaces;
using ONEERP.ERPServices.FieldForceTracking.Interfaces;
using ONEERP.Helpers;
using ONEERP.Models.Dashboard;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Threading.Tasks;


namespace ONEERP.Areas.FieldForceTracking.Controllers
{
    [Route("api/[controller]")]
    public class PrescriptionsController : Controller
    {
        object jwts;
        ApplicationUser user;

        private IUserInfoes userInfoes;
        private readonly IEmployeeService employeeService;
        private readonly IPrescriptionsService prescriptionsService;

        public PrescriptionsController(IUserInfoes _userInfoes, IEmployeeService _employeeService, IPrescriptionsService _prescriptionsService)
        {
            jwts = new object();
            user = new ApplicationUser();

            userInfoes = _userInfoes;
            employeeService = _employeeService;
            prescriptionsService = _prescriptionsService;
        }

        #region Prescriptios


        [HttpPost("SetPrescriptions")]
        public async Task<IActionResult> SetPrescriptions([FromForm] List<DoctorsPrescriptionsViewModel> models)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            if (models == null || models.Count == 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Prescriptions data not found", false);
                return new OkObjectResult(jwt);
            }

            bool result = await prescriptionsService.SetPrescriptions(user.employeeId, models);

            if (result == true)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Prescriptions has created successfully.", true);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Prescriptions has not created.", false);
                return new OkObjectResult(jwt);
            }
        }

        [HttpGet("GetPrescriptions")]
        public async Task<IActionResult> GetPrescriptions(int? prescriptioID, DateTime? date)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            var datajson = await prescriptionsService.GetPrescriptions(prescriptioID, date);
            var jwt = await Tokens.GetJwt(datajson.data);
            return new OkObjectResult(jwt);
        }

        #endregion

        #region Authentication
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
        #endregion
    }
}
