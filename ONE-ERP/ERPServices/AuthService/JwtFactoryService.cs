using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using ONEERP.Areas.Auth.Models;
using ONEERP.Data.Entity;
using ONEERP.ERPService.AuthService.Interfaces;
using ONEERP.ERPServices.AuthService.Interfaces;
using ONEERP.Helpers;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ONEERP.ERPServices.AuthService
{
    public class JwtFactoryService : IJwtFactoryService
    {
        public readonly JwtIssuerOptions _jwtIssuerOption;
        private readonly IUserInfoes _userInfoes;

        //private readonly JwtIssuerOptions _jwtOptions;

        //private readonly UserManager<ApplicationUser> _userManager;

        //private readonly RoleManager<IdentityRole> _roleManager;

        public JwtFactoryService(IOptions<JwtIssuerOptions> jwtIssuerOption, IUserInfoes userInfoes)
        {
            _jwtIssuerOption = jwtIssuerOption.Value;
            ThrowIfInvalidOptions(_jwtIssuerOption);
            _userInfoes = userInfoes;
            //_userManager = userManager;
            //_roleManager = roleManager;
        }

        //public JwtFactoryService(IOptions<JwtIssuerOptions> jwtIssuerOption)
        //{
        //    _jwtIssuerOption = jwtIssuerOption.Value;
        //}
        public async Task<string> GenerateEncodedToken(string userName, ClaimsIdentity identity)
        {
            var claims = new List<Claim>(new[]
            {
                 new Claim(JwtRegisteredClaimNames.Sub, userName),
                 new Claim(JwtRegisteredClaimNames.Jti, await _jwtIssuerOption.JtiGenerator()),
                 new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(_jwtIssuerOption.IssuedAt).ToString(), ClaimValueTypes.Integer64),
                 identity.FindFirst("Id")
             });

            claims.AddRange(identity.FindAll("Rol"));

            // Create the JWT security token and encode it.
            var jwt = new JwtSecurityToken(
                issuer: _jwtIssuerOption.Issuer,
                audience: _jwtIssuerOption.Audience,
                claims: claims,
                notBefore: _jwtIssuerOption.NotBefore,
                expires: _jwtIssuerOption.Expiration,
                signingCredentials: _jwtIssuerOption.SigningCredentials
            );

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return encodedJwt;
        }
        public async Task<string> GenerateToken(string userName, string id, IList<string> roles)
        {
            var Claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub,userName),
                new Claim(JwtRegisteredClaimNames.Jti, await _jwtIssuerOption.JtiGenerator()),
                new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(_jwtIssuerOption.IssuedAt).ToString(), ClaimValueTypes.Integer64),
                new Claim("id", id),
                new Claim("rol",roles[0])
            };

            var jwt = new JwtSecurityToken(
                issuer: _jwtIssuerOption.Issuer,
                audience: _jwtIssuerOption.Audience,
                claims: Claims,
                notBefore: _jwtIssuerOption.NotBefore,
                expires: _jwtIssuerOption.Expiration,
                signingCredentials: _jwtIssuerOption.SigningCredentials
            );

            return new JwtSecurityTokenHandler().WriteToken(jwt);

        }
        
        private static void ThrowIfInvalidOptions(JwtIssuerOptions options)
        {
            if (options == null) throw new ArgumentNullException(nameof(options));

            if (options.ValidFor <= TimeSpan.Zero)
            {
                throw new ArgumentException("Must be a non-zero TimeSpan.", nameof(JwtIssuerOptions.ValidFor));
            }

            if (options.SigningCredentials == null)
            {
                throw new ArgumentNullException(nameof(JwtIssuerOptions.SigningCredentials));
            }

            if (options.JtiGenerator == null)
            {
                throw new ArgumentNullException(nameof(JwtIssuerOptions.JtiGenerator));
            }
        }

        #region Global API Request authenticator

        //By Tuhin
        // Summary:
        //     Initializes a new instance of the JwtFactoryService class with a specified
        //     message and send user data if valid token.
        //
        // Parameters: takes a string value which is authToken received from request header
        //   message:
        //   
        //
        //   innerException:
        //     The exception that is the cause of the current exception, or a null reference
       
        public async Task<Tuple<ApplicationUser, string, bool>> AuthenticateRequest(string authToken)
        {



            if (string.IsNullOrEmpty(authToken))
            {
                string errorResult = await GenerateErrorJwt("Invalid Token.");
                return new Tuple<ApplicationUser, string, bool>(null, errorResult, false);
            }

            try
            {
                var handler = new JwtSecurityTokenHandler();
                var token = handler.ReadToken(authToken) as JwtSecurityToken;

                if (token == null)
                {
                    string errorResult = await GenerateErrorJwt("Invalid Token.");
                    return new Tuple<ApplicationUser, string, bool>(null, errorResult, false);
                }

                var jti = token.Claims.FirstOrDefault(claim => claim.Type == "Id")?.Value;
                if (string.IsNullOrEmpty(jti))
                {
                    string errorResult = await GenerateErrorJwt("Invalid Token.");
                    return new Tuple<ApplicationUser, string, bool>(null, errorResult, false);
                }

                var user = await _userInfoes.GetUserBasicInfoesbyId(jti);
                if (user == null || user.token != authToken)
                {
                    //string errorResult = await GenerateErrorJwt("Invalid Token.");
                    return new Tuple<ApplicationUser, string, bool>(null, "Invalid Token.", false);
                }

                return new Tuple<ApplicationUser, string, bool>(user, "Authentic User", true);
            }
            catch (Exception ex)
            {
                //string errorResult = await GenerateErrorJwt("Error authenticating token.");
                return new Tuple<ApplicationUser, string, bool>(null, "Error authenticating token", false);
            }
        }

        private async Task<string> GenerateErrorJwt(string message)
        {
            bool status = false;
            return await Tokens.changePasswordJwt(
                status,
                message,
                new JsonSerializerSettings { Formatting = Formatting.Indented }
            );
        }

        #endregion

        /// <returns>Date converted to seconds since Unix epoch (Jan 1, 1970, midnight UTC).</returns>
        private static long ToUnixEpochDate(DateTime date)
          => (long)Math.Round((date.ToUniversalTime() -
                               new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero))
                              .TotalSeconds);
    }
}
