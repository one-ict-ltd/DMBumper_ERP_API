using ONEERP.Areas.Sales.Models;
using ONEERP.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ONEERP.ERPServices.Sales.Interfaces
{
    public interface ISalesReturnService
    {
        #region Sales Return Master
        Task<int> SaveSalesReturnMaster(string id, SalesReturnMasterViewModel model);
        Task<JsonViewModel> GetSalesReturnMasterByMasterId(int? salesReturnMasterId, int? userId, DateTime? fDate, DateTime? tDate);       
        Task<bool> DeleteSalesReturnMasterByMasterId(string id, int salesReturnMasterId);
        Task<JsonViewModel> GetMaxSalesReturnNumber(DateTime datetime);
        Task<JsonViewModel> GetManufactAndExpireDateFromStock(string BatchNo,int productWiseSpecificationId, int? userId);

        #endregion

        #region Sales Return Details

        Task<int> SaveSalesReturnDetails(string id, List<SalesReturnDetailsViewModel> salesReturnDetailsViewModels, int salesReturnMasterId);
        Task<JsonViewModel> GetSalesReturnDetailsByMasterId(int salesReturnMasterId);
        Task<JsonViewModel> GetSalesReturnReportByMasterId(int salesReturnMasterId);
        //Task<JsonViewModel> GetSalesPExpireReturnDetailsByMasterId(int salesReturnMasterId);

        #endregion

        Task<int> SaveSalesGrossReturnMaster(string id, SalesReturnMasterViewModel model);
        Task<int> SaveSalesGrossReturnMultiMaster(string id, SalesReturnMasterViewModel model);
        Task<int> SaveSalesGrossReturnDetailsMultiItems(string id, List<SalesReturnDetailsViewModel> salesReturnDetailsViewModels, int salesReturnMasterId);
        Task<int> SaveSalesGrossReturnDetailsMultiInvoice(string id, List<SalesGrossReturnInvoiceViewModel> salesReturnDetailsViewModels, int salesReturnMasterId);

        Task<int> SaveSalesPExpireReturnMaster(int? id, SalesProductExpireReturnMasterViewModel model);
        Task<JsonViewModel> GetSalesGrossReturnById(int? salesReturnMasterId, int? userId);
        Task<JsonViewModel> GetSalesGrossReturnMultiById(int? salesReturnMasterId, int? userId);
        Task<JsonViewModel> GetSalesGrossReturnDetailsProductByMasterId(int salesReturnMasterId);
        Task<JsonViewModel> GetSalesGrossReturnDetailsInvoiceByMasterId(int salesReturnMasterId, int PartyId, int userId);
        Task<JsonViewModel> GetSalesPExpireReturnById(int? salesReturnMasterId, int? userId);
        Task<JsonViewModel> GetSalesGrossReturnSummary(int? userId, int? masterId, DateTime fDate, DateTime tDate, string depotCode, string territoryCode, int? partyId);
        Task<JsonViewModel> GetCreditNoteReport(int? userId, int? masterId, DateTime fDate, DateTime tDate, string depotCode, string territoryCode, int? partyId);
        Task<JsonViewModel> GetSalesPExpireReturnByPartyId(int? userId, int? partyId, int? salesinvoicId);
        Task<JsonViewModel> PExpireReturnInvoiceIdRemoveByUncheck(int? userId, int? productExpireReturnId);
        Task<bool> DeletePExpireReturnDetailsById(string id, int detailsId);
        Task<bool> SalSpDeleteSalesGrossReturn(string id, int salesReturnMasterId);
        Task<bool> SalSpDeleteSalesPExpireReturn(string id, int salesReturnMasterId);
        Task<JsonViewModel> GetMaxSalesGrossReturnNumber(DateTime datetime, int? userId);
        Task<JsonViewModel> GetMaxSalesPExpireReturnNumber(int? userId,DateTime datetime);

    }
}
