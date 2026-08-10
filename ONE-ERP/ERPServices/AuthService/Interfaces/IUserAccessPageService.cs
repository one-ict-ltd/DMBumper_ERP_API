using ONEERP.Areas.Auth.Models;

using ONEERP.Data.Entity;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ONEERP.ERPService.AuthService.Interfaces
{
    public interface IUserAccessPageService
    {
        Task<IEnumerable<UserAccessPageListViewModel>> GetUserAccessPageList();
    }
}
