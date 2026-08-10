using Microsoft.EntityFrameworkCore;
using ONEERP.Areas.Accounting.Models;
using ONEERP.Areas.Auth.Models;
using ONEERP.Data;
using ONEERP.ERPServices.Accounting.MasterData.Interfaces;
using ONEERP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.ERPServices.Accounting.MasterData
{
    public class PartyService : IPartyService
    {
        private readonly ERPDbContext _context;

        public PartyService(ERPDbContext context)
        {
            _context = context;
        }

        #region Party
        public async Task<JsonViewModel> getBenificiaryByID(int userId, int id)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"AccSpGetBenificiaryForDropdownJson {userId},{id}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<int> SaveParty(string Id, PartyViewModel partyViewModel)
        {

            try
            {
                var result = await _context.saveUpdateValueViewModels.FromSql($"AccSpSetParty {Id},{partyViewModel.partyId},{partyViewModel.partyCode},{partyViewModel.partyName},{partyViewModel.aliasName},{partyViewModel.addressLine},{partyViewModel.contactNumber},{partyViewModel.contactPerson},{partyViewModel.email},{partyViewModel.partyTypeId},{partyViewModel.companyId},{partyViewModel.sbuId},{partyViewModel.isActive},{partyViewModel.officeName},{partyViewModel.ownerName},{partyViewModel.fatherName},{partyViewModel.motherName},{partyViewModel.nid},{partyViewModel.gender},{partyViewModel.businessStartDate},{partyViewModel.companyCategoryId},{partyViewModel.creditLimit},{partyViewModel.creditLimitWord},{partyViewModel.isApproved},{partyViewModel.isHold},{partyViewModel.territoryId},{partyViewModel.tradeLicense},{partyViewModel.drugLicense},{partyViewModel.creditDays}").AsNoTracking().FirstOrDefaultAsync();

                return result.isSuccess;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public async Task<int> SavePartyObservation(string Id, PartyObservationViewModel partyViewModel)
        {

            try
            {
                var result = await _context.saveUpdateValueViewModels.FromSql($"AccSpSetPartyObservation {Id},{partyViewModel.partyId},{partyViewModel.partyName},{partyViewModel.addressLine},{partyViewModel.contactNumber},{partyViewModel.contactPerson},{partyViewModel.email},{partyViewModel.partyTypeId},{partyViewModel.ownerName},{partyViewModel.nid},{partyViewModel.companyCategoryId},{partyViewModel.creditLimit},{partyViewModel.isApproved},{partyViewModel.accPartyId},{partyViewModel.territoryId},{partyViewModel.MarketCode},{partyViewModel.MarketName},{partyViewModel.chemberLocation}").AsNoTracking().FirstOrDefaultAsync();

                return result.isSuccess;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<PartyListViewModel>> GetPartyList()
        {
            var result = await _context.partyListViewModels.FromSql($"AccSpGetParty {0}").AsNoTracking().ToListAsync();
            return result;
        }

        public async Task<PartyListViewModel> GetPartyById(int id)
        {
            var result = await _context.partyListViewModels.FromSql($"AccSpGetParty {id}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        } 

        public async Task<JsonViewModel> GetPartyByIdJson(int id)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"AccSpGetPartyJsonAccount {id}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<JsonViewModel> GetNewPartyCode(int? id)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"AccSpGetNewPartyCodeJson {id}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<JsonViewModel> GetPartyByIdJsonNew(int employeeId,int id)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"AccSpGetPartyJson {employeeId},{id}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<JsonViewModel> AccSpGetPartyObserbationJson(int employeeId,int id)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"AccSpGetPartyObserbationJson {employeeId},{id}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<JsonViewModel> GetPartyByIdJsonNewForGrid(int employeeId,int id)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"AccSpGetPartyGridJson {employeeId},{id}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<JsonViewModel> GetPartyByIdJsonNewNewForGrid(int employeeId, int id,string territoryCode)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"AccSpGetPartyGridNewJson {employeeId},{id},{territoryCode}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<JsonViewModel> AccSpGetPartyObserbationGridJson(int employeeId,int id)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"AccSpGetPartyObserbationGridJson {employeeId},{id}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<JsonViewModel> GetPartyForAccountingByIdJson(int id)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"AccSpGetPartyForAccountingJson {id}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<JsonViewModel> GetSupplierForDropdown(int id)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"AccSpGetSupplierForDropdownJson {id}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<JsonViewModel> GetPartyForDropdownJson(int? userid, int id, string depotCode = "")
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"AccSpGetPartyForDropdownJson {id}, {userid}, {depotCode}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<JsonViewModel> GetPartyUpdateByIdJson(string userId,int id)
        {
            var result = await _context.jsonViewModels.FromSql($"CmnchemistForUpdateById {userId},{id}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<JsonViewModel> GetPartyObsevationByIdJson(int id, string userId)
        {
            var result = await _context.jsonViewModels.FromSql($"AccSpGetPartyObservationJson {id},{userId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        } 
        public async Task<JsonViewModel> GetPartyObsevationByIdbyEmpJson(string id, string EmpCode, string RegionCode, string AreaCode)
        {
            var result = await _context.jsonViewModels.FromSql($"CmnChemistObserbationByStatus {id},{EmpCode},{RegionCode},{AreaCode}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> GetPartyByPartyTypeJson(int partyId, int partyTypeId)
        {
            var result = await _context.jsonViewModels.FromSql($"AccSpGetPartyByTypeJson {partyId},{partyTypeId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<JsonViewModel> GetDuplicateParty(int partyId, string partyName)
        {
            var result = await _context.jsonViewModels.FromSql($"AccSpGetDuplicateParty {partyId},{partyName}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<JsonViewModel> GetDuplicatePartyCode(int partyId, string partyCode)
        {
            var result = await _context.jsonViewModels.FromSql($"AccSpGetDuplicatePartyCode {partyId},{partyCode}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<JsonViewModel> GetVisaParty(int visaPartyId)
        {
            var result = await _context.jsonViewModels.FromSql($"AccSpGetVisaParty {visaPartyId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<JsonViewModel> GetPartyTypeByIdJson()
        {
            var result = await _context.jsonViewModels.FromSql($"AccSpGetPartyTypeJson").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<bool> DeletePartyById(string Id, int partyId)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"AccSpDeleteParty {Id},{partyId}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }

        public async Task<JsonViewModel> AccSpGetPartyByDepotTerritoyJson(int userid, string depoCode, string territoryCode, int id)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"AccSpGetPartyByDepotTerritoyJson {userid},{depoCode},{territoryCode},{id}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        #endregion

        #region Party Contact

        public async Task<int> SavePartyContact(string userid, List<PartyContactViewModel> partyContactViewModels, int partyId)
        {
            await _context.saveUpdateViewModels.FromSql($"AccSpDeletePartyContact {userid},{partyId}").AsNoTracking().FirstOrDefaultAsync();
            var result = new SaveUpdateValueViewModel();
            foreach (PartyContactViewModel model in partyContactViewModels)
            {
                result = await _context.saveUpdateValueViewModels.FromSql($"AccSpSetPartyContact {userid},{partyId},{model.partyContactId},{model.mobileOne},{model.mobileTwo},{model.emailAddress},{model.managerName},{model.managerContact}").AsNoTracking().FirstOrDefaultAsync();
            }
            return result.isSuccess;
        }

        public async Task<JsonViewModel> GetPartyContactByPartyId(int partyId)
        {
            var result = await _context.jsonViewModels.FromSql($"AccSpGetPartyContactByParty {partyId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        #endregion

        #region Party Address

        public async Task<int> SavePartyAddress(string userid, List<PartyAddressViewModel> partyAddressViewModels, int partyId)
        {
            await _context.saveUpdateViewModels.FromSql($"AccSpDeletePartyAddress {userid},{partyId}").AsNoTracking().FirstOrDefaultAsync();
            var result = new SaveUpdateValueViewModel();
            foreach (PartyAddressViewModel model in partyAddressViewModels)
            {
                result = await _context.saveUpdateValueViewModels.FromSql($"AccSpSetPartyAddress {userid},{partyId},{model.addressType},{model.division},{model.district},{model.thana},{model.policeStation},{model.postOffice},{model.houseStreet}").AsNoTracking().FirstOrDefaultAsync();
            }
            return result.isSuccess;
        }
      

        public async Task<JsonViewModel> GetPartyAddressByPartyId(int partyId)
        {
            var result = await _context.jsonViewModels.FromSql($"AccSpGetPartyAddressByParty {partyId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        #endregion

        #region Party Bank

        public async Task<int> SavePartyBank(string userid, List<PartyBankViewModel> partyBankViewModels, int partyId)
        {
            await _context.saveUpdateViewModels.FromSql($"AccSpDeletePartyBank {userid},{partyId}").AsNoTracking().FirstOrDefaultAsync();
            var result = new SaveUpdateValueViewModel();
            foreach (PartyBankViewModel model in partyBankViewModels)
            {
                result = await _context.saveUpdateValueViewModels.FromSql($"AccSpSetPartyBank {userid},{partyId},{model.bankId},{model.bankAccName},{model.bankAccNo},{model.bankBranchName}").AsNoTracking().FirstOrDefaultAsync();
            }
            return result.isSuccess;
        }

        public async Task<JsonViewModel> GetPartyBankByPartyId(int partyId)
        {
            var result = await _context.jsonViewModels.FromSql($"AccSpGetPartyBankByParty {partyId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        #endregion

        public async Task<JsonViewModel> GetSupplierByIdJson(int userId, int id)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"AccSpGetSupplierJson {userId},{id}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<JsonViewModel> GetSupplierConvertToLedgerJson(int userId, int id, int isConverted)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"AccSpGetSupplierConvertToLedgerJson {userId},{id},{isConverted}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<int> SaveLedgersConvertedFromBenificiary(int? userId, List<BenificiaryconverttoledgerViewModel> model)
        {
            try
            {
                var result = new SaveUpdateValueViewModel();
                foreach (var m in model.Where(x => x.isSelect == true))
                {
                    result = await _context.saveUpdateValueViewModels.FromSql($"AccSpSetLedgersConvertedFromBenificiary {userId}, {m.partyId},{m.partyName}").AsNoTracking().FirstOrDefaultAsync();
                }
                return result.isSuccess;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<int> SaveSalesHoldForCustomer(int Id, List<SalesHoldForCustomerlist> models)
        {

            try
            {
                var result = new SaveUpdateValueViewModel();
                foreach (var model in models)
                {
                    result = await _context.saveUpdateValueViewModels.FromSql($"AccSpSaveSalesHoldForCustomer {Id},{model.partyId},{model.isHold}").AsNoTracking().FirstOrDefaultAsync();
                }

                return result.isSuccess;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
