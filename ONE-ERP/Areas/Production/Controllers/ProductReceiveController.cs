using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ONEERP.Areas.Production.Models;
using ONEERP.Data.Entity;
using ONEERP.ERPService.AuthService.Interfaces;
using ONEERP.ERPServices.Production.Interfaces;
using ONEERP.Helpers;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.Production.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductReceiveController : ControllerBase
    {
        object jwts;
        ApplicationUser user;
        private readonly IUserInfoes userInfoes;
        private readonly IProductionIssueAndReceived service;
        public ProductReceiveController(IUserInfoes _userInfoes, IProductionIssueAndReceived productionIssueService)
        {
            userInfoes = _userInfoes;
            jwts = new object();
            user = new ApplicationUser();
            service = productionIssueService;
        }


        [HttpGet("GetIssueNumberforIssue")]
        public async Task<IActionResult> GetIssueNumberforIssue(int type)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            var datajson = await service.GetIssueNoForReceive(type, (int)user.employeeId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpGet("GetIssueDataById")]
        public async Task<IActionResult> GetIssueDataById(int? issueId)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            var datajson = await service.GetIssueDataById(issueId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }


        [HttpGet("GetIssueDetailsByMasterIdForReceive")]
        public async Task<IActionResult> GetIssueDetailsByMasterIdForReceive(int? issueId)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            var datajson = await service.GetIssueDetailsByMasterIdForReceive(issueId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpPost("SaveReceiveMaster")]
        public async Task<IActionResult> SaveReceiveMaster([FromBody] ProductionReceiveViewModel model)
        {
            int result = 0;
            int flag = model.productReceiveMasterId;
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            if (model == null)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Product Receive has not created.", false);
                return new OkObjectResult(jwt);
            }

            int receiveId = await service.SaveReceiveMaster(user.employeeId.ToString(), model);

            if (receiveId == 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Product Receive has not created.", false);
                return new OkObjectResult(jwt);
            }
            if (flag == 0)
            {
                result = await service.SaveReceiveDetails(user.employeeId.ToString(), model.lstDetailsViewModel, receiveId);
            }



            ///int result = await service.CreateAutoJournalForBOM(user.employeeId.ToString(), model);
            ///


            if (result != 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Product Receive Details has created successfully.", true);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Product Receive Details has not created.", false);
                return new OkObjectResult(jwt);
            }
        }

        [HttpGet("GetReceiveMasterById")]
        public async Task<IActionResult> GetReceiveMasterById(int? receiveId)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            var datajson = await service.GetReceiveById(receiveId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }
        [HttpGet("GetReceiveMasterByIdDate")]
        public async Task<IActionResult> GetReceiveMasterByIdDate(DateTime fromDate, DateTime toDate, int? receiveId)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            var datajson = await service.GetReceiveByIdDate(user.employeeId, fromDate, toDate, receiveId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }
        [HttpPost("DeleteReceiveMasterById")]
        public async Task<IActionResult> DeleteReceiveMasterById([FromBody] int receiveId)
        {

            if (Authentication().Result == false) return new OkObjectResult(jwts);

            if (receiveId <= 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Product Receive Master has not deleted.", false);
                return new OkObjectResult(jwt);
            }
            string result = await service.DeleteReceiveById(user.employeeId.ToString(), receiveId);

            if (result == "success")
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Product Receive Master has deleted successfully.", true);
                return new OkObjectResult(jwt);
            }
            if (result != null)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, result, false);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Product Receive Master has not deleted.", false);
                return new OkObjectResult(jwt);
            }
        }



        [HttpGet("GetReceiveDetailsByMasterId")]
        public async Task<IActionResult> GetReceiveDetailsByMasterId(int? receiveId)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            var datajson = await service.GetReceiveDetailsByMasterId(receiveId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }


       

        [HttpGet("GetMaxReceiveMasterNumber")]
        public async Task<IActionResult> GetMaxIssueMasterNumber(DateTime bomDate, int type)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            var datajson = await service.GetMaxReceiveMasterNumber(bomDate, type);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }
        [HttpGet("GetMaxReturnMasterNumber")]
        public async Task<IActionResult> GetMaxReturnMasterNumber(DateTime ReturnDate, int type)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            var datajson = await service.GetMaxReturnMasterNumber(ReturnDate, type);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpGet("GetRequisitionNumberforReturn")]
        public async Task<IActionResult> GetRequisitionNumberforReturn(int type)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            var datajson = await service.GetRequisitionNumberforReturn(type, (int)user.employeeId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }
        [HttpGet("GetRMPMReturnDetailsByReqMasterId")]
        public async Task<IActionResult> GetRMPMReturnDetialsByReqMasterId(int? requisitionId)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            var datajson = await service.GetRMPMReturnDetailsByReqMasterId(requisitionId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }
        [HttpPost("SaveProductReturn")]
        public async Task<IActionResult> SaveProductReturn([FromBody] ProductionReturnViewModel model)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            if (model == null)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Product Return has not created.", false);
                return new OkObjectResult(jwt);
            }

            int returnId = await service.SaveProductReturn(user.employeeId.ToString(), model);

            if (returnId == 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Product Return has not created.", false);
                return new OkObjectResult(jwt);
            }

            int result = await service.SaveProductReturnDetails(user.employeeId.ToString(), model.lstDetailsViewModel, returnId);

            ///int result = await service.CreateAutoJournalForBOM(user.employeeId.ToString(), model);
            ///


            if (result != 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Product Receive Details has created successfully.", true);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Product Receive Details has not created.", false);
                return new OkObjectResult(jwt);
            }
        }
        [HttpGet("GetReturnMasterByIdDate")]
        public async Task<IActionResult> GetReturnMasterByIdDate(DateTime fromDate, DateTime toDate, int? returnId)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            var datajson = await service.GetReturnByIdDate(fromDate, toDate, returnId, (int)user.employeeId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpPost("DeleteReturnMasterById")]
        public async Task<IActionResult> DeleteReturnMasterById([FromBody] int ReturnMasterId)
        {

            if (Authentication().Result == false) return new OkObjectResult(jwts);

            if (ReturnMasterId <= 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Product Return Master has not deleted.", false);
                return new OkObjectResult(jwt);
            }
            string result = await service.DeleteReturnMasterById(user.employeeId.ToString(), ReturnMasterId);

            if (result == "success")
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Product Return Master has deleted successfully.", true);
                return new OkObjectResult(jwt);
            }
            if (result != null)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, result, false);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Product Return Master has not deleted.", false);
                return new OkObjectResult(jwt);
            }
        }
        [HttpGet("GetReturnDetailsByReturnMasterId")]
        public async Task<IActionResult> GetReturnDetailsByReturnMasterId(int? ProductReturnMasterId)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            var datajson = await service.GetReturnDetailsByReturnMasterId(ProductReturnMasterId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }
        [HttpPost("SaveProductReceiveFromReturn")]
        public async Task<IActionResult> SaveProductReceiveFromReturn([FromBody] ProductReceiveFromReturnViewModel model)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            if (model == null)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Product Receive from Return has not created.", false);
                return new OkObjectResult(jwt);
            }

            int returnId = await service.SaveProductReceiveFromReturn(user.employeeId.ToString(), model);

            if (returnId == 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Product Receive from Return has not created.", false);
                return new OkObjectResult(jwt);
            }

            int result = await service.SaveProductReceiveFromReturnDetails(user.employeeId.ToString(), model.lstDetailsViewModel, returnId);

            ///int result = await service.CreateAutoJournalForBOM(user.employeeId.ToString(), model);
            ///


            if (result != 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Product Receive Details has created successfully.", true);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Product Receive Details has not created.", false);
                return new OkObjectResult(jwt);
            }
        }
        [HttpGet("GetReturnFromReceiveByIdDate")]
        public async Task<IActionResult> GetReturnFromReceiveByIdDate(DateTime fromDate, DateTime toDate, int? ProductReceiveFromReturnMasterId)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            var datajson = await service.GetReturnFromReceiveByIdDate(fromDate, toDate, ProductReceiveFromReturnMasterId, (int)user.employeeId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpPost("DeleteProductReceiveFromReturnById")]
        public async Task<IActionResult> DeleteProductReceiveFromReturnById([FromBody] int ProductReceiveFromReturnMasterId)
        {

            if (Authentication().Result == false) return new OkObjectResult(jwts);

            if (ProductReceiveFromReturnMasterId <= 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Production Receive From Return Master has not deleted.", false);
                return new OkObjectResult(jwt);
            }
            string result = await service.DeleteProductReceiveFromReturnById(user.employeeId.ToString(), ProductReceiveFromReturnMasterId);

            if (result == "success")
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Product Receive from Return Master has deleted successfully.", true);
                return new OkObjectResult(jwt);
            }
            if (result != null)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, result, false);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Product Return Master has not deleted.", false);
                return new OkObjectResult(jwt);
            }
        }
        [HttpGet("GetProductReceiveFromReturnDetails")]
        public async Task<IActionResult> GetProductReceiveFromReturnDetails(int? ProductReceiveFromReturnMasterId)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            var datajson = await service.GetProductReceiveFromReturnDetails(ProductReceiveFromReturnMasterId);
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
    }
}
