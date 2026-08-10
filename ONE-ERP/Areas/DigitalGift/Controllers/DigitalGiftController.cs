using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ONEERP.Areas.DigitalGift.Models;
using ONEERP.Data.Entity;
using ONEERP.ERPService.AuthService.Interfaces;
using ONEERP.ERPServices.DigitalGift.Interfaces;
using ONEERP.Helpers;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.DigitalGift.Controllers
{
    [Route("api/[controller]")]
    public class DigitalGiftController : Controller
    {
        object jwts;
        ApplicationUser user;
        private readonly IUserInfoes userInfoes;
        private readonly IDigitalGiftService service;
        public DigitalGiftController(IUserInfoes _userInfoes, IDigitalGiftService _service)
        {
            userInfoes = _userInfoes;
            service = _service;
            jwts = new object();
            user = new ApplicationUser();
        }

        [HttpPost("DigitalGiftDisburse")]
        public async Task<IActionResult> DigitalGiftDisburse([FromBody] DigitalGiftModels model)
        {
            try
            {
                // another api in CmnDropDown controller: getAllTerritoriesHH

                if (Authentication().Result == false) return new OkObjectResult(jwts);

                /*
                if (!ValidateTheToken(Request.Headers["auth_token"]))
                {
                    //var log = await userInfoes.SetPaymentApiLog(model.UserName, "Request: BillPayment", "406", $"Auth_token expired or invalid!", model.TrxId, model.Amount, model.PayTime, model.BillMonth, model.CustomerNo);

                    var invalidTokenJwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Invalid Token !.", false);
                    return new OkObjectResult(invalidTokenJwt);
                }
                */

                if (!ModelState.IsValid) //if (model == null)
                {
                    var jwtRes = await Tokens.GetJsonResponse("Invalid or empty model data", false);
                    return new OkObjectResult(jwtRes);
                }

                if (string.IsNullOrWhiteSpace(model.Name) && model.Name.Trim().Length < 3)
                {
                    var jwtRes = await Tokens.GetJsonResponse("Please input a valid Name", false);
                    return new OkObjectResult(jwtRes);
                }

                if (string.IsNullOrWhiteSpace(model.MobileNumber) || model.MobileNumber.Trim().Length != 11)
                {
                    var jwtRes = await Tokens.GetJsonResponse("Invalid mobile number", false);
                    return new OkObjectResult(jwtRes);
                }

                if (string.IsNullOrWhiteSpace(model.CouponCode) || model.CouponCode.Trim().Length != 8)
                {
                    var jwtRes = await Tokens.GetJsonResponse("Invalid Coupon Code", false);
                    return new OkObjectResult(jwtRes);
                }

                //if (DateTime.Now < Convert.ToDateTime("2025-Mar-17"))
                //    await service.DigitalGiftDisburseLog(model);

                // check the response succes or failed
                var jsonResult = await service.ValidateRequestedInfo(user.employeeId, model);
                dynamic data = JsonConvert.DeserializeObject(jsonResult.data);

                int couponStatus = data[0].CouponStatus;
                string resMessage = data[0].Message;

                if (couponStatus == 1)
                {
                    var result = await service.DigitalGiftDisburse(user.employeeId, model);
                    bool status = result.data.Contains("False: ") ? false : true;
                    var jwt = await Tokens.GetJsonResponse(result.data.Replace("False: ", ""), status);
                    return new OkObjectResult(jwt);
                }
                else
                {
                    var jwt = await Tokens.GetJsonResponse(resMessage, false);
                    return new OkObjectResult(jwt);
                }

            }
            catch (Exception ex)
            {
                var jwt = await Tokens.GetJsonResponse($"Exceeption occurred in Digital Gift Disburse processing!", false);
                return new OkObjectResult(jwt);
            }
        }

        [HttpPost("DigitalGiftDisburse_v2")]
        public async Task<IActionResult> DigitalGiftDisburse_v2([FromBody] DigitalGiftModels model)
        {
            try
            {
                if (Authentication().Result == false) return new OkObjectResult(jwts);

                /*
                if (!ValidateTheToken(Request.Headers["auth_token"]))
                {
                    //var log = await userInfoes.SetPaymentApiLog(model.UserName, "Request: BillPayment", "406", $"Auth_token expired or invalid!", model.TrxId, model.Amount, model.PayTime, model.BillMonth, model.CustomerNo);

                    var invalidTokenJwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Invalid Token !.", false);
                    return new OkObjectResult(invalidTokenJwt);
                }
                */

                if (!ModelState.IsValid) //if (model == null)
                {
                    var jwtRes = await Tokens.GetJsonResponse("Invalid or empty model data", false);
                    return new OkObjectResult(jwtRes);
                }

                if (string.IsNullOrWhiteSpace(model.Name) && model.Name.Trim().Length < 3)
                {
                    var jwtRes = await Tokens.GetJsonResponse("Please input a valid Name", false);
                    return new OkObjectResult(jwtRes);
                }

                if (string.IsNullOrWhiteSpace(model.MobileNumber) || model.MobileNumber.Trim().Length != 11)
                {
                    var jwtRes = await Tokens.GetJsonResponse("Invalid mobile number", false);
                    return new OkObjectResult(jwtRes);
                }

                if (string.IsNullOrWhiteSpace(model.CouponCode) || model.CouponCode.Trim().Length != 8)
                {
                    var jwtRes = await Tokens.GetJsonResponse("Invalid Coupon Code", false);
                    return new OkObjectResult(jwtRes);
                }

                // check the response succes or failed
                var jsonResult = await service.ValidateRequestedInfo(user.employeeId, model);
                dynamic data = JsonConvert.DeserializeObject(jsonResult.data);

                int couponStatus = data[0].CouponStatus;
                string resMessage = data[0].Message;

                if (couponStatus == 1)
                {

                    var authData = await service.GetBulkOAuthResponse(); //1
                    var packListResponse = await service.GetPackList(authData); //2
                    if (packListResponse != null && packListResponse.data.pack_list.Count > 0)
                    {
                        Pack_list packList = packListResponse.data.pack_list[0];
                        var dispurse = await service.DigitalGiftPackDisburse(user.employeeId, authData.accessToken, model, packList); //3
                    }
                    else
                    {
                        //msg
                    }

                    var result = await service.DigitalGiftDisburseV2(user.employeeId, model);
                    bool status = result.data.Contains("False: ") ? false : true;
                    var jwt = await Tokens.GetJsonResponse(result.data.Replace("False: ", ""), status);
                    return new OkObjectResult(jwt);
                }
                else
                {
                    var jwt = await Tokens.GetJsonResponse(resMessage, false);
                    return new OkObjectResult(jwt);
                }

            }
            catch (Exception ex)
            {
                var jwt = await Tokens.GetJsonResponse($"Exceeption occurred in Digital Gift Disburse processing!", false);
                return new OkObjectResult(jwt);
            }
        }

        async Task<bool> Authentication()
        {
            #region common
            try
            {
                var uid = Request.Headers["auth_token"];
                var fixedToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJNb3N0YWZhQGVtYWlsLmNvbSIsImp0aSI6IjUwMzhjNzQyLWVkNzAtNDRiNS1hMGI3LTBhMjRlNzFmYWJiYiIsImlhdCI6MTcyNzU4NjEzNSwiSWQiOiIwMjEwNzVEQy0xNjFBLTQ4QTUtODlBMy1FM0NEODQ1OTVFODkiLCJuYmYiOjE3Mjc1ODYxMzQsImV4cCI6MTcyNzU5MzMzNCwiaXNzIjoid2ViQXBpIiwiYXVkIjoiaHR0cDovL2xvY2FsaG9zdDo5MTE1LyJ9.cy7P0ws__zHO2ZuwuDnVmxggrJ1LYM9se81lx3cxCYM";

                if (uid == fixedToken)
                {
                    return true;
                }
                else
                {
                    bool status = false;
                    string actionresult = "Invalid Token.";
                    jwts = await Tokens.changePasswordJwt(status, actionresult, new JsonSerializerSettings { Formatting = Formatting.Indented });
                    return false;
                }
                /*
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
                    string actionresult = "Unauthorized access or invalid token.";
                    jwts = Tokens.changePasswordJwt(status, actionresult, new JsonSerializerSettings { Formatting = Formatting.Indented });
                    return false;
                }
                return true;
                */
            }
            catch (Exception)
            {
                return false;
            }
            #endregion
        }

        private bool ValidateTheToken(string token)
        {
            JwtSecurityToken jwtSecurityToken;
            try
            {
                jwtSecurityToken = new JwtSecurityToken(token);

                var handler = new JwtSecurityTokenHandler();
                var tokenS = handler.ReadToken(token) as JwtSecurityToken;
                var jti = tokenS.Claims.First(claim => claim.Type == "Id").Value;
                //var user = await userInfoes.GetUserBasicInfoesbyId(jti); // no need

                if (string.IsNullOrEmpty(jti) || (jti != "292f4b79-1268-4472-b276-6edea892cec8"))
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }

            return jwtSecurityToken.ValidTo > DateTime.UtcNow;
        }

    }
}
