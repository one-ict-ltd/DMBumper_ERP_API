using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ONEERP.Areas.Sales.Models;
using ONEERP.Data.Entity;
using ONEERP.ERPService.AuthService.Interfaces;
using ONEERP.ERPServices.Purchase;
using ONEERP.ERPServices.Purchase.Interfaces;
using ONEERP.ERPServices.Sales.Interfaces;
using ONEERP.Helpers;

namespace ONEERP.Areas.Sales.Controllers
{
    [Route("api/[controller]")]
    public class SalesDistributionController : Controller
    {
        object jwts;
        ApplicationUser user;
        private IUserInfoes userInfoes;
        private readonly ISalesDistributionService service;
        private readonly IProductRequisitionService productRequisitionService;
        public SalesDistributionController(IUserInfoes _userInfoes, ISalesDistributionService _service, IProductRequisitionService _productRequisitionService)
        {
            this.userInfoes = _userInfoes;
            this.service = _service;
            productRequisitionService = _productRequisitionService;
        }

        #region Sales Distribution

        [HttpPost("SaveSalesDistribution")]
        public async Task<IActionResult> SaveSalesDistribution([FromBody] SalesDistributionMasterViewModel model)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            if (model.lstDetailsViewModel.Count() == 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Sales Distribution has not created.", false);
                return new OkObjectResult(jwt);
            }

            int result = 0;
            int distributionMasterId = await service.SaveSalesDistribution(user.employeeId.ToString(), model);

            if (distributionMasterId == 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Sales Distribution has not created.", false);
                return new OkObjectResult(jwt);
            }

            result = await service.SaveSalesDistributionDetails(user.employeeId.ToString(), model.lstDetailsViewModel, distributionMasterId);

