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

        Task<IEnumerable<PessoaDto>> GetAll();
    }
}
