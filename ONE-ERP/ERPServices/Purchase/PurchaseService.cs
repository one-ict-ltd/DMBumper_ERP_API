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
    public class PurchaseService : IPurchaseService
    {
        private readonly ERPDbContext _context;
        public PurchaseService(ERPDbContext context)
        {
            _context = context;
        }

        #region Purchase Master

        public async Task<int> SavePurchase(string id, PurchaseViewModel model)
        {
            var result = await _context.saveUpdateValueViewModels.FromSql($"PurSpSetPurchase {id}, {model.purchaseOrderId}, {model.purchaseOrderDate}, {model.fromWarehouseId}, {model.purpose}, {model.supplierId}, {model.grossAmount}, {model.totalVat}, {model.totalAit}, {model.totalDiscount}, {model.freightCharge}, {model.netAmount}, {model.isAutoStock}, {model.lcNo}, {model.refNo}, {model.transactionTypeId}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }

        public async Task<JsonViewModel> GetPurchaseById(int? purchaseOrderId)
        {
            var result = await _context.jsonViewModels.FromSql($"PurSpGetPurchaseByPurchaseId {purchaseOrderId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }


        #endregion

        #region Purchase Details

        public async Task<int> SavePurchaseDetails(string id, List<PurchaseDetailsViewModel> purchaseDetailsViewModels, int purchaseOrderId, decimal? totalVat, decimal? totalAit, decimal? freightCharge, decimal? grossAmount, int? storeId, bool? isAutoStock)
        {
            //await _context.saveUpdateViewModels.FromSql($"PurSpDeletePurOrderDtlByPurId {id},{purchaseOrderId}").AsNoTracking().FirstOrDefaultAsync();

            var result = new SaveUpdateValueViewModel();
            foreach (var model in purchaseDetailsViewModels)
            {
                decimal? costPrice = 0;
                try
                {
                    costPrice = model.price + ((((totalVat + totalAit + freightCharge) * (model.reqQty * model.price)) / grossAmount) / model.reqQty); 
                    
                    result = await _context.saveUpdateValueViewModels.FromSql($"PurSpSetPurchaseDetails {id},{model.purchaseOrderDetailsId},{purchaseOrderId},{model.productId},{model.productWiseSpecificationId},{model.reqQty},{model.price},{model.vatPercent},{model.aitPercent},{model.discountPercent},{costPrice},{model.totalAmount},{storeId}, {isAutoStock}").AsNoTracking().FirstOrDefaultAsync();
                }
                catch (System.Exception ex)
                {

                    throw ex;
                }
            }
            return result.isSuccess;
        }

        #endregion


    }
}
