using Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositorios
{
    public interface IPessoaRepositorio
    {
        Task<PessoaDto> GetPessoaByCpf(string cpf);

        Task<IEnumerable<PessoaDto>> GetAll();
    }
}
