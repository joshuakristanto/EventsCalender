using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
//using Microsoft.IdentityModel.Tokens.Jwt;
using EventCalendar.Identity;
using Microsoft.AspNetCore.Authorization;
using Google.Apis.Auth;

namespace EventCalendar.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
       // private readonly SignInManager<IdentityUser> _signInManager;
        public AuthController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
         //   _signInManager = signInManager;
        }

        public class AuthenticateRequest
        {
            [Required]
            public string IdToken { get; set; }
        }

        //Firebase 

        [AllowAnonymous]
        [HttpPost("FirebaseAuthenticate")]
        public IActionResult Authenticate([FromBody] AuthenticateRequest data)
        {
            GoogleJsonWebSignature.ValidationSettings settings = new GoogleJsonWebSignature.ValidationSettings();

            // Change this to your google client ID
            settings.Audience = new List<string>() { "218984349286-j5ri6sd2vkl0u85j2h6g41glgekrlis1.apps.googleusercontent.com" };

            GoogleJsonWebSignature.Payload payload = GoogleJsonWebSignature.ValidateAsync(data.IdToken, settings).Result;

            //  var resultTrue = await _signInManager.PasswordSignInAsync(UserName, Password, false, false);
            SymmetricSecurityKey IssuerSigningKey =
                new(Encoding.UTF8.GetBytes("CSUN590@8:59PM#cretKey"));

            SigningCredentials signingCreds = new(IssuerSigningKey, SecurityAlgorithms.HmacSha256);


            JwtSecurityToken tokenOptions = new JwtSecurityToken(
                issuer: "https://www.eventcalendar-2.azurewebsites.net",
                audience: "https://www.eventcalendar-2.azurewebsites.net",
                claims: new List<Claim>(),
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: signingCreds
            );




            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
            return Ok(new { Token = tokenString });
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

          //  var resultTrue = await _signInManager.PasswordSignInAsync(UserName, Password, false, false);
            SymmetricSecurityKey IssuerSigningKey =
                    new(Encoding.UTF8.GetBytes("CSUN590@8:59PM#cretKey"));

            SigningCredentials signingCreds = new(IssuerSigningKey, SecurityAlgorithms.HmacSha256);

            
            JwtSecurityToken tokenOptions = new JwtSecurityToken(
                issuer: "https://www.eventcalendar-2.azurewebsites.net",
                audience: "https://www.eventcalendar-2.azurewebsites.net",
                claims: new List<Claim>(),
                expires: DateTime.Now.AddMinutes(30),
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
            /*
            ApplicationUser user = await _userManager.FindByNameAsync(UserName);

            if (user is null)
            {
                return NotFound();
            }
            
            SymmetricSecurityKey IssuerSigningKey =
                new(Encoding.UTF8.GetBytes("CSUN590@8:59PM#cretKey"));

            SigningCredentials signingCreds = new(IssuerSigningKey, SecurityAlgorithms.HmacSha256);


            JwtSecurityToken tokenOptions = new JwtSecurityToken(
                issuer: "https://www.eventcalendar-2.azurewebsites.net",
                audience: "https://www.eventcalendar-2.azurewebsites.net",
                claims: new List<Claim>(),
                expires: DateTime.Now.AddMinutes(0),

                signingCredentials: signingCreds
            );

           
          

            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
            return Ok(new { Token = tokenString });
            */
            return Ok();

        }

        [Authorize]
        [HttpPost]
        [Route("CheckLoginState")]

        public async Task<IActionResult> LoginState()
        {



            return Ok();

        }



    }



}
