using Microsoft.EntityFrameworkCore;
using ONEERP.Areas.Auth.Models;
using ONEERP.Areas.Inventory.Models;
using ONEERP.Data;
using ONEERP.ERPServices.Inventory.Interfaces;
using ONEERP.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ONEERP.ERPServices.Inventory
{
    public class ProductTransferService : IProductTransferService
    {
        private readonly ERPDbContext _context;
        public ProductTransferService(ERPDbContext context)
        {
            _context = context;
        }

        #region ProductTransferService Master

        public async Task<bool> DeleteProductTransferById(string id, int prodTrnfrId)
        {
            try
            {
                var result = await _context.saveUpdateViewModels.FromSql($"InvSpDeleteProductTransfer {id}, {prodTrnfrId}").AsNoTracking().FirstOrDefaultAsync();
                return result.isSuccess;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<JsonViewModel> GetProductTransferById(int? userId, int? prodTrnfrId, string transferType, DateTime? fDate, DateTime? tDate)
        {
            var result = await _context.jsonViewModels.FromSql($"InvSpGetProductTransferJSON {userId}, {prodTrnfrId}, {transferType}, {fDate}, {tDate}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<JsonViewModel> GetAllProductReqNumberBySbuId(int sbuId)
        {
            var result = await _context.jsonViewModels.FromSql($"InvSpGetAllProductReqNumberBySbuIdJSON {sbuId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> GetMaxProductTransferNumber(DateTime datetime, int? userId, string transferType)
        {
            var result = await _context.jsonViewModels.FromSql($"InvSpGetMaxProductTransferNumberJSON {datetime},{userId},{transferType}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> GetProductTransferDpottoDepotById(int? userId, int? prodTrnfrId)
        {
            var result = await _context.jsonViewModels.FromSql($"InvSpGetProductTransferDepottoDepotJSON {userId}, {prodTrnfrId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<int> SaveProductTransfer(string id, ProductTransferViewModel model)
        {
            //var result = await _context.saveUpdateValueViewModels.FromSql($"InvSpSetProductTransFer {id}, {model.prodTrnfrId}, {model.productReqId},{model.prodTrnDate},{model.fromsbuId},{model.tosbuId},{model.approvalStatus},{model.purpose},{model.isUrgency},{model.isActive}").AsNoTracking().FirstOrDefaultAsync();
            //return result.isSuccess;
            var result = await _context.saveUpdateValueViewModels.FromSql($"InvSpSetProductTransfer {id}, {model.prodTrnfrId}, {model.productReqId},{model.prodTrnDate},{model.fromsbuId},{model.tosbuId},{model.approvalStatus},{model.purpose},{model.driverName},{model.vehicleNo},{model.isUrgency},{model.isActive},{model.transferType}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }
        
        public async Task<int> SaveProductTransferWithoutBatch(string id, ProductTransferViewModel model)
        {
            //var result = await _context.saveUpdateValueViewModels.FromSql($"InvSpSetProductTransFer {id}, {model.prodTrnfrId}, {model.productReqId},{model.prodTrnDate},{model.fromsbuId},{model.tosbuId},{model.approvalStatus},{model.purpose},{model.isUrgency},{model.isActive}").AsNoTracking().FirstOrDefaultAsync();
            //return result.isSuccess;
            var result = await _context.saveUpdateValueViewModels.FromSql($"InvSpSetProductTransferWithoutBatch {id}, {model.prodTrnfrId}, {model.productReqId},{model.prodTrnDate},{model.fromsbuId},{model.tosbuId},{model.approvalStatus},{model.purpose},{model.driverName},{model.vehicleNo},{model.isUrgency},{model.isActive},{model.transferType}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }

        public async Task<JsonViewModel> GetRePackProductTransferById(int? userId, int? RePackProductTransferId)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"InvSpGetRePackProductTransferById {userId}, {RePackProductTransferId}").AsNoTracking().FirstOrDefaultAsync();
                  return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<JsonViewModel> GetRePackProductTransferNoListForReceive(int? userId, int? RePackProductTransferId)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"InvSpGetRePackProductTransferNoListForReceive {userId}, {RePackProductTransferId}").AsNoTracking().FirstOrDefaultAsync();
                  return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<JsonViewModel> GetRePackTransferDetailsById(int? userId, int? RePackProductTransferId)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"InvSpGetRePackTransferDetailsById {userId}, {RePackProductTransferId}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> DeleteRePackProductTransferById(int? userId, int RePackProductTransferId)
        {
            try
            {
                var result = await _context.saveUpdateViewModels.FromSql($"InvSpDeleteRePackProductTransferById {userId}, {RePackProductTransferId}").AsNoTracking().FirstOrDefaultAsync();
                return result.isSuccess;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        #endregion

        #region ProductTransferService Details


        public async Task<bool> DeleteProductTransferDetailsById(string id, int purReqDetailsId)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"InvSpDeleteProductTransferDetails {id}, {purReqDetailsId}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }
        public async Task<bool> DeleteProductTrnfrDetailsById(string id, int productTrnfrDetailsId)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"SalSpDeleteSalesTransferDetailsById {id}, {productTrnfrDetailsId}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }
        public async Task<JsonViewModel> GetProductTransferDetailsByMasterId(int? prodTrnfrId)
        {
            var result = await _context.jsonViewModels.FromSql($"InvSpGetProductTransferDetailsJSON {prodTrnfrId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<JsonViewModel> GetProductReqDetailsForProdTrnsfrById(int? prodReqId, int? storeId)
        {
            var result = await _context.jsonViewModels.FromSql($"PurSpGetProductReqDetailsForProdTrnsfrJSON {prodReqId}, {storeId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> GetAllApprovedDestructionNoteNo(int? userId)
        {
            var result = await _context.jsonViewModels.FromSql($"InvSpGetAllApprovedDestructionNoteNo {userId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> GetAllDestructionNoteReceive(int? userId)
        {
            var result = await _context.jsonViewModels.FromSql($"InvSpGetAllDestructionNoteReceive {userId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> GetDestructionNoteById(int? userId, int masterId)
        {
            var result = await _context.jsonViewModels.FromSql($"InvSpGetDestructionNoteById {userId}, {masterId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<bool> DeleteDestructionNoteReceiveById(int? userId, int masterId)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"InvSpDeleteDestructionNoteReceiveById {userId}, {masterId}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }
        public async Task<int> SaveProductTransferDetails(string id, List<ProductTransferDetailsViewModel> models, int prodTrnfrId)
        {
            var result = new SaveUpdateValueViewModel();
            foreach (ProductTransferDetailsViewModel model in models)
            {
                //string sql = $"InvSpSetProductTransferDetails {id},{model.productTrnfrDetailsId},{prodTrnfrId},{model.productReqDetailsId},{model.fromStoreId},{model.productId},{model.productWiseSpecificationId},{model.transferQty},{model.price},{model.isActive},{model.isSelect},{model.batchNo}";

                result = await _context.saveUpdateValueViewModels.FromSql($"InvSpSetProductTransferDetails {id},{model.productTrnfrDetailsId},{prodTrnfrId},{model.productReqDetailsId},{model.fromStoreId},{model.productId},{model.productWiseSpecificationId},{model.transferQty},{model.price},{model.isActive},{model.isSelect},{model.batchNo}").AsNoTracking().FirstOrDefaultAsync();
            }
            return result.isSuccess;
        }
        public async Task<int> SaveProductTransferDetailsWithoutBatch(string id, List<ProductTransferDetailsViewModel> models, int prodTrnfrId)
        {
            var result = new SaveUpdateValueViewModel();
            foreach (ProductTransferDetailsViewModel model in models)
            {
                //string sql = $"InvSpSetProductTransferDetails {id},{model.productTrnfrDetailsId},{prodTrnfrId},{model.productReqDetailsId},{model.fromStoreId},{model.productId},{model.productWiseSpecificationId},{model.transferQty},{model.price},{model.isActive},{model.isSelect},{model.batchNo}";

                result = await _context.saveUpdateValueViewModels.FromSql($"InvSpSetProductTransferDetailsWithoutBatch {id},{model.productTrnfrDetailsId},{prodTrnfrId},{model.productReqDetailsId},{model.fromStoreId},{model.productId},{model.productWiseSpecificationId},{model.transferQty},{model.price},{model.isActive},{model.isSelect},{model.batchNo}").AsNoTracking().FirstOrDefaultAsync();
            }
            return result.isSuccess;
        }


        public async Task<int> SaveDestructionNoteReceive(int? userId, DestructionNoteReceiveViewModel model)
        {
            try
            {
                var result = await _context.saveUpdateValueViewModels.FromSql($"InvSpSetDestructionNoteReceive {userId}, {model.destructionNoteReceiveId}, {model.damageExpireProductReturnMasterId}, {model.destructionNoteReceiveDate}, {model.miscellaneousTypeId}, {model.remarks}, {model.isApproved},{model.MarketOrDepo}").AsNoTracking().FirstOrDefaultAsync();
                return result.isSuccess;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public async Task<int> SaveDestructionNoteDetails(int? userId, List<DestructionNoteReceiveDetailViewModel> models, int masterId)
        {
            var result = new SaveUpdateValueViewModel();
            try
            {
                foreach (DestructionNoteReceiveDetailViewModel model in models)
                {
                    result = await _context.saveUpdateValueViewModels.FromSql($"InvSpSetDestructionNoteReceiveDetail {userId},{model.destructionNoteRecvDetailId}, {masterId},{model.damageExpireProductReturnDetailId},{model.MiscellaneousItemDetailId},{model.productSpecificationId},{model.qty}").AsNoTracking().FirstOrDefaultAsync();
                }
            }
            catch (Exception ex)
            {
                result.isSuccess = 0;
                throw;
            }
            return result.isSuccess;
        }

        public async Task<JsonViewModel> GetDestructionNoteReceiveForRePack(int? userId)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"InvSpGetDestructionNoteReceiveForRePack {userId}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<JsonViewModel> GetDestructionNoteReceiveDetailForRePack(int? userId, int? destructionNoteReceiveId)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"InvSpGetDestructionNoteDetailForRePackById {userId}, {destructionNoteReceiveId}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> SaveRePackProductTransfer(int? userId, InvRePackProductTransferViewModel model)
        {
            try
            {
                var result = await _context.saveUpdateValueViewModels.FromSql($"InvSpSetRePackProductTransfer {userId}, {model.RePackProductTransferId},{model.destructionNoteReceiveId}, {model.RePackProductTransferNo}, {model.RePackProductTransferDate}, {model.miscellaneousTypeId}, {model.remarks}, {model.isApproved},{model.MarketOrDepo}").AsNoTracking().FirstOrDefaultAsync();
                return result.isSuccess;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public async Task<int> SaveRePackProductTransferDetails(int? userId, List<InvRePackProductTransferDetailViewModel> models, int masterId)
        {
            var result = new SaveUpdateValueViewModel();
            try
            {
                foreach (InvRePackProductTransferDetailViewModel model in models)
                {
                    result = await _context.saveUpdateValueViewModels.FromSql($"InvSpSetRePackProductTransferDetail {userId},{model.RePackProductTransferDetailId},{model.destructionNoteRecvDetailId}, {masterId},{model.productSpecificationId},{model.transferQty},{model.batchNo}").AsNoTracking().FirstOrDefaultAsync();
                }
            }
            catch (Exception ex)
            {
                result.isSuccess = 0;
                throw;
            }
            return result.isSuccess;
        }

        #endregion

        #region Reports
        public async Task<JsonViewModel> GetProductTransferReportData(int? userId, DateTime? fromDate, DateTime? toDate, int? fromSbuId, int? fromStoreId)
        {
            var result = await _context.jsonViewModels.FromSql($"InvSpGetProductTransferReportDataJSON {fromDate}, {toDate}, {fromSbuId}, {fromStoreId}, {userId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        #endregion
    }
}
