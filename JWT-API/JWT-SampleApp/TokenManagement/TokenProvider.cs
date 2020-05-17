using JWT_SampleApp.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;
using System.Web;

namespace JWT_SampleApp.TokenManagement
{
    public static class TokenProvider
    {
        //private static byte[] symmetricKey = Utilities.GetBytes("SampleKey");
        private const string issuerName = "JWT Sample";
        private static string allowedAudience = "http://localhost/";//todo eliminating audience
        public const string Secret = "856FECBA3B06519C8DDDBC80BB080553";

        //GenerateToken
        public static string GenerateToken(ApplicationUser user)
        {
            var Secretkey = Convert.FromBase64String(Secret);
            try
            {

                //Claims
                var claimsIdentidy = new ClaimsIdentity(new[]
                            {
                              new Claim("TokenVersion","1"),
                              new Claim(ClaimTypes.Name, user.Username),
                              new Claim("UserId",user.UserId==0?"0":user.UserId.ToString()),
                              new Claim("FirstName", user.FirstName),
                            });


                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = claimsIdentidy,
                    Issuer = issuerName,
                    Audience = allowedAudience,
                    Expires = DateTime.Now.AddDays(1),
                    //SigningCredentials = new SigningCredentials(
                    //    new SymmetricSecurityKey(symmetricKey),
                    //    SecurityAlgorithms.HmacSha256),
                    SigningCredentials = new SigningCredentials(
                        new SymmetricSecurityKey(Secretkey),
                        SecurityAlgorithms.HmacSha256),
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var token = tokenHandler.CreateToken(tokenDescriptor);

                //return tokenHandler.WriteToken(token).ToString();
                return new System.Net.Http.Headers.AuthenticationHeaderValue("BEARER", tokenHandler.WriteToken(token)).ToString();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        //ValidateToken
        public static bool ValidateToken(string authenticationToken)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var validationParameters = TokenProvider.GetValidationParameters();

            SecurityToken validatedToken;
            IPrincipal principal = tokenHandler.ValidateToken(authenticationToken, validationParameters, out validatedToken);
            Thread.CurrentPrincipal = principal;
            HttpContext.Current.User = principal;
            return true;
        }

        private static TokenValidationParameters GetValidationParameters()
        {
            var Secretkey = Convert.FromBase64String(Secret);
            return new TokenValidationParameters()
            {
                ValidAudience = allowedAudience,
                ValidIssuer = issuerName,
                //IssuerSigningKey =  new SymmetricSecurityKey(symmetricKey),
                IssuerSigningKey = new SymmetricSecurityKey(Secretkey),
                ValidateIssuerSigningKey = true,
                ValidateIssuer = true,
                ValidateAudience = true
            };
        }

        public static string GetClaimValue(this System.Security.Principal.IPrincipal user, string key)
        {
            try
            {
                var claim = ((System.Security.Claims.ClaimsPrincipal)user).Claims.Where(c => c.Type == key).First();
                return claim.Value.ToString();
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}