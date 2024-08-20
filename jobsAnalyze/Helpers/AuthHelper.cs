using jobsAnalyze.Business_Logic.Interfaces;
using jobsAnalyze.Helpers.Interfaces;
using jobsAnalyze.Models.DTO;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace jobsAnalyze.Helpers
{
    public class AuthHelper : IAuthHelper
    {
        private readonly IConfiguration _config;

        public AuthHelper(IConfiguration config)
        {
            _config = config;
        }

        public string CreateToken(string userId)
        {
            Claim[] claims = new Claim[] {
                new Claim("userId", userId)
            };
            string? tokenKeyValue = _config.GetSection("AppSettings:TokenKey").Value;
            byte[] key = Encoding.UTF8.GetBytes(tokenKeyValue);
            SymmetricSecurityKey tokenKey = new SymmetricSecurityKey(key);

            SigningCredentials credentials = new SigningCredentials(tokenKey, SecurityAlgorithms.HmacSha256Signature);

            SecurityTokenDescriptor descriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(claims),
                SigningCredentials = credentials,
                Expires = DateTime.Now.AddDays(1)
            };
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken token = tokenHandler.CreateToken(descriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
