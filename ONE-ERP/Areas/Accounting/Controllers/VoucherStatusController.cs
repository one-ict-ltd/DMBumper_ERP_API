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

    public class VoucherStatusController : Controller
    {
        private UserManager<ApplicationUser> _userManager;
        private IJwtFactoryService _jwtFactory;
        private IUserInfoes userInfoes;
        private RoleManager<ApplicationRole> _roleManager;
        private ICompanyService companyService;
        private ISpecialBranchUnitService specialBranchUnitService;
        private IVoucherStatusService voucherStatusService;
        public VoucherStatusController(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager, IJwtFactoryService jwtFactory, IUserInfoes userInfoes, ICompanyService companyService, ISpecialBranchUnitService specialBranchUnitService, IVoucherTypeService voucherTypeService, IVoucherStatusService voucherStatusService)
        {
            _userManager = userManager;
            _jwtFactory = jwtFactory;

            _roleManager = roleManager;
            this.userInfoes = userInfoes;
            this.companyService = companyService;
            this.specialBranchUnitService = specialBranchUnitService;
            this.voucherStatusService = voucherStatusService;
          

        }

        //public IActionResult Index()
        //{
        //    return View();
        //}

        //[Route("api/VoucherStatus/GetVoucherStatus/")]
        //[HttpGet]
        //[AllowAnonymous]
        //public async Task<IActionResult> GetVoucherStatus()
        //{
        //    return Json(await voucherStatusService.GetVoucherStatus());
        //}

        // GET: api/<CompanyController>
        [HttpPost("setVoucherStatus")]
        public async Task<IActionResult> setVoucherStatus([FromBody] VoucherStatusViewModel model)
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
            if (model.statusName == null)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented },"Voucher status has not created successfully.",false);

                return new OkObjectResult(jwt);
            }
            bool result = await voucherStatusService.SaveVoucherStatus(user.employeeId.ToString(),model);
         

            if (result == true)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Voucher status has created successfully.", true);

                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Voucher status has not created successfully.", false);

                return new OkObjectResult(jwt);
            }
           



        }

        // GET api/<CompanyController>/5
        [HttpGet("getVoucherStatus")]
        public async Task<IActionResult> getVoucherStatus(int voucherStatusId)
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
            var datajson = await voucherStatusService.GetVoucherStausByIdJson(voucherStatusId);
           
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });

            return new OkObjectResult(jwt);
            
        }
        [HttpPost("deletevoucherStatus")]
        public async Task<IActionResult> deletevoucherStatus([FromBody] VoucherStatusViewModel model)
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
            if (model.voucherStatusId <= 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Voucher status has not deleted.", false);

                return new OkObjectResult(jwt);
            }
            bool result = await voucherStatusService.DeleteVoucherStatusById(user.employeeId.ToString(), (int)model.voucherStatusId);


            if (result == true)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Voucher status has deleted successfully.", true);

                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Voucher status has not deleted.", false);

                return new OkObjectResult(jwt);
            }




        }
    }
}
