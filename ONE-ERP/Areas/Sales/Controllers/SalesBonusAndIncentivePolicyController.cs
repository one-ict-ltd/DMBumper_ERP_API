using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ONEERP.Areas.Inventory.Models;
using ONEERP.Areas.Sales.Models;
using ONEERP.Data.Entity;
using ONEERP.ERPService.AuthService.Interfaces;
using ONEERP.ERPServices.Sales.Interfaces;
using ONEERP.Helpers;

namespace ONEERP.Areas.Sales.Controllers
{
    [Route("api/[controller]")]
    public class SalesBonusAndIncentivePolicyController : Controller
    {
        object jwts;
        ApplicationUser user;
        private IUserInfoes userInfoes;
        private readonly ISalesBonusAndIncentivePolicyService service;
        public SalesBonusAndIncentivePolicyController(IUserInfoes _userInfoes, ISalesBonusAndIncentivePolicyService _service)
        {
            this.userInfoes = _userInfoes;
            this.service = _service;
        }

        #region General Customer Bonus Policy

        [HttpPost("SaveGeneralCustomerBonusPolicy")]
        public async Task<IActionResult> SaveGeneralCustomerBonusPolicy([FromBody] SalGeneralCustomerBonusPolicyViewModel model)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            if (model == null)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "General Customer Bonus Policy has not created.", false);
                return new OkObjectResult(jwt);
            }

            int result = 0;
            int distributionMasterId = await service.SaveGeneralCustomerBonusPolicy(user.employeeId.ToString(), model);

            if (distributionMasterId == 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "General Customer Bonus Policy has not created.", false);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "General Customer Bonus Policy has created.", true);
                return new OkObjectResult(jwt);
            }
        }

        [HttpPost("DeleteGeneralCustomerBonusPolicy")]
        public async Task<IActionResult> DeleteGeneralCustomerBonusPolicy([FromBody] int mangoPolicyId)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            if (mangoPolicyId <= 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Sales Distribution has not deleted.", false);
                return new OkObjectResult(jwt);
            }
            bool result = await service.DeleteGeneralCustomerBonusPolicy(user.employeeId.ToString(), mangoPolicyId);

            if (result == true)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Sales Distribution has deleted successfully.", true);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Sales Distribution has not deleted.", false);
                return new OkObjectResult(jwt);
            }
        }

        [HttpGet("GetGeneralCustomerBonusPolicy")]
        public async Task<IActionResult> GetGeneralCustomerBonusPolicy(int? generalPolicyId)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            var datajson = await service.GetGeneralCustomerBonusPolicy(generalPolicyId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        #endregion

        #region Discount Rate & Item Policy

        [HttpGet("GetitemPriceBySpecId")]
        public async Task<IActionResult> GetitemPriceBySpecId(int? SecId)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            var datajson = await service.SalSpGetitemPriceBySpecId(SecId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpGet("GetSalesDiscountRatePolicy")]
        public async Task<IActionResult> GetSalesDiscountRatePolicy(int? DiscountRateId, string depotCode = "", int partyId=0, string discountType="", DateTime? fromdate = null, DateTime? toDate = null)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            var datajson = await service.SalSpGetSalesDiscountRatePolicy(user.employeeId,DiscountRateId, depotCode, partyId, discountType, fromdate, toDate);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpGet("GetSalesDiscountItemPolicy")]
        public async Task<IActionResult> GetSalesDiscountItemPolicy(int? DiscountItemId, DateTime? fromDate, DateTime? endDate)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            var datajson = await service.SalSpGetDiscountItemPolicy(DiscountItemId, fromDate, endDate, user.employeeId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpPost("SaveDiscountRatePolicy")]
        public async Task<IActionResult> SaveDiscountRatePolicy([FromBody] SalDiscountRateViewModel model)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            if (model == null)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Discount Rate Policy has not created.", false);
                return new OkObjectResult(jwt);
            }

            int result = 0;
            int distributionMasterId = await service.SaveDiscountRatePolicy(user.employeeId.ToString(), model);

            if (distributionMasterId == 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Discount Rate Policy has not created.", false);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Discount Rate Policy has created.", true);
                return new OkObjectResult(jwt);
            }
        }
        
        [HttpPost("SaveFlatRatePolicyList")]
        public async Task<IActionResult> SaveFlatRatePolicyList([FromBody] List<SalDiscountRateViewModel> models)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            if (models == null || models.Count ==0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "No Item found to add to flat rate.", false);
                return new OkObjectResult(jwt);
            }

            
            int distributionMasterId = 0;
            foreach(var item in models)
            {
                distributionMasterId = await service.SaveDiscountRatePolicy(user.employeeId.ToString(), item);
            }
             

            if (distributionMasterId == 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Flat Rate Policy has not created.", false);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Flat Rate Policy has created.", true);
                return new OkObjectResult(jwt);
            }
        }
        
        [HttpGet("GetProductsForDiscount")]
        public async Task<IActionResult> GetProductsForDiscount(int? productTypeId)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            var datajson = await service.GetProductsForDiscount(productTypeId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpPost("SaveDiscountItemPolicy")]
        public async Task<IActionResult> SaveDiscountItemPolicy([FromBody] SalDiscountItemViewModel model)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            if (model == null)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Discount Item Policy has not created.", false);
                return new OkObjectResult(jwt);
            }

            int result = 0;
            int distributionMasterId = await service.SaveDiscountItemPolicy(user.employeeId.ToString(), model);

            if (distributionMasterId == 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Discount Item Policy has not created.", false);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Discount Item Policy has created.", true);
                return new OkObjectResult(jwt);
            }
        }
        [HttpPost("SaveDiscountItemPolicyForMultipleProduct")]
        public async Task<IActionResult> SaveDiscountItemPolicyForMultipleProduct([FromBody] List<SalDiscountItemViewModel> model)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            if (model == null)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Discount Item Policy has not created.", false);
                return new OkObjectResult(jwt);
            }

            int result = 0;
            if (model.Count > 0)
            {
                foreach(var item in model)
                {
                    result = await service.SaveDiscountItemPolicy(user.employeeId.ToString(), item);
                }
            }
            

            if (result == 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Discount Item Policy has not created.", false);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Discount Item Policy has created.", true);
                return new OkObjectResult(jwt);
            }
        }
        [HttpPost("SaveListOfDiscountItemPolicy")]
        public async Task<IActionResult> SaveListOfDiscountItemPolicy([FromBody] SalDiscountRateViewModel model)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            if (model == null)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Discount Rate Policy has not created.", false);
                return new OkObjectResult(jwt);
            }

            int result = 0;
            int distributionMasterId = await service.SaveListOfDiscountItemPolicy(user.employeeId.ToString(), model);

            if (distributionMasterId == 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Discount Rate Policy has not created.", false);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Discount Rate Policy has created.", true);
                return new OkObjectResult(jwt);
            }
        }
        
        [HttpPost("UpdateStatusOfDiscountItemPolicies")]
        public async Task<IActionResult> UpdateStatusOfDiscountItemPolicies([FromBody] List<SalDiscountPolicyUpdateViewModel> models)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            if (models == null)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Discount Rate Policy has not updated.", false);
                return new OkObjectResult(jwt);
            }

            //int result = 0;
            int distributionMasterId = await service.UpdateStatusOfDiscountItemPolicies(user.employeeId, models);

            if (distributionMasterId == 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Discount Rate Policy has not updated.", false);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Discount Rate Policy has updated successfully.", true);
                return new OkObjectResult(jwt);
            }
        }

        [HttpPost("DeleteDiscountRatePolicy")]
        public async Task<IActionResult> DeleteDiscountRatePolicy([FromBody] int DiscountRateId)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            if (DiscountRateId <= 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Discount Rate has not deleted.", false);
                return new OkObjectResult(jwt);
            }
            bool result = await service.DeleteDiscountRatePolicy(user.employeeId.ToString(), DiscountRateId);

            if (result == true)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Discount Rate  has deleted successfully.", true);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Discount Rate  has not deleted.", false);
                return new OkObjectResult(jwt);
            }
        }

        [HttpPost("DeleteDiscountItemPolicy")]
        public async Task<IActionResult> DeleteDiscountItemPolicy([FromBody] int DiscountItemId)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            if (DiscountItemId <= 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Discount Item has not deleted.", false);
                return new OkObjectResult(jwt);
            }
            bool result = await service.DeleteDiscountItemPolicy(user.employeeId.ToString(), DiscountItemId);

            if (result == true)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Discount Item  has deleted successfully.", true);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Discount Item  has not deleted.", false);
                return new OkObjectResult(jwt);
            }
        }

        #endregion


        #region Mango Customer Bonus Policy

        [HttpPost("SaveMangoCustomerBonusPolicy")]
        public async Task<IActionResult> SaveMangoCustomerBonusPolicy([FromBody] SalMangoCustomerBonusPolicyViewModel model)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            if (model == null)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Mango Customer Bonus Policy has not created.", false);
                return new OkObjectResult(jwt);
            }

            int result = 0;
            int distributionMasterId = await service.SaveMangoCustomerBonusPolicy(user.employeeId.ToString(), model);

            if (distributionMasterId == 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Mango Customer Bonus Policy has not created.", false);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Mango Customer Bonus Policy has created.", true);
                return new OkObjectResult(jwt);
            }
        }

        [HttpPost("DeleteMangoCustomerBonusPolicy")]
        public async Task<IActionResult> DeleteMangoCustomerBonusPolicy([FromBody] int mangoPolicyId)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            if (mangoPolicyId <= 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Sales Distribution has not deleted.", false);
                return new OkObjectResult(jwt);
            }
            bool result = await service.DeleteMangoCustomerBonusPolicy(user.employeeId.ToString(), mangoPolicyId);

            if (result == true)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Sales Distribution has deleted successfully.", true);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Sales Distribution has not deleted.", false);
                return new OkObjectResult(jwt);
            }
        }

        [HttpGet("GetMangoCustomerBonusPolicy")]
        public async Task<IActionResult> GetMangoCustomerBonusPolicy(int? mangoPolicyId)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            var datajson = await service.GetMangoCustomerBonusPolicy(mangoPolicyId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        #endregion
        

        #region Product Spec. Wise Incentive Policy

        [HttpPost("SaveProductSpecWiseIncentivePolicy")]
        public async Task<IActionResult> SaveProductSpecWiseIncentivePolicy([FromBody] List<SalProductSpecWiseIncentivePolicyViewModel> models)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            if (models.Count == 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "ProductSpecWiseIncentivePolicy has not created.", false);
                return new OkObjectResult(jwt);
            }

            int result = 0;
            int distributionMasterId = await service.SaveProductSpecWiseIncentivePolicy(user.employeeId.ToString(), models);

            if (distributionMasterId == 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Product Spec. Wise Incentive Policy has not created.", false);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Product Spec. Wise Incentive Policy has created.", true);
                return new OkObjectResult(jwt);
            }
        }

        [HttpPost("DeleteProductSpecWiseIncentivePolicy")]
        public async Task<IActionResult> DeleteProductSpecWiseIncentivePolicy([FromBody] int incentivePolicyId)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            if (incentivePolicyId <= 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Sales Distribution has not deleted.", false);
                return new OkObjectResult(jwt);
            }
            bool result = await service.DeleteProductSpecWiseIncentivePolicy(user.employeeId.ToString(), incentivePolicyId);

            if (result == true)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Sales Distribution has deleted successfully.", true);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Sales Distribution has not deleted.", false);
                return new OkObjectResult(jwt);
            }
        }

        [HttpGet("GetProductSpecWiseIncentivePolicy")]
        public async Task<IActionResult> GetProductSpecWiseIncentivePolicy(int? incentivePolicyId, DateTime? fDate, DateTime? tDate)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            var datajson = await service.GetProductSpecWiseIncentivePolicy(incentivePolicyId, fDate, tDate);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        #endregion
        [HttpPost("SaveCategorySales")]
        public async Task<IActionResult> SaveCategorySales([FromBody] CategoryWiseProductVM model)
        {
            #region common

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


            #endregion

            int promoTrfId = 0;
            promoTrfId = await service.SaveCategorySalesMaster(user.employeeId.ToString(),model.month, model.year, model.productCategoryId);

            if (promoTrfId == 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Receive has not created.", false);
                return new OkObjectResult(jwt);
            }

            int result = 0;
            result = await service.SaveCategorySalesMasterDetails(user.employeeId.ToString(), model.lstDetailsViewModel, promoTrfId);

            if (result == 1)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Receive Details has created successfully.", true);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Already Selected In Another Category", true);
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