using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ONEERP.Areas.FieldForceTracking.Models;
using ONEERP.Areas.Hrm.Models;
using ONEERP.ERPService.AuthService.Interfaces;
using ONEERP.ERPServices.FieldForceTracking.Interfaces;
using ONEERP.Helpers;
using ONEERP.Models.Dashboard;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Threading.Tasks;


namespace ONEERP.Areas.FieldForceTracking.Controllers
{
    [Route("api/[controller]")]
    public class ScheduleController : Controller
    {
        private IUserInfoes userInfoes;
        private readonly IChemistService _chemistService;
        private readonly IDoctorService _doctorService;
        private readonly IDoctorScheduleService _doctorScheduleService;
        private readonly IChemistScheduleService _chemistScheduleService;
        private readonly IEmployeeService employeeService;

        public ScheduleController(IEmployeeService employeeService, IDoctorService doctorService, IUserInfoes userInfoes, IChemistService chemistService, IDoctorScheduleService doctorScheduleService, IChemistScheduleService chemistScheduleService)
        {
            this.userInfoes = userInfoes;
            this._chemistService = chemistService;
            this._doctorScheduleService = doctorScheduleService;
            this._chemistScheduleService = chemistScheduleService;
            this._doctorService = doctorService;
            this.employeeService = employeeService;
        }

        #region Market

        [HttpPost("setPlanMarket")]
        public async Task<IActionResult> setPlanMarket([FromBody] MarketScheduleParamViewModel model)
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
            // var jsonToken = handler.ReadToken(stream);

            var tokenS = handler.ReadToken(stream) as JwtSecurityToken;
            var jti = tokenS.Claims.First(claim => claim.Type == "Id").Value;

            var user = await userInfoes.GetUserBasicInfoesbyId(jti);
            var employee = await employeeService.GetEmployeeLoadViewModels();
            employee = employee.Where(x => x.employeeNo == user.UserName).ToList();
            if (employee?.FirstOrDefault()?.Token != uid && employee.Count() != 0)
            {
                bool status = false;
                string actionresult = "Invalid Token.";
                var jwts = await Tokens.changePasswordJwt(status, actionresult, new JsonSerializerSettings { Formatting = Formatting.Indented });

                return new OkObjectResult(jwts);

            }
            if (model.RosterID == null || model.visitDate == null || model.RosterID == 0 || model.visitDate == "")
            {
                var jwt = await Tokens.setMarketplanFailJwt(new JsonSerializerSettings { Formatting = Formatting.Indented });

                return new OkObjectResult(jwt);
            }
            bool result = await _doctorScheduleService.setPlanMarket(jti, model.RosterID, model.MarketID, Convert.ToDateTime(model.visitDate), model.VisitTime, model.Opinion, model.ZoneCode, model.DepotCode, model.RegionCode, model.AreaCode, model.TerritoryCode, model.MioCode);
            //DateTime ValidTo = jsonToken.ValidTo;

            if (result == true)
            {
                var jwt = await Tokens.setMarketplanSuccessJwt(new JsonSerializerSettings { Formatting = Formatting.Indented });

                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setMarketplanFailJwt(new JsonSerializerSettings { Formatting = Formatting.Indented });

                return new OkObjectResult(jwt);
            }
            // int cnic = jsonData.enad_list[0].et_cnic;



        }

        [HttpPost("setDailyPlanDoc")]
        public async Task<IActionResult> setDailyPlanDoc([FromBody] DailyPlanDocViewModel model)
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
            // var jsonToken = handler.ReadToken(stream);

            var tokenS = handler.ReadToken(stream) as JwtSecurityToken;

            var jti = tokenS.Claims.First(claim => claim.Type == "Id").Value;
            var user = await userInfoes.GetUserBasicInfoesbyId(jti);
            var employee = await employeeService.GetEmployeeLoadViewModels();
            employee = employee.Where(x => x.employeeNo == user.UserName).ToList();
            if (employee?.FirstOrDefault()?.Token != uid && employee.Count() != 0)
            {
                bool status = false;
                string actionresult = "Invalid Token.";
                var jwts = await Tokens.changePasswordJwt(status, actionresult, new JsonSerializerSettings { Formatting = Formatting.Indented });

                return new OkObjectResult(jwts);

            }
            bool result = false;
            for (int i = 0; i < model.dailyPlanDocViewModelDetails.Count(); i++)
            {
                result = await _chemistScheduleService.setDailyPlanDoc(model.dailyPlanDocViewModelDetails[i].EmpCode, model.dailyPlanDocViewModelDetails[i].DoctorCode, model.dailyPlanDocViewModelDetails[i].day, model.dailyPlanDocViewModelDetails[i].StartTime, model.dailyPlanDocViewModelDetails[i].EndTime, model.dailyPlanDocViewModelDetails[i].Remarks);
            }
            //DateTime ValidTo = jsonToken.ValidTo;

            if (result == true)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Set Doc Plan Created successfully.", true);

                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Set Doc Plan has not Created successfully.", true);

