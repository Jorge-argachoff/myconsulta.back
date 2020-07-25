using Application.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Interfaces
{
    public interface IPessoaRepository
    {
        Task<PessoaDto> GetPessoaByCpf(string cpf);

        Task CreatePessoa(PessoaDto pessoa);
        Task<IEnumerable<PessoaDto>> GetAll();
        Task<PessoaDto> GetPessoaByIdConsulta(int idConsulta);
    }
}
