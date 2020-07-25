using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Dtos;
using Domain.Dtos;
using Infra.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace myConsulta.Controllers
{

    [Route("api/user")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;

        private readonly AppSettings _appSettings;
        private readonly RoleManager<IdentityRole> roleManager;

        public AuthController(SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IOptions<AppSettings> appSettings)
        {
            this.roleManager = roleManager;
            this._signInManager = signInManager;
            this._userManager = userManager;
            this._appSettings = appSettings.Value;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Registrar(RegisterUserDto registerUserDto)
        {
            try
            {

                if (!ModelState.IsValid) return BadRequest(ModelState.Values.SelectMany(e => e.Errors));

                var user = new IdentityUser
                {

                    UserName = registerUserDto.Email,
                    Email = registerUserDto.Email,
                    EmailConfirmed = true
                };

                var result = await _userManager.CreateAsync(user, registerUserDto.Password);

                if (!result.Succeeded) return BadRequest(result.Errors);
                var identityuser = await _userManager.FindByEmailAsync(user.Email);

                if (registerUserDto.Role == null)
                    await _userManager.AddToRoleAsync(user, "User");
                else
                {
                    
                    bool x = await roleManager.RoleExistsAsync(registerUserDto.Role);
                    if (x)
                        await _userManager.AddToRoleAsync(user, registerUserDto.Role);
                    else return BadRequest("Role não existe");
                }

                await _signInManager.SignInAsync(user, false);

                UserDto userDto = new UserDto();
                userDto.Token = await GerarJwt(registerUserDto.Email);
                return Ok(new { userDto });

            }
            catch (System.Exception ex)
            {

                throw;
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginUserDto loginUserDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState.Values.SelectMany(e => e.Errors));

            var result = await _signInManager.PasswordSignInAsync(loginUserDto.Email, loginUserDto.Password, false, true);


            if (result.Succeeded)
            {
                UserDto user = new UserDto() { Email = loginUserDto.Email };
                user.Token = await GerarJwt(loginUserDto.Email);
                return Ok(new { user });

            }
            return BadRequest("Usuario ou senha invalidos");
        }

        private async Task<string> GerarJwt(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            var tokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = _appSettings.Emissor,
                Audience = _appSettings.ValidoEm,
                Expires = DateTime.UtcNow.AddHours(_appSettings.ExpiracaoHoras),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            return tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));
        }

        [HttpPost("role")]
        public async Task<IActionResult> CreateRole(RoleDto roleDto)
        {
            bool x = await roleManager.RoleExistsAsync(roleDto.Name);
            if (!x)
            {
                // first we create Admin rool    
                var role = new IdentityRole();
                role.Name = roleDto.Name;
                await roleManager.CreateAsync(role);

                return Ok();
            }
            else
                return BadRequest("Role já existe");
        }
        private async Task<IActionResult> CreateRole(string roleName)
        {
            bool x = await roleManager.RoleExistsAsync(roleName);
            if (!x)
            {
                // first we create Admin rool    
                var role = new IdentityRole();
                role.Name = roleName;
                await roleManager.CreateAsync(role);

                return Ok();
            }
            else
                return BadRequest("Role já existe");
        }

    }
}