
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
    public class EmailController : ControllerBase
    {
        private readonly IEmailService _emailSender;
        public EmailController(IEmailService emailSender, IHostingEnvironment env)
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


                   return Ok("EmailEnviado");
                }
                catch (Exception)
                {
                    return BadRequest("EmailFalhou");
                }
            }
            return Ok(email);
        }
        
       
    }
}