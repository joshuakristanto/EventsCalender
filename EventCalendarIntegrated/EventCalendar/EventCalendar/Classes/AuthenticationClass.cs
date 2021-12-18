using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using EventCalendar.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace EventCalendar.Classes
{
    public class AuthenticationClass : IAuthentication
    {
        public string GenerateToken(string role)
        {
            SymmetricSecurityKey IssuerSigningKey =
                new(Encoding.UTF8.GetBytes("CSUN590@8:59PM#cretKey"));

            SigningCredentials signingCreds = new(IssuerSigningKey, SecurityAlgorithms.HmacSha256);
            List<Claim> claim = new List<Claim>();
            claim.Add(new Claim(ClaimTypes.Role, role));

            JwtSecurityToken tokenOptions = new JwtSecurityToken(
                issuer: "https://www.eventcalendar-2.azurewebsites.net",
                audience: "https://www.eventcalendar-2.azurewebsites.net",
                claims: claim,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: signingCreds
            );




            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

            return tokenString;
        }
    }
}
