using System.Threading.Tasks;
using ONEERP.Data.Entity;

namespace ONEERP.ERPService.AuthService.Interfaces
{
    public interface ITokenAuthenticatorService
    {
       Task<AuthModel> Authentication(string auth_token);
    }

    public class AuthModel
    {
        public bool IsAuthorized { get; set; } = true;
        public string Jwts { get; set; }
        public ApplicationUser ApplicationUserInfo { get; set; }
    }
}
