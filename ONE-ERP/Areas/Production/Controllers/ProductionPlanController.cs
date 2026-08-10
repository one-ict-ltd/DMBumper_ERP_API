using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ONEERP.Areas.Production.Models;
using ONEERP.Data.Entity;
using ONEERP.ERPService.AuthService.Interfaces;
using ONEERP.ERPServices.Production.Interfaces;
using ONEERP.ERPServices.Purchase.Interfaces;
using ONEERP.Helpers;


namespace ONEERP.Areas.Production.Controllers
{
    [Route("api/[controller]")]
    public class ProductionPlanController : Controller
    {
        object jwts;
        ApplicationUser user;
        private IUserInfoes userInfoes;
        private TokenAuthenticator authenticator;
        private readonly IProductionPlanService productionPlanService;
        public ProductionPlanController(IUserInfoes userInfoes, IProductionPlanService _productionPlanService)
        {
            this.userInfoes = userInfoes;
            this.productionPlanService = _productionPlanService;
            authenticator = new TokenAuthenticator(userInfoes);
            jwts = new object();
            user = new ApplicationUser();
        }

        [HttpGet("GetProductionPlanById")]
        public async Task<IActionResult> GetProductionPlanById(int? planId)
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

            var datajson = await productionPlanService.GetProductionPlanById(planId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }
        [HttpGet("GetProductionPlanByIdwithDate")]
        public async Task<IActionResult> GetProductionPlanByIdwithDate(DateTime fromDate, DateTime toDate, int? planId)
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

            var datajson = await productionPlanService.GetProductionPlanByIdWithDate(fromDate, toDate, planId, user.employeeId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }
        [HttpPost("SaveProductionPlan")]
        public async Task<IActionResult> SaveProductionPlan([FromBody] ProductionPlanViewModel model)
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

            //int result = 0;
            int productionPlanId = await productionPlanService.SaveProductionPlan(user.employeeId.ToString(), model);

            if (productionPlanId == 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Production Plan  has not created.", false);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, " Production Plan  has  created.", true);
                return new OkObjectResult(jwt);
            }
        }

