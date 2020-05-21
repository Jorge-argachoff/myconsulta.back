using Domain.Dtos;
using Domain.Repositorios;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Linq;
using Infra.Interfaces;
using Application.Dtos;

namespace Domain.Services
{
    public class ConsultaServices : IConsultaService
    {
        private readonly IConsultaRepository _consultaRepository;

        public ConsultaServices(IConsultaRepository consultaRepository)
        {
            _consultaRepository = consultaRepository;
        }

        public async Task CreateConsulta(ConsultaDto consulta)
        {
           await _consultaRepository.CreateConsulta(consulta);
        }

        public async Task<IEnumerable<ConsultaDto>> GetConsultasByCpf(string cpf)
        {
            return await _consultaRepository.GetConsultasByCpf(cpf);
        }

        public async Task<IEnumerable<ConsultaDto>> GetConsultasByData(string data)
        {
            return await _consultaRepository.GetConsultasByData(data);
        }

        public async Task<ConsultaDto> GetConsultasById(int id)
        {
            return await _consultaRepository.GetConsultasById(id);
        }

        public async Task<IEnumerable<ConsultaDto>> GetNextConsultas()
        {
            return await _consultaRepository.GetNextConsultas();
        }
    }
}
