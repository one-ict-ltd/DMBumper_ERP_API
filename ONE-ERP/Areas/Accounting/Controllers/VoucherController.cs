using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Newtonsoft.Json;
using ONEERP.Areas.Accounting.Models;
using ONEERP.ERPService.AuthService.Interfaces;
using ONEERP.ERPServices.Accounting.Transaction.Interfaces;
using ONEERP.ERPServices.EmailService.Interfaces;
using ONEERP.Helpers;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.Accounting.Controllers
{
    [Route("api/[controller]")]
    //[Authorize]
    public class VoucherController : Controller
    {
        private readonly IUserInfoes userInfoes;
        private readonly IVoucherMasterService voucherMasterService;
        private readonly IVoucherDetailService voucherDetailService;
        private readonly IVoucherApprovalLogService voucherApprovalLogService;
        private readonly ICostCentreAllocationService costCentreAllocationService;
        private readonly IEmailSenderService emailSenderService;
        public VoucherController(IUserInfoes userInfoes, IVoucherMasterService voucherMasterService, IVoucherDetailService voucherDetailService, IVoucherApprovalLogService voucherApprovalLogService, ICostCentreAllocationService costCentreAllocationService, IEmailSenderService emailSenderService)
        {
            this.userInfoes = userInfoes;
            this.voucherMasterService = voucherMasterService;
            this.voucherDetailService = voucherDetailService;
            this.voucherApprovalLogService = voucherApprovalLogService;
            this.costCentreAllocationService = costCentreAllocationService;
            this.emailSenderService = emailSenderService;
        }


        [HttpPost("setVoucher")]
        public async Task<IActionResult> setVoucher([FromBody] VoucherMasterViewModel model)
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

            if (model.voucherAmount == 0 && !model.lstdetailmodel.Any())
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Voucher has not created.", false);
                return new OkObjectResult(jwt);
            }
            bool result = false;
            int voucherMasterId = await voucherMasterService.SaveVoucherMaster(user.employeeId.ToString(), model);

            if(voucherMasterId>0 && model.isPosted == 1)
            {
                var message = "Dear Sir ,<br> A voucher <b> " + model.voucherNo + "</b> is waiting for your approval. <br> Application Link - http://103.106.236.93:9116/";
                //await emailSenderService.SendEmailWithFrom("tonoy300oneict@gmail.com","One ICT","A voucher waiting for your Approval",message);
                await emailSenderService.SendEmailViaAppPass("yalid007@nationalagricare.com", "A voucher waiting for your Approval",message);
                //await emailSenderService.SendEmailNew();
            }

            if (voucherMasterId == 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Voucher has not created.", false);
                return new OkObjectResult(jwt);
            }
            result = await voucherDetailService.SaveVoucherDetails(user.employeeId.ToString(), model.lstdetailmodel, model.lstcostmodel, model.voucherAttachmentList, voucherMasterId,model.isPosted);

            if (result)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Voucher has created successfully.", true, voucherMasterId);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Voucher has not created.", false);
                return new OkObjectResult(jwt);
            }
        }

        [HttpPost("setVoucherExcel")]
        public async Task<IActionResult> setVoucherExcel([FromBody] VoucherMasterViewModelExcel model)
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

            if (model.voucherAmount == 0 && !model.lstMaster.Any())
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Voucher details has not found.", false);
                return new OkObjectResult(jwt);
            }

            model.voucherAmount = Math.Round((decimal)model.lstMaster.Sum(x => x.drAmount), 2);
            //var credit = Math.Round((decimal)model.lstMaster.Sum(x => x.crAmount),2);

            if (Math.Round((decimal)model.lstMaster.Sum(x => x.drAmount), 2) != Math.Round((decimal)model.lstMaster.Sum(x => x.crAmount), 2))
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Voucher debit amount and credit amount must be same!!", false);
                return new OkObjectResult(jwt);
            }

            bool result = false;
            //model.remarks = "Uploaded";
            model.fundSourceId = 5;
            int voucherMasterId = await voucherMasterService.SaveVoucherMasterExcel(user.employeeId.ToString(), model);

            if (voucherMasterId == 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Voucher has not created.", false);
                return new OkObjectResult(jwt);
            }

            var convertedModel = await voucherMasterService.ConvertVoucherExcelToVoucherMaster(model);

            result = await voucherDetailService.SaveVoucherDetails(user.employeeId.ToString(), convertedModel.lstdetailmodel, convertedModel.lstcostmodel, convertedModel.voucherAttachmentList, voucherMasterId,0);

            if (result)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Voucher has created successfully.", true, voucherMasterId);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Voucher has not created.", false);
                return new OkObjectResult(jwt);
            }
        }


        [HttpGet("getvoucherMaster")]
        public async Task<IActionResult> getvoucherMaster(int voucherMasterId, int voucherTypeId)
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

            var datajson = await voucherMasterService.GetVoucherMasterListbyVoucherMasterIdJson(voucherMasterId, voucherTypeId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }


        [HttpGet("getUploadedVoucher")]
        public async Task<IActionResult> getUploadedVoucher(int voucherTypeId)
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

            var datajson = await voucherMasterService.GetUploadedVoucherListJson((int)user.employeeId, voucherTypeId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }


        [HttpGet("getvoucherMasterWithDate")]
        public async Task<IActionResult> getvoucherMasterWithDate(int voucherMasterId, int voucherTypeId, DateTime fromDate, DateTime toDate)
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

            var datajson = await voucherMasterService.GetVoucherMasterListbyVoucherMasterIdDateJson(voucherMasterId, voucherTypeId, fromDate, toDate, (int)user.employeeId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpGet("VoucherEditDeleteCheck")]
        public async Task<IActionResult> VoucherEditDeleteCheck(int voucherMasterId)
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

            var datajson = await voucherMasterService.GetVoucherEditDeleteCheckJson(voucherMasterId, (int)user.employeeId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }


        [HttpGet("getvoucherMasterForPosting")]
        public async Task<IActionResult> getvoucherMasterForPosting(int voucherMasterId, int voucherTypeId, int isPost)
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

            var datajson = await voucherMasterService.GetVoucherMasterListbyVoucherMasterForPostingIdJson((int)user.employeeId,voucherMasterId, voucherTypeId, isPost);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpGet("getvoucherMasterForPostingFactory")]
        public async Task<IActionResult> getvoucherMasterForPostingFactory(int voucherMasterId, int voucherTypeId, int isPost)
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

            var datajson = await voucherMasterService.GetVoucherMasterListbyVoucherMasterForPostingIdFactoryJson(voucherMasterId, voucherTypeId, isPost);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpGet("getvoucherDetailByMasterId")]
        public async Task<IActionResult> getvoucherDetailByMasterId(int voucherMasterId)
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

            var datajson = await voucherDetailService.GetVoucherDetailListbyVoucherMasterIdJson(voucherMasterId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpGet("getvoucherAttachmentByMasterId")]
        public async Task<IActionResult> getvoucherAttachmentByMasterId(int voucherMasterId)
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

            var datajson = await voucherDetailService.GetVoucherAttachmentListbyVoucherMasterIdJson(voucherMasterId, 0);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpGet("downloadVoucherAttachmentByMasterId")]
        public async Task<IActionResult> downloadVoucherAttachmentByMasterId(int voucherAttachmentId)
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

            var datajson = await voucherDetailService.GetVoucherAttachmentListbyVoucherMasterIdJson(0, voucherAttachmentId);
            var dataList = JsonConvert.DeserializeObject<List<VoucherAttachmentlViewModel>>(datajson.data);
            var data = dataList.FirstOrDefault();
            if (data != null)
            {
                var filePath = data.attachmentUrl ?? string.Empty;
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

        [HttpGet("getCostCentreAllocationByMasterId")]
        public async Task<IActionResult> getCostCentreAllocationByMasterId(int voucherMasterId)
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

            var datajson = await costCentreAllocationService.GetCostCentreAllocationbyVoucherMasterIdJson(voucherMasterId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpGet("getvoucherNo")]
        public async Task<IActionResult> getvoucherNo(int voucherTypeId, DateTime voucherDate, int IsCheque)
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
            var datajson = await voucherMasterService.GetVoucherNoJson(voucherTypeId, voucherDate, IsCheque);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpPost("deletevoucherMaster")]
        public async Task<IActionResult> deletevoucherMaster([FromBody] VoucherMasterViewModel model)
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

            if (model.voucherMasterId <= 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Voucher has not deleted.", false);
                return new OkObjectResult(jwt);
            }
            bool result = await voucherMasterService.DeleteVoucherMasterById(user.employeeId.ToString(), (int)model.voucherMasterId);
            if (result)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Voucher has deleted successfully.", true);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Voucher has not deleted.", false);
                return new OkObjectResult(jwt);
            }
        }


        [HttpGet("getBalanceAmountByLedger")]
        public async Task<IActionResult> getBalanceAmountByLedger(int ledgerId, int? partyId)
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

            var datajson = await voucherMasterService.GetBalanceAmountByLedgerJson(ledgerId, partyId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpGet("CheckLockFiscalYear")]
        public async Task<IActionResult> CheckLockFiscalYear(DateTime voucherDate)
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

            var datajson = await voucherMasterService.CheckLockFiscalYear(Convert.ToDateTime(voucherDate).ToString("yyyyMMdd"));
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpPost("UpdateVoucherMaster")]
        public async Task<IActionResult> UpdateVoucherMaster([FromBody] VoucherPostingViewModel model)
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

            if (!model.lstMasterViewModel.Any())
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Voucher Posting Not Found.", false);
                return new OkObjectResult(jwt);
            }
            int result = 0;
            for (int i = 0; i < model.lstMasterViewModel.Count; i++)
            {
                if (model.lstMasterViewModel[i].isSelect == true)
                {
                    result = await voucherMasterService.UpdateVoucherMaster(user.employeeId.ToString(), (int)model.isPosted, model.lstMasterViewModel[i]);
                }
            }


            if (result > 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Voucher Posting has Approved successfully.", true);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Voucher Posting has not Approved.", false);
                return new OkObjectResult(jwt);
            }
        }


    }
}
