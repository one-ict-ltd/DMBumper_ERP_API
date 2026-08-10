using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ONEERP.Data.Entity;
using ONEERP.ERPService.AuthService.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Helpers
{
    public class Authentication : Controller
    {
       public object jwts;
        public ApplicationUser appUser;
        private IUserInfoes userInfoes;
        public Authentication(IUserInfoes _userInfoes)
        {
            this.userInfoes = _userInfoes;
            jwts = new object();
            appUser = new ApplicationUser();
        }

        async Task<bool> Authorization()
        {
            #region common
            var uid = Request.Headers["auth_token"];
            if (uid.Count() == 0)
            {
                bool status = false;
                string actionresult = "Invalid Token.";
                jwts = await Tokens.changePasswordJwt(status, actionresult, new JsonSerializerSettings { Formatting = Formatting.Indented });
                return false;
            }

            var stream = uid;
            var handler = new JwtSecurityTokenHandler();
            var tokenS = handler.ReadToken(stream) as JwtSecurityToken;
            var jti = tokenS.Claims.First(claim => claim.Type == "Id").Value;
            appUser = await userInfoes.GetUserBasicInfoesbyId(jti);

            if (appUser.token != uid && appUser != null)
            {
                bool status = false;
                string actionresult = "Invalid Token.";
                jwts = Tokens.changePasswordJwt(status, actionresult, new JsonSerializerSettings { Formatting = Formatting.Indented });
                return false;
            }
            return true;
            #endregion
        }
    }
}
