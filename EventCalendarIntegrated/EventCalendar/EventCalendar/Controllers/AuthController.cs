using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.Design;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
//using Microsoft.IdentityModel.Tokens.Jwt;
using EventCalendar.Identity;
using EventCalendar.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Google.Apis.Auth;

namespace EventCalendar.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;

        private readonly RoleManager<IdentityRole> _roleManager;
       // private readonly SignInManager<IdentityUser> _signInManager;
       private readonly IConfiguration _configuration;
       private readonly IAuthentication _auth;
        public AuthController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration, IAuthentication auth)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _auth = auth;
            //   _signInManager = signInManager;
        }

        public class AuthenticateRequest
        {
            [Required]
            public string IdToken { get; set; }
        }

        //Authenticate with Google 

        [AllowAnonymous]
        [HttpPost("GoogleAuthenticate")]
        public IActionResult Authenticate([FromBody] AuthenticateRequest data)
        {
            GoogleJsonWebSignature.ValidationSettings settings = new GoogleJsonWebSignature.ValidationSettings();
            
            // Change this to your google client ID
            settings.Audience = new List<string>() { _configuration["GoogleCloud:ClientId"] };

            try
            {
                GoogleJsonWebSignature.Payload payload = GoogleJsonWebSignature.ValidateAsync(data.IdToken, settings).Result;
                if (!payload.EmailVerified)
                {
                    //  return Ok(new {result = false});
                    return NotFound();
                }


                var tokenString = _auth.GenerateToken("Guest");

                //  var resultTrue = await _signInManager.PasswordSignInAsync(UserName, Password, false, false);

            


                return Ok(new { Token = tokenString });
            }
            catch (InvalidJwtException)
            {
                return NotFound();
            }
          
            
         
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
             //   return Ok(new { result = false });
                return NotFound();
            }
          
            var role = "Guest";
            var guestRoleResult = await _userManager.IsInRoleAsync(user, "Guest");
            if (guestRoleResult )
            {
             
              role = "Guest";
            }

            var adminRoleResult = await _userManager.IsInRoleAsync(user, "Admin");
            if (adminRoleResult)
            {
         

             role = "Admin";
            }
            
           
            var tokenString = _auth.GenerateToken(role);
            return Ok(new { Token = tokenString });


            // return Unauthorized();
        }


        [HttpPost]
        [Route("Create")]

        public async Task<IActionResult> Create(string UserName, string Password, string Role)
        {
            
            if (Role == null)
            {
                Role = "guest";
            }
            
            ApplicationUser user = new()
            {
                UserName = UserName,
                Email = $"{UserName}@gmail.com",
                EmailConfirmed = true
            };
            var result = await _userManager.CreateAsync(user, Password);
            var newRole = await _roleManager.RoleExistsAsync(Role);

            if (!newRole)
            {
                var roleResult = await _roleManager.CreateAsync(new IdentityRole(Role));
            }
            var role = await _userManager.AddToRoleAsync(user, Role);
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
