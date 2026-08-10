using ONEERP.Areas.Sales.Models;
using ONEERP.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ONEERP.ERPServices.Sales.Interfaces
{
    public interface ISalesOfferService
    {
        #region SalesOfferService Master

        Task<int> SaveSalesOffer(string id, SalesOfferViewModel model);
        Task<JsonViewModel> GetSalesOfferById(int? salesOfferId);
        //Task<JsonViewModel> GetAllProductReqNumberBySbuId(int sbuId);
        Task<JsonViewModel> GetMaxSalesOfferNumber(DateTime datetime);
        Task<JsonViewModel> GetCurrentStock(int storeId, int productWiseSpecificationId);
        Task<bool> DeleteSalesOfferById(string id, int salesOfferId);

        #endregion

        #region SalesOfferService Details

        Task<int> SaveSalesOfferDetails(string id, List<SalesOfferDetailsViewModel> Model, int salesOfferId);
        Task<JsonViewModel> GetSalesOfferDetailsByMasterId(int? salesOfferId);
        Task<JsonViewModel> GetAllPartysByTypeId(int? partyTypeId);
        Task<JsonViewModel> GetPartyDetailsById(int? partyId);
        Task<JsonViewModel> GetProductSpecDetailsBySpecId(int? productSpecId);
        Task<bool> DeleteSalesOfferDetailsById(string id, int salesInvDetailsId);

        #endregion

        #region Reports

        Task<JsonViewModel> GetSalesOfferListByPartyId(int? partyId);
        Task<JsonViewModel> GetSalesOfferReportData(int? salesOfferId, int? partyId, DateTime? fromDate, DateTime? toDate, string userId);
        Task<JsonViewModel> GetSalesOfferReportDataById(int? salesOfferId);//, int? partyId, DateTime? fromDate, DateTime? toDate, string userId);

        #endregion

        #region Create Sales Auto Voucher  

        //Task<int> CreateAutoJournalForSalesOffer(string id, SalesOfferViewModel model);

        #endregion
    }
}
