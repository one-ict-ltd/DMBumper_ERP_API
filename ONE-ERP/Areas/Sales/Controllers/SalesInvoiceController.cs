using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NPOI.SS.UserModel;
using ONEERP.Areas.Sales.Models;
using ONEERP.Data.Entity;
using ONEERP.ERPService.AuthService.Interfaces;
using ONEERP.ERPServices.FieldForceTracking.Interfaces;
using ONEERP.ERPServices.Sales.Interfaces;
using ONEERP.Helpers;
using ONEERP.Models;

namespace ONEERP.Areas.Sales.Controllers
{
    [Route("api/[controller]")]
    public class SalesInvoiceController : Controller
    {
        private IUserInfoes userInfoes;
        private TokenAuthenticator authenticator;
        private readonly ISalesInvoiceService service;
        private readonly IEmployeeService employeeService;
        public SalesInvoiceController(IUserInfoes _userInfoes, ISalesInvoiceService _service, IEmployeeService employeeService)
        {
            this.userInfoes = _userInfoes;
            this.service = _service;
            this.employeeService = employeeService;
            authenticator = new TokenAuthenticator(_userInfoes);
        }

        #region Sales Order for Mobile App

        [HttpGet("GetSalesOrderMasterApprovedList")]
        public async Task<IActionResult> GetSalesOrderMasterApprovedList(int masterId, string territoryCode)
        {
            var AuthModel = await authenticator.AuthenticationStatus(Request.Headers["auth_token"]);
            if (AuthModel.IsAuthorized == false) return new OkObjectResult(AuthModel.Jwts);
            var employeeId = AuthModel.ApplicationUserInfo.employeeId.ToString();

            var datajson = await service.GetSalesOrderMasterApprovedList(employeeId, masterId, territoryCode);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpGet("GetSalesOrderDetailsByIdForApproval")]
        public async Task<IActionResult> GetSalesOrderDetailsByIdForApproval(int salesOrderId)
        {
            var AuthModel = await authenticator.AuthenticationStatus(Request.Headers["auth_token"]);
            if (AuthModel.IsAuthorized == false) return new OkObjectResult(AuthModel.Jwts);
            //var employeeId = AuthModel.ApplicationUserInfo.employeeId.ToString();

            var datajson = await service.GetSalesOrderDetailsByIdForApproval(salesOrderId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpPost("SaveSalesOrder")]
        public async Task<IActionResult> SaveSalesOrder([FromBody] SalesOrderViewModel model)
        {
            var AuthModel = await authenticator.AuthenticationStatus(Request.Headers["auth_token"]);
            if (AuthModel.IsAuthorized == false) return new OkObjectResult(AuthModel.Jwts);
            var employeeId = AuthModel.ApplicationUserInfo.employeeId.ToString();

            if (model == null || model.lstDetailsViewModel == null || model.lstDetailsViewModel.Count() == 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "No item found to save.", false);
                return new OkObjectResult(jwt);
            }

            foreach (var item in model.lstDetailsViewModel)
            {
                if (item.isSelect == true)
                {
                    var res = await service.GetValidateProductAvailableStockForOrder(employeeId, model.storeId, item.productWiseSpecificationId, item.batchNo, item.orderQty, model.partyId, model.salesOrderDate, item.hasNationalBonus);

                    if (!string.IsNullOrWhiteSpace(res))
                    {
                        var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, res, false);
                        return new OkObjectResult(jwt);
                    }
                }
            }

            int result = 0;
            int salesOrderId = await service.SaveSalesOrder(employeeId, model);

            if (salesOrderId == 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Sales Invoice has not created.", false);
                return new OkObjectResult(jwt);
            }

            var employee = await userInfoes.GetEmployeeById(Convert.ToInt32(employeeId));
            result = await service.SaveSalesOrderDetails(employeeId, model.lstDetailsViewModel, salesOrderId, (int)model.storeId, (int)employee.companyId);

            //await service.SaveSalesOrderTC(employeeId, model.tcLstDetailsViewModel, salesOrderId);

