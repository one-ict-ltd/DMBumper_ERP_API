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
    public class LedgerTypeController : Controller
    {
        private UserManager<ApplicationUser> _userManager;
        private IJwtFactoryService _jwtFactory;
        private IUserInfoes userInfoes;
        private RoleManager<ApplicationRole> _roleManager;
        private ICompanyService companyService;
        private ISpecialBranchUnitService specialBranchUnitService;
        private ILedgerTypeService ledgerTypeService;
        public LedgerTypeController(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager, IJwtFactoryService jwtFactory, IUserInfoes userInfoes, ICompanyService companyService, ISpecialBranchUnitService specialBranchUnitService,  ILedgerTypeService ledgerTypeService)
        {
            _userManager = userManager;
            _jwtFactory = jwtFactory;

            _roleManager = roleManager;
            this.userInfoes = userInfoes;
            this.companyService = companyService;
            this.specialBranchUnitService = specialBranchUnitService;
            this.ledgerTypeService = ledgerTypeService;
        



        }

        //public IActionResult Index()
        //{
        //    return View();
        //}

        //[Route("api/LedgerType/GetLedgerType/")]
        //[HttpGet]
        //[AllowAnonymous]
        //public async Task<IActionResult> GetLedgerType()
        //{
        //    return Json(await ledgerTypeService.GetLedgerType());
        //}

        // GET: api/<CompanyController>
        [HttpPost("setledgerType")]
        public async Task<IActionResult> setledgerType([FromBody] LedgerTypeViewModel model)
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
            if (model.ledgerTypeName == null)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented },"Ledger type has not created successfully.",false);

                return new OkObjectResult(jwt);
            }
            bool result = await ledgerTypeService.SaveLedgerType(user.employeeId.ToString(),model);
         

            if (result == true)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Ledger type has created successfully.", true);

                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Ledger type has not created successfully.", false);

                return new OkObjectResult(jwt);
            }
           



        }

        // GET api/<CompanyController>/5
        [HttpGet("getledgerType")]
        public async Task<IActionResult> getledgerType(int ledgerTypeId)
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
            var datajson = await ledgerTypeService.GetLedgerTypeByIdJson(ledgerTypeId);
           
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });

            return new OkObjectResult(jwt);
            
        }
        [HttpPost("deleteledgerType")]
        public async Task<IActionResult> deleteledgerType([FromBody] LedgerTypeViewModel model)
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
            if (model.ledgerTypeId <= 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Ledger type has not deleted successfully.", false);

                return new OkObjectResult(jwt);
            }
            bool result = await ledgerTypeService.DeleteLedgerTypeById(user.employeeId.ToString(), (int)model.ledgerTypeId);


            if (result == true)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Ledger type has deleted successfully.", true);

                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Ledger type has not deleted successfully.", false);

                return new OkObjectResult(jwt);
            }




        }
    }
}
