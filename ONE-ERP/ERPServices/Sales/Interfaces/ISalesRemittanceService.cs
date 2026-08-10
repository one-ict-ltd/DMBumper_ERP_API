using ONEERP.Areas.Sales.Models;
using ONEERP.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ONEERP.ERPServices.Sales.Interfaces
{
    public interface ISalesRemittanceService
    {
        #region SalesInvoiceService Master

        Task<int> SaveSalesRemittance(string id, SalesRemittanceMasterViewModel model);
        Task<int> UpdateHasRemittanceOfCollectionMaster(string id, ICollection<HasRemittanceOfCollectionMasterUpdateViewModel> model);
        Task<int> DeleteRemittance(string id,int remittanceId);

        Task<int> SaveSalesRemittanceSlips(string id, List<SalesRemittanceSlipViewModel> salesRemittanceSlips, int? remittanceId);

        Task<JsonViewModel> GetSalesRemittanceById(int? remittanceId);
        Task<JsonViewModel> GetSalesRemittanceList(int? remittanceId, int? userId, DateTime? fDate, DateTime? tDate);
        Task<JsonViewModel> GetSalesRemittanceSummary(string depotCode, int? userId, DateTime? fDate, DateTime? tDate, int? bankId);
        Task<JsonViewModel> GetOplTranNoStatus(string opltranNo, int? remittanceId);
        Task<JsonViewModel> GetCashinHandByDepotCode(int? userId, string depotCode, DateTime? qDate);
        Task<JsonViewModel> GetDepotWiseCollections(int? userId, string depotCode);
        Task<JsonViewModel> GetRemittanceSlipsJson(int? remittanceId, int? remittanceSlipId);
        Task<ICollection<SalRemittanceViewModel>> CheckRemittanceTransactionNumber(SalesRemittanceMasterViewModel model);


        #endregion SalesInvoiceService Master
    }
}