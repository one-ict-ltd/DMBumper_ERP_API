using ONEERP.Areas.Sales.Models;
using ONEERP.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ONEERP.ERPServices.Sales.Interfaces
{
    public interface ISalesExecutiveMemberService
    {
        Task<int> SaveExecutiveMember(int? userId, List<SalExecutiveTeamViewModel> salExecutiveTeamViewModels);
        Task<JsonViewModel> GetExecutiveMember(int? executiveTeamId);
        Task<bool> DeleteExecutiveMember(int? userId, int executiveTeamId);
    }
}
