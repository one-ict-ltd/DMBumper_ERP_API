using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ONEERP.Areas.Purchase.Models;
using ONEERP.ERPService.AuthService.Interfaces;
using ONEERP.ERPServices.Purchase.Interfaces;
using ONEERP.Helpers;
using ONEERP.Data.Entity;

namespace ONEERP.Areas.Purchase.Controllers
{
    [Route("api/[controller]")]
    public class PurchaseOrderController : Controller
    {
        object jwts;
        ApplicationUser user;
        private IUserInfoes userInfoes;
        private readonly IPurchaseOrderService purOrderService;
        public PurchaseOrderController(IUserInfoes userInfoes, IPurchaseOrderService purOrderService)
        {
            this.userInfoes = userInfoes;
            this.purOrderService = purOrderService;
            jwts = new object();
            user = new ApplicationUser();
        }

        #region Purchase Order

        [HttpPost("setPurchaseOrder")]
        public async Task<IActionResult> setPurchaseOrder([FromBody] PurchaseOrderViewModel model)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);
            //model.toWarehouseId == 0 &&
            if (model.lstPurOrderDetailsViewModel.Count() == 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Purchase To Warehouse or Order Details is empty! Purchase Order has not created.", false);
                return new OkObjectResult(jwt);
            }

            if (model.purchaseFromId == 1) model.csMasterId = null;
            else if (model.purchaseFromId == 2) model.requisitionFinalizeMasterId = null;
            else
            {
                model.csMasterId = null;
                model.requisitionFinalizeMasterId = null;
            }


            int result = 0;
            int prodReqId = await purOrderService.SavePurchaseOrder(user.employeeId.ToString(), model);

            if (prodReqId == 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Purchase Order has not created.", false);
                return new OkObjectResult(jwt);
            }

            result = await purOrderService.SavePurchaseOrderDetails(user.employeeId.ToString(), model.lstPurOrderDetailsViewModel, prodReqId);

            if (model.poWiseTermsAndConditions.Count() > 0)
            {
                result = await purOrderService.SavePOWisetermsAndConditions(user.employeeId.ToString(), model.poWiseTermsAndConditions, prodReqId);
                if (result != 0)
                {
                    var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Purchase Order Details has created successfully.", true);
                    return new OkObjectResult(jwt);
                }

                else
                {
                    var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Purchase Order has not created.", false);
                    return new OkObjectResult(jwt);
                }
            }


