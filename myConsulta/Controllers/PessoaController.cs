﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Dtos;
using Domain.Repositorios;
using Infra.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace myConsulta.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PessoaController : ControllerBase
    {
        private readonly IPessoaService _pessoaServices;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IUserService userService;
        private readonly UserManager<ApplicationUser> _userManager;

        public PessoaController(IPessoaService pessoaServices,
        RoleManager<IdentityRole> roleManager,
         IUserService userService)
        {
            _pessoaServices = pessoaServices;
            this._roleManager = roleManager;
            this.userService = userService;
        }
        // GET: api/Pessoa
        [AllowAnonymous]
        [HttpGet("form-data/{email}")]
        public async Task<IActionResult> GetFormData(string email)
        {            
            var roles = await this.userService.ObterPermissoes(email);

            return Ok(roles);
        }

        // GET: api/Pessoa/5
        [HttpGet("{cpf}")]
        public async Task<IActionResult> GetPessoaByCpf(string cpf)
        {
            try
            {
                if (string.IsNullOrEmpty(cpf)) { BadRequest("Model invalido"); }

                var pessoa = await _pessoaServices.GetPessoaByCpf(cpf);
                return Ok(pessoa);

            }
            catch (Exception ex)
            { return BadRequest(ex.Message); }
        }
        [HttpGet]
        public async Task<IActionResult> GetPessoas()
        {
            try
            {
                
                var pessoas = await _pessoaServices.GetAll();
                return Ok(pessoas);

            }
            catch (Exception ex)
            { return BadRequest(ex.Message); }
        }

        [HttpGet("{id}/edit")]
        public async Task<IActionResult> GetPessoaByIdConsulta(int? id)
        {
            try
            {
                if (id == null) { BadRequest("Model invalido"); }

                var pessoa = await _pessoaServices.GetPessoaByIdConsulta(id.Value);
                return Ok(pessoa);

            }
            catch (Exception ex)
            { return BadRequest(ex.Message); }
        }

        // POST: api/Pessoa
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PessoaDto pessoa)
        {
            try
            {
                if (pessoa == null) { BadRequest("Model invalido"); }

                await _pessoaServices.CreatePessoa(pessoa);

                return Ok(pessoa);

            }
            catch (Exception ex)
            { return BadRequest(ex.Message); }
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