            if (result != 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Sales Distribution Details has created successfully.", true);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Sales Distribution Details has not created.", false);
                return new OkObjectResult(jwt);
            }
        }

        [HttpPost("DeleteSalesDistributionById")]
        public async Task<IActionResult> DeleteSalesDistributionById([FromBody] int distributionMasterId)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            if (distributionMasterId <= 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Sales Distribution has not deleted.", false);
                return new OkObjectResult(jwt);
            }
            bool result = await service.DeleteSalesDistributionById(user.employeeId.ToString(), distributionMasterId);

            if (result == true)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Sales Distribution has deleted successfully.", true);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Sales Distribution has not deleted.", false);
                return new OkObjectResult(jwt);
            }
        }

        [HttpGet("GetSalesDistributionById")]
        public async Task<IActionResult> GetSalesDistributionById(int? distributionMasterId)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            var datajson = await service.GetSalesDistributionById(distributionMasterId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpGet("GetMaxSalesDistributionNumber")]
        public async Task<IActionResult> GetMaxSalesDistributionNumber(DateTime dateTime)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            var datajson = await service.GetMaxSalesDistributionNumber(dateTime);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpGet("GetDepoWiseSalesInvoiceList")]
        public async Task<IActionResult> GetDepoWiseSalesInvoiceList(int? depoId)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            var datajson = await service.GetDepoWiseSalesInvoiceList(depoId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        #endregion

        #region Sales Distribution Details

        [HttpGet("GetSalesDistributionDetailsByInvoiceId")]
        public async Task<IActionResult> GetSalesDistributionDetailsByInvoiceId(int? salesInvoiceId)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            var datajson = await service.GetSalesDistributionDetailsByInvoiceId(salesInvoiceId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpGet("GetSalesDistributionDetailsByMasterId")]
        public async Task<IActionResult> GetSalesDistributionDetailsByMasterId(int? distributionMasterId)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            var datajson = await service.GetSalesDistributionDetailsByMasterId(distributionMasterId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpPost("DeleteSalesDistributionDetailsById")]
        public async Task<IActionResult> DeleteSalesDistributionDetailsById([FromBody] int distributionDetailId)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            bool result = await service.DeleteSalesDistributionDetailsById(user.employeeId.ToString(), distributionDetailId);

            if (result == true)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Sales Distribution Details has deleted successfully.", true);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Sales Distribution Details has not deleted.", false);
                return new OkObjectResult(jwt);
            }
        }

        #endregion

        #region Approval

        [HttpPost("ApproveSalesDistribution")]
        public async Task<IActionResult> ApproveSalesDistribution([FromBody] SalesDistributionMasterViewModel model)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            if (model.lstMasterViewModel.Count() == 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Sales Distribution Not Found.", false);
                return new OkObjectResult(jwt);
            }
            int result = await service.ApproveSalesDistribution(user.employeeId.ToString(), model.lstMasterViewModel, model.approvalStatus);

            if (result > 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Sales Distribution has Approved successfully.", true);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Sales Distribution has not Approved.", false);
                return new OkObjectResult(jwt);
            }
        }


        [HttpGet("GetSalesDistributionListForApproval")]
        public async Task<IActionResult> GetSalesDistributionListForApproval(int? distributionMasterId)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            var datajson = await service.GetSalesDistributionListForApproval(user.employeeId.ToString(), distributionMasterId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }
        [HttpGet("GetSalesDistributionApprovedList")]
        public async Task<IActionResult> GetSalesDistributionApprovedList(int? distributionMasterId)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            var datajson = await service.GetSalesDistributionApprovedList(user.employeeId.ToString(), distributionMasterId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }


        #endregion


        #region Reports

        [HttpGet("GetSalesDistributionReportDataById")]
        public async Task<IActionResult> GetSalesDistributionReportDataById(int? distributionMasterId)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            var datajson = await service.GetSalesDistributionReportDataById(distributionMasterId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpGet("GetDestructionReportById")]
        public async Task<IActionResult> GetDestructionReportById(int? masterId, string rType, string depotCode, DateTime fDate, DateTime tDate)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            var datajson = await service.GetDestructionReportById(user.employeeId, masterId, rType, depotCode, fDate, tDate);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        #endregion



        #region Miscellaneous for factory

        [HttpPost("SaveMiscellaneousItem")]
        public async Task<IActionResult> SaveMiscellaneousItem([FromBody] MiscellaneousItemViewModel model)
        {
            if (!Authentication().Result) return new OkObjectResult(jwts);

            if (!model.lstMiscellaneousDetailsViewModel.Any())
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Miscellaneous Item has not created.", false);
                return new OkObjectResult(jwt);
            }
            foreach (var item in model.lstMiscellaneousDetailsViewModel)
            {
                var res = await productRequisitionService.ValidateBatchWiseProductStock(user.employeeId, model.sbuId, item.productSpecificationId, item.batchNo, item.ctnQty);
                if (!string.IsNullOrWhiteSpace(res))
                {
                    var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, res, false);
                    return new OkObjectResult(jwt);
                }

            }

            int result = 0;
            int miscellaneousItemId = await service.SaveMiscellaneousItem(user.employeeId, model);

            if (miscellaneousItemId == 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Miscellaneous Item has not created.", false);
                return new OkObjectResult(jwt);
            }
            //foreach (var item in model.lstMiscellaneousDetailsViewModel)
            //{
            //    var res = await productRequisitionService.ValidateBatchWiseProductStock(user.employeeId, model.sbuId, item.productSpecificationId, item.batchNo, item.ctnQty);
            //    if (!string.IsNullOrWhiteSpace(res))
            //    {
            //        var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, res, false);
            //        return new OkObjectResult(jwt);
            //    }

            //}

            result = await service.SaveMiscellaneousItemDetails(user.employeeId, model.lstMiscellaneousDetailsViewModel, miscellaneousItemId);

            if (result != 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Miscellaneous Item Details has created successfully.", true);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Miscellaneous Item Details has not created.", false);
                return new OkObjectResult(jwt);
            }
        }

        [HttpPost("DeleteMiscellaneousItem")]
        public async Task<IActionResult> DeleteMiscellaneousItem([FromBody] int miscellaneousItemId)
        {
            if (!Authentication().Result) return new OkObjectResult(jwts);

            int result = await service.DeleteMiscellaneousItem(user.employeeId, miscellaneousItemId);

            if (result != 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Miscellaneous Item Details has deleted successfully.", true);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Miscellaneous Item Details has not deleted.", false);
                return new OkObjectResult(jwt);
            }
        }

        [HttpPost("DeleteMiscellaneousItemDetails")]
        public async Task<IActionResult> DeleteMiscellaneousItemDetails([FromBody] int miscellaneousItemDetailsId)
        {
            if (!Authentication().Result) return new OkObjectResult(jwts);

            int result = await service.DeleteMiscellaneousItemDetails(user.employeeId, miscellaneousItemDetailsId);

            if (result != 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Miscellaneous Item Details has deleted successfully.", true);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Miscellaneous Item Details has not deleted.", false);
                return new OkObjectResult(jwt);
            }
        }
        [HttpGet("GetMiscellaneousItemById")]
        public async Task<IActionResult> GetMiscellaneousItemById(int? miscellaneousItemId)
        {
            if (!Authentication().Result) return new OkObjectResult(jwts);

            var datajson = await service.GetMiscellaneousItemById(user.employeeId, miscellaneousItemId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpGet("GetMiscellaneousItemDetailsByMasterId")]
        public async Task<IActionResult> GetMiscellaneousItemDetailsByMasterId(int? miscellaneousItemId)
        {
            if (!Authentication().Result) return new OkObjectResult(jwts);

            var datajson = await service.GetMiscellaneousItemDetailsByMasterId(user.employeeId, miscellaneousItemId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpGet("GetMaxMiscellaneousNumber")]
        public async Task<IActionResult> GetMaxMiscellaneousNumber(DateTime dateTime)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            var datajson = await service.GetMaxMiscellaneousNumber(user.employeeId, dateTime);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }
        #endregion for factory


        #region Miscellaneous for depot

        [HttpPost("SaveMiscellaneousItemDepot")]
        public async Task<IActionResult> SaveMiscellaneousItemDepot([FromBody] MiscellaneousItemViewModel model)
        {
            if (!Authentication().Result) return new OkObjectResult(jwts);

            if (!model.lstMiscellaneousDetailsViewModel.Any())
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Miscellaneous Item has not created.", false);
                return new OkObjectResult(jwt);
            }

            if (model.miscellaneousTypeId == 14 || model.miscellaneousTypeId == 7)// repack receive or Quarantine In
            {// stock in

            }
            else
            {// stock out
                foreach (var item in model.lstMiscellaneousDetailsViewModel)
                {
                    var res = await productRequisitionService.ValidateBatchWiseProductStock(user.employeeId, model.sbuId, item.productSpecificationId, item.batchNo, item.ctnQty);
                    if (!string.IsNullOrWhiteSpace(res))
                    {
                        var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, res, false);
                        return new OkObjectResult(jwt);
                    }
                }
            }

            int result = 0;
            int miscellaneousItemId = await service.SaveMiscellaneousItemDepot(user.employeeId, model);

            if (miscellaneousItemId == 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Miscellaneous Item has not created.", false);
                return new OkObjectResult(jwt);
            }

            result = await service.SaveMiscellaneousItemFileDepot(user.employeeId, model.lstFileAttachment, miscellaneousItemId);
            result = await service.SaveMiscellaneousItemDetailsDepot(user.employeeId, model.lstMiscellaneousDetailsViewModel, miscellaneousItemId);

            if (result != 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Miscellaneous Item Details has created successfully.", true);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Miscellaneous Item Details has not created.", false);
                return new OkObjectResult(jwt);
            }
        }

        [HttpPost("DeleteMiscellaneousItemDepot")]
        public async Task<IActionResult> DeleteMiscellaneousItemDepot([FromBody] int miscellaneousItemId)
        {
            if (!Authentication().Result) return new OkObjectResult(jwts);

            int result = await service.DeleteMiscellaneousItemDepot(user.employeeId, miscellaneousItemId);

            if (result != 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Miscellaneous Item Details has deleted successfully.", true);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Miscellaneous Item Details has not deleted.", false);
                return new OkObjectResult(jwt);
            }
        }

        [HttpPost("DeleteMiscellaneousItemDetailsDepot")]
        public async Task<IActionResult> DeleteMiscellaneousItemDetailsDepot([FromBody] int miscellaneousItemId)
        {
            if (!Authentication().Result) return new OkObjectResult(jwts);

            int result = await service.DeleteMiscellaneousItemDetailsDepot(user.employeeId, miscellaneousItemId);

            if (result != 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Miscellaneous Item Details has deleted successfully.", true);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Miscellaneous Item Details has not deleted.", false);
                return new OkObjectResult(jwt);
            }
        }

        [HttpGet("GetMiscellaneousItemDepotById")]
        public async Task<IActionResult> GetMiscellaneousItemDepotById(int? miscellaneousItemId)
        {
            if (!Authentication().Result) return new OkObjectResult(jwts);

            var datajson = await service.GetMiscellaneousItemDepotById(user.employeeId, miscellaneousItemId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpGet("GetMiscellaneousItemDetailsDepotByMasterId")]
        public async Task<IActionResult> GetMiscellaneousItemDetailsDepotByMasterId(int? miscellaneousItemId)
        {
            if (!Authentication().Result) return new OkObjectResult(jwts);

            var datajson = await service.GetMiscellaneousItemDetailsDepotByMasterId(user.employeeId, miscellaneousItemId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpGet("GetMaxMiscellaneousNumberDepot")]
        public async Task<IActionResult> GetMaxMiscellaneousNumberDepot(DateTime dateTime)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            var datajson = await service.GetMaxMiscellaneousNumberDepot(user.employeeId, dateTime);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpGet("GetAllMiscellaneousType")]
        public async Task<IActionResult> GetAllMiscellaneousType(string param)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            var datajson = await service.GetAllMiscellaneousType(user.employeeId, param);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        #endregion

        #region  miscellaneous item  for depot(Approval)
        [HttpGet("GetALLMiscellaneousItemDepotByApproval")]
        public async Task<IActionResult> GetALLMiscellaneousItemDepotByApproval(int? isApproved)
        {
            if (!Authentication().Result) return new OkObjectResult(jwts);

            var datajson = await service.GetALLMiscellaneousItemDepotByApproval(user.employeeId, isApproved);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }
        [HttpPost("SaveMiscellaneousItemForDepotApproval")]
        public async Task<IActionResult> SaveMiscellaneousItemForDepotApproval([FromBody] MiscellaneousItemApprovalViewModel models)
        {
            if (!Authentication().Result) return new OkObjectResult(jwts);

            if (models == null || models.lstMasterViewModel == null || models.lstMasterViewModel.Count() == 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "No Data Found To Save.", false);
                return new OkObjectResult(jwt);
            }

            int result = await service.SaveMiscellaneousItemForDepotApproval(user.employeeId, models);


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
        #endregion miscellaneous item  for depot(Approval)

        #region Deal Not Applicable

        [HttpPost("SaveDealNotApplicableCustomerAndInstitute")]
        public async Task<IActionResult> SaveDealNotApplicableCustomerAndInstitute([FromBody] SalDealNotApplicableCustomerAndInstituteViewModel model)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            bool result = await service.SaveDealNotApplicableCustomerAndInstitute(user.employeeId.ToString(), model);

            if (result == true)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Sales Distribution has created.", false);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Sales Distribution has not created.", false);
                return new OkObjectResult(jwt);
            }


        }

        [HttpGet("getDealNotApplicableCustomerAndInstituteList")]
        public async Task<IActionResult> getDealNotApplicableCustomerAndInstituteList(int dealNotApplicableCustomerAndInstituteId)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            var datajson = await service.getDealNotApplicableCustomerAndInstituteList(user.employeeId.ToString(), dealNotApplicableCustomerAndInstituteId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
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