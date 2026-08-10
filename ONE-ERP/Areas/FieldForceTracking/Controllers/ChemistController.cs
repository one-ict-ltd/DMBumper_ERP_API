using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ONEERP.Areas.Accounting.Models;
using ONEERP.Data.Entity;
using ONEERP.ERPService.AuthService.Interfaces;
using ONEERP.ERPServices.FieldForceTracking.Interfaces;
using ONEERP.Helpers;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ONEERP.Areas.FieldForceTracking.Models;

namespace ONEERP.Areas.FieldForceTracking.Controllers
{
    [Route("api/[controller]")]
    public class ChemistController : Controller
    {
        private UserManager<ApplicationUser> _userManager;       
        private IUserInfoes userInfoes;
        private readonly IChemistService _chemistService;        
        private RoleManager<ApplicationRole> _roleManager;
        private readonly IEmployeeService employeeService;

        public ChemistController(UserManager<ApplicationUser> userManager, IEmployeeService employeeService, RoleManager<ApplicationRole> roleManager, IUserInfoes userInfoes, IChemistService chemistService)
        {
            _userManager = userManager;           
            this.employeeService = employeeService;
            _roleManager = roleManager;
            this.userInfoes = userInfoes;
            this._chemistService = chemistService;
        }

       
        [HttpGet("GetChemistList")]
        public async Task<IActionResult> GetChemistList()
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
            var employee = await employeeService.GetEmployeeLoadViewModels();
            employee = employee.Where(x => x.employeeNo == user.UserName).ToList();

            if (employee?.FirstOrDefault()?.Token != uid && employee.Count() != 0)
            {
                bool status = false;
                string actionresult = "Invalid Token.";
                var jwts = await Tokens.changePasswordJwt(status, actionresult, new JsonSerializerSettings { Formatting = Formatting.Indented });
                return new OkObjectResult(jwts);
            }
            var chemistlist = await _chemistService.GetChemistListAPIViewModel(jti);
            if (chemistlist.Count() > 0)
            {
                var jwt = await Tokens.ChemistlistSuccessJwt(chemistlist.ToList(), new JsonSerializerSettings { Formatting = Formatting.Indented });
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.ChemistlistfailJwt(new JsonSerializerSettings { Formatting = Formatting.Indented });
                return new OkObjectResult(jwt);
            }           
        }
       
        [HttpGet("GetChemistListByCode")]
        public async Task<IActionResult> GetChemistListByCode(string code)
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
            var employee = await employeeService.GetEmployeeLoadViewModels();
            employee = employee.Where(x => x.employeeNo == user.UserName).ToList();

            if (employee?.FirstOrDefault()?.Token != uid && employee.Count() != 0)
            {
                bool status = false;
                string actionresult = "Invalid Token.";
                var jwts = await Tokens.changePasswordJwt(status, actionresult, new JsonSerializerSettings { Formatting = Formatting.Indented });
                return new OkObjectResult(jwts);
            }
            var chemistlist = await _chemistService.GetChemistListAPIViewModelBycode(jti,code);
            if (chemistlist.Count() > 0)
            {
                var jwt = await Tokens.ChemistlistSuccessJwt(chemistlist.ToList(), new JsonSerializerSettings { Formatting = Formatting.Indented });
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.ChemistlistfailJwt(new JsonSerializerSettings { Formatting = Formatting.Indented });
                return new OkObjectResult(jwt);
            }           
        }

       
        [HttpGet("GetChemistListJson")]
        public async Task<IActionResult> GetChemistListJson(string employeeNo)
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
            var employee = await employeeService.GetEmployeeLoadViewModels();
            employee = employee.Where(x => x.employeeNo == user.UserName).ToList();

            if (employee?.FirstOrDefault()?.Token != uid && employee.Count() != 0)
            {
                bool status = false;
                string actionresult = "Invalid Token.";
                var jwts = await Tokens.changePasswordJwt(status, actionresult, new JsonSerializerSettings { Formatting = Formatting.Indented });
                return new OkObjectResult(jwts);
            }
            var datajson = await _chemistService.GetChemistListAPIViewModelJson(jti, employeeNo);

            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpGet("GetChemistListJsonWithConversionCode")]
        public async Task<IActionResult> GetChemistListJsonWithConversionCode(string employeeNo)
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
            var employee = await employeeService.GetEmployeeLoadViewModels();
            employee = employee.Where(x => x.employeeNo == user.UserName).ToList();

            if (employee?.FirstOrDefault()?.Token != uid && employee.Count() != 0)
            {
                bool status = false;
                string actionresult = "Invalid Token.";
                var jwts = await Tokens.changePasswordJwt(status, actionresult, new JsonSerializerSettings { Formatting = Formatting.Indented });
                return new OkObjectResult(jwts);
            }
            var datajson = await _chemistService.GetChemistListAPIViewModelJsonWithConversionCode(jti, employeeNo);

            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpGet("GetListChemistMarketForPlan")]
        public async Task<IActionResult> GetListChemistMarketForPlan(string MarketCode)
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
            var chemistlist = await _chemistService.GetChemistListAPIbyMktViewModel(MarketCode);
            if (chemistlist.Count() > 0)
            {
                var jwt = await Tokens.ChemistlistSuccessJwt(chemistlist.ToList(), new JsonSerializerSettings { Formatting = Formatting.Indented });
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.ChemistlistfailJwt(new JsonSerializerSettings { Formatting = Formatting.Indented });
                return new OkObjectResult(jwt);
            }
        }


        [HttpPost("UpdateChemistListWithConversionCode")]
        public async Task<IActionResult> UpdateChemistListWithConversionCode([FromBody] UpdateChemistListViewModel model)
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
            var employee = await employeeService.GetEmployeeLoadViewModels();
            employee = employee.Where(x => x.employeeNo == user.UserName).ToList();
            if (employee?.FirstOrDefault()?.Token != uid && employee.Count() != 0)
            {
                bool status = false;
                string actionresult = "Invalid Token.";
                var jwts = await Tokens.changePasswordJwt(status, actionresult, new JsonSerializerSettings { Formatting = Formatting.Indented });
                return new OkObjectResult(jwts);
            }
            int result = await _chemistService.UpdateChemistListWithConversionCode(model, (int)user.employeeId);

            if (result != 0)
            {
                var jwt = await Tokens.setChemistWithConversionCodeSuccessJwt(new JsonSerializerSettings { Formatting = Formatting.Indented });
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setChemistWithConversionCodeFailJwt(new JsonSerializerSettings { Formatting = Formatting.Indented });
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
