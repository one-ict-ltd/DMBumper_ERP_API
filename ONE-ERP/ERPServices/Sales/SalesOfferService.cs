using Microsoft.EntityFrameworkCore;
using ONEERP.Areas.Auth.Models;
using ONEERP.Areas.Sales.Models;
using ONEERP.Data;
using ONEERP.ERPServices.Sales.Interfaces;
using ONEERP.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ONEERP.ERPServices.Sales
{
    public class SalesOfferService : ISalesOfferService
    {
        private readonly ERPDbContext _context;
        public SalesOfferService(ERPDbContext context)
        {
            _context = context;
        }

        #region SalesOfferService Master

        public async Task<bool> DeleteSalesOfferById(string id, int salesOfferId)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"SalSpDeleteSalesOffer {id}, {salesOfferId}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }

        public async Task<JsonViewModel> GetSalesOfferById(int? salesOfferId)
        {
            var result = await _context.jsonViewModels.FromSql($"SalSpGetSalesOfferJSON {salesOfferId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> GetCurrentStock(int storeId, int productWiseSpecificationId)
        {
            var result = await _context.jsonViewModels.FromSql($"SalSpGetGetCurrentStockJSON {storeId},{productWiseSpecificationId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> GetMaxSalesOfferNumber(DateTime datetime)
        {
            try
            {
            var result = await _context.jsonViewModels.FromSql($"SalSpGetMaxSalesOfferNumberJSON {datetime}").AsNoTracking().FirstOrDefaultAsync();
            return result;

            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<int> SaveSalesOffer(string id, SalesOfferViewModel model)
        {
            try
            {
                var result = await _context.saveUpdateValueViewModels.FromSql($"SalSpSetSalesOffer {id}, {model.salesOfferId}, {model.salesOfferNo}, {model.salesOfferDate}, {model.paymentDate}, {model.storeId}, {model.partyId}, {model.mobileNo}, {model.alternateMobileNo}, {model.address}, {model.totalGross}, {model.totalVat}, {model.totalAit}, {model.shippingCost}, {model.totalDiscountAmount}, {model.grandTotal}, {model.approvalStatus}, {model.isActive}").AsNoTracking().FirstOrDefaultAsync();
                return result.isSuccess;
            }
            catch (Exception ex)
            {
                throw;
            }
            
        }

        #endregion

        #region Sales Offer Details
        public async Task<bool> DeleteSalesOfferDetailsById(string id, int salesInvDetailsId)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"SalSpDeleteSalesOfferDetails {id}, {salesInvDetailsId}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }

        public async Task<JsonViewModel> GetSalesOfferDetailsByMasterId(int? salesOfferId)
        {
            var result = await _context.jsonViewModels.FromSql($"SalSpGetSalesOfferDetailsJSON {salesOfferId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> GetAllPartysByTypeId(int? partyTypeId)
        {
            var result = await _context.jsonViewModels.FromSql($"SalSpGetAllPartysByTypeJSON {partyTypeId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> GetPartyDetailsById(int? partyId)
        {
            var result = await _context.jsonViewModels.FromSql($"SalSpGetPartyDetailsByIdJSON {partyId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> GetProductSpecDetailsBySpecId(int? productSpecId)
        {
            var result = await _context.jsonViewModels.FromSql($"SalSpGetProductSpecDetailsBySpecIdJSON {productSpecId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<int> SaveSalesOfferDetails(string id, List<SalesOfferDetailsViewModel> models, int salesOfferId)
        {
            var result = new SaveUpdateValueViewModel();
            foreach (SalesOfferDetailsViewModel model in models)
            {//salesOfferId, productId, productWiseSpecificationId, OfferQty, price, vat, ait, discountAmount, Total, isActive, createdBy, createdAt	
                result = await _context.saveUpdateValueViewModels.FromSql($"SalSpSetSalesOfferDetails {id},{model.salesOfferDetailsId},{salesOfferId},{model.productId},{model.productWiseSpecificationId},{model.salesOfferQty},{model.price},{model.vat},{model.ait},{model.discountAmount},{model.Total},{model.isActive},{model.isSelect}").AsNoTracking().FirstOrDefaultAsync();
            }
            return result.isSuccess;
        }
        #endregion

        #region Reports
        public async Task<JsonViewModel> GetSalesOfferReportData(int? salesOfferId, int? partyId, DateTime? fromDate, DateTime? toDate, string userId)
        {
            var result = await _context.jsonViewModels.FromSql($"SalSpGetSalesOfferReportDataJSON {salesOfferId}, {partyId}, {fromDate}, {toDate}, {userId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> GetSalesOfferReportDataById(int? salesOfferId)//, int? partyId, DateTime? fromDate, DateTime? toDate, string userId)
        {
            var result = await _context.jsonViewModels.FromSql($"SalSpGetSalesOfferReportDataByIdJSON {salesOfferId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<JsonViewModel> GetSalesOfferListByPartyId(int? partyId)
        {
            var result = await _context.jsonViewModels.FromSql($"SalSpGetSalesOfferListJSON {partyId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        #endregion

        #region Create Sales Auto Voucher       

        //public async Task<int> CreateAutoJournalForSalesOffer(string id, SalesOfferViewModel model)
        //{
        //    var result = await _context.saveUpdateValueViewModels.FromSql($"AccSpCreateSalesOfferJournal {id},{model.grandTotal},{model.salesOfferDate},{model.salesOfferNo},{model.partyId}").AsNoTracking().FirstOrDefaultAsync();

        //    return result.isSuccess;
        //}

        #endregion
    }
}
