using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Dtos;
using Domain.Dtos;
using Domain.Repositorios;
using Domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace myConsulta.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ConsultaController : ControllerBase
    {
        private readonly IConsultaService _consultaServices;

        public ConsultaController(IConsultaService consultaServices)
        {
            _consultaServices = consultaServices;
        }
        // GET: api/Consulta
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var consulta = await _consultaServices.GetNextConsultas();

                return Ok(consulta);

            }
            catch (Exception ex)
            { return BadRequest(ex); }
        }

        // GET: api/Consulta/
        [HttpGet("{cpf}")]
        public async Task<IActionResult> getConsultasByCpf(string cpf)
        {
            try
            {
                if (string.IsNullOrEmpty(cpf)) { BadRequest("Model invalido"); }

                var consultas = await _consultaServices.GetConsultasByCpf(cpf);
                return Ok(consultas);

            }
            catch (Exception ex)
            { return BadRequest(ex); }
        }

        // GET: api/Consulta/
        [HttpGet]
        [Route("{id}/Detalhes")]
        public async Task<IActionResult> getConsultasById(int id)
        {
            try
            {
                if (id > 0) { BadRequest("Model invalido"); }

                var consultas = await _consultaServices.GetConsultasById(id);
                return Ok(consultas);

            }
            catch (Exception ex)
            { return BadRequest(ex); }
        }

        [HttpGet]
        [Route("get-data/{data}")]
        public async Task<IActionResult> getConsultasByData(string data)
        {
            try
            {
                if (string.IsNullOrEmpty(data.ToString())) { BadRequest("Model invalido"); }

                var consultas = await _consultaServices.GetConsultasByData(data.ToString());
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
