using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using EventCalendarServer.Identity;
using Microsoft.AspNetCore.Authorization;

namespace EventCalendarServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public AuthController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(string UserName, string Password)
        {
            ApplicationUser user = await _userManager.FindByNameAsync(UserName);

            if (user is null)
            {
                return NotFound();
            }

            var result = await _userManager.CheckPasswordAsync(user, Password);



            if (!result)
            {
                return Ok(new { result = false });
            }
            SymmetricSecurityKey IssuerSigningKey =
                    new(Encoding.UTF8.GetBytes("CSUN590@8:59PM#cretKey"));

            SigningCredentials signingCreds = new(IssuerSigningKey, SecurityAlgorithms.HmacSha256);

            
            JwtSecurityToken tokenOptions = new JwtSecurityToken(
                issuer: "https://localhost:44382/",
                audience: "https://localhost:44200/",
                claims: new List<Claim>(),
                expires: DateTime.Now.AddMinutes(5),
                signingCredentials: signingCreds
            );

            
            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
            return Ok(new { Token = tokenString });


            // return Unauthorized();
        }


        [HttpPost]
        [Route("Create")]

        public async Task<IActionResult> Create(string UserName, string Password)
        {

            ApplicationUser user = new()
            {
                UserName = UserName,
                Email = $"{UserName}@gmail.com",
                EmailConfirmed = true
            };
            var result = await _userManager.CreateAsync(user, Password);
            return Ok(result);
        }

        [Authorize]
        [HttpPost]
        [Route("Logout")]

        public async Task<IActionResult> Logout(string UserName)
        {
            ApplicationUser user = await _userManager.FindByNameAsync(UserName);

            if (user is null)
            {
                return NotFound();
            }


            SymmetricSecurityKey IssuerSigningKey =
                new(Encoding.UTF8.GetBytes("CSUN590@8:59PM#cretKey"));

            SigningCredentials signingCreds = new(IssuerSigningKey, SecurityAlgorithms.HmacSha256);


            JwtSecurityToken tokenOptions = new JwtSecurityToken(
                issuer: "https://localhost:44382/",
                audience: "https://localhost:44200/",
                claims: new List<Claim>(),
                expires: DateTime.Now,
                signingCredentials: signingCreds
            );


            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
            return Ok(new { Token = tokenString });

        }

        [Authorize]
        [HttpPost]
        [Route("CheckLoginState")]

        public async Task<IActionResult> LoginState()
        {



            return Ok("success");

        }



    }



}
