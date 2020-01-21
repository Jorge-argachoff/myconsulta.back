using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Dtos;
using Domain.Repositorios;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace myConsulta.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConfiguracaoController : ControllerBase
    {

        private readonly IConfiguracaoRepositorio _configuracaoServices;

        public ConfiguracaoController(IConfiguracaoRepositorio configuracaoServices)
        {
            _configuracaoServices = configuracaoServices;
        }

        // GET: api/Configuracao
        [HttpGet]
        public async Task<IActionResult> GetAllFormData()
        {
            try
            {
                var horarios = await _configuracaoServices.GetAllHours();                

                var nomesConsulta = await _configuracaoServices.GetAllNomes();

                return Ok(new { horarios, nomesConsulta });

            }
            catch (Exception ex)
            { return BadRequest(ex); }
        }


        // POST: api/Configuracao
        [HttpPost]
        [Route("add-hora")]
        public async Task<IActionResult> AddHour([FromBody] HorarioDto horario)
        {
            try
            {
                if (horario == null) return BadRequest("Horario invalido!");

                await _configuracaoServices.CreateHorario(horario);

                return Ok();
            }
            catch (Exception ex)
            { return BadRequest(ex); }
        }
        [HttpPost]
        [Route("add-nome")]
        public async Task<IActionResult> AddName([FromBody] NomeConsultaDto nome)
        {
            try
            {
                if (nome == null) return BadRequest("objeto invalido!");

                await _configuracaoServices.CreateNome(nome);

                return Ok();
            }
            catch (Exception ex)
            { return BadRequest(ex); }
        }

        // PUT: api/Configuracao/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete]
        [Route("excluir-hora/{id}")]
        public void DeleteHour(int id)
        {
            try
            {
                if (id <= 0) BadRequest("Horario invalido!");

                _configuracaoServices.deleteHorario(id);               
            }
            catch (Exception ex)
            { BadRequest(ex); }
        }

        [HttpDelete]
        [Route("excluir-nome/{id}")]
        public void DeleteNome(int id)
        {
            try
            {
                if (id <= 0) BadRequest("Nome invalido!");

                _configuracaoServices.deleteNome(id);
            }
            catch (Exception ex)
            { BadRequest(ex); }
        }
    }
}
