using Application.Dtos;
using Dapper;
using Domain.Dtos;
using Domain.Repositorios;
using Infra.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    public class ConfiguracaoServices:IConfiguracaoService
    {
        private readonly IConfiguracaoRepository _configuracaoRepository;
              
        public ConfiguracaoServices(IConfiguracaoRepository configuracaoRepository)
        {
           _configuracaoRepository = configuracaoRepository;
        }

        #region[[SELECT]]
        public async Task<IEnumerable<HorarioDto>> GetAllHours()
        {
            
            return await _configuracaoRepository.GetAllHours();
        }
        public async Task<IEnumerable<EspecialidadeDto>> GetAllEspecialidades()
        {
            return await _configuracaoRepository.GetAllNomes();
        }

        public async Task<IEnumerable<MedicoDto>> GetAllMedicos()
        {
            return await _configuracaoRepository.GetAllMedicos();
        }

        #endregion

        #region[[INSERT]]

        public async Task CreateHorario(HorarioDto horario)
        {
            await _configuracaoRepository.CreateHorario(horario);
        }         

        public async Task CreateNome(EspecialidadeDto nome)
        {
            await _configuracaoRepository.CreateNome(nome);
        }
        public async Task CreateMedico(MedicoDto nome)
        {
           await _configuracaoRepository.CreateMedico(nome);
        }
        #endregion

        #region[[DELETE]]

        public async Task deleteNome(int id)
        {
            await _configuracaoRepository.deleteNome(id);
        }
        public async Task deleteHorario(int id)
        {
            await _configuracaoRepository.deleteHorario(id);
        }
        public async Task InactiveMedico(int id)
        {
            await _configuracaoRepository.InactiveMedico(id);
        }

        #endregion

    }
}
