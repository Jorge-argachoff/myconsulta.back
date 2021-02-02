using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            { return BadRequest(ex.Message); }
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
            { return BadRequest(ex.Message); }
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
            { return BadRequest(ex.Message); }
        }

        [HttpGet]
        [Route("consulta/{data}")]
        public async Task<IActionResult> getConsultasByData(string data)
        {
            try
            {
                if (string.IsNullOrEmpty(data.ToString())) { BadRequest("Model invalido"); }

                var consultas = await _consultaServices.GetConsultasByData(data.ToString());
                return Ok(consultas);

            }
            catch (Exception ex)
            { return BadRequest(ex.Message); }
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
            { return BadRequest(ex.Message); }
        }

        [HttpPost]
        [Route("comment")]
        public async Task<IActionResult> PostComent([FromBody] ComentarioDto comentario)
        {
            try
            {
                if (comentario == null) BadRequest("Comentario invalido");

                await _consultaServices.CreateComentario(comentario);

                return Ok();

            }
            catch (Exception ex)
            { return BadRequest(ex.Message); }
        }
        [HttpPost]
        [Route("send-message")]
        public async Task<IActionResult> PostMessage([FromBody] ComentarioDto comentario)
        {
            try
            {
               
                await _consultaServices.sendMessage(comentario);

                return Accepted();

            }
            catch (Exception ex)
            { return BadRequest(ex.Message); }
        }


        [HttpGet]
        [Route("{id}/comments")]
        public async Task<IActionResult> GetCommentsByPersonId(int id)
        {
            try
            {
                var comments = await _consultaServices.GetComentsByPersonId(id);

                return Ok(comments);

            }
            catch (Exception ex)
            { return BadRequest(ex.Message); }
        }

        // PUT: api/Consulta/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        
        [HttpPut]
        [Route("cancel")]
        public async Task<IActionResult> CancelConsulta([FromBody]int id)
        {
            try
            {
                if(id < 1)BadRequest("Id Invalido");

                await _consultaServices.CancelConsulta(id);

                return Ok();

            }
            catch (Exception ex)
            { return BadRequest(ex.Message); }

        }
    }
}
