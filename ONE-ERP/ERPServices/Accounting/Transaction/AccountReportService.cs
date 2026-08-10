using Microsoft.EntityFrameworkCore;
using ONEERP.Areas.Accounting.Models;
using ONEERP.Data;
using ONEERP.ERPServices.Accounting.Transaction.Interfaces;
using ONEERP.Models;
using System;
using System.Threading.Tasks;

namespace ONEERP.ERPServices.Accounting.Transaction
{
    public class AccountReportService : IAccountReportService
    {
        private readonly ERPDbContext _context;

        public AccountReportService(ERPDbContext context)
        {
            _context = context;
        }

        #region Note Parent

        public async Task<JsonViewModel> GetNoteParentByType(string noteType)
        {
            var result = await _context.jsonViewModels.FromSql($"AccSpGetNoteParentByType {noteType}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        #endregion

        #region  Note Master

        public async Task<bool> SaveNoteMaster(string id, NoteMasterViewModel noteMasterViewModel)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"CmnSpSetNoteMaster {id},{noteMasterViewModel.noteMasterId},{noteMasterViewModel.noteParentId},{noteMasterViewModel.noteName},{noteMasterViewModel.noteNo},{noteMasterViewModel.sortOrder},{noteMasterViewModel.companyId},{noteMasterViewModel.sbuId},{noteMasterViewModel.isActive}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }

        public async Task<JsonViewModel> GetNoteMasterByIdJson(int companyId, int sbuId, int noteMasterId, string noteType)
        {
            var result = await _context.jsonViewModels.FromSql($"AccSpGetNoteMaster {companyId},{sbuId},{noteMasterId},{noteType}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

       

        public async Task<bool> DeleteNoteMasterById(string id, int noteMasterId)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"AccSpDeleteNoteMaster {id},{noteMasterId}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }
        public async Task<JsonViewModel> GetNoteMasterByIdJsonNew(string noteType)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"AccSpGetNoteMasterNew {noteType}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch(Exception ex)
            {
                throw ex;
            }
            
        }

        #endregion

        #region  Note Details

        public async Task<bool> SaveNoteDetails(string id, NoteDetailsViewModel noteDetailsViewModel)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"CmnSpSetNoteDetails {id},{noteDetailsViewModel.noteDetailsId},{noteDetailsViewModel.noteMasterId},{noteDetailsViewModel.ledgerId},{noteDetailsViewModel.sortOrder},{noteDetailsViewModel.isActive}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }

        public async Task<JsonViewModel> GetNoteDetailsByIdJson(int noteDetailsId, string noteType)
        {
            var result = await _context.jsonViewModels.FromSql($"AccSpGetNoteDetails {noteDetailsId},{noteType}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<JsonViewModel> GetDuplicateNoteDetail(int noteDetailsId, int ledgerId, string noteType)
        {
            var result = await _context.jsonViewModels.FromSql($"AccSpGetDuplicateNoteDetail {noteDetailsId},{ledgerId},{noteType}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<bool> DeleteNoteDetailsById(string id, int noteDetailsId)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"AccSpDeleteNoteDetails {id},{noteDetailsId}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }

        #endregion

        #region Report
        public async Task<JsonViewModel> RptVoucherPreview(int vmasterId)
        {
            var result = await _context.jsonViewModels.FromSql($"AccSpRptVoucherPreview {vmasterId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<JsonViewModel> RptAccountGroupBook(int companyId, int sbuId, int accountGroupId, DateTime fromDate, DateTime toDate, string reportType)
        {
            var result = await _context.jsonViewModels.FromSql($"AccSpRptAccountGroupBook {companyId},{sbuId},{accountGroupId},{fromDate},{toDate},{reportType}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<JsonViewModel> RptAccountGroupBookWithNature(int companyId, int sbuId, int accountGroupId, DateTime fromDate, DateTime toDate, string reportType,int natureId)
        {
            var result = await _context.jsonViewModels.FromSql($"AccSpRptAccountGroupBookWithNature {companyId},{sbuId},{accountGroupId},{fromDate},{toDate},{reportType},{natureId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<JsonViewModel> RptLedgerBook(int companyId, int sbuId, int ledgerId, int partyId, DateTime fromDate, DateTime toDate, string reportType)
        {
            var result = await _context.jsonViewModels.FromSql($"AccSpRptLedgerBook {companyId},{sbuId},{ledgerId},{partyId},{fromDate},{toDate},{reportType}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<JsonViewModel> AccSpRptLedgerBookByCodeRange(int companyId, int sbuId, int ledgerId, int partyId, DateTime fromDate, DateTime toDate, string reportType,int LedgerFrom,int LedgerTo)
        {
            var result = await _context.jsonViewModels.FromSql($"AccSpRptLedgerBookByCodeRange {companyId},{sbuId},{ledgerId},{partyId},{fromDate},{toDate},{reportType},{LedgerFrom},{LedgerTo}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> RptCostCentreWiseReport(int companyId, int sbuId,int CostCentreId, int costCostCentreLocationId, int costCentreCategoryId, int ledgerId,int natureId,int groupId, DateTime fromDate, DateTime toDate)
        {
            var result = await _context.jsonViewModels.FromSql($"getCostCentreWiseData {companyId},{sbuId},{CostCentreId},{ledgerId},{natureId},{groupId},{fromDate},{toDate},{costCostCentreLocationId},{costCentreCategoryId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> RptCostCentreWiseMonthlyReport(int companyId, int sbuId,int CostCentreId, int costCostCentreLocationId, int costCentreCategoryId, int ledgerId,int natureId,int groupId, DateTime fromDate, DateTime toDate)
        {
            var result = await _context.jsonViewModels.FromSql($"getCostCentreWiseDataMonthwise {companyId},{sbuId},{CostCentreId},{ledgerId},{natureId},{groupId},{fromDate},{toDate},{costCostCentreLocationId},{costCentreCategoryId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<JsonViewModel> RptPartyLedgerBook(int companyId, int sbuId,int partyTypeId, int partyId, DateTime fromDate, DateTime toDate)
        {
            var result = await _context.jsonViewModels.FromSql($"AccSpRptPartyLedgerBook {companyId},{sbuId},{partyTypeId},{partyId},{fromDate},{toDate}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<JsonViewModel> RptDayBook(int companyId, int sbuId, int vouchertypeId, int empId, DateTime fromDate, DateTime toDate, decimal amount, string remarks)
        {
            var result = await _context.jsonViewModels.FromSql($"AccSpRptDayBook {companyId},{sbuId},{vouchertypeId},{empId},{fromDate},{toDate},{amount},{remarks}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<JsonViewModel> GetRxReportResult(int companyId, int vouchertypeId, int empId, DateTime fromDate, DateTime toDate)
        {
            var result = await _context.jsonViewModels.FromSql($"CmnSpGetRxReportResult {empId},{vouchertypeId},{companyId},{fromDate},{toDate}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<JsonViewModel> RptCashBook(int companyId, int sbuId, int ledgerId, DateTime fromDate, DateTime toDate)
        {
            var result = await _context.jsonViewModels.FromSql($"AccSpRptCashBook {companyId},{sbuId},{ledgerId},{fromDate},{toDate}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<JsonViewModel> RptBankBook(int companyId, int sbuId, int ledgerId, DateTime fromDate, DateTime toDate)
        {
            var result = await _context.jsonViewModels.FromSql($"AccSpRptBankBook {companyId},{sbuId},{ledgerId},{fromDate},{toDate}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<JsonViewModel> RptTrialBalance(int companyId, int sbuId, int accountGroupId, DateTime fromDate, DateTime toDate,string rptType)
        {
            var result = await _context.jsonViewModels.FromSql($"AccSpRptTrialBalance {companyId},{sbuId},{accountGroupId},{fromDate},{toDate},{rptType}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<JsonViewModel> RptTrialBalancewithpreCode(int companyId, int sbuId, int accountGroupId, DateTime fromDate, DateTime toDate,string rptType)
        {
            var result = await _context.jsonViewModels.FromSql($"AccSpRptTrialBalancewithpreCode {companyId},{sbuId},{accountGroupId},{fromDate},{toDate},{rptType}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<JsonViewModel> RptIncomeStatement(int companyId, int sbuId, DateTime fromDate, DateTime toDate)
        {
            var result = await _context.jsonViewModels.FromSql($"AccSpRptIncomeStatement {companyId},{sbuId},{fromDate},{toDate}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<JsonViewModel> RptIncomeStatementGrossFormat(int companyId, int sbuId, DateTime fromDate, DateTime toDate)
        {
            var result = await _context.jsonViewModels.FromSql($"AccSpRptIncomeStatement_Gross {companyId},{sbuId},{fromDate},{toDate}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> RptIncomeStatementIFRS(int companyId, int sbuId, int noteMasterId, DateTime fromDate, DateTime toDate, string rptType)
        {
            var result = await _context.jsonViewModels.FromSql($"AccSpRptIncomeStatement_IFRS {companyId},{sbuId},{noteMasterId},{fromDate},{toDate},{rptType}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<JsonViewModel> RptPaymentReceived(int companyId, int sbuId, DateTime fromDate, DateTime toDate)
        {
            var result = await _context.jsonViewModels.FromSql($"AccSpRptPaymentReceive {companyId},{sbuId},{Convert.ToDateTime(fromDate).ToString("yyyyMMdd")},{Convert.ToDateTime(toDate).ToString("yyyyMMdd")}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<JsonViewModel> RptPaymentReceivedNew(int companyId, int sbuId, int ledgerId, DateTime fromDate, DateTime toDate)
        {
            var result = await _context.jsonViewModels.FromSql($"AccSpRptPaymentReceiveNewJson {companyId},{sbuId},{ledgerId},{fromDate},{toDate}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<JsonViewModel> RptBalanceSheet(int companyId, int sbuId, DateTime fromDate, DateTime toDate)
        {
            var result = await _context.jsonViewModels.FromSql($"AccSpRptBalanceSheet {companyId},{sbuId},{fromDate},{toDate}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<JsonViewModel> RptBalanceSheetTwo(int companyId, int sbuId, DateTime fromDate, DateTime toDate)
        {
            var result = await _context.jsonViewModels.FromSql($"AccSpRptBalanceSheetTwo {companyId},{sbuId},{fromDate},{toDate}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> RptBalanceSheetDetails(int companyId, int sbuId, int noteMasterId, DateTime fromDate, DateTime toDate, string rptType)
        {
            var result = await _context.jsonViewModels.FromSql($"AccSpRptBalanceSheetDetails {companyId},{sbuId},{noteMasterId},{fromDate},{toDate},{rptType}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> getRptBalanceSheetCoa(int companyId, int sbuId, DateTime fromDate, DateTime toDate, int level)
        {
            var result = await _context.jsonViewModels.FromSql($"AccSpRptBalanceSheetDetailsDirect {companyId},{sbuId},{fromDate},{toDate},{level}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> RptBalanceCostOfGoodsSold(int companyId, int sbuId, int noteMasterId, DateTime fromDate, DateTime toDate, string rptType)
        {
            var result = await _context.jsonViewModels.FromSql($"AccSpRptCostofGoodsSold {companyId},{sbuId},{noteMasterId},{fromDate},{toDate},{rptType}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> RptCOGS(int companyId, int sbuId, int noteMasterId, DateTime fromDate, DateTime toDate)
        {
            var result = await _context.jsonViewModels.FromSql($"AccSpRptCOGS {companyId},{sbuId},{noteMasterId},{fromDate},{toDate}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> RptCOGSPrevious(int companyId, int sbuId, int noteMasterId, DateTime fromDate, DateTime toDate)
        {
            var result = await _context.jsonViewModels.FromSql($"AccSpRptCOGSPreviousYear {companyId},{sbuId},{noteMasterId},{fromDate},{toDate}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> RptBalanceCostOfGoodsSoldByParentId(int companyId, int sbuId, int noteMasterId, DateTime fromDate, DateTime toDate, string rptType)
        {
            var result = await _context.jsonViewModels.FromSql($"AccSpRptCostofGoodsSoldDetailsByParentId {companyId},{sbuId},{noteMasterId},{fromDate},{toDate},{rptType}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> RptCashFlowDirect(int companyId, int sbuId, DateTime fromDate, DateTime toDate)
        {
            var result = await _context.jsonViewModels.FromSql($"AccSpRptCashFlowDirect {companyId},{sbuId},{fromDate},{toDate}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> RptCashFlowInDirect(int companyId, int sbuId, DateTime fromDate, DateTime toDate)
        {
            var result = await _context.jsonViewModels.FromSql($"AccSpRptCashFlowInDirect {companyId},{sbuId},{fromDate},{toDate}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<JsonViewModel> RptOwnersEquityStatement(int companyId, int sbuId, DateTime fromDate, DateTime toDate)
        {
            var result = await _context.jsonViewModels.FromSql($"AccSpRptOwnersEquity {companyId},{sbuId},{fromDate},{toDate}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<JsonViewModel> RptProfitLossForOwnersEquityStatement(int companyId, int sbuId, DateTime fromDate, DateTime toDate)
        {
            var result = await _context.jsonViewModels.FromSql($"AccSpRptIncomeStatement_ForOEquity {companyId},{sbuId},{fromDate},{toDate}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<JsonViewModel> RptRatioAnalysis(int companyId, int sbuId, DateTime fromDate, DateTime toDate,string noteName)
        {
            var result = await _context.jsonViewModels.FromSql($"AccSpRptRatioAnalysis {companyId},{sbuId},{fromDate},{toDate},{noteName}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<JsonViewModel> RptWithDrawings(int companyId, int sbuId, DateTime fromDate, DateTime toDate)
        {
            var result = await _context.jsonViewModels.FromSql($"AccSpRptDrawing {companyId},{sbuId},{fromDate},{toDate}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<JsonViewModel> RptVoucherListByDate(int companyId, int sbuId, int voucherTypeId, int ledgerTypeId, DateTime fromDate, DateTime toDate)
        {
            var result = await _context.jsonViewModels.FromSql($"AccSpRptVoucherListByDate {companyId},{sbuId},{voucherTypeId},{ledgerTypeId},{Convert.ToDateTime(fromDate).ToString("yyyyMMdd")},{Convert.ToDateTime(toDate).ToString("yyyyMMdd")}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        #endregion

        #region Dashboard

        public async Task<JsonViewModel> DashboardDailyVoucher(string filterType, int voucherTypeId, string dateType)
        {
            var result = await _context.jsonViewModels.FromSql($"AccSpDashboardDailyVoucher {filterType},{voucherTypeId},{dateType}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<JsonViewModel> DashboardTotalVoucherByType()
        {
            var result = await _context.jsonViewModels.FromSql($"AccSpDashboardTotalVoucherByType").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<JsonViewModel> DashboardGroupNaturePercent()
        {
            var result = await _context.jsonViewModels.FromSql($"AccSpDashboardGroupNaturePercent").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        #endregion
    }
}
