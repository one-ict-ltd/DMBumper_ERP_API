using Microsoft.EntityFrameworkCore;
using ONEERP.Areas.Auth.Models;
using ONEERP.Areas.Sales.Models;
using ONEERP.Data;
using ONEERP.ERPServices.Sales.Interfaces;
using ONEERP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.ERPServices.Sales
{
    public class SalesReturnService : ISalesReturnService
    {
        private readonly ERPDbContext _context;
        public SalesReturnService(ERPDbContext context)
        {
            _context = context;
        }

        #region Sales Return Master

        public async Task<int> SaveSalesReturnMaster(string id, SalesReturnMasterViewModel model)
        {
            try
            {
                var result = await _context.saveUpdateValueViewModels.FromSql($"SalSpSetSalesReturnMaster {id}, {model.salesReturnMasterId}, {model.salesInvoiceId}, {model.partyId}, {model.storeId}, {model.salesReturnNo}, {model.salesReturnDate}, {model.grossAmount}, {model.totalVatAmount}, {model.totalAitAmount}, {model.shippingCostAmount}, {model.totalDiscountAmount}, {model.netAmount}").AsNoTracking().FirstOrDefaultAsync();
                return result.isSuccess;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public async Task<int> SaveSalesGrossReturnMaster(string id, SalesReturnMasterViewModel model)
        {
            try
            {
                var result = await _context.saveUpdateValueViewModels.FromSql($"SalSpSetSalesGrossReturn {id}, {model.salesReturnMasterId}, {model.salesInvoiceId}, {model.partyId}, {model.productWiseSpecificationId}, {model.salesReturnNo}, {model.salesReturnDate}, {model.netAmount}, {model.returnQty}").AsNoTracking().FirstOrDefaultAsync();
                return result.isSuccess;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        public async Task<int> SaveSalesGrossReturnMultiMaster(string id, SalesReturnMasterViewModel model)
        {
            try
            {
                var result = await _context.saveUpdateValueViewModels.FromSql($"SalSpSetSalesGrossReturnMulti {id}, {model.salesReturnMasterId}, {model.salesInvoiceId}, {model.partyId}, {model.productWiseSpecificationId}, {model.salesReturnNo}, {model.salesReturnDate}, {model.netAmount}, {model.returnQty}").AsNoTracking().FirstOrDefaultAsync();
                return result.isSuccess;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public async Task<int> SaveSalesGrossReturnDetailsMultiInvoice(string id, List<SalesGrossReturnInvoiceViewModel> salesReturnDetailsViewModels, int salesReturnMasterId)
        {
            try
            {
                await _context.saveUpdateViewModels.FromSql($"SalSpDeleteSalesGrossReturnDetailsMultiInvoice {id},{salesReturnMasterId}").AsNoTracking().FirstOrDefaultAsync();

                var result = new SaveUpdateValueViewModel();
                //foreach (SalesGrossReturnInvoiceViewModel model in salesReturnDetailsViewModels.Where(a => (a.isSelect == true)))
                foreach (SalesGrossReturnInvoiceViewModel model in salesReturnDetailsViewModels.Where(a => (a.isSelect == true)))
                {
                    result = await _context.saveUpdateValueViewModels.FromSql($"SalSpSetSalesGrossReturnDetailsMultiInvoice {id},{0},{salesReturnMasterId},{model.collectionAmount},{model.salesInvoiceId}").AsNoTracking().FirstOrDefaultAsync();
                }
                return result.isSuccess;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }


        public async Task<int> SaveSalesGrossReturnDetailsMultiItems(string id, List<SalesReturnDetailsViewModel> salesReturnDetailsViewModels, int salesReturnMasterId)
        {
            try
            {
                await _context.saveUpdateViewModels.FromSql($"SalSpDeleteSalesGrossReturnDetailsMultiItem {id},{salesReturnMasterId}").AsNoTracking().FirstOrDefaultAsync();

                var result = new SaveUpdateValueViewModel();
                //foreach (SalesReturnDetailsViewModel model in salesReturnDetailsViewModels.Where(a => (a.returnQty == null ? 0 : a.returnQty) > 0))
                foreach (SalesReturnDetailsViewModel model in salesReturnDetailsViewModels)
                {
                    //var tt = $"SalSpSetSalesGrossReturnDetailsMultiItem {id},{0},{salesReturnMasterId},{model.returnQty},{model.price},{model.totalPrice},{model.productId},{model.productWiseSpecificationId},0,0,0, {model.batchNo}";

                    result = await _context.saveUpdateValueViewModels.FromSql($"SalSpSetSalesGrossReturnDetailsMultiItem {id},{0},{salesReturnMasterId},{model.returnQty},{model.price},{model.totalPrice},{model.productId},{model.productWiseSpecificationId},0,0,0,{model.batchNo},{model.vat},{model.discount}").AsNoTracking().FirstOrDefaultAsync();
                }
                return result.isSuccess;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }



        public async Task<int> SaveSalesPExpireReturnMaster(int? id, SalesProductExpireReturnMasterViewModel models)
        {
            try
            {
                var result = await _context.saveUpdateValueViewModels.FromSql($"SalSpSetCreditNoteMaster {id}, {models.productExpireReturnMasterId}, {models.returnDate}, {models.productExpireReturnId}, {models.partyId}, {models.grandTotal}").AsNoTracking().FirstOrDefaultAsync();

                foreach (var model in models.lstDetailsViewModel)
                {

                    //var s = $"SalSpSetSalesProductExpiredReturn {id}, {model.productExpireReturnId}, {model.salesInvoiceId}, {model.partyId}, {model.productWiseSpecificationId}, {model.expireReturnNumber}, {model.returnDate}, {model.amount}, {model.returnQty}, {result.isSuccess}";

                    var res = await _context.saveUpdateValueViewModels.FromSql($"SalSpSetCreditNoteDetails {id}, {model.expireReturnDetailsId}, {model.salesInvoiceId}, {model.partyId}, {model.productWiseSpecificationId}, {model.expireReturnNumber}, {model.returnDate}, {model.amount}, {model.returnQty}, {model.returnPrice}, {result.isSuccess},{model.batchNo},{model.mgfDate},{model.expireDate}").AsNoTracking().FirstOrDefaultAsync();
                    //return res.isSuccess;

                }
                return result.isSuccess;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public async Task<int> SaveSalesPExpireReturnMaster_OLD(int? id, SalesProductExpireReturnMasterViewModel models)
        {
            try
            {
                var result = await _context.saveUpdateValueViewModels.FromSql($"SalSpSetSalesProductExpiredReturnMaster {id}, {models.productExpireReturnMasterId}, {models.returnDate}").AsNoTracking().FirstOrDefaultAsync();

                foreach (var model in models.lstDetailsViewModel)
                {
                    var s = $"SalSpSetSalesProductExpiredReturn {id}, {model.productExpireReturnId}, {model.salesInvoiceId}, {model.partyId}, {model.productWiseSpecificationId}, {model.expireReturnNumber}, {model.returnDate}, {model.amount}, {model.returnQty}, {result.isSuccess}";

                    var res = await _context.saveUpdateValueViewModels.FromSql($"SalSpSetSalesProductExpiredReturn {id}, {model.productExpireReturnId}, {model.salesInvoiceId}, {model.partyId}, {model.productWiseSpecificationId}, {model.expireReturnNumber}, {model.returnDate}, {model.amount}, {model.returnQty}, {result.isSuccess}").AsNoTracking().FirstOrDefaultAsync();
                    return res.isSuccess;
                }
                return result.isSuccess;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public async Task<JsonViewModel> GetSalesGrossReturnDetailsProductByMasterId(int salesReturnMasterId)
        {
            var result = await _context.jsonViewModels.FromSql($"AccSpGetSalesGrossReturnDetailsProductByMasterId {salesReturnMasterId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> GetSalesGrossReturnSummary(int? userId, int? masterId, DateTime fDate, DateTime tDate, string depotCode, string territoryCode, int? partyId)
        {
            var result = await _context.jsonViewModels.FromSql($"SalSpGetSalesGrossReturnSummary {userId},{masterId},{fDate},{tDate},{depotCode},{territoryCode},{partyId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<JsonViewModel> GetCreditNoteReport(int? userId, int? masterId, DateTime fDate, DateTime tDate, string depotCode, string territoryCode, int? partyId)
        {
            var result = await _context.jsonViewModels.FromSql($"SalSpGetCreditNoteReport {userId},{masterId},{fDate},{tDate},{depotCode},{territoryCode},{partyId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> GetSalesGrossReturnById(int? salesReturnMasterId, int? userId)
        {
            var result = await _context.jsonViewModels.FromSql($"SalSpGetSalesGrossReturnById {salesReturnMasterId},{userId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> GetSalesGrossReturnMultiById(int? salesReturnMasterId, int? userId)
        {
            var result = await _context.jsonViewModels.FromSql($"SalSpGetSalesGrossReturnMultiById {salesReturnMasterId},{userId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> GetSalesGrossReturnDetailsInvoiceByMasterId(int salesReturnMasterId, int PartyId, int userId)
        {
            var result = await _context.jsonViewModels.FromSql($"SalSpGetSalesGrossReturnDetailsInvoiceByMasterId {userId},{salesReturnMasterId},{PartyId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<JsonViewModel> GetSalesPExpireReturnById(int? salesReturnMasterId, int? userId)
        {
            var result = await _context.jsonViewModels.FromSql($"SalSpGetCreditNoteById {salesReturnMasterId},{userId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> GetSalesPExpireReturnByPartyId(int? userId, int? partyId, int? salesinvoicId)
        {
            var result = await _context.jsonViewModels.FromSql($"GetSalesPExpireReturnByPartyId {userId},{partyId},{salesinvoicId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> PExpireReturnInvoiceIdRemoveByUncheck(int? userId, int? productExpireReturnId)
        {
            var result = await _context.jsonViewModels.FromSql($"SalSpSetPExpireReturnInvoiceIdRemoveByUncheck {userId},{productExpireReturnId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> GetSalesPExpireReturnById_OLD(int? salesReturnMasterId, int? userId)
        {
            var result = await _context.jsonViewModels.FromSql($"SalSpGetSalesProductExpireReturnById {salesReturnMasterId},{userId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<bool> DeletePExpireReturnDetailsById(string id, int detailsId)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"SalSpDeletePExpireReturnDetailsById {id}, {detailsId}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }

        public async Task<bool> SalSpDeleteSalesGrossReturn(string id, int salesReturnMasterId)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"SalSpDeleteSalesGrossReturn {id}, {salesReturnMasterId}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }
        public async Task<bool> SalSpDeleteSalesPExpireReturn(string id, int MasterId)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"SalSpDeleteSalesProductExpireReturn {id}, {MasterId}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }

        public async Task<JsonViewModel> GetSalesReturnMasterByMasterId(int? salesReturnMasterId, int? userId, DateTime? fDate, DateTime? tDate)
        {
            var result = await _context.jsonViewModels.FromSql($"SalSpGetSalesReturnMasterByMasterId {salesReturnMasterId},{userId},{fDate},{tDate}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<bool> DeleteSalesReturnMasterByMasterId(string id, int salesReturnMasterId)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"SalSpDeleteSalesReturn {id}, {salesReturnMasterId}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }

        public async Task<JsonViewModel> GetMaxSalesReturnNumber(DateTime datetime)
        {
            var result = await _context.jsonViewModels.FromSql($"SalSpGetMaxSalesReturnNumber {datetime}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<JsonViewModel> GetMaxSalesGrossReturnNumber(DateTime datetime, int? userId)
        {
            var result = await _context.jsonViewModels.FromSql($"SalSpGetMaxSalesGrossReturnNumber {datetime}, {userId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<JsonViewModel> GetMaxSalesPExpireReturnNumber(int? userId, DateTime datetime)
        {
            var result = await _context.jsonViewModels.FromSql($"SalSpGetMaxSalesProductExpireReturnNumber {userId},{datetime}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<JsonViewModel> GetManufactAndExpireDateFromStock(string BatchNo,int productWiseSpecificationId,int? userId)
        {
            var result = await _context.jsonViewModels.FromSql($"SalSpGetManufactAndExpireDateFromStock {BatchNo},{productWiseSpecificationId},{userId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        #endregion

        #region Sales Return Details

        public async Task<int> SaveSalesReturnDetails(string id, List<SalesReturnDetailsViewModel> salesReturnDetailsViewModels, int salesReturnMasterId)
        {
            try
            {
                var dStat = await _context.saveUpdateViewModels.FromSql($"SalSpDeleteSalesReturnDetails {id},{salesReturnMasterId}").AsNoTracking().FirstOrDefaultAsync();
                var result = new SaveUpdateValueViewModel();
                //foreach (SalesReturnDetailsViewModel model in salesReturnDetailsViewModels.Where(a => a.returnQty != 0))
                foreach (SalesReturnDetailsViewModel model in salesReturnDetailsViewModels)
                {
                    //if (model.unitPrice == 0)
                    //{
                    //    var txt = $"SalSpSetSalesReturnDetails {id},{0},{salesReturnMasterId},{model.salesInvDetailsId},{model.returnQty},{model.unitPrice},{model.vatPercent},{model.aitPercent},{model.discountPercent},{model.totalAmount},{model.productId},{model.productWiseSpecificationId}";
                    //}

                    result = await _context.saveUpdateValueViewModels.FromSql($"SalSpSetSalesReturnDetails {id},{0},{salesReturnMasterId},{model.salesInvDetailsId},{model.returnQty},{model.unitPrice},{model.vatPercent},{model.aitPercent},{model.discountPercent},{model.totalAmount},{model.productId},{model.productWiseSpecificationId}").AsNoTracking().FirstOrDefaultAsync();
                }
                return result.isSuccess;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public async Task<JsonViewModel> GetSalesReturnDetailsByMasterId(int salesReturnMasterId)
        {
            var result = await _context.jsonViewModels.FromSql($"AccSpGetSalesReturnDetailsByMasterId {salesReturnMasterId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> GetSalesReturnReportByMasterId(int salesReturnMasterId)
        {
            var result = await _context.jsonViewModels.FromSql($"SalSpGetSalesReturnReportByMasterId {salesReturnMasterId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        //public async Task<JsonViewModel> GetSalesPExpireReturnDetailsByMasterId(int salesReturnMasterId)
        //{
        //    var result = await _context.jsonViewModels.FromSql($"SalSpGetSalesProductExpireReturnById {salesReturnMasterId}").AsNoTracking().FirstOrDefaultAsync();
        //    return result;
        //}

        #endregion


    }
}
