using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using EventCalendar.Identity;
using EventCalendar.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace EventCalendar.Classes
{
    public class AuthenticationClass : IAuthentication
    {
        /*
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        // private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly IAuthentication _auth;
        public  AuthenticationClass(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration, IAuthentication auth)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _auth = auth;
        }
        */
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
