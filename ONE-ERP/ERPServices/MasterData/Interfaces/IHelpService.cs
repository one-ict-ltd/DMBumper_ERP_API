
using ONEERP.Areas.MasterData.Models;
using ONEERP.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ONEERP.ERPServices.MasterData.Interfaces
{
    public interface IHelpService
    {
        Task<bool> SaveHelpMaster(string Id, HelpMasterViewModel helpMasterViewModel, List<HelpDetailViewModel> helpDetailViewModels, List<HelpMultiViewModel> helpMultiViewModels, List<HelpImageViewModel> helpImageViewModels);
        Task<JsonViewModel> GetHelpMasterListbyId(int helpId);
        Task<JsonViewModel> GetHelpDetailListbyId(int helpId, int helpDetailId);
        Task<JsonViewModel> GetHelpMultiListbyId(int helpId, int multiId);
        Task<JsonViewModel> GetHelpImageListbyId(int helpId, int helpImageId);
        Task<bool> DeleteHelpMasterListbyId(string Id, int helpId);
        Task<bool> DeleteHelpDetailListbyId(string Id, int helpDetailId);
        Task<bool> DeleteHelpMultiListbyId(string Id, int multiId);
        Task<bool> DeleteHelpImageListbyId(string Id, int imageId);
       
    }
}
