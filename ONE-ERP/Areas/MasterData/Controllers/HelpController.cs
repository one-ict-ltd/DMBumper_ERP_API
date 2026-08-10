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
using ONEERP.ERPServices.Accounting.Transaction.Interfaces;
using ONEERP.ERPServices.AuthService.Interfaces;
using ONEERP.ERPServices.MasterData.Interfaces;
using ONEERP.Helpers;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ONEERP.Areas.MasterData.Controllers
{
    [Route("api/[controller]")]
    //[Authorize]
    //[Area("Accounting")]
    public class HelpController : Controller
    {
        private UserManager<ApplicationUser> _userManager;
        private IJwtFactoryService _jwtFactory;
        private IUserInfoes userInfoes;
        private RoleManager<ApplicationRole> _roleManager;
        private ICompanyService companyService;
        private ISpecialBranchUnitService specialBranchUnitService;
        private IHelpService helpService;
        public HelpController(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager, IJwtFactoryService jwtFactory, IUserInfoes userInfoes, ICompanyService companyService, ISpecialBranchUnitService specialBranchUnitService, IHelpService helpService)
        {
            _userManager = userManager;
            _jwtFactory = jwtFactory;

            _roleManager = roleManager;
            this.userInfoes = userInfoes;
            this.companyService = companyService;
            this.specialBranchUnitService = specialBranchUnitService;
            this.helpService = helpService;
           

        



        }
      

        // GET: api/<CompanyController>
        [HttpPost("setHelp")]
        public async Task<IActionResult> setHelp([FromBody] HelpMasterViewModel model)
        //public async Task<IActionResult> setHelp([FromBody] HelpMasterViewModel model)
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
            if (model==null)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented },"Data has not created successfully.",false);

                return new OkObjectResult(jwt);
            }
            bool result = false;
           
            result = await helpService.SaveHelpMaster(user.employeeId.ToString(),model,model.lstdetailmodel,model.lstmultimodel,model.lstimagemodel);

            if (result == true)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Data has created successfully.", true);

                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Data has not created successfully.", false);

                return new OkObjectResult(jwt);
            }
           



        }

   
        [HttpGet("getHelpMaster")]
        
        public async Task<IActionResult> getHelp(int helpMasterId, int helpDetailId,int helpMultiId,int helpImageId)
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


            var dataMaster = await helpService.GetHelpMasterListbyId(helpMasterId);
            var dataDetail = await helpService.GetHelpDetailListbyId(helpMasterId,helpDetailId);
            var dataMulti = await helpService.GetHelpMultiListbyId(helpMasterId,helpMultiId);
            var dataImage = await helpService.GetHelpImageListbyId(helpMasterId,helpImageId);
           
            var jwt = await Tokens.getMultipleData(dataMaster.data,dataDetail.data,dataMulti.data,dataImage.data, new JsonSerializerSettings { Formatting = Formatting.Indented });

            return new OkObjectResult(jwt);
            
        }
        [HttpGet("getHelpDetail")]
        public async Task<IActionResult> getHelpDetail(int helpId,int helpDetailId)
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
            var datajson = await helpService.GetHelpDetailListbyId(helpId,helpDetailId);

            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });

            return new OkObjectResult(jwt);

        }
        [HttpGet("getHelpMulti")]
        public async Task<IActionResult> getHelpMulti(int helpId, int helpMultiId)
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
            var datajson = await helpService.GetHelpMultiListbyId(helpId, helpMultiId);

            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });

            return new OkObjectResult(jwt);

        }

        [HttpGet("getHelpImage")]
        public async Task<IActionResult> getHelpImage(int helpId, int helpImageId)
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
            var datajson = await helpService.GetHelpImageListbyId(helpId, helpImageId);

            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });

            return new OkObjectResult(jwt);

        }

        [HttpPost("deletehelpMaster")]
        public async Task<IActionResult> deletehelpMaster([FromBody] HelpMasterViewModel model)
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
            if (model.helpId <= 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Data has not deleted.", false);

                return new OkObjectResult(jwt);
            }
            bool result = await helpService.DeleteHelpMasterListbyId(user.employeeId.ToString(), (int)model.helpId);


            if (result == true)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Data has deleted successfully.", true);

                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "data has not deleted.", false);

                return new OkObjectResult(jwt);
            }




        }

    }
}
