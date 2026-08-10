using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ONEERP.Data.Entity;
using ONEERP.ERPService.AuthService.Interfaces;
using ONEERP.ERPServices.AuthService.Interfaces;
using ONEERP.ERPServices.FieldForceTracking.Interfaces;
using ONEERP.ERPServices.Hrm.EmployeesInfo.Interfaces;
using ONEERP.Helpers;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ONEERP.Areas.FieldForceTracking.Controllers
{
    [Route("api/[controller]")]
    public class MarketController : Controller
    {
        private  UserManager<ApplicationUser> _userManager;
        private  IJwtFactoryService _jwtFactory;
        private  IUserInfoes userInfoes;
        private readonly  IDoctorService _doctorService;
        private readonly IEmployeeService employeeService;
        //private  JwtIssuerOptions _jwtOptions;
        private  RoleManager<ApplicationRole> _roleManager;

        public MarketController(UserManager<ApplicationUser> userManager, IEmployeeService employeeService, RoleManager<ApplicationRole> roleManager, IJwtFactoryService jwtFactory, IUserInfoes userInfoes, IDoctorService doctorService)
        {
            _userManager = userManager;
            _jwtFactory = jwtFactory;
            
            _roleManager = roleManager;
            this.userInfoes = userInfoes;
            this._doctorService = doctorService;
            this.employeeService = employeeService;

        }

        [HttpGet]
        public async Task<IActionResult> GetMarket()
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
            var jsonToken = handler.ReadToken(stream);

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
            //DateTime ValidTo = jsonToken.ValidTo;
            var doctorlist = await _doctorService.MarketListAPIViewModels(jti);
            if (doctorlist.Count() > 0)
            {
                var jwt = await Tokens.MarketlistSuccessJwt(doctorlist.ToList(), new JsonSerializerSettings { Formatting = Formatting.Indented });

                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.MarketlistfailJwt(new JsonSerializerSettings { Formatting = Formatting.Indented });

                return new OkObjectResult(jwt);
            }
            // int cnic = jsonData.enad_list[0].et_cnic;



        }


        [HttpGet("GetListMarketForPlan")]
        public async Task<IActionResult> GetListMarketForPlan(DateTime date)
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
            var jsonToken = handler.ReadToken(stream);

            var tokenS = handler.ReadToken(stream) as JwtSecurityToken;
            var jti = tokenS.Claims.First(claim => claim.Type == "Id").Value;
            //DateTime ValidTo = jsonToken.ValidTo;
            var doctorlist = await _doctorService.MarketListAPIPlanViewModels(jti,Convert.ToDateTime(date).ToString("yyyyMMdd"));
            if (doctorlist.Count() > 0)
            {
                var jwt = await Tokens.MarketlistPlanSuccessJwt(doctorlist.ToList(), new JsonSerializerSettings { Formatting = Formatting.Indented });

                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.MarketlistfailJwt(new JsonSerializerSettings { Formatting = Formatting.Indented });

                return new OkObjectResult(jwt);
            }
            // int cnic = jsonData.enad_list[0].et_cnic;



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
