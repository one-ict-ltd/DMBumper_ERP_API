
using ONEERP.Areas.MasterData.Models;
using ONEERP.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ONEERP.ERPServices.MasterData.Interfaces
{
    public interface ISpecialBranchUnitService
    {
        #region SBU
        Task<bool> SaveSpecialBranchUnit(string id, SBUViewModel sbuViewModel);
        Task<IEnumerable<SBUListViewModel>> GetSpecialBranchUnit();
        Task<SBUListViewModel> GetSpecialBranchUnitById(int id);
        Task<IEnumerable<SBUListViewModel>> GetSpecialBranchUnitBysbuidcompanyid(int id, int companyId);
        Task<JsonViewModel> GetSpecialBranchUnitBysbuidcompanyidJson(int id, int companyId);
        Task<bool> DeleteSpecialBranchUnitById(string id, int SbuId);

        #endregion

        Task<JsonViewModel> GetSpecialBranchUnitBysbuidcompanyidJson(int sbuId, int companyId, int? userId);
        Task<JsonViewModel> GetSpecialBranchUnitBywithoutselfsbuidcompanyidJson(int sbuId, int companyId, int? userId);
        Task<JsonViewModel> GetSpecialBranchUnitBySpecificSbuIdJson(int sbuId, int companyId, int? userId);

        #region Store
        Task<bool> SaveStore(string id, StoreViewModel model);       
        Task<JsonViewModel> GetStoreById(int employeeId,int companyId, int sbuId, int storeId);
        Task<bool> DeleteStoreById(string id, int storeId);
        Task<JsonViewModel> GetSpecialBranchUnitBycompanyidJson(int sbuId, int companyId, int? userId);
        Task<JsonViewModel> GetSpecialBranchUnitBysbuidcompanyidForPurchaseRequisitionJson(int sbuId, int companyId, int? userId);
        Task<JsonViewModel> GetSpecialBranchUnitBysbuidcompanyidForAccountingJson(int sbuId, int companyId, int? userId);

        Task<JsonViewModel> GetSalaryLocationJson();

        #endregion
    }
}
