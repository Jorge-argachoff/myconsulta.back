using Application.Dtos;
using Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;


namespace Domain.Repositorios
{
    public interface IConsultaService
    {
        Task<IEnumerable<ConsultaDto>> GetConsultasByCpf(string cpf);

        Task<IEnumerable<ConsultaDto>> GetConsultasByData(string data);

        Task<ConsultaDto> GetConsultasById(int id);

        Task<IEnumerable<ConsultaDto>> GetNextConsultas();

        Task CreateConsulta(ConsultaDto consulta);

        Task<IEnumerable<ComentarioDto>> GetComentsByPersonId(int id);
    }
}
