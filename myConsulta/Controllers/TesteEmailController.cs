
using Application.Dtos;
using Domain.Repositorios;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
namespace myConsulta.Controllers
{
     [ApiController]
    [Route("api/[controller]")]
    public class TesteEmailController : ControllerBase
    {
        private readonly IEmailService _emailSender;
        public TesteEmailController(IEmailService emailSender, IHostingEnvironment env)
        {
            _emailSender = emailSender;
        }
       
        [HttpPost("send")]
        public async Task<IActionResult> EnviaEmail(EmailDto email)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _emailSender.SendConfirmationEmailAsync(email.Destino, email.Assunto, email.Mensagem,"");
                    return Ok("EmailEnviado");
                }
                catch (Exception)
                {
                    return BadRequest("EmailFalhou");
                }
            }
            return Ok(email);
        }
        public async Task TesteEnvioEmail(string email, string assunto, string mensagem)
        {
            try
            {
                //email destino, assunto do email, mensagem a enviar
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
       
    }
}