using Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositorios
{
    public interface IConsultaRepositorio
    {
        Task<IEnumerable<ConsultaDto>> GetConsultasByCpf(string cpf);

        Task<IEnumerable<ConsultaDto>> GetAllConsultas();
    }
}
