using Microsoft.EntityFrameworkCore;
using ONEERP.Areas.Auth.Models;
using ONEERP.Areas.Purchase;
using ONEERP.Areas.Purchase.Models;
using ONEERP.Data;
using ONEERP.ERPServices.Purchase.Interfaces;
using ONEERP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.ERPServices.Purchase
{
    public class ImportService : IImportService
    {

        private readonly ERPDbContext _context;
        public ImportService(ERPDbContext context)
        {
            _context = context;
        }



        public async Task<bool> DeleteChargeHeadById(string id, int Id)
        {
            try
            {
                var result = await _context.saveUpdateViewModels.FromSql($"PurSpDeleteChargeHead {id},{Id}").AsNoTracking().FirstOrDefaultAsync();
                return result.isSuccess;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<JsonViewModel> GetChargeHeadById(int? Id)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"PurImpSpGetChargeHeadData {Id}").AsNoTracking().FirstOrDefaultAsync();
                return result;

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<int> SaveChargeHead(string id, ChargeHeadViewModel chargeHeadViewModel)
        {
            try
            {
                var result = await _context.saveUpdateValueViewModels.FromSql($"PurSpSaveChargeHead {id}, {chargeHeadViewModel.chargeHeadName},{chargeHeadViewModel.shortName}, {chargeHeadViewModel.shortOrder},{chargeHeadViewModel.isActive},{chargeHeadViewModel.chargeCode},{chargeHeadViewModel.chargeHeadId}").AsNoTracking().FirstOrDefaultAsync();
                return result.isSuccess;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Benificiary
        public async Task<int> SaveBenificiary(string id, BenificiaryViewModel benificiaryViewModel)
        {
            try
            {
                var result = await _context.saveUpdateValueViewModels.FromSql($"PurSpSaveBenificiary {id}, {benificiaryViewModel.benificiaryName},{benificiaryViewModel.shortName}, {benificiaryViewModel.shortOrder},{benificiaryViewModel.isActive},{benificiaryViewModel.benificiaryCode},{benificiaryViewModel.benificiaryId}").AsNoTracking().FirstOrDefaultAsync();
                return result.isSuccess;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<JsonViewModel> GetBenificiaryById(int? Id)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"PurSpGetBenificiaryData {Id}").AsNoTracking().FirstOrDefaultAsync();
                return result;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> DeleteBenificiaryById(string id, int Id)
        {
            try
            {
                var result = await _context.saveUpdateViewModels.FromSql($"PurSpBenificiaryDelete {id},{Id}").AsNoTracking().FirstOrDefaultAsync();
                return result.isSuccess;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //Local Agent
        public async Task<int> SaveLocalAgent(string id, LocalAgentViewModel localAgentViewModel)
        {
            try
            {
                var result = await _context.saveUpdateValueViewModels.FromSql($"PurSpSaveLocalAgent {id}, {localAgentViewModel.localAgentName},{localAgentViewModel.shortName}, {localAgentViewModel.shortOrder},{localAgentViewModel.isActive},{localAgentViewModel.localAgentCode},{localAgentViewModel.localAgentId}").AsNoTracking().FirstOrDefaultAsync();
                return result.isSuccess;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<JsonViewModel> GetLocalAgentById(int? Id)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"PurSpGetLocalAgentData {Id}").AsNoTracking().FirstOrDefaultAsync();
                return result;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> DeleteLocalAgentById(string id, int Id)
        {
            try
            {
                var result = await _context.saveUpdateViewModels.FromSql($"PurSpLocalAgentDelete {id},{Id}").AsNoTracking().FirstOrDefaultAsync();
                return result.isSuccess;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //Transport
        public async Task<int> SaveModeTransPort(string id, ModeTransportViewModel modeTransportViewModel)
        {
            try
            {
                var result = await _context.saveUpdateValueViewModels.FromSql($"PurSpSaveModeTransport {id}, {modeTransportViewModel.modeTransportName},{modeTransportViewModel.shortName}, {modeTransportViewModel.shortOrder},{modeTransportViewModel.isActive},{modeTransportViewModel.modeTransportCode},{modeTransportViewModel.modeTransportId}").AsNoTracking().FirstOrDefaultAsync();
                return result.isSuccess;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<JsonViewModel> GetModeTransportById(int? Id)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"PurSpGetModeTransportData {Id}").AsNoTracking().FirstOrDefaultAsync();
                return result;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> DeleteModeTransportById(string id, int Id)
        {
            try
            {
                var result = await _context.saveUpdateViewModels.FromSql($"PurSpModeTransportDelete {id},{Id}").AsNoTracking().FirstOrDefaultAsync();
                return result.isSuccess;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //Port Info
        public async Task<int> SavePortInfo(string id, PortInfoViewModel portInfoViewModel)
        {
            try
            {
                var result = await _context.saveUpdateValueViewModels.FromSql($"PurSpSavePortInfo {id}, {portInfoViewModel.portInfoName},{portInfoViewModel.shortName}, {portInfoViewModel.shortOrder},{portInfoViewModel.isActive},{portInfoViewModel.portInfoCode},{portInfoViewModel.portInfoId}").AsNoTracking().FirstOrDefaultAsync();
                return result.isSuccess;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<JsonViewModel> GetPortInfoById(int? Id)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"PurSpGetPortInfoData {Id}").AsNoTracking().FirstOrDefaultAsync();
                return result;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<JsonViewModel> GetInsurenceCompanyById(int? Id)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"PurSpGetInsuranceCompanyData {Id}").AsNoTracking().FirstOrDefaultAsync();
                return result;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<bool> DeletePortInfoById(string id, int Id)
        {
            try
            {
                var result = await _context.saveUpdateViewModels.FromSql($"PurSpDeletePortInfo {id},{Id}").AsNoTracking().FirstOrDefaultAsync();
                return result.isSuccess;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Advise Bank

        public async Task<int> SaveAdviceBank(string id, AdviceBankViewModel adviceBankViewModel)
        {
            try
            {
                var result = await _context.saveUpdateValueViewModels.FromSql($"PurSpSaveAdviceBank {id}, {adviceBankViewModel.adviceBankName},{adviceBankViewModel.shortName}, {adviceBankViewModel.shortOrder},{adviceBankViewModel.isActive},{adviceBankViewModel.adviceBankCode},{adviceBankViewModel.adviceBankId}").AsNoTracking().FirstOrDefaultAsync();
                return result.isSuccess;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<JsonViewModel> GetAdviceBankById(int? Id)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"PurSpGetAdviceBankData {Id}").AsNoTracking().FirstOrDefaultAsync();
                return result;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> DeleteAdviceBankById(string id, int Id)
        {
            try
            {
                var result = await _context.saveUpdateViewModels.FromSql($"PurSpDeleteAdviceBank {id},{Id}").AsNoTracking().FirstOrDefaultAsync();
                return result.isSuccess;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //PreLcInfo

        //public async Task<int> SavePreLcInfo(string id, PreLcViewModel model)
        //{
        //    try
        //    {
        //        var x = $"PurImpSpPreLcInfoSave {id},{model.ImpPreLCInfoMasterId}, {model.lcPaymentType},{model.UnitId}, {model.refNo},{model.currencyId},{model.lcAmount},{model.ImpModeOfTransportId},{model.conversionRate},{model.ImpLocalAgentId},{model.ImpBenificiaryId},{model.indentNo},{model.indentDate},{model.indentRecvDate},{model.proformaInvoiceNo},{model.proformaInvoiceDate},{model.productTypeId},{model.manufacturerId},{model.requisitionNo},{model.requisitionDate},{model.rfiNo},{model.partShipment},{model.transShipment},{model.dockShipt},{model.psiStatus},{model.psiNo},{model.psiCompany}";

        //        var result = await _context.saveUpdateValueViewModels.FromSql($"PurImpSpPreLcInfoSave {id},{model.ImpPreLCInfoMasterId}, {model.lcPaymentType},{model.UnitId}, {model.refNo},{model.currencyId},{model.lcAmount},{model.ImpModeOfTransportId},{model.conversionRate},{model.ImpLocalAgentId},{model.ImpBenificiaryId},{model.indentNo},{model.indentDate},{model.indentRecvDate},{model.proformaInvoiceNo},{model.proformaInvoiceDate},{model.productTypeId},{model.manufacturerId},{model.requisitionNo},{model.requisitionDate},{model.rfiNo},{model.partShipment},{model.transShipment},{model.dockShipt},{model.psiStatus},{model.psiNo},{model.psiCompany}").AsNoTracking().FirstOrDefaultAsync();
        //        return result.isSuccess;
        //    }
        //    catch (Exception ex)
        //    {

        //        throw ex;
        //    }
        //}
        public async Task<int> SavePreLcInfo(string id, PreLcViewModel model)
        {
            try
            {
                var result = await _context.saveUpdateValueViewModels.FromSql($"PurImpSpPreLcInfoSaveNew {id},{model.ImpPreLCInfoMasterId}, {model.lcPaymentType},{model.refNo},{model.currencyId},{model.lcAmount},{model.ImpModeOfTransportId},{model.conversionRate},{model.ImpBenificiaryId},{model.proformaInvoiceNo},{model.proformaInvoiceDate},{model.productTypeId},{model.partShipment},{model.Remarks},{(model.csMasterId == 0 ? null : model.csMasterId)}").AsNoTracking().FirstOrDefaultAsync();
                return result.isSuccess;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<int> SaveNewLcInfo(string id, lcInfo model, int preLcId)
        {
            try
            {

                var result = await _context.saveUpdateValueViewModels.FromSql($"PurImpNewLcInfoSave {id},{preLcId},{model.ImpLCInfoMasterId}, {model.lcNo},{model.lcOpenDate},{model.bankId},{model.adviceBankId},{model.validityDate},{model.exshiptDate},{model.expireDate},{model.loadingPortId},{model.destinatinPortId},{model.countryOriginId},{model.frightAmount},{model.totalLcAmount}").AsNoTracking().FirstOrDefaultAsync();
                return result.isSuccess;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<int> SavePrelcInfoDetails(string id, List<PreLcDetailsViewModel> model, int preLcId)
        {
            try
            {
                var result = new SaveUpdateValueViewModel();
                foreach (PreLcDetailsViewModel preLcDetail in model)
                {

                    result = await _context.saveUpdateValueViewModels.FromSql($"PurImpSpSavePreLcDetails {id},{preLcDetail.ImpPreLCInfoDetailId},{preLcId},{preLcDetail.productWiseSpecificationId},{preLcDetail.unitPrice},{preLcDetail.totalPrice},{preLcDetail.blNo},{preLcDetail.blDate},{preLcDetail.hsCode},{preLcDetail.blRate},{preLcDetail.blValue},{(preLcDetail.csDetailId == 0 ? null : preLcDetail.csDetailId)}").AsNoTracking().FirstOrDefaultAsync();
                }
                return result.isSuccess;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<JsonViewModel> GetPreLcInfoById(int? preLcId)
        {
            var result = await _context.jsonViewModels.FromSql($"PurSpGetPreLcInfoJsonData {preLcId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<bool> DeletePreLcInfo(string id, int preLcId)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"PurSpDeletePreLcInfo {id}, {preLcId}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }

        public async Task<JsonViewModel> GetPreLcDetailsByMasterId(int? masterIddd)
        {
            var result = await _context.jsonViewModels.FromSql($"PurSpGetPreLcDetailsJsonData {masterIddd}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<JsonViewModel> DeletePreLcDetialsInfoByMasterId(int? masterIddd)
        {
            var result = await _context.jsonViewModels.FromSql($"PurSpGetPreLcDetailsJsonData {masterIddd}").AsNoTracking().FirstOrDefaultAsync();
            return result;

        }

        //Lc Info


        public async Task<int> SaveLcInfo(string id, LcViewModel model)
        {
            try
            {

                var result = await _context.saveUpdateValueViewModels.FromSql($"PurImpLcInfoSave {id},{model.ImpPreLCInfoMasterId},{model.ImpLCInfoMasterId},{model.lcStatus}, {model.lcNo},{model.lcOpenDate},{model.lcaNo},{model.bankId},{model.adviceBankId},{model.validityDate},{model.lcNegotiation},{model.exshiptDate},{model.expireDate},{model.loadingPortId},{model.destinatinPortId},{model.countryOriginId},{model.frightAmount},{model.totalLcAmount},{model.shiptDay},{model.remindDate},{model.sortedDate},{model.mailReqRcvDate},{model.signDate},{model.typedDate},{model.faxedOnDate},{model.appliedOnDate},{model.bankSubDate},{model.amndCopyDate},{model.remarks}").AsNoTracking().FirstOrDefaultAsync();
                return result.isSuccess;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<JsonViewModel> GetLcInfoById(int? lcId)
        {
            var result = await _context.jsonViewModels.FromSql($"PurSpGetLcInfoJsonData {lcId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<bool> DeleteLcInfo(string id, int lcId)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"PurSpDeleteLcInfo {id}, {lcId}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }
        public async Task<JsonViewModel> GetPreLcIdListFromLcTable(int? flag)
        {
            var result = await _context.jsonViewModels.FromSql($"PurSpGetReferenceListforAmendment {flag}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        //charge

        public async Task<JsonViewModel> GetLCAndPreLcInfoByPreId(int? preLcId)
        {
            var result = await _context.jsonViewModels.FromSql($"SPPreLcAndLcDataforCharge {preLcId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<int> SaveChargeInfo(string id, BankAndInsuranceChargeViewModel model)
        {
            try
            {
                var result = await _context.saveUpdateValueViewModels.FromSql($"PurImpSpBankAndInsuranceChargeInfoSave {id},{model.ImpBankInsuranceChargeMasterId}, {model.ImpLCInfoMasterId},{model.insuranceAmount}, {model.documentNo},{model.bankChargeDate},{model.insuranceCompany},{model.insuranceBranch},{model.insuranceNo},{model.insuranceDate},{model.chargeType},{model.remarks}").AsNoTracking().FirstOrDefaultAsync();
                return result.isSuccess;

            }
            catch (Exception ex)
            {
                throw ex;

            }

        }

        public async Task<int> SaveChargeInfoDetails(string id, List<BankInsuranceChargeDetailsViewModel> model, int chargeId)
        {
            try
            {
                var result = new SaveUpdateValueViewModel();
                foreach (BankInsuranceChargeDetailsViewModel chargeDetails in model)
                {

                    result = await _context.saveUpdateValueViewModels.FromSql($"PurImpSpSaveBankAndChargeDetails {id},{chargeDetails.ChargeDetailsId},{chargeId},{chargeDetails.amount},{chargeDetails.paticularId}").AsNoTracking().FirstOrDefaultAsync();
                }
                return result.isSuccess;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<JsonViewModel> GetChargeInfoById(int? chargeId)
        {
            var result = await _context.jsonViewModels.FromSql($"PurSpGetBankInsuranceChargeInfoJsonData {chargeId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<bool> DeleteChargeInfo(string id, int chargeId)
        {
            try
            {
                var result = await _context.saveUpdateViewModels.FromSql($"PurSpDeleteBankInsuranceChargeInfo {id}, {chargeId}").AsNoTracking().FirstOrDefaultAsync();

                return result.isSuccess;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<JsonViewModel> GetChargeDetailsByMasterId(int? masterId)
        {
            var result = await _context.jsonViewModels.FromSql($"PurSpGetbankInsuranceChargeDetailsJsonData {masterId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }


        public Task<JsonViewModel> DeleteChargeDetialsInfoByMasterId(int? masterIddd)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteChargeDetialsInfoById(string id, int? detailId)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"PurSpDeleteBankInsuranceChargeDetailsInfo {id}, {detailId}").AsNoTracking().FirstOrDefaultAsync();

            return result.isSuccess;
        }
        //Shipment
        public async Task<JsonViewModel> GetPreLcLcInfoForShipmentById(int? preLcId)
        {
            var result = await _context.jsonViewModels.FromSql($"GetSpPrelcLcDataJsonDataforShipment {preLcId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<int> SaveShipmentInfo(string id, ShipmentViewModel model)
        {
            try
            {
                var result = await _context.saveUpdateValueViewModels.FromSql($"PurSPSaveShipmentInfo {id},{model.ImpShipmentInformationId},{model.ImpLCInfoMasterId},{model.invoiceNo},{model.invoiceAmt},{model.carrierBillNo},{model.carrierName},{model.cagesDrumsItems},{model.actualLoadingPortId},{model.actualDestinationPortId},{model.remainderDays},{model.invoiceDate},{model.shipmentDate},{model.expectedDurgCLrDate},{model.carrierBillDate},{model.shipmentNo},{model.transShipment}").AsNoTracking().FirstOrDefaultAsync();
                return result.isSuccess;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<JsonViewModel> GetShipmentInfoById(int? shipmentId)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"PurSpGetShipmentInfoJsonData {shipmentId}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> DeleteShipmentInfo(string id, int shipmentId)
        {
            try
            {
                var result = await _context.saveUpdateViewModels.FromSql($"PurSPDeleteShiomentInfo {id}, {shipmentId}").AsNoTracking().FirstOrDefaultAsync();

                return result.isSuccess;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        //Bank Clearence
        public async Task<JsonViewModel> GetReferenceNoListforBankClearenceBasedOnShipmentInfo(int? shipmentId)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"PurSpGetReferenceListFromPrelcBasedOnShipmentInfo {shipmentId}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<JsonViewModel> getPreLcLcShipmentInfoForBankClearence(int? shipmentId)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"PurSpGetPreLcLcShipmentDataforBankClearence {shipmentId}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> saveClearanceInfo(string id, ClearanceViewModel model)
        {
            try
            {
                var result = await _context.saveUpdateValueViewModels.FromSql($"PurSpSaveClearanceInfo {id},{model.ImpClearenceInfoId},{model.lcMasterId},{model.actBankClrDt},{model.expCustomeClrDt},{model.DocRecvDate},{model.cnfAgent},{model.remainderDays},{model.remarks},{model.type}").AsNoTracking().FirstOrDefaultAsync();
                return result.isSuccess;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<JsonViewModel> getClearacneDataByIdAndType(int? clearacneId, int type)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"PurSpGetClearanceInof {clearacneId},{type}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> DeleteClearanceInfo(string id, int clearanceId)
        {
            try
            {
                var result = await _context.saveUpdateViewModels.FromSql($"PurSpDeleteClearance {id}, {clearanceId}").AsNoTracking().FirstOrDefaultAsync();

                return result.isSuccess;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<JsonViewModel> GetReferenceNoListforCustomeClearenceBasedOnBankClearanceInfo(int? clearanceId)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"PurSpGetReferenceListFromPrelcBasedOnBankClearance {clearanceId}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<JsonViewModel> getPreLcLcShipmentInfoForCustomClearance(int? clearanceId)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"PurSpGetPreLcLcShipmentDataforCustomeClearance {clearanceId}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region amendment
        public async Task<JsonViewModel> GetLCAndPreLcInfoByPreIdforAmendment(int? preLcId)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"PurSpGetPreLcLcDataByPreLcIdForAmendment {preLcId}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<int> SaveLcAmendmentInfo(string id, AmendmentViewModel model)
        {
            try
            {
                var result = await _context.saveUpdateValueViewModels.FromSql($"PurSpSaveLcAmendment {id},{model.ImpLCAmendmentId},{model.ImpLCInfoMasterId},{model.amendmentNo},{model.amendment},{model.amendmentDate},{model.remarks}").AsNoTracking().FirstOrDefaultAsync();
                return result.isSuccess;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<JsonViewModel> GetLcAmendmenttInfoById(int? Amendmentid)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"PurSpgetLcAmendmentInfoJsonData {Amendmentid}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> DeleteLcAmendmentInfo(string id, int Amendmentid)
        {
            try
            {
                var result = await _context.saveUpdateViewModels.FromSql($"PurSpDeleteAmendmentInfo {id}, {Amendmentid}").AsNoTracking().FirstOrDefaultAsync();

                return result.isSuccess;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        //Amendemtn Charge

        public async Task<JsonViewModel> GetReferenctListForAmendmentCharge(int? amendmentId)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"PurSpGetReferenceListFromPreLcforAmendmentCharge {amendmentId}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> SaveLcAmendmentChargeInfo(string id, AmendmentChargeViewModel model)
        {
            try
            {
                var result = await _context.saveUpdateValueViewModels.FromSql($"PurSpSaveAmendmentChargeInfo {id},{model.ImpLCAmendmentChargeId},{model.ImpLCAmendmentId},{model.amendmentAmount},{model.amendmentChargeDate},{model.remarks}").AsNoTracking().FirstOrDefaultAsync();
                return result.isSuccess;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<JsonViewModel> GetLcAmendmentChargeInfoById(int? amendmentChargeId)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"PurSpgetLcAmendmentChargeInfoJsonData {amendmentChargeId}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> DeleteLcAmendmentChargeInfo(string id, int amendmentChargeId)
        {
            try
            {
                var result = await _context.saveUpdateViewModels.FromSql($"PurSpDeleteAmendmentChargeInfo {id}, {amendmentChargeId}").AsNoTracking().FirstOrDefaultAsync();

                return result.isSuccess;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<JsonViewModel> GetLCAndPreLcLcAndAmendmentDataforAmendmentCharge(int? amendmentId)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"PurSpGetPreLcLcAmendmentMasterDataByAmendmentIdforAmendmentCharge {amendmentId}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Ohter charge

        public Task<JsonViewModel> GetReferenctListForOtherCharge(int? amendmentId)
        {
            throw new NotImplementedException();
        }

        public async Task<JsonViewModel> GetLCAndPreLcLcAndChargeDataforOtherChargee(int? preLcId)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"PurSPGetPreLcLcChargeDataforOtherCharge {preLcId}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> SaveOtherChargeInfo(string id, OtherChargeViewModel model)
        {
            try
            {
                var result = await _context.saveUpdateValueViewModels.FromSql($"PurSpSaveOtherChargeInfo {id},{model.ImpOtherChargeId},{model.ImpLCInfoMasterId},{model.CustomsDutyOthersCharge},{model.ClearingCNFCharge},{model.LoadingUnloadingCharge},{model.CarringCharge },{model.OthersCharge},{model.OthersCharge2 },{model.CustomsDutyOthersChargeDate},{model.ClearingCNFChargeDate},{model.LoadingUnloadingChargeDate},{model.CarringChargeDate},{model.remarks}").AsNoTracking().FirstOrDefaultAsync();
                return result.isSuccess;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<JsonViewModel> GetOtherChargeInfoById(int? chargeId)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"PurSPGetOtherChargeJsonData {chargeId}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> DeleteLcOtherChargeInfo(string id, int chargeId)
        {
            try
            {
                var result = await _context.saveUpdateViewModels.FromSql($"PurSpDeleteOtherChargeInfo {id}, {chargeId}").AsNoTracking().FirstOrDefaultAsync();

                return result.isSuccess;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        //Offshore Charge

        public Task<JsonViewModel> GetReferenctListForOffshoreCharge(int? amendmentId)
        {
            throw new NotImplementedException();
        }

        public async Task<JsonViewModel> GetLCAndPreLcLcBankInsuranceotherChargeDataforOffshoreChargee(int? preLcId)
        {
            //PurSPGetPreLcLcBnakInsuranceOtherChargeDataforOffshoreCharge
            try
            {
                var result = await _context.jsonViewModels.FromSql($"PurSPGetPreLcLcBnakInsuranceOtherChargeDataforOffshoreCharge {preLcId}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> SaveOffshoreChargeInfo(string id, OffshoreChargeViewModel model)
        {
            try
            {
                var result = await _context.saveUpdateValueViewModels.FromSql($"PurSpSaveOffshoreCharge {id},{model.ImpOffshoreChargeId},{model.ImpLCInfoMasterId},{model.OffshoreBankCharge},{model.OffshoreBankChargeDate},{model.remarks}").AsNoTracking().FirstOrDefaultAsync();
                return result.isSuccess;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<JsonViewModel> GetOffshoreChargeInfoById(int? chargeId)
        {
            //PurSPGetOffshoreChargeJsonData
            try
            {
                var result = await _context.jsonViewModels.FromSql($"PurSPGetOffshoreChargeJsonData {chargeId}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> DeleteOffshoreChargeInfo(string id, int chargeId)
        {
            //PurSpDeleteOffshoreCharge
            try
            {
                var result = await _context.saveUpdateViewModels.FromSql($"PurSpDeleteOffshoreCharge {id}, {chargeId}").AsNoTracking().FirstOrDefaultAsync();

                return result.isSuccess;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            #endregion
        }
    }
}

