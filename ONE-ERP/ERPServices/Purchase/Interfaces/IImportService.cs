using ONEERP.Areas.Purchase;
using ONEERP.Areas.Purchase.Models;
using ONEERP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.ERPServices.Purchase.Interfaces
{
    public interface IImportService
    {
        Task<int> SaveChargeHead(string id, ChargeHeadViewModel chargeHeadViewModel);
        Task<JsonViewModel> GetChargeHeadById(int? Id);
        Task<bool> DeleteChargeHeadById(string id, int Id);


        Task<int> SaveBenificiary(string id, BenificiaryViewModel chargeHeadViewModel);
        Task<JsonViewModel> GetBenificiaryById(int? Id);
        Task<bool> DeleteBenificiaryById(string id, int Id);


        Task<int> SaveLocalAgent(string id, LocalAgentViewModel chargeHeadViewModel);
        Task<JsonViewModel> GetLocalAgentById(int? Id);
        Task<bool> DeleteLocalAgentById(string id, int Id);



        Task<int> SaveModeTransPort(string id, ModeTransportViewModel modeTransportViewModel);
        Task<JsonViewModel> GetModeTransportById(int? Id);
        Task<bool> DeleteModeTransportById(string id, int Id);


        Task<int> SavePortInfo(string id, PortInfoViewModel portInfoViewModel);
        Task<JsonViewModel> GetPortInfoById(int? Id);
        Task<bool> DeletePortInfoById(string id, int Id);




        Task<int> SaveAdviceBank(string id, AdviceBankViewModel portInfoViewModel);
        Task<JsonViewModel> GetAdviceBankById(int? Id);
        Task<bool> DeleteAdviceBankById(string id, int Id);


        //Quotation Area

        Task<int> SavePreLcInfo(string id, PreLcViewModel model);

        Task<int> SavePrelcInfoDetails(string id, List<PreLcDetailsViewModel> model, int preLcId);
        Task<JsonViewModel> GetPreLcInfoById(int? preLcId);
       
        Task<bool> DeletePreLcInfo(string id, int preLcId);
        Task<JsonViewModel> GetPreLcDetailsByMasterId(int? masterIddd);
        Task<JsonViewModel> DeletePreLcDetialsInfoByMasterId(int? masterIddd);

        //lcinfo
        Task<int> SaveLcInfo(string id, LcViewModel model);
        Task<JsonViewModel> GetLcInfoById(int? lcId);
        Task<bool> DeleteLcInfo(string id, int lcId);
        Task<JsonViewModel> GetPreLcIdListFromLcTable(int? flag);

        Task<int> SaveNewLcInfo(string id, lcInfo model, int preLcId);
        //charge
        Task<JsonViewModel> GetLCAndPreLcInfoByPreId(int? preLcId);
        Task<int> SaveChargeInfo(string id, BankAndInsuranceChargeViewModel model);
        Task<int> SaveChargeInfoDetails(string id, List<BankInsuranceChargeDetailsViewModel> model, int chargeId);
        Task<JsonViewModel> GetChargeInfoById(int? chargeId);
        Task<bool> DeleteChargeInfo(string id, int chargeId);
        Task<JsonViewModel> GetChargeDetailsByMasterId(int? masterId);
        Task<JsonViewModel> DeleteChargeDetialsInfoByMasterId(int? masterIddd);
        Task<bool> DeleteChargeDetialsInfoById(string id,int? detailId);

        //shipment
        Task<JsonViewModel> GetPreLcLcInfoForShipmentById(int? preLcId);
        Task<int> SaveShipmentInfo(string id, ShipmentViewModel model);
        Task<JsonViewModel> GetShipmentInfoById(int? shipmentId);
        Task<bool> DeleteShipmentInfo(string id, int shipmentId);

        //Bank Clearence
        Task<JsonViewModel> GetReferenceNoListforBankClearenceBasedOnShipmentInfo(int? shipmentId);
        Task<JsonViewModel> GetReferenceNoListforCustomeClearenceBasedOnBankClearanceInfo(int? clearanceId);
        Task<JsonViewModel> getPreLcLcShipmentInfoForBankClearence(int? shipmentId);
        Task<JsonViewModel> getPreLcLcShipmentInfoForCustomClearance(int? clearanceId);
        Task<JsonViewModel> getClearacneDataByIdAndType(int? clearacneId,int type);
        Task<int> saveClearanceInfo(string id, ClearanceViewModel model);
        Task<bool> DeleteClearanceInfo(string id, int clearanceId);


        #region Amendment
        Task<JsonViewModel> GetLCAndPreLcInfoByPreIdforAmendment(int? preLcId);

        Task<int> SaveLcAmendmentInfo(string id, AmendmentViewModel model);
        Task<JsonViewModel> GetLcAmendmenttInfoById(int? Amendmentid);
        Task<bool> DeleteLcAmendmentInfo(string id, int Amendmentid);
        #endregion

        #region Amendmnt Charge
        Task<JsonViewModel> GetReferenctListForAmendmentCharge(int? amendmentId);
        Task<JsonViewModel> GetLCAndPreLcLcAndAmendmentDataforAmendmentCharge(int? amendmentId);
        Task<int> SaveLcAmendmentChargeInfo(string id, AmendmentChargeViewModel model);
        Task<JsonViewModel> GetLcAmendmentChargeInfoById(int? amendmentChargeId);
        Task<bool> DeleteLcAmendmentChargeInfo(string id, int amendmentChargeId);
        #endregion
        // other charge

        Task<JsonViewModel> GetReferenctListForOtherCharge(int? amendmentId);
        Task<JsonViewModel> GetLCAndPreLcLcAndChargeDataforOtherChargee(int? preLcId);
        Task<int> SaveOtherChargeInfo(string id, OtherChargeViewModel model);
        Task<JsonViewModel> GetOtherChargeInfoById(int? chargeId);
        Task<bool> DeleteLcOtherChargeInfo(string id, int amendmentChargeId);

        // Offshore Charge

        Task<JsonViewModel> GetReferenctListForOffshoreCharge(int? amendmentId);
        Task<JsonViewModel> GetLCAndPreLcLcBankInsuranceotherChargeDataforOffshoreChargee(int? preLcId);
        Task<int> SaveOffshoreChargeInfo(string id, OffshoreChargeViewModel model);
        Task<JsonViewModel> GetOffshoreChargeInfoById(int? chargeId);
        Task<bool> DeleteOffshoreChargeInfo(string id, int chargeId);

        Task<JsonViewModel> GetInsurenceCompanyById(int? Id);

    }
}
