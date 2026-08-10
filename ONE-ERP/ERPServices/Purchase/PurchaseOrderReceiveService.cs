using Microsoft.EntityFrameworkCore;
using ONEERP.Areas.Auth.Models;
using ONEERP.Areas.Purchase.Models;
using ONEERP.Data;
using ONEERP.ERPServices.Purchase.Interfaces;
using ONEERP.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ONEERP.ERPServices.Purchase
{
    public class PurchaseOrderReceiveService : IPurchaseOrderReceiveService
    {
        private readonly ERPDbContext _context;
        public PurchaseOrderReceiveService(ERPDbContext context)
        {
            _context = context;
        }

        #region PurchaseOrderReceive Master

        public async Task<bool> DeletePurchaseOrderReceiveById(string id, int poReceiveId)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"PurSpDeletePurchaseOrderReceive {id}, {poReceiveId}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }

        public async Task<JsonViewModel> GetPurchaseOrderReceiveById(int? poReceiveId)
        {
            var result = await _context.jsonViewModels.FromSql($"PurSpGetPurchaseOrderReceiveInfoJSON {poReceiveId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<int> SavePurchaseOrderReceive(string id, PurchaseOrderReceiveViewModel model)
        {
            try
            {
                var ssq = $"PurSpSetPurchaseOrderReceive {id}, {model.poReceiveId}, {model.purchaseOrderId},{model.purchaseOrderRecvDate},{model.tosbuId},{model.approvalStatus},{model.isActive}";

                var result = await _context.saveUpdateValueViewModels.FromSql($"PurSpSetPurchaseOrderReceive {id}, {model.poReceiveId}, {model.purchaseOrderId},{model.purchaseOrderRecvDate},{model.tosbuId},{model.approvalStatus},{model.isActive}").AsNoTracking().FirstOrDefaultAsync();
                return result.isSuccess;
            }
            catch (System.Exception ex)
            {

                throw;
            }
        }

        #endregion

        #region PurchaseOrderReceive Details

        public async Task<bool> DeletePurchaseOrderReceiveDetailsById(string id, int poReceiveDetailsId)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"PurSpDeletePurchaseOrderReceiveDetails {id}, {poReceiveDetailsId}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }

        public async Task<JsonViewModel> GetPurchaseOrderReceiveDetailsByMasterId(int? poReceiveId)
        {
            var result = await _context.jsonViewModels.FromSql($"PurSpGetPurchaseOrderReceiveDetailsJSON {poReceiveId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> GetPurchaseOrderDetailsByIdForPoRecv(int? purchaseOrderId)
        {
            var result = await _context.jsonViewModels.FromSql($"PurSpGetPurchaseOrderDetailsForPoRecvJSON {purchaseOrderId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<int> SavePurchaseOrderReceiveDetails(string id, List<PurchaseOrderReceiveDetailsViewModel> models, int poReceiveId)
        {
            var result = new SaveUpdateValueViewModel();
            foreach (PurchaseOrderReceiveDetailsViewModel model in models)
            {
                result = await _context.saveUpdateValueViewModels.FromSql($"PurSpSetPurchaseOrderReceiveDetails {id},{model.poReceiveDetailsId},{poReceiveId},{model.purchaseOrderDetailsId},{model.productId},{model.receiveQty},{model.price},{model.isActive},{model.productWiseSpecificationId}").AsNoTracking().FirstOrDefaultAsync();
            }
            return result.isSuccess;
        }

        #endregion
    }
}
