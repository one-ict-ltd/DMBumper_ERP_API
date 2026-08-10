using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ONEERP.Areas.Auth.Models;
using ONEERP.Data.Entity;
using ONEERP.ERPService.AuthService.Interfaces;
using ONEERP.ERPServices.AuthService.Interfaces;
using ONEERP.Helpers;

namespace ONEERP.Areas.Auth.Controllers
{
    [Route("api/[controller]")]

    public class LoginController : Controller
    {
        object jwts;
        ApplicationUser user;

        private UserManager<ApplicationUser> _userManager;
        private IJwtFactoryService _jwtFactory;
        private IUserInfoes userInfoes;
        //private  JwtIssuerOptions _jwtOptions;
        private RoleManager<ApplicationRole> _roleManager;
        public LoginController(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager, IJwtFactoryService jwtFactory, IUserInfoes userInfoes)
        {
            jwts = new object();
            user = new ApplicationUser();

            _userManager = userManager;
            _jwtFactory = jwtFactory;

            _roleManager = roleManager;
            this.userInfoes = userInfoes;
        }
        [HttpPost]
        public async Task<IActionResult> PostUser([FromBody] LogInViewModel model)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);

                var userInfos = await userInfoes.GetUserInfoByUser(model.Name);

                if (userInfos != null)
                {
                    var comModel = await userInfoes.GetCompanyById(userInfos.companyId);
                    var isValidLicense = true;//await userInfoes.GetLicenseStaus(comModel.companyName);

                    var user = await _userManager.FindByNameAsync(userInfos.UserName);
                    var identity = await GetClaimsIdentity(user.UserName, model.Password);

                    if (user != null && (await _userManager.CheckPasswordAsync(user, model.Password)) && isValidLicense)
                    {
                        var profile = await userInfoes.GetUserProfileJson(userInfos.UserName);

                        var passwordIsValid = await userInfoes.CheckPasswordValidity(userInfos.PassExpiredAt);

                        if (!passwordIsValid)
                        {
                            var jwtPasswordExpired = await Tokens.GenerateJwtPasswordExpired(identity, _jwtFactory, userInfos.UserName, new JsonSerializerSettings { Formatting = Formatting.Indented });
                            JObject objObject = JObject.Parse(jwtPasswordExpired);
                            string token = objObject["token"].ToString();
                            await userInfoes.userlogininfo(model.Name, 1, token);
                            return new OkObjectResult(jwtPasswordExpired);
                        }

                        if (await userInfoes.IsDummyPassword(model.Password))
                        {
                            var jwtdummyPassword = await Tokens.GenerateJwtDummyPassword(identity, _jwtFactory, userInfos.UserName, new JsonSerializerSettings { Formatting = Formatting.Indented });
                            JObject objObject = JObject.Parse(jwtdummyPassword);
                            string token = objObject["token"].ToString();
                            await userInfoes.userlogininfo(model.Name, 1, token);
                            return new OkObjectResult(jwtdummyPassword);
                        }
                        //  var profile = await userInfoes.AspNetUsersProfileViewModel(userInfos.UserName);
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
                        string milisecond = jObject["expires_in"].ToString();
                        HttpContext.Session.SetString("JWToken", jUser);
                        var expireDate = DateTime.Now.Add(TimeSpan.FromMilliseconds(Convert.ToDouble(milisecond)));
                        HttpContext.Session.SetString("JWTExpire", expireDate.ToString());
                        await userInfoes.userlogininfo(model.Name, 1, jUser);
                        //JObject jObject = JObject.Parse(jwt);
                        //string jUser = jObject["auth_token"].ToString();
                        //var data = _userManager.VerifyUserTokenAsync(user, "Jwt", "Login", jUser);
                        return new OkObjectResult(jwt);

                    }
                    else
                    {
                        if (isValidLicense)
                        {
                            var jwt = await Tokens.GenerateJwtFail(new JsonSerializerSettings { Formatting = Formatting.Indented });
                            return new OkObjectResult(jwt);
                        }
                        else
                        {
                            var jwt = await Tokens.GenerateJwtLicenseFail();
                            return new OkObjectResult(jwt);
                        }
                    }
                }
                else
                {
                    var jwt = await Tokens.GenerateJwtFail(new JsonSerializerSettings { Formatting = Formatting.Indented });

                    return new OkObjectResult(jwt);
                }


