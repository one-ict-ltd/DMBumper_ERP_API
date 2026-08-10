using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ONEERP.Areas.Purchase.Models;
using ONEERP.ERPService.AuthService.Interfaces;
using ONEERP.ERPServices.Purchase.Interfaces;
using ONEERP.Helpers;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.Purchase.Controllers
{
    [Route("api/[controller]")]
    public class ProductRequisitionController : Controller
    {
        //public IActionResult Index()
        //{
        //    return View();
        //}
        private IUserInfoes userInfoes;
        private IProductRequisitionService productRequisitionService;
        public ProductRequisitionController(IUserInfoes userInfoes, IProductRequisitionService productRequisitionService)
        {
            this.userInfoes = userInfoes;
            this.productRequisitionService = productRequisitionService;
        }

        #region Product Req.

        [HttpPost("setProductRequisition")]
        public async Task<IActionResult> setProductRequisition([FromBody] ProductRequisitionViewModel model)
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



            if (model.tosbuId == 0 && model.lstReqDetailsViewModel.Count() == 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Product To Warehouse or Req. Details is empty! Product Req. has not created.", false);
                return new OkObjectResult(jwt);
            }

            int result = 0;
            int prodReqId = await productRequisitionService.SaveProductReq(user.employeeId.ToString(), model);

            if (prodReqId == 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Product Req. has not created.", false);
                return new OkObjectResult(jwt);
            }

            result = await productRequisitionService.SaveProductReqDetails(user.employeeId.ToString(), model.lstReqDetailsViewModel, prodReqId);

            if (result != 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Product Req. Details has created successfully.", true);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Product Req.  has not created.", false);
                return new OkObjectResult(jwt);
            }
        }

        [HttpPost("setDepotToDepotTransfer")]
        public async Task<IActionResult> setDepotToDepotTransfer([FromBody] ProductRequisitionViewModel model)
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



            if (model.tosbuId == 0 && model.lstReqDetailsViewModel.Count() == 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "To Warehouse or Req. Details is empty! Product Req. has not created.", false);
                return new OkObjectResult(jwt);
            }


            foreach (var item in model.lstReqDetailsViewModel)
            {
                if (item.isSelect == true)
                {
                    var res = await productRequisitionService.ValidateBatchWiseProductStock(user.employeeId, model.fromsbuId, item.productWiseSpecificationId, item.batchNo, item.transferQty);
                    if (!string.IsNullOrWhiteSpace(res))
                    {
                        var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, res, false);
                        return new OkObjectResult(jwt);
                    }
                }
            }


            int result = 0;
            int prodReqId = await productRequisitionService.SaveProductTransfer(user.employeeId.ToString(), model);

            if (prodReqId == 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Transfer has not created.", false);
                return new OkObjectResult(jwt);
            }

            result = await productRequisitionService.SaveProductTransferDetails(user.employeeId.ToString(), model.lstReqDetailsViewModel, prodReqId);

            if (result != 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Transfer has created successfully.", true);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Transfer has not created.", false);
                return new OkObjectResult(jwt);
            }
        }

        [HttpGet("getProductRequisition")]
        public async Task<IActionResult> getProductRequisition(int? prodReqId)
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

            var datajson = await productRequisitionService.GetProductReqById(user.employeeId, prodReqId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpGet("getTerritoryOfficerByPartyId")]
        public async Task<IActionResult> getTerritoryOfficerByPartyId(int? partyId)
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

            var datajson = await productRequisitionService.getTerritoryOfficerByPartyId(partyId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        /*
                [HttpPost("DeleteProductReqById")]
                public async Task<IActionResult> DeleteProductReqById([FromBody] PurProductRequisitionViewModel model)
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

                    if (model.prodReqId <= 0)
                    {
                        var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Product Req. has not deleted.", false);
                        return new OkObjectResult(jwt);
                    }
                    bool result = await purProductRequisitionService.DeleteProductReqById(user.employeeId.ToString(), (int)model.prodReqId);

                    if (result == true)
                    {
                        var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Product Req. has deleted successfully.", true);
                        return new OkObjectResult(jwt);
                    }
                    else
                    {
                        var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Product Req. has not deleted.", false);
                        return new OkObjectResult(jwt);
                    }
                }

                */

        [HttpPost("DeleteProductReqById")]
        public async Task<IActionResult> DeleteProductReqById(int prodReqId)
        {
            //var uid = Request.Headers["auth_token"];
            //if (uid.Count() == 0)
            //{
            //    bool status = false;
            //    string actionresult = "Invalid Token.";
            //    var jwts = await Tokens.changePasswordJwt(status, actionresult, new JsonSerializerSettings { Formatting = Formatting.Indented });
            //    //return new OkObjectResult(jwts);
            //}
            //var stream = uid;
            //var handler = new JwtSecurityTokenHandler();

            //var tokenS = handler.ReadToken(stream) as JwtSecurityToken;
            //var jti = tokenS.Claims.First(claim => claim.Type == "Id").Value;
            //var user = await userInfoes.GetUserBasicInfoesbyId(jti);
            //if (user.token != uid && user != null)
            //{
            //    bool status = false;
            //    string actionresult = "Invalid Token.";
            //    var jwts = await Tokens.changePasswordJwt(status, actionresult, new JsonSerializerSettings { Formatting = Formatting.Indented });
            //    //return new OkObjectResult(jwts);
            //}

            if (prodReqId <= 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Product Req. has not deleted.", false);
                return new OkObjectResult(jwt);
            }
            bool result = await productRequisitionService.DeleteProductReqById("0", prodReqId);// All delete Method do not have token.

            if (result == true)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Product Req. has deleted successfully.", true);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Product Req. has not deleted.", false);
                return new OkObjectResult(jwt);
            }
        }


        [HttpGet("GetProductCurrentStockBySbuId")]
        public async Task<IActionResult> GetProductCurrentStockBySbuId(int productWiseSpecificationId, int sbuId)
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

            var datajson = await productRequisitionService.GetProductCurrentStockBySbuId(productWiseSpecificationId, sbuId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }


        #endregion

        #region Product Req. Details

        [HttpGet("GetProductReqDetails")]
        public async Task<IActionResult> GetProductReqDetails(int? prodReqId)//(int? productReqDetailsId)
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

            var datajson = await productRequisitionService.GetProductReqDetailsById(prodReqId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpPost("DeleteProductReqDetailsById")]
        public async Task<IActionResult> DeleteProductReqDetailsById(int productReqDetailsId)
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


            bool result = await productRequisitionService.DeleteProductReqDetailsById(user.employeeId.ToString(), productReqDetailsId);

            if (result == true)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Product Req. Details has deleted successfully.", true);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Product Req. Details has not deleted.", false);
                return new OkObjectResult(jwt);
            }
        }
        #endregion

        #region Product Report ---------------------
        [HttpGet("getRptGridProductReq")]
        public async Task<IActionResult> GetRptGridProductReq(int? prodReqId)
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

            var datajson = await productRequisitionService.GetRptGridProductReq(prodReqId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }
        #endregion

        [HttpGet("getProductApprovedRequisition")]
        public async Task<IActionResult> getProductApprovedRequisition(int? approvedStatus, int? finalizeMasterId)
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

            var datajson = await productRequisitionService.GetProductApprvedRequisition((int)user.employeeId, approvedStatus, finalizeMasterId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }
    }
}