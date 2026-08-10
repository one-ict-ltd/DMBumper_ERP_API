using ONEERP.Areas.Auth.Models;
using ONEERP.Data.Entity.Auth;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ONEERP.ERPServices.Interfaces
{
    public interface IPageAssignService
    {
        Task<int> SaveUserAccessPage(UserAccessPage userAccessPage);

        Task<IEnumerable<NavbarOldViewModel>> GetNavbarDataByUser(string userName, string lang);

        Task<IEnumerable<UserAccessPageViewModel>> GetUserMenuAccessByUserType(string userTypeId);

        Task<bool> DeleteUserAccesspageByUserTypeId(string userTypeId);
        Task<IEnumerable<Navbar>> GetNavbars(string userName);
        Task<IEnumerable<ERPModule>> GetERPModules();
        Task<IEnumerable<UserAccessPageViewModel>> GetUserMenuAccessByUserTypeModule(string userTypeId,string userTypeIdM);

        Task<IEnumerable<UserAccessPageViewModel>> GetUserMenuAccessByUserTypeByModule(string userTypeId, int ModuleId);
        Task<IEnumerable<UserAccessPageViewModel>> GetUserMenuAccessByUserTypeByMenu(string userTypeId, int ModuleId);
    }
}
