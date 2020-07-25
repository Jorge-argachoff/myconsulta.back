using Application.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Interfaces
{
    public interface IConfiguracaoRepository
    {
        Task<IEnumerable<HorarioDto>> GetAllHours();
        Task<IEnumerable<EspecialidadeDto>> GetAllNomes();
        Task<IEnumerable<MedicoDto>> GetAllMedicos();
        Task CreateHorario(HorarioDto horario);
        Task CreateNome(EspecialidadeDto nome);
        Task CreateMedico(MedicoDto nome);
        Task deleteHorario(int id);
        Task deleteNome(int id);
        Task InactiveMedico(int id);
    }
}
