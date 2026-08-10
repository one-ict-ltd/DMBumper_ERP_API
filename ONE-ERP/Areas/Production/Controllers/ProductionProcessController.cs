using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ONEERP.Areas.Production.Models;
using ONEERP.Areas.Purchase.Models;
using ONEERP.Data.Entity;
using ONEERP.ERPService.AuthService.Interfaces;
using ONEERP.ERPServices.Production.Interfaces;
using ONEERP.ERPServices.Purchase.Interfaces;
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
    public class ProductionProcessController : ControllerBase
    {
        object jwts;
        ApplicationUser user;
        private IUserInfoes userInfoes;
        private TokenAuthenticator authenticator;
        private readonly IPurchaseRequisitionService purRequisitionService;
        private readonly IProductionProcessService productionProcessService;
        public ProductionProcessController(IUserInfoes userInfoes, IPurchaseRequisitionService purRequisitionService, IProductionProcessService processService)
        {
            this.userInfoes = userInfoes;
            this.purRequisitionService = purRequisitionService;
            this.productionProcessService = processService;
            authenticator = new TokenAuthenticator(userInfoes);
            jwts = new object();
            user = new ApplicationUser();
        }

        [HttpGet("GetProductionProcessHeadById")]
        public async Task<IActionResult> GetProductionProcessHeadById(int headId)
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

            var datajson = await productionProcessService.GetProductionProductionProcessHeadById(headId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpPost("SaveProductionProcessHead")]
        public async Task<IActionResult> SaveProductionProcessHead([FromBody] ProductionProcessHeadViewModel model)
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
            int purChargeHead = await productionProcessService.SaveProductionProcessHead(user.employeeId.ToString(), model);

            if (purChargeHead == 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Production Process Head  has not created.", false);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, " Production Process Head  has  created.", true);
                return new OkObjectResult(jwt);
            }
        }

        [HttpPost("DeleteProductionProcessById")]
        public async Task<IActionResult> DeleteProductionProcessById([FromBody] int productionPlanProcessId)
        {

            if (Authentication().Result == false) return new OkObjectResult(jwts);

            if (productionPlanProcessId <= 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Production Process  has not deleted.", false);
                return new OkObjectResult(jwt);
            }
            string result = await productionProcessService.DeleteProductionProcessById(user.employeeId.ToString(), productionPlanProcessId);

            if (result == "success")
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Production Process  has deleted successfully.", true);
                return new OkObjectResult(jwt);
            }
            if (result != null)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, result, false);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Production Process  has not deleted.", false);
                return new OkObjectResult(jwt);
            }
        }


        [HttpGet("GetMachineInfoById")]
        public async Task<IActionResult> GetMachineInfoById(int machineId)
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

            var datajson = await productionProcessService.GetMachineInfoById(user.employeeId,machineId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpPost("SaveMachineInfo")]
        public async Task<IActionResult> SaveMachineInfo([FromBody] MachineInfoViewModel model)
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
            int purChargeHead = await productionProcessService.SaveMachineInfo(user.employeeId.ToString(), model);

            if (purChargeHead == 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Machine Info  has not created.", false);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Machine Info  has  created.", true);
                return new OkObjectResult(jwt);
            }
        }

        [HttpPost("DeleteMachineInfoById")]
        public async Task<IActionResult> DeleteMachineInfoById([FromBody] int machineId)
        {

            if (Authentication().Result == false) return new OkObjectResult(jwts);

            if (machineId <= 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Machine Info  has not deleted.", false);
                return new OkObjectResult(jwt);
            }
            bool result = await productionProcessService.DeleteMachineInfo(user.employeeId.ToString(), machineId);

            if (result == true)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Machine Info has deleted successfully.", true);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Production Process head has not deleted.", false);
                return new OkObjectResult(jwt);
            }
        }


        #region ProcessGroup
        [HttpGet("GetProductionProcessGroupById")]
        public async Task<IActionResult> GetProductionProcessGroupById(int phGroupMasterId)
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

            var datajson = await productionProcessService.GetProductionProcessGroupById((int)user.employeeId, phGroupMasterId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpPost("SaveProductionProcessGroup")]
        public async Task<IActionResult> SaveProductionProcessGroup([FromBody] ProcessHeadGroupViewModel model)
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
            int phGroupMasterId = await productionProcessService.SaveProductionProcessGroup(user.employeeId.ToString(), model);
            if (phGroupMasterId == 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Production Process Group has not created.", false);
                return new OkObjectResult(jwt);
            }

            int result = await productionProcessService.SaveProcessGroupDetails(user.employeeId.ToString(), model.lstDetailsViewModel, phGroupMasterId);
            if (result != 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Process Group Details has created successfully.", true);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Process Group Details has not created.", false);
                return new OkObjectResult(jwt);
            }
        }

        [HttpPost("DeleteProductionProcessGroupById")]
        public async Task<IActionResult> DeleteProductionProcessGroupById([FromBody] int phGroupMasterId)
        {

            if (Authentication().Result == false) return new OkObjectResult(jwts);

            if (phGroupMasterId <= 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Production Process Head has not deleted.", false);
                return new OkObjectResult(jwt);
            }
            bool result = await productionProcessService.DeleteProductionProcessGroupById(user.employeeId.ToString(), phGroupMasterId);

            if (result == true)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Production Process Head has deleted successfully.", true);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Production Process head has not deleted.", false);
                return new OkObjectResult(jwt);
            }
        }

        #endregion
        #region ProcessGroup Details
        [HttpGet("GetProcessGroupDetailsById")]
        public async Task<IActionResult> GetProcessGroupDetailsById(int phGroupMasterId)
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

            var datajson = await productionProcessService.GetProcessGroupDetailsById(phGroupMasterId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpPost("DeleteProcessGroupDetailsById")]
        public async Task<IActionResult> DeleteProcessGroupDetailsById([FromBody] int phGroupDetailId)
        {

            if (Authentication().Result == false) return new OkObjectResult(jwts);

            if (phGroupDetailId <= 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "process head Group Details has not deleted.", false);
                return new OkObjectResult(jwt);
            }
            bool result = await productionProcessService.DeleteProcessGroupDetailsById(user.employeeId.ToString(), phGroupDetailId);

            if (result == true)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "BOM Details has deleted successfully.", true);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "BOM Details has not deleted.", false);
                return new OkObjectResult(jwt);
            }
        }
        #endregion

        #region Group Assign

        [HttpPost("SaveProductGroupAssign")]
        public async Task<IActionResult> SaveProductGroupAssign([FromBody] ProductGroupAssignViewModel model)
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

            bool isDeleted = await productionProcessService.DeleteProductGroupAssignByGroupMasterId(user.employeeId.ToString(), model.phGroupMasterId);

            int result = await productionProcessService.SaveProductGroupAssign(user.employeeId.ToString(), model.lstDetailsViewModel, model.phGroupMasterId, model.prdGroupAssignId);
            if (result != 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Product Group Assign has created successfully.", true);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Product Group Assign has not created.", false);
                return new OkObjectResult(jwt);
            }
        }

        [HttpGet("GetProductGroupAssignById")]
        public async Task<IActionResult> GetProductGroupAssignById(int prdGroupAssignId)
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

            var datajson = await productionProcessService.GetProductGroupAssignById(prdGroupAssignId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpGet("GetGroupWiseProductSpecs")]
        public async Task<IActionResult> GetGroupWiseProductSpecs(int phGroupMasterId)
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

            var datajson = await productionProcessService.GetGroupWiseProductSpecs(phGroupMasterId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        #endregion

        #region ProductionProcess

        [HttpGet("GetProductionPlanProcessById")]
        public async Task<IActionResult> GetProductionPlanProcessById(int? productionPlanId, int? productionTypeId, int? productWiseSpecificationId)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            var datajson = await productionProcessService.GetProductionPlanProcessById(user.employeeId, productionPlanId, productionTypeId, productWiseSpecificationId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpGet("GetProductionPlanMachineById")]
        public async Task<IActionResult> GetProductionPlanMachineById(int? prdPlanProcessId)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            var datajson = await productionProcessService.GetProductionPlanMachineById(user.employeeId, prdPlanProcessId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpGet("GetBatchWiseProcesses")]
        public async Task<IActionResult> GetBatchWiseProcesses(int productWiseSpecificationId, int productionTypeId)
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

            var datajson = await productionProcessService.GetBatchWiseProcesses(productWiseSpecificationId, productionTypeId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }


        [HttpPost("SaveProductionProcess")]
        public async Task<IActionResult> SaveProductionProcess([FromBody] List<ProductionPlanProcessViewModel> models)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            //int result = 0;
            int masterId = await productionProcessService.SaveProductionProcess(user.employeeId.ToString(), models);

            if (masterId == 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Production Plan Process has not created.", false);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Production Process QA has created.", true);
                return new OkObjectResult(jwt);
            }
        }
        [HttpGet("GetProductionQAById")]
        public async Task<IActionResult> GetProductionQAById(int productionQaId)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            var datajson = await productionProcessService.GetProductionQAById(productionQaId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }
        [HttpGet("GetProductionQAByIdDate")]
        public async Task<IActionResult> GetProductionQAByIdDate(DateTime fromDate, DateTime toDate, int productionQaId)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            var datajson = await productionProcessService.GetProductionQAByIdDate(user.employeeId, fromDate, toDate, productionQaId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }
        [HttpPost("SaveProductionQA")]
        public async Task<IActionResult> SaveProductionQA([FromBody] ProductionQaMasterViewModel model)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);
            int productionQaId = await productionProcessService.SaveProductionQA(user.employeeId, model);
            await productionProcessService.SaveProductionQADetail(user.employeeId, productionQaId, model.QCprocessList);
            if (productionQaId == 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Production Process QA has not created.", false);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Production Process QA has created.", true);
                return new OkObjectResult(jwt);
            }
        }

        [HttpPost("SaveProductionMachine")]
        public async Task<IActionResult> SaveProductionMachine([FromBody] List<ProductionPlanMachineViewModel> models)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            //int result = 0;
            int masterId = await productionProcessService.SaveProductionMachine(user.employeeId.ToString(), models);

            if (masterId == 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Production Plan Machine has not created.", false);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, " Production Plan Machine has created.", true);
                return new OkObjectResult(jwt);
            }
        }


        [HttpGet("SetProcessTransfer")]
        public async Task<IActionResult> SetProcessTransfer(int? productionPlanId, decimal? outputQty)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            //int result = 0;
            int masterId = await productionProcessService.SetProcessTransfer(user.employeeId, productionPlanId, outputQty);

            if (masterId == 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Process Transfer Failed.", false);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, " Process Transfer Completed.", true);
                return new OkObjectResult(jwt);
            }
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
