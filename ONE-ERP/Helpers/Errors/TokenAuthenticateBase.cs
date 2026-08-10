namespace ONEERP.Helpers.Errors
{
    public class TokenAuthenticateBase
    {
        //async Task<bool> Authentication(string auth_token, out ApplicationUser user)
        //{
        //    #region common
        //    var uid = auth_token;// Request.Headers["auth_token"];
        //    if (uid.Count() == 0)
        //    {
        //        bool status = false;
        //        string actionresult = "Invalid Token.";
        //        jwts = await Tokens.changePasswordJwt(status, actionresult, new JsonSerializerSettings { Formatting = Formatting.Indented });
        //        return false;
        //    }

        //    var stream = uid;
        //    var handler = new JwtSecurityTokenHandler();
        //    var tokenS = handler.ReadToken(stream) as JwtSecurityToken;
        //    var jti = tokenS.Claims.First(claim => claim.Type == "Id").Value;
        //    user = await userInfoes.GetUserBasicInfoesbyId(jti);

        //    if (user.token != uid && user != null)
        //    {
        //        bool status = false;
        //        string actionresult = "Invalid Token.";
        //        jwts = Tokens.changePasswordJwt(status, actionresult, new JsonSerializerSettings { Formatting = Formatting.Indented });
        //        return false;
        //    }
        //    return true;
        //    #endregion
        //}
    }
}