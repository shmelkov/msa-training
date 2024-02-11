using Microsoft.IdentityModel.Tokens;
using Refit;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;

namespace AppGateway.Helpers
{
    public class TokenHelper
    {
        private readonly IUserServiceApi _userServiceApi;

        public TokenHelper(IUserServiceApi userServiceApi)
        {
            _userServiceApi = userServiceApi;
        }

        public async Task<string> GetJwtToken(ClaimsPrincipal claimsPrincipal)
        {
            UserInfo userInfo;

            var userId = GetUserIdFromClaims(claimsPrincipal);

            bool userDoesntExist = false;

            try
            {
                userInfo = await _userServiceApi.GetUserInfo(userId);
            }
            catch(ApiException e)
            {
                if (e.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    userInfo = GetUserInfoFromClaims(claimsPrincipal);
                    userDoesntExist = true;
                }
                else
                {
                   throw;
                }
            }

            var token = GenerateToken(userInfo);

            if(userDoesntExist)
                await _userServiceApi.CreateUser(userInfo, token);

            return token;
        }

        private static string GenerateToken(UserInfo userInfo)
        {
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                SigningCredentials = new SigningCredentials(
                    _symmetricSecurityKey, SecurityAlgorithms.HmacSha512Signature),

                Subject = new ClaimsIdentity(BuildUserClaimsFromUserInfo(userInfo))
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        private static List<Claim> BuildUserClaimsFromUserInfo(UserInfo userinfo)
        {
            var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Surname, userinfo.LastName),
                    new Claim(ClaimTypes.GivenName, userinfo.FirstName),
                    new Claim(ClaimTypes.NameIdentifier, userinfo.Id),
                    new Claim(ClaimTypes.Email, userinfo.Email),
                    new Claim(ClaimTypes.Name, userinfo.UserName)
                };

            if (userinfo.EmployeeId != null)
                claims.Add(new Claim("employeeId", userinfo.EmployeeId));


            foreach (var userRole in userinfo.Roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            return claims;
        }

        private static UserInfo GetUserInfoFromClaims(ClaimsPrincipal claimsPrincipal)
        {
            return new UserInfo
            {
                Id = claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier),
                UserName = claimsPrincipal.FindFirstValue("preferred_username"),
                Email = claimsPrincipal.FindFirstValue(ClaimTypes.Email),
                LastName = claimsPrincipal.FindFirstValue(ClaimTypes.Surname),
                FirstName = claimsPrincipal.FindFirstValue(ClaimTypes.GivenName),
                MiddleName = String.Empty
            };
        }

        private static string GetUserIdFromClaims(ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        private static TokenValidationParameters GetValidationParameters()
        {
            return new TokenValidationParameters()
            {
                ValidateLifetime = false,
                ValidateAudience = false,
                ValidateIssuer = false,
                IssuerSigningKey = _symmetricSecurityKey
            };
        }

        public static bool ValidateToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var validationParameters = GetValidationParameters();

            IPrincipal principal = tokenHandler.ValidateToken(token, validationParameters, out var validatedToken);

            return true;
        }

        private static SymmetricSecurityKey _symmetricSecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(AuthOptions.Jwt.SecretKey));
    }
}
