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
    public class PurchaseRequisitionController : Controller
    {
        private IUserInfoes userInfoes;
        private TokenAuthenticator authenticator;
        private readonly IPurchaseRequisitionService purRequisitionService;
        public PurchaseRequisitionController(IUserInfoes userInfoes, IPurchaseRequisitionService purRequisitionService)
        {
            this.userInfoes = userInfoes;
            this.purRequisitionService = purRequisitionService;
            authenticator = new TokenAuthenticator(userInfoes);
        }

        #region Purchase Req.

        [HttpPost("setPurchaseRequisition")]
        public async Task<IActionResult> setPurchaseRequisition([FromBody] PurchaseRequisitionViewModel model)
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



            if (model.toWarehouseId == 0 && model.lstReqDetailsViewModel.Count() == 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Purchase To Warehouse or Req. Details is empty! Purchase Req. has not created.", false);
                return new OkObjectResult(jwt);
            }

            int result = 0;
            int prodReqId = await purRequisitionService.SavePurchaseReq(user.employeeId.ToString(), model);

            if (prodReqId <= 0)
            {
                var msg = prodReqId == 0 ? "Purchase Req. has not created." : "Req. not allowed! Approval Matrix not found for your select Product Type!";
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, msg, false);
                return new OkObjectResult(jwt);
            }

            result = await purRequisitionService.SavePurchaseReqDetails(user.employeeId.ToString(), model.lstReqDetailsViewModel, prodReqId);

            if (result != 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Purchase Req. Details has created successfully.", true);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Purchase Req. Details has not created.", false);
                return new OkObjectResult(jwt);
            }
        }
        [HttpGet("IsPurchaseRequisitionFinalisedByPRId")]
        public async Task<IActionResult> IsPurchaseRequisitionFinalisedByPRId(int? purchaseReqId)
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

            var datajson = await purRequisitionService.IsPurchaseRequisitionFinalisedByPRId(purchaseReqId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }
        [HttpGet("getPurchaseRequisition")]
        public async Task<IActionResult> getPurchaseRequisition(int? purchaseReqId, int? isHo)
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

            var datajson = await purRequisitionService.GetPurchaseReqById(user.employeeId, purchaseReqId, isHo);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpPost("DeletePurchaseReqById")]
        public async Task<IActionResult> DeletePurchaseReqById(int purchaseReqId)
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

            if (purchaseReqId <= 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Purchase Req. has not deleted.", false);
                return new OkObjectResult(jwt);
            }
            bool result = await purRequisitionService.DeletePurchaseReqById(user.employeeId.ToString(), purchaseReqId);

            if (result == true)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Purchase Req. has deleted successfully.", true);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Purchase Req. has not deleted.", false);
                return new OkObjectResult(jwt);
            }
        }


        #endregion

        #region Comparative Statement

        [HttpPost("SetComparativeStatement")]
        public async Task<IActionResult> SetComparativeStatement([FromBody] ComparativeStatementMasterViewModel model)
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



            if (model.lstCSDetailsViewModel.Count() == 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "CS  not created", false);
                return new OkObjectResult(jwt);
            }

            int result = 0;
            int cSMasterId = await purRequisitionService.SaveComparativeStatement(user.employeeId.ToString(), model);

            if (cSMasterId == 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "CS. has not created.", false);
                return new OkObjectResult(jwt);
            }

            result = await purRequisitionService.SaveComparativeStatementDetails(user.employeeId.ToString(), model.lstCSDetailsViewModel, cSMasterId);

            if (result != 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "CS  has created successfully.", true);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "CS  Details has not created.", false);
                return new OkObjectResult(jwt);
            }
        }

        [HttpGet("GetComparativeStatementById")]
        public async Task<IActionResult> GetComparativeStatementById(int? csMasterId)
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

            var datajson = await purRequisitionService.GetComparativeStatementById(user.employeeId, csMasterId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);

        }

        [HttpPost("DeleteComparativeStatementById")]
        public async Task<IActionResult> DeleteComparativeStatementById([FromBody] int csMasterId)
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

            if (csMasterId <= 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Purchase Req. has not deleted.", false);
                return new OkObjectResult(jwt);
            }
            bool result = await purRequisitionService.DeleteComparativeStatementById(user.employeeId.ToString(), csMasterId);

            if (result == true)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Purchase Req. has deleted successfully.", true);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Purchase Req. has not deleted.", false);
                return new OkObjectResult(jwt);
            }
        }

        [HttpGet("GetCSListForApproval")]
        public async Task<IActionResult> GetCSListForApproval(int csId, int approvalStatus)
        {
            var AuthModel = await authenticator.AuthenticationStatus(Request.Headers["auth_token"]);
            if (AuthModel.IsAuthorized == false) return new OkObjectResult(AuthModel.Jwts);
            var employeeId = AuthModel.ApplicationUserInfo.employeeId.ToString();

            var datajson = await purRequisitionService.GetCSListForApproval(employeeId, csId, approvalStatus);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }


        [HttpPost("UpdateComparativeStatementForApproval")]
        public async Task<IActionResult> UpdateComparativeStatementForApproval([FromBody] ComparativeStatementMasterViewModel model)
        {
            var AuthModel = await authenticator.AuthenticationStatus(Request.Headers["auth_token"]);
            if (AuthModel.IsAuthorized == false) return new OkObjectResult(AuthModel.Jwts);

            #region common

            #endregion

            if (model.lstCSDetailsViewModel.Count() == 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Comparative Statement Not Found.", false);
                return new OkObjectResult(jwt);
            }
            int result = await purRequisitionService.UpdateCSMasterStatus(AuthModel.ApplicationUserInfo.employeeId.ToString(), model.ApprovalStatus, model.lstCSDetailsViewModel);

            if (result > 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Comparative Statement has Approved successfully.", true);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Comparative Statement  has not Approved.", false);
                return new OkObjectResult(jwt);
            }
        }

        [HttpGet("GetAllComparativeStatementsbyStatus")]
        public async Task<IActionResult> GetAllComparativeStatementsbyStatus(int approvalStatus, int quotationTypeId)
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

            var datajson = await purRequisitionService.GetAllComparativeStatementsbyStatus(approvalStatus, quotationTypeId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);

        }


        [HttpGet("GetAllComparativeStatementsForLCbyStatus")]
        public async Task<IActionResult> GetAllComparativeStatementsForLCbyStatus(int approvalStatus, int quotationTypeId)
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

            var datajson = await purRequisitionService.GetAllComparativeStatementsForLCbyStatus(approvalStatus, quotationTypeId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);

        }
        [HttpGet("GetCSDetailsbyMasterId")]
        public async Task<IActionResult> GetCSDetailsbyMasterId(int? csMasterId, int? supplierId)
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

            var datajson = await purRequisitionService.GetCSDetailsbyMasterId(csMasterId, supplierId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);

        }
        #endregion

        #region Final Purchase Req.
        [HttpPost("SaveFinalRequisition")]
        public async Task<IActionResult> SaveFinalRequisition([FromBody] RequisitionFinalMasterViewModel model)
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



            if (model.lstApproveReqViewModel != null && model.lstApproveReqViewModel.Count() == 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Purchase Final Requisiton not created", false);
                return new OkObjectResult(jwt);
            }

            int result = 0;
            int purFinalReqId = await purRequisitionService.SavePurchaseFianlReq(user.employeeId.ToString(), model);

            if (purFinalReqId == 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Purchase Req. has not created.", false);
                return new OkObjectResult(jwt);
            }

            result = await purRequisitionService.SavePurchaseFianlReqDetails(user.employeeId.ToString(), model.lstApproveReqViewModel, purFinalReqId);

            if (result != 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Purchase Final Req. Details has created successfully.", true);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Purchase Fianl Req. Details has not created.", false);
                return new OkObjectResult(jwt);
            }
        }

        [HttpGet("GetPurchaseFinalRequisitionById")]
        public async Task<IActionResult> GetPurchaseFinalRequisitionById(int? finalRequisitionId)
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

            var datajson = await purRequisitionService.GetPurchaseFinalReqById(user.employeeId, finalRequisitionId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);

        }


        [HttpGet("IsFinalisedRequisitionWordOrderedByFRId")]
        public async Task<IActionResult> IsFinalisedRequisitionWordOrderedByFRId(int? finalRequisitionId)
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

            var datajson = await purRequisitionService.isFinalisedRequisitionWordOrderedByFRId(finalRequisitionId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);

        }

        [HttpGet("GetPurchaseFinalRequisitionDetailsByMasterIdForPdfReport")]
        public async Task<IActionResult> GetPurchaseFinalRequisitionDetailsByMasterIdForPdfReport(int? finalRequisitionId)
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

            var datajson = await purRequisitionService.GetPurchaseFinalReqDetailByMasterIdForPdfReport(finalRequisitionId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);

        }


        [HttpPost("DeleteFinalPurchaseReqById")]
        public async Task<IActionResult> DeleteFinalPurchaseReqById([FromBody] int finalRequisitionId)
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

            if (finalRequisitionId <= 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Purchase Req. has not deleted.", false);
                return new OkObjectResult(jwt);
            }
            bool result = await purRequisitionService.DeleteFinalPurchaseReqById(user.employeeId.ToString(), finalRequisitionId);

            if (result == true)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Purchase Req. has deleted successfully.", true);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Purchase Req. has not deleted.", false);
                return new OkObjectResult(jwt);
            }
        }


        [HttpGet("GetAllFinalizeRequisitions")]
        public async Task<IActionResult> GetAllFinalizeRequisitions(int? finalRequisitionId, int appStatus)
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

            var datajson = await purRequisitionService.GetAllFinalizedRequisitions(finalRequisitionId, appStatus);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);

        }

        [HttpGet("GetAllFinalizeRequisitionDetailByMasterId")]
        public async Task<IActionResult> GetAllFinalizeRequisitionDetailByMasterId(int? finalRequisitionId, int? supplierId)
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

            var datajson = await purRequisitionService.GetAllFinalizeRequisitionDetailByMasterId(finalRequisitionId, supplierId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);

        }
        #endregion

        #region Purchase Req. Details

        [HttpGet("GetPurchaseReqDetails")]
        public async Task<IActionResult> GetPurchaseReqDetails(int? PurchaseReqDetailsId)
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

            var datajson = await purRequisitionService.GetPurchaseReqDetailsById(PurchaseReqDetailsId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpGet("GetPurchaseReqDetailsByMasterId")]
        public async Task<IActionResult> GetPurchaseReqDetailsByMasterId(int? masterId)
        {
            var datajson = await purRequisitionService.GetPurchaseReqDetailsByMasterId(masterId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpPost("DeletePurchaseReqDetailsById")]
        public async Task<IActionResult> DeletePurchaseReqDetailsById(int PurchaseReqDetailsId)
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


            bool result = await purRequisitionService.DeletePurchaseReqDetailsById(user.employeeId.ToString(), PurchaseReqDetailsId);

            if (result == true)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Purchase Req. Details has deleted successfully.", true);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Purchase Req. Details has not deleted.", false);
                return new OkObjectResult(jwt);
            }
        }

        [HttpGet("getRequisitionRevision")]
        public async Task<IActionResult> getRequisitionRevision()
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

            var datajson = await purRequisitionService.getRequisitionRevision();
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        #endregion

        #region Purchase Req. Approval

        [HttpPost("ApprovePurchaseRequisitionMaster")]
        public async Task<IActionResult> ApprovePurchaseRequisitionMaster([FromBody] PurchaseRequisitionViewModel model)
        {
            var AuthModel = await authenticator.AuthenticationStatus(Request.Headers["auth_token"]);
            if (AuthModel.IsAuthorized == false) return new OkObjectResult(AuthModel.Jwts);

            #region common

            #endregion

            if (model.lstReqDetailsViewModel.Count() == 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Purchase Requisition Not Found.", false);
                return new OkObjectResult(jwt);
            }
            int result = await purRequisitionService.ApprovePurchaseReqMaster(AuthModel.ApplicationUserInfo.employeeId.ToString(), model.approvalStatus, model.lstReqDetailsViewModel);

            if (result > 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Purchase Requisition has Approved successfully.", true);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Purchase Requisition  has not Approved.", false);
                return new OkObjectResult(jwt);
            }
        }

        [HttpPost("UpdatePurchaseRequisitionDetails")]
        public async Task<IActionResult> UpdatePurchaseRequisitionDetails([FromBody] List<PurchaseReqDetailsViewModel> models)
        {
            var AuthModel = await authenticator.AuthenticationStatus(Request.Headers["auth_token"]);
            if (AuthModel.IsAuthorized == false) return new OkObjectResult(AuthModel.Jwts);
            var employeeId = AuthModel.ApplicationUserInfo.employeeId.ToString();

            var result = await purRequisitionService.UpdatePurchaseReqDetails(employeeId, models);
            //var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            //return new OkObjectResult(jwt);
            if (result == 1)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Sales Invoice Updated & Approved Successfully.", true);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Sales Invoice Updated & Approved Failed.", false);
                return new OkObjectResult(jwt);
            }
        }

        [HttpGet("GetPurchaseReqMasterListForApproval")]
        public async Task<IActionResult> GetPurchaseReqMasterListForApproval(int purchaseReqId)
        {
            var AuthModel = await authenticator.AuthenticationStatus(Request.Headers["auth_token"]);
            if (AuthModel.IsAuthorized == false) return new OkObjectResult(AuthModel.Jwts);
            var employeeId = AuthModel.ApplicationUserInfo.employeeId.ToString();

            var datajson = await purRequisitionService.GetPurchaseReqMasterListForApproval(employeeId, purchaseReqId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }


        [HttpGet("GetPurchaseRequisitionListByStatusJson")]
        public async Task<IActionResult> GetPurchaseRequisitionListByStatusJson(int status)
        {
            var AuthModel = await authenticator.AuthenticationStatus(Request.Headers["auth_token"]);
            if (AuthModel.IsAuthorized == false) return new OkObjectResult(AuthModel.Jwts);
            var employeeId = AuthModel.ApplicationUserInfo.employeeId.ToString();

            var datajson = await purRequisitionService.GetPurchaseReqMasterListByStatus(employeeId, status);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpGet("GetPurchaseRequisitionDetailsByIdForApproval")]
        public async Task<IActionResult> GetPurchaseRequisitionDetailsByIdForApproval(int purchaseReqId)
        {
            var AuthModel = await authenticator.AuthenticationStatus(Request.Headers["auth_token"]);
            if (AuthModel.IsAuthorized == false) return new OkObjectResult(AuthModel.Jwts);
            //var employeeId = AuthModel.ApplicationUserInfo.employeeId.ToString();

            var datajson = await purRequisitionService.GetPurchaseReqDetailsByIdForApproval(purchaseReqId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        #endregion

        #region purchase Requisition Report----------------

        [HttpGet("getPurchaseRequisitionGridReport")]
        public async Task<IActionResult> getPurchaseRequisitionGridReport(int? purchaseReqId)
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

            var datajson = await purRequisitionService.GetPurchaseRequisitionGridReport(purchaseReqId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }


        #endregion

        #region Quotation Collection

        [HttpGet("getQuotationCollection")]
        public async Task<IActionResult> getQuotationCollection(int? quotationCollectionId)
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

            var datajson = await purRequisitionService.GetQuotationCollectionById(user.employeeId, quotationCollectionId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpPost("setQuotationCollection")]
        public async Task<IActionResult> setQuotationCollection([FromBody] QuotationCollectionViewModel model)
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



            if (model.lstQuoDetailsViewModel.Count() == 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Purchase To Warehouse or Req. Details is empty! Purchase Req. has not created.", false);
                return new OkObjectResult(jwt);
            }

            int result = 0;
            int prodReqId = await purRequisitionService.SaveQuotationCollection(user.employeeId.ToString(), model);

            if (prodReqId == 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Purchase Req. has not created.", false);
                return new OkObjectResult(jwt);
            }

            result = await purRequisitionService.SaveQuotationCollDetails(user.employeeId.ToString(), model.lstQuoDetailsViewModel, prodReqId);

            if (result != 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Purchase Req. Details has created successfully.", true);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Purchase Req. Details has not created.", false);
                return new OkObjectResult(jwt);
            }
        }


        [HttpPost("DeleteQuotationCollectionById")]
        public async Task<IActionResult> DeleteQuotationCollectionById([FromBody] int quotationCollectionId)
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

            if (quotationCollectionId <= 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Purchase Req. has not deleted.", false);
                return new OkObjectResult(jwt);
            }
            bool result = await purRequisitionService.DeleteQuotationCollectionById(user.employeeId.ToString(), quotationCollectionId);

            if (result == true)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Purchase Req. has deleted successfully.", true);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Purchase Req. has not deleted.", false);
                return new OkObjectResult(jwt);
            }
        }

        #endregion

        #region Quotation Collection Details

        [HttpGet("GetQuotationCollDetails")]
        public async Task<IActionResult> GetQuotationCollDetails(int? quotationCollDetailsId)
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

            var datajson = await purRequisitionService.GetQuotationCollDetailsById(quotationCollDetailsId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpGet("GetQuotationCollDetailsByMasterId")]
        public async Task<IActionResult> GetQuotationCollDetailsByMasterId(int? masterId)
        {
            var datajson = await purRequisitionService.GetQuotationCollDetailsByMasterId(masterId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpPost("DeleteQuotationCollDetailsById")]
        public async Task<IActionResult> DeleteQuotationCollDetailsById([FromBody] int quotationCollDetailsIdDetailsId)
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


            bool result = await purRequisitionService.DeleteQuotationCollDetailsById(user.employeeId.ToString(), quotationCollDetailsIdDetailsId);

            if (result == true)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Purchase Req. Details has deleted successfully.", true);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Purchase Req. Details has not deleted.", false);
                return new OkObjectResult(jwt);
            }
        }


        #endregion


        #region Purchase requisition Approval Matrix

        [HttpPost("SavePurchaseApprovalMatrix")]
        public async Task<IActionResult> SavePurchaseLeaveApprovalMatrix([FromBody] PurchaseApprovalMatrixViewModel model)
        {
            try
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

                if (model.lstDetails!=null && model.lstDetails.Count() == 0)
                {
                    var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "ApprovalMatrix has not created.", false);
                    return new OkObjectResult(jwt);
                }
                int result = 0;
                if (model.departmentId == null) model.departmentId = 0;
                if (model.employeeId == null) model.employeeId = 0;

                result = await purRequisitionService.SavePurchaseApprovalMatrix(user.employeeId.ToString(), model.lstDetails, model.employeeId, model.departmentId, model.productTypeId);


                if (result != 0)
                {
                    var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "ApprovalMatrix has created successfully.", true);
                    return new OkObjectResult(jwt);
                }
                else
                {
                    var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "ApprovalMatrix has not created.", false);
                    return new OkObjectResult(jwt);
                }
            }
            catch (Exception ex)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "ApprovalMatrix has not created.", false);
                return new OkObjectResult(jwt);
            }

        }

        [HttpGet("GetPurchaseApprovalMatrix")]
        public async Task<IActionResult> GetPurchaseApprovalMatrix(int? employeeId, int? productTypeId)
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

            var datajson = await purRequisitionService.GetPurchaseApprovalMatrix(user.employeeId, employeeId,  productTypeId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpGet("DeletePurchaseApprovalMatrixByemployeeId")]
        public async Task<IActionResult> DeletePurchaseApprovalMatrixByemployeeId(int employeeId, int productTypeId)
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

            if (employeeId <= 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "ApprovalMatrix has not deleted.", false);
                return new OkObjectResult(jwt);
            }
            bool result = await purRequisitionService.DeletePurchaseApprovalMatrixByTypeId(user.employeeId.ToString(), employeeId, productTypeId);

            if (result == true)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "ApprovalMatrix has deleted successfully.", true);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "ApprovalMatrix has not deleted.", false);
                return new OkObjectResult(jwt);
            }
        }

        #endregion Purchase requisition Approval Matrix
    }
}