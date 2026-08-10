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

namespace ONEERP.Areas.Purchase.Controllers
{
    [Route("api/[controller]")]
    public class PurchaseController : Controller
    {
        private IUserInfoes userInfoes;
        private readonly IPurchaseOrderService purOrderService;
        private readonly IPurchaseService purchaseService;
        public PurchaseController(IUserInfoes userInfoes, IPurchaseOrderService purOrderService, IPurchaseService purchaseService)
        {
            this.userInfoes = userInfoes;
            this.purOrderService = purOrderService;
            this.purchaseService = purchaseService;
        }

        #region Purchase Master

        [HttpPost("setPurchaseMaster")]
        public async Task<IActionResult> setPurchaseMaster([FromBody] PurchaseViewModel model)
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

            if (model.purchaseOrderDate == null && model.lstPurchaseDetailsViewModels.Count() == 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Purchase has not created.", false);
                return new OkObjectResult(jwt);
            }

            int result = 0;
            int purchaseId = await purchaseService.SavePurchase(user.employeeId.ToString(), model);

            if (purchaseId == 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Purchase has not created.", false);
                return new OkObjectResult(jwt);
            }

            // 
            result = await purchaseService.SavePurchaseDetails(user.employeeId.ToString(), model.lstPurchaseDetailsViewModels, purchaseId,model.totalVat,model.totalAit, model.freightCharge, model.grossAmount, model.fromWarehouseId, model.isAutoStock);

            if (model.poWiseTermsAndConditions.Count() > 0)
            {
                result = await purOrderService.SavePOWisetermsAndConditions(user.employeeId.ToString(), model.poWiseTermsAndConditions, purchaseId);
                if (result != 0)
                {
                    var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Purchase Details has created successfully.", true);
                    return new OkObjectResult(jwt);
                }

                else
                {
                    var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Purchase has not created.", false);
                    return new OkObjectResult(jwt);
                }
            }

            if (model.transactionTypeId == 1) //Cash
            {
                int voucherMasterId = await purOrderService.CreateAutoJournalForPurchaseDirect(user.employeeId.ToString(), model, purchaseId);
            }
            else if (model.transactionTypeId == 2) //Credit
            {
                int voucherMasterId = await purOrderService.CreateAutoJournalForPurchaseDirectOnCredit(user.employeeId.ToString(), model, purchaseId);
            }
            else if (model.transactionTypeId == 3) //Advance
            {
                int voucherMasterId = await purOrderService.CreateAutoJournalForPurchaseDirectOnAdvance(user.employeeId.ToString(), model, purchaseId);
            }

            if (result != 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Purchase Details has created successfully.", true);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Purchase Details has not created.", false);
                return new OkObjectResult(jwt);
            }



        }

        [HttpGet("GetPurchaseById")]
        public async Task<IActionResult> GetPurchaseById(int? purchaseOrderId)
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

            var datajson = await purchaseService.GetPurchaseById(purchaseOrderId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        #endregion

        #region Purchase Details

        
        #endregion

    }
}