        [HttpPost("DeleteProductionPlanById")]
        public async Task<IActionResult> DeleteProductionPlanById([FromBody] int? planId)
        {

            if (Authentication().Result == false) return new OkObjectResult(jwts);

            if (planId <= 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Production Plan has not deleted.", false);
                return new OkObjectResult(jwt);
            }
            string result = await productionPlanService.DeleteProductionPlan(user.employeeId.ToString(), planId);

            if (result == "success")
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Production Plan has deleted successfully.", true);
                return new OkObjectResult(jwt);
            }
            if (result != null && result != "success")
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, result, false);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Production Plan has not deleted.", false);
                return new OkObjectResult(jwt);
            }
        }
        [HttpPost("DeleteProductionProcessQaById")]
        public async Task<IActionResult> DeleteProductionProcessQaById([FromBody] int? productionQaId)
        {

            if (Authentication().Result == false) return new OkObjectResult(jwts);

            if (productionQaId <= 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Production Process QA has not deleted.", false);
                return new OkObjectResult(jwt);
            }
            string result = await productionPlanService.DeleteProductionProcessQaById(user.employeeId, productionQaId);

            if (result == "success")
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Production Process QA has deleted successfully.", true);
                return new OkObjectResult(jwt);
            }
            if (result != null && result != "success")
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, result, false);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Production Process QA has not deleted.", false);
                return new OkObjectResult(jwt);
            }
        }

        [HttpGet("GetBatchTypeById")]
        public async Task<IActionResult> GetBatchTypeById(int? batchTypeId)
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

            var datajson = await productionPlanService.GetBatchTypeById(batchTypeId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpGet("CheckDuplicatedBatchNo")]
        public async Task<IActionResult> CheckDuplicatedBatchNo(int? planId, string batchNo)
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

            var datajson = await productionPlanService.CheckDuplicatedBatchNo(planId, batchNo);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpGet("GetProductionPlanForRequisition")]
        public async Task<IActionResult> GetProductionPlanForRequisition(int? planId)
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

            var datajson = await productionPlanService.GetProductionPlanForRequisition(planId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }


        [HttpGet("GetProductionPlanForRequisitionWithType")]
        public async Task<IActionResult> GetProductionPlanForRequisitionWithType(int? planId, string bomType)
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

            var datajson = await productionPlanService.GetProductionPlanForRequisitionWithType(planId, bomType);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpGet("GetProductionPlanForProdProcess")]
        public async Task<IActionResult> GetProductionPlanForProdProcess(int? planId)
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

            var datajson = await productionPlanService.GetProductionPlanForProdProcess(planId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpGet("GetProductionPlanBatch")]
        public async Task<IActionResult> GetProductionPlanBatch(int? planId)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            var datajson = await productionPlanService.GetProductionPlanBatch(planId, (int)user.employeeId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }
        [HttpGet("GetProductionProcessBatch")]
        public async Task<IActionResult> GetProductionProcessBatch(int? prdPlanProcessId)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            var datajson = await productionPlanService.GetProductionProcessBatch(prdPlanProcessId, (int)user.employeeId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpGet("GetProductionPlanForStockIn")]
        public async Task<IActionResult> GetProductionPlanForStockIn(int? planId)
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

            var datajson = await productionPlanService.GetProductionPlanForStockIn(planId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }
        [HttpGet("GetCheckManufacturingAndPackingProcessComplete")]
        public async Task<IActionResult> GetCheckManufacturingAndPackingProcessComplete(int? planId)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            var datajson = await productionPlanService.GetCheckManufacturingAndPackingProcessComplete(planId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }
        [HttpGet("GetProductionPlanWithType")]
        public async Task<IActionResult> GetProductionPlanWithType(int? planId, string bomType)
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

            var datajson = await productionPlanService.GetProductionPlanWithType(user.employeeId, planId, bomType);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpGet("GetProductionPlanByIdForApproval")]
        public async Task<IActionResult> GetProductionPlanByIdForApproval(int? planId)
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

            var datajson = await productionPlanService.GetProductionPlanByIdForApproval((int)user.employeeId, planId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpPost("UpdateProductionPlanForApproval")]
        public async Task<IActionResult> UpdateProductionPlanForApproval([FromBody] ProductionPlanApprovalViewModel model)
        {
            var AuthModel = await authenticator.AuthenticationStatus(Request.Headers["auth_token"]);
            if (AuthModel.IsAuthorized == false) return new OkObjectResult(AuthModel.Jwts);

            #region common

            #endregion

            if (model.lstPlanDetailsViewModel.Count() == 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Comparative Statement Not Found.", false);
                return new OkObjectResult(jwt);
            }
            int result = await productionPlanService.UpdateProductionPlanForApproval(user.employeeId.ToString(), model.ApprovalStatus, model.lstPlanDetailsViewModel);

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


        [HttpGet("GetAllQcQaParameterList")]
        public async Task<IActionResult> GetAllQcQaParameterList()
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            var datajson = await productionPlanService.GetAllQcQaParameterList((int)user.employeeId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpGet("GetPredefineParameterFormat")]
        public async Task<IActionResult> GetPredefineParameterFormat(int productionPlanId)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            var datajson = await productionPlanService.GetPredefineParameterFormat((int)user.employeeId, productionPlanId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
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

        #region transferNote
        [HttpGet("GetTransferedProductionProcessBatch")]
        public async Task<IActionResult> GetTransferedProductionProcessBatch(int? prdPlanProcessId)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            var datajson = await productionPlanService.GetTransferedProductionProcessBatch(prdPlanProcessId, (int)user.employeeId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }
        [HttpGet("GetMaxTransferNoteNumber")]
        public async Task<IActionResult> GetMaxTransferNoteNumber(DateTime transferDate)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            var datajson = await productionPlanService.GetMaxTransferNoteNumber(transferDate);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }
        [HttpPost("SaveTransferNote")]
        public async Task<IActionResult> SaveTransferNote([FromBody] TransferNoteViewModel model)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);
            //int result = 0;
            int productTransferId = await productionPlanService.SaveTransferNote(user.employeeId.ToString(), model);

            if (productTransferId == 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Transfer Note has not created.", false);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Transfer Note has created.", true);
                return new OkObjectResult(jwt);
            }
        }
        //GetTransferNoteById
        [HttpGet("GetTransferNoteById")]
        public async Task<IActionResult> GetTransferNoteById(int? productTransferId, DateTime? fDate, DateTime? tDate)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            var datajson = await productionPlanService.GetTransferNoteById(user.employeeId, productTransferId, fDate, tDate);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpGet("GetTransferNoteListForStockIn")]
        public async Task<IActionResult> GetTransferNoteListForStockIn(int? productTransferId)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            var datajson = await productionPlanService.GetTransferNoteListForStockIn(user.employeeId, productTransferId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpPost("DeleteTransferNoteById")]
        public async Task<IActionResult> DeleteTransferNoteById([FromBody] int? productTransferId)
        {

            if (Authentication().Result == false) return new OkObjectResult(jwts);

            if (productTransferId <= 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Transfer Note has not deleted.", false);
                return new OkObjectResult(jwt);
            }
            bool result = await productionPlanService.DeleteTransferNoteById(user.employeeId, productTransferId);

            if (result == true)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Transfer Note has deleted successfully.", true);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Transfer Note has not deleted.", false);
                return new OkObjectResult(jwt);
            }
        }

        [HttpPost("UpdateTransferNote")]
        public async Task<IActionResult> UpdateTransferNote([FromBody] BatchReleaseViewModel model)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            if (model == null)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "No data found for Transfer Note Release.", true);
                return new OkObjectResult(jwt);
            }

            int productTransferId = await productionPlanService.UpdateTransferNote((int)user.employeeId, model.TransferDetailsList);

            if (productTransferId == 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Transfer Note has Released.", false);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Transfer Note has not Released.", true);
                return new OkObjectResult(jwt);
            }
        }

        [HttpGet("GetTransferNoteByIdForBatch")]
        public async Task<IActionResult> GetTransferNoteByIdForBatch(int? productTransferId)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            var datajson = await productionPlanService.GetTransferNoteByIdForBatch(user.employeeId, productTransferId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpPost("DeleteProcessMachineById")]
        public async Task<IActionResult> DeleteProcessMachineById([FromBody] int? prdPlanMachineId)
        {

            if (Authentication().Result == false) return new OkObjectResult(jwts);

            string result = await productionPlanService.DeleteProcessMachineById((int)user.employeeId, prdPlanMachineId);

            if (result == "success")
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Production Plan has deleted successfully.", true);
                return new OkObjectResult(jwt);
            }
            if (result != null && result != "success")
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, result, false);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Production Plan has not deleted.", false);
                return new OkObjectResult(jwt);
            }
        }
        #endregion
    }
}
