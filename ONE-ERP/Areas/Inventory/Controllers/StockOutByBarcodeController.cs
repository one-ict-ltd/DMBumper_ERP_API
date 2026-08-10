using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ONEERP.Areas.Inventory.Models;
using ONEERP.ERPService.AuthService.Interfaces;
using ONEERP.ERPServices.Inventory.Interfaces;
using ONEERP.Helpers;

namespace ONEERP.Areas.Inventory.Controllers
{
    [Route("api/[controller]")]
    public class StockOutByBarcodeController : Controller
    {
        private IUserInfoes userInfoes;
        private readonly IStockOutByBarcodeService service;
        public StockOutByBarcodeController(IUserInfoes _userInfoes, IStockOutByBarcodeService _service)
        {
            this.userInfoes = _userInfoes;
            this.service = _service;
        }

        #region Stock Out By Barcode

        [HttpPost("SaveStockOutByBarcode")]
        public async Task<IActionResult> SaveStockOutByBarcode([FromBody] StockOutByBarcode models)
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

            if (models.lstStockOutByBarcodeDetails.Count() == 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Stock Out by Barcode details is empty! Please at least one item !!!", false);
                return new OkObjectResult(jwt);
            }

            int result = 0;
            int stockMasterId = await service.SaveStockOutByBarcode(user.employeeId.ToString(), models);
            if (stockMasterId == 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Stock Out by Barcode has not created.", false);
                return new OkObjectResult(jwt);
            }

            result = await service.SaveStockOutByBarcodeDetails(user.employeeId.ToString(), models.lstStockOutByBarcodeDetails, stockMasterId);
            if (result != 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Stock Out by Barcode Details has created successfully.", true);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Stock Out by Barcode Details has not created.", false);
                return new OkObjectResult(jwt);
            }
        }

        [HttpGet("GetStockOutByBarcodeByMasterId")]
        public async Task<IActionResult> GetStockOutByBarcodeByMasterId(int? stockMasterId)
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

            var datajson = await service.GetStockOutByBarcodeByMasterId(stockMasterId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }
        
        [HttpGet("GetStockOutByBarcodeDetailsByMasterId")]
        public async Task<IActionResult> GetStockOutByBarcodeDetailsByMasterId(int? stockMasterId)
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

            var datajson = await service.GetStockOutByBarcodeDetailsByMasterId(stockMasterId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpPost("DeleteStockOutByBarcodeByMasterId")]
        public async Task<IActionResult> DeleteStockOutByBarcodeByMasterId([FromBody] int stockMasterId)
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

            if (stockMasterId <= 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Stock Out by Barcode has not deleted.", false);
                return new OkObjectResult(jwt);
            }
            bool result = await service.DeleteStockOutByBarcodeByMasterId(user.employeeId.ToString(), stockMasterId);

            if (result == true)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Stock Out by Barcode has deleted successfully.", true);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Stock Out by Barcode has not deleted.", false);
                return new OkObjectResult(jwt);
            }
        }

        [HttpGet("GetBarcodeDetails")]
        public async Task<IActionResult> GetBarcodeDetails(string barcodeNo)
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

            var datajson = await service.GetBarcodeDetails(barcodeNo);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpGet("GetMaxStockOutNo")]
        public async Task<IActionResult> GetMaxStockOutNo(DateTime date)
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

            var datajson = await service.GetMaxStockOutNo(date);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }
        #endregion

        #region Reports
        //[HttpGet("GetStockInWithBarcodeReportData")]
        //public async Task<IActionResult> GetStockInWithBarcodeReportData(DateTime? fromDate, DateTime? toDate, int? barcodeId, int? fromStoreId)
        //{
        //    #region common

        //    var uid = Request.Headers["auth_token"];
        //    if (uid.Count() == 0)
        //    {
        //        bool status = false;
        //        string actionresult = "Invalid Token.";
        //        var jwts = await Tokens.changePasswordJwt(status, actionresult, new JsonSerializerSettings { Formatting = Formatting.Indented });
        //        return new OkObjectResult(jwts);
        //    }

        //    var stream = uid;
        //    var handler = new JwtSecurityTokenHandler();
        //    var tokenS = handler.ReadToken(stream) as JwtSecurityToken;
        //    var jti = tokenS.Claims.First(claim => claim.Type == "Id").Value;
        //    var user = await userInfoes.GetUserBasicInfoesbyId(jti);

        //    if (user.token != uid && user != null)
        //    {
        //        bool status = false;
        //        string actionresult = "Invalid Token.";
        //        var jwts = await Tokens.changePasswordJwt(status, actionresult, new JsonSerializerSettings { Formatting = Formatting.Indented });
        //        return new OkObjectResult(jwts);
        //    }


        //    #endregion

        //    var datajson = await service.GetStockInWithBarcodeReportData(fromDate, toDate, barcodeId, fromStoreId);
        //    var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
        //    return new OkObjectResult(jwt);
        //}
        #endregion
    }
}