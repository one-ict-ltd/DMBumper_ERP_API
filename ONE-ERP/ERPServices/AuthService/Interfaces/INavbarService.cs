using ONEERP.Areas.Auth.Models;
using ONEERP.Data.Entity.Auth;
using ONEERP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.ERPServices.AuthService.Interfaces
{
    public interface INavbarService
    {
        #region Old NavBar
        Task<bool> DeleteNavbarItemById(int id);
        Task<IEnumerable<Navbar>> GetNavbarItem();
        Task<IEnumerable<Navbar>> GetNavigationMenu(string userName);
        Task<IEnumerable<Navbar>> GetNavbarItemByParent();
        Task<IEnumerable<Navbar>> GetNavbarItemByParentByModule(int moduleId, int isParent);
        Task<Navbar> GetNavbarItemById(int id);
        Task<bool> SaveNavbarItem(Navbar navbar);
        Task<IEnumerable<Navbar>> GetNavbarItemByParentIdByModule(int moduleId, int isParent);
        Task<int> GetNavbarParentIdById(int id);
        Task<IEnumerable<Navbar>> GetNavbarItemByModule(int id);

        #endregion


       
    }
}