                return new OkObjectResult(jwt);
            }
            // int cnic = jsonData.enad_list[0].et_cnic;

        }

        [HttpPost("setDailyPlanTerritory")]
        public async Task<IActionResult> setDailyPlanTerritory([FromBody] DailyPlanDocViewModel model)
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
            // var jsonToken = handler.ReadToken(stream);

            var tokenS = handler.ReadToken(stream) as JwtSecurityToken;

            var jti = tokenS.Claims.First(claim => claim.Type == "Id").Value;
            var user = await userInfoes.GetUserBasicInfoesbyId(jti);
            var employee = await employeeService.GetEmployeeLoadViewModels();
            employee = employee.Where(x => x.employeeNo == user.UserName).ToList();
            if (employee?.FirstOrDefault()?.Token != uid && employee.Count() != 0)
            {
                bool status = false;
                string actionresult = "Invalid Token.";
                var jwts = await Tokens.changePasswordJwt(status, actionresult, new JsonSerializerSettings { Formatting = Formatting.Indented });

                return new OkObjectResult(jwts);

            }
            bool result = false;
            for (int i = 0; i < model.dailyPlanDocViewModelDetails.Count(); i++)
            {
                result = await _chemistScheduleService.setDailyPlanTerritory(model.dailyPlanDocViewModelDetails[i].EmpCode, model.dailyPlanDocViewModelDetails[i].TerritoryCode, model.dailyPlanDocViewModelDetails[i].day, model.dailyPlanDocViewModelDetails[i].StartTime, model.dailyPlanDocViewModelDetails[i].EndTime, model.dailyPlanDocViewModelDetails[i].Remarks);
            }
            //DateTime ValidTo = jsonToken.ValidTo;

            if (result == true)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Set Territory Plan Created successfully.", true);

                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Set Territory Plan has not Created successfully.", true);

                return new OkObjectResult(jwt);
            }
            // int cnic = jsonData.enad_list[0].et_cnic;

        }

        [HttpPost("setDailyPlanChemist")]
        public async Task<IActionResult> setDailyPlanChemist([FromBody] DailyPlanDocViewModel model)
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
            // var jsonToken = handler.ReadToken(stream);

            var tokenS = handler.ReadToken(stream) as JwtSecurityToken;

            var jti = tokenS.Claims.First(claim => claim.Type == "Id").Value;
            var user = await userInfoes.GetUserBasicInfoesbyId(jti);
            var employee = await employeeService.GetEmployeeLoadViewModels();
            employee = employee.Where(x => x.employeeNo == user.UserName).ToList();
            if (employee?.FirstOrDefault()?.Token != uid && employee.Count() != 0)
            {
                bool status = false;
                string actionresult = "Invalid Token.";
                var jwts = await Tokens.changePasswordJwt(status, actionresult, new JsonSerializerSettings { Formatting = Formatting.Indented });

                return new OkObjectResult(jwts);

            }
            bool result = false;
            for (int i = 0; i < model.dailyPlanDocViewModelDetails.Count(); i++)
            {
                result = await _chemistScheduleService.setDailyPlanChemist(model.dailyPlanDocViewModelDetails[i].EmpCode, model.dailyPlanDocViewModelDetails[i].DoctorCode, model.dailyPlanDocViewModelDetails[i].day, model.dailyPlanDocViewModelDetails[i].StartTime, model.dailyPlanDocViewModelDetails[i].EndTime, model.dailyPlanDocViewModelDetails[i].Remarks);
            }
            //DateTime ValidTo = jsonToken.ValidTo;

            if (result == true)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Set chemist Plan Created successfully.", true);

                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Set chemist Plan has not Created successfully.", true);

                return new OkObjectResult(jwt);
            }
            // int cnic = jsonData.enad_list[0].et_cnic;

        }

        [HttpPost("updateDailyPlanDoc")]
        public async Task<IActionResult> updateDailyPlanDoc([FromBody] UpdateDailyPlanDocViewModelDetails model)
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
            // var jsonToken = handler.ReadToken(stream);

            var tokenS = handler.ReadToken(stream) as JwtSecurityToken;

            var jti = tokenS.Claims.First(claim => claim.Type == "Id").Value;
            var user = await userInfoes.GetUserBasicInfoesbyId(jti);
            var employee = await employeeService.GetEmployeeLoadViewModels();
            employee = employee.Where(x => x.employeeNo == user.UserName).ToList();
            if (employee?.FirstOrDefault()?.Token != uid && employee.Count() != 0)
            {
                bool status = false;
                string actionresult = "Invalid Token.";
                var jwts = await Tokens.changePasswordJwt(status, actionresult, new JsonSerializerSettings { Formatting = Formatting.Indented });

                return new OkObjectResult(jwts);

            }
            bool result = false;
            for (int i = 0; i < model.DailyPlanDocs.Count(); i++)
            {
                result = await _chemistScheduleService.updateDailyPlanDoc(model.DailyPlanDocs[i], model.status);
            }
            //DateTime ValidTo = jsonToken.ValidTo;

            if (result == true)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Set Doc Plan Updated successfully.", true);

                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Set Doc Plan has not Updated successfully.", true);

                return new OkObjectResult(jwt);
            }
            // int cnic = jsonData.enad_list[0].et_cnic;

        }

        [HttpPost("updateDailyPlanTerritoy")]
        public async Task<IActionResult> updateDailyPlanTerritoy([FromBody] UpdateDailyPlanDocViewModelDetails model)
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
            // var jsonToken = handler.ReadToken(stream);

            var tokenS = handler.ReadToken(stream) as JwtSecurityToken;

            var jti = tokenS.Claims.First(claim => claim.Type == "Id").Value;
            var user = await userInfoes.GetUserBasicInfoesbyId(jti);
            var employee = await employeeService.GetEmployeeLoadViewModels();
            employee = employee.Where(x => x.employeeNo == user.UserName).ToList();
            if (employee?.FirstOrDefault()?.Token != uid && employee.Count() != 0)
            {
                bool status = false;
                string actionresult = "Invalid Token.";
                var jwts = await Tokens.changePasswordJwt(status, actionresult, new JsonSerializerSettings { Formatting = Formatting.Indented });

                return new OkObjectResult(jwts);

            }
            bool result = false;
            for (int i = 0; i < model.DailyPlanDocs.Count(); i++)
            {
                result = await _chemistScheduleService.updateDailyPlanTerritory(model.DailyPlanDocs[i], model.status);
            }
            //DateTime ValidTo = jsonToken.ValidTo;

            if (result == true)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Set Territory Plan Updated successfully.", true);

                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Set Territory Plan has not Updated successfully.", true);

                return new OkObjectResult(jwt);
            }
            // int cnic = jsonData.enad_list[0].et_cnic;

        }

        [HttpPost("updateEmployeeMonthlyPromoItem")]
        public async Task<IActionResult> updateEmployeeMonthlyPromoItem([FromBody] UpdateEmployeeMonthlyPromoItem model)
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
            // var jsonToken = handler.ReadToken(stream);

            var tokenS = handler.ReadToken(stream) as JwtSecurityToken;

            var jti = tokenS.Claims.First(claim => claim.Type == "Id").Value;
            var user = await userInfoes.GetUserBasicInfoesbyId(jti);
            var employee = await employeeService.GetEmployeeLoadViewModels();
            employee = employee.Where(x => x.employeeNo == user.UserName).ToList();
            if (employee?.FirstOrDefault()?.Token != uid && employee.Count() != 0)
            {
                bool status = false;
                string actionresult = "Invalid Token.";
                var jwts = await Tokens.changePasswordJwt(status, actionresult, new JsonSerializerSettings { Formatting = Formatting.Indented });

                return new OkObjectResult(jwts);

            }
            bool result = false;
            for (int i = 0; i < model.employeeMonthlyPromoItems.Count(); i++)
            {
                result = await _chemistScheduleService.updateEmployeeMonthlyPromoItem(model.employeeMonthlyPromoItems[i].TerritoryWiseMonthlyPromoItemID, model.employeeMonthlyPromoItems[i].amount, model.employeeMonthlyPromoItems[i].monthno);
            }
            //DateTime ValidTo = jsonToken.ValidTo;

            if (result == true)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Monthly promo item receive Updated successfully.", true);

                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Monthly promo item receive has not Updated successfully.", true);

                return new OkObjectResult(jwt);
            }
            // int cnic = jsonData.enad_list[0].et_cnic;

        }

        [HttpPost("updateDailyPlanChemist")]
        public async Task<IActionResult> updateDailyPlanChemist([FromBody] UpdateDailyPlanDocViewModelDetails model)
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
            // var jsonToken = handler.ReadToken(stream);

            var tokenS = handler.ReadToken(stream) as JwtSecurityToken;

            var jti = tokenS.Claims.First(claim => claim.Type == "Id").Value;
            var user = await userInfoes.GetUserBasicInfoesbyId(jti);
            var employee = await employeeService.GetEmployeeLoadViewModels();
            employee = employee.Where(x => x.employeeNo == user.UserName).ToList();
            if (employee?.FirstOrDefault()?.Token != uid && employee.Count() != 0)
            {
                bool status = false;
                string actionresult = "Invalid Token.";
                var jwts = await Tokens.changePasswordJwt(status, actionresult, new JsonSerializerSettings { Formatting = Formatting.Indented });

                return new OkObjectResult(jwts);

            }
            bool result = false;
            for (int i = 0; i < model.DailyPlanDocs.Count(); i++)
            {
                result = await _chemistScheduleService.updateDailyPlanChemist(model.DailyPlanDocs[i], model.status);
            }
            //DateTime ValidTo = jsonToken.ValidTo;

            if (result == true)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Set Chemist Plan Updated successfully.", true);

                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Set Chemist Plan has not Updated successfully.", true);

                return new OkObjectResult(jwt);
            }
            // int cnic = jsonData.enad_list[0].et_cnic;

        }

        [HttpPost("updateDoctorUnderObservation")]
        public async Task<IActionResult> updateDoctorUnderObservation([FromBody] UpdateDailyPlanDocViewModelDetails model)
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
            // var jsonToken = handler.ReadToken(stream);

            var tokenS = handler.ReadToken(stream) as JwtSecurityToken;

            var jti = tokenS.Claims.First(claim => claim.Type == "Id").Value;
            var user = await userInfoes.GetUserBasicInfoesbyId(jti);
            var employee = await employeeService.GetEmployeeLoadViewModels();
            employee = employee.Where(x => x.employeeNo == user.UserName).ToList();
            if (employee?.FirstOrDefault()?.Token != uid && employee.Count() != 0)
            {
                bool status = false;
                string actionresult = "Invalid Token.";
                var jwts = await Tokens.changePasswordJwt(status, actionresult, new JsonSerializerSettings { Formatting = Formatting.Indented });

                return new OkObjectResult(jwts);

            }
            bool result = false;
            for (int i = 0; i < model.DailyPlanDocs.Count(); i++)
            {
                result = await _chemistScheduleService.updateDoctorUnderObservation(model.DailyPlanDocs[i], model.status);
            }
            //DateTime ValidTo = jsonToken.ValidTo;

            if (result == true)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Set Doctor Updated successfully.", true);

                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Set Doctor has not Updated successfully.", true);

                return new OkObjectResult(jwt);
            }
            // int cnic = jsonData.enad_list[0].et_cnic;

        }
        [HttpPost("updatePartyUnderObservation")]
        public async Task<IActionResult> updatePartyUnderObservation([FromBody] UpdatePartyObserbationViewModel model)
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
            // var jsonToken = handler.ReadToken(stream);

            var tokenS = handler.ReadToken(stream) as JwtSecurityToken;

            var jti = tokenS.Claims.First(claim => claim.Type == "Id").Value;
            var user = await userInfoes.GetUserBasicInfoesbyId(jti);
            var employee = await employeeService.GetEmployeeLoadViewModels();
            employee = employee.Where(x => x.employeeNo == user.UserName).ToList();
            if (employee?.FirstOrDefault()?.Token != uid && employee.Count() != 0)
            {
                bool status = false;
                string actionresult = "Invalid Token.";
                var jwts = await Tokens.changePasswordJwt(status, actionresult, new JsonSerializerSettings { Formatting = Formatting.Indented });

                return new OkObjectResult(jwts);

            }
            bool result = false;
            for (int i = 0; i < model.PartyList.Count(); i++)
            {
                result = await _chemistScheduleService.updatePartyUnderObservation(model.PartyList[i].PartyId, model.status, model.PartyList[i].creditLimit);
            }
            //DateTime ValidTo = jsonToken.ValidTo;

            if (result == true)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Set Chemist Updated successfully.", true);

                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Set Chemist has not Updated successfully.", true);

                return new OkObjectResult(jwt);
            }
            // int cnic = jsonData.enad_list[0].et_cnic;

        }

        [HttpPost("updateEmployeeTADA")]
        public async Task<IActionResult> updateEmployeeTADA([FromBody] UpdateEmployeeTADAViewModelDetails model)
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
            // var jsonToken = handler.ReadToken(stream);

            var tokenS = handler.ReadToken(stream) as JwtSecurityToken;

            var jti = tokenS.Claims.First(claim => claim.Type == "Id").Value;
            var user = await userInfoes.GetUserBasicInfoesbyId(jti);
            var employee = await employeeService.GetEmployeeLoadViewModels();
            employee = employee.Where(x => x.employeeNo == user.UserName).ToList();
            if (employee?.FirstOrDefault()?.Token != uid && employee.Count() != 0)
            {
                bool status = false;
                string actionresult = "Invalid Token.";
                var jwts = await Tokens.changePasswordJwt(status, actionresult, new JsonSerializerSettings { Formatting = Formatting.Indented });

                return new OkObjectResult(jwts);

            }
            bool result = false;
            for (int i = 0; i < model.DailyPlanDocs.Count(); i++)
            {
                result = await _chemistScheduleService.updateEmployeeTADA(model.DailyPlanDocs[i].EmployeeTADAId, model.DailyPlanDocs[i].status, model.DailyPlanDocs[i].amount, model.DailyPlanDocs[i].remarks);
            }
            //DateTime ValidTo = jsonToken.ValidTo;

            if (result == true)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Set Employee TADA Updated successfully.", true);

                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Set Employee TADA has not Updated successfully.", true);

                return new OkObjectResult(jwt);
            }
            // int cnic = jsonData.enad_list[0].et_cnic;

        }

        [HttpGet("setPlanUploadDoc")]
        public async Task<IActionResult> setPlanUploadDoc(string model)
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
            // var jsonToken = handler.ReadToken(stream);

            var tokenS = handler.ReadToken(stream) as JwtSecurityToken;
            var jti = tokenS.Claims.First(claim => claim.Type == "Id").Value;

            var user = await userInfoes.GetUserBasicInfoesbyId(jti);
            var employee = await employeeService.GetEmployeeLoadViewModels();
            employee = employee.Where(x => x.employeeNo == user.UserName).ToList();
            if (employee?.FirstOrDefault()?.Token != uid && employee.Count() != 0)
            {
                bool status = false;
                string actionresult = "Invalid Token.";
                var jwts = await Tokens.changePasswordJwt(status, actionresult, new JsonSerializerSettings { Formatting = Formatting.Indented });

                return new OkObjectResult(jwts);

            }
            //  var data=JsonConvert.DeserializeObject<List<ExeclField>>(model);

            dynamic data = JValue.Parse(model);
            //if (model.RosterID == null || model.visitDate == null || model.RosterID == 0 || model.visitDate == "")
            //{
            //    var jwt = await Tokens.setMarketplanFailJwt(new JsonSerializerSettings { Formatting = Formatting.Indented });

            //    return new OkObjectResult(jwt);
            //}
            //bool result = await _doctorScheduleService.setPlanMarket(jti, model.RosterID, model.MarketID, Convert.ToDateTime(model.visitDate), model.VisitTime, model.Opinion, model.ZoneCode, model.DepotCode, model.RegionCode, model.AreaCode, model.TerritoryCode, model.MioCode);
            //  DateTime ValidTo = jsonToken.ValidTo;

            List<string> lstEmpCode = new List<string>();
            int z = 0;
            foreach (dynamic d in data)
            {
                if (z > 0)
                {
                    string Code = string.Empty;
                    Code = d[0];
                    lstEmpCode.Add(Code);
                }
                z++;


            }
            for (int i = 0; i < lstEmpCode.Distinct().Count(); i++)
            {
                string Code = string.Empty;
                Code = lstEmpCode[i];

                await _chemistScheduleService.updateWeeklyPalnDoc(Code);

            }

            bool result = false;
            int j = 0;
            foreach (dynamic d in data)
            {

                //string name = d[0];
                //string value = d[1];
                if (j > 0)
                {
                    string EmpCode = d[0];
                    string Saturday = d[6];
                    string StartTimeSaturday = d[7];
                    string EndTimeSaturday = d[8];
                    string RemarksSaturday = d[9];
                    string Sunday = d[10];
                    string StartTimeSunDay = d[11];
                    string EndTimeSunday = d[12];
                    string RemarksSunday = d[13];
                    string Monday = d[14];
                    string StartTimeMonDay = d[15];
                    string EndTimeMonday = d[16];
                    string RemarksMonday = d[17];
                    string Tuesday = d[18];
                    string StartTimeTuesDay = d[19];
                    string EndTimeTuesday = d[20];
                    string RemarksTuesday = d[21];
                    string Wednesday = d[22];
                    string StartTimeWednesDay = d[23];
                    string EndTimeWednesday = d[24];
                    string RemarksWednesday = d[25];
                    string Thursday = d[26];
                    string StartTimeThursDay = d[27];
                    string EndTimeThursday = d[28];
                    string RemarksThursday = d[29];

                    string Friday = d[30];
                    string StartTimeFriDay = d[31];
                    string EndTimeFriday = d[32];
                    string RemarksFriday = d[33];
                    try
                    {
                        result = await _chemistScheduleService.setPlanDocExcel(EmpCode, Saturday, StartTimeSaturday, EndTimeSaturday, RemarksSaturday, Sunday, StartTimeSunDay, EndTimeSunday, RemarksSunday, Monday, StartTimeMonDay, EndTimeMonday, RemarksMonday, Tuesday, StartTimeTuesDay, EndTimeTuesday, RemarksTuesday, Wednesday, StartTimeWednesDay, EndTimeWednesday, RemarksWednesday, Thursday, StartTimeThursDay, EndTimeThursday, RemarksThursday, Friday, StartTimeFriDay, EndTimeFriday, RemarksFriday);
                    }
                    catch (Exception ex)
                    {


                    }

                }
                j++;
            }



            if (result == true)
            {
                var jwt = await Tokens.setMarketplanSuccessJwt(new JsonSerializerSettings { Formatting = Formatting.Indented });

                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setMarketplanFailJwt(new JsonSerializerSettings { Formatting = Formatting.Indented });

                return new OkObjectResult(jwt);
            }
            // int cnic = jsonData.enad_list[0].et_cnic;



        }
        [HttpGet("setPlanUpload")]
        public async Task<IActionResult> setPlanUpload(string model)
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
            // var jsonToken = handler.ReadToken(stream);

            var tokenS = handler.ReadToken(stream) as JwtSecurityToken;
            var jti = tokenS.Claims.First(claim => claim.Type == "Id").Value;

            var user = await userInfoes.GetUserBasicInfoesbyId(jti);
            var employee = await employeeService.GetEmployeeLoadViewModels();
            employee = employee.Where(x => x.employeeNo == user.UserName).ToList();
            if (employee?.FirstOrDefault()?.Token != uid && employee.Count() != 0)
            {
                bool status = false;
                string actionresult = "Invalid Token.";
                var jwts = await Tokens.changePasswordJwt(status, actionresult, new JsonSerializerSettings { Formatting = Formatting.Indented });

                return new OkObjectResult(jwts);

            }
            //  var data=JsonConvert.DeserializeObject<List<ExeclField>>(model);

            dynamic data = JValue.Parse(model);
            //if (model.RosterID == null || model.visitDate == null || model.RosterID == 0 || model.visitDate == "")
            //{
            //    var jwt = await Tokens.setMarketplanFailJwt(new JsonSerializerSettings { Formatting = Formatting.Indented });

            //    return new OkObjectResult(jwt);
            //}
            //bool result = await _doctorScheduleService.setPlanMarket(jti, model.RosterID, model.MarketID, Convert.ToDateTime(model.visitDate), model.VisitTime, model.Opinion, model.ZoneCode, model.DepotCode, model.RegionCode, model.AreaCode, model.TerritoryCode, model.MioCode);
            //  DateTime ValidTo = jsonToken.ValidTo;

            List<string> lstEmpCode = new List<string>();
            int z = 0;
            foreach (dynamic d in data)
            {
                if (z > 0)
                {
                    string Code = string.Empty;
                    Code = d[0];
                    lstEmpCode.Add(Code);
                }
                z++;


            }
            for (int i = 0; i < lstEmpCode.Distinct().Count(); i++)
            {
                string Code = string.Empty;
                Code = lstEmpCode[i];

                await _chemistScheduleService.updateWeeklyPaln(Code);

            }

            bool result = false;
            int j = 0;
            foreach (dynamic d in data)
            {

                //string name = d[0];
                //string value = d[1];
                if (j > 0)
                {
                    string EmpCode = d[0];
                    string Saturday = d[6];
                    string StartTimeSaturday = d[7];
                    string EndTimeSaturday = d[8];
                    string RemarksSaturday = d[9];
                    string Sunday = d[10];
                    string StartTimeSunDay = d[11];
                    string EndTimeSunday = d[12];
                    string RemarksSunday = d[13];
                    string Monday = d[14];
                    string StartTimeMonDay = d[15];
                    string EndTimeMonday = d[16];
                    string RemarksMonday = d[17];
                    string Tuesday = d[18];
                    string StartTimeTuesDay = d[19];
                    string EndTimeTuesday = d[20];
                    string RemarksTuesday = d[21];
                    string Wednesday = d[22];
                    string StartTimeWednesDay = d[23];
                    string EndTimeWednesday = d[24];
                    string RemarksWednesday = d[25];
                    string Thursday = d[26];
                    string StartTimeThursDay = d[27];
                    string EndTimeThursday = d[28];
                    string RemarksThursday = d[29];

                    string Friday = d[30];
                    string StartTimeFriDay = d[31];
                    string EndTimeFriday = d[32];
                    string RemarksFriday = d[33];
                    result = await _chemistScheduleService.setPlanExcel(EmpCode, Saturday, StartTimeSaturday, EndTimeSaturday, RemarksSaturday, Sunday, StartTimeSunDay, EndTimeSunday, RemarksSunday, Monday, StartTimeMonDay, EndTimeMonday, RemarksMonday, Tuesday, StartTimeTuesDay, EndTimeTuesday, RemarksTuesday, Wednesday, StartTimeWednesDay, EndTimeWednesday, RemarksWednesday, Thursday, StartTimeThursDay, EndTimeThursday, RemarksThursday, Friday, StartTimeFriDay, EndTimeFriday, RemarksFriday);
                }
                j++;
            }



            if (result == true)
            {
                var jwt = await Tokens.setMarketplanSuccessJwt(new JsonSerializerSettings { Formatting = Formatting.Indented });

                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setMarketplanFailJwt(new JsonSerializerSettings { Formatting = Formatting.Indented });

                return new OkObjectResult(jwt);
            }
            // int cnic = jsonData.enad_list[0].et_cnic;



        }
        public class ExeclField
        {
            public string col0 { get; set; }
            public string col1 { get; set; }
            public string col2 { get; set; }
            public string col3 { get; set; }
            public string col4 { get; set; }
            public string col5 { get; set; }
            public string col6 { get; set; }
            public string col7 { get; set; }
            public string col8 { get; set; }
            public string col9 { get; set; }
            public string col10 { get; set; }
            public string col11 { get; set; }
            public string col12 { get; set; }
            public string col13 { get; set; }
            public string col14 { get; set; }
            public string col15 { get; set; }
            public string col16 { get; set; }
            public string col17 { get; set; }
            public string col18 { get; set; }
            public string col19 { get; set; }
            public string col20 { get; set; }
            public string col21 { get; set; }
            public string col22 { get; set; }
            public string col23 { get; set; }
            public string col24 { get; set; }
            public string col25 { get; set; }
            public string col26 { get; set; }
            public string col27 { get; set; }
            public string col28 { get; set; }
            public string col29 { get; set; }
            public string col30 { get; set; }
            public string col31 { get; set; }
            public string col32 { get; set; }
            public string col33 { get; set; }
        }
        [HttpPost("setMarket")]
        public async Task<IActionResult> setMarket([FromBody] MarketSetParamViewModel model)
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
            // var jsonToken = handler.ReadToken(stream);

            var tokenS = handler.ReadToken(stream) as JwtSecurityToken;

            var jti = tokenS.Claims.First(claim => claim.Type == "Id").Value;
            var user = await userInfoes.GetUserBasicInfoesbyId(jti);
            var employee = await employeeService.GetEmployeeLoadViewModels();
            employee = employee.Where(x => x.employeeNo == user.UserName).ToList();
            if (employee?.FirstOrDefault()?.Token != uid && employee.Count() != 0)
            {
                bool status = false;
                string actionresult = "Invalid Token.";
                var jwts = await Tokens.changePasswordJwt(status, actionresult, new JsonSerializerSettings { Formatting = Formatting.Indented });

                return new OkObjectResult(jwts);

            }
            bool result = await _doctorService.setMarketAPI(jti, Convert.ToInt32(model.MarketId), model.Name, model.Address, model.Latitude, model.Longitude);
            //DateTime ValidTo = jsonToken.ValidTo;

            if (result == true)
            {
                var jwt = await Tokens.setMarketSuccessJwt(new JsonSerializerSettings { Formatting = Formatting.Indented });

                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setMarketFailJwt(new JsonSerializerSettings { Formatting = Formatting.Indented });

                return new OkObjectResult(jwt);
            }
            // int cnic = jsonData.enad_list[0].et_cnic;



        }


        #endregion

        #region CheckInOut/Location

        [HttpPost("setLocationData")]
        public async Task<IActionResult> setLocationData([FromBody] List<LocationDataViewModel> model)
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
            var employee = await employeeService.GetEmployeeLoadViewModels();
            employee = employee.Where(x => x.employeeNo == user.UserName).ToList();
            if (employee?.FirstOrDefault()?.Token != uid && employee.Count() != 0)
            {
                bool status = false;
                string actionresult = "Invalid Token.";
                var jwts = await Tokens.changePasswordJwt(status, actionresult, new JsonSerializerSettings { Formatting = Formatting.Indented });
                return new OkObjectResult(jwts);
            }
            bool result = false;
            if (model != null)
            {
                foreach (var data in model)
                {
                    result = await _doctorScheduleService.setCurrentLocation(jti, data.latitude, data.longitude, data.address, data.visitDateTime);
                }
            }

            if (result == true)
            {
                var jwt = await Tokens.setLocationSuccessJwt(new JsonSerializerSettings { Formatting = Formatting.Indented });
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setLocationfailJwt(new JsonSerializerSettings { Formatting = Formatting.Indented });
                return new OkObjectResult(jwt);
            }
        }

        [HttpPost("setCheckInOut")]
        public async Task<IActionResult> setCheckInOut([FromBody] CheckInOutParamViewModel model)
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
            // var jsonToken = handler.ReadToken(stream);

            var tokenS = handler.ReadToken(stream) as JwtSecurityToken;
            var jti = tokenS.Claims.First(claim => claim.Type == "Id").Value;

            var user = await userInfoes.GetUserBasicInfoesbyId(jti);
            var employee = await employeeService.GetEmployeeLoadViewModels();
            employee = employee.Where(x => x.employeeNo == user.UserName).ToList();
            if (employee?.FirstOrDefault()?.Token != uid && employee.Count() != 0)
            {
                bool status = false;
                string actionresult = "Invalid Token.";
                var jwts = await Tokens.changePasswordJwt(status, actionresult, new JsonSerializerSettings { Formatting = Formatting.Indented });

                return new OkObjectResult(jwts);

            }
            bool result = false;
            result = await _doctorScheduleService.setCheckInOut(jti, model.latitude, model.longitude, model.dateTime, Convert.ToInt32(model.flag), model.address, model.opinion, model.time, model.isHQ, model.isEHQ, model.isOS, model.isOther);

            //DateTime ValidTo = jsonToken.ValidTo;

            if (result == true)
            {
                var jwt = await Tokens.setLocationSuccessJwt(new JsonSerializerSettings { Formatting = Formatting.Indented });

                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setLocationfailJwt(new JsonSerializerSettings { Formatting = Formatting.Indented });

                return new OkObjectResult(jwt);
            }
            // int cnic = jsonData.enad_list[0].et_cnic;




        }

        #endregion 


        #region Doctor

        [HttpPost("setDoctor")]
        public async Task<IActionResult> setDoctor([FromBody] DoctorSetParamViewModel model)
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
            if (model.DoctorName == null)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Doctor Status has not created successfully.", false);
                return new OkObjectResult(jwt);
            }

            //bool result = await _doctorService.SaveActivityStatus(user.employeeId.ToString(), model);
            bool result = await _doctorService.setDoctorAPI(user.employeeId.ToString(), Convert.ToInt32(model.DoctorID), model.DoctorName, model.Address, model.Latitude, model.Longitude, model.MobileNo, model.Speciality, model.Institude, model.Designation, model.Degree, model.NoOfPatient, model.MarketID, model.TerritoryID, model.AreaId, model.RegionId, model.DepoId, model.ZoneId, model.DoctorCategoryId);
            if (model.lstDetailsViewModel.Count() > 0)
            {
                var doctorid = 0;
                if (Convert.ToInt32(model.DoctorID) > 0)
                {
                    doctorid = Convert.ToInt32(model.DoctorID);
                    await _doctorService.DeleteDoctorRxById(user.employeeId.ToString(), Convert.ToInt32(model.DoctorID));
                }
                else
                {
                    var data = await _doctorService.GetAllCmnDoctor();
                    doctorid = data.Select(x => x.DoctorID).Max();
                    await _doctorService.DeleteDoctorRxById(user.employeeId.ToString(), Convert.ToInt32(doctorid));

                }
                foreach (var data in model.lstDetailsViewModel)
                {

                    await _doctorService.setDoctorRx(user.employeeId.ToString(), Convert.ToInt32(0), doctorid, data.productId, data.productWiseSpecificationId, data.Quantity, 1);
                }

            }


            if (result == true)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Doctor created successfully.", true);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Doctor has not created successfully.", false);
                return new OkObjectResult(jwt);
            }

        }


        [HttpPost("setDoctorchemistDeleteHistory")]
        public async Task<IActionResult> setDoctorchemistDeleteHistory([FromBody] DoctorchemistDeleteHistoryViewModel model)
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
            var employee = await userInfoes.GetEmployeeById((int)user.employeeId);
            if (user.token != uid && user != null)
            {
                bool status = false;
                string actionresult = "Invalid Token.";
                var jwts = await Tokens.changePasswordJwt(status, actionresult, new JsonSerializerSettings { Formatting = Formatting.Indented });
                return new OkObjectResult(jwts);
            }
            if (model.type == null)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Please Select Type", false);
                return new OkObjectResult(jwt);
            }

            //bool result = await _doctorService.SaveActivityStatus(user.employeeId.ToString(), model);
            bool result = await _doctorService.setDoctorchemistDeleteHistory(user.employeeId.ToString(), Convert.ToInt32(model.DoctorDeleteHistoryID), (int)model.type, model.doctorCode, model.chemistCode, (int)model.status);

            if (result == true)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Doctor/Chemist deleted successfully.", true);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Doctor/Chemist  has not deleted successfully.", false);
                return new OkObjectResult(jwt);
            }

        }

        [HttpPost("setDoctorUnderObservation")]
        public async Task<IActionResult> setDoctorUnderObservation([FromBody] DoctorUnderObjervationViewModel model)
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
            var employee = await userInfoes.GetEmployeeById((int)user.employeeId);
            if (user.token != uid && user != null)
            {
                bool status = false;
                string actionresult = "Invalid Token.";
                var jwts = await Tokens.changePasswordJwt(status, actionresult, new JsonSerializerSettings { Formatting = Formatting.Indented });
                return new OkObjectResult(jwts);
            }
            if (model.DoctorName == null)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Doctor Status has not created successfully.", false);
                return new OkObjectResult(jwt);
            }

            //bool result = await _doctorService.SaveActivityStatus(user.employeeId.ToString(), model);
            bool result = await _doctorService.setDoctorUnderObserbationAPI(user.employeeId.ToString(), Convert.ToInt32(model.DoctorID), model.DoctorName, model.Address, model.Latitude, model.Longitude, model.MobileNo, model.Speciality, model.Institude, model.Designation, model.Degree, model.NoOfPatient, employee.TERRITORY_CODE, model.DoctorCategoryId, model.dateofBirth, model.dateofMarrige, model.favThings, model.practicePerMonth, model.honariumPerMonth,
                model.rxPerDay, model.rxPerMonth, model.docDutyType, model.productId1, model.productId1RxPerDay, model.productId2, model.productId2RxPerDay, model.productId3, model.productId3RxPerDay, model.productId4, model.productId4RxPerDay, model.productId5, model.productId5RxPerDay, model.productId6, model.productId6RxPerDay, model.chemberLocation, model.cmnDoctorId, model.status,model.MarketCode,model.MarketName,model.BasicDegreeId);

            if (result == true)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Doctor created successfully.", true);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Doctor has not created successfully.", false);
                return new OkObjectResult(jwt);
            }

        }

        [HttpPost("deleteDoctor")]
        public async Task<IActionResult> deleteDoctor([FromBody] DoctorSetParamViewModel model)
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

            if (model.Id == "")
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Doctor has not deleted.", false);
                return new OkObjectResult(jwt);
            }

            bool result = await _doctorService.DeleteDoctorById(user.employeeId.ToString(), Convert.ToInt32(model.Id));

            if (result == true)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Doctor has deleted successfully.", true);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Doctor has not deleted.", false);
                return new OkObjectResult(jwt);
            }
        }

        [HttpPost("setPlanDoctor")]
        public async Task<IActionResult> setPlanDoctor([FromBody] DoctorScheduleParamViewModel model)
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
            // var jsonToken = handler.ReadToken(stream);

            var tokenS = handler.ReadToken(stream) as JwtSecurityToken;
            var jti = tokenS.Claims.First(claim => claim.Type == "Id").Value;
            var user = await userInfoes.GetUserBasicInfoesbyId(jti);
            var employee = await employeeService.GetEmployeeLoadViewModels();
            employee = employee.Where(x => x.employeeNo == user.UserName).ToList();
            if (employee?.FirstOrDefault()?.Token != uid && employee.Count() != 0)
            {
                bool status = false;
                string actionresult = "Invalid Token.";
                var jwts = await Tokens.changePasswordJwt(status, actionresult, new JsonSerializerSettings { Formatting = Formatting.Indented });
                return new OkObjectResult(jwts);
            }
            if (model.DoctorID == null || model.visitDate == null || model.DoctorID == 0 || model.visitDate == "")
            {
                var jwt = await Tokens.setDocplanFailJwt(new JsonSerializerSettings { Formatting = Formatting.Indented });
                return new OkObjectResult(jwt);
            }
            bool result = await _doctorScheduleService.setPlanDoctor(jti, model.RosterID, model.DoctorID, Convert.ToDateTime(model.visitDate), model.VisitTime, model.Opinion);
            if (result == true)
            {
                var jwt = await Tokens.setDocplanSuccessJwt(new JsonSerializerSettings { Formatting = Formatting.Indented });
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setDocplanFailJwt(new JsonSerializerSettings { Formatting = Formatting.Indented });
                return new OkObjectResult(jwt);
            }
        }

        [HttpPost("updatePlanDoctor")]
        public async Task<IActionResult> updatePlanDoctor([FromForm] DoctorScheduleUpdateParamViewModel model)
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
            var employee = await employeeService.GetEmployeeLoadViewModels();
            employee = employee.Where(x => x.employeeNo == user.UserName).ToList();
            if (employee?.FirstOrDefault()?.Token != uid && employee.Count() != 0)
            {
                bool status = false;
                string actionresult = "Invalid Token.";
                var jwts = await Tokens.changePasswordJwt(status, actionresult, new JsonSerializerSettings { Formatting = Formatting.Indented });
                return new OkObjectResult(jwts);
            }
            #endregion

            if (model == null )
            {
                var jwt = await Tokens.updateDocplanFailJwt(new JsonSerializerSettings { Formatting = Formatting.Indented });
                return new OkObjectResult(jwt);
            }
            string location = "";
            string fileName = "";
            if (model.ImageUrl != null)
            {
                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".webp" };
                string message = "success";
                var extention = Path.GetExtension(model.ImageUrl.FileName);
                if (model.ImageUrl.Length > 2000000)
                    message = "Select jpg or jpeg or png less than 2Μ";
                else if (!allowedExtensions.Contains(extention.ToLower()))
                    message = "Must be jpeg or png";
                fileName = DateTime.Now.Ticks + extention;
                location = Path.Combine("visitedImage", fileName);
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", location);
                using (var streams = new FileStream(path, FileMode.Create))
                {
                    model.ImageUrl.CopyTo(streams);
                }
            }

            //List<ProductSubCatGetViewModel> lstModel = JsonConvert.DeserializeObject<List<ProductSubCatGetViewModel>>(model.lstModel);

            int DoctorScheduleID = await _doctorScheduleService.updatePlanDoctor(jti, model.PlanID, location, model.VisitTime, model.Latitude, model.Longitude, model.Remarks, model.LLAddress, model.ExecutionType, model.territoryCode,model.DoctorID
                );

            if (model.ExecutionType==2)
            {
                int result = await _doctorScheduleService.setDocExecutionDetails(jti, DoctorScheduleID, model.lstDocExecutionDetailsModel,model.territoryCode);
                if (result == 0)
                {
                    var jwt = await Tokens.updateDocplanFailJwt(new JsonSerializerSettings { Formatting = Formatting.Indented });
                    return new OkObjectResult(jwt);
                    
                }
                else
                {
                    var jwt = await Tokens.updateDocplanSuccessJwt(new JsonSerializerSettings { Formatting = Formatting.Indented });
                    return new OkObjectResult(jwt);
                }
            }
            else
            {
                if (DoctorScheduleID == 0)
                {
                    var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Doctor Schedule has not updated", false);
                    return new OkObjectResult(jwt);
                }
                else
                {
                    var jwt = await Tokens.updateDocplanSuccessJwt(new JsonSerializerSettings { Formatting = Formatting.Indented });
                    return new OkObjectResult(jwt);
                }
            }
            
        }

        [HttpPost("updatePlanDoctorstartTime")]
        public async Task<IActionResult> updatePlanDoctorstartTime([FromBody] DoctorScheduleStartUpdateParamViewModel model)
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
            var employee = await employeeService.GetEmployeeLoadViewModels();
            employee = employee.Where(x => x.employeeNo == user.UserName).ToList();
            if (employee?.FirstOrDefault()?.Token != uid && employee.Count() != 0)
            {
                bool status = false;
                string actionresult = "Invalid Token.";
                var jwts = await Tokens.changePasswordJwt(status, actionresult, new JsonSerializerSettings { Formatting = Formatting.Indented });
                return new OkObjectResult(jwts);
            }
            if (model.PlanID == null || model.PlanID == 0)
            {
                var jwt = await Tokens.updateDocplanFailJwt(new JsonSerializerSettings { Formatting = Formatting.Indented });
                return new OkObjectResult(jwt);
            }

            bool result = await _doctorScheduleService.UpdatePlanDoctorstartTime(jti, model.PlanID, model.startTime, model.Latitude, model.Longitude);
            if (result == true)
            {
                var jwt = await Tokens.updateDocplanSuccessJwt(new JsonSerializerSettings { Formatting = Formatting.Indented });
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.updateDocplanFailJwt(new JsonSerializerSettings { Formatting = Formatting.Indented });
                return new OkObjectResult(jwt);
            }
        }

        [HttpPost("PlanSetExecuteDoctor")]
        public async Task<IActionResult> PlanSetExecuteDoctor([FromForm] DoctorScheduleExecutionParamViewModel model)
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
            // var jsonToken = handler.ReadToken(stream);

            var tokenS = handler.ReadToken(stream) as JwtSecurityToken;
            var jti = tokenS.Claims.First(claim => claim.Type == "Id").Value;

            var user = await userInfoes.GetUserBasicInfoesbyId(jti);
            var employee = await employeeService.GetEmployeeLoadViewModels();
            employee = employee.Where(x => x.employeeNo == user.UserName).ToList();
            if (employee?.FirstOrDefault()?.Token != uid && employee.Count() != 0)
            {
                bool status = false;
                string actionresult = "Invalid Token.";
                var jwts = await Tokens.changePasswordJwt(status, actionresult, new JsonSerializerSettings { Formatting = Formatting.Indented });

                return new OkObjectResult(jwts);

            }
            if (model.DoctorID == null || model.DoctorID == 0 || model.MarketScheduleID == null || model.MarketScheduleID == 0 || model.RosterID == null || model.RosterID == 0)
            //if (PlanID == null|| PlanID == "0")
            {
                var jwt = await Tokens.updateDocplanFailJwt(new JsonSerializerSettings { Formatting = Formatting.Indented });

                return new OkObjectResult(jwt);
            }
            string location = "";
            string fileName = "";
            if (model.ImageUrl != null)
            {
                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".webp" };
                string message = "success";

                var extention = Path.GetExtension(model.ImageUrl.FileName);
                if (model.ImageUrl.Length > 2000000)
                    message = "Select jpg or jpeg or png less than 2Μ";
                else if (!allowedExtensions.Contains(extention.ToLower()))
                    message = "Must be jpeg or png";

                fileName = DateTime.Now.Ticks + extention;
                location = Path.Combine("visitedImage", fileName);
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", location);
                using (var streams = new FileStream(path, FileMode.Create))
                {
                    model.ImageUrl.CopyTo(streams);
                }

            }
            bool result = await _doctorScheduleService.PlanExecutionDoctor(jti, model.RosterID, model.DoctorID, model.MarketScheduleID, location, model.visitDate, model.VisitTime, model.Latitude, model.Longitude, model.Remarks, model.LLAddress);

            if (result == true)
            {
                var jwt = await Tokens.updateDocplanSuccessJwt(new JsonSerializerSettings { Formatting = Formatting.Indented });

                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.updateDocplanFailJwt(new JsonSerializerSettings { Formatting = Formatting.Indented });

                return new OkObjectResult(jwt);
            }
            // int cnic = jsonData.enad_list[0].et_cnic;
        }

        [HttpGet("getDrListAfterSetPlan")]
        public async Task<IActionResult> getDrListAfterSetPlan(string visitDate,string employeeNo)
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
            var employee = await employeeService.GetEmployeeLoadViewModels();
            employee = employee.Where(x => x.employeeNo == user.UserName).ToList();
            if (employee?.FirstOrDefault()?.Token != uid && employee.Count() != 0)
            {
                bool status = false;
                string actionresult = "Invalid Token.";
                var jwts = await Tokens.changePasswordJwt(status, actionresult, new JsonSerializerSettings { Formatting = Formatting.Indented });
                return new OkObjectResult(jwts);
            }
            if (visitDate == null || visitDate == "")
            {
                var jwt = await Tokens.getDoctorSchedulelistaftersetfail(new JsonSerializerSettings { Formatting = Formatting.Indented });
                return new OkObjectResult(jwt);
            }

            var datajson = await _doctorScheduleService.getDrListAfterSetPlan(jti, visitDate, 0,employeeNo);
            if (datajson.data != "[]")
            {
                var jwt = await Tokens.getDataWithStatusAndMessage(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented }, "Doctor's schedules are found successfully.");
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.getDataWithStatusAndMessage(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented }, "No schedule for doctor's visit is found");
                return new OkObjectResult(jwt);
            }
        }

        [HttpGet("getDashboardAttendanceDetails")]
        public async Task<IActionResult> getDashboardAttendanceDetails(string usertype, string type, string ZoneCode, string RegionCode, string AreaCode, DateTime date, string TerritoryCode)
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
            var employee = await employeeService.GetEmployeeLoadViewModels();
            employee = employee.Where(x => x.employeeNo == user.UserName).ToList();
            if (employee?.FirstOrDefault()?.Token != uid && employee.Count() != 0)
            {
                bool status = false;
                string actionresult = "Invalid Token.";
                var jwts = await Tokens.changePasswordJwt(status, actionresult, new JsonSerializerSettings { Formatting = Formatting.Indented });
                return new OkObjectResult(jwts);
            }
            if (usertype == null || usertype == "")
            {
                var jwt = await Tokens.getDoctorSchedulelistaftersetfail(new JsonSerializerSettings { Formatting = Formatting.Indented });
                return new OkObjectResult(jwt);
            }

            var datajson = await _chemistScheduleService.getDashboardAttendanceDetails(jti, usertype, type, ZoneCode, RegionCode, AreaCode, date, TerritoryCode);
            if (datajson.data != "[]")
            {
                var jwt = await Tokens.getDataWithStatusAndMessage(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented }, "Attendance are found successfully.");
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.getDataWithStatusAndMessage(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented }, "No Attendance is found");
                return new OkObjectResult(jwt);
            }
        }

        [HttpGet("getDashboardAttendanceDetails_V2")]
        public async Task<IActionResult> getDashboardAttendanceDetails_V2(string usertype, string type, string ZoneCode, string RegionCode, string AreaCode, DateTime date, string userName, string TerritoryCode)
        {
            //var uid = Request.Headers["auth_token"];
            //if (uid.Count() == 0)
            //{
            //    bool status = false;
            //    string actionresult = "Invalid Token.";
            //    var jwts = await Tokens.changePasswordJwt(status, actionresult, new JsonSerializerSettings { Formatting = Formatting.Indented });
            //    return new OkObjectResult(jwts);
            //}
            //var stream = uid;
            //var handler = new JwtSecurityTokenHandler();
            //var tokenS = handler.ReadToken(stream) as JwtSecurityToken;
            //var jti = tokenS.Claims.First(claim => claim.Type == "Id").Value;
            //var user = await userInfoes.GetUserBasicInfoesbyId(jti);
            //var employee = await employeeService.GetEmployeeLoadViewModels();
            //employee = employee.Where(x => x.employeeNo == user.UserName).ToList();
            //if (employee?.FirstOrDefault()?.Token != uid && employee.Count() != 0)
            //{
            //    bool status = false;
            //    string actionresult = "Invalid Token.";
            //    var jwts = await Tokens.changePasswordJwt(status, actionresult, new JsonSerializerSettings { Formatting = Formatting.Indented });
            //    return new OkObjectResult(jwts);
            //}
            //if (usertype == null || usertype == "")
            //{
            //    var jwt = await Tokens.getDoctorSchedulelistaftersetfail(new JsonSerializerSettings { Formatting = Formatting.Indented });
            //    return new OkObjectResult(jwt);
            //}

            var user = await userInfoes.GetUserBasicInfoes(userName);

            var datajson = await _chemistScheduleService.getDashboardAttendanceDetails(user.Id, usertype, type, ZoneCode, RegionCode, AreaCode, date, TerritoryCode);
            if (datajson.data != "[]")
            {
                var jwt = await Tokens.getDataWithStatusAndMessage(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented }, "Attendance are found successfully.");
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.getDataWithStatusAndMessage(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented }, "No Attendance is found");
                return new OkObjectResult(jwt);
            }
        }

        [HttpPost("deletePlanDoctor")]
        public async Task<IActionResult> deletePlanDoctor([FromBody] DoctorScheduleUpdateParamViewModel model)
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
            var employee = await employeeService.GetEmployeeLoadViewModels();
            employee = employee.Where(x => x.employeeNo == user.UserName).ToList();
            if (employee?.FirstOrDefault()?.Token != uid && employee.Count() != 0)
            {
                bool status = false;
                string actionresult = "Invalid Token.";
                var jwts = await Tokens.changePasswordJwt(status, actionresult, new JsonSerializerSettings { Formatting = Formatting.Indented });
                return new OkObjectResult(jwts);
            }
            if (model.PlanID == null || model.PlanID == 0)
            {
                var jwt = await Tokens.setJwtWithStatus(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Doctor plan has not deleted.", false);
                return new OkObjectResult(jwt);
            }
            bool result = await _doctorScheduleService.deletePlanDoctor(user.employeeId.ToString(), model.PlanID);

            if (result == true)
            {
                var jwt = await Tokens.setJwtWithStatus(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Doctor plan has deleted successfully.", true);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwtWithStatus(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Doctor plan has not deleted.", false);
                return new OkObjectResult(jwt);
            }
        }


        [HttpGet("GetDoctorCategory")]
        public async Task<IActionResult> GetDoctorCategory(int doctorCategoryId)
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
            var jsonToken = handler.ReadToken(stream);

            var tokenS = handler.ReadToken(stream) as JwtSecurityToken;
            var jti = tokenS.Claims.First(claim => claim.Type == "Id").Value;
            var user = await userInfoes.GetUserBasicInfoesbyId(jti);
            var employee = await employeeService.GetEmployeeLoadViewModels();
            employee = employee.Where(x => x.employeeNo == user.UserName).ToList();

            if (employee?.FirstOrDefault()?.Token != uid && employee.Count() != 0)
            {
                bool status = false;
                string actionresult = "Invalid Token.";
                var jwts = await Tokens.changePasswordJwt(status, actionresult, new JsonSerializerSettings { Formatting = Formatting.Indented });

                return new OkObjectResult(jwts);

            }

            var datajson = await _doctorService.GetDoctorCategory(doctorCategoryId,(int)user.employeeId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);

        }

        [HttpPost("setDoctorCategory")]
        public async Task<IActionResult> setDoctorCategory([FromBody] DoctorCategorySetParamViewModel model)
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
            if (model.DoctorCategoryName == null)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Doctor Category has not created successfully.", false);
                return new OkObjectResult(jwt);
            }

            //bool result = await _doctorService.SaveActivityStatus(user.employeeId.ToString(), model);
            bool result = await _doctorService.setDoctorCategory(user.employeeId.ToString(), Convert.ToInt32(model.DoctorCategoryID), model.DoctorCategoryName, model.DoctorCategoryCode, model.IsActive);
            if (result == true)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Doctor Category created successfully.", true);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Doctor Category has not created successfully.", false);
                return new OkObjectResult(jwt);
            }

        }
        [HttpPost("deleteDoctorCategory")]
        public async Task<IActionResult> deleteDoctorCategory([FromBody] DoctorCategorySetParamViewModel model)
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
            var employee = await employeeService.GetEmployeeLoadViewModels();
            employee = employee.Where(x => x.employeeNo == user.UserName).ToList();
            if (employee?.FirstOrDefault()?.Token != uid && employee.Count() != 0)
            {
                bool status = false;
                string actionresult = "Invalid Token.";
                var jwts = await Tokens.changePasswordJwt(status, actionresult, new JsonSerializerSettings { Formatting = Formatting.Indented });
                return new OkObjectResult(jwts);
            }
            if (model.DoctorCategoryID == null || model.DoctorCategoryID == 0)
            {
                var jwt = await Tokens.setJwtWithStatus(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Doctor Category has not deleted.", false);
                return new OkObjectResult(jwt);
            }
            bool result = await _doctorService.DeleteDoctorCategoryById(user.employeeId.ToString(), model.DoctorCategoryID);

            if (result == true)
            {
                var jwt = await Tokens.setJwtWithStatus(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Doctor Category has deleted successfully.", true);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwtWithStatus(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Doctor Category has not deleted.", false);
                return new OkObjectResult(jwt);
            }
        }

        [HttpGet("GetDoctorRx")]
        public async Task<IActionResult> GetDoctorRx(int doctorId)
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
            var jsonToken = handler.ReadToken(stream);

            var tokenS = handler.ReadToken(stream) as JwtSecurityToken;
            var jti = tokenS.Claims.First(claim => claim.Type == "Id").Value;
            var user = await userInfoes.GetUserBasicInfoesbyId(jti);
            var employee = await employeeService.GetEmployeeLoadViewModels();
            employee = employee.Where(x => x.employeeNo == user.UserName).ToList();

            if (employee?.FirstOrDefault()?.Token != uid && employee.Count() != 0)
            {
                bool status = false;
                string actionresult = "Invalid Token.";
                var jwts = await Tokens.changePasswordJwt(status, actionresult, new JsonSerializerSettings { Formatting = Formatting.Indented });

                return new OkObjectResult(jwts);

            }

            var datajson = await _doctorService.GetDoctorRx(doctorId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);

        }

        [HttpPost("setDoctorRx")]
        public async Task<IActionResult> setDoctorRx([FromBody] DoctorRxSetParamViewModel model)
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
            //if (model.doctorID == null)
            //{
            //    var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Doctor Rx has not created successfully.", false);
            //    return new OkObjectResult(jwt);
            //}

            //bool result = await _doctorService.SaveActivityStatus(user.employeeId.ToString(), model);
            bool result = await _doctorService.setDoctorRx(user.employeeId.ToString(), Convert.ToInt32(0), 0, model.productId, 0, 0, 1);
            if (result == true)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Doctor Category created successfully.", true);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Doctor Category has not created successfully.", false);
                return new OkObjectResult(jwt);
            }

        }
        [HttpPost("deleteDoctorRx")]
        public async Task<IActionResult> deleteDoctorRx([FromBody] DoctorRxSetParamViewModel model)
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
            var employee = await employeeService.GetEmployeeLoadViewModels();
            employee = employee.Where(x => x.employeeNo == user.UserName).ToList();
            if (employee?.FirstOrDefault()?.Token != uid && employee.Count() != 0)
            {
                bool status = false;
                string actionresult = "Invalid Token.";
                var jwts = await Tokens.changePasswordJwt(status, actionresult, new JsonSerializerSettings { Formatting = Formatting.Indented });
                return new OkObjectResult(jwts);
            }
            //if (model.doctorID == null || model.doctorID == 0)
            //{
            //    var jwt = await Tokens.setJwtWithStatus(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Doctor Rx has not deleted.", false);
            //    return new OkObjectResult(jwt);
            //}
            bool result = await _doctorService.DeleteDoctorRxById(user.employeeId.ToString(), 0);

            if (result == true)
            {
                var jwt = await Tokens.setJwtWithStatus(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Doctor Rx has deleted successfully.", true);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwtWithStatus(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Doctor Rx has not deleted.", false);
                return new OkObjectResult(jwt);
            }
        }



        [HttpGet("getDashboardReportApp")]
        public async Task<IActionResult> getDashboardReportApp(DateTime date, string ZoneCode, string RegionCode, string AreaCode, string TerritoryCode)
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
            var employee = await employeeService.GetEmployeeLoadViewModels();
            employee = employee.Where(x => x.employeeNo == user.UserName).ToList();
            if (employee?.FirstOrDefault()?.Token != uid && employee.Count() != 0)
            {
                bool status = false;
                string actionresult = "Invalid Token.";
                var jwts = await Tokens.changePasswordJwt(status, actionresult, new JsonSerializerSettings { Formatting = Formatting.Indented });
                return new OkObjectResult(jwts);
            }

            var datajson = await _doctorScheduleService.getDashBoardPlanApp(jti, date, ZoneCode, RegionCode, AreaCode, TerritoryCode);
            var datajsonattn = await _doctorScheduleService.getDashBoardAttnApp(jti, date, ZoneCode, RegionCode, AreaCode, TerritoryCode);
            if (datajson.data != "[]")
            {
                var jwt = await Tokens.getDashboard(datajson.data, datajsonattn.data, new JsonSerializerSettings { Formatting = Formatting.Indented }, "data found successfully.");
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.getDashboard(datajson.data, datajsonattn.data, new JsonSerializerSettings { Formatting = Formatting.Indented }, "data not found.");
                return new OkObjectResult(jwt);
            }
        }

        [HttpGet("getDashboardReportAppATT")]
        public async Task<IActionResult> getDashboardReportAppATT(DateTime date, string ZoneCode, string RegionCode, string AreaCode, string TerritoryCode)
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
            var employee = await employeeService.GetEmployeeLoadViewModels();
            employee = employee.Where(x => x.employeeNo == user.UserName).ToList();
            if (employee?.FirstOrDefault()?.Token != uid && employee.Count() != 0)
            {
                bool status = false;
                string actionresult = "Invalid Token.";
                var jwts = await Tokens.changePasswordJwt(status, actionresult, new JsonSerializerSettings { Formatting = Formatting.Indented });
                return new OkObjectResult(jwts);
            }

        //    var datajson = await _doctorScheduleService.getDashBoardPlanApp(jti, date, ZoneCode, RegionCode, AreaCode, TerritoryCode);
            var datajsonattn = await _doctorScheduleService.getDashBoardAttnApp(jti, date, ZoneCode, RegionCode, AreaCode, TerritoryCode);
            if (datajsonattn.data != "[]")
            {
                var jwt = await Tokens.getDashboardAtt( datajsonattn.data, new JsonSerializerSettings { Formatting = Formatting.Indented }, "data found successfully.");
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.getDashboardAtt( datajsonattn.data, new JsonSerializerSettings { Formatting = Formatting.Indented }, "data not found.");
                return new OkObjectResult(jwt);
            }
        }

        [HttpGet("getDashboardReportAppDaily")]
        public async Task<IActionResult> getDashboardReportAppDaily(DateTime date, string ZoneCode, string RegionCode, string AreaCode, string TerritoryCode)
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
            var employee = await employeeService.GetEmployeeLoadViewModels();
            employee = employee.Where(x => x.employeeNo == user.UserName).ToList();
            if (employee?.FirstOrDefault()?.Token != uid && employee.Count() != 0)
            {
                bool status = false;
                string actionresult = "Invalid Token.";
                var jwts = await Tokens.changePasswordJwt(status, actionresult, new JsonSerializerSettings { Formatting = Formatting.Indented });
                return new OkObjectResult(jwts);
            }

            var datajson = await _doctorScheduleService.getDashBoardPlanAppDaily(jti, date, ZoneCode, RegionCode, AreaCode, TerritoryCode);
            //var datajsonattn = await _doctorScheduleService.getDashBoardAttnApp(jti, date, ZoneCode, RegionCode, AreaCode, TerritoryCode);
            if (datajson.data != "[]")
            {
                var jwt = await Tokens.getDashboardDaily(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented }, "data found successfully.");
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.getDashboardDaily(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented }, "data not found.");
                return new OkObjectResult(jwt);
            }
        }


        [HttpGet("getDashboardReportAppMonthly")]
        public async Task<IActionResult> getDashboardReportAppMonthly(DateTime date, string ZoneCode, string RegionCode, string AreaCode, string TerritoryCode)
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
            var employee = await employeeService.GetEmployeeLoadViewModels();
            employee = employee.Where(x => x.employeeNo == user.UserName).ToList();
            if (employee?.FirstOrDefault()?.Token != uid && employee.Count() != 0)
            {
                bool status = false;
                string actionresult = "Invalid Token.";
                var jwts = await Tokens.changePasswordJwt(status, actionresult, new JsonSerializerSettings { Formatting = Formatting.Indented });
                return new OkObjectResult(jwts);
            }

            var datajson = await _doctorScheduleService.getDashBoardPlanApp3(jti, date, ZoneCode, RegionCode, AreaCode, TerritoryCode);
            //var datajsonattn = await _doctorScheduleService.getDashBoardAttnApp(jti, date, ZoneCode, RegionCode, AreaCode, TerritoryCode);
            if (datajson.data1 != "[]")
            {
                var jwt = await Tokens.getDashboardMonthly(datajson.data1, datajson.data2, datajson.data3, new JsonSerializerSettings { Formatting = Formatting.Indented }, "data found successfully.");
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.getDashboardMonthly(datajson.data1, datajson.data2, datajson.data3, new JsonSerializerSettings { Formatting = Formatting.Indented }, "data not found.");
                return new OkObjectResult(jwt);
            }
        }
        [HttpGet("getDashboardReportAppMonthlyAsOnProductivity")]
        public async Task<IActionResult> getDashboardReportAppMonthlyAsOnProductivity(DateTime date, string ZoneCode, string RegionCode, string AreaCode, string TerritoryCode)
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
            var employee = await employeeService.GetEmployeeLoadViewModels();
            employee = employee.Where(x => x.employeeNo == user.UserName).ToList();
            if (employee?.FirstOrDefault()?.Token != uid && employee.Count() != 0)
            {
                bool status = false;
                string actionresult = "Invalid Token.";
                var jwts = await Tokens.changePasswordJwt(status, actionresult, new JsonSerializerSettings { Formatting = Formatting.Indented });
                return new OkObjectResult(jwts);
            }

            var datajson = await _doctorScheduleService.getDashBoardPlanApp4(jti, date, ZoneCode, RegionCode, AreaCode, TerritoryCode);
            //var datajsonattn = await _doctorScheduleService.getDashBoardAttnApp(jti, date, ZoneCode, RegionCode, AreaCode, TerritoryCode);
            if (datajson.data3 != "[]")
            {
                var jwt = await Tokens.getDashboardMonthlyproductvity( datajson.data3, new JsonSerializerSettings { Formatting = Formatting.Indented }, "data found successfully.");
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.getDashboardMonthlyproductvity(datajson.data3, new JsonSerializerSettings { Formatting = Formatting.Indented }, "data not found.");
                return new OkObjectResult(jwt);
            }
        }

        [HttpGet("getDashboardReportAppMonthlyAsOnCollection")]
        public async Task<IActionResult> getDashboardReportAppMonthlyAsOnCollection(DateTime date, string ZoneCode, string RegionCode, string AreaCode, string TerritoryCode)
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
            var employee = await employeeService.GetEmployeeLoadViewModels();
            employee = employee.Where(x => x.employeeNo == user.UserName).ToList();
            if (employee?.FirstOrDefault()?.Token != uid && employee.Count() != 0)
            {
                bool status = false;
                string actionresult = "Invalid Token.";
                var jwts = await Tokens.changePasswordJwt(status, actionresult, new JsonSerializerSettings { Formatting = Formatting.Indented });
                return new OkObjectResult(jwts);
            }

            var datajson = await _doctorScheduleService.getDashBoardPlanApp5(jti, date, ZoneCode, RegionCode, AreaCode, TerritoryCode);
            //var datajsonattn = await _doctorScheduleService.getDashBoardAttnApp(jti, date, ZoneCode, RegionCode, AreaCode, TerritoryCode);
            if (datajson.data3 != "[]")
            {
                var jwt = await Tokens.getDashboardMonthlyproductvity(datajson.data3, new JsonSerializerSettings { Formatting = Formatting.Indented }, "data found successfully.");
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.getDashboardMonthlyproductvity(datajson.data3, new JsonSerializerSettings { Formatting = Formatting.Indented }, "data not found.");
                return new OkObjectResult(jwt);
            }
        }


        [HttpGet("getDashboardReportApp_V2")]
        public async Task<IActionResult> getDashboardReportApp_V2(DateTime date, string ZoneCode, string RegionCode, string AreaCode, string userName)
        {
            //var uid = Request.Headers["auth_token"];
            //if (uid.Count() == 0)
            //{
            //    bool status = false;
            //    string actionresult = "Invalid Token.";
            //    var jwts = await Tokens.changePasswordJwt(status, actionresult, new JsonSerializerSettings { Formatting = Formatting.Indented });
            //    return new OkObjectResult(jwts);
            //}
            //var stream = uid;
            //var handler = new JwtSecurityTokenHandler();
            //var tokenS = handler.ReadToken(stream) as JwtSecurityToken;
            //var jti = tokenS.Claims.First(claim => claim.Type == "Id").Value;
            //var user = await userInfoes.GetUserBasicInfoesbyId(jti);
            //var employee = await employeeService.GetEmployeeLoadViewModels();
            //employee = employee.Where(x => x.employeeNo == user.UserName).ToList();
            //if (employee?.FirstOrDefault()?.Token != uid && employee.Count() != 0)
            //{
            //    bool status = false;
            //    string actionresult = "Invalid Token.";
            //    var jwts = await Tokens.changePasswordJwt(status, actionresult, new JsonSerializerSettings { Formatting = Formatting.Indented });
            //    return new OkObjectResult(jwts);
            //}


            var user = await userInfoes.GetUserBasicInfoes(userName);
            var datajson = await _doctorScheduleService.getDashBoardPlanApp(user.Id, date, ZoneCode, RegionCode, AreaCode, null);
            var datajsonattn = await _doctorScheduleService.getDashBoardAttnApp(user.Id, date, ZoneCode, RegionCode, AreaCode, null);

            if (datajson.data != "[]")
            {
                var jwt = await Tokens.getDashboard(datajson.data, datajsonattn.data, new JsonSerializerSettings { Formatting = Formatting.Indented }, "data found successfully.");
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.getDashboard(datajson.data, datajsonattn.data, new JsonSerializerSettings { Formatting = Formatting.Indented }, "data not found.");
                return new OkObjectResult(jwt);
            }
        }

        #endregion

        #region Employee

        [HttpPost("PlanSetExecuteEmp")]
        public async Task<IActionResult> PlanSetExecuteEmp([FromForm] EmpScheduleExecutionParamViewModel model)
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
            var employee = await employeeService.GetEmployeeLoadViewModels();
            employee = employee.Where(x => x.employeeNo == user.UserName).ToList();
            if (employee?.FirstOrDefault()?.Token != uid && employee.Count() != 0)
            {
                bool status = false;
                string actionresult = "Invalid Token.";
                var jwts = await Tokens.changePasswordJwt(status, actionresult, new JsonSerializerSettings { Formatting = Formatting.Indented });
                return new OkObjectResult(jwts);
            }
            if (model.EmpCode == null || model.EmpCode == "" || model.MarketScheduleID == null || model.MarketScheduleID == 0 || model.RosterID == null || model.RosterID == 0)
            //if (PlanID == null|| PlanID == "0")
            {
                var jwt = await Tokens.updateEmpplanFailJwt(new JsonSerializerSettings { Formatting = Formatting.Indented });
                return new OkObjectResult(jwt);
            }
            string location = "";
            string fileName = "";
            if (model.ImageUrl != null)
            {
                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".webp" };
                string message = "success";

                var extention = Path.GetExtension(model.ImageUrl.FileName);
                if (model.ImageUrl.Length > 2000000)
                    message = "Select jpg or jpeg or png less than 2Μ";
                else if (!allowedExtensions.Contains(extention.ToLower()))
                    message = "Must be jpeg or png";

                fileName = DateTime.Now.Ticks + extention;
                location = Path.Combine("visitedImage", fileName);
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", location);
                using (var streams = new FileStream(path, FileMode.Create))
                {
                    model.ImageUrl.CopyTo(streams);
                }

            }
            bool result = await _doctorScheduleService.PlanExecutionEmp(jti, model.RosterID, model.EmpCode, model.MarketScheduleID, location, model.VisitDate, model.VisitTime, model.Latitude, model.Longitude, model.Remarks, model.LLAddress);

            if (result == true)
            {
                var jwt = await Tokens.updateEmpplanSuccessJwt(new JsonSerializerSettings { Formatting = Formatting.Indented });
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.updateEmpplanFailJwt(new JsonSerializerSettings { Formatting = Formatting.Indented });
                return new OkObjectResult(jwt);
            }
            // int cnic = jsonData.enad_list[0].et_cnic;
        }


        [HttpPost("TAAmountUploadWithImage")]
        public async Task<IActionResult> TAAmountUploadWithImage([FromForm] List<TAAmountUploadViewModel> taAmountListModel)
        // public async Task<IActionResult> TAAmountUploadWithImage([FromForm] TaAmountList model)
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
            // var jsonToken = handler.ReadToken(stream);

            var tokenS = handler.ReadToken(stream) as JwtSecurityToken;
            var jti = tokenS.Claims.First(claim => claim.Type == "Id").Value;
            var user = await userInfoes.GetUserBasicInfoesbyId(jti);
            var employee = await employeeService.GetEmployeeLoadViewModels();
            employee = employee.Where(x => x.employeeNo == user.UserName).ToList();
            if (employee?.FirstOrDefault()?.Token != uid && employee.Count() != 0)
            {
                bool status = false;
                string actionresult = "Invalid Token.";
                var jwts = await Tokens.changePasswordJwt(status, actionresult, new JsonSerializerSettings { Formatting = Formatting.Indented });

                return new OkObjectResult(jwts);

            }
            int result = 0;


            foreach (var item in taAmountListModel)
            {
                // int cnTADAForEmployeeId = 0;
                // Update on TADAForEmployee table 
                int cnTADAForEmployeeId = await employeeService.setTAAmount((int)user.employeeId, item.taDate, item.taAmount);
                if (cnTADAForEmployeeId > 0) result = 1;
                if (item.ImageUrls != null)
                {
                    for (int i = 0; i < item.ImageUrls.Count(); i++)
                    {
                        string location = "";
                        string fileName = "";
                        if (item.ImageUrls[i] != null)
                        {
                            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".webp" };
                            string message = "success";

                            var extention = Path.GetExtension(item.ImageUrls[i].FileName);
                            if (item.ImageUrls[i].Length > 2000000)
                                message = "Select jpg or jpeg or png less than 2Μ";
                            else if (!allowedExtensions.Contains(extention.ToLower()))
                                message = "Must be jpeg or png";

                            fileName = DateTime.Now.Ticks + extention;
                            location = Path.Combine("TAReceipt", fileName);
                            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", location);
                            using (var streams = new FileStream(path, FileMode.Create))
                            {
                                item.ImageUrls[i].CopyTo(streams);
                            }
                        }
                        int receiptId = await employeeService.setTAAmountImages((int)user.employeeId, cnTADAForEmployeeId, item.taDate, location);
                    }
                }
            }


            if (result > 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "TA Amount uploaded successfully.", true);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "TA Amount has not  uploaded successfully", false);
                return new OkObjectResult(jwt);
            }
        }


        [HttpPost("TAUploadWithReceipts")]
        public async Task<IActionResult> TAUploadWithReceipts([FromForm] TaUploadViewModel model)
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
            try
            {
                var tokenS = handler.ReadToken(stream) as JwtSecurityToken;
                var jti = tokenS.Claims.First(claim => claim.Type == "Id").Value;
                var user = await userInfoes.GetUserBasicInfoesbyId(jti);
                var employee = await employeeService.GetEmployeeLoadViewModels();
                employee = employee.Where(x => x.employeeNo == user.UserName).ToList();
                if (employee?.FirstOrDefault()?.Token != uid && employee.Count() != 0)
                {
                    bool status = false;
                    string actionresult = "Invalid Token.";
                    var jwts = await Tokens.changePasswordJwt(status, actionresult, new JsonSerializerSettings { Formatting = Formatting.Indented });

                    return new OkObjectResult(jwts);

                }
                int result = 0;
                result = await employeeService.setTAAmount((int)user.employeeId, model.taDate, model.taAmount);
                if (model.ImageUrls != null)
                {
                    for (int i = 0; i < model.ImageUrls.Count(); i++)
                    {
                        string location = "";
                        string fileName = "";
                        if (model.ImageUrls[i] != null)
                        {
                            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".webp" };
                            string message = "success";

                            var extention = Path.GetExtension(model.ImageUrls[i].FileName);
                            if (model.ImageUrls[i].Length > 2000000)
                                message = "Select jpg or jpeg or png less than 2Μ";
                            else if (!allowedExtensions.Contains(extention.ToLower()))
                                message = "Must be jpeg or png";

                            fileName = DateTime.Now.Ticks + extention;
                            location = Path.Combine("TAReceipt", fileName);
                            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", location);
                            using (var streams = new FileStream(path, FileMode.Create))
                            {
                                model.ImageUrls[i].CopyTo(streams);
                            }
                        }
                        int receiptId = await employeeService.setTAAmountImages((int)user.employeeId, result, model.taDate, location);
                    }
                }


                if (result > 0)
                {
                    var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "TA Amount uploaded successfully.", true);
                    return new OkObjectResult(jwt);
                }
                else
                {
                    var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "TA Amount has not  uploaded successfully", false);
                    return new OkObjectResult(jwt);
                }
            }
            catch (Exception ex)
            {

                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, ex.Message, false);
                return new OkObjectResult(jwt);
            }
            
        }

        #endregion

        #region Chemist

        [HttpPost("setPlanChemist")]
        public async Task<IActionResult> setPlanChemist([FromBody] ChemistScheduleParamViewModel model)
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
            var employee = await employeeService.GetEmployeeLoadViewModels();
            employee = employee.Where(x => x.employeeNo == user.UserName).ToList();
            if (employee?.FirstOrDefault()?.Token != uid && employee.Count() != 0)
            {
                bool status = false;
                string actionresult = "Invalid Token.";
                var jwts = await Tokens.changePasswordJwt(status, actionresult, new JsonSerializerSettings { Formatting = Formatting.Indented });
                return new OkObjectResult(jwts);
            }
            if (model.ChemistID == null || model.visitDate == null || model.ChemistID == 0 || model.visitDate == "")
            {
                var jwt = await Tokens.setChemistplanFailJwt(new JsonSerializerSettings { Formatting = Formatting.Indented });
                return new OkObjectResult(jwt);
            }
            bool result = await _chemistScheduleService.setPlanChemist(jti, model.RosterID, model.ChemistID, Convert.ToDateTime(model.visitDate), model.VisitTime, model.Opinion);

            if (result == true)
            {
                var jwt = await Tokens.setChemistplanSuccessJwt(new JsonSerializerSettings { Formatting = Formatting.Indented });
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setChemistplanFailJwt(new JsonSerializerSettings { Formatting = Formatting.Indented });
                return new OkObjectResult(jwt);
            }
        }

        [HttpPost("setChemist")]
        public async Task<IActionResult> setChemist([FromBody] ChemistListViewModel model)
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
            var employee = await employeeService.GetEmployeeLoadViewModels();
            employee = employee.Where(x => x.employeeNo == user.UserName).ToList();
            if (employee?.FirstOrDefault()?.Token != uid && employee.Count() != 0)
            {
                bool status = false;
                string actionresult = "Invalid Token.";
                var jwts = await Tokens.changePasswordJwt(status, actionresult, new JsonSerializerSettings { Formatting = Formatting.Indented });
                return new OkObjectResult(jwts);
            }
            bool result = await _chemistService.setChemist(model, (int)employee?.FirstOrDefault()?.employeeId);

            if (result == true)
            {
                var jwt = await Tokens.setChemistSuccessJwt(new JsonSerializerSettings { Formatting = Formatting.Indented });
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setChemistFailJwt(new JsonSerializerSettings { Formatting = Formatting.Indented });
                return new OkObjectResult(jwt);
            }
        }
       
        [HttpPost("updatePlanChemist")]
        public async Task<IActionResult> updatePlanChemist([FromForm] ChemistScheduleUpdateParamViewModel model)
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
            var employee = await employeeService.GetEmployeeLoadViewModels();
            employee = employee.Where(x => x.employeeNo == user.UserName).ToList();
            if (employee?.FirstOrDefault()?.Token != uid && employee.Count() != 0)
            {
                bool status = false;
                string actionresult = "Invalid Token.";
                var jwts = await Tokens.changePasswordJwt(status, actionresult, new JsonSerializerSettings { Formatting = Formatting.Indented });
                return new OkObjectResult(jwts);
            }
            #endregion
            if (model == null || model.PlanID == 0)
            {
                var jwt = await Tokens.updateChemistplanFailJwt(new JsonSerializerSettings { Formatting = Formatting.Indented });
                return new OkObjectResult(jwt);
            }
            string location = "";
            string fileName = "";
            if (model.ImageUrl != null)
            {
                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".webp" };
                string message = "success";

                var extention = Path.GetExtension(model.ImageUrl.FileName);
                if (model.ImageUrl.Length > 2000000)
                    message = "Select jpg or jpeg or png less than 2Μ";
                else if (!allowedExtensions.Contains(extention.ToLower()))
                    message = "Must be jpeg or png";

                fileName = DateTime.Now.Ticks + extention;
                location = Path.Combine("visitedImage", fileName);
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", location);
                using (var streams = new FileStream(path, FileMode.Create))
                {
                    model.ImageUrl.CopyTo(streams);
                }
            }

            //List<ProductSubCatGetViewModel> lstModel = JsonConvert.DeserializeObject<List<ProductSubCatGetViewModel>>(model.lstModel);

            int ChemScheduleID = await _chemistScheduleService.updatePlanChemist(jti, model.PlanID, location, model.VisitTime, model.Latitude, model.Longitude, model.Remarks, model.LLAddress, model.InvoiceAmount, model.CollectionAmount, model.paymentModeId,model.ExecutionType,model.territoryCode);

            if (model.ExecutionType == 2)
            {
                int result = await _chemistScheduleService.setChemExecutionDetails(jti, ChemScheduleID, model.lstChemExecutionDetailsModel, model.territoryCode);
                if (result == 0)
                {
                    var jwt = await Tokens.updateChemistplanFailJwt(new JsonSerializerSettings { Formatting = Formatting.Indented });
                    return new OkObjectResult(jwt);

                }
                else
                {
                    var jwt = await Tokens.updateChemistplanSuccessJwt(new JsonSerializerSettings { Formatting = Formatting.Indented });
                    return new OkObjectResult(jwt);
                }
            }
            else
            {
                if (ChemScheduleID == 0)
                {
                    var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Chemist Schedule has not updated", false);
                    return new OkObjectResult(jwt);
                }
                else
                {
                    var jwt = await Tokens.updateChemistplanSuccessJwt(new JsonSerializerSettings { Formatting = Formatting.Indented });
                    return new OkObjectResult(jwt);
                }
            }
           
        }

        [HttpPost("createOrderByChemist")]
        public async Task<IActionResult> createOrderByChemist([FromBody] ChemistSalesOrderCreateViewModel model)
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
            var employee = await employeeService.GetEmployeeLoadViewModels();
            employee = employee.Where(x => x.employeeNo == user.UserName).ToList();
            if (employee?.FirstOrDefault()?.Token != uid && employee.Count() != 0)
            {
                bool status = false;
                string actionresult = "Invalid Token.";
                var jwts = await Tokens.changePasswordJwt(status, actionresult, new JsonSerializerSettings { Formatting = Formatting.Indented });
                return new OkObjectResult(jwts);
            }
            if (model.chemistId == null || model.chemistId == 0)
            {
                var jwt = await Tokens.salesOrderFailJwt(new JsonSerializerSettings { Formatting = Formatting.Indented });
                return new OkObjectResult(jwt);
            }

            //List<ProductSubCatGetViewModel> lstModel = JsonConvert.DeserializeObject<List<ProductSubCatGetViewModel>>(model.lstModel);
            //int result = await _chemistScheduleService.CreateSalesOrderByChemist(jti, model.visitDate, model.chemistId, lstModel);

            int result = 0;
            int salesInvoiceId = await _chemistScheduleService.SaveSalesOrderMasterByChemist(user.employeeId.ToString(), model);

            if (salesInvoiceId == 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Sales order has not created.", false);
                return new OkObjectResult(jwt);
            }

            result = await _chemistScheduleService.SalesOrderDetailsByChemist(user.employeeId.ToString(), model.OrderDetails, salesInvoiceId);

            if (result != 0)
            {
                var jwt = await Tokens.commonMesageForAll(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Sales order has created successfully.", true);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.commonMesageForAll(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Sales order has not created.", false);
                return new OkObjectResult(jwt);
            }
        }

        [HttpPost("updatePlanChemiststartTime")]
        public async Task<IActionResult> updatePlanChemistwithstartTime([FromBody] ChemistScheduleStartUpdateParamViewModel model)
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
            var employee = await employeeService.GetEmployeeLoadViewModels();
            employee = employee.Where(x => x.employeeNo == user.UserName).ToList();
            if (employee?.FirstOrDefault()?.Token != uid && employee.Count() != 0)
            {
                bool status = false;
                string actionresult = "Invalid Token.";
                var jwts = await Tokens.changePasswordJwt(status, actionresult, new JsonSerializerSettings { Formatting = Formatting.Indented });
                return new OkObjectResult(jwts);
            }
            if (model.PlanID == null || model.PlanID == 0)
            {
                var jwt = await Tokens.updateChemistplanFailJwt(new JsonSerializerSettings { Formatting = Formatting.Indented });
                return new OkObjectResult(jwt);
            }

            bool result = await _chemistScheduleService.updatePlanChemiststartTime(jti, model.PlanID, model.startTime, model.Latitude, model.Longitude);

            if (result == true)
            {
                var jwt = await Tokens.updateChemistplanSuccessJwt(new JsonSerializerSettings { Formatting = Formatting.Indented });
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.updateChemistplanFailJwt(new JsonSerializerSettings { Formatting = Formatting.Indented });
                return new OkObjectResult(jwt);
            }
        }

        [HttpPost("deletePlanChemistData")]
        public async Task<IActionResult> deletePlanChemistData([FromBody] ChemistScheduleParamDataViewModel model)
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
            var employee = await employeeService.GetEmployeeLoadViewModels();
            employee = employee.Where(x => x.employeeNo == user.UserName).ToList();
            if (employee?.FirstOrDefault()?.Token != uid && employee.Count() != 0)
            {
                bool status = false;
                string actionresult = "Invalid Token.";
                var jwts = await Tokens.changePasswordJwt(status, actionresult, new JsonSerializerSettings { Formatting = Formatting.Indented });
                return new OkObjectResult(jwts);
            }
            if (model.PlanID == null || model.PlanID == 0)
            {
                var jwt = await Tokens.deleteChemistplanFailJwt(new JsonSerializerSettings { Formatting = Formatting.Indented });
                return new OkObjectResult(jwt);
            }
            //  List<VisitTaskParamViewModel> modellvisit = JsonConvert.DeserializeObject<List<VisitTaskParamViewModel>>(model.lstModelTask);
            bool result = await _chemistScheduleService.deletePlanChemist(jti, model.PlanID);

            if (result == true)
            {
                var jwt = await Tokens.deleteChemistplanSuccessJwt(new JsonSerializerSettings { Formatting = Formatting.Indented });
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.deleteChemistplanFailJwt(new JsonSerializerSettings { Formatting = Formatting.Indented });
                return new OkObjectResult(jwt);
            }
        }

        [HttpPost("PlanSetExecuteChemist")]
        public async Task<IActionResult> PlanSetExecuteChemist([FromForm] ChemistScheduleExecutionParamViewModel model)
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
            // var jsonToken = handler.ReadToken(stream);

            var tokenS = handler.ReadToken(stream) as JwtSecurityToken;
            var jti = tokenS.Claims.First(claim => claim.Type == "Id").Value;
            var user = await userInfoes.GetUserBasicInfoesbyId(jti);
            var employee = await employeeService.GetEmployeeLoadViewModels();
            employee = employee.Where(x => x.employeeNo == user.UserName).ToList();
            if (employee?.FirstOrDefault()?.Token != uid && employee.Count() != 0)
            {
                bool status = false;
                string actionresult = "Invalid Token.";
                var jwts = await Tokens.changePasswordJwt(status, actionresult, new JsonSerializerSettings { Formatting = Formatting.Indented });

                return new OkObjectResult(jwts);

            }
            if (model.ChemistID == null || model.ChemistID == 0 || model.MarketScheduleID == null || model.MarketScheduleID == 0 || model.RosterID == null || model.RosterID == 0)
            {
                var jwt = await Tokens.updateChemistplanFailJwt(new JsonSerializerSettings { Formatting = Formatting.Indented });

                return new OkObjectResult(jwt);
            }
            string location = "";
            string fileName = "";
            if (model.ImageUrl != null)
            {
                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".webp" };
                string message = "success";

                var extention = Path.GetExtension(model.ImageUrl.FileName);
                if (model.ImageUrl.Length > 2000000)
                    message = "Select jpg or jpeg or png less than 2Μ";
                else if (!allowedExtensions.Contains(extention.ToLower()))
                    message = "Must be jpeg or png";

                fileName = DateTime.Now.Ticks + extention;
                location = Path.Combine("visitedImage", fileName);
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", location);
                using (var streams = new FileStream(path, FileMode.Create))
                {
                    model.ImageUrl.CopyTo(streams);
                }

            }
            bool result = await _chemistScheduleService.PlanExecutionChemist(jti, model.RosterID, model.ChemistID, model.MarketScheduleID, location, model.visitDate, model.VisitTime, model.Latitude, model.Longitude, model.Remarks, model.LLAddress, model.InvoiceAmount, model.CollectionAmount);
            //DateTime ValidTo = jsonToken.ValidTo;

            if (result == true)
            {
                var jwt = await Tokens.updateChemistplanSuccessJwt(new JsonSerializerSettings { Formatting = Formatting.Indented });

                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.updateChemistplanFailJwt(new JsonSerializerSettings { Formatting = Formatting.Indented });

                return new OkObjectResult(jwt);
            }
        }

        [HttpGet("getChListAfterSetPlan")]
        public async Task<IActionResult> getChListAfterSetPlan(string visitDate,string employeeNo)
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
            var employee = await employeeService.GetEmployeeLoadViewModels();
            employee = employee.Where(x => x.employeeNo == user.UserName).ToList();

            if (employee?.FirstOrDefault()?.Token != uid && employee.Count() != 0)
            {
                bool status = false;
                string actionresult = "Invalid Token.";
                var jwts = await Tokens.changePasswordJwt(status, actionresult, new JsonSerializerSettings { Formatting = Formatting.Indented });
                return new OkObjectResult(jwts);
            }
            if (visitDate == null || visitDate == "")
            {
                var jwt = await Tokens.getChemistSchedulelistaftersetfail(new JsonSerializerSettings { Formatting = Formatting.Indented });
                return new OkObjectResult(jwt);
            }
            //var result = await _chemistScheduleService.getChListAfterSetPlan(jti, visitDate, 0);
            //if (result.Count() > 0)
            //{
            //    var jwt = await Tokens.getChemistSchedulelistaftersetsuccess(result.ToList(), new JsonSerializerSettings { Formatting = Formatting.Indented });
            //    return new OkObjectResult(jwt);
            //}
            //else
            //{
            //    var jwt = await Tokens.getChemistSchedulelistaftersetfail(new JsonSerializerSettings { Formatting = Formatting.Indented });
            //    return new OkObjectResult(jwt);
            //}

            var datajson = await _chemistScheduleService.getChListAfterSetPlan(jti, visitDate, 0,employeeNo);
            if (datajson.data != "[]")
            {
                var jwt = await Tokens.getDataWithStatusAndMessage(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented }, "Chemist's schedules are found successfully.");
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.getDataWithStatusAndMessage(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented }, "No schedule for Chemist's visit is found.");
                return new OkObjectResult(jwt);
            }
        }

        [HttpPost("deleteChemist")]
        public async Task<IActionResult> deleteChemist([FromBody] ChemistListViewModel model)
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

            if (model.chemistID == 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Chemist has not deleted.", false);
                return new OkObjectResult(jwt);
            }

            bool result = await _chemistService.DeleteChemist(model.chemistID);

            if (result == true)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Chemist has deleted successfully.", true);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Chemist has not deleted.", false);
                return new OkObjectResult(jwt);
            }
        }

        #endregion


        #region Notice/Plan Upload

        [HttpPost("Noticeuploadwithimage")]
        public async Task<IActionResult> Noticeuploadwithimage([FromForm] NoticeUploadViewModel model)
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
            // var jsonToken = handler.ReadToken(stream);

            var tokenS = handler.ReadToken(stream) as JwtSecurityToken;
            var jti = tokenS.Claims.First(claim => claim.Type == "Id").Value;
            var user = await userInfoes.GetUserBasicInfoesbyId(jti);
            var employee = await employeeService.GetEmployeeLoadViewModels();
            employee = employee.Where(x => x.employeeNo == user.UserName).ToList();
            if (employee?.FirstOrDefault()?.Token != uid && employee.Count() != 0)
            {
                bool status = false;
                string actionresult = "Invalid Token.";
                var jwts = await Tokens.changePasswordJwt(status, actionresult, new JsonSerializerSettings { Formatting = Formatting.Indented });

                return new OkObjectResult(jwts);

            }
            int result = 0;

            if (model.ImageUrls != null)
            {
                for (int i = 0; i < model.ImageUrls.Count(); i++)
                {
                    string location = "";
                    string fileName = "";
                    if (model.ImageUrls[i] != null)
                    {
                        var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".webp" };
                        string message = "success";

                        var extention = Path.GetExtension(model.ImageUrls[i].FileName);
                        if (model.ImageUrls[i].Length > 2000000)
                            message = "Select jpg or jpeg or png less than 2Μ";
                        else if (!allowedExtensions.Contains(extention.ToLower()))
                            message = "Must be jpeg or png";

                        fileName = DateTime.Now.Ticks + extention;
                        if (model.status == 1)
                        {
                            location = Path.Combine("ActionPlan", fileName);
                        }
                        else
                        {
                            location = Path.Combine("ActionCampain", fileName);
                        }
                        var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", location);
                        using (var streams = new FileStream(path, FileMode.Create))
                        {
                            model.ImageUrls[i].CopyTo(streams);
                        }

                    }
                    result = await _chemistScheduleService.setNoticeUploadImage((int)user.employeeId, model.UploadMasterID, (int)model.status, model.startDate, model.endDate, location);
                }
            }


            //DateTime ValidTo = jsonToken.ValidTo;

            if (result > 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Plan/Notice uploaded successfully.", true);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Plan/Notice has not  uploaded successfully", false);
                return new OkObjectResult(jwt);
            }
        }

        #endregion



        #region Rx Upload

        [HttpPost("Rxuploadwithimageandproduct")]
        public async Task<IActionResult> Rxuploadwithimageandproduct([FromForm] RxuploadViewModel model)
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
            // var jsonToken = handler.ReadToken(stream);

            var tokenS = handler.ReadToken(stream) as JwtSecurityToken;
            var jti = tokenS.Claims.First(claim => claim.Type == "Id").Value;
            var user = await userInfoes.GetUserBasicInfoesbyId(jti);
            var employee = await employeeService.GetEmployeeLoadViewModels();
            employee = employee.Where(x => x.employeeNo == user.UserName).ToList();
            if (employee?.FirstOrDefault()?.Token != uid && employee.Count() != 0)
            {
                bool status = false;
                string actionresult = "Invalid Token.";
                var jwts = await Tokens.changePasswordJwt(status, actionresult, new JsonSerializerSettings { Formatting = Formatting.Indented });

                return new OkObjectResult(jwts);

            }
            if (model.doctorId == null || model.doctorId == 0)
            {
                var jwt = await Tokens.updateRxUploadplanFailJwt(new JsonSerializerSettings { Formatting = Formatting.Indented });
                //var jwt = await Tokens.updateChemistplanFailJwt(new JsonSerializerSettings { Formatting = Formatting.Indented });

                return new OkObjectResult(jwt);
            }

            int masterId = await _chemistScheduleService.setRxUploadMaster(jti, model.rxUploadMasterID, (int)model.doctorId, DateTime.Now);

            if (model.ImageUrls != null)
            {
                for (int i = 0; i < model.ImageUrls.Count(); i++)
                {
                    string location = "";
                    string fileName = "";
                    if (model.ImageUrls[i] != null)
                    {
                        var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".webp" };
                        string message = "success";

                        var extention = Path.GetExtension(model.ImageUrls[i].FileName);
                        if (model.ImageUrls[i].Length > 2000000)
                            message = "Select jpg or jpeg or png less than 2Μ";
                        else if (!allowedExtensions.Contains(extention.ToLower()))
                            message = "Must be jpeg or png";

                        fileName = DateTime.Now.Ticks + extention;
                        location = Path.Combine("RxUploadImage", fileName);
                        var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", location);
                        using (var streams = new FileStream(path, FileMode.Create))
                        {
                            model.ImageUrls[i].CopyTo(streams);
                        }

                    }
                    int result = await _chemistScheduleService.setRxUploadImage(jti, masterId, location);
                }
            }

            if (model.InvProductWiseSpecificationIds != null)
            {
                for (int i = 0; i < model.InvProductWiseSpecificationIds.Count(); i++)
                {
                    int result = await _chemistScheduleService.setRxUploadProduct(jti, masterId, (int)model.InvProductWiseSpecificationIds[i]);
                }
            }


            //DateTime ValidTo = jsonToken.ValidTo;

            if (masterId > 0)
            {
                var jwt = await Tokens.updateRxUploadplanSuccessJwt(new JsonSerializerSettings { Formatting = Formatting.Indented });

                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.updateRxUploadplanFailJwt(new JsonSerializerSettings { Formatting = Formatting.Indented });

                return new OkObjectResult(jwt);
            }
        }

        #endregion

        #region Report

        [HttpGet("GetMarketScheduleData")]
        public async Task<IActionResult> GetMarketScheduleData(DateTime Date, int RosterId)
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
            // var jsonToken = handler.ReadToken(stream);

            var tokenS = handler.ReadToken(stream) as JwtSecurityToken;
            var jti = tokenS.Claims.First(claim => claim.Type == "Id").Value;
            var user = await userInfoes.GetUserBasicInfoesbyId(jti);
            var employee = await employeeService.GetEmployeeLoadViewModels();
            employee = employee.Where(x => x.employeeNo == user.UserName).ToList();

            if (employee?.FirstOrDefault()?.Token != uid && employee.Count() != 0)
            {
                bool status = false;
                string actionresult = "Invalid Token.";
                var jwts = await Tokens.changePasswordJwt(status, actionresult, new JsonSerializerSettings { Formatting = Formatting.Indented });

                return new OkObjectResult(jwts);

            }
            var datajson = await _doctorScheduleService.GetMarketScheduleJsonViewModels(jti, Convert.ToDateTime(Date).ToString("yyyyMMdd"), RosterId);
            string EMP_ID = "";
            string EMPLOYEE_NAME = "";
            if (employee.Count() == 0)
            {
                EMP_ID = user.UserName;
                EMPLOYEE_NAME = user.UserName;
            }
            else
            {
                EMP_ID = employee.FirstOrDefault()?.employeeNo;
                EMPLOYEE_NAME = employee.FirstOrDefault()?.fullName;
            }
            var jwt = await Tokens.getMarketPlanData(EMP_ID, EMPLOYEE_NAME, datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });

            return new OkObjectResult(jwt);
            //var depot = await userInfoes.TeritoryListViewModels();
            //return Json(depot.Where(x => x.AreaCode == code).ToList());
        }

        [HttpGet("GetEmployeeDynamicData")]
        public async Task<IActionResult> GetEmployeeDynamicData(string Code, string Type)
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
            // var jsonToken = handler.ReadToken(stream);

            var tokenS = handler.ReadToken(stream) as JwtSecurityToken;
            var jti = tokenS.Claims.First(claim => claim.Type == "Id").Value;
            var user = await userInfoes.GetUserBasicInfoesbyId(jti);
            var employee = await employeeService.GetEmployeeLoadViewModels();
            employee = employee.Where(x => x.employeeNo == user.UserName).ToList();

            if (employee?.FirstOrDefault()?.Token != uid && employee.Count() != 0)
            {
                bool status = false;
                string actionresult = "Invalid Token.";
                var jwts = await Tokens.changePasswordJwt(status, actionresult, new JsonSerializerSettings { Formatting = Formatting.Indented });

                return new OkObjectResult(jwts);

            }
            var datajson = await _doctorScheduleService.GetEmployeeDynamicJsonViewModels(Code, Type, employee?.FirstOrDefault()?.employeeNo);

            string EMP_ID = "";
            string EMPLOYEE_NAME = "";
            if (employee.Count() == 0)
            {
                EMP_ID = user.UserName;
                EMPLOYEE_NAME = user.UserName;
            }
            else
            {
                EMP_ID = employee.FirstOrDefault()?.employeeNo;
                EMPLOYEE_NAME = employee.FirstOrDefault()?.fullName;
            }
            var jwt = await Tokens.getEmployeeDynamicData(EMP_ID, EMPLOYEE_NAME, datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });

            return new OkObjectResult(jwt);
            //var depot = await userInfoes.TeritoryListViewModels();
            //return Json(depot.Where(x => x.AreaCode == code).ToList());
        }


        [HttpGet("GetEmployeeReportDynamicData")]
        public async Task<IActionResult> GetEmployeeReportDynamicData(string Code, string CodeType, string Type)
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
            // var jsonToken = handler.ReadToken(stream);

            var tokenS = handler.ReadToken(stream) as JwtSecurityToken;
            var jti = tokenS.Claims.First(claim => claim.Type == "Id").Value;
            var user = await userInfoes.GetUserBasicInfoesbyId(jti);
            var employee = await employeeService.GetEmployeeLoadViewModels();
            employee = employee.Where(x => x.employeeNo == user.UserName).ToList();

            if (employee?.FirstOrDefault()?.Token != uid && employee.Count() != 0)
            {
                bool status = false;
                string actionresult = "Invalid Token.";
                var jwts = await Tokens.changePasswordJwt(status, actionresult, new JsonSerializerSettings { Formatting = Formatting.Indented });

                return new OkObjectResult(jwts);

            }
            var datajson = await _doctorScheduleService.GetEmployeeReportDynamicJsonViewModels(Code, CodeType, Type, employee?.FirstOrDefault()?.employeeNo);
            string EMP_ID = "";
            string EMPLOYEE_NAME = "";
            if (employee.Count() == 0)
            {
                EMP_ID = user.UserName;
                EMPLOYEE_NAME = user.UserName;
            }
            else
            {
                EMP_ID = employee.FirstOrDefault()?.employeeNo;
                EMPLOYEE_NAME = employee.FirstOrDefault()?.fullName;
            }
            var jwt = await Tokens.getEmployeeDynamicData(EMP_ID, EMPLOYEE_NAME, datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });

            return new OkObjectResult(jwt);
            //var depot = await userInfoes.TeritoryListViewModels();
            //return Json(depot.Where(x => x.AreaCode == code).ToList());
        }

        [HttpGet("GetDoctorsDynamicData")]
        public async Task<IActionResult> GetDoctorsDynamicData(string Code, string Type)
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
            // var jsonToken = handler.ReadToken(stream);

            var tokenS = handler.ReadToken(stream) as JwtSecurityToken;
            var jti = tokenS.Claims.First(claim => claim.Type == "Id").Value;
            var user = await userInfoes.GetUserBasicInfoesbyId(jti);
            var employee = await employeeService.GetEmployeeLoadViewModels();
            employee = employee.Where(x => x.employeeNo == user.UserName).ToList();

            if (employee?.FirstOrDefault()?.Token != uid && employee.Count() != 0)
            {
                bool status = false;
                string actionresult = "Invalid Token.";
                var jwts = await Tokens.changePasswordJwt(status, actionresult, new JsonSerializerSettings { Formatting = Formatting.Indented });

                return new OkObjectResult(jwts);

            }
            var datajson = await _doctorScheduleService.GetDoctorsDynamicJsonViewModels(Code, Type);
            string EMP_ID = "";
            string EMPLOYEE_NAME = "";
            if (employee.Count() == 0)
            {
                EMP_ID = user.UserName;
                EMPLOYEE_NAME = user.UserName;
            }
            else
            {
                EMP_ID = employee.FirstOrDefault()?.employeeNo;
                EMPLOYEE_NAME = employee.FirstOrDefault()?.fullName;
            }
            var jwt = await Tokens.getDoctorDynamicData(EMP_ID, EMPLOYEE_NAME, datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });

            return new OkObjectResult(jwt);
            //var depot = await userInfoes.TeritoryListViewModels();
            //return Json(depot.Where(x => x.AreaCode == code).ToList());
        }

        [HttpGet("GetChemistsDynamicData")]
        public async Task<IActionResult> GetChemistsDynamicData(string Code, string Type)
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
            // var jsonToken = handler.ReadToken(stream);

            var tokenS = handler.ReadToken(stream) as JwtSecurityToken;
            var jti = tokenS.Claims.First(claim => claim.Type == "Id").Value;
            var user = await userInfoes.GetUserBasicInfoesbyId(jti);
            var employee = await employeeService.GetEmployeeLoadViewModels();
            employee = employee.Where(x => x.employeeNo == user.UserName).ToList();

            if (employee?.FirstOrDefault()?.Token != uid && employee.Count() != 0)
            {
                bool status = false;
                string actionresult = "Invalid Token.";
                var jwts = await Tokens.changePasswordJwt(status, actionresult, new JsonSerializerSettings { Formatting = Formatting.Indented });

                return new OkObjectResult(jwts);

            }
            var datajson = await _doctorScheduleService.GetChemistsDynamicJsonViewModels(Code, Type);
            string EMP_ID = "";
            string EMPLOYEE_NAME = "";
            if (employee.Count() == 0)
            {
                EMP_ID = user.UserName;
                EMPLOYEE_NAME = user.UserName;
            }
            else
            {
                EMP_ID = employee.FirstOrDefault()?.employeeNo;
                EMPLOYEE_NAME = employee.FirstOrDefault()?.fullName;
            }
            var jwt = await Tokens.getChemistDynamicData(EMP_ID, EMPLOYEE_NAME, datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });

            return new OkObjectResult(jwt);
            //var depot = await userInfoes.TeritoryListViewModels();
            //return Json(depot.Where(x => x.AreaCode == code).ToList());
        }


        [HttpGet("GetMIODoctorVisitReport")]

        public async Task<IActionResult> GetMIODoctorVisitReport(string ZoneCode, string DepotCode, string RegionCode, string AreaCode, string TerritoryCode, string EmpCode, DateTime FromDate, DateTime ToDate)
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
            var employee = await employeeService.GetEmployeeLoadViewModels();
            employee = employee.Where(x => x.employeeNo == user.UserName).ToList();
            if (employee?.FirstOrDefault()?.Token != uid && employee.Count() != 0)
            {
                bool status = false;
                string actionresult = "Invalid Token.";
                var jwts = await Tokens.changePasswordJwt(status, actionresult, new JsonSerializerSettings { Formatting = Formatting.Indented });
                return new OkObjectResult(jwts);
            }
            IEnumerable<VisitReportDoctorViewModel> lstdata = await _chemistScheduleService.VisitReportDoctorViewModels(ZoneCode, DepotCode, RegionCode, AreaCode, TerritoryCode, EmpCode, Convert.ToDateTime(FromDate).ToString("yyyyMMdd"), Convert.ToDateTime(ToDate).ToString("yyyyMMdd"));
            var jwt = await Tokens.getDoctorVisitReport(lstdata.ToList(), new JsonSerializerSettings { Formatting = Formatting.Indented });

            return new OkObjectResult(jwt);
        }


        [HttpGet("GetEmpVisitReport")]

        public async Task<IActionResult> GetEmpVisitReport(string ZoneCode, string RegionCode, string AreaCode, string TerritoryCode, string EmpCode, DateTime FromDate, DateTime ToDate)
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
            var employee = await employeeService.GetEmployeeLoadViewModels();
            employee = employee.Where(x => x.employeeNo == user.UserName).ToList();
            if (employee?.FirstOrDefault()?.Token != uid && employee.Count() != 0)
            {
                bool status = false;
                string actionresult = "Invalid Token.";
                var jwts = await Tokens.changePasswordJwt(status, actionresult, new JsonSerializerSettings { Formatting = Formatting.Indented });
                return new OkObjectResult(jwts);
            }
            IEnumerable<VisitReportEmployeeViewModel> lstdata = await _chemistScheduleService.VisitReportEmployeeViewModels(ZoneCode, "", RegionCode, AreaCode, TerritoryCode, EmpCode, Convert.ToDateTime(FromDate).ToString("yyyyMMdd"), Convert.ToDateTime(ToDate).ToString("yyyyMMdd"));
            var jwt = await Tokens.getEmpVisitReport(lstdata.ToList(), new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpGet("GetMIOChemistVisitReport")]
        public async Task<IActionResult> GetMIOChemistVisitReport(string ZoneCode, string RegionCode, string AreaCode, string TerritoryCode, string EmpCode, DateTime FromDate, DateTime ToDate)
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
            var employee = await employeeService.GetEmployeeLoadViewModels();
            employee = employee.Where(x => x.employeeNo == user.UserName).ToList();
            if (employee?.FirstOrDefault()?.Token != uid && employee.Count() != 0)
            {
                bool status = false;
                string actionresult = "Invalid Token.";
                var jwts = await Tokens.changePasswordJwt(status, actionresult, new JsonSerializerSettings { Formatting = Formatting.Indented });
                return new OkObjectResult(jwts);
            }
            IEnumerable<VisitReportChemistViewModel> lstdata = await _chemistScheduleService.VisitReportChemistViewModels(ZoneCode, "", RegionCode, AreaCode, TerritoryCode, EmpCode, Convert.ToDateTime(FromDate).ToString("yyyyMMdd"), Convert.ToDateTime(ToDate).ToString("yyyyMMdd"));
            var jwt = await Tokens.getChemistVisitReport(lstdata.ToList(), new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        /// For APPS
        [HttpGet("GetMIOWiseTrackingReport")]
        public async Task<IActionResult> GetMIOWiseTrackingReport(string ZoneCode, string RegionCode, string AreaCode, string TerritoryCode, string EmpCode, DateTime Date)
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
            var employee = await employeeService.GetEmployeeLoadViewModels();
            employee = employee.Where(x => x.employeeNo == user.UserName).ToList();
            if (employee?.FirstOrDefault()?.Token != uid && employee.Count() != 0)
            {
                bool status = false;
                string actionresult = "Invalid Token.";
                var jwts = await Tokens.changePasswordJwt(status, actionresult, new JsonSerializerSettings { Formatting = Formatting.Indented });
                return new OkObjectResult(jwts);
            }
            //IEnumerable<MIOCurrentLocationViewModel> lstdata = await userInfoes.MIOCurrentLocationViewModelsByMIO(ZoneCode, "", RegionCode, AreaCode, TerritoryCode, EmpCode, Convert.ToDateTime(Date).ToString("yyyyMMdd"));
            IEnumerable<MIOCurrentLocationViewModel> lstdata = await userInfoes.MIOCurrentLocationViewModelsByMIOForApps(ZoneCode, "", RegionCode, AreaCode, TerritoryCode, EmpCode, Convert.ToDateTime(Date).ToString("yyyyMMdd"));
            var jwt = await Tokens.getMIOWiseTrackingReport(lstdata.ToList(), new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpGet("GetMIOWiseCurrentTrackingReport")]
        public async Task<IActionResult> GetMIOWiseCurrentTrackingReport(string ZoneCode, string RegionCode, string AreaCode, string TerritoryCode, string EmpCode, DateTime Date)
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
            var employee = await employeeService.GetEmployeeLoadViewModels();
            employee = employee.Where(x => x.employeeNo == user.UserName).ToList();
            if (employee?.FirstOrDefault()?.Token != uid && employee.Count() != 0)
            {
                bool status = false;
                string actionresult = "Invalid Token.";
                var jwts = await Tokens.changePasswordJwt(status, actionresult, new JsonSerializerSettings { Formatting = Formatting.Indented });
                return new OkObjectResult(jwts);
            }
            IEnumerable<MIOCurrentLocationViewModel> lstdata = await userInfoes.MIOCurrentLocationViewModels(ZoneCode, "", RegionCode, AreaCode, TerritoryCode, EmpCode);
            var jwt = await Tokens.getMIOWiseTrackingReport(lstdata.ToList(), new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpGet("GetChemistWiseVisitReport")]
        public async Task<IActionResult> GetChemistWiseVisitReport(string ZoneCode, string RegionCode, string AreaCode, string TerritoryCode, int Id, DateTime FromDate, DateTime ToDate)
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
            var employee = await employeeService.GetEmployeeLoadViewModels();
            employee = employee.Where(x => x.employeeNo == user.UserName).ToList();
            if (employee?.FirstOrDefault()?.Token != uid && employee.Count() != 0)
            {
                bool status = false;
                string actionresult = "Invalid Token.";
                var jwts = await Tokens.changePasswordJwt(status, actionresult, new JsonSerializerSettings { Formatting = Formatting.Indented });
                return new OkObjectResult(jwts);
            }
            IEnumerable<ChemistWiseVisitReportViewModel> lstdata = await _chemistScheduleService.ChemistWiseVisitReportViewModels(ZoneCode, "", RegionCode, AreaCode, TerritoryCode, "", Id, Convert.ToDateTime(FromDate).ToString("yyyyMMdd"), Convert.ToDateTime(ToDate).ToString("yyyyMMdd"));
            var jwt = await Tokens.getChemistWiseVisitReport(lstdata.ToList(), new JsonSerializerSettings { Formatting = Formatting.Indented });

            return new OkObjectResult(jwt);
        }

        [HttpGet("GetDoctorWiseVisitReport")]

        public async Task<IActionResult> GetDoctorWiseVisitReport(string ZoneCode, string RegionCode, string AreaCode, string TerritoryCode, int Id, DateTime FromDate, DateTime ToDate)
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
            var employee = await employeeService.GetEmployeeLoadViewModels();
            employee = employee.Where(x => x.employeeNo == user.UserName).ToList();
            if (employee?.FirstOrDefault()?.Token != uid && employee.Count() != 0)
            {
                bool status = false;
                string actionresult = "Invalid Token.";
                var jwts = await Tokens.changePasswordJwt(status, actionresult, new JsonSerializerSettings { Formatting = Formatting.Indented });
                return new OkObjectResult(jwts);
            }
            IEnumerable<DoctorWiseVisitReportViewModel> lstdata = await _chemistScheduleService.DoctorWiseVisitReportViewModels(ZoneCode, "", RegionCode, AreaCode, TerritoryCode, "", Id, Convert.ToDateTime(FromDate).ToString("yyyyMMdd"), Convert.ToDateTime(ToDate).ToString("yyyyMMdd"));
            var jwt = await Tokens.getDoctorWiseVisitReport(lstdata.ToList(), new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }


        [HttpGet("GetEmployeeforAllEmployeeCT")]
        public async Task<IActionResult> GetEmployeeforAllEmployeeCT(string code, string Type)
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
            var employee = await employeeService.GetEmployeeLoadViewModels();
            employee = employee.Where(x => x.employeeNo == user.UserName).ToList();
            if (employee?.FirstOrDefault()?.Token != uid && employee.Count() != 0)
            {
                bool status = false;
                string actionresult = "Invalid Token.";
                var jwts = await Tokens.changePasswordJwt(status, actionresult, new JsonSerializerSettings { Formatting = Formatting.Indented });
                return new OkObjectResult(jwts);
            }

            var depot = await employeeService.GetEmployeeLoadViewModels();
            var fdata = depot;
            if (Type == "Z")
            {
                fdata = depot.Where(x => x.ZONE_CODE == code).ToList();
            }
            else if (Type == "D")
            {
                fdata = depot.Where(x => x.DEPOT_CODE == code).ToList();
            }
            else if (Type == "R")
            {
                fdata = depot.Where(x => x.REGION_CODE == code).ToList();
            }
            else if (Type == "A")
            {
                fdata = depot.Where(x => x.AREA_CODE == code).ToList();
            }
            else if (Type == "T")
            {
                fdata = depot.Where(x => x.TERRITORY_CODE == code).ToList();
            }
            else
            {
                fdata = depot.ToList();
            }

            if (employee.FirstOrDefault().POSTING_LOCATION == "Z")
            {
                fdata = fdata.Where(x => x.ZONE_CODE == employee.FirstOrDefault().ZONE_CODE);
            }
            else if (employee.FirstOrDefault().POSTING_LOCATION == "R")
            {
                fdata = fdata.Where(x => x.REGION_CODE == employee.FirstOrDefault().REGION_CODE);

            }
            else if (employee.FirstOrDefault().POSTING_LOCATION == "D")
            {
                fdata = fdata.Where(x => x.DEPOT_CODE == employee.FirstOrDefault().DEPOT_CODE);

            }
            else if (employee.FirstOrDefault().POSTING_LOCATION == "A")
            {
                fdata = fdata.Where(x => x.AREA_CODE == employee.FirstOrDefault().AREA_CODE);

            }
            else if (employee.FirstOrDefault().POSTING_LOCATION == "T")
            {
                fdata = fdata.Where(x => x.TERRITORY_CODE == employee.FirstOrDefault().TERRITORY_CODE);

            }

            IEnumerable<EmployeeViewModel> data = fdata;
            var jwt = await Tokens.getEmployeeData(data.ToList(), new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpGet("GetEmployeeforAllEmployeeByMarketCode")]
        public async Task<IActionResult> GetEmployeeforAllEmployeeByMarketCode(string ZoneCode, string RegionCode, string AreaCode, string TerritoryCode)
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
            var employee = await employeeService.GetEmployeeLoadViewModels();
            employee = employee.Where(x => x.employeeNo == user.UserName).ToList();
            if (employee?.FirstOrDefault()?.Token != uid && employee.Count() != 0)
            {
                bool status = false;
                string actionresult = "Invalid Token.";
                var jwts = await Tokens.changePasswordJwt(status, actionresult, new JsonSerializerSettings { Formatting = Formatting.Indented });
                return new OkObjectResult(jwts);
            }

            var depot = await employeeService.GetEmployeeLoadViewModels();
            var fdata = depot;
            if (ZoneCode != null)
            {
                fdata = depot.Where(x => x.ZONE_CODE == ZoneCode).ToList();
            }
            else if (RegionCode != null)
            {
                fdata = depot.Where(x => x.REGION_CODE == RegionCode).ToList();
            }
            else if (AreaCode != null)
            {
                fdata = depot.Where(x => x.AREA_CODE == AreaCode).ToList();
            }
            else if (TerritoryCode != null)
            {
                fdata = depot.Where(x => x.TERRITORY_CODE == TerritoryCode).ToList();
            }
            else
            {
                fdata = depot.ToList();
            }

            if (employee.FirstOrDefault().POSTING_LOCATION == "Z")
            {
                fdata = fdata.Where(x => x.ZONE_CODE == employee.FirstOrDefault().ZONE_CODE);
            }
            else if (employee.FirstOrDefault().POSTING_LOCATION == "R")
            {
                fdata = fdata.Where(x => x.REGION_CODE == employee.FirstOrDefault().REGION_CODE);

            }
            else if (employee.FirstOrDefault().POSTING_LOCATION == "D")
            {
                fdata = fdata.Where(x => x.DEPOT_CODE == employee.FirstOrDefault().DEPOT_CODE);

            }
            else if (employee.FirstOrDefault().POSTING_LOCATION == "A")
            {
                fdata = fdata.Where(x => x.AREA_CODE == employee.FirstOrDefault().AREA_CODE);

            }
            else if (employee.FirstOrDefault().POSTING_LOCATION == "T")
            {
                fdata = fdata.Where(x => x.TERRITORY_CODE == employee.FirstOrDefault().TERRITORY_CODE);

            }

            IEnumerable<EmployeeViewModel> data = fdata;
            var jwt = await Tokens.getEmployeeData(data.ToList(), new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }


        [HttpGet("GetEmployeeforAllEmployeeCTAll")]
        public async Task<IActionResult> GetEmployeeforAllEmployeeCTAll()
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
            var employee = await employeeService.GetEmployeeLoadViewModels();
            employee = employee.Where(x => x.employeeNo == user.UserName).ToList();
            if (employee?.FirstOrDefault()?.Token != uid && employee.Count() != 0)
            {
                bool status = false;
                string actionresult = "Invalid Token.";
                var jwts = await Tokens.changePasswordJwt(status, actionresult, new JsonSerializerSettings { Formatting = Formatting.Indented });
                return new OkObjectResult(jwts);
            }

            var depot = await employeeService.GetEmployeeLoadViewModels();
            var fdata = depot;


            if (employee.FirstOrDefault().POSTING_LOCATION == "Z")
            {
                fdata = fdata.Where(x => x.ZONE_CODE == employee.FirstOrDefault().ZONE_CODE && x.POSTING_LOCATION == "R");
            }
            else if (employee.FirstOrDefault().POSTING_LOCATION == "")
            {
                fdata = fdata.Where(x => x.REGION_CODE == employee.FirstOrDefault().REGION_CODE && x.POSTING_LOCATION == "Z");
            }
            else if (employee.FirstOrDefault().POSTING_LOCATION == "R")
            {
                fdata = fdata.Where(x => x.REGION_CODE == employee.FirstOrDefault().REGION_CODE && x.POSTING_LOCATION == "A");
            }
            else
            {

            }

            IEnumerable<EmployeeViewModel> data = fdata;
            var jwt = await Tokens.getEmployeeData(data.ToList(), new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        #endregion


        [HttpGet("ProcessPlan")]
        public async Task<IActionResult> ProcessPlan(DateTime fromDate, DateTime toDate)
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
            //  string EMP = HttpContext.User.Identity.Name;
            var sistancedata = await _chemistScheduleService.PlanProcess(Convert.ToDateTime(fromDate).ToString("yyyyMMdd"), Convert.ToDateTime(toDate).ToString("yyyyMMdd"), user.UserName.ToString());

            return new OkObjectResult(1);
        }
        [HttpGet("ProcessPlanDoc")]
        public async Task<IActionResult> ProcessPlanDoc(DateTime fromDate, DateTime toDate)
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
            //  string EMP = HttpContext.User.Identity.Name;
            var sistancedata = await _chemistScheduleService.PlanProcessDoc(Convert.ToDateTime(fromDate).ToString("yyyyMMdd"), Convert.ToDateTime(toDate).ToString("yyyyMMdd"), user.UserName.ToString());

            return new OkObjectResult(1);
        }

        [HttpGet("getTADAByEmployeeCode")]
        public async Task<IActionResult> getTADAByEmployeeCode()
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

            var datajson = await _chemistScheduleService.getTADAByEmployeeCode(jti);

            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpGet("getTADAReportByEmployeeCode")]
        public async Task<IActionResult> getTADAReportByEmployeeCode()
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

            var datajson = await _chemistScheduleService.getTADAReportByEmployeeCode(jti);
            var datajson1 = await _chemistScheduleService.getVehicleBillByEmployeeCode(jti);

            var jwt = await Tokens.getDataDouble(datajson.data, datajson1.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpGet("getCmnWeeklyPlanDocByStatus")]
        public async Task<IActionResult> getCmnWeeklyPlanDocByStatus(string employeeCode)
        {
            var uid = Request.Headers["auth_token"];
            if (uid.Count() == 0)
            {
                bool flag = false;
                string actionresult = "Invalid Token.";
                var jwts = await Tokens.changePasswordJwt(flag, actionresult, new JsonSerializerSettings { Formatting = Formatting.Indented });
                return new OkObjectResult(jwts);
            }
            var stream = uid;
            var handler = new JwtSecurityTokenHandler();
            var tokenS = handler.ReadToken(stream) as JwtSecurityToken;
            var jti = tokenS.Claims.First(claim => claim.Type == "Id").Value;
            var user = await userInfoes.GetUserBasicInfoesbyId(jti);
            if (user.token != uid && user != null)
            {
                bool flag = false;
                string actionresult = "Invalid Token.";
                var jwts = await Tokens.changePasswordJwt(flag, actionresult, new JsonSerializerSettings { Formatting = Formatting.Indented });
                return new OkObjectResult(jwts);
            }

            var datajson = await _chemistScheduleService.getCmnWeeklyPlanDocByStatus(jti, employeeCode);

            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpGet("getCmnGetCmnWeeklyPlanChemistByStatus")]
        public async Task<IActionResult> CmnGetCmnWeeklyPlanChemistByStatus(string employeeCode)
        {
            var uid = Request.Headers["auth_token"];
            if (uid.Count() == 0)
            {
                bool flag = false;
                string actionresult = "Invalid Token.";
                var jwts = await Tokens.changePasswordJwt(flag, actionresult, new JsonSerializerSettings { Formatting = Formatting.Indented });
                return new OkObjectResult(jwts);
            }
            var stream = uid;
            var handler = new JwtSecurityTokenHandler();
            var tokenS = handler.ReadToken(stream) as JwtSecurityToken;
            var jti = tokenS.Claims.First(claim => claim.Type == "Id").Value;
            var user = await userInfoes.GetUserBasicInfoesbyId(jti);
            if (user.token != uid && user != null)
            {
                bool flag = false;
                string actionresult = "Invalid Token.";
                var jwts = await Tokens.changePasswordJwt(flag, actionresult, new JsonSerializerSettings { Formatting = Formatting.Indented });
                return new OkObjectResult(jwts);
            }

            var datajson = await _chemistScheduleService.getCmnGetCmnWeeklyPlanChemistByStatus(jti, employeeCode);

            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpGet("GetSetsalesTargetIdJson")]
        public async Task<IActionResult> GetSetsalesTargetIdJson(int employeeId, int month, int year)
        {
            var uid = Request.Headers["auth_token"];
            if (uid.Count() == 0)
            {
                bool flag = false;
                string actionresult = "Invalid Token.";
                var jwts = await Tokens.changePasswordJwt(flag, actionresult, new JsonSerializerSettings { Formatting = Formatting.Indented });
                return new OkObjectResult(jwts);
            }
            var stream = uid;
            var handler = new JwtSecurityTokenHandler();
            var tokenS = handler.ReadToken(stream) as JwtSecurityToken;
            var jti = tokenS.Claims.First(claim => claim.Type == "Id").Value;
            var user = await userInfoes.GetUserBasicInfoesbyId(jti);
            if (user.token != uid && user != null)
            {
                bool flag = false;
                string actionresult = "Invalid Token.";
                var jwts = await Tokens.changePasswordJwt(flag, actionresult, new JsonSerializerSettings { Formatting = Formatting.Indented });
                return new OkObjectResult(jwts);
            }

            var datajson = await _chemistScheduleService.GetSetsalesTargetIdJson(employeeId, month, year);

            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpGet("GetSetsalesTargetMonthYearJson")]
        public async Task<IActionResult> GetSetsalesTargetMonthYearJson(int month, int year)
        {
            var uid = Request.Headers["auth_token"];
            if (uid.Count() == 0)
            {
                bool flag = false;
                string actionresult = "Invalid Token.";
                var jwts = await Tokens.changePasswordJwt(flag, actionresult, new JsonSerializerSettings { Formatting = Formatting.Indented });
                return new OkObjectResult(jwts);
            }
            var stream = uid;
            var handler = new JwtSecurityTokenHandler();
            var tokenS = handler.ReadToken(stream) as JwtSecurityToken;
            var jti = tokenS.Claims.First(claim => claim.Type == "Id").Value;
            var user = await userInfoes.GetUserBasicInfoesbyId(jti);
            var employee = await userInfoes.GetEmployeeById((int)user.employeeId);
            if (user.token != uid && user != null)
            {
                bool flag = false;
                string actionresult = "Invalid Token.";
                var jwts = await Tokens.changePasswordJwt(flag, actionresult, new JsonSerializerSettings { Formatting = Formatting.Indented });
                return new OkObjectResult(jwts);
            }

            var datajson = await _chemistScheduleService.GetSetsalesTargetIdReportJson(employee.employeeId, month, year);

            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpGet("getCmnDoctorUnderObserbationByStatus")]
        public async Task<IActionResult> CmnDoctorUnderObserbationByStatus(string employeeCode, string RegionCode, string AreaCode)
        {
            var uid = Request.Headers["auth_token"];
            if (uid.Count() == 0)
            {
                bool flag = false;
                string actionresult = "Invalid Token.";
                var jwts = await Tokens.changePasswordJwt(flag, actionresult, new JsonSerializerSettings { Formatting = Formatting.Indented });
                return new OkObjectResult(jwts);
            }
            var stream = uid;
            var handler = new JwtSecurityTokenHandler();
            var tokenS = handler.ReadToken(stream) as JwtSecurityToken;
            var jti = tokenS.Claims.First(claim => claim.Type == "Id").Value;
            var user = await userInfoes.GetUserBasicInfoesbyId(jti);
            if (user.token != uid && user != null)
            {
                bool flag = false;
                string actionresult = "Invalid Token.";
                var jwts = await Tokens.changePasswordJwt(flag, actionresult, new JsonSerializerSettings { Formatting = Formatting.Indented });
                return new OkObjectResult(jwts);
            }

            var datajson = await _chemistScheduleService.getCmnDoctorUnderObserbationByStatus(jti, employeeCode,RegionCode,AreaCode);

            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpGet("getCmnweeklyplanterritoryByempCode")]
        public async Task<IActionResult> getCmnweeklyplanterritoryByempCode(string employeeCode)
        {
            var uid = Request.Headers["auth_token"];
            if (uid.Count() == 0)
            {
                bool flag = false;
                string actionresult = "Invalid Token.";
                var jwts = await Tokens.changePasswordJwt(flag, actionresult, new JsonSerializerSettings { Formatting = Formatting.Indented });
                return new OkObjectResult(jwts);
            }
            var stream = uid;
            var handler = new JwtSecurityTokenHandler();
            var tokenS = handler.ReadToken(stream) as JwtSecurityToken;
            var jti = tokenS.Claims.First(claim => claim.Type == "Id").Value;
            var user = await userInfoes.GetUserBasicInfoesbyId(jti);
            if (user.token != uid && user != null)
            {
                bool flag = false;
                string actionresult = "Invalid Token.";
                var jwts = await Tokens.changePasswordJwt(flag, actionresult, new JsonSerializerSettings { Formatting = Formatting.Indented });
                return new OkObjectResult(jwts);
            }

            var datajson = await _chemistScheduleService.getCmnweeklyplanterritoryByempCode(jti, employeeCode);

            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpGet("CmnweeklyplanterritoryApprovedToday")]
        public async Task<IActionResult> CmnweeklyplanterritoryApprovedToday()
        {
            var uid = Request.Headers["auth_token"];
            if (uid.Count() == 0)
            {
                bool flag = false;
                string actionresult = "Invalid Token.";
                var jwts = await Tokens.changePasswordJwt(flag, actionresult, new JsonSerializerSettings { Formatting = Formatting.Indented });
                return new OkObjectResult(jwts);
            }
            var stream = uid;
            var handler = new JwtSecurityTokenHandler();
            var tokenS = handler.ReadToken(stream) as JwtSecurityToken;
            var jti = tokenS.Claims.First(claim => claim.Type == "Id").Value;
            var user = await userInfoes.GetUserBasicInfoesbyId(jti);
            var emp = await userInfoes.GetEmployeeById((int)user.employeeId);
            if (user.token != uid && user != null)
            {
                bool flag = false;
                string actionresult = "Invalid Token.";
                var jwts = await Tokens.changePasswordJwt(flag, actionresult, new JsonSerializerSettings { Formatting = Formatting.Indented });
                return new OkObjectResult(jwts);
            }

            var datajson = await _chemistScheduleService.CmnweeklyplanterritoryApprovedToday(jti, emp.employeeNo);

            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpGet("getEmployeeTADAByStatus")]
        public async Task<IActionResult> getEmployeeTADAByStatus()
        {
            var uid = Request.Headers["auth_token"];
            if (uid.Count() == 0)
            {
                bool flag = false;
                string actionresult = "Invalid Token.";
                var jwts = await Tokens.changePasswordJwt(flag, actionresult, new JsonSerializerSettings { Formatting = Formatting.Indented });
                return new OkObjectResult(jwts);
            }
            var stream = uid;
            var handler = new JwtSecurityTokenHandler();
            var tokenS = handler.ReadToken(stream) as JwtSecurityToken;
            var jti = tokenS.Claims.First(claim => claim.Type == "Id").Value;
            var user = await userInfoes.GetUserBasicInfoesbyId(jti);
            if (user.token != uid && user != null)
            {
                bool flag = false;
                string actionresult = "Invalid Token.";
                var jwts = await Tokens.changePasswordJwt(flag, actionresult, new JsonSerializerSettings { Formatting = Formatting.Indented });
                return new OkObjectResult(jwts);
            }

            var datajson = await _chemistScheduleService.getEmployeeTADAByStatus(jti);

            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpGet("getActionPlan")]
        public async Task<IActionResult> getActionPlan()
        {
            var uid = Request.Headers["auth_token"];
            if (uid.Count() == 0)
            {
                bool flag = false;
                string actionresult = "Invalid Token.";
                var jwts = await Tokens.changePasswordJwt(flag, actionresult, new JsonSerializerSettings { Formatting = Formatting.Indented });
                return new OkObjectResult(jwts);
            }
            var stream = uid;
            var handler = new JwtSecurityTokenHandler();
            var tokenS = handler.ReadToken(stream) as JwtSecurityToken;
            var jti = tokenS.Claims.First(claim => claim.Type == "Id").Value;
            var user = await userInfoes.GetUserBasicInfoesbyId(jti);
            if (user.token != uid && user != null)
            {
                bool flag = false;
                string actionresult = "Invalid Token.";
                var jwts = await Tokens.changePasswordJwt(flag, actionresult, new JsonSerializerSettings { Formatting = Formatting.Indented });
                return new OkObjectResult(jwts);
            }

            var datajson = await _chemistScheduleService.getActionPlan((int)user.employeeId);

            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpGet("getExamContent")]
        public async Task<IActionResult> getExamContent()
        {
            var uid = Request.Headers["auth_token"];
            if (uid.Count() == 0)
            {
                bool flag = false;
                string actionresult = "Invalid Token.";
                var jwts = await Tokens.changePasswordJwt(flag, actionresult, new JsonSerializerSettings { Formatting = Formatting.Indented });
                return new OkObjectResult(jwts);
            }
            var stream = uid;
            var handler = new JwtSecurityTokenHandler();
            var tokenS = handler.ReadToken(stream) as JwtSecurityToken;
            var jti = tokenS.Claims.First(claim => claim.Type == "Id").Value;
            var user = await userInfoes.GetUserBasicInfoesbyId(jti);
            if (user.token != uid && user != null)
            {
                bool flag = false;
                string actionresult = "Invalid Token.";
                var jwts = await Tokens.changePasswordJwt(flag, actionresult, new JsonSerializerSettings { Formatting = Formatting.Indented });
                return new OkObjectResult(jwts);
            }

            var datajson = await _chemistScheduleService.getExamContent();

            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }
        [HttpGet("getExamContentNew")]
        public async Task<IActionResult> getExamContentNew()
        {
            var uid = Request.Headers["auth_token"];
            if (uid.Count() == 0)
            {
                bool flag = false;
                string actionresult = "Invalid Token.";
                var jwts = await Tokens.changePasswordJwt(flag, actionresult, new JsonSerializerSettings { Formatting = Formatting.Indented });
                return new OkObjectResult(jwts);
            }
            var stream = uid;
            var handler = new JwtSecurityTokenHandler();
            var tokenS = handler.ReadToken(stream) as JwtSecurityToken;
            var jti = tokenS.Claims.First(claim => claim.Type == "Id").Value;
            var user = await userInfoes.GetUserBasicInfoesbyId(jti);
            if (user.token != uid && user != null)
            {
                bool flag = false;
                string actionresult = "Invalid Token.";
                var jwts = await Tokens.changePasswordJwt(flag, actionresult, new JsonSerializerSettings { Formatting = Formatting.Indented });
                return new OkObjectResult(jwts);
            }

            var datajson = await _chemistScheduleService.getExamContentNew();

            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpGet("getExamContentById")]
        public async Task<IActionResult> getExamContent(int contentId)
        {
            var uid = Request.Headers["auth_token"];
            if (uid.Count() == 0)
            {
                bool flag = false;
                string actionresult = "Invalid Token.";
                var jwts = await Tokens.changePasswordJwt(flag, actionresult, new JsonSerializerSettings { Formatting = Formatting.Indented });
                return new OkObjectResult(jwts);
            }
            var stream = uid;
            var handler = new JwtSecurityTokenHandler();
            var tokenS = handler.ReadToken(stream) as JwtSecurityToken;
            var jti = tokenS.Claims.First(claim => claim.Type == "Id").Value;
            var user = await userInfoes.GetUserBasicInfoesbyId(jti);
            if (user.token != uid && user != null)
            {
                bool flag = false;
                string actionresult = "Invalid Token.";
                var jwts = await Tokens.changePasswordJwt(flag, actionresult, new JsonSerializerSettings { Formatting = Formatting.Indented });
                return new OkObjectResult(jwts);
            }

            var datajson = await _chemistScheduleService.getExamContentById(contentId);

            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpGet("getAllExamContent")]
        public async Task<IActionResult> getAllExamContent()
        {
            var uid = Request.Headers["auth_token"];
            if (uid.Count() == 0)
            {
                bool flag = false;
                string actionresult = "Invalid Token.";
                var jwts = await Tokens.changePasswordJwt(flag, actionresult, new JsonSerializerSettings { Formatting = Formatting.Indented });
                return new OkObjectResult(jwts);
            }
            var stream = uid;
            var handler = new JwtSecurityTokenHandler();
            var tokenS = handler.ReadToken(stream) as JwtSecurityToken;
            var jti = tokenS.Claims.First(claim => claim.Type == "Id").Value;
            var user = await userInfoes.GetUserBasicInfoesbyId(jti);
            if (user.token != uid && user != null)
            {
                bool flag = false;
                string actionresult = "Invalid Token.";
                var jwts = await Tokens.changePasswordJwt(flag, actionresult, new JsonSerializerSettings { Formatting = Formatting.Indented });
                return new OkObjectResult(jwts);
            }

            var datajson = await _chemistScheduleService.getAllExamContent();

            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpGet("getExamByContentId")]
        public async Task<IActionResult> getExamByContentId(int contentId)
        {
            var uid = Request.Headers["auth_token"];
            if (uid.Count() == 0)
            {
                bool flag = false;
                string actionresult = "Invalid Token.";
                var jwts = await Tokens.changePasswordJwt(flag, actionresult, new JsonSerializerSettings { Formatting = Formatting.Indented });
                return new OkObjectResult(jwts);
            }
            var stream = uid;
            var handler = new JwtSecurityTokenHandler();
            var tokenS = handler.ReadToken(stream) as JwtSecurityToken;
            var jti = tokenS.Claims.First(claim => claim.Type == "Id").Value;
            var user = await userInfoes.GetUserBasicInfoesbyId(jti);
            if (user.token != uid && user != null)
            {
                bool flag = false;
                string actionresult = "Invalid Token.";
                var jwts = await Tokens.changePasswordJwt(flag, actionresult, new JsonSerializerSettings { Formatting = Formatting.Indented });
                return new OkObjectResult(jwts);
            }

            var datajson = await _chemistScheduleService.getExamByContentId(contentId, (int)user.employeeId);
            var datajson1 = await _chemistScheduleService.getGetExamResultByexamId((int)user.employeeId, 0, 0);
            var datajson2 = await _chemistScheduleService.getGetExamResultByexamId((int)user.employeeId, 0, 1);

            var jwt = await Tokens.getDataTripple(datajson.data, datajson1.data,datajson2.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpGet("getExamById")]
        public async Task<IActionResult> getExamById(int examId)
        {
            var uid = Request.Headers["auth_token"];
            if (uid.Count() == 0)
            {
                bool flag = false;
                string actionresult = "Invalid Token.";
                var jwts = await Tokens.changePasswordJwt(flag, actionresult, new JsonSerializerSettings { Formatting = Formatting.Indented });
                return new OkObjectResult(jwts);
            }
            var stream = uid;
            var handler = new JwtSecurityTokenHandler();
            var tokenS = handler.ReadToken(stream) as JwtSecurityToken;
            var jti = tokenS.Claims.First(claim => claim.Type == "Id").Value;
            var user = await userInfoes.GetUserBasicInfoesbyId(jti);
            if (user.token != uid && user != null)
            {
                bool flag = false;
                string actionresult = "Invalid Token.";
                var jwts = await Tokens.changePasswordJwt(flag, actionresult, new JsonSerializerSettings { Formatting = Formatting.Indented });
                return new OkObjectResult(jwts);
            }

            var datajson = await _chemistScheduleService.getExamById(examId, (int)user.employeeId);

            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpGet("getExamQuestionSetByexamId")]
        public async Task<IActionResult> getExamQuestionByexamId(int examId)
        {
            var uid = Request.Headers["auth_token"];
            if (uid.Count() == 0)
            {
                bool flag = false;
                string actionresult = "Invalid Token.";
                var jwts = await Tokens.changePasswordJwt(flag, actionresult, new JsonSerializerSettings { Formatting = Formatting.Indented });
                return new OkObjectResult(jwts);
            }
            var stream = uid;
            var handler = new JwtSecurityTokenHandler();
            var tokenS = handler.ReadToken(stream) as JwtSecurityToken;
            var jti = tokenS.Claims.First(claim => claim.Type == "Id").Value;
            var user = await userInfoes.GetUserBasicInfoesbyId(jti);
            if (user.token != uid && user != null)
            {
                bool flag = false;
                string actionresult = "Invalid Token.";
                var jwts = await Tokens.changePasswordJwt(flag, actionresult, new JsonSerializerSettings { Formatting = Formatting.Indented });
                return new OkObjectResult(jwts);
            }

            var datajson = await _chemistScheduleService.GetExamQuestionSetByExamId(examId, (int)user.employeeId);

            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpGet("getExamQuestionByExamId")]
        public async Task<IActionResult> getExamQuestionByExamId(int examId)
        {
            var uid = Request.Headers["auth_token"];
            if (uid.Count() == 0)
            {
                bool flag = false;
                string actionresult = "Invalid Token.";
                var jwts = await Tokens.changePasswordJwt(flag, actionresult, new JsonSerializerSettings { Formatting = Formatting.Indented });
                return new OkObjectResult(jwts);
            }
            var stream = uid;
            var handler = new JwtSecurityTokenHandler();
            var tokenS = handler.ReadToken(stream) as JwtSecurityToken;
            var jti = tokenS.Claims.First(claim => claim.Type == "Id").Value;
            var user = await userInfoes.GetUserBasicInfoesbyId(jti);
            if (user.token != uid && user != null)
            {
                bool flag = false;
                string actionresult = "Invalid Token.";
                var jwts = await Tokens.changePasswordJwt(flag, actionresult, new JsonSerializerSettings { Formatting = Formatting.Indented });
                return new OkObjectResult(jwts);
            }

            var datajson = await _chemistScheduleService.getExamQuestionByexamId(examId);

            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpGet("getExamResultByExamId")]
        public async Task<IActionResult> getExamResultByExamId(int examId)
        {
            var uid = Request.Headers["auth_token"];
            if (uid.Count() == 0)
            {
                bool flag = false;
                string actionresult = "Invalid Token.";
                var jwts = await Tokens.changePasswordJwt(flag, actionresult, new JsonSerializerSettings { Formatting = Formatting.Indented });
                return new OkObjectResult(jwts);
            }
            var stream = uid;
            var handler = new JwtSecurityTokenHandler();
            var tokenS = handler.ReadToken(stream) as JwtSecurityToken;
            var jti = tokenS.Claims.First(claim => claim.Type == "Id").Value;
            var user = await userInfoes.GetUserBasicInfoesbyId(jti);
            if (user.token != uid && user != null)
            {
                bool flag = false;
                string actionresult = "Invalid Token.";
                var jwts = await Tokens.changePasswordJwt(flag, actionresult, new JsonSerializerSettings { Formatting = Formatting.Indented });
                return new OkObjectResult(jwts);
            }

            var datajson = await _chemistScheduleService.getExamResultByExamId(examId);

            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpPost("ExamContent")]
        public async Task<IActionResult> SaveExamContent([FromBody] ExamContentViewModel model)
        {
            #region Token
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

            int result = await _chemistScheduleService.setExamContent((int)user.employeeId, model);

            if (result != 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Content has created successfully.", true, result);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Content has not created successfully.", false);
                return new OkObjectResult(jwt);
            }

        }



        [HttpPost("DeleteExamContent")]
        public async Task<IActionResult> DeleteExamContent([FromBody] ExamContentViewModel model)
        {
            #region Token
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

            int result = await _chemistScheduleService.deleteExamContent((int)user.employeeId, model.CmnExamContentID);

            if (result != 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Content has deleted successfully.", true, result);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Content has not deleted successfully.", false);
                return new OkObjectResult(jwt);
            }

        }

        [HttpPost("ExamQuestionSet")]
        public async Task<IActionResult> SaveExamQuestionSet([FromBody] CmnExamQuestionViewModel model)
        {
            #region Token
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

            int result = await _chemistScheduleService.setExam((int)user.employeeId, model);

            if (result != 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Content has created successfully.", true, result);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Content has not created successfully.", false);
                return new OkObjectResult(jwt);
            }

        }

        [HttpPost("setExamPerform")]
        public async Task<IActionResult> setExamPerform([FromBody] CmnExamPerformListViewModel model)
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
            var employee = await userInfoes.GetEmployeeById((int)user.employeeId);
            if (user.token != uid && user != null)
            {
                bool status = false;
                string actionresult = "Invalid Token.";
                var jwts = await Tokens.changePasswordJwt(status, actionresult, new JsonSerializerSettings { Formatting = Formatting.Indented });
                return new OkObjectResult(jwts);
            }
            int result = 0;
            int? examId = 0;
            //bool result = await _doctorService.SaveActivityStatus(user.employeeId.ToString(), model);
            for (int i = 0; i < model.cmnExamPerformViewModels.Count(); i++)
            {
                result = await _chemistScheduleService.setExamPerform((int)user.employeeId, model.cmnExamPerformViewModels[i]);
                examId = model.cmnExamPerformViewModels[i].CmnExamQuestionId;
            }

            if (result > 0)
            {

                var datajson = await _chemistScheduleService.getGetExamResultByexamId((int)user.employeeId, (int)examId, 0);
                var datajson1 = await _chemistScheduleService.getGetExamResultByexamId((int)user.employeeId, (int)examId, 1);

                var jwt = await Tokens.getDataDouble(datajson.data, datajson1.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
                return new OkObjectResult(jwt);

                //var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Exam Perform save successfully.", true);
                //return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Exam Perform  has not save successfully.", false);
                return new OkObjectResult(jwt);
            }

        }

        [HttpGet("getExamPerform")]
        public async Task<IActionResult> getExamPerform()
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
            var employee = await userInfoes.GetEmployeeById((int)user.employeeId);
            if (user.token != uid && user != null)
            {
                bool status = false;
                string actionresult = "Invalid Token.";
                var jwts = await Tokens.changePasswordJwt(status, actionresult, new JsonSerializerSettings { Formatting = Formatting.Indented });
                return new OkObjectResult(jwts);
            }
            #endregion
                var datajson = await _chemistScheduleService.GetExamResult((int)user.employeeId,  0);
                var datajson1 = await _chemistScheduleService.GetExamResult((int)user.employeeId,  1);

                var jwt = await Tokens.getDataDouble(datajson.data, datajson1.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
                return new OkObjectResult(jwt);

        }

        [HttpGet("getActionCampain")]
        public async Task<IActionResult> getActionCampain()
        {
            var uid = Request.Headers["auth_token"];
            if (uid.Count() == 0)
            {
                bool flag = false;
                string actionresult = "Invalid Token.";
                var jwts = await Tokens.changePasswordJwt(flag, actionresult, new JsonSerializerSettings { Formatting = Formatting.Indented });
                return new OkObjectResult(jwts);
            }
            var stream = uid;
            var handler = new JwtSecurityTokenHandler();
            var tokenS = handler.ReadToken(stream) as JwtSecurityToken;
            var jti = tokenS.Claims.First(claim => claim.Type == "Id").Value;
            var user = await userInfoes.GetUserBasicInfoesbyId(jti);
            if (user.token != uid && user != null)
            {
                bool flag = false;
                string actionresult = "Invalid Token.";
                var jwts = await Tokens.changePasswordJwt(flag, actionresult, new JsonSerializerSettings { Formatting = Formatting.Indented });
                return new OkObjectResult(jwts);
            }

            var datajson = await _chemistScheduleService.getActionCampain((int)user.employeeId);

            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpGet("getKnowledgeSkill")]
        public async Task<IActionResult> getKnowledgeSkill()
        {
            var uid = Request.Headers["auth_token"];
            if (uid.Count() == 0)
            {
                bool flag = false;
                string actionresult = "Invalid Token.";
                var jwts = await Tokens.changePasswordJwt(flag, actionresult, new JsonSerializerSettings { Formatting = Formatting.Indented });
                return new OkObjectResult(jwts);
            }
            var stream = uid;
            var handler = new JwtSecurityTokenHandler();
            var tokenS = handler.ReadToken(stream) as JwtSecurityToken;
            var jti = tokenS.Claims.First(claim => claim.Type == "Id").Value;
            var user = await userInfoes.GetUserBasicInfoesbyId(jti);
            if (user.token != uid && user != null)
            {
                bool flag = false;
                string actionresult = "Invalid Token.";
                var jwts = await Tokens.changePasswordJwt(flag, actionresult, new JsonSerializerSettings { Formatting = Formatting.Indented });
                return new OkObjectResult(jwts);
            }

            var datajson = await _chemistScheduleService.getKnowledgeSkill();

            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpGet("getAppsversion")]
        public async Task<IActionResult> getAppsversion()
        {
            var uid = Request.Headers["auth_token"];
            if (uid.Count() == 0)
            {
                bool flag = false;
                string actionresult = "Invalid Token.";
                var jwts = await Tokens.changePasswordJwt(flag, actionresult, new JsonSerializerSettings { Formatting = Formatting.Indented });
                return new OkObjectResult(jwts);
            }
            var stream = uid;
            var handler = new JwtSecurityTokenHandler();
            var tokenS = handler.ReadToken(stream) as JwtSecurityToken;
            var jti = tokenS.Claims.First(claim => claim.Type == "Id").Value;
            var user = await userInfoes.GetUserBasicInfoesbyId(jti);
            if (user.token != uid && user != null)
            {
                bool flag = false;
                string actionresult = "Invalid Token.";
                var jwts = await Tokens.changePasswordJwt(flag, actionresult, new JsonSerializerSettings { Formatting = Formatting.Indented });
                return new OkObjectResult(jwts);
            }

            var datajson = await _chemistScheduleService.getAppsversion((int)user.employeeId);

            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpGet("getEmployeeWiseTADAByEmployeeCode")]
        public async Task<IActionResult> getEmployeeWiseTADAByEmployeeCode(string code)
        {
            var uid = Request.Headers["auth_token"];
            if (uid.Count() == 0)
            {
                bool flag = false;
                string actionresult = "Invalid Token.";
                var jwts = await Tokens.changePasswordJwt(flag, actionresult, new JsonSerializerSettings { Formatting = Formatting.Indented });
                return new OkObjectResult(jwts);
            }
            var stream = uid;
            var handler = new JwtSecurityTokenHandler();
            var tokenS = handler.ReadToken(stream) as JwtSecurityToken;
            var jti = tokenS.Claims.First(claim => claim.Type == "Id").Value;
            var user = await userInfoes.GetUserBasicInfoesbyId(jti);
            if (user.token != uid && user != null)
            {
                bool flag = false;
                string actionresult = "Invalid Token.";
                var jwts = await Tokens.changePasswordJwt(flag, actionresult, new JsonSerializerSettings { Formatting = Formatting.Indented });
                return new OkObjectResult(jwts);
            }

            var datajson = await _chemistScheduleService.getEmployeeWiseTADAByEmployeeCode(code);
            var datajson1 = await _chemistScheduleService.getEmployeeWiseVehicleBillByEmployeeCode(code);

            var jwt = await Tokens.getDataDouble(datajson.data, datajson1.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpGet("getEmployeeByRegionZoneTerritory")]
        public async Task<IActionResult> getEmployeeByRegionZoneTerritory()
        {
            var uid = Request.Headers["auth_token"];
            if (uid.Count() == 0)
            {
                bool flag = false;
                string actionresult = "Invalid Token.";
                var jwts = await Tokens.changePasswordJwt(flag, actionresult, new JsonSerializerSettings { Formatting = Formatting.Indented });
                return new OkObjectResult(jwts);
            }
            var stream = uid;
            var handler = new JwtSecurityTokenHandler();
            var tokenS = handler.ReadToken(stream) as JwtSecurityToken;
            var jti = tokenS.Claims.First(claim => claim.Type == "Id").Value;
            var user = await userInfoes.GetUserBasicInfoesbyId(jti);
            if (user.token != uid && user != null)
            {
                bool flag = false;
                string actionresult = "Invalid Token.";
                var jwts = await Tokens.changePasswordJwt(flag, actionresult, new JsonSerializerSettings { Formatting = Formatting.Indented });
                return new OkObjectResult(jwts);
            }

            var datajson = await _chemistScheduleService.getEmployeeByRegionZoneTerritory(jti);

            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpGet("getDoctorBasicDegree")]
        public async Task<IActionResult> getDoctorBasicDegree()
        {
            var uid = Request.Headers["auth_token"];
            if (uid.Count() == 0)
            {
                bool flag = false;
                string actionresult = "Invalid Token.";
                var jwts = await Tokens.changePasswordJwt(flag, actionresult, new JsonSerializerSettings { Formatting = Formatting.Indented });
                return new OkObjectResult(jwts);
            }
            var stream = uid;
            var handler = new JwtSecurityTokenHandler();
            var tokenS = handler.ReadToken(stream) as JwtSecurityToken;
            var jti = tokenS.Claims.First(claim => claim.Type == "Id").Value;
            var user = await userInfoes.GetUserBasicInfoesbyId(jti);
            if (user.token != uid && user != null)
            {
                bool flag = false;
                string actionresult = "Invalid Token.";
                var jwts = await Tokens.changePasswordJwt(flag, actionresult, new JsonSerializerSettings { Formatting = Formatting.Indented });
                return new OkObjectResult(jwts);
            }

            var datajson = await _chemistScheduleService.getDoctorBasicDegree(jti);

            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpGet("getTerritoryWiseMonthlyPromoItem")]
        public async Task<IActionResult> getTerritoryWiseMonthlyPromoItem(int monthno)
        {
            var uid = Request.Headers["auth_token"];
            if (uid.Count() == 0)
            {
                bool flag = false;
                string actionresult = "Invalid Token.";
                var jwts = await Tokens.changePasswordJwt(flag, actionresult, new JsonSerializerSettings { Formatting = Formatting.Indented });
                return new OkObjectResult(jwts);
            }
            var stream = uid;
            var handler = new JwtSecurityTokenHandler();
            var tokenS = handler.ReadToken(stream) as JwtSecurityToken;
            var jti = tokenS.Claims.First(claim => claim.Type == "Id").Value;
            var user = await userInfoes.GetUserBasicInfoesbyId(jti);
            if (user.token != uid && user != null)
            {
                bool flag = false;
                string actionresult = "Invalid Token.";
                var jwts = await Tokens.changePasswordJwt(flag, actionresult, new JsonSerializerSettings { Formatting = Formatting.Indented });
                return new OkObjectResult(jwts);
            }

            var datajson = await _chemistScheduleService.getTerritoryWiseMonthlyPromoItem(jti, monthno);

            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpPost("SetsalesTarget")]
        public async Task<IActionResult> SetsalesTarget([FromBody] IncentiveCalculationViewModel model)
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
            var employee = await userInfoes.GetEmployeeById((int)user.employeeId);
            if (user.token != uid && user != null)
            {
                bool status = false;
                string actionresult = "Invalid Token.";
                var jwts = await Tokens.changePasswordJwt(status, actionresult, new JsonSerializerSettings { Formatting = Formatting.Indented });
                return new OkObjectResult(jwts);
            }
            if (model.employeeId == 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Please Select Employee.", false);
                return new OkObjectResult(jwt);
            }

            //bool result = await _doctorService.SaveActivityStatus(user.employeeId.ToString(), model);
            bool result = await _doctorService.SetsalesTarget(user.employeeId.ToString(), model);

            if (result == true)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Target Set successfully.", true);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Target has not Set successfully.", false);
                return new OkObjectResult(jwt);
            }

        }
    }
}
