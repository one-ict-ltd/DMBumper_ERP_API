
using ONEERP.Areas.Accounting.Models;
using ONEERP.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ONEERP.ERPServices.Accounting.MasterData.Interfaces
{
    public interface IFundSourceService
    {
        Task<bool> SaveFundSource(string Id, FundSourceViewModel fundSourceViewModel);
        Task<IEnumerable<FundSourceListViewModel>> GetFundSourceList();
        Task<FundSourceListViewModel> GetFundSourceById(int id);
        Task<IEnumerable<FundSourceListViewModel>> GetFundSourceListbyCompanyId(int companyId);
        Task<IEnumerable<FundSourceListViewModel>> GetFundSourceListbySbuId(int sbuId);
        Task<JsonViewModel> GetFundSourceByIdJson(int id, int companyId, int sbuId);
        Task<JsonViewModel> GetDuplicateFundSource(int fundSourceId, string fundSourceName);
        Task<bool> DeleteFundSourceById(string Id, int fundSourceId);


    }
}
