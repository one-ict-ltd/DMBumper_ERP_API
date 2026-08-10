using Microsoft.AspNetCore.Identity;
using ONEERP.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ONEERP.ERPServices.AuthService.Interfaces
{
    public interface IJwtFactoryService
    {
        Task<string> GenerateEncodedToken(string userName, ClaimsIdentity identity);
        Task<String> GenerateToken(string userName, string id, IList<string> roles);
        //Task<ClaimsIdentity> GenerateClaimsIdentity(string userName, string id, List<IdentityRole> roles);
        Task<Tuple<ApplicationUser, string, bool>> AuthenticateRequest(string authToken);
    }
}
