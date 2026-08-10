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
using ONEERP.Helpers;

namespace ONEERP.Areas.Production.Controllers
{
    [Route("api/[controller]")]
    public class BomController : Controller
    {
        object jwts;
        ApplicationUser user;
        private readonly IUserInfoes userInfoes;
        private readonly IBomMasterService service;
        public BomController(IUserInfoes _userInfoes, IBomMasterService _service)
        {
            userInfoes = _userInfoes;
            service = _service;
            jwts = new object();
            user = new ApplicationUser();
        }

        #region BOM
        [HttpGet("GetBomTypeIdByName")]
        public async Task<IActionResult> GetBomTypeIdByName(string bomType)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            var datajson = await service.GetBomTypeIdByName(bomType);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }
        [HttpPost("SaveBomMaster")]
        public async Task<IActionResult> SaveBomMaster([FromBody] BomPendingMasterViewModel model)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            if (model == null)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "BOM Master has not created.", false);
                return new OkObjectResult(jwt);
            }

            int pendingbomId = await service.SaveBomMaster(user.employeeId, model);

            if (pendingbomId == 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "BOM Master has not created.", false);
                return new OkObjectResult(jwt);
            }


            int result = await service.SaveBomDetails(user.employeeId, model.pendinglstDetailsViewModel, pendingbomId);

            ///int result = await service.CreateAutoJournalForBOM(user.employeeId.ToString(), model);

            if (result != 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "BOM Details has created successfully.", true);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "BOM Details has not created.", false);
                return new OkObjectResult(jwt);
            }
        }
       
        [HttpPost("SaveBomForApproval")]
        public async Task<IActionResult> SaveBomForApproval([FromBody] BomMasterViewModel model)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            int bomId = await service.SaveBomForApproval(user.employeeId, model.bomMaster);
            if (bomId == 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "BOM Master has not created.", false);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "BOM Master has created.", true);
                return new OkObjectResult(jwt);
            }
        }
        [HttpPost("DeleteBomMasterById")]
        public async Task<IActionResult> DeleteBomMasterById([FromBody] int bomId)
        {

            if (Authentication().Result == false) return new OkObjectResult(jwts);

            if (bomId <= 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "BOM Master has not deleted.", false);
                return new OkObjectResult(jwt);
            }
            bool result = await service.DeleteBomMasterById(user.employeeId, bomId);

            if (result == true)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "BOM Master has deleted successfully.", true);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "BOM Master has not deleted.", false);
                return new OkObjectResult(jwt);
            }
        }

        [HttpGet("GetBomMasterById")]
        public async Task<IActionResult> GetBomMasterById(int? pendingbomId)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            var datajson = await service.GetBomMasterById(user.employeeId, pendingbomId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }
        [HttpGet("GetApprovedBomMasterById")]
        public async Task<IActionResult> GetApprovedBomMasterById(int? bomId)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            var datajson = await service.GetApprovedBomMasterById(user.employeeId, bomId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }
        [HttpGet("GetPendingBomMasterById")]
        public async Task<IActionResult> GetPendingBomMasterById(int? bomId)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            var datajson = await service.GetPendingBomMasterById(user.employeeId, bomId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpGet("GetMaxBomMasterNumber")]
        public async Task<IActionResult> GetMaxBomMasterNumber(DateTime bomDate)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            var datajson = await service.GetMaxBomMasterNumber(bomDate);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpGet("GetBomProductWiseSpecification")]
        public async Task<IActionResult> GetBomProductWiseSpecification(int productId)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            var datajson = await service.GetBomProductWiseSpecification(productId, user.employeeId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpGet("GetProductWiseSpecificationWsieBOM")]
        public async Task<IActionResult> GetProductWiseSpecificationWsieBOM(int productId)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            var datajson = await service.GetProductWiseSpecificationWsieBOM(productId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }
        [HttpGet("GetBOMForListFromBOM")]
        public async Task<IActionResult> GetBOMForListFromBOM(int? planId,string materialType)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            var datajson = await service.GetBOMForListFromBOM(planId, materialType);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }
        [HttpGet("GetRevisionNoFromBOM")]
        public async Task<IActionResult> GetRevisionNoFromBOM(int? productWiseSpecificationId, string materialsType)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            var datajson = await service.GetRevisionNoFromBOM(productWiseSpecificationId, materialsType);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }
        [HttpGet("GetBomMasterIsApproveOrNot")]
        public async Task<IActionResult> GetBomMasterIsApproveOrNot(int? pendingbomId, string materialsType)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            var datajson = await service.GetBomMasterIsApproveOrNot(pendingbomId,materialsType);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }
        [HttpGet("GetBomMasterIsExistOrNot")]
        public async Task<IActionResult> GetBomMasterIsExistOrNot(int? bomProductWiseSpecificationId, string materialsType)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            var datajson = await service.GetBomMasterIsExistOrNot(bomProductWiseSpecificationId, materialsType);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpGet("GetLastGroupNameForBom")]
        public async Task<IActionResult> GetLastGroupNameForBom(int? productWiseSpecificationId)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            var datajson = await service.GetLastGroupNameForBom(productWiseSpecificationId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }
        #endregion

        #region BOM Details

        [HttpGet("GetBomDetailsByMasterId")]
        public async Task<IActionResult> GetBomDetailsByMasterId(int? pendingbomId)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            var datajson = await service.GetBomDetailsByMasterId(pendingbomId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpPost("DeleteBomDetailsById")]
        public async Task<IActionResult> DeleteBomDetailsById([FromBody] int pendingbomDetailsId)
        {

            if (Authentication().Result == false) return new OkObjectResult(jwts);

            if (pendingbomDetailsId <= 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "BOM Details has not deleted.", false);
                return new OkObjectResult(jwt);
            }
            bool result = await service.DeleteBomDetailsById(user.employeeId, pendingbomDetailsId);

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

        [HttpGet("GetAllbomForList")]
        public async Task<IActionResult> GetAllbomForList(int? bomForId)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            var datajson = await service.GetAllbomForList(bomForId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }
        #endregion

        #region BOM Report

        [HttpGet("GetBomReportDataById")]
        public async Task<IActionResult> GetBomReportDataById(int? pendingbomId)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            var datajson = await service.GetBomReportDataById(pendingbomId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        #endregion

        #region BOM Approval Edit

        [HttpGet("GetApprovedBomReportDataById")]
        public async Task<IActionResult> GetApprovedBomReportDataById(int? bomId)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            var datajson = await service.GetApprovedBomReportDataById(bomId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }
        [HttpGet("GetBomMasterByIdForApprovedBom")]
        public async Task<IActionResult> GetBomMasterByIdForApprovedBom(int? bomId)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            var datajson = await service.GetBomMasterByIdForApprovedBom((int)user.employeeId, bomId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpGet("GetBomDetailsByMasterIdForApprovedBom")]
        public async Task<IActionResult> GetBomDetailsByMasterIdForApprovedBom(int? bomId)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            var datajson = await service.GetBomDetailsByMasterIdForApprovedBom((int)user.employeeId, bomId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }
        [HttpPost("DeleteBomDetailsByIdForApprovedBom")]
        public async Task<IActionResult> DeleteBomDetailsByIdForApprovedBom([FromBody] int bomDetailsId)
        {

            if (Authentication().Result == false) return new OkObjectResult(jwts);

            if (bomDetailsId <= 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "BOM Details has not deleted.", false);
                return new OkObjectResult(jwt);
            }
            bool result = await service.DeleteBomDetailsByIdForApprovedBom(user.employeeId, bomDetailsId);

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
        [HttpPost("SaveBomMasterFromApproval")]
        public async Task<IActionResult> SaveBomMasterFromApproval([FromBody] BomMasterViewModelForApproval model)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            if (model == null)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "BOM Master has not created.", false);
                return new OkObjectResult(jwt);
            }

            int bomId = await service.SaveBomMasterFromApproval(user.employeeId, model);

            if (bomId == 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "BOM Master has not created.", false);
                return new OkObjectResult(jwt);
            }


            int result = await service.SaveBomDetailsFromApproval(user.employeeId, model.pendinglstDetailsViewModel, bomId);

            if (result != 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "BOM Details has created successfully.", true);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "BOM Details has not created.", false);
                return new OkObjectResult(jwt);
            }
        }
        #endregion

        [HttpGet("GetAllActiveInActiveBomListJson")]
        public async Task<IActionResult> GetAllActiveInActiveBomListJson(int? productWiseSpecificationId)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            var datajson = await service.GetAllActiveInActiveBomListJson((int)user.employeeId, productWiseSpecificationId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpPost("SaveActiveInActiveBom")]
        public async Task<IActionResult> SaveActiveInActiveBom([FromBody] BomActiveInActiveViewModel model)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            if (model == null)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "BOM Master has not created.", false);
                return new OkObjectResult(jwt);
            }

            int result = await service.SaveActiveInActiveBom((int)user.employeeId, model.lstMasterViewModel);

            if (result != 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "BOM Details has created successfully.", true);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "BOM Details has not created.", false);
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