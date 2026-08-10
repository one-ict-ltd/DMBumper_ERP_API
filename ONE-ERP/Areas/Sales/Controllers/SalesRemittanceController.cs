using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Newtonsoft.Json;
using ONEERP.Areas.Accounting.Models;
using ONEERP.Areas.Sales.Models;
using ONEERP.ERPService.AuthService.Interfaces;
using ONEERP.ERPServices.Sales.Interfaces;
using ONEERP.Helpers;

namespace ONEERP.Areas.Sales.Controllers
{
    [Route("api/[controller]")]
    public class SalesRemittanceController : Controller
    {
        private readonly IUserInfoes userInfoes;
        private readonly ISalesRemittanceService service;
        public SalesRemittanceController(IUserInfoes _userInfoes, ISalesRemittanceService _service)
        {
            this.userInfoes = _userInfoes;
            this.service = _service;
        }

        #region Sales Remittance

        [HttpPost("SaveRemittance")]
        public async Task<IActionResult> SaveRemittance([FromBody] SalesRemittanceMasterViewModel model)
        {
            #region common

            var uid = Request.Headers["auth_token"];
            if (!uid.Any())
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

            if (user == null || user.token != uid)
            {
                bool status = false;
                string actionresult = "Invalid Token.";
                var jwts = await Tokens.changePasswordJwt(status, actionresult, new JsonSerializerSettings { Formatting = Formatting.Indented });
                return new OkObjectResult(jwts);
            }


            #endregion

            ICollection<SalRemittanceViewModel> remittances = await service.CheckRemittanceTransactionNumber(model);
            if (remittances != null && remittances.Count > 0)
            {
                StringBuilder message = new StringBuilder();
                foreach (var item in remittances)
                {
                    message.AppendLine($"Transaction number: {item.oplTranNo}, Remittance No: {item.remittanceNo} has already been posted on: {item.remittanceDate?.ToString("dd-MMM-yyyy")}");
                }

                //var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, message.ToString(), false);
                //return new OkObjectResult(jwt);
          

                if (!string.IsNullOrWhiteSpace(message.ToString()))
                {
                    var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, message.ToString(), false);
                    return new OkObjectResult(jwt);
                }
            }



            int result = 0;

            int remittanceMasterId = await service.SaveSalesRemittance(user.employeeId.ToString(), model);

            if (remittanceMasterId == 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Remittance has not created.", false);
                return new OkObjectResult(jwt);
            }

            if (model.salesRemittanceSlips.Any())
            {
                result = await service.SaveSalesRemittanceSlips(user.employeeId.ToString(), model.salesRemittanceSlips, remittanceMasterId);

            }
            else
            {
                var jwtsuccess = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Remittance has created successfully.", true, remittanceMasterId);
                return new OkObjectResult(jwtsuccess);
            }

