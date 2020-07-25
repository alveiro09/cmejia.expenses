using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using User.API.Application.Contracts;
using User.API.Application.Model.Response;

namespace User.API.Application.Services
{
    public class TokenAuthentication : ITokenAuthentication
    {
        private readonly IConfiguration _configuration;

        public TokenAuthentication(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string BuildToken(TokenResponse tokenResponse)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var claims = new List<Claim>()
            {
                new Claim("Mail", string.IsNullOrEmpty(tokenResponse.Mail)? "": tokenResponse.Mail),
                new Claim("FirstName", string.IsNullOrEmpty(tokenResponse.FirstName)?  "": tokenResponse.FirstName),
                new Claim("LastName", string.IsNullOrEmpty(tokenResponse.LastName)?  "": tokenResponse.LastName),
                new Claim("UserName", string.IsNullOrEmpty(tokenResponse.UserName) ? "" : tokenResponse.UserName)
            };
            var token = new JwtSecurityToken(
              issuer: _configuration["Jwt:Issuer"],
              audience: _configuration["Jwt:Issuer"],
              claims: claims,
              expires: DateTime.Now.AddMinutes(30),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