                //return BadRequest(Errors.AddErrorToModelState("", "User not found ,Your credential not match, Please try again.", ModelState));
            }
            catch (Exception ex)
            {

                throw;
            }

        }
        [HttpPost("LogoutUser")]
        public async Task<IActionResult> LogoutUser([FromBody] LogOutViewModel model)
        {
            var uid = Request.Headers["auth_token"];
            // await userInfoes.userlogininfo("admin@email.com", 1, uid);
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

            if (user == null)
            {
                bool status = false;
                string actionresult = "Invalid User.";
                var jwts = await Tokens.changePasswordJwt(status, actionresult, new JsonSerializerSettings { Formatting = Formatting.Indented });
                var rest = await userInfoes.userlogininfo(user.UserName, 0, "");

                return new OkObjectResult(jwts);

            }
            if (user.token != uid && user.Id != null)
            {
                bool status = false;
                string actionresult = "Invalid Token.";
                var jwts = await Tokens.changePasswordJwt(status, actionresult, new JsonSerializerSettings { Formatting = Formatting.Indented });
                var rest = await userInfoes.userlogininfo(user.UserName, 0, "");

                return new OkObjectResult(jwts);

            }

            //var JWToken = HttpContext.Session.GetString("JWToken");
            //if (JWToken != uid)
            //{
            //    bool status = false;
            //    string actionresult = "Invalid Token.";
            //    var jwts = await Tokens.changePasswordJwt(status, actionresult, new JsonSerializerSettings { Formatting = Formatting.Indented });

            //    return new OkObjectResult(jwts);

            //}
            HttpContext.Session.Clear();
            var res = await userInfoes.userlogininfo(user.UserName, 0, "");
            var data = _userManager.VerifyUserTokenAsync(user, "", "", uid);
            var jwt = await Tokens.GenerateoutJwt(new JsonSerializerSettings { Formatting = Formatting.Indented });

            return new OkObjectResult(jwt);


            //return BadRequest(Errors.AddErrorToModelState("", "User not found ,Your credential not match, Please try again.", ModelState));
        }

        [HttpPost("UpdatePassword")]
        public async Task<IActionResult> UpdatePassword([FromBody] ChangePsswordViewModel model)
        {
            var uid = Request.Headers["tat"];
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

            if (user.token != uid && user.Id != null)
            {
                bool status = false;
                string actionresult = "Invalid Token.";
                var jwts = await Tokens.changePasswordJwt(status, actionresult, new JsonSerializerSettings { Formatting = Formatting.Indented });

                return new OkObjectResult(jwts);

            }

            if (await _userManager.CheckPasswordAsync(user, model.Password))
            {
                bool status = false;
                string actionresult = "You provide old password.";
                var jwts = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, actionresult, status);

                return new OkObjectResult(jwts);
            }

            await _userManager.RemovePasswordAsync(user);
            var resetResult = await _userManager.AddPasswordAsync(user, model.Password);
            if (resetResult.Succeeded)
            {
                await userInfoes.UpdatePasswordValidity(user.UserName);

                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Users Password Reset successfully.", true);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Users has not Updated successfully.", false);
                return new OkObjectResult(jwt);
            }

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

            if (await _userManager.CheckPasswordAsync(userToVerify, password))
            {
                return await Task.FromResult<ClaimsIdentity>(new ClaimsIdentity(claims));
            }

            return await Task.FromResult<ClaimsIdentity>(null);
        }


        #region Validate Token
        [HttpPost("ValidateTheToken")]
        public async Task<IActionResult> ValidateTheToken()
        {
            return new OkObjectResult(await Tokens.SetJwtTokenStatus(AuthenticationStatus().Result));
        }
        private async Task<bool> AuthenticationStatus()
        {
            #region common
            var uid = Request.Headers["auth_token"];
            if (string.IsNullOrEmpty(uid)) return true;
            if (uid.Count() == 0) return false;
            var stream = uid;
            var handler = new JwtSecurityTokenHandler();
            var tokenS = handler.ReadToken(stream) as JwtSecurityToken;
            var jti = tokenS.Claims.First(claim => claim.Type == "Id").Value;
            user = await userInfoes.GetUserBasicInfoesbyId(jti);
            if (user == null || user.token != uid) return false;
            return true;
            #endregion
        }
        #endregion Validate Token
    }
}
