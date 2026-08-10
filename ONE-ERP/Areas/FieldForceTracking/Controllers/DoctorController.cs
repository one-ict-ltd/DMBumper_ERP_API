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
    public class DoctorController : Controller
    {
        private UserManager<ApplicationUser> _userManager;
        private IJwtFactoryService _jwtFactory;
        private IUserInfoes userInfoes;
        private readonly IDoctorService _doctorService;
        private readonly IEmployeeService employeeService;
        //private  JwtIssuerOptions _jwtOptions;
        private RoleManager<ApplicationRole> _roleManager;

        public DoctorController(UserManager<ApplicationUser> userManager, IEmployeeService employeeService, RoleManager<ApplicationRole> roleManager, IJwtFactoryService jwtFactory, IUserInfoes userInfoes, IDoctorService doctorService)
        {
            _userManager = userManager;
            _jwtFactory = jwtFactory;
            this.employeeService = employeeService;
            _roleManager = roleManager;
            this.userInfoes = userInfoes;
            this._doctorService = doctorService;

        }

        [HttpGet("GetDoctorListByUser")]
        public async Task<IActionResult> GetDoctorListByUser(string employeeNo)
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

            var datajson = await _doctorService.GetDoctorListByUser(jti, employeeNo);
            var jwt = await Tokens.getDataWithStatusAndMessage(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented },"Data found");
            return new OkObjectResult(jwt);
        }

        [HttpGet("GetDoctorListByUserAndCode")]
        public async Task<IActionResult> GetDoctorListByUserAndCode(string code)
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

            var datajson = await _doctorService.getDoctorlistByUserAndCode(jti, code);
            var jwt = await Tokens.getDataWithStatusAndMessage(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented },"Data found");
            return new OkObjectResult(jwt);
        }

        [HttpGet("GetDoctorforUpdate")]
        public async Task<IActionResult> GetDoctorforUpdate(int doctorId)
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

            var datajson = await _doctorService.CmnDoctorForUpdateById(jti, doctorId);
            var datajsonattn = await _doctorService.CmnDoctorRxwithproductById(jti,doctorId);
            if (datajson.data != "[]")
            {
                var jwt = await Tokens.getDashboard(datajson.data, datajsonattn.data, new JsonSerializerSettings { Formatting = Formatting.Indented }, "data found successfully.");
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.getDashboard(datajson.data, datajsonattn.data, new JsonSerializerSettings { Formatting = Formatting.Indented }, "data not found.");
                return new OkObjectResult(jwt);
            }
        }

        [HttpGet("GetDoctor")]
        public async Task<IActionResult> GetDoctor(string doctorId)
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

            var datajson = await _doctorService.DoctorListAPIViewModels(doctorId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);

        }
        [HttpGet("GetDoctorId")]
        public async Task<IActionResult> GetDoctorId(string doctorNo)
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

            var datajson = await _doctorService.GetDoctorId(doctorNo);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);

        }

        [HttpGet("GetListDoctorMarketForPlan")]
        public async Task<IActionResult> GetListDoctorMarketForPlan(string MarketCode)
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
            var doctorlist = await _doctorService.DoctorListAPIbyMarketViewModels(MarketCode);
            if (doctorlist.Count() > 0)
            {
                var jwt = await Tokens.DoctorlistSuccessJwt(doctorlist.ToList(), new JsonSerializerSettings { Formatting = Formatting.Indented });

                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.DoctorlistfailJwt(new JsonSerializerSettings { Formatting = Formatting.Indented });

                return new OkObjectResult(jwt);
            }
            // int cnic = jsonData.enad_list[0].et_cnic;
        }

        [HttpGet("GetDoctorByTerritoryMarket")]
        public async Task<IActionResult> GetDoctorByTerritoryMarket(string MarketID, string TerritoryID)
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
            var datajson = await _doctorService.GetDoctorByTerritoryMarket(MarketID, TerritoryID);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
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
