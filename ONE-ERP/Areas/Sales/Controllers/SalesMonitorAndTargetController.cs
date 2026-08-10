using System;
using System.Collections;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ONEERP.Areas.Sales.Models;
using ONEERP.Data.Entity;
using ONEERP.ERPService.AuthService.Interfaces;
using ONEERP.ERPServices.Sales.Interfaces;
using ONEERP.Helpers;

namespace ONEERP.Areas.Sales.Controllers
{
    [Route("api/[controller]")]
    public class SalesMonitorAndTargetController : Controller
    {
        object jwts;
        ApplicationUser user;
        private IUserInfoes userInfoes;
        private readonly ISalesMonitorAndTargetService service;
        public SalesMonitorAndTargetController(IUserInfoes _userInfoes, ISalesMonitorAndTargetService _service)
        {
            this.userInfoes = _userInfoes;
            this.service = _service;
        }

        #region Sales Monitor And Target

        [HttpPost("SaveProductMonitor")]
        public async Task<IActionResult> SaveProductMonitor([FromBody] SalProductMonitorViewModel model)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            if (model == null)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Product Monitor not created.", false);
                return new OkObjectResult(jwt);
            }

            int result = 0;
            result = await service.SaveProductMonitor(user.employeeId, model);

            if (result == 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Product Monitor not created.", false);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Product Monitor created.", true);
                return new OkObjectResult(jwt);
            }
        }

        [HttpPost("DeleteProductMonitor")]
        public async Task<IActionResult> DeleteProductMonitor([FromBody] int monitorId)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            if (monitorId <= 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Product Monitor has not deleted.", false);
                return new OkObjectResult(jwt);
            }
            bool result = await service.DeleteProductMonitor(user.employeeId, monitorId);

            if (result == true)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Product Monitor has deleted successfully.", true);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Product Monitor has not deleted.", false);
                return new OkObjectResult(jwt);
            }
        }

        [HttpPost("DeleteWeeklyTargetPercentage")]
        public async Task<IActionResult> DeleteWeeklyTargetPercentage([FromBody] int weeklyTargetId)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            if (weeklyTargetId <= 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Weekly Target Percentage has not deleted.", false);
                return new OkObjectResult(jwt);
            }
            bool result = await service.DeleteWeeklyTargetPercentage(user.employeeId, weeklyTargetId);

            if (result == true)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Weekly Target Percentage has deleted successfully.", true);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Weekly Target Percentage has not deleted.", false);
                return new OkObjectResult(jwt);
            }
        }

        [HttpGet("GetProductMonitor")]
        public async Task<IActionResult> GetProductMonitor(int? monitorId, DateTime? fromDate, DateTime? toDate, string territoryCode)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            var datajson = await service.GetProductMonitor(user.employeeId, monitorId, fromDate, toDate, territoryCode);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpGet("GetWeeklyProductMonitorReport")]
        public async Task<IActionResult> GetWeeklyProductMonitorReport(DateTime? fDate, DateTime? tDate, string zoneCode, string regionCode, string areaCode, string depotCode, string territoryCode, string empCode)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            var datajson = await service.GetWeeklyProductMonitorReport(user.employeeId, fDate, tDate, zoneCode, regionCode, areaCode, depotCode, territoryCode, empCode);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpGet("GetWeeklyProductTarget")]
        public async Task<IActionResult> GetWeeklyProductTarget(DateTime? fDate, DateTime? tDate,int? weeklyTargetId)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            var datajson = await service.GetWeeklyProductTarget(user.employeeId, fDate, tDate, weeklyTargetId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }
        [HttpGet("GetWeeklyTargetPercentageById")]
        public async Task<IActionResult> GetWeeklyTargetPercentageById(int? weeklyTargetId)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            var datajson = await service.GetWeeklyTargetPercentageById(user.employeeId, weeklyTargetId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpPost("SaveWeeklyTargetPercentage")]
        public async Task<IActionResult> SaveWeeklyTargetPercentage([FromBody] SalWeeklyTargetPercentage model)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            if (model == null)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Weekly Target Percentage not created.", false);
                return new OkObjectResult(jwt);
            }

            int result = 0;
            result = await service.SaveWeeklyTargetPercentage(user.employeeId, model);

            if (result == 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Weekly Target Percentage not created.", false);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Weekly Target Percentage created.", true);
                return new OkObjectResult(jwt);
            }
        }

        #endregion
        [HttpPost("SaveMIOSalesForecast")]
        public async Task<IActionResult> SaveMIOSalesForecast([FromBody] MIODailySalesForecastViewModel model)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            if (model == null)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Sales Forecast not created.", false);
                return new OkObjectResult(jwt);
            }

            int result = 0;
            result = await service.SaveMIOSalesForecast(user.employeeId.ToString(), model);

            if (result == 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Sales Forecast not created.", false);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Sales Forecast created successfully.", true);
                return new OkObjectResult(jwt);
            }
        }

        [HttpPost("saveExecutiveWiseProduct")]
        public async Task<IActionResult> SaveExecutiveWiseProduct([FromBody] List<SalExecutiveWiseProductViewModel> model)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);
            if (model == null)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Executive Wise Product not created.", false);
                return new OkObjectResult(jwt);
            }
            int result = 0;
            result = await service.SaveExecutiveWiseProduct(user.employeeId, model);
            if (result == 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Executive Wise Product not created.", false);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Executive Wise Product created.", true);
                return new OkObjectResult(jwt);
            }
        }

        [HttpGet("GetExecutiveWiseProduct")]
        public async Task<IActionResult> GetExecutiveWiseProduct(int? executiveWiseProductId)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            var datajson = await service.GetExecutiveWiseProduct(executiveWiseProductId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        
        [HttpGet("DeleteExecutiveWiseProduct")]
        public async Task<IActionResult> DeleteExecutiveWiseProduct(int executiveWiseProductId)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            bool result = await service.DeleteExecutiveWiseProduct(user.employeeId, executiveWiseProductId);

            if (result == true)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Executive Wise Product has deleted successfully.", true);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Executive Wise Product has not deleted.", false);
                return new OkObjectResult(jwt);
            }
        }

        
        [HttpGet("GetProductWiseGrossReturn")]
        public async Task<IActionResult> GetProductWiseGrossReturn(string depotCode, DateTime? fDate, DateTime? tDate)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            var datajson = await service.GetProductWiseGrossReturn(user.employeeId, depotCode, fDate, tDate);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        async Task<bool> Authentication()
        {
            #region common
            var uid = Request.Headers["auth_token"];
            if (!uid.Any())
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

            if (user == null || user.token != uid)
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