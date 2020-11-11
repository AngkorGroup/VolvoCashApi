using System;
using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using IdentityModel;

namespace VolvoCash.DistributedServices.Seedwork.Utils
{
    public class TokenManager : ITokenManager
    {
        #region Members
        private readonly IConfiguration _configuration;
        #endregion

        #region Constructor
        public TokenManager(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        #endregion

        #region Public Methods
        public string GenerateTokenJWT(Guid sessionId, int userId, string username, string names, string email, string role)
        {
            var symmetricSecurityKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(_configuration["JWT:SecretKey"])
                );
            var signingCredentials = new SigningCredentials(
                    symmetricSecurityKey, SecurityAlgorithms.HmacSha256
                );
            var header = new JwtHeader(signingCredentials);
            var claims = new[] {
                 new Claim(JwtClaimTypes.JwtId, sessionId.ToString()),
                 new Claim(JwtClaimTypes.Subject, userId.ToString()),
                 new Claim(JwtClaimTypes.PreferredUserName, username),
                 new Claim(JwtClaimTypes.GivenName, names),
                 new Claim(JwtClaimTypes.Email, email),
                 new Claim(ClaimTypes.Role, role)
             };
            var expiresMinutes = int.Parse(_configuration["JWT:ExpiresMinutes"]);
            var payload = new JwtPayload(
                    issuer: _configuration["JWT:Issuer"],
                    audience: _configuration["JWT:Audience"],
                    claims: claims,
                    notBefore: DateTime.UtcNow,
                    expires: DateTime.UtcNow.AddMinutes(expiresMinutes)
                );
            var _Token = new JwtSecurityToken(
                    header,
                    payload
                );
            return new JwtSecurityTokenHandler().WriteToken(_Token);
        }
        #endregion
    }
}