            if (result != 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Purchase Order Details has created successfully.", true);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Purchase Order Details has not created.", false);
                return new OkObjectResult(jwt);
            }



        }

        [HttpGet("getPurchaseOrder")]
        public async Task<IActionResult> getPurchaseOrder(int? purchaseOrderId, int? purchaseTypeId)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);
            var datajson = await purOrderService.GetPurchaseOrderById(purchaseOrderId, purchaseTypeId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpGet("getPurchaseOrderDetailsData")]
        public async Task<IActionResult> getPurchaseOrderDetailsData(int? purchaseOrderId)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            var datajson = await purOrderService.GetPurchaseOrderDataById(purchaseOrderId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpGet("GetPurchaseOrderBypurchaseOrderId")]
        public async Task<IActionResult> GetPurchaseOrderBypurchaseOrderId(int? purchaseOrderId)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            var datajson = await purOrderService.GetPurchaseOrderBypurchaseOrderId(purchaseOrderId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }


        [HttpPost("DeletePurchaseOrderById")]
        public async Task<IActionResult> DeletePurchaseOrderById([FromBody] int purchaseOrderId)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            if (purchaseOrderId <= 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Purchase Order has not deleted.", false);
                return new OkObjectResult(jwt);
            }
            bool result = await purOrderService.DeletePurchaseOrderById(user.employeeId.ToString(), purchaseOrderId);

            if (result == true)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Purchase Order has deleted successfully.", true);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Purchase Order has not deleted.", false);
                return new OkObjectResult(jwt);
            }
        }


        #endregion

        #region Purchase Order Details

        [HttpGet("GetPurchaseOrderDetails")]
        public async Task<IActionResult> GetPurchaseOrderDetails(int? purchaseOrderDetailsId)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);
            var datajson = await purOrderService.GetPurchaseOrderDetailsById(purchaseOrderDetailsId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpPost("DeletePurchaseOrderDetailsById")]
        public async Task<IActionResult> DeletePurchaseOrderDetailsById(int purchaseOrderDetailsId)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);
            bool result = await purOrderService.DeletePurchaseOrderDetailsById(user.employeeId.ToString(), purchaseOrderDetailsId);

            if (result == true)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Purchase Order Details has deleted successfully.", true);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Purchase Order Details has not deleted.", false);
                return new OkObjectResult(jwt);
            }
        }
        #endregion

        #region Terms && Conditions 

        [HttpPost("setTermsAndConditions")]
        public async Task<IActionResult> setTermsAndConditions(int supplierId, int productTypeId, [FromBody] List<TermsAndConditionsViewModel> model)
        {

            if (Authentication().Result == false) return new OkObjectResult(jwts);

            foreach (var item in model)
            {
                if (item.termsAndConditions == null)
                {
                    var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Compnay has not created successfully.", false);
                    return new OkObjectResult(jwt);
                }
            }

            int result = await purOrderService.SaveTermsAndConditions(user.employeeId.ToString(), model, supplierId, productTypeId);


            if (result == 1)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Compnay has created successfully.", true);

                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Compnay has not created successfully.", false);

                return new OkObjectResult(jwt);
            }
        }

        [HttpGet("getTermsAndConditions")]
        public async Task<IActionResult> getTermsAndConditions(int supplierId)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            var datajson = await purOrderService.GetTermsAndConditionsById(supplierId);

            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });

            return new OkObjectResult(jwt);

        }

        [HttpGet("getTermsAndConditionsNoStuff")]
        public async Task<IActionResult> getTermsAndConditionsNoStuff(int supplierId, int productTypeId)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            var datajson = await purOrderService.GetTermsAndConditionsNoStuffById(supplierId, productTypeId);

            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });

            return new OkObjectResult(jwt);

        }


        [HttpGet("getProductTypeWiseTermsAndConditions")]
        public async Task<IActionResult> getProductTypeWiseTermsAndConditions(int purchaseOrderId, int productTypeId)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            var datajson = await purOrderService.GetProductTypeWiseTermsAndConditions(purchaseOrderId, productTypeId);

            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });

            return new OkObjectResult(jwt);

        }

        [HttpGet("getTermsAndConditionsInUpdate")]
        public async Task<IActionResult> getTermsAndConditionsInUpdate(int purchaseOrderId)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            var datajson = await purOrderService.GetTermsAndConditionsInUpdate(purchaseOrderId);

            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });

            return new OkObjectResult(jwt);

        }

        [HttpPost("deleteTermsAndConditionsId")]
        public async Task<IActionResult> deleteTermsAndConditionsId([FromBody] int termsAndConditionsId)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            if (termsAndConditionsId <= 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "terms And Conditions has not deleted.", false);
                return new OkObjectResult(jwt);
            }
            bool result = await purOrderService.DeleteTermsAndConditionsById(user.employeeId.ToString(), termsAndConditionsId);

            if (result == true)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Purchase Order has deleted successfully.", true);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Purchase Order has not deleted.", false);
                return new OkObjectResult(jwt);
            }
        }


        

        #endregion

        #region GRN

        [HttpPost("setGRNImport")]
        public async Task<IActionResult> setGRNImport([FromBody] GRNImportViewModel model)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            if (model.lstDetailsViewModel.Count() == 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "GRN Details is empty! GRN has not created.", false);
                return new OkObjectResult(jwt);
            }

            int result = 0;
            int prodReqId = await purOrderService.SaveGRNImport(user.employeeId.ToString(), model);

            if (prodReqId == 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "GRN has not created.", false);
                return new OkObjectResult(jwt);
            }

            result = await purOrderService.SaveGRNImportDetails(user.employeeId.ToString(), model.lstDetailsViewModel, prodReqId);


            if (result != 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "GRN Details has created successfully.", true);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "GRN  Details has not created.", false);
                return new OkObjectResult(jwt);
            }



        }

        [HttpGet("getGRNImport")]
        public async Task<IActionResult> getGRNImport(int? grnId)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);
            var datajson = await purOrderService.getGRNImportById(grnId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }
        [HttpGet("getGRNImportForReturnOrder")]
        public async Task<IActionResult> getGRNImportForReturnOrder(int? grnId)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);
            var datajson = await purOrderService.getGRNImportForReturnOrder(grnId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpPost("setGRN")]
        public async Task<IActionResult> setGRN([FromBody] GRNViewModel model)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            if (model == null || model.lstDetailsViewModel.Count() == 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "GRN Details is empty! GRN has not created.", false);
                return new OkObjectResult(jwt);
            }
            int result = 0;
            int prodReqId = await purOrderService.SaveGRN(user.employeeId.ToString(), model);

            if (prodReqId == 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "GRN has not created.", false);
                return new OkObjectResult(jwt);
            }

            result = await purOrderService.SaveGRNDetails(user.employeeId.ToString(), model.lstDetailsViewModel, prodReqId);


            if (result != 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "GRN Details has created successfully.", true);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "GRN  Details has not created.", false);
                return new OkObjectResult(jwt);
            }



        }

        [HttpGet("getGRN")]
        public async Task<IActionResult> getGRN(int? grnId, DateTime? fDate, DateTime? tDate)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            var datajson = await purOrderService.GetGRNById(user.employeeId,grnId, fDate, tDate);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpGet("getGRNForReturnOrder")]
        public async Task<IActionResult> getGRNForReturnOrder(int? grnId)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            var datajson = await purOrderService.GetGRNForReturnOrderById(user.employeeId, grnId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpGet("getGRNDetailsById")]
        public async Task<IActionResult> getGRNDetailsById(int? grnId)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            var datajson = await purOrderService.GetGRNDetailsById(grnId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpPost("deleteGRNById")]
        public async Task<IActionResult> deleteGRNById([FromBody] int grnId)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);
            if (grnId <= 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Purchase Order has not deleted.", false);
                return new OkObjectResult(jwt);
            }
            bool result = await purOrderService.DeleteGRNById(user.employeeId.ToString(), grnId);

            if (result == true)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Purchase Order has deleted successfully.", true);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Purchase Order has not deleted.", false);
                return new OkObjectResult(jwt);
            }
        }

        [HttpGet("getGRNsupplierChallanNo")]
        public async Task<IActionResult> getGRNsupplierChallanNo(int? poId)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            var datajson = await purOrderService.getGRNsupplierChallanNo(poId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpGet("getPurchaseOrdersForGRN")]
        public async Task<IActionResult> getPurchaseOrdersForGRN(int? poId)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            var datajson = await purOrderService.GetPurchaseOrdersForGRN(poId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpGet("getPurchaseOrdersForGRNN")]
        public async Task<IActionResult> getPurchaseOrdersForGRNN(int? poId)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            var datajson = await purOrderService.GetPurchaseOrdersForGRNN(poId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }
        [HttpGet("getPurchaseOrdersForRejectedGRN")]
        public async Task<IActionResult> getPurchaseOrdersForRejectedGRN(int? poId)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            var datajson = await purOrderService.GetPurchaseOrdersForRejectedGRN(poId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }
        [HttpGet("getLcNo")]
        public async Task<IActionResult> getLcNo()
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            var datajson = await purOrderService.getLcNo();
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }
        [HttpGet("getLcNoForRejectedQty")]
        public async Task<IActionResult> getLcNoForRejectedQty()
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            var datajson = await purOrderService.getLcNoForRejectedQty();
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpGet("getPODetailsByIdForGRN")]
        public async Task<IActionResult> getPODetailsByIdForGRN(int? poId, int? grnMasterid)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);
            var datajson = await purOrderService.GetPODetailsByIdForGRN(poId, grnMasterid);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpGet("GetGRNImportDetails")]
        public async Task<IActionResult> GetGRNImportDetails(int? lcId, int? grnMasterid)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);
            var datajson = await purOrderService.GetGRNImportDetails(lcId, grnMasterid);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpGet("getPODetailsByLcInfo")]
        public async Task<IActionResult> getPODetailsByLcInfo(int? ImpPreLCInfoMasterId)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);
            var datajson = await purOrderService.getPODetailsByLcInfo(ImpPreLCInfoMasterId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }



        [HttpGet("getPODetailsByIdForGRNForPdfReport")]
        public async Task<IActionResult> getPODetailsByIdForGRNForPdfReport(int? poId, int? grnMasterid)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            var datajson = await purOrderService.GetPODetailsByIdForGRNForReport(poId, grnMasterid);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpGet("GetRejectedGRN")]
        public async Task<IActionResult> getRejectedGRN(int? purchaseOrderId)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);
            var datajson = await purOrderService.GetRejectedGRN(purchaseOrderId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }
        [HttpGet("getRejectedImportGRN")]
        public async Task<IActionResult> getRejectedImportGRN(int? ImpPreLCInfoMasterId)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);
            var datajson = await purOrderService.GetRejectedImportGRN(ImpPreLCInfoMasterId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }
        #endregion


        #region Bill

        [HttpPost("setBill")]
        public async Task<IActionResult> setBill([FromBody] BillViewModel model)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            if (model.lstDetailsViewModel.Count() == 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Bill Details is empty! Bill has not created.", false);
                return new OkObjectResult(jwt);
            }

            int result = 0;
            int masterId = await purOrderService.SaveBill(user.employeeId.ToString(), model);

            if (masterId == 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Bill has not created.", false);
                return new OkObjectResult(jwt);
            }

            result = await purOrderService.SaveBillDetails(user.employeeId.ToString(), model.lstDetailsViewModel, masterId);


            if (result != 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Bill Details has created successfully.", true);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Bill  Details has not created.", false);
                return new OkObjectResult(jwt);
            }



        }

        [HttpGet("getBill")]
        public async Task<IActionResult> getBill(int? billId)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            var datajson = await purOrderService.GetBillById(user.employeeId,billId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpGet("getBillDetailsById")]
        public async Task<IActionResult> getBillDetailsById(int? billId)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);
            var datajson = await purOrderService.GetBillDetailsById(billId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpPost("deleteBillById")]
        public async Task<IActionResult> deleteBillById([FromBody] int billId)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);
            if (billId <= 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Purchase Order has not deleted.", false);
                return new OkObjectResult(jwt);
            }
            bool result = await purOrderService.DeleteBillById(user.employeeId.ToString(), billId);

            if (result == true)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Purchase Order has deleted successfully.", true);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Purchase Order has not deleted.", false);
                return new OkObjectResult(jwt);
            }
        }

        [HttpGet("getSupplierWiseProductsForBill")]
        public async Task<IActionResult> getSupplierWiseProductsForBill(int? supplierId, int? billMasterid, int? poId=0)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);
            var datajson = await purOrderService.getSupplierWiseProductsForBill(supplierId, billMasterid, poId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }
        [HttpGet("getBillPayableJV")]
        public async Task<IActionResult> getBillPayableJV(int? billMasterId, int? partyId, decimal paymentAmount, decimal vatPaymentAmount, decimal vdsPaymentAmount, decimal tdsPaymentAmount, decimal netPaymentAmount)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);
            var datajson = await purOrderService.getBillPayableJV(user.employeeId.ToString(), billMasterId, partyId, paymentAmount, vatPaymentAmount, vdsPaymentAmount, tdsPaymentAmount, netPaymentAmount);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpGet("getAllPOForBill")]
        public async Task<IActionResult> getAllPOForBill()
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);
            var datajson = await purOrderService.getAllPOForBill(user.employeeId.ToString());
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }



        [HttpGet("getSupplierWiseProductsForBillForPdfReport")]
        public async Task<IActionResult> getSupplierWiseProductsForBillForPdfReport(int? supplierId, int? billMasterid)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);
            var datajson = await purOrderService.getSupplierWiseProductsForBillForPdfReport(supplierId, billMasterid);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpGet("getBillForPdfReport")]
        public async Task<IActionResult> getBillForPdfReport(int? billId)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            var datajson = await purOrderService.GetBillByIdForPdfReport(billId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        #endregion


        #region Bill Payment
        [HttpGet("getBillInfoForPayment")]
        public async Task<IActionResult> getBillInfoForPayment(int? billId)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            var datajson = await purOrderService.GetBillInfoForPayment(user.employeeId, billId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpPost("setBillPayableVoucherPosting")]
        public async Task<IActionResult> setBillPayableVoucherPosting([FromBody] BillPayableViewModel model)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);
            int masterId = await purOrderService.SaveBillPayableVoucherPosting(user.employeeId.ToString(), model);


            if (masterId != 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Bill Payable has created successfully.", true);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Bill Payable has not created.", false);
                return new OkObjectResult(jwt);
            }

        }

        [HttpGet("getBillPayableVoucher")]
        public async Task<IActionResult> getBillPayableVoucher(int? voucherMasterId, DateTime fromDate, DateTime toDate)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);
            var datajson = await purOrderService.GetBillPayableVoucherById(user.employeeId, voucherMasterId, fromDate, toDate);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpGet("getSupplierInfoForBillPayment")]
        public async Task<IActionResult> getSupplierInfoForBillPayment()
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            var datajson = await purOrderService.getSupplierInfoForBillPayment(user.employeeId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpPost("setBillPayment")]
        public async Task<IActionResult> setBillPayment([FromBody] BillPaymentsViewModel model)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);
            int masterId = await purOrderService.SaveBillPayment(user.employeeId.ToString(), model);


            if (masterId != 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Bill Payment has created successfully.", true);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Bill Payment has not created.", false);
                return new OkObjectResult(jwt);
            }

        }

        [HttpGet("getBillPayment")]
        public async Task<IActionResult> getBillPayment(int? voucherMasterId, DateTime fromDate, DateTime toDate)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);
            var datajson = await purOrderService.GetBillPaymentById(user.employeeId, voucherMasterId, fromDate, toDate);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpPost("deleteBillPaymentById")]
        public async Task<IActionResult> deleteBillPaymentById([FromBody] int paymentMasterId)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);
            if (paymentMasterId <= 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Bill Payment has not deleted.", false);
                return new OkObjectResult(jwt);
            }
            bool result = await purOrderService.DeleteBillPaymentById(user.employeeId.ToString(), paymentMasterId);

            if (result == true)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Bill Payment has deleted successfully.", true);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Bill Payment has not deleted.", false);
                return new OkObjectResult(jwt);
            }
        }

        [HttpGet("getSupplierWiseBillsForPayment")]
        public async Task<IActionResult> getSupplierWiseBillsForPayment(int? supplierId, int? billMasterid)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);
            var datajson = await purOrderService.GetSupplierWiseBillsForPayment(user.employeeId,supplierId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        #endregion
        #region Budget Create

        [HttpGet("GetBudgetCategoryList")]
        public async Task<IActionResult> GetBudgetCategoryList()
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);
            var datajson = await purOrderService.GetBudgetCategoryList();
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpPost("SaveBudgetCreate")]
        public async Task<IActionResult> SaveBudgetCreate([FromBody] PurBudgetCreateViewModel model)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);
            int masterId = await purOrderService.SaveBudgetCreate(user.employeeId.ToString(), model.lstBudgetDetailsViewModel);


            if (masterId != 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Bill Payment has created successfully.", true);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Bill Payment has not created.", false);
                return new OkObjectResult(jwt);
            }




        }

        [HttpGet("GetBudgetCreateList")]
        public async Task<IActionResult> GetBudgetCreateList(int? BudgetCreateId)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);
            var datajson = await purOrderService.GetBudgetCreateList(BudgetCreateId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        #endregion
        #region Reports

        [HttpGet("GetPurchaseOrderNumberByType")]
        public async Task<IActionResult> GetPurchaseOrderNumberByType(int? reportTypeId, int? partyId)
        {
            #region Authentication

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

            var datajson = await purOrderService.GetPurchaseOrderNumberByType(reportTypeId, partyId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpGet("SpGetPartyBySbu")]
        public async Task<IActionResult> SpGetPartyBySbu(int? reportTypeId, int? sbuId)
        {
            #region Authentication

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

            var datajson = await purOrderService.SpGetPartyBySbu(reportTypeId, sbuId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpGet("GetDateRangeWisePoEntryUser")]
        public async Task<IActionResult> GetDateRangeWisePoEntryUser(DateTime? fromDate, DateTime? toDate)
        {
            #region Authentication

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

            var datajson = await purOrderService.GetDateRangeWisePoEntryUser(fromDate, toDate);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }
        
        [HttpGet("GetPurchaseOrdersReportData")]
        public async Task<IActionResult> GetPurchaseOrdersReportData(int? reportTypeId, int? sbuId, int? partyId, int? userId, DateTime? fromDate, DateTime? toDate)
        {
            #region Authentication

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

            var datajson = await purOrderService.GetPurchaseOrdersReportData(reportTypeId, sbuId, partyId, userId, fromDate, toDate);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }
        [HttpGet("GetPurchaseOrdersReport")]
        public async Task<IActionResult> GetPurchaseOrdersReport(int? supplierId, int? productTypeId, int? productId, int? userId, DateTime? fromDate, DateTime? toDate)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);
            var datajson = await purOrderService.GetPurchaseOrdersReport(supplierId, productTypeId, productId, userId, fromDate, toDate);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }
        [HttpGet("GetPOSearchResult")]
        public async Task<IActionResult> GetPOSearchResult(string SearchingText, DateTime? FromDate, DateTime? ToDate)
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

            var datajson = await purOrderService.GetPOSearchResult(SearchingText, FromDate, ToDate);
            var jwt = await Tokens.GetJwt(datajson.data);
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