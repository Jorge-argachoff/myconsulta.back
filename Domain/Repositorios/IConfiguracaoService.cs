using Application.Dtos;
using Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositorios
{
    public interface IConfiguracaoService
    {
        Task<IEnumerable<HorarioDto>> GetAllHours();
        Task<IEnumerable<EspecialidadeDto>> GetAllEspecialidades();
        Task<IEnumerable<MedicoDto>> GetAllMedicos();
        Task CreateHorario(HorarioDto horario);
        Task CreateNome(EspecialidadeDto nome);
        Task CreateMedico(MedicoDto nome);
        Task deleteHorario(int id);
        Task deleteNome(int id);
        Task InactiveMedico(int id);


    }
}
