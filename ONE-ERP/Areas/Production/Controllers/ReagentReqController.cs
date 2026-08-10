using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ONEERP.Areas.Production.Models;
using ONEERP.Areas.Purchase.Models;
using ONEERP.Data.Entity;
using ONEERP.ERPService.AuthService.Interfaces;
using ONEERP.ERPServices.Production.Interfaces;
using ONEERP.ERPServices.Purchase;
using ONEERP.ERPServices.Purchase.Interfaces;
using ONEERP.Helpers;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.Production.Controllers
{
    [Route("api/[controller]")]
    public class ReagentReqController : Controller
    {
        object jwts; ApplicationUser user;
        private IUserInfoes userInfoes;
        private IReagentReqService _reagentReqService;
        public ReagentReqController(IUserInfoes userInfoes, IReagentReqService reagentReqService)
        {
            this.userInfoes = userInfoes;
            this._reagentReqService = reagentReqService;
        }

        [HttpGet("GetMaxReagentReqNumber")]
        public async Task<IActionResult> GetMaxReagentReqNumber(DateTime reagentReqDate)
        {
            #region Common
            if (Authentication().Result == false) return new OkObjectResult(jwts);
            #endregion

            var datajson = await _reagentReqService.GetMaxReagentReqNumber(reagentReqDate);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpGet("getAllProductForReagentReq")]
        public async Task<IActionResult> GetAllProductForReagentReq(int productId = 0)
        {
            #region Common
            if (Authentication().Result == false) return new OkObjectResult(jwts);
            #endregion

            var datajson = await _reagentReqService.GetAllProductForReagentReq(productId, (int)user.employeeId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpPost("saveReagentReq")]
        public async Task<IActionResult> setProductRequisition([FromBody] ReagentRequisitionViewModel model)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);


            if (model.tosbuId == 0 && model.lstReqDetailsViewModel.Count() == 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Product To Warehouse or Req. Details is empty! Product Req. has not created.", false);
                return new OkObjectResult(jwt);
            }

            int result = 0;
            int prodReqId = await _reagentReqService.SaveReagentReq(user.employeeId.ToString(), model);

            if (prodReqId == 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Reagent Req. has not created.", false);
                return new OkObjectResult(jwt);
            }

            result = await _reagentReqService.SaveReagentReqDetails(user.employeeId.ToString(), model.lstReqDetailsViewModel, prodReqId);

            if (result != 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Reagent Req. Details has created successfully.", true);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Reagent Req.  has not created.", false);
                return new OkObjectResult(jwt);
            }
        }

        [HttpGet("getReagentRequisition")]
        public async Task<IActionResult> getReagentRequisition(int? reagentReqId)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            var datajson = await _reagentReqService.GetReagentReqById(user.employeeId, reagentReqId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpGet("deleteReagentReqById")]
        public async Task<IActionResult> DeleteReagentReqById(int reagentReqId)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            if (reagentReqId <= 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Reagent Req. has not deleted.", false);
                return new OkObjectResult(jwt);
            }
            bool result = await _reagentReqService.DeleteReagentReqById((int)user.employeeId, reagentReqId);// All delete Method do not have token.

            if (result == true)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Reagent Req. has deleted successfully.", true);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Reagent Req. has not deleted.", false);
                return new OkObjectResult(jwt);
            }
        }

        [HttpGet("getReagentReqDetails")]
        public async Task<IActionResult> GetProductReqDetails(int? reagentReqId)//(int? productReqDetailsId)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            var datajson = await _reagentReqService.GetReagentReqDetailsById(reagentReqId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        async Task<bool> Authentication()
        {
            #region common
            var uid = Request.Headers["auth_token"];
            if (!uid.Any())
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

            if (user == null || user.token != uid)
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
