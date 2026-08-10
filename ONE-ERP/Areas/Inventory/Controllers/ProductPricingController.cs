using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ONEERP.Areas.Inventory.Models;
using ONEERP.Data.Entity;
using ONEERP.ERPService.AuthService.Interfaces;
using ONEERP.ERPServices.Inventory.Interfaces;
using ONEERP.Helpers;

namespace ONEERP.Areas.Inventory.Controllers
{
    [Route("api/[controller]")]
    public class ProductPricingController : Controller
    {
        object jwts;
        ApplicationUser appUser;

        private IUserInfoes userInfoes;
        private readonly IProductPricingService service;
        public ProductPricingController(IUserInfoes _userInfoes, IProductPricingService _service)
        {
            this.userInfoes = _userInfoes;
            this.service = _service;
            jwts = new object();
            appUser = new ApplicationUser();
        }

        #region Sales Offer

        [HttpPost("SaveProductPricing")]
        public async Task<IActionResult> SaveProductPricing([FromBody] ProductPricingViewModel model)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            if (model == null)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Product Pricing has not created.", false);
                return new OkObjectResult(jwt);
            }

            int salesOfferId = await service.SaveProductPricing(appUser.employeeId.ToString(), model);

            if (salesOfferId == 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Product Pricing has not created.", false);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Product Pricing has created successfully.", true);
                return new OkObjectResult(jwt);
            }
        }
        
        [HttpPost("SaveCashSetUp")]
        public async Task<IActionResult> SaveCashSetUp([FromBody] CashSetUpViewModel model)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            if (model == null)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Cash Salary SetUp has not created.", false);
                return new OkObjectResult(jwt);
            }

            int salesOfferId = await service.SaveCashSetUp(appUser.employeeId.ToString(), model);

            if (salesOfferId == 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Cash Salary SetUp has not created.", false);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Cash Salary SetUp has created successfully.", true);
                return new OkObjectResult(jwt);
            }
        }

        [HttpGet("GetProductPricingByMasterId")]
        public async Task<IActionResult> GetProductPricingByMasterId(int? pricingId, int? productWiseSpecificationId)
        {
            if (Authentication().Result == false) return new OkObjectResult(this.jwts);

            var datajson = await service.GetProductPricingByMasterId(pricingId, productWiseSpecificationId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpGet("GetProductPricingNByMasterId")]
        public async Task<IActionResult> GetProductPricingNByMasterId(int? pricingId, int? productWiseSpecificationId)
        {
            if (Authentication().Result == false) return new OkObjectResult(this.jwts);

            var datajson = await service.GetProductPricingNByMasterId(pricingId, productWiseSpecificationId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }
        
        [HttpGet("GetEmployeeCashSalaryJSON")]
        public async Task<IActionResult> GetEmployeeCashSalaryJSON()
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

            var datajson = await service.GetEmployeeCashSalaryJSON(user.employeeId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        #endregion

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
            appUser = await userInfoes.GetUserBasicInfoesbyId(jti);

            if (appUser.token != uid && appUser != null)
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