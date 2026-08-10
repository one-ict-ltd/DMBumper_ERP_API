using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ONEERP.Data.Entity;
using ONEERP.ERPService.AuthService.Interfaces;
namespace ONEERP.Helpers
{
    public class AuthModel
    {
        public bool IsAuthorized { get; set; } = true;
        public string Jwts { get; set; }
        public ApplicationUser ApplicationUserInfo { get; set; }
    }

    public class TokenAuthenticator
    {
        private AuthModel authModel;
        private readonly IUserInfoes userInfoes;
        public TokenAuthenticator(IUserInfoes _userInfoes)
        {
            authModel = new AuthModel();
            userInfoes = _userInfoes;
        }

        public async Task<AuthModel> AuthenticationStatus(string auth_token)
        {            
            return await Authenticate(auth_token);
        }
        async Task<AuthModel> Authenticate(string auth_token)
        {
            var uid = auth_token;
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
                //return authModel;
            }

            return authModel;
        }
    }
}
