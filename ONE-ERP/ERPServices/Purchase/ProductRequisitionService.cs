using Microsoft.EntityFrameworkCore;
using ONEERP.Areas.Auth.Models;
using ONEERP.Areas.Purchase.Models;
using ONEERP.Data;
using ONEERP.ERPServices.Purchase.Interfaces;
using ONEERP.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ONEERP.ERPServices.Purchase
{
    public class ProductRequisitionService : IProductRequisitionService
    {
        private readonly ERPDbContext _context;
        public ProductRequisitionService(ERPDbContext context)
        {
            _context = context;
        }

        #region Prodct Req. Master

        public async Task<bool> DeleteProductReqById(string id, int prodReqId)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"PurSpDeleteProductRequisition {id}, {prodReqId}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }

        public async Task<JsonViewModel> GetProductReqById(int? userId, int? prodReqId)//, int? productReqDetailsId)
        {
            var result = await _context.jsonViewModels.FromSql($"PurSpGetProductReqInfoJSON {prodReqId},{userId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<JsonViewModel> getTerritoryOfficerByPartyId(int? partyId)
        {
            var result = await _context.jsonViewModels.FromSql($"CmnSpGetTerritoryOfficerByPartyId {partyId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<int> SaveProductReq(string id, ProductRequisitionViewModel ReqViewModel)
        {
            var result = await _context.saveUpdateValueViewModels.FromSql($"PurSpSetProductRequisition {id}, {ReqViewModel.prodReqId},{ReqViewModel.prodReqDate},{ReqViewModel.fromsbuId},{ReqViewModel.tosbuId},{ReqViewModel.purpose},{ReqViewModel.isUrgency},{ReqViewModel.isActive}").AsNoTracking().FirstOrDefaultAsync();//,{ReqViewModel.purpose},{ReqViewModel.isUrgency},{ReqViewModel.approvalStatus}
            return result.isSuccess;

        }
        public async Task<string> ValidateBatchWiseProductStock(int? userId, int? sbuId, int? productWiseSpecificationId, string batchNo, decimal? transferQty)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"salSpGetProductStockStatus {userId},{sbuId},{productWiseSpecificationId},{batchNo},{transferQty}").AsNoTracking().FirstOrDefaultAsync();
                return result.data;
            }
            catch (Exception ex)
            {
                return "Validation Process Failed!";
            }
        }

        public async Task<int> SaveProductTransfer(string id, ProductRequisitionViewModel model)
        {
            try
            {
                //var txt = $"InvSpSetProductTransFer {id}, {model.prodTrnfrId}, {model.prodReqId},{model.prodReqDate},{model.fromsbuId},{model.tosbuId},0,{model.purpose},{model.driverName},{model.vehicleNo},{model.isUrgency},{model.isActive},{model.transferType}";

                var result = await _context.saveUpdateValueViewModels.FromSql($"InvSpSetProductTransFer {id}, {model.prodTrnfrId}, {model.prodReqId},{model.prodReqDate},{model.fromsbuId},{model.tosbuId},0,{model.purpose},{model.driverName},{model.vehicleNo},{model.isUrgency},{model.isActive},{model.transferType}").AsNoTracking().FirstOrDefaultAsync();
                return result.isSuccess;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public async Task<int> SaveProductTransferDetails(string id, List<ProductReqDetailsViewModel> models, int prodTrnfrId)
        {
            try
            {
                var result = new SaveUpdateValueViewModel();
                foreach (ProductReqDetailsViewModel model in models)
                {
                    var txt = ($"InvSpSetProductTransferDetails {id},{model.productReqDetailsId},{prodTrnfrId},{model.productReqDetailsId},{model.fromStoreId},{model.productId},{model.productWiseSpecificationId},{model.reqQty},{model.price},{model.isActive},{0},{model.batchNo}");

                    result = await _context.saveUpdateValueViewModels.FromSql($"InvSpSetProductTransferDetails {id},{model.productTrnfrDetailsId},{prodTrnfrId},{model.productReqDetailsId},{model.fromStoreId},{model.productId},{model.productWiseSpecificationId},{model.reqQty},{model.price},{model.isActive},{0},{model.batchNo}").AsNoTracking().FirstOrDefaultAsync();
                }
                return result.isSuccess;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }


        public async Task<JsonViewModel> GetProductCurrentStockBySbuId(int productWiseSpecificationId, int sbuId)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"spGetProductCurrentStockBySbuIdJSON {productWiseSpecificationId}, {sbuId}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Prodct Req. Details

        public async Task<bool> DeleteProductReqDetailsById(string id, int productReqDetailsId)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"PurSpDeleteProductReqDetails {id}, {productReqDetailsId}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }

        public async Task<JsonViewModel> GetProductReqDetailsById(int? prodReqId)//(int? productReqDetailsId)
        {
            var result = await _context.jsonViewModels.FromSql($"PurSpGetProductReqDetailsJSON {prodReqId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<int> SaveProductReqDetails(string id, List<ProductReqDetailsViewModel> purProductReqDetailsViewModels, int prodReqId)
        {
            var result = new SaveUpdateValueViewModel();
            foreach (ProductReqDetailsViewModel purProductReqDetailsViewModel in purProductReqDetailsViewModels)
            {
                result = await _context.saveUpdateValueViewModels.FromSql($"PurSpSetProductReqDetails {id},{purProductReqDetailsViewModel.productReqDetailsId},{prodReqId},{purProductReqDetailsViewModel.productId},{purProductReqDetailsViewModel.productWiseSpecificationId},{purProductReqDetailsViewModel.reqQty},{purProductReqDetailsViewModel.price},{purProductReqDetailsViewModel.isActive}").AsNoTracking().FirstOrDefaultAsync();
            }
            return result.isSuccess;
        }

        #endregion

        #region preoduct req ------
        public async Task<JsonViewModel> GetRptGridProductReq(int? prodReqId)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"PurSpGetProductReqReportInfo {prodReqId}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        public async Task<JsonViewModel> GetProductApprvedRequisition(int UserId, int? approvedStatus, int? finalizeMasterId)
        {
            var result = await _context.jsonViewModels.FromSql($"PurSpGetPurchaseReqDetailsByApprovalStatus {UserId},{approvedStatus},{finalizeMasterId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
    }
}