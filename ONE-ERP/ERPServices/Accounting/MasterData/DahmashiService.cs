using Microsoft.EntityFrameworkCore;
using ONEERP.Areas.Accounting.Models;
using ONEERP.Areas.Auth.Models;
using ONEERP.Data;
using ONEERP.ERPServices.Accounting.MasterData.Interfaces;
using ONEERP.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ONEERP.ERPServices.Accounting.MasterData
{
    public class DahmashiService : IDahmashiService
    {
        private readonly ERPDbContext _context;

        public DahmashiService(ERPDbContext context)
        {
            _context = context;
        }

        #region Company

        public async Task<JsonViewModel> GetVisaCompany(int visaCompanyId)
        {
            var result = await _context.jsonViewModels.FromSql($"AccSpGetVisaCompany {visaCompanyId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        #endregion

        #region Trade

        public async Task<JsonViewModel> GetVisaTrade(int visaTradeId)
        {
            var result = await _context.jsonViewModels.FromSql($"AccSpGetVisaTrade {visaTradeId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        #endregion

        #region Agency

        public async Task<JsonViewModel> GetVisaAgency()
        {
            var result = await _context.jsonViewModels.FromSql($"AccSpGetVisaAgency").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        #endregion

        #region Agent/Party

        public async Task<int> SaveLocalAgent(string id, PartyViewModel model)
        { 
            var result = await _context.saveUpdateValueViewModels.FromSql($"AccSpSetLocalAgent {id},{model.visaPartyId},{model.partyName},{model.officeName},{model.ownerName},{model.fatherName},{model.motherName},{model.birthdate},{model.nid},{model.gender}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }

        public async Task<int> SaveUpdateAllAgent(string id, List<PartyViewModel> partyViewModels)
        {
            var result = new SaveUpdateValueViewModel();
            foreach (PartyViewModel model in partyViewModels)
            {
                result = await _context.saveUpdateValueViewModels.FromSql($"AccSpSetLocalAgent {id},{model.visaPartyId},{model.partyName},{model.officeName},{model.ownerName},{model.fatherName},{model.motherName},{model.birthdate},{model.nid},{model.gender}").AsNoTracking().FirstOrDefaultAsync();
            }          
            return result.isSuccess;
        }


        #endregion

        #region Visa Work Order
        public async Task<JsonViewModel> getVisaInfoByWorkOrder(string workOrderNo)
        {
            var result = await _context.jsonViewModels.FromSql($"AccSpGetVisaInfoByWorkOrder {workOrderNo}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<int> SaveVisaWorkOrder(string id, VisaWorkOrderViewModel model)
        {
            var result = await _context.saveUpdateValueViewModels.FromSql($"AccSpSetVisaWorkOrder {id},{model.visaWorkOrderId},{model.workOrderNo},{model.countryId},{model.countryName},{model.cityId},{model.cityName},{model.companyId},{model.companyName},{model.issueDate},{model.expireDate},{model.visaGroupQuantity},{model.visaQuantity},{model.visaAssigned},{model.visaUnassigned}").AsNoTracking().FirstOrDefaultAsync();

            return result.isSuccess;
        }

        public async Task<JsonViewModel> GetVisaWorkOrderById(int visaWorkOrderId, string isProcessed)
        {
            var result = await _context.jsonViewModels.FromSql($"AccSpGetVisaWorkOrderJson {visaWorkOrderId},{isProcessed}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<JsonViewModel> GetDuplicateVisaWorkOrder(int visaId, string workOrderNo)
        {
            var result = await _context.jsonViewModels.FromSql($"AccSpGetDuplicateVisaWorkOrder {visaId},{workOrderNo}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<bool> DeleteVisaWorkOrderById(string id, int visaId)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"AccSpDeleteVisaWorkOrder {id},{visaId}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }

        #endregion

        #region Visa Group

        public async Task<int> SaveVisaGroup(string userid, List<VisaGroupViewModel> visaGroupViewModels, int visaId)
        {
            await _context.saveUpdateViewModels.FromSql($"AccSpDeleteVisaGroup {userid},{visaId},{0}").AsNoTracking().FirstOrDefaultAsync();
            var result = new SaveUpdateValueViewModel();
            foreach (VisaGroupViewModel model in visaGroupViewModels)
            {
                result = await _context.saveUpdateValueViewModels.FromSql($"AccSpSetVisaGroup {userid},{visaId},{model.visa_WorkOrder_Id},{model.visa_group_id},{model.group_title},{model.visa_number},{model.type},{model.assigned_visas},{model.unassigned_visas},{model.total_visas},{model.trade_id},{model.trade},{model.salary},{model.license_id},{model.license},{model.sponsor_id},{model.purchaseRate},{model.purchaseAmount},{model.serviceCharge},{model.agentCommission},{model.otherCharge},{model.hadia},{model.purchaseDate},{model.purchaseVisa}").AsNoTracking().FirstOrDefaultAsync();
            }
            return result.isSuccess;
        }

        public async Task<JsonViewModel> GetVisaGroupByWorkOrderId(int visaWorkOrderId)
        {
            var result = await _context.jsonViewModels.FromSql($"AccSpGetVisaGroupJson {visaWorkOrderId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<bool> DeleteVisaGroupById(string id, int visaGroupId)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"AccSpDeleteVisaGroup {id},{0},{visaGroupId}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }

        #endregion

        #region Create Auto Journal Voucher For Work Order

        public async Task<int> CreateAutoJournalForWorkOrder(string id, VisaWorkOrderViewModel model)
        {
            var result = await _context.saveUpdateValueViewModels.FromSql($"AccSpCreateVisaWorkOrderJournal {id},{model.purchaseAmount},{model.issueDate},{model.visaId},{model.companyId},{model.workOrderNo}").AsNoTracking().FirstOrDefaultAsync();

            return result.isSuccess;
        }

        #endregion

        #region Visa Sales/PassengerInfo

        public async Task<int> SaveVisaSales(string id, VisaSalesViewModel model)
        {
            var result = await _context.saveUpdateValueViewModels.FromSql($"AccSpSetVisaSales {id},{model.visaSaleId},{model.candidateId},{model.candidateName},{model.candidateCode},{model.candidateStatus},{model.passportNo},{model.agentId},{model.agentName},{model.companyId},{model.companyName},{model.groupId},{model.groupName},{model.tradeId},{model.tradeName},{model.countryId},{model.countryName},{model.cityId},{model.cityName},{model.workOrderId},{model.workOrderNo},{model.visaNo},{model.sponsorId},{model.contact},{model.reference},{model.assignRemarks},{model.unAssignRemarks},{model.salesAmount},{model.agentCommission},{model.additionalCharge},{model.specialDiscount},{model.salesDate},{model.netAmount}").AsNoTracking().FirstOrDefaultAsync();

            return result.isSuccess;
        }

        public async Task<JsonViewModel> GetVisaSalesById(int visaSaleId, string isProcessed)
        {
            var result = await _context.jsonViewModels.FromSql($"AccSpGetVisaSalesJson {visaSaleId},{isProcessed}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<JsonViewModel> GetDuplicateVisaSales(int visaSaleId, string passportNo)
        {
            var result = await _context.jsonViewModels.FromSql($"AccSpGetDuplicateVisaSales {visaSaleId},{passportNo}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<bool> DeleteVisaSalesById(string id, int visaSaleId)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"AccSpDeleteVisaSales {id},{visaSaleId}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }

        #endregion

        #region Create Auto Voucher For Visa Sales

        public async Task<int> CreateAutoVoucherForSales(string id, VisaSalesViewModel model)
        {
            var result = await _context.saveUpdateValueViewModels.FromSql($"AccSpCreateVisaSalesVoucher {id},{model.netAmount},{model.visaSaleId},{model.agentId},{model.agentName},{model.passportNo},{model.salesDate}").AsNoTracking().FirstOrDefaultAsync();

            return result.isSuccess;
        }

        #endregion

        #region Create Auto Voucher For Visa Sales Two

        public async Task<int> CreateAutoVoucherForSalesTwo(string id, VisaSalesViewModel model)
        {
            var result = await _context.saveUpdateValueViewModels.FromSql($"AccSpCreateVisaSalesVoucher_Two {id},{model.workOrderId},{model.visaSaleId},{model.groupId},{model.passportNo},{model.salesDate}").AsNoTracking().FirstOrDefaultAsync();

            return result.isSuccess;
        }

        #endregion

        #region Report

        public async Task<JsonViewModel> RptVisaWorkOrder(int visaId)
        {
            var result = await _context.jsonViewModels.FromSql($"AccSpRptVisaWorkOrder {visaId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<JsonViewModel> RptVisaStock(int visaWorkOrderId, int agencyId)
        {
            var result = await _context.jsonViewModels.FromSql($"AccSpRptVisaStock {visaWorkOrderId},{agencyId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<JsonViewModel> RptVisaPurchaseByDate(int tradeId, int companyId, DateTime fromDate, DateTime toDate)
        {
            var result = await _context.jsonViewModels.FromSql($"AccSpRptVisaPurchaseByDate {tradeId},{companyId},{fromDate},{toDate}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<JsonViewModel> RptVisaSalesByDate(int tradeId, int companyId, int agentId, DateTime fromDate, DateTime toDate)
        {
            var result = await _context.jsonViewModels.FromSql($"AccSpRptVisaSalesByDate {tradeId},{companyId},{agentId},{fromDate},{toDate}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        #endregion
    }
}