            if (result != 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Remittance has created successfully.", true,remittanceMasterId);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Remittance has not created.", false);
                return new OkObjectResult(jwt);
            }
        }

        [HttpGet("GetRemittanceById")]
        public async Task<IActionResult> GetRemittanceById(int? remittanceId)
        {
            #region common

            var uid = Request.Headers["auth_token"];
            if (!uid.Any())
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

            if (user == null || user.token != uid)
            {
                bool status = false;
                string actionresult = "Invalid Token.";
                var jwts = await Tokens.changePasswordJwt(status, actionresult, new JsonSerializerSettings { Formatting = Formatting.Indented });
                return new OkObjectResult(jwts);
            }


            #endregion

            var datajson = await service.GetSalesRemittanceById(remittanceId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }


        [HttpGet("GetRemittanceList")]
        public async Task<IActionResult> GetRemittanceList(int? remittanceId, DateTime? fDate, DateTime? tDate)
        {
            #region common

            var uid = Request.Headers["auth_token"];
            if (!uid.Any())
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

            if (user == null || user.token != uid)
            {
                bool status = false;
                string actionresult = "Invalid Token.";
                var jwts = await Tokens.changePasswordJwt(status, actionresult, new JsonSerializerSettings { Formatting = Formatting.Indented });
                return new OkObjectResult(jwts);
            }


            #endregion

            var datajson = await service.GetSalesRemittanceList(remittanceId, user.employeeId, fDate, tDate);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }


        [HttpGet("GetRemittanceSummary")]
        public async Task<IActionResult> GetRemittanceSummary(string depotCode, DateTime? fromDate, DateTime? toDate,int? bankId)
        {
            #region common

            var uid = Request.Headers["auth_token"];
            if (!uid.Any())
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

            if (user == null || user.token != uid)
            {
                bool status = false;
                string actionresult = "Invalid Token.";
                var jwts = await Tokens.changePasswordJwt(status, actionresult, new JsonSerializerSettings { Formatting = Formatting.Indented });
                return new OkObjectResult(jwts);
            }


            #endregion

            var datajson = await service.GetSalesRemittanceSummary(depotCode, user.employeeId, fromDate, toDate, bankId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpGet("GetOplTranNoStatus")]
        public async Task<IActionResult> GetOplTranNoStatus(string oplTranNo, int? remittanceId)
        {
            #region common

            var uid = Request.Headers["auth_token"];
            if (!uid.Any())
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

            if (user == null || user.token != uid)
            {
                bool status = false;
                string actionresult = "Invalid Token.";
                var jwts = await Tokens.changePasswordJwt(status, actionresult, new JsonSerializerSettings { Formatting = Formatting.Indented });
                return new OkObjectResult(jwts);
            }


            #endregion

            var datajson = await service.GetOplTranNoStatus(oplTranNo, remittanceId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpGet("GetCashInHandByDepotCode")]
        public async Task<IActionResult> GetCashInHandByDepotCode(string depotCode, DateTime? queryDate)
        {
            #region common

            var uid = Request.Headers["auth_token"];
            if (!uid.Any())
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

            if (user == null || user.token != uid)
            {
                bool status = false;
                string actionresult = "Invalid Token.";
                var jwts = await Tokens.changePasswordJwt(status, actionresult, new JsonSerializerSettings { Formatting = Formatting.Indented });
                return new OkObjectResult(jwts);
            }


            #endregion

            var datajson = await service.GetCashinHandByDepotCode(user.employeeId,depotCode, queryDate);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }
        
        [HttpPost("DeleteRemittance")]
        public async Task<IActionResult> DeleteRemittance([FromQuery] int remittanceId)
        {
            #region common

            var uid = Request.Headers["auth_token"];
            if (!uid.Any())
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

            if (user == null || user.token != uid)
            {
                bool status = false;
                string actionresult = "Invalid Token.";
                var jwts = await Tokens.changePasswordJwt(status, actionresult, new JsonSerializerSettings { Formatting = Formatting.Indented });
                return new OkObjectResult(jwts);
            }


            #endregion

            var deletedId = await service.DeleteRemittance(user.employeeId.ToString(), remittanceId);

            if (deletedId > 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Remittance has deleted successfully.", true);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Remittance has not deleted.", false);
                return new OkObjectResult(jwt);
            }
        }


        [HttpGet("downloadRemittanceSlipsByRemslipId")]
        public async Task<IActionResult> downloadRemittanceSlipsByRemslipId(int remittanceSlipId)
        {

            var uid = Request.Headers["auth_token"];
            if (!uid.Any())
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

            if (user == null || user.token != uid)
            {
                bool status = false;
                string actionresult = "Invalid Token.";
                var jwts = await Tokens.changePasswordJwt(status, actionresult, new JsonSerializerSettings { Formatting = Formatting.Indented });
                return new OkObjectResult(jwts);
            }

            var datajson = await service.GetRemittanceSlipsJson(0, remittanceSlipId);
            var dataList = JsonConvert.DeserializeObject<List<SalesRemittanceSlipViewModel>>(datajson.data);
            var data = dataList.FirstOrDefault();
            if (data != null)
            {
                var filePath = data.resourceUrl ?? string.Empty;
                var bytes = await System.IO.File.ReadAllBytesAsync(filePath);
                var provider = new FileExtensionContentTypeProvider();
                if (!provider.TryGetContentType(filePath, out var contentType))
                {
                    contentType = "application/octet-stream";
                }
                var returnObj = new FileViewModel();
                returnObj.fileName = Path.GetFileName(filePath);
                returnObj.contentType = contentType;
                returnObj.fileString = Convert.ToBase64String(bytes);
                return new OkObjectResult(returnObj);
            }

            var jwtMessage = await Tokens.changePasswordJwt(false, "File Not Found.", new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwtMessage);
        }
        [HttpGet("GetDepotWiseCollections")]
        public async Task<IActionResult> GetDepotWiseCollections(string depotCode)
        {
            #region common

            var uid = Request.Headers["auth_token"];
            if (!uid.Any())
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

            if (user == null || user.token != uid)
            {
                bool status = false;
                string actionresult = "Invalid Token.";
                var jwts = await Tokens.changePasswordJwt(status, actionresult, new JsonSerializerSettings { Formatting = Formatting.Indented });
                return new OkObjectResult(jwts);
            }


            #endregion

            var datajson = await service.GetDepotWiseCollections(user.employeeId, depotCode);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpPost("UpdateHasRemittanceOfCollectionMaster")]
        public async Task<IActionResult> UpdateHasRemittanceOfCollectionMaster([FromBody] ICollection<HasRemittanceOfCollectionMasterUpdateViewModel> models)
        {
            #region common

            var uid = Request.Headers["auth_token"];
            if (!uid.Any())
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

            if (user == null || user.token != uid)
            {
                bool status = false;
                string actionresult = "Invalid Token.";
                var jwts = await Tokens.changePasswordJwt(status, actionresult, new JsonSerializerSettings { Formatting = Formatting.Indented });
                return new OkObjectResult(jwts);
            }


            #endregion


            
            int remittanceId = await service.UpdateHasRemittanceOfCollectionMaster(user.employeeId.ToString(), models);

            if (remittanceId == 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Remittance has not created.", false);
                return new OkObjectResult(jwt);
            }     
            else
            {
                var jwtsuccess = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Remittance has created successfully.", true);
                return new OkObjectResult(jwtsuccess);
            }

            //if (result != 0)
            //{
            //    var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Remittance has created successfully.", true);
            //    return new OkObjectResult(jwt);
            //}
            //else
            //{
            //    var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Remittance has not created.", false);
            //    return new OkObjectResult(jwt);
            //}
        }


        #endregion Sales Remittance


    }
}