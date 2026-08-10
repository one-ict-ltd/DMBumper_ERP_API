using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ONEERP.Areas.Auth.Models;
using ONEERP.Areas.FieldForceTracking.Models;
using ONEERP.Data.Entity;
using ONEERP.ERPService.AuthService.Interfaces;
using ONEERP.ERPServices.AuthService.Interfaces;
using ONEERP.ERPServices.FieldForceTracking.Interfaces;
using ONEERP.Helpers;
using ONEERP.Helpers.Errors;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ONEICT.Areas.Auth.Controllers
{
    //[EnableCors]
    [Route("api/[controller]")]
    public class LoginAppController : Controller
    {
        private  UserManager<ApplicationUser> _userManager;
        private  IJwtFactoryService _jwtFactory;
        private  IUserInfoes userInfoes;
        private  RoleManager<ApplicationRole> _roleManager;
        private readonly IEmployeeService employeeService;

        public LoginAppController(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager, IJwtFactoryService jwtFactory, IUserInfoes userInfoes, IEmployeeService employeeService)
        {
            _userManager = userManager;
            _jwtFactory = jwtFactory;            
            _roleManager = roleManager;
            this.userInfoes = userInfoes;
            this.employeeService = employeeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetDoctor([FromBody] DoctorListParamViewModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var stream = model.auth_token;
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(stream);
            var tokenS = handler.ReadToken(stream) as JwtSecurityToken;
            return BadRequest(Errors.AddErrorToModelState("", "User not found ,Your credential not match, Please try again.", ModelState));
        }

        [HttpPost]
        public async Task<IActionResult> PostUser([FromBody] LogInViewModel model)
        {           
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var userInfos = await userInfoes.GetUserInfoByUser(model.Name);

            if (userInfos != null)
            {
                var user = await _userManager.FindByNameAsync(userInfos.UserName);
                var identity = await GetClaimsIdentity(user.UserName, model.Password);
                if (user != null && (await _userManager.CheckPasswordAsync(user, model.Password)))
                {
                    //var profile = await userInfoes.GetUserprofileInfoByUser(userInfos.UserName);
                    
                    var profile = await userInfoes.AspNetUsersProfileViewModel(userInfos.UserName);
                    if(profile.deviceNo !="" && profile.deviceNo != model.deviceNo)
                    {
                        var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "You are logged in another device.!", false);
                        return new OkObjectResult(jwt);
                    }
                    else
                    {
                        var roles = await _userManager.GetRolesAsync(user);
                        //string id = await personalInfoService.GetEmployeeIDByAuthID(user.Id);
                        //var response = new
                        //{
                        //    //  id = id,
                        //    auth_token = await _jwtFactory.GenerateToken(user.UserName, "", roles)
                        //};

                        //var jwt = JsonConvert.SerializeObject(response);
                        var jwt = await Tokens.GenerateJwt(identity, _jwtFactory, userInfos.UserName, profile, new JsonSerializerSettings { Formatting = Formatting.Indented });
                        JObject jObject = JObject.Parse(jwt);
                        string jUser = jObject["auth_token"].ToString();
                        await userInfoes.userlogininfo(model.Name, model.Latitude, model.Longitude, model.Address, 1, jUser, model.deviceNo);
                        //JObject jObject = JObject.Parse(jwt);
                        //string jUser = jObject["auth_token"].ToString();
                        //var data = _userManager.VerifyUserTokenAsync(user, "Jwt", "Login", jUser);
                        return new OkObjectResult(jwt);
                    }
                    
                }
                else 
                {
                    var jwt = await Tokens.GenerateJwtFail(new JsonSerializerSettings { Formatting = Formatting.Indented });
                    return new OkObjectResult(jwt);
                }
            }
            else
            {
                var jwt = await Tokens.GenerateJwtFail(new JsonSerializerSettings { Formatting = Formatting.Indented });
                return new OkObjectResult(jwt);
            }   
        }

        [HttpPost("LogoutUser")]
        public async Task<IActionResult> LogoutUser([FromBody] LogOutViewModel model)
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
            // var jsonToken = handler.ReadToken(stream);

            var tokenS = handler.ReadToken(stream) as JwtSecurityToken;
     
            var jti = tokenS.Claims.First(claim => claim.Type == "Id").Value;
            var user = await userInfoes.GetUserBasicInfoesbyId(jti);
            var employee = await employeeService.GetEmployeeLoadViewModels();
            employee = employee.Where(x => x.employeeNo == user.UserName).ToList();
            if (employee?.FirstOrDefault()?.Token != uid && employee.Count() != 0)
            {
                bool status = false;
                string actionresult = "Invalid Token.";
                var jwts = await Tokens.changePasswordJwt(status, actionresult, new JsonSerializerSettings { Formatting = Formatting.Indented });

                return new OkObjectResult(jwts);

            }
            await userInfoes.userlogininfo(user.UserName, model.Latitude, model.Longitude, model.Address, 0,"", "");
           var data= _userManager.VerifyUserTokenAsync(user, "", "", uid);
            var jwt = await Tokens.GenerateoutJwt(new JsonSerializerSettings { Formatting = Formatting.Indented });
    
            return new OkObjectResult(jwt);


            //return BadRequest(Errors.AddErrorToModelState("", "User not found ,Your credential not match, Please try again.", ModelState));
        } 
        [HttpPost("ConnectionUser")]
        public async Task<IActionResult> ConnectionUser([FromBody] ConnectionViewModel model)
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
            // var jsonToken = handler.ReadToken(stream);

            var tokenS = handler.ReadToken(stream) as JwtSecurityToken;
            var jti = tokenS.Claims.First(claim => claim.Type == "Id").Value;
            var user = await userInfoes.GetUserBasicInfoesbyId(jti);

            await userInfoes.userconnectioninfo(user.UserName, model.Date, model.Time, model.IsLocation,model.IsDataConnected);

            var jwt = await Tokens.GenerateconnectionJwt(new JsonSerializerSettings { Formatting = Formatting.Indented });

            return new OkObjectResult(jwt);


            //return BadRequest(Errors.AddErrorToModelState("", "User not found ,Your credential not match, Please try again.", ModelState));
        }
        private async Task<ClaimsIdentity> GetClaimsIdentity(string userName, string password)
        {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
                return await Task.FromResult<ClaimsIdentity>(null);
            //var user = await _userManager.FindByNameAsync(userName);
            //var userRoles = await _userManager.GetRolesAsync(user);
            // get the user to verifty
            var userToVerify = await _userManager.FindByNameAsync(userName);

            if (userToVerify == null) return await Task.FromResult<ClaimsIdentity>(null);
            var user = await _userManager.FindByNameAsync(userName);

            var userRoles = await _userManager.GetRolesAsync(user);

            var claims = new List<Claim>(new[] {
                new Claim("Id", user.Id)
            });

            //add roles of user to the claim
            foreach (var roleName in userRoles)
            {
                var role = await _roleManager.FindByNameAsync(roleName);
                if (role != null)
                {
                    var roleClaim = new Claim("Rol", role.Name);
                    claims.Add(roleClaim);
                }
            }

           // return new ClaimsIdentity(claims);
            // check the credentials
            if (await _userManager.CheckPasswordAsync(userToVerify, password))
            {
                return await Task.FromResult<ClaimsIdentity>(new ClaimsIdentity(claims));
            }

            // Credentials are invalid, or account doesn't exist
            return await Task.FromResult<ClaimsIdentity>(null);
        }


    }
}
