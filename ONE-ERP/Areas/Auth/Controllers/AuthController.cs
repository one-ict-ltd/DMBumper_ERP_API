using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ONEERP.Areas.Auth.Models;
using ONEERP.Data.Entity;
using ONEERP.ERPService.AuthService.Interfaces;
using ONEERP.ERPServices.AuthService.Interfaces;
using ONEERP.Helpers;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace ONEICT.Areas.Auth.Controllers
{
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private  UserManager<ApplicationUser> _userManager;
        private readonly IJwtFactoryService _jwtFactory;
        private readonly IUserInfoes userInfoes;

        public AuthController(UserManager<ApplicationUser> userManager, IJwtFactoryService jwtFactory, IUserInfoes userInfoes)
        {
            _userManager = userManager;
            _jwtFactory = jwtFactory;
            this.userInfoes = userInfoes;
            
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Json(_userManager);
        }

        private IActionResult Json(UserManager<ApplicationUser> userManager)
        {
            throw new NotImplementedException();
        }
       
        //[HttpPost]       
        //public async Task<IActionResult> LogIn([FromBody] LogInViewModel model)
        //{ 
        //    if (!ModelState.IsValid) return BadRequest(ModelState);

        //    var user = await _userManager.FindByNameAsync(model.Name);

        //    if (user != null && (await _userManager.CheckPasswordAsync(user, model.Password)))
        //    {
        //        var roles = await _userManager.GetRolesAsync(user);
        //        //string id = await personalInfoService.GetEmployeeIDByAuthID(user.Id);
        //        var response = new
        //        {
        //            //  id = id,
        //            auth_token = await _jwtFactory.GenerateToken(user.UserName, "", roles)
        //        };

        //        var jwt = JsonConvert.SerializeObject(response);
        //        return new OkObjectResult(jwt);

        //    }

        //    return BadRequest(Errors.AddErrorToModelState("login_failure", "Invalid username or password.", ModelState));
        //}

        [HttpPost("changePasswordAPI")]
        public async Task<IActionResult> changePasswordAPI([FromBody] ChangePasswordParamModel model)
        {
            var uid = Request.Headers["auth_token"];
            if (uid.Count() == 0 )
            {
                bool status = false;
                string actionresult = "Invalid Token.";
                var jwt = await Tokens.changePasswordJwt(status, actionresult, new JsonSerializerSettings { Formatting = Formatting.Indented });
                return new OkObjectResult(jwt);
            }
            var stream = uid;
            var handler = new JwtSecurityTokenHandler();
            var tokenS = handler.ReadToken(stream) as JwtSecurityToken;
            var jti = tokenS.Claims.First(claim => claim.Type == "Id").Value;
            if (jti == null || jti == "")
            {
                bool status = false;
                string actionresult = "Your Id is wrong.Please contact with admin.";
                var jwt = await Tokens.changePasswordJwt(status, actionresult, new JsonSerializerSettings { Formatting = Formatting.Indented });
                return new OkObjectResult(jwt);
            }
            var user = await userInfoes.GetUserBasicInfoesbyId(jti);
            var userbyname = await _userManager.FindByNameAsync(user.UserName);
            bool result = false;
            result=await _userManager.CheckPasswordAsync(user, model.previousPassword);
            if (result == false)
            {
                bool status = false;
                string actionresult = "Your previous password is wrong.Please contact with admin.";
                var jwt = await Tokens.changePasswordJwt(status, actionresult, new JsonSerializerSettings { Formatting = Formatting.Indented });
                return new OkObjectResult(jwt);
            }
            else
            {
                var data = await _userManager.ChangePasswordAsync(await _userManager.FindByNameAsync(user.UserName), model.previousPassword, model.newPassword);
                string actionresult = data.ToString();
                bool status = true;
                var jwt = await Tokens.changePasswordJwt(status, actionresult, new JsonSerializerSettings { Formatting = Formatting.Indented });
                return new OkObjectResult(jwt);
            }
        }
        
    }
}
