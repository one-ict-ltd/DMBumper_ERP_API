using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ONEERP.Areas.Purchase.Models;
using ONEERP.ERPService.AuthService.Interfaces;
using ONEERP.ERPServices.Purchase.Interfaces;
using ONEERP.Helpers;
using ONEERP.Data.Entity;


namespace ONEERP.Areas.Purchase.Controllers
{
    [Route("api/[controller]")]
    public class GRNController : Controller
    {
        object jwts;
        ApplicationUser user;
        private IUserInfoes userInfoes;
        private readonly IGRNService GRNService;
        public GRNController(IUserInfoes userInfoes, IGRNService GRNService)
        {
            this.userInfoes = userInfoes;
            this.GRNService = GRNService;
            jwts = new object();
            user = new ApplicationUser();
        }
        [HttpGet("getGRNForQA")]
        public async Task<IActionResult> getGRNForQA()
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            var datajson = await GRNService.getGRNForQA((int)user.employeeId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);

        }
        [HttpGet("getGRNForRetest")]
        public async Task<IActionResult> getGRNForRetest()
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            var datajson = await GRNService.getGRNForRetest((int)user.employeeId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);

        }
        [HttpGet("getGRNImportForQA")]
        public async Task<IActionResult> getGRNImportForQA()
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            var datajson = await GRNService.getGRNImportForQA((int)user.employeeId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);

        }
        [HttpGet("getGrnDetailsForQA")]
        public async Task<IActionResult> getGrnDetailsForQA(int? grnMasterId, string InitialOrRetest)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            var datajson = await GRNService.getGrnDetailsForQA(grnMasterId, InitialOrRetest);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);

        }
        [HttpGet("getGrnDetailsForRetest")]
        public async Task<IActionResult> getGrnDetailsForRetest(int? grnMasterId,string grnType)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            var datajson = await GRNService.getGrnDetailsForRetest(grnMasterId, grnType);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);

        }
        [HttpGet("getGrnImportDetailsForQA")]
        public async Task<IActionResult> getGrnImportDetailsForQA(int? ImpgrnMasterId, string InitialOrRetest)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            var datajson = await GRNService.getGrnImportDetailsForQA(ImpgrnMasterId, InitialOrRetest);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);

        }
        [HttpPost("UpdateGRNQaForApproval")]
        public async Task<IActionResult> UpdateGRNQaForApproval([FromBody] GRNQAViewModel model)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            if (model.grnModel.Count() == 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "GRN Not Found.", false);
                return new OkObjectResult(jwt);
            }
            if(model.InitialOrRetest == "Initial")
            {
                await GRNService.UpdateGRNQaMasterForApproval(user.employeeId, model.approvalStatus, model.grnModel);
            }
             
            int result = await GRNService.UpdateGRNQaForApproval(user.employeeId, model.approvalStatus, model.grnModel,model.RetestDate,model.InitialOrRetest);
            if (result > 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "GRN has Approved successfully.", true);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "GRN  has not Approved.", false);
                return new OkObjectResult(jwt);
            }
            
        }
        [HttpPost("UpdateGRNImportQaForApproval")]
        public async Task<IActionResult> UpdateGRNImportQaForApproval([FromBody] GRNImportQAViewModel model)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            if (model.grnModel.Count() == 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "GRN Not Found.", false);
                return new OkObjectResult(jwt);
            }
            if (model.InitialOrRetest == "Initial")
            {
                await GRNService.UpdateGRNImportQaMasterForApproval(user.employeeId, model.approvalStatus, model.grnModel);
            }
            
            int result = await GRNService.UpdateGRNImportQaForApproval(user.employeeId, model.approvalStatus, model.grnModel, model.RetestDate, model.InitialOrRetest);

            if (result > 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "GRN has Approved successfully.", true);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "GRN  has not Approved.", false);
                return new OkObjectResult(jwt);
            }

        }
        [HttpPost("setGrnLogtbl")]
        public async Task<IActionResult> setGrnLogtbl([FromBody] PurGrnLogViewModel model)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            int result = await GRNService.SaveGrnLogtbl((int)user.employeeId, model);


            if (result != 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "GRN logtbl has created successfully.", true);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "GRN  logtbl has not created.", false);
                return new OkObjectResult(jwt);
            }



        }
        async Task<bool> Authentication()
        {
            #region common
            var uid = Request.Headers["auth_token"];
            if (uid.Count() == 0)
            {
                bool status = false;
                string actionresult = "Invalid Token.";
                jwts = await Tokens.changePasswordJwt(status, actionresult, new JsonSerializerSettings { Formatting = Formatting.Indented });
                return false;
            }

            var stream = uid;
            var handler = new JwtSecurityTokenHandler();
            var tokenS = handler.ReadToken(stream) as JwtSecurityToken;
            var jti = tokenS.Claims.First(claim => claim.Type == "Id").Value;
            user = await userInfoes.GetUserBasicInfoesbyId(jti);

            if (user.token != uid && user != null)
            {
                bool status = false;
                string actionresult = "Invalid Token.";
                jwts = Tokens.changePasswordJwt(status, actionresult, new JsonSerializerSettings { Formatting = Formatting.Indented });
                return false;
            }
            return true;
            #endregion
        }
    }
}
