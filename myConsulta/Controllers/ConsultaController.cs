using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Dtos;
using Domain.Repositorios;
using Domain.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace myConsulta.Controllers
{
   
    [ApiController]
    [Route("api/[controller]")]
    public class ConsultaController : ControllerBase
    {
        private readonly IConsultaRepositorio _consultaServices;

        public ConsultaController(IConsultaRepositorio consultaServices)
        {
            _consultaServices = consultaServices;
        }
        // GET: api/Consulta
        [HttpGet]      
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var consulta = await _consultaServices.GetAllConsultas();

                return Ok(consulta);

            }
            catch (Exception ex)
            { return BadRequest(ex); }
        }

        // GET: api/Consulta/5
        [HttpGet("{cpf}")]       
        public async Task<IActionResult> getConsultasByCpf(string cpf)
        {
            try
            {
                if (string.IsNullOrEmpty(cpf)){ BadRequest("Model invalido"); }

                var consultas = await _consultaServices.GetConsultasByCpf(cpf);
                return Ok(consultas);

            }
            catch (Exception ex)
            { return BadRequest(ex); }
        }

        // POST: api/Consulta
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ConsultaDto consulta)
        {
            try
            {
                if (consulta == null) { BadRequest("Model invalido"); }

                await _consultaServices.CreateConsulta(consulta);
                return Ok();

            }
            catch (Exception ex)
            { return BadRequest(ex); }
        }

        // PUT: api/Consulta/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
