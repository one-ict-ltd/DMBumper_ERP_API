using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ONEERP.Data;
using ONEERP.ERPService.AuthService.Interfaces;
using ONEERP.Helpers;

namespace ONEERP.ERPService.AuthService
{
    public class TokenAuthenticatorService : ITokenAuthenticatorService
    {
        private AuthModel authModel;
        private readonly ERPDbContext _context;
        private readonly IUserInfoes userInfoes;
        public TokenAuthenticatorService(IUserInfoes _userInfoes, ERPDbContext context)
        {
            _context = context;
            authModel = new AuthModel();
            userInfoes = _userInfoes;
        }

        public async Task<AuthModel> Authentication(string auth_token)
        {
            var uid = auth_token;   // Request.Headers["auth_token"];
            if (uid.Count() == 0)
            {
                bool status = false;
                authModel.IsAuthorized = status;
                string actionresult = "Invalid Token.";
                authModel.Jwts = await Tokens.changePasswordJwt(status, actionresult, new JsonSerializerSettings { Formatting = Formatting.Indented });
                return authModel;
            }

            var stream = uid;
            var handler = new JwtSecurityTokenHandler();
            var tokenS = handler.ReadToken(stream) as JwtSecurityToken;
            var jti = tokenS.Claims.First(claim => claim.Type == "Id").Value;
            authModel.ApplicationUserInfo = await userInfoes.GetUserBasicInfoesbyId(jti);

            if (authModel.ApplicationUserInfo.token != uid && authModel.ApplicationUserInfo != null)
            {
                bool status = false;
                authModel.IsAuthorized = status;
                string actionresult = "Invalid Token.";
                authModel.Jwts = await Tokens.changePasswordJwt(status, actionresult, new JsonSerializerSettings { Formatting = Formatting.Indented });
                return authModel;
            }

            return authModel;
        }
    }
}
