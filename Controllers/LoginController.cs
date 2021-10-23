using FilterDemoApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FilterDemoApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : Controller
    {
        private readonly IConfiguration _config;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public LoginController(IConfiguration config, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _config = config;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> LoginAsync([FromBody] UserViewModel userViewModel)
        {
            IActionResult response = Unauthorized();
            if (!ModelState.IsValid) response = BadRequest(userViewModel);
            else
            {
                var logInUser = await AuthenticateUserAsync(userViewModel);

                if (logInUser != null)
                {
                    var tokenString = GenerateJSONWebToken(logInUser);
                    response = Ok(new { token = tokenString });
                }
            }

            return response;
        }

        private string GenerateJSONWebToken(IdentityUser user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Issuer"],
              null,
              expires: DateTime.Now.AddMinutes(120),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private async Task<IdentityUser> AuthenticateUserAsync(UserViewModel userViewModel)
        {
            IdentityUser logInUser = null;

            var result = await _signInManager.PasswordSignInAsync(userViewModel.UserName, userViewModel.Password, false, false);

            if (result.Succeeded)
            {
                logInUser = new IdentityUser { UserName = "jasonmandabrandjai", Email = "jasonmandabrandja@gmail.com" };
            }
  
            return logInUser;
        }
    }
}