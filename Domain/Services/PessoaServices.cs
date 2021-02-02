using Application.Dtos;
using Dapper;
using Domain.Dtos;
using Domain.Repositorios;
using Infra.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    public class PessoaServices : IPessoaService
    {
        private readonly IPessoaRepository _pessoaRepository;

        public PessoaServices(IPessoaRepository pessoaRepository)
        {
            _pessoaRepository = pessoaRepository;
        }

        public async Task CreatePessoa(PessoaDto pessoa)
        {
            await _pessoaRepository.CreatePessoa(pessoa);
        }

        public async Task<IEnumerable<PessoaDto>> GetAll()
        {
            return await _pessoaRepository.GetAll();
        }

        public async Task<PessoaDto> GetPessoaByCpf(string cpf)
        {
            return await _pessoaRepository.GetPessoaByCpf(cpf);
        }
        public async Task<PessoaDto> GetPessoaByIdConsulta(int idConsulta)
        {
            return await _pessoaRepository.GetPessoaByIdConsulta(idConsulta);
        }
    }
}