            if (result > 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Sales Invoice Details has created successfully.", true, salesOrderId);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Sales Invoice Details has not created.", false);
                return new OkObjectResult(jwt);
            }
        }

        [HttpGet("GetAvailableStockForOrder")]
        public async Task<IActionResult> GetAvailableStockForOrder(int storeId, int productWiseSpecificationId)
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

            var datajson = await service.GetAvailableStockForOrder(user.employeeId, storeId, productWiseSpecificationId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpGet("GetSalesOrderById")]
        public async Task<IActionResult> GetSalesOrderById(int? salesOrderId, DateTime? fDate, DateTime? tDate, int? approvalStatus, int? partyId)
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

            var datajson = await service.GetSalesOrderById(salesOrderId, user.employeeId, fDate, tDate, approvalStatus, partyId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpPost("DeleteSalesOrderById")]
        public async Task<IActionResult> DeleteSalesOrderById([FromBody] DeleteSalesOrderViewModel model)
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

            if (model == null || model.salesOrderId <= 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Sales Order ID not found.", false);
                return new OkObjectResult(jwt);
            }

            bool result = await service.DeleteSalesOrderById(user.employeeId.ToString(), model.salesOrderId);

            if (result == true)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Sales Order has deleted successfully.", true);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "This Order already Approved. So you can not delete.", false);
                return new OkObjectResult(jwt);
            }
        }

        [HttpPost("DeleteSalesOrderDetailsByOrderDetailsId")]
        public async Task<IActionResult> DeleteSalesOrderDetailsByOrderDetailsId([FromBody] int salesOrderDetailsId)
        {

            var AuthModel = await authenticator.AuthenticationStatus(Request.Headers["auth_token"]);
            if (AuthModel.IsAuthorized == false) return new OkObjectResult(AuthModel.Jwts);


            bool result = await service.DeleteSalesOrderDetailsByOrderDetailsId(AuthModel.ApplicationUserInfo.employeeId, salesOrderDetailsId);

            if (result == true)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Sales Order has deleted successfully.", true);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "This Order already Approved. So you can not delete.", false);
                return new OkObjectResult(jwt);
            }
        }

        #endregion Sales Order for Mobile App


        #region Sales Invoice
        [HttpPost("GenerateSalesInvoiceBySalesOrder")]
        public async Task<IActionResult> GenerateSalesInvoiceBySalesOrder([FromBody] GenerateInvoiceViewModel model)
        {
            var AuthModel = await authenticator.AuthenticationStatus(Request.Headers["auth_token"]);
            if (AuthModel.IsAuthorized == false) return new OkObjectResult(AuthModel.Jwts);
            var employeeId = AuthModel.ApplicationUserInfo.employeeId;

            if (model == null || model.lstApprovedOrderList.Count() == 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Sales Invoice has not created.", false);
                return new OkObjectResult(jwt);
            }
            else
            {
                foreach (var item in model.lstApprovedOrderList)
                {
                    if (item.isSelect == true)
                    {
                        var res = await service.ValidateCurrentStockForOrder(employeeId, item.salesOrderId, 0, 0, 0);
                        if (!string.IsNullOrWhiteSpace(res))
                        {
                            var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, res, false);
                            return new OkObjectResult(jwt);
                        }
                    }
                }
            }

            int salesInvoiceId = 0;
            salesInvoiceId = await service.GenerateSalesInvoiceBySalesOrder(employeeId, model);

            if (salesInvoiceId > 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Sales Invoice has created Successfully.", true);
                return new OkObjectResult(jwt);
            }
            else if (salesInvoiceId == -1)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Current stock not available for one or more product", false);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Something went wrong! Invoice has not generated.", false);
                return new OkObjectResult(jwt);
            }
        }

        [HttpPost("GenerateSalesInvoiceBySalesOrder_v2")]
        public async Task<IActionResult> GenerateSalesInvoiceBySalesOrder_v2([FromBody] GenerateInvoiceViewModel model)
        {
            var AuthModel = await authenticator.AuthenticationStatus(Request.Headers["auth_token"]);
            if (AuthModel.IsAuthorized == false) return new OkObjectResult(AuthModel.Jwts);
            var employeeId = AuthModel.ApplicationUserInfo.employeeId;

            if (model == null || model.lstApprovedOrderList.Count() == 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Sales Order not found!", false);
                return new OkObjectResult(jwt);
            }
            else
            {
                int orderId = 0;

                var obj = model.lstApprovedOrderList.Where(x => x.isSelect == true).SingleOrDefault();

                if (obj == null)
                {
                    var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Please select a Order.", false);
                    return new OkObjectResult(jwt);
                }

                orderId = obj.salesOrderId;

                var DuesStatus = await service.ValidateCustomerDuesStatusForOrder(employeeId, orderId);
                if (!string.IsNullOrWhiteSpace(DuesStatus))
                {
                    var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, DuesStatus, false);
                    return new OkObjectResult(jwt);
                }


                foreach (var item in model.lstApprovedOrderList)
                {
                    if (item.isSelect == true)
                    {
                        orderId = item.salesOrderId;
                        var res = await service.ValidateCurrentStockForOrder(employeeId, item.salesOrderId, 0, 0, 0);
                        if (!string.IsNullOrWhiteSpace(res))
                        {
                            var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, res, false);
                            return new OkObjectResult(jwt);
                        }
                    }
                }
            }

            int salesInvoiceId = 0;
            salesInvoiceId = await service.GenerateSalesInvoiceBySalesOrder_v2(employeeId, model);

            if (salesInvoiceId > 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Sales Invoice has generated Successfully.", true);
                return new OkObjectResult(jwt);
            }
            else if (salesInvoiceId == -1)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Current stock not available for one or more product", false);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Something went wrong! Invoice has not generated.", false);
                return new OkObjectResult(jwt);
            }
        }

        [HttpPost("SaveSalesInvoice")]
        public async Task<IActionResult> SaveSalesInvoice([FromBody] SalesInvoiceViewModel model)
        {
            var AuthModel = await authenticator.AuthenticationStatus(Request.Headers["auth_token"]);
            if (AuthModel.IsAuthorized == false) return new OkObjectResult(AuthModel.Jwts);
            var employeeId = AuthModel.ApplicationUserInfo.employeeId.ToString();
            //  var employee=await employeeService.empl

            if (model == null || model.lstDetailsViewModel == null || model.lstDetailsViewModel.Count() == 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "No item found to save.", false);
                return new OkObjectResult(jwt);
            }

            /*
            foreach (var item in model.lstDetailsViewModel)
            {
                if (string.IsNullOrWhiteSpace(item.batchNo))
                {
                    var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Batch Number Not Found. Empty batch number does not allowed!", false);
                    return new OkObjectResult(jwt);
                }
            }
            */

            foreach (var item in model.lstDetailsViewModel)
            {
                if (item.isSelect == true)
                {
                    var res = await service.GetValidateProductStockForInvoice(employeeId, model.storeId, item.productWiseSpecificationId, item.batchNo, item.invoiceQty, model.partyId, model.salesInvoiceDate, item.hasNationalBonus);

                    if (!string.IsNullOrWhiteSpace(res))
                    {
                        var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, res, false);
                        return new OkObjectResult(jwt);
                    }
                }
            }

            int result = 0;
            int salesInvoiceId = await service.SaveSalesInvoice(employeeId, model);

            if (salesInvoiceId == 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Sales Invoice has not created.", false);
                return new OkObjectResult(jwt);
            }

            var employee = await userInfoes.GetEmployeeById(Convert.ToInt32(employeeId));
            result = await service.SaveSalesInvoiceDetails(employeeId, model.lstDetailsViewModel, salesInvoiceId, (int)model.storeId, (int)employee.companyId);

            await service.SaveSalesInvoiceTC(employeeId, model.tcLstDetailsViewModel, salesInvoiceId);

            if (model.transactionTypeId == 1) //Cash
            {
                int voucherMasterId = await service.CreateAutoJournalForSalesInvoice(employeeId, model);
            }
            else if (model.transactionTypeId == 2) //Credit
            {
                int voucherMasterId = await service.CreateAutoJournalForSalesInvoiceOnCredit(employeeId, model);
            }
            else if (model.transactionTypeId == 3) //Advance
            {
                int voucherMasterId = await service.CreateAutoJournalForSalesInvoiceOnAdvance(employeeId, model);
            }

            if (result != 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Sales Invoice Details has created successfully.", true, salesInvoiceId);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Sales Invoice Details has not created.", false);
                return new OkObjectResult(jwt);
            }
        }

        [HttpGet("GetSalesInvoiceById")]
        public async Task<IActionResult> GetSalesInvoiceById(int? salesInvoiceId, DateTime? fDate, DateTime? tDate)
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

            var datajson = await service.GetSalesInvoiceById(salesInvoiceId, user.employeeId, fDate, tDate);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpGet("GetInvoiceGDNConfirmation")]
        public async Task<IActionResult> GetInvoiceGDNConfirmation(int? salesInvoiceId, DateTime? fDate, DateTime? tDate, int gdnType)
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

            var datajson = await service.GetInvoiceGDNConfirmation(salesInvoiceId, user.employeeId, fDate, tDate, gdnType);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpGet("GetSalesInvoiceForPosById")]
        public async Task<IActionResult> GetSalesInvoiceForPosById(int? salesInvoiceId)
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

            var datajson = await service.GetSalesInvoiceForPosById(salesInvoiceId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpPost("DeleteSalesInvoiceById")]
        public async Task<IActionResult> DeleteSalesInvoiceById([FromBody] int salesInvoiceId)
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

            if (salesInvoiceId <= 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Sales Invoice has not deleted.", false);
                return new OkObjectResult(jwt);
            }
            bool result = await service.DeleteSalesInvoiceById(user.employeeId.ToString(), salesInvoiceId);

            if (result == true)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Sales Invoice has deleted successfully.", true);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Sales Invoice has not deleted.", false);
                return new OkObjectResult(jwt);
            }
        }

        [HttpPost("DeleteGDNById")]
        public async Task<IActionResult> DeleteGDNById([FromBody] int salesInvoiceId)
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

            if (salesInvoiceId <= 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "GDN Sales Confirmation has not deleted.", false);
                return new OkObjectResult(jwt);
            }
            bool result = await service.DeleteGDNById(user.employeeId.ToString(), salesInvoiceId);

            if (result == true)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "GDN Sales Confirmation has deleted successfully.", true);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "GDN Sales Confirmation has not deleted.", false);
                return new OkObjectResult(jwt);
            }
        }

        [HttpGet("GetMaxSalesInvoiceNumber")]
        public async Task<IActionResult> GetMaxSalesInvoiceNumber(DateTime dateTime)
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

            var datajson = await service.GetMaxSalesInvoiceNumber((int)user.employeeId, dateTime);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpGet("GetCurrentStock")]
        public async Task<IActionResult> GetCurrentStock(int storeId, int productWiseSpecificationId, string batchNo = null)
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

            var datajson = await service.GetCurrentStock(storeId, productWiseSpecificationId, batchNo);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpGet("GetProductBatch")]
        public async Task<IActionResult> GetProductBatch(int storeId, int productWiseSpecificationId)
        {
            var AuthModel = await authenticator.AuthenticationStatus(Request.Headers["auth_token"]);
            if (AuthModel.IsAuthorized == false) return new OkObjectResult(AuthModel.Jwts);

            var datajson = await service.GetProductBatch(storeId, productWiseSpecificationId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpGet("SetCurrentStock")]
        public async Task<IActionResult> SetCurrentStock(int storeId, string productCode, decimal ProposedStockQty, string batchNo = null)
        {
            //var AuthModel = await authenticator.AuthenticationStatus(Request.Headers["auth_token"]);
            //if (AuthModel.IsAuthorized == false) return new OkObjectResult(AuthModel.Jwts);

            var datajson = await service.SetCurrentStock(storeId, productCode, ProposedStockQty, batchNo);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpGet("GetItemWsieBonus")]
        public async Task<IActionResult> GetItemWsieBonus(int? partyId, int? productWiseSpecificationId, DateTime? invoiceDate, decimal? invQty)
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

            var datajson = await service.GetItemWsieBonus(partyId, productWiseSpecificationId, invoiceDate, invQty);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpGet("GetCollectionDiscountNotApplicableProductList")]
        public async Task<IActionResult> GetCollectionDiscountNotApplicableProductList(int? partyId)
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

            var datajson = await service.GetCollectionDiscountNotApplicableProductList(user.employeeId, partyId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpGet("getProductSerialNoByProductSpec")]
        public async Task<IActionResult> getProductSerialNoByProductSpec(int productWiseSpecificationId)
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

            var datajson = await service.GetProductSerialNoByProductSpec(productWiseSpecificationId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpGet("GetSalesInvoiceAmountById")]
        public async Task<IActionResult> GetSalesInvoiceAmountById(int salesInvoiceId)
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

            var datajson = await service.GetSalesInvoiceAmountById(salesInvoiceId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpGet("GetSalesInvoiceByPartyId")]
        public async Task<IActionResult> GetSalesInvoiceByPartyId(int partyId)
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

            var datajson = await service.GetSalesInvoiceByPartyId(partyId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }


        [HttpGet("GetTargetVsAchievementReport")]
        public async Task<IActionResult> GetTargetVsAchievementReport(string depotCode, string territoryCode, DateTime fromDate, DateTime toDate)
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

            var datajson = await service.GetTargetVsAchievementReport(user.employeeId, depotCode, territoryCode, fromDate, toDate);
            var jwt = await Tokens.GetJwt(datajson.data);
            return new OkObjectResult(jwt);
        }



        [HttpGet("GetMoneyReceiptType")]
        public async Task<IActionResult> GetMoneyReceiptType()
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

            var datajson = await service.GetMoneyReceiptType(user.employeeId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpGet("GetMaxMoneyReceiptNo")]
        public async Task<IActionResult> GetMaxMoneyReceiptNo(DateTime dateTime)
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

            var datajson = await service.GetMaxMoneyReceiptNo(user.employeeId, dateTime);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpGet("GetAllMoneyReceiptNote")]
        public async Task<IActionResult> GetAllMoneyReceiptNote(int? masterId, DateTime? fdate, DateTime? tdate)
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

            var datajson = await service.GetAllMoneyReceiptNote(user.employeeId, masterId, fdate, tdate);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }
        [HttpGet("GetAllMoneyReceipt")]
        public async Task<IActionResult> GetAllMoneyReceipt(int? masterId, DateTime? fdate, DateTime? tdate)
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

            var datajson = await service.GetAllMoneyReceipt(user.employeeId, masterId, fdate, tdate);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }
        [HttpGet("GetAllMoneyReceiptDetails")]
        public async Task<IActionResult> GetAllMoneyReceiptDetails(int? masterId)
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

            var datajson = await service.GetAllMoneyReceiptDetails(masterId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpGet("GetAllPendingMoneyRecipts")]
        public async Task<IActionResult> GetAllPendingMoneyRecipts(string territoryCode, string mioCode)
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

            var datajson = await service.GetAllPendingMoneyRecipts(user.employeeId, territoryCode, mioCode);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }
        [HttpGet("GetAllPendingMoneyReciptsNew")]
        public async Task<IActionResult> GetAllPendingMoneyReciptsNew()
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

            var datajson = await service.GetAllPendingMoneyReciptsNew();
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }
        [HttpGet("GetAllPendingMoneyReciptsForBill")]
        public async Task<IActionResult> GetAllPendingMoneyReciptsForBill()
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

            var datajson = await service.GetAllPendingMoneyReciptsForBill(user.employeeId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }
        [HttpPost("SaveMoneyReceiptNote")]
        public async Task<IActionResult> SaveMoneyReceiptNote([FromBody] MoneyReceiptNoteViewModel model)
        {
            var AuthModel = await authenticator.AuthenticationStatus(Request.Headers["auth_token"]);
            if (AuthModel.IsAuthorized == false) return new OkObjectResult(AuthModel.Jwts);
            var employeeId = AuthModel.ApplicationUserInfo.employeeId;

            //if (Authentication().Result == false) return new OkObjectResult(jwts);

            var res = await service.ValidateMoneyReceiptNoteTrxnNo(employeeId, model.trxNo);

            if (!string.IsNullOrEmpty(res.data) && res.data != "Success")
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, res.data, false);
                return new OkObjectResult(jwt);
            }

            int masterId = await service.SaveMoneyReceiptNote(employeeId.ToString(), model);

            if (masterId != 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Money Receipt Note has created successfully.", true, masterId);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Money Receipt Note has not created.", false);
                return new OkObjectResult(jwt);
            }
        }
        [HttpPost("SaveMoneyReceipt")]
        public async Task<IActionResult> SaveMoneyReceipt([FromBody] MoneyReceiptViewModel model)
        {
            var AuthModel = await authenticator.AuthenticationStatus(Request.Headers["auth_token"]);
            if (AuthModel.IsAuthorized == false) return new OkObjectResult(AuthModel.Jwts);
            var employeeId = AuthModel.ApplicationUserInfo.employeeId.ToString();

            int masterId = await service.SaveMoneyReceipt(employeeId, model);

            if (masterId == 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Money Receipt has not created.", false);
                return new OkObjectResult(jwt);
            }
            int result = 0;
            int rs = await service.DeleteMoneyReceiptDetails(masterId);
            result = await service.SaveMoneyReceiptDetails(employeeId.ToString(), model.lstDetailsViewModel, masterId);
            if (result != 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Money Receipt Details has created successfully.", true);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Money Receipt Details has not created.", false);
                return new OkObjectResult(jwt);
            }
        }

        [HttpPost("SaveSalesInvoiceGDNConfirmation")]
        public async Task<IActionResult> SaveSalesInvoiceGDNConfirmation([FromBody] InvoiceSalesGDNConfirmationViewModel model)
        {
            var AuthModel = await authenticator.AuthenticationStatus(Request.Headers["auth_token"]);
            if (AuthModel.IsAuthorized == false) return new OkObjectResult(AuthModel.Jwts);
            var employeeId = AuthModel.ApplicationUserInfo.employeeId.ToString();

            int masterId = await service.SaveInvoiceGDNConfirmation(employeeId, model.Ids, model.gdnType);
            if (masterId != 0)
            {
                int log = await service.SaveGDNConfirmationLogs(employeeId, model.Ids);
            }

            if (masterId != 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "GDN  Confirmation Updated Suceessfully.", true, masterId);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "GDN  Confirmation Updated not Suceessfully.", false);
                return new OkObjectResult(jwt);
            }
        }

        [HttpPost("SetDamageExpireProductsReturn")]
        public async Task<IActionResult> SetDamageExpireProductsReturn([FromBody] DamageExpireProductsReturnViewModel model)
        {
            var AuthModel = await authenticator.AuthenticationStatus(Request.Headers["auth_token"]);
            if (AuthModel.IsAuthorized == false) return new OkObjectResult(AuthModel.Jwts);
            var empId = AuthModel.ApplicationUserInfo.employeeId.ToString();

            if (model.lstMasterViewModel.Count() == 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Damage Expire Return Product Not Found.", false);
                return new OkObjectResult(jwt);
            }
            int masterId = await service.SaveDamageExpireProductsReturn(empId, (int)model.damageExpireProductReturnMasterId, model.typeId, model.startDate, model.MarketOrDepo);

            if (masterId > 0)
            {
                foreach (var el in model.lstMasterViewModel)
                {
                    if (el.isSelect) await service.SaveDamageExpireProductsReturnDetails(empId, masterId, el.miscellaneousItemDetailsId, el.qty, el.productSpecificationId);
                }
            }

            if (masterId > 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Damage Expire Return has save successfully.", true);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Damage Expire Return has not Saved.", false);
                return new OkObjectResult(jwt);
            }
        }

        [HttpPost("DeleteMoneyReceiptNoteById")]
        public async Task<IActionResult> DeleteMoneyReceiptNoteById([FromBody] int masterId)
        {
            var AuthModel = await authenticator.AuthenticationStatus(Request.Headers["auth_token"]);
            if (AuthModel.IsAuthorized == false) return new OkObjectResult(AuthModel.Jwts);
            var employeeId = AuthModel.ApplicationUserInfo.employeeId;

            bool result = await service.DeleteMoneyReceiptNoteById(employeeId, masterId);

            if (result == true)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Money Receipt Note has deleted successfully.", true);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Money Receipt Note has not deleted.", false);
                return new OkObjectResult(jwt);
            }
        }
        [HttpPost("DeleteMoneyReceiptById")]
        public async Task<IActionResult> DeleteMoneyReceiptById([FromBody] int masterId)
        {
            var AuthModel = await authenticator.AuthenticationStatus(Request.Headers["auth_token"]);
            if (AuthModel.IsAuthorized == false) return new OkObjectResult(AuthModel.Jwts);
            var employeeId = AuthModel.ApplicationUserInfo.employeeId;

            bool result = await service.DeleteMoneyReceiptById(employeeId, masterId);

            if (result == true)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Money Receipt Note has deleted successfully.", true);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Money Receipt Note has not deleted.", false);
                return new OkObjectResult(jwt);
            }
        }



        #endregion

        #region Tender Quotation
        [HttpPost("SaveTenderQuotation")]
        public async Task<IActionResult> SaveTenderQuotation([FromBody] TenderQuotationViewModel model)
        {
            var AuthModel = await authenticator.AuthenticationStatus(Request.Headers["auth_token"]);
            if (AuthModel.IsAuthorized == false) return new OkObjectResult(AuthModel.Jwts);
            var employeeId = AuthModel.ApplicationUserInfo.employeeId.ToString();
            
            if (model == null || model.lstDetailsViewModel == null || model.lstDetailsViewModel.Count() == 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "No item found to save.", false);
                return new OkObjectResult(jwt);
            }

            int result = 0;
            int quotationMasterId = await service.SaveTenderQuotation(employeeId, model);

            if (quotationMasterId == 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Tender Quotation has not created.", false);
                return new OkObjectResult(jwt);
            }
            result = await service.SaveTenderQuotationDetails(employeeId, model.lstDetailsViewModel, quotationMasterId);


            if (result != 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Tender Quotation Details has created successfully.", true, quotationMasterId);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Tender Quotation Details has not created.", false);
                return new OkObjectResult(jwt);
            }
        }
        [HttpGet("GetTenderQuotationId")]
        public async Task<IActionResult> GetTenderQuotationId(int? quotationMasterId, DateTime? fDate, DateTime? tDate)
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

            var datajson = await service.GetTenderQuotationId(quotationMasterId, user.employeeId, fDate, tDate);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpGet("GetTenderQuotationDetailsById")]
        public async Task<IActionResult> GetTenderQuotationDetailsById(int? quotationMasterId)
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

            var datajson = await service.GetTenderQuotationDetailsById(quotationMasterId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpPost("DeleteTenderQuotationById")]
        public async Task<IActionResult> DeleteTenderQuotationById([FromBody] int quotationMasterId)
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

            if (quotationMasterId <= 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Quotation has not deleted.", false);
                return new OkObjectResult(jwt);
            }
            bool result = await service.DeleteTenderQuotationById(user.employeeId.ToString(), quotationMasterId);

            if (result == true)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Quotation has deleted successfully.", true);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Quotation has not deleted.", false);
                return new OkObjectResult(jwt);
            }
        }
        #endregion

        #region  Tender Quotation Approval
        [HttpGet("GetALLTenderQuotationApproval")]
        public async Task<IActionResult> GetALLTenderQuotationApproval(int? isApproved)
        {
            var AuthModel = await authenticator.AuthenticationStatus(Request.Headers["auth_token"]);
            if (AuthModel.IsAuthorized == false) return new OkObjectResult(AuthModel.Jwts);
            var employeeId = AuthModel.ApplicationUserInfo.employeeId;

            var datajson = await service.GetALLTenderQuotationApproval(employeeId, isApproved);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }
        [HttpPost("SaveTenderQuotationApproval")]
        public async Task<IActionResult> SaveTenderQuotationApproval([FromBody] TenderQuotationApprovalViewModel models)
        {
            var AuthModel = await authenticator.AuthenticationStatus(Request.Headers["auth_token"]);
            if (AuthModel.IsAuthorized == false) return new OkObjectResult(AuthModel.Jwts);
            var employeeId = AuthModel.ApplicationUserInfo.employeeId;

            if (models == null || models.lstMasterViewModel == null || models.lstMasterViewModel.Count() == 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "No Data Found To Save.", false);
                return new OkObjectResult(jwt);
            }

            int result = await service.SaveTenderQuotationApproval(employeeId, models);


            if (result > 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Approved successfully.", true);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Approval Failed.", false);
                return new OkObjectResult(jwt);
            }
        }
        #endregion Tender Quotation Approval

        #region Tender Challan
        [HttpPost("SaveTenderChallan")]
        public async Task<IActionResult> SaveTenderChallan([FromBody] TenderChallanViewModel model)
        {
            var AuthModel = await authenticator.AuthenticationStatus(Request.Headers["auth_token"]);
            if (AuthModel.IsAuthorized == false) return new OkObjectResult(AuthModel.Jwts);
            var employeeId = AuthModel.ApplicationUserInfo.employeeId.ToString();

            if (model == null || model.lstDetailsViewModel == null || model.lstDetailsViewModel.Count() == 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "No item found to save.", false);
                return new OkObjectResult(jwt);
            }

            int result = 0;
            int result2 = 0;
            int challanMasterId = await service.SaveTenderChallan(employeeId, model);

            if (challanMasterId == 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Tender Challan has not created.", false);
                return new OkObjectResult(jwt);
            }

            result = await service.SaveTenderChallanDetails(employeeId, model.lstDetailsViewModel, challanMasterId);

            if(model.finalChallanDetailsViewModel!=null && model.isFinal==true)
            {
                result2 = await service.SaveTenderFinalChallanDetails(employeeId, model.finalChallanDetailsViewModel, challanMasterId);
            }
            


            if (result != 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Tender Challan Details has created successfully.", true, challanMasterId);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Tender Challan Details has not created.", false);
                return new OkObjectResult(jwt);
            }
        }

        [HttpPost("SaveTenderBill")]
        public async Task<IActionResult> SaveTenderBill([FromBody] TenderBillViewModel model)
        {
            var AuthModel = await authenticator.AuthenticationStatus(Request.Headers["auth_token"]);
            if (AuthModel.IsAuthorized == false) return new OkObjectResult(AuthModel.Jwts);
            var employeeId = AuthModel.ApplicationUserInfo.employeeId.ToString();

            if (model == null || model.lstDetailsViewModel == null || model.lstDetailsViewModel.Count() == 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "No item found to save.", false);
                return new OkObjectResult(jwt);
            }

            int result = 0;
            int billMasterId = await service.SaveTenderBill(employeeId, model);

            if (billMasterId == 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Tender Bill has not created.", false);
                return new OkObjectResult(jwt);
            }

            result = await service.SaveTenderBillDetails(employeeId, model.lstDetailsViewModel, billMasterId);

            if (result != 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Tender Bill Details has created successfully.", true, billMasterId);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Tender Bill Details has not created.", false);
                return new OkObjectResult(jwt);
            }
        }
        [HttpGet("GetTenderChallanById")]
        public async Task<IActionResult> GetTenderChallanById(int? challanMasterId, DateTime? fDate, DateTime? tDate)
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

            var datajson = await service.GetTenderChallanById(challanMasterId, user.employeeId, fDate, tDate);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpGet("GetTenderChallanWihoutQuotationById")]
        public async Task<IActionResult> GetTenderChallanWihoutQuotationById(int? challanMasterId, DateTime? fDate, DateTime? tDate)
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

            var datajson = await service.GetTenderChallanWihoutQuotationById(challanMasterId, user.employeeId, fDate, tDate);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpGet("GetTenderBillById")]
        public async Task<IActionResult> GetTenderBillById(int? billMasterId, DateTime? fDate, DateTime? tDate)
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

            var datajson = await service.GetTenderBillById(billMasterId, user.employeeId, fDate, tDate);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }
        [HttpGet("GetQuotationForChallan")]
        public async Task<IActionResult> GetQuotationForChallan(int? partyId)
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

            var datajson = await service.GetQuotationForChallan(user.employeeId, partyId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }
        [HttpGet("GetTenderQuotationDetailsForChallanById")]
        public async Task<IActionResult> GetTenderQuotationDetailsForChallanById(int? quotationMasterId)
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

            var datajson = await service.GetTenderQuotationDetailsForChallanById(quotationMasterId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }
        [HttpGet("GetChallanForBill")]
        public async Task<IActionResult> GetChallanForBill(int? partyId)
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

            var datajson = await service.GetChallanForBill(user.employeeId, partyId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }
        [HttpGet("GetChallanDetailsForBillById")]
        public async Task<IActionResult> GetChallanDetailsForBillById(int? challanMasterId)
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

            var datajson = await service.GetChallanDetailsForBillById(challanMasterId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }
        [HttpGet("GetTenderChallanDetailsForFinalChallanByQuotationMasterId")]
        public async Task<IActionResult> GetTenderChallanDetailsForFinalChallanByQuotationMasterId(int? quotationMasterId)
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

            var datajson = await service.GetTenderChallanDetailsForFinalChallanByQuotationMasterId(quotationMasterId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }
        #endregion

        #region Sales Invoice Details

        [HttpGet("GetSalesInvoiceDetailsByMasterId")]
        public async Task<IActionResult> GetSalesInvoiceDetailsByMasterId(int? salesInvoiceId)
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

            var datajson = await service.GetSalesInvoiceDetailsByMasterId(salesInvoiceId);
            var jwt = await Tokens.getDataWithStatus(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpGet("GetProductSpecDetailsBySpecId")]
        public async Task<IActionResult> GetProductSpecDetailsBySpecId(int? productSpecId)
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

            var datajson = await service.GetProductSpecDetailsBySpecId(productSpecId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpGet("GetAllPartysByTypeId")]
        public async Task<IActionResult> GetAllPartysByTypeId(int? partyTypeId, int? sbuId, string territoryCode)
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

            var datajson = await service.GetAllPartysByTypeId((int)user.employeeId, partyTypeId, sbuId, territoryCode);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpGet("GetAllActivePartysByTypeId")]
        public async Task<IActionResult> GetAllActivePartysByTypeId(int? partyTypeId, int? sbuId, string territoryCode)
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

            var datajson = await service.GetAllActivePartysByTypeId((int)user.employeeId, partyTypeId, sbuId, territoryCode);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }
        [HttpGet("GetAllActivePartysForChallanByTypeId")]
        public async Task<IActionResult> GetAllActivePartysForChallanByTypeId(int? partyTypeId, int? sbuId, string territoryCode)
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

            var datajson = await service.GetAllActivePartysForChallanByTypeId((int)user.employeeId, partyTypeId, sbuId, territoryCode);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpGet("GetAllActivePartysForBillByTypeId")]
        public async Task<IActionResult> GetAllActivePartysForBillByTypeId(int? partyTypeId, int? sbuId, string territoryCode)
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

            var datajson = await service.GetAllActivePartysForBillByTypeId((int)user.employeeId, partyTypeId, sbuId, territoryCode);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpGet("GetAllMIOByTerritory")]
        public async Task<IActionResult> GetAllMIOByTerritory(string territoryCode)
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

            var datajson = await service.GetAllMIOByTerritory(user.employeeId, territoryCode);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }


        [HttpGet("GetPartyDetailsById")]
        public async Task<IActionResult> GetPartyDetailsById(int? partyId)
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

            var datajson = await service.GetPartyDetailsById(partyId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpPost("DeleteSalesInvoiceDetailsById")]
        public async Task<IActionResult> DeleteSalesInvoiceDetailsById([FromBody] int salesInvDetailsId)
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

            bool result = await service.DeleteSalesInvoiceDetailsById(user.employeeId.ToString(), salesInvDetailsId);

            if (result == true)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Sales Invoice Details has deleted successfully.", true);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Sales Invoice Details has not deleted.", false);
                return new OkObjectResult(jwt);
            }
        }

        [HttpGet("GetSalesDashboardChartData")]
        public async Task<IActionResult> GetSalesDashboardChartData()
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

            var datajson = await service.GetSalesDashboardChartData(user.employeeId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpGet("GetSalesDashboardDueChartData")]
        public async Task<IActionResult> GetSalesDashboardDueChartData()
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

            var datajson = await service.GetSalesDashboardDueChartData(user.employeeId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpGet("GetSalesDashboardDataDetails")]
        public async Task<IActionResult> GetSalesDashboardDataDetails(DateTime? fromDate, DateTime? toDate, int type, int partyId)
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

            var datajson = await service.GetSalesDashboardDataDetails(fromDate, toDate, (int)user.employeeId, type, partyId);  //user.employeeId.ToString());
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpGet("GetSalesDashboardDataDetailsPartyWise")]
        public async Task<IActionResult> GetSalesDashboardDataDetailsPartyWise(DateTime? fromDate, DateTime? toDate, int type)
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

            var datajson = await service.GetSalesDashboardDataDetailsPartyWise(fromDate, toDate, (int)user.employeeId, type);  //user.employeeId.ToString());
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }



        [HttpGet("GetSalesDashboardData")]
        public async Task<IActionResult> GetSalesDashboardData(DateTime? fromDate, DateTime? toDate)
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

            var datajson = await service.GetSalesDashboardData(fromDate, toDate, user.employeeId.ToString());  //user.employeeId.ToString());
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
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




        [HttpGet("GetCustomerDuesStatus")]
        public async Task<IActionResult> GetCustomerDuesStatus(int partyId, string territoryCode)
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

            var datajson = await service.GetCustomerDuesStatus(user.employeeId, partyId, territoryCode);  //user.employeeId.ToString());
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }







        #endregion

        #region Sales Invoice and Details for APP       

        [HttpGet("GetSalesOrderByChemist")]
        public async Task<IActionResult> GetSalesOrderByChemist(int? chemistId, int? statusId, DateTime fromDate, DateTime toDate)
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
            var datajson = await service.GetSalesOrderByChemist(chemistId, statusId, Convert.ToDateTime(fromDate).ToString("yyyyMMdd"), Convert.ToDateTime(toDate).ToString("yyyyMMdd"), (int)user.employeeId);
            var jwt = await Tokens.getDataWithStatus(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpGet("GetSalesOrderByAdminForApprove")]
        public async Task<IActionResult> GetSalesOrderByAdminForApprove(DateTime fromDate, DateTime toDate, int? statusId)
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

            var datajson = await service.GetSalesOrderByAdminForApprove(user.employeeId, statusId, Convert.ToDateTime(fromDate).ToString("yyyyMMdd"), Convert.ToDateTime(toDate).ToString("yyyyMMdd"));
            if (datajson.data != "[]")
            {
                var jwt = await Tokens.getDataWithStatusAndMessage(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented }, "Data found successfully.");
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.getDataWithStatusAndMessage(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented }, "No data found");
                return new OkObjectResult(jwt);
            }
        }

        [HttpPost("ApproveSalesOrderStatusByAdmin")]
        public async Task<IActionResult> ApproveSalesOrderStatusByAdmin([FromBody] SalesInvoiceApproveViewModel model)
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

            if (model.data.Count() == 0)
            {
                var jwt = await Tokens.setJwtWithStatus(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Sales Order Not Found.", false);
                return new OkObjectResult(jwt);
            }
            int result = await service.ApproveSalesOrderStatusByAdmin(user.employeeId.ToString(), model.data);

            if (result > 0)
            {
                var jwt = await Tokens.setJwtWithStatus(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Sales Order has updated successfully.", true);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwtWithStatus(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Sales Order has not updated.", false);
                return new OkObjectResult(jwt);
            }
        }

        [HttpGet("GetSalesOrderDtlByInvIdForApp")]
        public async Task<IActionResult> GetSalesOrderDtlByInvIdForApp(int? salesInvoiceId)
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

            var datajson = await service.GetSalesOrderDtlByInvIdForApp(salesInvoiceId);
            var jwt = await Tokens.getDataWithStatus(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpPost("UpdateSalesOrderStatus")]
        public async Task<IActionResult> UpdateSalesOrderStatus([FromBody] SalesOrderStatusUpdateViewModel model)
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

            if (model.salesInvoiceId <= 0)
            {
                var jwt = await Tokens.setJwtWithStatus(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Sales Invoice not found.", false);
                return new OkObjectResult(jwt);
            }
            bool result = await service.UpdateSalesOrderStatusForApp(user.employeeId.ToString(), model.salesInvoiceId, model.statusId);

            string statusMessage = "";
            if (model.statusId == 0)
            {
                statusMessage = "pending";
            }
            else if (model.statusId == 1)
            {
                statusMessage = "approved";
            }
            else if (model.statusId == 2)
            {
                statusMessage = "cancelled";
            }
            else if (model.statusId == 3)
            {
                statusMessage = "shipped";
            }
            else if (model.statusId == 4)
            {
                statusMessage = "received";
            }
            else if (model.statusId == 5)
            {
                statusMessage = "onhold";
            }
            else if (model.statusId == 6)
            {
                statusMessage = "refunded";
            }

            if (result == true)
            {
                var jwt = await Tokens.setJwtWithStatus(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Sales order has " + statusMessage + " successfully.", true);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwtWithStatus(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Sales order has not " + statusMessage + ".", false);
                return new OkObjectResult(jwt);
            }
        }

        #endregion

        #region T&C

        [HttpGet("GetSalesInvoiceTCByMasterId")]
        public async Task<IActionResult> GetSalesInvoiceTCByMasterId(int? salesInvoiceId)
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

            var datajson = await service.GetSalesInvoiceTCByMasterId(salesInvoiceId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpPost("DeleteSalesInvoiceTCById")]
        public async Task<IActionResult> DeleteSalesInvoiceTCById(int? salesInvoiceTCId, bool? isSelect)
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

            bool result = await service.DeleteSalesInvoiceTCById(user.employeeId.ToString(), salesInvoiceTCId, isSelect);

            if (result == true)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Sales Invoice Details has deleted successfully.", true);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Sales Invoice Details has not deleted.", false);
                return new OkObjectResult(jwt);
            }
        }
        #endregion

        #region Reports

        [HttpGet("GetDateRangeWiseUserName")]
        public async Task<IActionResult> GetDateRangeWiseUserName(int? salesInvoiceId, int? partyId, DateTime? fromDate, DateTime? toDate, string salesUserId)
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

            var datajson = await service.GetDateRangeWiseUserName(fromDate, toDate, (int)user.employeeId);  //user.employeeId.ToString());
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }
        [HttpGet("GetSalesInvoiceReportData")]
        public async Task<IActionResult> GetSalesInvoiceReportData(int? salesInvoiceId, int? partyId, DateTime? fromDate, DateTime? toDate, string salesUserId)
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

            var datajson = await service.GetSalesInvoiceReportData(salesInvoiceId, partyId, fromDate, toDate, salesUserId);  //user.employeeId.ToString());
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }
        [HttpGet("GetAddressForReportFooter")]
        public async Task<IActionResult> GetAddressForReportFooter(int? companyId)
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

            var datajson = await service.GetAddressForReportFooter(companyId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpGet("GetSalesInvoiceReportDataById")]
        public async Task<IActionResult> GetSalesInvoiceReportDataById(int? salesInvoiceId)//int? partyId, DateTime? fromDate, DateTime? toDate, )
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

            var datajson = await service.GetSalesInvoiceReportDataById(salesInvoiceId);//, partyId, fromDate, toDate, user.employeeId.ToString());
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpGet("GetSalesReportByInvId")]
        public async Task<IActionResult> GetSalesReportByInvId(int? salesInvoiceId)
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

            var datajson = await service.GetSalesReportByInvId(salesInvoiceId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpGet("GetInvoiceListByPartyId")]
        public async Task<IActionResult> GetInvoiceListByPartyId(int? partyId, DateTime? fDate, DateTime? tDate)
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

            var datajson = await service.GetSalesInvoiceListByPartyId(partyId, fDate, tDate);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }
        [HttpGet("GetSalesInvoiceSearchResult")]
        public async Task<IActionResult> GetSalesInvoiceSearchResult(string SearchingText, DateTime? FromDate, DateTime? ToDate)
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

            var datajson = await service.GetSalesInvoiceSearchResult((string.IsNullOrWhiteSpace(SearchingText) ? "" : SearchingText), FromDate, ToDate);
            var jwt = await Tokens.GetJwt(datajson.data);
            return new OkObjectResult(jwt);
        }


        [HttpGet("GetSaleRegisterReport")]
        public async Task<IActionResult> GetSaleRegisterReport(string depoCode, string territoryCode, int? partyId, DateTime? fDate, DateTime? tDate, string zoneCode, string regionCode, string areaCode)
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

            var datajson = await service.GetSaleRegisterReport(user.employeeId, depoCode, territoryCode, partyId, fDate, tDate, zoneCode, regionCode, areaCode);
            var jwt = await Tokens.GetJwt(datajson.data);
            return new OkObjectResult(jwt);
        }

        [HttpGet("GetSaleRegisterReportForBill")]
        public async Task<IActionResult> GetSaleRegisterReportForBill(int? partyId, DateTime? fDate, DateTime? tDate)
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

            var datajson = await service.GetSaleRegisterReportForBill(user.employeeId, partyId, fDate, tDate);
            var jwt = await Tokens.GetJwt(datajson.data);
            return new OkObjectResult(jwt);
        }

        [HttpGet("GetMioProductSalesReport")]
        public async Task<IActionResult> GetMioProductSalesReport(string depotCode, string territoryCode, DateTime fromDate, DateTime toDate, string zoneCode, string regionCode, string areaCode, int? partyId, int? productWiseSpecificationId)
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

            if (user == null || user.token != uid)
            {
                bool status = false;
                string actionresult = "Invalid Token.";
                var jwts = await Tokens.changePasswordJwt(status, actionresult, new JsonSerializerSettings { Formatting = Formatting.Indented });
                return new OkObjectResult(jwts);
            }


            #endregion

            var datajson = await service.GetMioProductSalesReport(user.employeeId, depotCode, territoryCode, fromDate, toDate, zoneCode, regionCode, areaCode, partyId, productWiseSpecificationId);
            var jwt = await Tokens.GetJwt(datajson.data);
            return new OkObjectResult(jwt);
        }

        [HttpGet("GetZoneRegionWiseSalesCollectionBalanceReport")]
        public async Task<IActionResult> GetZoneRegionWiseSalesCollectionBalanceReport(string zoneCode, string regionCode, string areaCode, string territoryCode, DateTime fDate, DateTime tDate, string type, string mioType)
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

            var datajson = await service.GetZoneRegionWiseSalesCollectionBalanceReport(user.employeeId, zoneCode, regionCode, areaCode, territoryCode, fDate, tDate, type, mioType);
            var jwt = await Tokens.GetJwt(datajson.data);
            return new OkObjectResult(jwt);
        }

        [HttpGet("GetWeeklyProductMonitorReport")]
        public async Task<IActionResult> GetWeeklyProductMonitorReport(DateTime? fDate, DateTime? tDate, string zoneCode, string regionCode, string areaCode, string depotCode, string territoryCode, string empCode)
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

            if (user == null || user.token != uid)
            {
                bool status = false;
                string actionresult = "Invalid Token.";
                var jwts = await Tokens.changePasswordJwt(status, actionresult, new JsonSerializerSettings { Formatting = Formatting.Indented });
                return new OkObjectResult(jwts);
            }


            #endregion

            var datajson = await service.GetWeeklyProductMonitorReport(user.employeeId, fDate, tDate, zoneCode, regionCode, areaCode, depotCode, territoryCode, empCode);
            var jwt = await Tokens.GetJwt(datajson.data);
            return new OkObjectResult(jwt);
        }

        [HttpGet("GetNationalProductSalesReport")]
        public async Task<IActionResult> GetNationalProductSalesReport(int? userId, DateTime? fDate, DateTime? tDate, string depoCode, string territoryCode, int? partyId)
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

            var datajson = await service.GetNationalProductSalesReport(user.employeeId, fDate, tDate, depoCode, territoryCode, partyId);
            var jwt = await Tokens.GetJwt(datajson.data);
            return new OkObjectResult(jwt);
        }
        [ResponseCache(Duration = 300)]
        [HttpGet("GetZone")]
        public async Task<IActionResult> GetZone()
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

            var datajson = await service.GetZone(user.employeeId);
            var jwt = await Tokens.GetJwt(datajson.data);
            return new OkObjectResult(jwt);
        }

        [HttpGet("GetRegion")]
        public async Task<IActionResult> GetRegion(string zoneCode)
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

            var datajson = await service.GetRegion(user.employeeId, zoneCode);
            var jwt = await Tokens.GetJwt(datajson.data);
            return new OkObjectResult(jwt);
        }

        [HttpGet("GetArea")]
        public async Task<IActionResult> GetArea(string regionCode)
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

            if (user == null || user.token != uid)
            {
                bool status = false;
                string actionresult = "Invalid Token.";
                var jwts = await Tokens.changePasswordJwt(status, actionresult, new JsonSerializerSettings { Formatting = Formatting.Indented });
                return new OkObjectResult(jwts);
            }


            #endregion

            var datajson = await service.GetArea(user.employeeId, regionCode);
            var jwt = await Tokens.GetJwt(datajson.data);
            return new OkObjectResult(jwt);
        }
        [HttpGet("GetAreaForNationalSalesReport")]
        public async Task<IActionResult> GetAreaForNationalSalesReport(string regionCode)
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

            if (user == null || user.token != uid)
            {
                bool status = false;
                string actionresult = "Invalid Token.";
                var jwts = await Tokens.changePasswordJwt(status, actionresult, new JsonSerializerSettings { Formatting = Formatting.Indented });
                return new OkObjectResult(jwts);
            }


            #endregion

            var datajson = await service.GetAreaForNationalSalesReport(user.employeeId, regionCode);
            var jwt = await Tokens.GetJwt(datajson.data);
            return new OkObjectResult(jwt);
        }

        [HttpGet("GetTerritory")]
        public async Task<IActionResult> GetTerritory(string areaCode)
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

            if (user == null || user.token != uid)
            {
                bool status = false;
                string actionresult = "Invalid Token.";
                var jwts = await Tokens.changePasswordJwt(status, actionresult, new JsonSerializerSettings { Formatting = Formatting.Indented });
                return new OkObjectResult(jwts);
            }


            #endregion

            var datajson = await service.GetTerritory(user.employeeId, areaCode);
            var jwt = await Tokens.GetJwt(datajson.data);
            return new OkObjectResult(jwt);
        }


        [HttpGet("GetProductWiseNationalSalesReport")]
        public async Task<IActionResult> GetProductWiseNationalSalesReport(int? userId, DateTime? fDate, DateTime? tDate, string depoCode, string territoryCode, int? partyId)
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

            var datajson = await service.GetProductWiseNationalSalesReport(user.employeeId, fDate, tDate, depoCode, territoryCode, partyId);
            var jwt = await Tokens.GetJwt(datajson.data);
            return new OkObjectResult(jwt);
        }


        [HttpGet("GetUsersByUserName")]
        public async Task<IActionResult> GetUsersByUserName()
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

            if (user == null || user.token != uid)
            {
                bool status = false;
                string actionresult = "Invalid Token.";
                var jwts = await Tokens.changePasswordJwt(status, actionresult, new JsonSerializerSettings { Formatting = Formatting.Indented });
                return new OkObjectResult(jwts);
            }


            #endregion

            var datajson = await userInfoes.GetUsersByUserName(user.UserName);
            var jwt = await Tokens.GetJwt(datajson.data);
            return new OkObjectResult(jwt);
        }



        #endregion

        #region Approval


        [HttpPost("ApproveSalesInvoiceMaster")]
        public async Task<IActionResult> ApproveSalesInvoiceMaster([FromBody] SalesInvoiceViewModel model)
        {
            var AuthModel = await authenticator.AuthenticationStatus(Request.Headers["auth_token"]);
            if (AuthModel.IsAuthorized == false) return new OkObjectResult(AuthModel.Jwts);

            #region common

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

            //if (user.token != uid && user != null)
            //{
            //    bool status = false;
            //    string actionresult = "Invalid Token.";
            //    var jwts = await Tokens.changePasswordJwt(status, actionresult, new JsonSerializerSettings { Formatting = Formatting.Indented });
            //    return new OkObjectResult(jwts);
            //}


            #endregion

            if (model.lstMasterViewModel.Count() == 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Sales invoice Not Found.", false);
                return new OkObjectResult(jwt);
            }
            int result = await service.ApproveSalesInvoiceMaster(AuthModel.ApplicationUserInfo.employeeId.ToString(), model.approvalStatus, model.lstMasterViewModel);

            if (result > 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Sales invoice has Approved successfully.", true);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Sales invoice has not Approved.", false);
                return new OkObjectResult(jwt);
            }
        }


        [HttpGet("GetSalesInvoiceMasterListForApproval")]
        public async Task<IActionResult> GetSalesInvoiceMasterListForApproval(int salesInvoiceId)
        {
            var AuthModel = await authenticator.AuthenticationStatus(Request.Headers["auth_token"]);
            if (AuthModel.IsAuthorized == false) return new OkObjectResult(AuthModel.Jwts);
            var employeeId = AuthModel.ApplicationUserInfo.employeeId.ToString();

            var datajson = await service.GetSalesInvoiceMasterListForApproval(employeeId, salesInvoiceId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }


        [HttpGet("GetSalesInvoiceMasterListByStatusJson")]
        public async Task<IActionResult> GetSalesInvoiceMasterListByStatusJson(int status, string territoryCode, int? transactionTypeId, string areaCode)
        {
            var AuthModel = await authenticator.AuthenticationStatus(Request.Headers["auth_token"]);
            if (AuthModel.IsAuthorized == false) return new OkObjectResult(AuthModel.Jwts);
            var employeeId = AuthModel.ApplicationUserInfo.employeeId.ToString();

            var datajson = await service.GetSalesInvoiceMasterListByStatus(employeeId, status, territoryCode, transactionTypeId, areaCode);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }


        [HttpGet("GetSalesInvoiceMasterListByStatusandTerritory")]
        public async Task<IActionResult> GetSalesInvoiceMasterListByStatusandTerritory(int status, string territoryCode)
        {
            var AuthModel = await authenticator.AuthenticationStatus(Request.Headers["auth_token"]);
            if (AuthModel.IsAuthorized == false) return new OkObjectResult(AuthModel.Jwts);
            var employeeId = AuthModel.ApplicationUserInfo.employeeId.ToString();

            var datajson = await service.GetSalesInvoiceMasterListByStatusandTerritory(employeeId, status, territoryCode);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }


        [HttpGet("GetSalesPickingMasterListJson")]
        public async Task<IActionResult> GetSalesPickingMasterListJson(int pikingMasterId)
        {
            var AuthModel = await authenticator.AuthenticationStatus(Request.Headers["auth_token"]);
            if (AuthModel.IsAuthorized == false) return new OkObjectResult(AuthModel.Jwts);
            var employeeId = AuthModel.ApplicationUserInfo.employeeId.ToString();

            var datajson = await service.GetSalesPickingMasterListJson(employeeId, pikingMasterId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }


        [HttpGet("GetMiscellaneousItemDepotListJson")]
        public async Task<IActionResult> GetMiscellaneousItemDepotListJson(int typeId)
        {
            var AuthModel = await authenticator.AuthenticationStatus(Request.Headers["auth_token"]);
            if (AuthModel.IsAuthorized == false) return new OkObjectResult(AuthModel.Jwts);
            var employeeId = AuthModel.ApplicationUserInfo.employeeId.ToString();

            var datajson = await service.GetMiscellaneousItemDepotListJson(employeeId, typeId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpGet("GetMiscellaneousItemMarketListJson")]
        public async Task<IActionResult> GetMiscellaneousItemMarketListJson()
        {
            var AuthModel = await authenticator.AuthenticationStatus(Request.Headers["auth_token"]);
            if (AuthModel.IsAuthorized == false) return new OkObjectResult(AuthModel.Jwts);
            var employeeId = AuthModel.ApplicationUserInfo.employeeId.ToString();

            var datajson = await service.GetMiscellaneousItemMarketListJson(employeeId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpGet("GetSalSpGetAllSalesDispatchByIdJson")]
        public async Task<IActionResult> GetSalSpGetAllSalesDispatchByIdJson(int masterId)
        {
            var AuthModel = await authenticator.AuthenticationStatus(Request.Headers["auth_token"]);
            if (AuthModel.IsAuthorized == false) return new OkObjectResult(AuthModel.Jwts);
            var employeeId = AuthModel.ApplicationUserInfo.employeeId.ToString();

            var datajson = await service.GetSalSpGetAllSalesDispatchByIdJson(employeeId, masterId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }


        [HttpGet("GetAllDamageExpireReturnByIdJson")]
        public async Task<IActionResult> GetAllDamageExpireReturnByIdJson(int masterId, string MarketOrDepo)
        {
            var AuthModel = await authenticator.AuthenticationStatus(Request.Headers["auth_token"]);
            if (AuthModel.IsAuthorized == false) return new OkObjectResult(AuthModel.Jwts);
            var employeeId = AuthModel.ApplicationUserInfo.employeeId.ToString();

            var datajson = await service.GetAllDamageExpireReturnByIdJson(employeeId, masterId, MarketOrDepo);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }


        [HttpGet("GetSalesPickingDetailByMasterIdJson")]
        public async Task<IActionResult> GetSalesPickingDetailByMasterIdJson(int pikingMasterId)
        {
            var AuthModel = await authenticator.AuthenticationStatus(Request.Headers["auth_token"]);
            if (AuthModel.IsAuthorized == false) return new OkObjectResult(AuthModel.Jwts);
            var employeeId = AuthModel.ApplicationUserInfo.employeeId.ToString();

            var datajson = await service.SalSpGetAllPickingDetailsByMasterIdJson(employeeId, pikingMasterId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }


        [HttpGet("GetSalesPickingSummaryByMasterIdJson")]
        public async Task<IActionResult> GetSalesPickingSummaryByMasterIdJson(int pikingMasterId)
        {
            var AuthModel = await authenticator.AuthenticationStatus(Request.Headers["auth_token"]);
            if (AuthModel.IsAuthorized == false) return new OkObjectResult(AuthModel.Jwts);
            var employeeId = AuthModel.ApplicationUserInfo.employeeId.ToString();

            var datajson = await service.GetSalesPickingSummaryByMasterIdJson(employeeId, pikingMasterId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }


        [HttpGet("GetSalesInvoiceListfromDispatchJson")]
        public async Task<IActionResult> GetSalesInvoiceListfromDispatchJson(int dispatchMasterId)
        {
            var AuthModel = await authenticator.AuthenticationStatus(Request.Headers["auth_token"]);
            if (AuthModel.IsAuthorized == false) return new OkObjectResult(AuthModel.Jwts);
            var employeeId = AuthModel.ApplicationUserInfo.employeeId.ToString();

            var datajson = await service.GetSalesInvoiceListfromDispatchJson(employeeId, dispatchMasterId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }
        [HttpGet("GetSalesInvoiceListfromDispatchJson_v2")]
        public async Task<IActionResult> GetSalesInvoiceListfromDispatchJson_v2(int dispatchMasterId, int? partyId, DateTime? collectionDate, string territoryCode, int? transactionTypeId, string mioCode)
        {
            var AuthModel = await authenticator.AuthenticationStatus(Request.Headers["auth_token"]);
            if (AuthModel.IsAuthorized == false) return new OkObjectResult(AuthModel.Jwts);
            var employeeId = AuthModel.ApplicationUserInfo.employeeId.ToString();

            var datajson = await service.GetSalesInvoiceListfromDispatchJson_v2(employeeId, dispatchMasterId, partyId, collectionDate, territoryCode, transactionTypeId, mioCode);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpGet("GetSalesInvoiceListForBillCollection")]
        public async Task<IActionResult> GetSalesInvoiceListForBillCollection(int? collectionMasterId, int? partyId, DateTime? collectionDate)
        {
            var AuthModel = await authenticator.AuthenticationStatus(Request.Headers["auth_token"]);
            if (AuthModel.IsAuthorized == false) return new OkObjectResult(AuthModel.Jwts);
            var employeeId = AuthModel.ApplicationUserInfo.employeeId.ToString();

            var datajson = await service.GetSalesInvoiceListForBillCollection(employeeId, collectionMasterId, partyId, collectionDate);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpGet("GetSalespickingJson")]
        public async Task<IActionResult> GetSalespickingJson(DateTime? fDate, DateTime? tDate)
        {
            var AuthModel = await authenticator.AuthenticationStatus(Request.Headers["auth_token"]);
            if (AuthModel.IsAuthorized == false) return new OkObjectResult(AuthModel.Jwts);
            var employeeId = AuthModel.ApplicationUserInfo.employeeId.ToString();

            var datajson = await service.GetSalSpGetAllPickingJson(AuthModel.ApplicationUserInfo.employeeId, fDate, tDate);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpGet("GetAllDepot")]
        public async Task<IActionResult> GetAllDepot(string SearchingText, DateTime? FromDate, DateTime? ToDate)
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

            var datajson = await service.GetAllDepot(user.employeeId);
            var jwt = await Tokens.GetJwt(datajson.data);
            return new OkObjectResult(jwt);
        }


        [HttpGet("GetSalSpGetAllSalesDispatchJson")]
        public async Task<IActionResult> GetSalSpGetAllSalesDispatchJson(DateTime? fromDate, DateTime? toDate)
        {

            var AuthModel = await authenticator.AuthenticationStatus(Request.Headers["auth_token"]);
            if (AuthModel.IsAuthorized == false) return new OkObjectResult(AuthModel.Jwts);
            var employeeId = AuthModel.ApplicationUserInfo.employeeId;

            var datajson = await service.GetSalSpGetAllSalesDispatchJson((int)employeeId, fromDate, toDate);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }


        [HttpGet("GetAllDamageExpireProductReturn")]
        public async Task<IActionResult> GetAllDamageExpireProductReturn(string MarketOrDepo, int? isApproved)
        {
            var AuthModel = await authenticator.AuthenticationStatus(Request.Headers["auth_token"]);
            if (AuthModel.IsAuthorized == false) return new OkObjectResult(AuthModel.Jwts);
            var employeeId = AuthModel.ApplicationUserInfo.employeeId;

            var datajson = await service.GetAllDamageExpireProductReturn(MarketOrDepo, employeeId, isApproved);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpGet("GetAllDestructionNoteReceive")]
        public async Task<IActionResult> GetAllDestructionNoteReceive(int? masterId)
        {
            var AuthModel = await authenticator.AuthenticationStatus(Request.Headers["auth_token"]);
            if (AuthModel.IsAuthorized == false) return new OkObjectResult(AuthModel.Jwts);
            var employeeId = AuthModel.ApplicationUserInfo.employeeId;

            var datajson = await service.GetAllDestructionNoteReceive(employeeId, masterId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }


        [HttpGet("GetSalesInvoiceDetailsByIdForApproval")]
        public async Task<IActionResult> GetSalesInvoiceDetailsByIdForApproval(int salesInvoiceId)
        {
            var AuthModel = await authenticator.AuthenticationStatus(Request.Headers["auth_token"]);
            if (AuthModel.IsAuthorized == false) return new OkObjectResult(AuthModel.Jwts);
            //var employeeId = AuthModel.ApplicationUserInfo.employeeId.ToString();

            var datajson = await service.GetSalesInvoiceDetailsByIdForApproval(salesInvoiceId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpPost("UpdateSalesInvoiceDetails")]
        public async Task<IActionResult> UpdateSalesInvoiceDetails([FromBody] List<SalesInvoiceDetailsViewModel> models)
        {
            var AuthModel = await authenticator.AuthenticationStatus(Request.Headers["auth_token"]);
            if (AuthModel.IsAuthorized == false) return new OkObjectResult(AuthModel.Jwts);
            var employeeId = AuthModel.ApplicationUserInfo.employeeId.ToString();

            var result = await service.UpdateSalesInvoiceDetails(employeeId, models);
            //var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            //return new OkObjectResult(jwt);
            if (result == 1)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Sales Invoice Updated & Approved Successfully.", true);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Sales Invoice Updated & Approved Failed.", false);
                return new OkObjectResult(jwt);
            }
        }


        #endregion

        #region Party
        [HttpPost("SaveParty")]
        public async Task<IActionResult> SaveParty([FromBody] SalesInvPartyViewModel model)
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

            if (string.IsNullOrWhiteSpace(model.partyName))
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Party Name is Empty.", false);
                return new OkObjectResult(jwt);
            }
            int result = await service.SaveParty(user.employeeId.ToString(), model);

            if (result > 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Party saved successfully !", true);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Party is not saved !", false);
                return new OkObjectResult(jwt);
            }
        }

        [HttpGet("GetDuplicatePartyInfo")]
        public async Task<IActionResult> GetDuplicatePartyInfo(string partyName, string mobileNo)
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

            var datajson = await service.GetDuplicatePartyInfo(partyName, mobileNo);  //user.employeeId.ToString());
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        #endregion

        #region Sales Picking

        [HttpPost("SetSalesPicking")]
        public async Task<IActionResult> SetSalesPicking([FromBody] SalesPickingViewModel model)
        {
            var AuthModel = await authenticator.AuthenticationStatus(Request.Headers["auth_token"]);
            if (AuthModel.IsAuthorized == false) return new OkObjectResult(AuthModel.Jwts);

            if (model.lstMasterViewModel.Count() == 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Sales invoice Not Found.", false);
                return new OkObjectResult(jwt);
            }

            int result = await service.SaveSalesPicking(AuthModel.ApplicationUserInfo.employeeId.ToString(), (int)model.distributionMasterId, model.pickingDate);

            for (int i = 0; i < model.lstMasterViewModel.Count(); i++)
            {
                if (model.lstMasterViewModel[i].isSelect == true)
                {
                    await service.SaveSalesPickingSammary(AuthModel.ApplicationUserInfo.employeeId.ToString(), result, (int)model.lstMasterViewModel[i].salesInvoiceId);
                }
            }

            /*
            for (int i = 0; i < model.lstProductListViewModel.Count(); i++)
            {
                await service.SaveSalesPickingDetails(AuthModel.ApplicationUserInfo.employeeId.ToString(), result, (int)model.lstProductListViewModel[i].productWiseSpecificationId, (int)model.lstProductListViewModel[i].invoiceQty);
            }
            */

            await service.SaveSalesPickingDetails(AuthModel.ApplicationUserInfo.employeeId.ToString(), result, 0, 0);

            if (result > 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Sales invoice Picking has save successfully.", true);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Sales invoice Picking has not Saved.", false);
                return new OkObjectResult(jwt);
            }
        }


        #endregion

        #region Sales Dispatch

        [HttpPost("SetSalesDispatch")]
        public async Task<IActionResult> SetSalesDispatch([FromBody] SalesDispatchViewModel model)
        {
            var AuthModel = await authenticator.AuthenticationStatus(Request.Headers["auth_token"]);
            if (AuthModel.IsAuthorized == false) return new OkObjectResult(AuthModel.Jwts);

            //if (model.lstMasterViewModel.Count() == 0)
            //{
            //    var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Sales Picking Not Found.", false);
            //    return new OkObjectResult(jwt);
            //}
            int result = await service.SaveSalesDispatch(AuthModel.ApplicationUserInfo.employeeId.ToString(), (int)model.distributionMasterId, model.employeeId, model.startDate);

            for (int i = 0; i < model.lstMasterViewModel.Count(); i++)
            {
                if (model.lstMasterViewModel[i].isSelect == true)
                {
                    await service.SaveSalesDispatchDetails(AuthModel.ApplicationUserInfo.employeeId.ToString(), result, (int)model.lstMasterViewModel[i].pickingMasterId, model.lstMasterViewModel[i].salesInvoiceId);
                }
            }

            if (result > 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Sales invoice Dispatch has save successfully.", true);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Sales invoice Dispatch has not Saved.", false);
                return new OkObjectResult(jwt);
            }
        }

        [HttpGet("GetSalesDispatchDetailsbyId")]
        public async Task<IActionResult> GetSalesDispatchDetailsbyId(int distributionMasterId)
        {
            var AuthModel = await authenticator.AuthenticationStatus(Request.Headers["auth_token"]);
            if (AuthModel.IsAuthorized == false) return new OkObjectResult(AuthModel.Jwts);
            //var employeeId = AuthModel.ApplicationUserInfo.employeeId.ToString();

            var datajson = await service.GetSalesDispatchDetailsbyId(distributionMasterId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpPost("DeleteSalesPicking")]
        public async Task<IActionResult> DeleteSalesPicking([FromBody] int pickingMasterId)
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

            bool result = await service.DeleteSalesPicking(user.employeeId, pickingMasterId);

            if (result == true)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Sales Invoice has deleted successfully.", true);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Sales Invoice has not deleted.", false);
                return new OkObjectResult(jwt);
            }
        }

        [HttpPost("DeleteDispatch")]
        public async Task<IActionResult> DeleteDispatch([FromBody] int masterId)
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

            bool result = await service.DeleteDispatch(user.employeeId, masterId);

            if (result == true)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Sales Invoice has deleted successfully.", true);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Sales Invoice has not deleted.", false);
                return new OkObjectResult(jwt);
            }
        }


        [HttpPost("DestructionNoteApproval")]
        public async Task<IActionResult> DestructionNoteApproval([FromBody] DestructionNoteApprovalViewModel models)
        {
            var AuthModel = await authenticator.AuthenticationStatus(Request.Headers["auth_token"]);
            if (AuthModel.IsAuthorized == false) return new OkObjectResult(AuthModel.Jwts);

            if (models == null || models.lstMasterViewModel == null || models.lstMasterViewModel.Count() == 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "No Data Found To Save.", false);
                return new OkObjectResult(jwt);
            }

            int result = await service.DestructionNoteApproval(AuthModel.ApplicationUserInfo.employeeId, models);


            if (result > 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Approved successfully.", true);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Approval Failed.", false);
                return new OkObjectResult(jwt);
            }
        }

        #endregion

        #region National Sales Report

        [HttpGet("GetSalesReportNationally")]
        public async Task<IActionResult> GetSalesReportNationally(string reportName, string reportType, string zoneCode, string regionCode, string areaCode, string territoryCode, DateTime? fDate, DateTime? tDate, int? productWiseSpecificationId, int reportPeriod)
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


            var datajson = await service.GetSalesReportNationally(user.employeeId, reportName, reportType, zoneCode, regionCode, areaCode, territoryCode, fDate, tDate, productWiseSpecificationId, reportPeriod);

            if (datajson == null)
            {
                //  var jwt = await Tokens.GetJwt("");

                return new OkObjectResult(await Tokens.GetJwt(""));
            }
            else
            {

                return new OkObjectResult(await Tokens.GetJwt(datajson.data));
            }
            //  var jwt = await Tokens.GetJwt(datajson.data);

        }

        [HttpGet("GetSalesReportNationallyExcelOnly")]
        public async Task<IActionResult> GetSalesReportNationallyExcelOnly(string reportName, string reportType, string zoneCode, string regionCode, string areaCode, string territoryCode, DateTime? fDate, DateTime? tDate, int? productWiseSpecificationId, int reportPeriod, string reportTypeName, string zoneName, string regionName, string territoryName, string area, string productName)
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

            // Call the service to get the Excel file as MemoryStream
            var success = await service.GetSalesReportNationallyExcelOnly(user.employeeId, reportName, reportType, zoneCode, regionCode, areaCode, territoryCode, fDate, tDate, productWiseSpecificationId, reportPeriod, reportTypeName, zoneName, regionName, territoryName, area, productName);

            if (success == "")
            {
                return new OkObjectResult(await Tokens.GetJwt(""));
            }
            else
            {
                //var x = new OkObjectResult(await Tokens.GetJwt("success"));
                //return new OkObjectResult(await Tokens.GetJwt("success"));
                //var filePath = @"D:\Tuhin_Projects\One-ERP\ONE-ERP-API\ONE-ERP\wwwroot\ExcelReports";

                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "ExcelReports");

                // Assuming there's only one file in the directory
                var file = Directory.GetFiles(filePath).FirstOrDefault();

                if (file == null)
                {
                    return NotFound("No file found in the specified directory.");
                }

                var fileName = Path.GetFileName(file);
                var fileBytes = System.IO.File.ReadAllBytes(file);
                return File(fileBytes, "application/octet-stream", fileName);
            }
        }

        [HttpGet("GetRptAccountScheduleReportByAccountGroupIdsExcelOnly")]
        public async Task<IActionResult> GetRptAccountScheduleReportByAccountGroupIdsExcelOnly(int companyId, int sbuId, string accountGroupId, DateTime? fromDate, DateTime? toDate, string reportType, int? natureId, int? isOb, string reportFormat)
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

            // Call the service to get the Excel file as MemoryStream
            var success = await service.GetRptAccountScheduleReportByAccountGroupIdsExcelOnly(companyId, sbuId, accountGroupId, fromDate, toDate, reportType, natureId == null ? 0 : natureId, isOb == null ? 0 : isOb, reportFormat);

            if (success == "")
            {
                return new OkObjectResult(await Tokens.GetJwt(""));
            }
            else
            {
                //var x = new OkObjectResult(await Tokens.GetJwt("success"));
                //return new OkObjectResult(await Tokens.GetJwt("success"));
                //var filePath = @"D:\Tuhin_Projects\One-ERP\ONE-ERP-API\ONE-ERP\wwwroot\ExcelReports";

                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "ExcelReports");

                // Assuming there's only one file in the directory
                var file = Directory.GetFiles(filePath).FirstOrDefault();

                if (file == null)
                {
                    return NotFound("No file found in the specified directory.");
                }

                var fileName = Path.GetFileName(file);
                var fileBytes = System.IO.File.ReadAllBytes(file);
                return File(fileBytes, "application/octet-stream", fileName);
            }
        }








        [HttpGet("GetNationalSalesPerformance")]
        public async Task<IActionResult> GetNationalSalesPerformance(string reportName, string reportType, string zoneCode, string regionCode, string areaCode, string territoryCode, DateTime? fDate, DateTime? tDate, int? productWiseSpecificationId)
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

            var datajson = await service.GetNationalSalesPerformance(user.employeeId, reportName, reportType, zoneCode, regionCode, areaCode, territoryCode, fDate, tDate, productWiseSpecificationId);
            var jwt = await Tokens.GetJwt(datajson.data);
            return new OkObjectResult(jwt);
        }


        [HttpGet("GetNationSalesClosingStatement")]
        public async Task<IActionResult> GetNationSalesClosingStatement(int? userId, DateTime fDate, DateTime tDate, int? productWiseSpecificationId)
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

            var datajson = await service.GetNationSalesClosingStatement(user.employeeId, fDate, tDate, productWiseSpecificationId);
            var jwt = await Tokens.GetJwt(datajson.data);
            return new OkObjectResult(jwt);
        }
        [HttpGet("GetNationSalesClosingStatementLM")]
        public async Task<IActionResult> GetNationSalesClosingStatementLM(int? userId, DateTime fDate, DateTime tDate, int? productWiseSpecificationId)
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

            var datajson = await service.GetNationSalesClosingStatementLM(user.employeeId, fDate, tDate, productWiseSpecificationId);
            var jwt = await Tokens.GetJwt(datajson.data);
            return new OkObjectResult(jwt);
        }


        [HttpGet("GetNationalStockByQtyReport")]
        public async Task<IActionResult> GetNationalStockByQtyReport(int? userId, DateTime fDate, int? productWiseSpecificationId, string productTypeName)
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

            var datajson = await service.GetNationalStockByQtyReport(user.employeeId, fDate, productWiseSpecificationId, productTypeName);
            var jwt = await Tokens.GetJwt(datajson.data);
            return new OkObjectResult(jwt);
        }


        [HttpGet("GetNationalOutStandingReport")]
        public async Task<IActionResult> GetNationalOutStandingReport(string reportName, string reportType, string zoneCode, string regionCode, string areaCode, string depotCode, string territoryCode, DateTime? fDate, DateTime? tDate, int? productWiseSpecificationId, string reportFormat, int isJsonOutput, int isDuesAmtOnly, string invoiceNo, string mioCode)
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

            var datajson = await service.GetNationalOutStandingReport(user.employeeId, reportName, reportType, zoneCode, regionCode, areaCode, depotCode, territoryCode, fDate, tDate, productWiseSpecificationId, reportFormat, isJsonOutput, isDuesAmtOnly, invoiceNo, mioCode);
            var jwt = await Tokens.GetJwt(datajson.data);
            return new OkObjectResult(jwt);
        }
        [HttpGet("download")]
        public IActionResult DownloadFile()
        {
            var filePath = @"D:\Tuhin_Projects\One-ERP\ONE-ERP-API\ONE-ERP\wwwroot\ExcelReports";

            // Assuming there's only one file in the directory
            var file = Directory.GetFiles(filePath).FirstOrDefault();

            if (file == null)
            {
                return NotFound("No file found in the specified directory.");
            }

            var fileName = Path.GetFileName(file);
            var fileBytes = System.IO.File.ReadAllBytes(file);
            return File(fileBytes, "application/octet-stream", fileName);
        }

        [HttpGet("getProductWiseSpecificationIdByName")] // 
        public async Task<IActionResult> GetProductWiseSpecificationIdByName(string productCode)
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

            var datajson = await service.GetProductWiseSpecificationIdByName(user.employeeId, productCode);
            var jwt = await Tokens.GetJwt(datajson.data);
            return new OkObjectResult(jwt);
        }


        #endregion

        #region Cash In Hand Report
        [HttpGet("GetCashInHand")]
        public async Task<IActionResult> GetCashInHand(string DepotCode, int? userId, DateTime fDate)
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

            var datajson = await service.GetCashInHand(DepotCode, userId, fDate);
            var jwt = await Tokens.GetJwt(datajson.data);
            return new OkObjectResult(jwt);
        }
        #endregion

        #region Check Depot and Territory code
        [HttpGet("CheckDepotandTerritory")]
        public async Task<IActionResult> CheckDepotandTerritory(int? userId, string DepotCode, string territoryCode, string productCode)
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

            var datajson = await service.CheckDepotandTerritory(userId, DepotCode, territoryCode, productCode);
            var jwt = await Tokens.GetJwt(datajson.data);
            return new OkObjectResult(jwt);
        }
        #endregion



        #region Territory Sales Transfer

        [HttpGet("getTerritoryForTerritoryTransfer")]
        public async Task<IActionResult> getTerritoryForTerritoryTransfer(int TerritoryID)
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

            var datajson = await service.GetTerritoryForTerritoryTransfer(TerritoryID, (int)user.employeeId);
            var jwt = await Tokens.GetJwt(datajson.data);
            return new OkObjectResult(jwt);

        }
        [HttpPost("TransferTerritoryData")]
        public async Task<IActionResult> TransferTerritoryData([FromBody] transferSalesTerritoryViewModel model)
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

            bool result = await service.TransferTerritoryData((int)user.employeeId, model.fromTerritoryCode, model.toTerritoryCode);

            if (result == true)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Promo Requision Product has uploaded successfully.", true);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Promo Requision Product has not uploaded successfully", false);
                return new OkObjectResult(jwt);
            }
        }
        #endregion

        [HttpPost("UpdateFrizzProductStatus")]
        public async Task<IActionResult> UpdateFrizzProductStatus([FromBody] IEnumerable<FrizzProductViewModel> models)
        {
            var AuthModel = await authenticator.AuthenticationStatus(Request.Headers["auth_token"]);
            if (AuthModel.IsAuthorized == false) return new OkObjectResult(AuthModel.Jwts);
            var employeeId = AuthModel.ApplicationUserInfo.employeeId.ToString();

            int masterId = await service.UpdateFrizzProductStatus(employeeId, models);

            if (masterId == 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Frizz Status  has not created.", false);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Frizz Status  has not created.", true);
                return new OkObjectResult(jwt);
            }
        }
        [HttpGet("GetAppVersion")]
        public async Task<IActionResult> GetAppVersion()
        {
            var AuthModel = await authenticator.AuthenticationStatus(Request.Headers["auth_token"]);
            if (AuthModel.IsAuthorized == false) return new OkObjectResult(AuthModel.Jwts);
            int? employeeId = AuthModel.ApplicationUserInfo.employeeId;

            var datajson = await service.GetAppVersion(employeeId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }
        [HttpGet("SetAppVersion")]
        public async Task<IActionResult> SetAppVersion([FromQuery] int versionId, int newVersion)
        {
            var AuthModel = await authenticator.AuthenticationStatus(Request.Headers["auth_token"]);
            if (AuthModel.IsAuthorized == false) return new OkObjectResult(AuthModel.Jwts);
            var employeeId = AuthModel.ApplicationUserInfo.employeeId.ToString();

            bool isSuccess = await service.SetAppVersion(employeeId, versionId, newVersion);

            if (!isSuccess)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "App version update failed!.", false);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "App version  updated successfully!.", true);
                return new OkObjectResult(jwt);
            }
        }
        #region SalesOrder Details
        [HttpGet("GetSalesOrderDetailsByMasterId")]
        public async Task<IActionResult> GetSalesOrderDetailsByMasterId(int? salesOrderId)
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

            var datajson = await service.GetSalesOrderDetailsByMasterId(salesOrderId);
            var jwt = await Tokens.getDataWithStatus(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }
        #endregion

        #region Authentication
        /*
        object jwts;
        ApplicationUser user;
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
        */
        #endregion
    }
}