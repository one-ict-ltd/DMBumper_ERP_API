using ONEERP.Areas.Accounting.Models;
using ONEERP.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ONEERP.ERPServices.Accounting.MasterData.Interfaces
{
    public interface IDahmashiService
    {
        #region Company

        Task<JsonViewModel> GetVisaCompany(int visaCompanyId);

        #endregion

        #region Trade

        Task<JsonViewModel> GetVisaTrade(int visaTradeId);

        #endregion

        #region Agency

        Task<JsonViewModel> GetVisaAgency();

        #endregion

        #region Agent/Party
        Task<int> SaveLocalAgent(string id, PartyViewModel model);
        Task<int> SaveUpdateAllAgent(string id, List<PartyViewModel> partyViewModels);

        #endregion

        #region Visa Work Order

        Task<JsonViewModel> getVisaInfoByWorkOrder(string workOrderNo);
        Task<int> SaveVisaWorkOrder(string id, VisaWorkOrderViewModel visaWorkOrderViewModel);
        Task<JsonViewModel> GetVisaWorkOrderById(int visaWorkOrderId, string isProcessed);
        Task<JsonViewModel> GetDuplicateVisaWorkOrder(int visaId, string workOrderNo);
        Task<bool> DeleteVisaWorkOrderById(string id, int visaId);

        #endregion

        #region Visa Group

        Task<int> SaveVisaGroup(string id, List<VisaGroupViewModel> visaGroupViewModels, int visaWorkOrderId);
        Task<JsonViewModel> GetVisaGroupByWorkOrderId(int visaWorkOrderId);
        Task<bool> DeleteVisaGroupById(string id, int visaGroupId);


        #endregion

        #region Create Auto Journal Voucher For Work Order

        Task<int> CreateAutoJournalForWorkOrder(string id, VisaWorkOrderViewModel visaWorkOrderViewModel);

        #endregion

        #region Visa Sales/PassengerInfo

        Task<int> SaveVisaSales(string id, VisaSalesViewModel model);
        Task<JsonViewModel> GetVisaSalesById(int visaSaleId, string isProcessed);
        Task<JsonViewModel> GetDuplicateVisaSales(int visaSaleId, string passportNo);
        Task<bool> DeleteVisaSalesById(string id, int visaSaleId);

        #endregion

        #region Create Auto Voucher For Visa Sales

        Task<int> CreateAutoVoucherForSales(string id, VisaSalesViewModel model);

        #endregion

        #region Create Auto Voucher For Visa Sales Two

        Task<int> CreateAutoVoucherForSalesTwo(string id, VisaSalesViewModel model);

        #endregion

        #region Report

        Task<JsonViewModel> RptVisaWorkOrder(int visaId);
        Task<JsonViewModel> RptVisaStock(int visaWorkOrderId, int agencyId);
        Task<JsonViewModel> RptVisaPurchaseByDate(int tradeId, int companyId,DateTime fromDate, DateTime toDate);
        Task<JsonViewModel> RptVisaSalesByDate(int tradeId, int companyId, int agentId, DateTime fromDate, DateTime toDate);

        #endregion

    }
}
