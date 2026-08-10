using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ONEERP.Areas.Accounting.Models;
using ONEERP.Areas.MasterData.Models;
using ONEERP.Data.Entity;
using ONEERP.ERPService.AuthService.Interfaces;
using ONEERP.ERPServices.Accounting.MasterData.Interfaces;
using ONEERP.ERPServices.AuthService.Interfaces;
using ONEERP.ERPServices.MasterData.Interfaces;
using ONEERP.Helpers;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ONEERP.Areas.Accounting.Controllers
{
    [Route("api/[controller]")]
    //[Authorize]
    //[Area("Accounting")]
    public class GroupNatureController : Controller
    {
        private UserManager<ApplicationUser> _userManager;
        private IJwtFactoryService _jwtFactory;
        private IUserInfoes userInfoes;
        private RoleManager<ApplicationRole> _roleManager;
        private ICompanyService companyService;
        private ISpecialBranchUnitService specialBranchUnitService;
        private IGroupNatureService groupNatureService;
        public GroupNatureController(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager, IJwtFactoryService jwtFactory, IUserInfoes userInfoes, ICompanyService companyService, ISpecialBranchUnitService specialBranchUnitService, IGroupNatureService groupNatureService)
        {
            _userManager = userManager;
            _jwtFactory = jwtFactory;

            _roleManager = roleManager;
            this.userInfoes = userInfoes;
            this.companyService = companyService;
            this.specialBranchUnitService = specialBranchUnitService;
            this.groupNatureService = groupNatureService;
          

        }

        //public IActionResult Index()
        //{
        //    return View();
        //}

        //[Route("api/GroupNature/GetGroupNature/")]
        //[HttpGet]
        //[AllowAnonymous]
        //public async Task<IActionResult> GetGroupNature()
        //{
        //    return Json(await groupNatureService.GetGroupNature());
        //}

        // GET: api/<CompanyController>
        [HttpPost("setgroupNature")]
        public async Task<IActionResult> setgroupNature([FromBody] GroupNatureViewModel model)
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
            if (user.token != uid && user != null)
            {
                bool status = false;
                string actionresult = "Invalid Token.";
                var jwts = await Tokens.changePasswordJwt(status, actionresult, new JsonSerializerSettings { Formatting = Formatting.Indented });

                return new OkObjectResult(jwts);

            }
            //var JWToken = HttpContext.Session.GetString("JWToken");
            //if (JWToken != uid && user != null)
            //{
            //    bool status = false;
            //    string actionresult = "Invalid Token.";
            //    var jwts = await Tokens.changePasswordJwt(status, actionresult, new JsonSerializerSettings { Formatting = Formatting.Indented });

            //    return new OkObjectResult(jwts);

            //}
            if (model.natureName == null)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented },"Group nature has not created successfully.",false);

                return new OkObjectResult(jwt);
            }
            bool result = await groupNatureService.SaveGroupNature(user.employeeId.ToString(),model);
         

            if (result == true)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Group nature has created successfully.", true);

                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Group nature has not created successfully.", false);

                return new OkObjectResult(jwt);
            }
           



        }

        // GET api/<CompanyController>/5
        [HttpGet("getgroupNature")]
        public async Task<IActionResult> getgroupNature(int groupNatureId)
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
            //var JWToken = HttpContext.Session.GetString("JWToken");
            //if (JWToken != uid && user != null)
            //{
            //    bool status = false;
            //    string actionresult = "Invalid Token.";
            //    var jwts = await Tokens.changePasswordJwt(status, actionresult, new JsonSerializerSettings { Formatting = Formatting.Indented });

            //    return new OkObjectResult(jwts);

            //}
            var datajson = await groupNatureService.GetGroupNatureByIdJson(groupNatureId);
           
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });

            return new OkObjectResult(jwt);
            
        }
        [HttpPost("deletegroupNature")]
        public async Task<IActionResult> deletegroupNature([FromBody] GroupNatureViewModel model)
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
            if (user.token != uid && user != null)
            {
                bool status = false;
                string actionresult = "Invalid Token.";
                var jwts = await Tokens.changePasswordJwt(status, actionresult, new JsonSerializerSettings { Formatting = Formatting.Indented });

                return new OkObjectResult(jwts);

            }
            //var JWToken = HttpContext.Session.GetString("JWToken");
            //if (JWToken != uid && user != null)
            //{
            //    bool status = false;
            //    string actionresult = "Invalid Token.";
            //    var jwts = await Tokens.changePasswordJwt(status, actionresult, new JsonSerializerSettings { Formatting = Formatting.Indented });

            //    return new OkObjectResult(jwts);

            //}
            if (model.groupNatureId <= 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Group nature has not deleted.", false);

                return new OkObjectResult(jwt);
            }
            bool result = await groupNatureService.DeleteGroupNatureById(user.employeeId.ToString(), (int)model.groupNatureId);


            if (result == true)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Group nature has deleted successfully.", true);

                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Group nature has not deleted.", false);

                return new OkObjectResult(jwt);
            }




        }
    }
}
