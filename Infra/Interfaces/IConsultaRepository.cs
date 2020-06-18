using Application.Dtos;

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Interfaces
{
    public interface IConsultaRepository
    {
        Task<IEnumerable<ConsultaDto>> GetConsultasByCpf(string cpf);

        Task<IEnumerable<ConsultaDto>> GetConsultasByData(string data);

        Task<ConsultaDto> GetConsultasById(int id);

        Task<IEnumerable<ConsultaDto>> GetNextConsultas();

        Task CreateConsulta(ConsultaDto consulta);

        Task CreateComentario(ComentarioDto comentario);

        Task<IEnumerable<ComentarioDto>> GetComentsByPersonId(int id);
    }
}
