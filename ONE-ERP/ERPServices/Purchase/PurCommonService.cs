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
    public class PurCommonService : IPurCommonService
    {
        private readonly ERPDbContext _context;
        public PurCommonService(ERPDbContext context)
        {
            _context = context;
        }

        /*
        public async Task<JsonViewModel> GetAllUsers(string userName)
        {
            var result = await _context.jsonViewModels.FromSql($"PurSpGetAllUsersJSON {0}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        */

        #region Get All Number for Dropdown / ComboBox

        public async Task<JsonViewModel> GetProductReqNumber(string id, string prodReqNumber)
        {
            var result = await _context.jsonViewModels.FromSql($"PurSpGetProductReqNumberJSON {id}, {prodReqNumber}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<JsonViewModel> GetPurchaseOrderNumber(string id, string purOrderNumber)
        {
            var result = await _context.jsonViewModels.FromSql($"PurSpGetPurchaseOrderNumberJSON {id}, {purOrderNumber}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<JsonViewModel> GetPurchaseOrderReceiveNumber(string id, string purOrderReceiveNumber)
        {
            var result = await _context.jsonViewModels.FromSql($"PurSpGetPurchaseOrderReceiveNumberJSON {id}, {purOrderReceiveNumber}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<JsonViewModel> GetPurchaseReqNumber(string id, string purReqNumber)
        {
            var result = await _context.jsonViewModels.FromSql($"PurSpGetPurchaseReqNumberJSON {id}, {purReqNumber}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        #endregion

        #region Get Max Number

        public async Task<JsonViewModel> GetMaxProductReqNumber(int userId, DateTime prodReqDate)
        {
            var result = await _context.jsonViewModels.FromSql($"PurSpGetMaxProductReqNumberJson {userId},{prodReqDate}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> GetMaxGRNNo(DateTime grnDate)
        {
            var result = await _context.jsonViewModels.FromSql($"PurSpGetMaxGRNNo {grnDate}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> GetMaxGRNImpNo(DateTime grnDate)
        {
            var result = await _context.jsonViewModels.FromSql($"PurSpGetMaxGRNImportNo {grnDate}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> GetMaxPlanNo(DateTime planDate)
        {
            var result = await _context.jsonViewModels.FromSql($"PurSpGetMaxPlanNo {planDate}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> GetMaxBatchNo(DateTime planDate)
        {
            var result = await _context.jsonViewModels.FromSql($"PurSpGetMaxBatchNo {planDate}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> GetMaxBillNo(DateTime billDate)
        {
            var result = await _context.jsonViewModels.FromSql($"PurSpGetMaxBillNo {billDate}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> GetMaxBillPaymentNo(DateTime paymentDate)
        {
            var result = await _context.jsonViewModels.FromSql($"PurSpGetMaxBillPaymentNo {paymentDate}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> GetMaxPurchaseReqNumber(DateTime purchaseReqDate)
        {
            var result = await _context.jsonViewModels.FromSql($"PurSpGetMaxPurchaseReqNumberJson {purchaseReqDate}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<JsonViewModel> GetMaxPurchaseOrderNumber(DateTime purchaseOrderDate)
        {
            var result = await _context.jsonViewModels.FromSql($"PurSpGetMaxPurchaseOrderNumberJson {purchaseOrderDate}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<JsonViewModel> GetMaxPurchaseOrderReceiveNumber(DateTime purchaseOrderRecvDate)
        {
            var result = await _context.jsonViewModels.FromSql($"PurSpGetMaxPurchaseOrderReceiveNumberJson {purchaseOrderRecvDate}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> GetMaxPurchaseFinalReqNumber(DateTime purchaseFinalReqDate)
        {
            var result = await _context.jsonViewModels.FromSql($"PurSpGetMaxPurchaseFinalReqNumberJson {purchaseFinalReqDate}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> GetMaxComperativeStatementNo(DateTime productdate)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"PurSpGetMaxComperativeStatementNoJson {productdate}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public Task<JsonViewModel> getQuotationCollectionNoName()
        {
            try
            {
                var result = _context.jsonViewModels.FromSql($"PurSpGetQuotationCollectionNoName").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<JsonViewModel> getQuotationCollectionDetail(int masterId, int csMasterId)
        {
            var result = await _context.jsonViewModels.FromSql($"PurSpGetQuotationCollectionDetail { masterId},{ csMasterId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<JsonViewModel> GetMaxQuotationCollectionNumber(DateTime quotationCollDate)
        {
            var result = await _context.jsonViewModels.FromSql($"PurSpGetMaxQuotationCollectionNumberJson {quotationCollDate}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        #endregion
        public async Task<JsonViewModel> GetMaxImportPreLcRequisitionNumber(DateTime purchaseReqDate)
        {
            var result = await _context.jsonViewModels.FromSql($"PurSpGetImporPrelCReqNumberJson {purchaseReqDate}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<JsonViewModel> GetMaxImportLcNumber(DateTime purchaseReqDate)
        {
            var result = await _context.jsonViewModels.FromSql($"PurSpGetImportLcNumberJson {purchaseReqDate}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<JsonViewModel> GetMaxImportShipmentNumber(DateTime todayDate)
        {
            var result = await _context.jsonViewModels.FromSql($"PurSpGetImportShipmentNumber {todayDate}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

    }
}