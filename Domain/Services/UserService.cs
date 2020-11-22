using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Dtos;
using Domain.Dtos;
using Domain.Repositorios;
using Infra.Entities;
using Infra.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Domain.Services
{
    public class UserService : IUserService
    {
        private readonly IPessoaRepository pessoaRepository;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public UserService(IPessoaRepository pessoaRepository,
        UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager
        )
        {
            this.pessoaRepository = pessoaRepository;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }
        public async Task<int> AddPessoa(RegisterUserDto registerUserDto)
        {
            var pessoa = await pessoaRepository.GetPessoaByCpf(registerUserDto.Cpf);

            if(pessoa != null) throw new Exception("Cpf já cadastrado");

            var newpessoa = new PessoaDto
            {
                Nome = registerUserDto.Nome,
                Sobrenome = registerUserDto.Sobrenome,
                CPF = registerUserDto.Cpf,
                DataNascimento = registerUserDto.DataNascimento,
                Telefone = registerUserDto.Celular
            };

            var result = await this.pessoaRepository.CreatePessoa(newpessoa);

            return result;
        }

        public async Task<List<IdentityRole>> ObterPermissoes(string email)
        {
            var user = await this.userManager.FindByEmailAsync(email);

            var userRole = await this.userManager.GetRolesAsync(user);

            var role = this.roleManager.Roles.ToList();

            if (userRole.FirstOrDefault().ToUpper() != "ADMIN")
            {
                role.Remove(role.FirstOrDefault(x=>x.Name.Contains("Admin")));
            }            

            return role;
        }
    }
}