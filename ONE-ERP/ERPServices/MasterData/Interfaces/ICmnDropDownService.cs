
using ONEERP.Areas.MasterData.Models;
using ONEERP.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ONEERP.ERPServices.MasterData.Interfaces
{
    public interface ICmnDropDownService
    {
        #region DropDown Type
        Task<bool> SaveCmnDropDownType(string id, CmnDropDownTypeViewModel cmnDropDownTypeViewModel); 
        Task<JsonViewModel> GetCmnDropDownTypeByIdJson(int id);
        Task<bool> DeleteCmnDropDownTypeById(string id, int dropDownTypeId);

        #endregion

        #region DropDown
        Task<bool> SaveCmnDropDown(string id, CmnDropDownViewModel cmnDropDownViewModel);
        Task<JsonViewModel> GetCmnDropDownByIdJson(int id, string type);
        Task<bool> DeleteCmnDropDownById(string id, int dropDownId);

        Task<JsonViewModel> GetRegionByZoneCodes(int? userId, string zoneCode);
        Task<JsonViewModel> GetAreaByRegionCodes(int? userId, string regionCodes);
        Task<JsonViewModel> GetTerritoryByAreaCodes(int? userId, string areaCodes);

        #endregion

        #region Bank
        Task<JsonViewModel> GetCmnBankByIdJson(int bankId, int bankTypeId);
        Task<JsonViewModel> GetCmnOriginCountries(int countryIdankId);
        Task<JsonViewModel> GetCmnBankBranchByBankIdJson(int bankId);
        Task<JsonViewModel> GetCmnDepotByCodeJson(int userId);
        Task<JsonViewModel> GetAllTerritoriesHH(int userId);

        #endregion

        #region transaction Type
        Task<JsonViewModel> GetCmnTransactionType(int transactionTypeId);

        #endregion
    }
}
