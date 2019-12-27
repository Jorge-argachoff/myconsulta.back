using Domain.Dtos;
using Domain.Repositorios;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace Domain.Services
{
    public class ConsultaServices : IConsultaRepositorio
    {
        public ConsultaDb Db { get; set; }
        public ConsultaServices(ConsultaDb db = null)
        {
            Db = db;
        }

        public async Task<IEnumerable<ConsultaDto>> GetAllConsultas()
        {           

            var query = @"SELECT Id
                              , PessoaId
                              , Descricao
                              , Detalhes
                              , DataConsulta
                              , DataAgendamento
                          FROM Tb_Consulta";

            var result = await Db.SqlConnection.QueryAsync<ConsultaDto>(query);             

            return result;
        }

        public async Task<IEnumerable<ConsultaDto>> GetConsultasByCpf(string cpf)
        {           
            var query = @"SELECT C.Id
                              ,PessoaId
                              ,Descricao
                              ,Detalhes
                              ,DataConsulta
                              ,DataAgendamento
                          FROM Tb_Consulta C
                          INNER JOIN Tb_Pessoa P ON P.Id = C.PessoaId
                          WHERE P.CPF = @cpf";

            var result = await Db.SqlConnection.QueryAsync<ConsultaDto>(query,new { cpf });

            return result;
        }
    }
}
