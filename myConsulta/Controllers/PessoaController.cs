using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Repositorios;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace myConsulta.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PessoaController : ControllerBase
    {

        private readonly IPessoaRepositorio _pessoaServices;

        public PessoaController(IPessoaRepositorio pessoaServices)
        {
            _pessoaServices = pessoaServices;
        }
        // GET: api/Pessoa
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Pessoa/5
        [HttpGet("{cpf}")]
        public async Task<IActionResult> GetPessoa(string cpf)
        {
            try
            {
                if (string.IsNullOrEmpty(cpf)) { BadRequest("Model invalido"); }

                var pessoa = await _pessoaServices.GetPessoaByCpf(cpf);
                return Ok(pessoa);

            }
            catch (Exception ex)
            { return BadRequest(ex); }
        }

        // POST: api/Pessoa
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Pessoa/5
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
