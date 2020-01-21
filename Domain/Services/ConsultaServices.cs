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

            var parameters = new { DataDeHoje = DateTime.Now};

            var query = @"SELECT C.Id
                    	  ,P.Nome
                          ,PessoaId
                          ,Descricao
                          ,Detalhes
                          ,DataConsulta
                          ,DataCriacao
                      FROM Tb_Consulta C
                      INNER JOIN Tb_Pessoa P ON P.Id = C.PessoaId
                      WHERE DataConsulta > @DataDeHoje 
                      Order by DataConsulta";

            var result = await Db.SqlConnection.QueryAsync<ConsultaDto>(query,parameters);             

            return result;
        }

        public async Task<IEnumerable<ConsultaDto>> GetConsultasByCpf(string cpf)
        {           
            var query = @"SELECT C.Id
                              ,PessoaId
                              ,Descricao
                              ,Detalhes
                              ,DataConsulta
                              ,DataCriacao
                          FROM Tb_Consulta C
                          INNER JOIN Tb_Pessoa P ON P.Id = C.PessoaId
                          WHERE P.CPF = @cpf";

            var result = await Db.SqlConnection.QueryAsync<ConsultaDto>(query,new { cpf });

            return result;
        }

        public async Task CreateConsulta(ConsultaDto consulta)
        {
            var parameters = new {
               PessoaId = consulta.PessoaId,
               Descricao = consulta.Descricao,
               Detalhes = consulta.Detalhes,
               DataConsulta = consulta.DataConsulta,
               DataCriacao = DateTime.Now            
            };
             var query = @"INSERT INTO Tb_Consulta
                               ([PessoaId]
                               ,[Descricao]
                               ,[Detalhes]
                               ,[DataConsulta]
                               ,[DataCriacao])
                         VALUES
                               (@PessoaId,@Descricao,@Detalhes,@DataConsulta,@DataCriacao)";

            await Db.SqlConnection.ExecuteAsync(query, parameters);
          
        }
    }
}
