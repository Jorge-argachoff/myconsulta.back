using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Dtos;
using Domain.Dtos;
using Domain.Repositorios;
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
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        private readonly AppSettings _appSettings;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IUserService userService;
        private readonly IEmailService emailSender;

        public AuthController(
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IOptions<AppSettings> appSettings,
            IUserService userService,
            IEmailService emailSender)
        {
            this.roleManager = roleManager;
            this.userService = userService;
            this.emailSender = emailSender;
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
                
                var pessoaId = await userService.AddPessoa(registerUserDto);

                        var user = new ApplicationUser
                        {
                            PessoaId = pessoaId,
                            UserName = registerUserDto.Email,
                            Email = registerUserDto.Email,
                            EmailConfirmed = false
                        };

                var result = await _userManager.CreateAsync(user, registerUserDto.Password);
               
                if (!result.Succeeded) return BadRequest(result.Errors);

                // var confirmationToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            //    string confirmationLink = Url.Action("ConfirmEmail", 
            //   "Auth", new { userid = user.Id, 
            //    token = confirmationToken }, 
            //    Request.Scheme);

            //     await emailSender.SendConfirmationEmailAsync(registerUserDto.Email, "Confirme seu cadastro", "",confirmationLink);
                
                if ( string.IsNullOrEmpty(registerUserDto.Role))
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
                return BadRequest(ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpGet("confirm")]
        public async Task<IActionResult> ConfirmEmail([FromQuery]ConfirmEmailDto confirm)
        {
            if (confirm == null 
                || string.IsNullOrEmpty(confirm.UserId)  
                || string.IsNullOrEmpty(confirm.Token))
            {
                return BadRequest("Dados Invalidos");
            }

            var user = await _userManager.FindByIdAsync(confirm.UserId);
            
            if(await _userManager.IsEmailConfirmedAsync(user))
            {
                return BadRequest("Email já confirmado");
            }

            if (user == null)
            {
                return BadRequest("Usuario nao encontrado");
            }


            var result = await _userManager.ConfirmEmailAsync(user,confirm.Token);
            
            if (result.Succeeded)
            {
                return Ok("Confirmado com sucesso");
            }

            return BadRequest("Email não confirmado");
            
        }



        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginUserDto loginUserDto)
        {
            try
            {
                
            if (!ModelState.IsValid) return BadRequest(ModelState.Values.SelectMany(e => e.Errors));

            var userMan = await _userManager.FindByEmailAsync(loginUserDto.Email);

            // if (userMan != null && !userMan.EmailConfirmed )
            // {
            //     return BadRequest("Email não confirmado, entre no seu email para confirmar.");
            // }

            var result = await _signInManager.PasswordSignInAsync(loginUserDto.Email, loginUserDto.Password, false, true);

            if (result.Succeeded)
            {
                UserDto user = new UserDto() { Email = loginUserDto.Email };
                user.Token = await GerarJwt(loginUserDto.Email);
                return Ok(new { user });

            }
            return BadRequest("Usuario ou senha invalidos");
            }
            catch (System.Exception ex)
            {
                 // TODO
            return BadRequest(ex.Message);
            }
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