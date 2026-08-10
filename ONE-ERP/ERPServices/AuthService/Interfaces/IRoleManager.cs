using Microsoft.AspNetCore.Identity;
using ONEERP.Areas.Auth.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.ERPServices.AuthService.Interfaces
{
    public interface IRoleManager
    {
        Task<List<ApplicationRoleViewModel>> GetAllRolesAsync();
        Task<IdentityResult> CreateAsync(ApplicationRoleViewModel model);
        Task<IdentityResult> DeleteAsync(ApplicationRoleViewModel model);
        Task<IEnumerable<string>> GetRoleIds(string userName);
    }
}
