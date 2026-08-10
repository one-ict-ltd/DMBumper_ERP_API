
using ONEERP.Areas.Accounting.Models;
using ONEERP.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ONEERP.ERPServices.Accounting.Transaction.Interfaces
{
    public interface IAccountReportService
    {

        #region Note Parent

        Task<JsonViewModel> GetNoteParentByType(string noteType);

        #endregion

        #region Note Master
        Task<bool> SaveNoteMaster(string id, NoteMasterViewModel noteMasterViewModel);
        Task<JsonViewModel> GetNoteMasterByIdJson(int companyId, int sbuId, int noteMasterId, string noteType);
        Task<bool> DeleteNoteMasterById(string id, int noteMasterId);
        Task<JsonViewModel> GetNoteMasterByIdJsonNew(string noteType);

        #endregion

        #region Note Details
        Task<bool> SaveNoteDetails(string id, NoteDetailsViewModel  noteDetailsViewModel);
        Task<JsonViewModel> GetNoteDetailsByIdJson(int noteDetailsId, string noteType);
        Task<JsonViewModel> GetDuplicateNoteDetail(int noteDetailsId, int ledgerId, string noteType);
        Task<bool> DeleteNoteDetailsById(string id, int noteDetailsId);

        #endregion      

        #region Report
        Task<JsonViewModel> RptVoucherPreview(int vmasterId);
        Task<JsonViewModel> RptAccountGroupBook(int companyId, int sbuId, int accountGroupId, DateTime fromDate, DateTime toDate, string reportType);
        Task<JsonViewModel> RptLedgerBook(int companyId, int sbuId, int ledgerId, int partyId, DateTime fromDate, DateTime toDate, string reportType);
        Task<JsonViewModel> RptPartyLedgerBook(int companyId, int sbuId, int partyTypeId, int partyId, DateTime fromDate, DateTime toDate);
        Task<JsonViewModel> RptDayBook(int companyId, int sbuId, int vouchertypeId, int empId, DateTime fromDate, DateTime toDate, decimal amount, string remarks);
        Task<JsonViewModel> RptCashBook(int companyId, int sbuId, int ledgerId, DateTime fromDate, DateTime toDate);
        Task<JsonViewModel> RptBankBook(int companyId, int sbuId, int ledgerId, DateTime fromDate, DateTime toDate);
        Task<JsonViewModel> RptTrialBalance(int companyId, int sbuId, int accountGroupId, DateTime fromDate, DateTime toDate, string rptType);
        Task<JsonViewModel> RptIncomeStatement(int companyId, int sbuId, DateTime fromDate, DateTime toDate);
        Task<JsonViewModel> RptIncomeStatementGrossFormat(int companyId, int sbuId, DateTime fromDate, DateTime toDate);
        Task<JsonViewModel> RptIncomeStatementIFRS(int companyId, int sbuId, int noteMasterId, DateTime fromDate, DateTime toDate, string rptType);
        Task<JsonViewModel> RptPaymentReceived(int companyId, int sbuId, DateTime fromDate, DateTime toDate);
        Task<JsonViewModel> RptPaymentReceivedNew(int companyId, int sbuId, int ledgerId, DateTime fromDate, DateTime toDate);
        Task<JsonViewModel> RptBalanceSheet(int companyId, int sbuId, DateTime fromDate, DateTime toDate);
        Task<JsonViewModel> RptBalanceSheetTwo(int companyId, int sbuId, DateTime fromDate, DateTime toDate);
        Task<JsonViewModel> RptBalanceSheetDetails(int companyId, int sbuId, int noteMasterId, DateTime fromDate, DateTime toDate, string rptType);
        Task<JsonViewModel> RptCashFlowDirect(int companyId, int sbuId, DateTime fromDate, DateTime toDate);
        Task<JsonViewModel> RptCashFlowInDirect(int companyId, int sbuId, DateTime fromDate, DateTime toDate);
        Task<JsonViewModel> RptOwnersEquityStatement(int companyId, int sbuId, DateTime fromDate, DateTime toDate);
        Task<JsonViewModel> RptProfitLossForOwnersEquityStatement(int companyId, int sbuId, DateTime fromDate, DateTime toDate);
        Task<JsonViewModel> RptRatioAnalysis(int companyId, int sbuId, DateTime fromDate, DateTime toDate, string noteName);
        Task<JsonViewModel> RptWithDrawings(int companyId, int sbuId, DateTime fromDate, DateTime toDate);
        Task<JsonViewModel> RptVoucherListByDate(int companyId, int sbuId, int voucherTypeId, int ledgerTypeId, DateTime fromDate, DateTime toDate);
        //Task<JsonViewModel> RptCostCentreWiseReport(int companyId, int sbuId, int CostCentreId, int ledgerId, DateTime fromDate, DateTime toDate);
        Task<JsonViewModel> RptCostCentreWiseReport(int companyId, int sbuId, int CostCentreId, int costCostCentreLocationId, int costCentreCategoryId, int ledgerId, int natureId, int groupId, DateTime fromDate, DateTime toDate);
        Task<JsonViewModel> RptCostCentreWiseMonthlyReport(int companyId, int sbuId, int CostCentreId, int costCostCentreLocationId, int costCentreCategoryId, int ledgerId, int natureId, int groupId, DateTime fromDate, DateTime toDate);
        Task<JsonViewModel> RptBalanceCostOfGoodsSold(int companyId, int sbuId, int noteMasterId, DateTime fromDate, DateTime toDate, string rptType);
        Task<JsonViewModel> RptBalanceCostOfGoodsSoldByParentId(int companyId, int sbuId, int noteMasterId, DateTime fromDate, DateTime toDate, string rptType);
        Task<JsonViewModel> RptTrialBalancewithpreCode(int companyId, int sbuId, int accountGroupId, DateTime fromDate, DateTime toDate, string rptType);
        Task<JsonViewModel> RptCOGS(int companyId, int sbuId, int noteMasterId, DateTime fromDate, DateTime toDate);
        Task<JsonViewModel> RptCOGSPrevious(int companyId, int sbuId, int noteMasterId, DateTime fromDate, DateTime toDate);
        Task<JsonViewModel> AccSpRptLedgerBookByCodeRange(int companyId, int sbuId, int ledgerId, int partyId, DateTime fromDate, DateTime toDate, string reportType, int LedgerFrom, int LedgerTo);
        Task<JsonViewModel> getRptBalanceSheetCoa(int companyId, int sbuId, DateTime fromDate, DateTime toDate, int level);
        Task<JsonViewModel> GetRxReportResult(int companyId, int vouchertypeId, int empId, DateTime fromDate, DateTime toDate);
        Task<JsonViewModel> RptAccountGroupBookWithNature(int companyId, int sbuId, int accountGroupId, DateTime fromDate, DateTime toDate, string reportType, int natureId);
        #endregion

        #region Dashboard
        Task<JsonViewModel> DashboardDailyVoucher(string filterType, int voucherTypeId, string dateType);
        Task<JsonViewModel> DashboardTotalVoucherByType();
        Task<JsonViewModel> DashboardGroupNaturePercent();

        #endregion
    }
}
