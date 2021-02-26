
using Application.Dtos;
using Domain.Dtos;
using Domain.Repositorios;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;
namespace myConsulta.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MessageController : ControllerBase
    {
        private IHubContext<ChatHub> _hubcontext;

        public MessageController(IHubContext<ChatHub> hubcontext)
        {
            _hubcontext = hubcontext;
        }

        [HttpPost("send")]
        public async Task<IActionResult> EnviarMensagem(ChatMessageDto chat)
        {
            try
            {
                await _hubcontext.Clients.All.SendAsync("send", chat.Message);

                return Ok("EmailEnviado");
            }
            catch (Exception)
            {
                return BadRequest("EmailFalhou");
            }

        }


    }
}