
using ONEERP.Areas.Accounting.Models;
using ONEERP.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ONEERP.ERPServices.Accounting.MasterData.Interfaces
{
    public interface IPartyService
    {
        #region Party
        Task<int> SaveParty(string Id, PartyViewModel partyViewModel);
        Task<IEnumerable<PartyListViewModel>> GetPartyList();
        Task<PartyListViewModel> GetPartyById(int id);
        Task<JsonViewModel> GetPartyByIdJson(int id);
        Task<JsonViewModel> GetNewPartyCode(int? id);
        Task<JsonViewModel> GetPartyByPartyTypeJson(int partyId, int partyTypeId);
        Task<JsonViewModel> GetDuplicateParty(int partyId, string partyName);
        Task<JsonViewModel> GetDuplicatePartyCode(int partyId, string partyCode);
        Task<JsonViewModel> GetVisaParty(int visaPartyId);
        Task<bool> DeletePartyById(string Id, int partyId);
        Task<JsonViewModel> AccSpGetPartyByDepotTerritoyJson(int userid, string depoCode, string territoryCode, int id);
        Task<JsonViewModel> GetPartyTypeByIdJson();
        Task<int> SavePartyObservation(string Id, PartyObservationViewModel partyViewModel);
        Task<JsonViewModel> GetPartyObsevationByIdJson(int id, string userId);
        Task<JsonViewModel> GetPartyObsevationByIdbyEmpJson(string id, string EmpCode, string RegionCode, string AreaCode);
        Task<JsonViewModel> GetPartyUpdateByIdJson(string userId, int id);
        Task<JsonViewModel> GetPartyForDropdownJson(int? userid, int id, string depotCode = "");
        Task<JsonViewModel> GetSupplierForDropdown(int id);
        Task<JsonViewModel> GetPartyForAccountingByIdJson(int id);
        Task<JsonViewModel> GetPartyByIdJsonNew(int employeeId, int id);
        Task<JsonViewModel> GetPartyByIdJsonNewForGrid(int employeeId, int id);
        Task<JsonViewModel> GetPartyByIdJsonNewNewForGrid(int employeeId, int id, string territoryCode);
        Task<JsonViewModel> AccSpGetPartyObserbationGridJson(int employeeId, int id);
        Task<JsonViewModel> AccSpGetPartyObserbationJson(int employeeId, int id);
        #endregion

        #region Party Contact
        Task<int> SavePartyContact(string id, List<PartyContactViewModel> partyContactViewModels, int partyId);
        Task<JsonViewModel> GetPartyContactByPartyId(int partyId);

        #endregion

        #region Party Address
        Task<int> SavePartyAddress(string id, List<PartyAddressViewModel> partyAddressViewModels, int partyId);        
        Task<JsonViewModel> GetPartyAddressByPartyId(int partyId);

        #endregion

        #region Party Bank
        Task<int> SavePartyBank(string id, List<PartyBankViewModel> partyBankViewModels, int partyId);
        Task<JsonViewModel> GetPartyBankByPartyId(int partyId);

        #endregion

        Task<JsonViewModel> GetSupplierByIdJson(int userId, int id);
        Task<JsonViewModel> GetSupplierConvertToLedgerJson(int userId, int id, int isConverted);
        Task<int> SaveLedgersConvertedFromBenificiary(int? userId, List<BenificiaryconverttoledgerViewModel> model);
        Task<JsonViewModel> getBenificiaryByID(int userId, int id);
        Task<int> SaveSalesHoldForCustomer(int Id, List<SalesHoldForCustomerlist> models);
    }
}
