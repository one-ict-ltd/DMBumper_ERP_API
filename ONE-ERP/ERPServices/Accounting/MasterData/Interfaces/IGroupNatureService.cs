
using ONEERP.Areas.Accounting.Models;
using ONEERP.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ONEERP.ERPServices.Accounting.MasterData.Interfaces
{
    public interface IGroupNatureService
    {
        Task<bool> SaveGroupNature(string Id, GroupNatureViewModel groupNatureViewModel);
        Task<IEnumerable<GroupNatureListViewModel>> GetGroupNature();
        Task<GroupNatureListViewModel> GetGroupNatureById(int id);
        Task<JsonViewModel> GetGroupNatureByIdJson(int id);
        Task<bool> DeleteGroupNatureById(string Id, int groupNatureId);
     
    }
}
