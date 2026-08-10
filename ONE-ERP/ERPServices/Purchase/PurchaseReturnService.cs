using Microsoft.EntityFrameworkCore;
using ONEERP.Areas.Auth.Models;
using ONEERP.Areas.Purchase.Models;
using ONEERP.Data;
using ONEERP.ERPServices.Purchase.Interfaces;
using ONEERP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.ERPServices.Purchase
{
    public class PurchaseReturnService : IPurchaseReturnService
    {
        private readonly ERPDbContext _context;
        public PurchaseReturnService(ERPDbContext context)
        {
            _context = context;
        }

        #region Purchase Return Master

        public async Task<int> SavePurchaseReturnMaster(string id, PurchaseReturnMasterViewModel model)
        {
            try
            {
                var result = await _context.saveUpdateValueViewModels.FromSql($"PurSpSetPurchaseReturnMaster {id}, {model.purchaseReturnMasterId}, {model.purchaseOrderId}, {model.partyId}, {model.storeId}, {model.purchaseReturnNo}, {model.purchaseReturnDate}, {model.grossAmount}, {model.totalVatAmount}, {model.totalAitAmount}, {model.freightChargeAmount}, {model.totalDiscountAmount}, {model.netAmount}, {model.comments}").AsNoTracking().FirstOrDefaultAsync();
                return result.isSuccess;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<JsonViewModel> GetPurchaseReturnMasterByMasterId(int? purchaseReturnMasterId)
        {
            var result = await _context.jsonViewModels.FromSql($"PurSpGetPurchaseReturnMasterByMasterId {purchaseReturnMasterId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<bool> DeletePurchaseReturnMasterByMasterId(string id, int purchaseReturnMasterId)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"PurSpDeletePurchaseReturn {id}, {purchaseReturnMasterId}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }

        public async Task<JsonViewModel> GetMaxPurchaseReturnNumber(DateTime datetime)
        {
            var result = await _context.jsonViewModels.FromSql($"PurSpGetMaxPurchaseReturnNo {datetime}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<JsonViewModel> GetPOListBySupplierId(int? supplierId)
        {
            var result = await _context.jsonViewModels.FromSql($"PurSpGetPOListBySupplierId {supplierId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        #endregion

        #region Purchase Return Details

        public async Task<int> SavePurchaseReturnDetails(string id, List<PurchaseReturnDetailsViewModel> purchaseReturnDetailsViewModels, int purchaseReturnMasterId)
        {
            await _context.saveUpdateViewModels.FromSql($"PurSpDeletePurchaseReturnDetails {id},{purchaseReturnMasterId}").AsNoTracking().FirstOrDefaultAsync();
            var result = new SaveUpdateValueViewModel();
            foreach (PurchaseReturnDetailsViewModel model in purchaseReturnDetailsViewModels.Where(a => a.returnQty != 0))
            {
                result = await _context.saveUpdateValueViewModels.FromSql($"SalSpSetPurchaseReturnDetails {id},{0},{purchaseReturnMasterId},{model.purchaseOrderDetailsId},{model.returnQty},{model.unitPrice},{model.vatPercent},{model.aitPercent},{model.discountPercent},{model.totalAmount},{model.productId},{model.productWiseSpecificationId}").AsNoTracking().FirstOrDefaultAsync();
            }
            return result.isSuccess;
        }

        public async Task<JsonViewModel> GetPurchaseReturnDetailsByMasterId(int purchaseReturnMasterId)
        {
            var result = await _context.jsonViewModels.FromSql($"AccSpGetPurchaseReturnDetailsByMasterId {purchaseReturnMasterId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        #endregion


    }
}
