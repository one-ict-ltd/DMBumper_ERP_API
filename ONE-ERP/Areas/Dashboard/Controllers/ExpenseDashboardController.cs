using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ONEERP.Data.Entity;
using ONEERP.ERPService.AuthService.Interfaces;
using ONEERP.ERPServices.AuthService.Interfaces;
using ONEERP.ERPServices.ExpenseDashboard.Interfaces;
using ONEERP.Helpers;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.Dashboard.Controllers
{
    [Route("api/[controller]")]
    public class ExpenseDashboardController : Controller
    {
        #region Fields

        private readonly IUserInfoes _userInfoes;
        private readonly TokenAuthenticator _authenticator;
        private readonly IJwtFactoryService _jwtFactoryService;
        private readonly IExpenseDashboardService _expenseDashboard;

        #endregion

        #region Ctor

        public ExpenseDashboardController
            (
            IUserInfoes userInfoes,
            IExpenseDashboardService expenseDashboard,
            IJwtFactoryService jwtFactoryService
            )
        {
            _userInfoes = userInfoes;
            _authenticator = new TokenAuthenticator(userInfoes);
            _jwtFactoryService = jwtFactoryService;
            _expenseDashboard = expenseDashboard;
        }

        #endregion

        #region Methods

        [ResponseCache(Duration =300)]
        [HttpGet("GetLocationWiseExpense")]
        public async Task<IActionResult> GetLocationWiseExpense(string locationType,string zoneCodes, string regionCodes, string areaCodes, string territoryCodes, DateTime? fromDate, DateTime? toDate, bool isDetails)
        {

            #region common

            var (user, message, isAuthenticUser) = await _jwtFactoryService.AuthenticateRequest(Request.Headers["auth_token"]);
            if (!isAuthenticUser)
            {
                var jwts = await Tokens.changePasswordJwt(isAuthenticUser, message, new JsonSerializerSettings { Formatting = Formatting.Indented });
                return new OkObjectResult(jwts);
            }

            #endregion

            var datajson = await _expenseDashboard.GetLocationWiseExpense(user.employeeId, locationType, zoneCodes, regionCodes, areaCodes, territoryCodes, fromDate, toDate, isDetails);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [ResponseCache(Duration = 300)]
        [HttpGet("GetNationalExpeseSumamry")]
        public async Task<IActionResult> GetNationalExpeseSumamry(string locationType,string zoneCodes, string regionCodes, string areaCodes, string territoryCodes, DateTime? fromDate, DateTime? toDate)
        {

            #region common

            var (user, message, isAuthenticUser) = await _jwtFactoryService.AuthenticateRequest(Request.Headers["auth_token"]);
            if (!isAuthenticUser)
            {
                var jwts = await Tokens.changePasswordJwt(isAuthenticUser, message, new JsonSerializerSettings { Formatting = Formatting.Indented });
                return new OkObjectResult(jwts);
            }

            #endregion

            var datajson = await _expenseDashboard.GetNationalExpeseSumamry(user.employeeId, locationType ,zoneCodes, regionCodes, areaCodes, territoryCodes ,fromDate, toDate);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        //Cost head
        [ResponseCache(Duration = 300)]
        [HttpGet("GetNationalCostHeadWiseExpense")]
        public async Task<IActionResult> GetNationalCostHeadWiseExpense(int ? expenseYear)
        {

            #region common

            var (user, message, isAuthenticUser) = await _jwtFactoryService.AuthenticateRequest(Request.Headers["auth_token"]);
            if (!isAuthenticUser)
            {
                var jwts = await Tokens.changePasswordJwt(isAuthenticUser, message, new JsonSerializerSettings { Formatting = Formatting.Indented });
                return new OkObjectResult(jwts);
            }

            #endregion

            var datajson = await _expenseDashboard.GetNationalCostHeadWiseExpense(user.employeeId, expenseYear, false);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [ResponseCache(Duration = 300)]
        [HttpGet("getDepotWiseExpense")]
        public async Task<IActionResult> GetDepotWiseExpense(int? expenseYear, bool isDetails=false)
        {

            #region common

            var (user, message, isAuthenticUser) = await _jwtFactoryService.AuthenticateRequest(Request.Headers["auth_token"]);
            if (!isAuthenticUser)
            {
                var jwts = await Tokens.changePasswordJwt(isAuthenticUser, message, new JsonSerializerSettings { Formatting = Formatting.Indented });
                return new OkObjectResult(jwts);
            }

            #endregion

            var datajson = await _expenseDashboard.GetDepotWiseExpense(user.employeeId, expenseYear, isDetails);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [ResponseCache(Duration = 300)]
        [HttpGet("getExpenseCategoryWiseOverview")]
        public async Task<IActionResult> ExpenseCategoryWiseOverview(string locationType, string zoneCodes, string regionCodes, string areaCodes, string territoryCodes, DateTime? fromDate, DateTime? toDate, bool isDetails)
        {

            #region common

            var (user, message, isAuthenticUser) = await _jwtFactoryService.AuthenticateRequest(Request.Headers["auth_token"]);
            if (!isAuthenticUser)
            {
                var jwts = await Tokens.changePasswordJwt(isAuthenticUser, message, new JsonSerializerSettings { Formatting = Formatting.Indented });
                return new OkObjectResult(jwts);
            }

            #endregion

            var datajson = await _expenseDashboard.ExpenseCategoryWiseOverview(user.employeeId,locationType, zoneCodes, regionCodes, areaCodes, territoryCodes, fromDate, toDate, isDetails);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [ResponseCache(Duration =300)]
        [HttpGet("GetNationalExpenseComparisonByYears")]
        public async Task<IActionResult> GetNationalExpenseComparisonByYears(int? expenseYearOne, int? expenseYearTwo)
        {

            #region common

            var (user, message, isAuthenticUser) = await _jwtFactoryService.AuthenticateRequest(Request.Headers["auth_token"]);
            if (!isAuthenticUser)
            {
                var jwts = await Tokens.changePasswordJwt(isAuthenticUser, message, new JsonSerializerSettings { Formatting = Formatting.Indented });
                return new OkObjectResult(jwts);
            }

            #endregion

            var datajson = await _expenseDashboard.GetNationalExpenseComparisonByYears(user.employeeId, expenseYearOne, expenseYearTwo);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }


        #endregion
    }
}
