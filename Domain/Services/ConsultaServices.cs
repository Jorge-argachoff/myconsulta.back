using Domain.Dtos;
using Domain.Repositorios;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Linq;

namespace Domain.Services
{
    public class ConsultaServices : IConsultaRepositorio
    {
        public ConsultaDb Db { get; set; }
        public ConsultaServices(ConsultaDb db = null)
        {
            Db = db;
        }

        public async Task<IEnumerable<ConsultaDto>> GetNextConsultas()
        {

            var parameters = new { DataDeHoje = DateTime.Now};

            var query = @"SELECT C.Id
                    	  ,P.Nome
                          ,PessoaId                          
                          ,Detalhes
                          ,DataConsulta
                          ,DataCriacao
                          ,Hora
                          ,MedicoId
                          ,M.Nome as Medico
                      FROM Tb_Consulta C
                      INNER JOIN Tb_Pessoa P ON P.Id = C.PessoaId
                      INNER JOIN Tb_Medico M ON M.Id = C.MedicoId
                      WHERE DataConsulta >= @DataDeHoje                                                                                                                                     
                      Order by DataConsulta,Hora";

            var result = await Db.SqlConnection.QueryAsync<ConsultaDto>(query,parameters);             

            return result;
        }

        public async Task<IEnumerable<ConsultaDto>> GetConsultasByCpf(string cpf)
        {           
            var query = @"SELECT C.Id
                              ,PessoaId                              
                              ,Detalhes
                              ,DataConsulta
                              ,DataCriacao
                              ,Hora
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
               Detalhes = consulta.Detalhes,
               DataConsulta = consulta.DataConsulta,
               DataCriacao = DateTime.Now ,
               Hora = consulta.Hora,
               MedicoId = consulta.MedicoId
            };
             var query = @"INSERT INTO Tb_Consulta
                               ([PessoaId]                               
                               ,[Detalhes]
                               ,[DataConsulta]
                               ,[DataCriacao]
                               ,[Hora]                               
                               ,[MedicoId])
                         VALUES
                               (@PessoaId,@Detalhes,@DataConsulta,@DataCriacao,@Hora,@MedicoId)";

            await Db.SqlConnection.ExecuteAsync(query, parameters);
          
        }

        public async Task<IEnumerable<ConsultaDto>> GetConsultasByData(string data)
        {
            var query = @"SELECT Id
                               ,PessoaId                              
                               ,Detalhes
                               ,DataConsulta
                               ,DataCriacao
                               ,Hora                              
                               ,MedicoId
                          FROM Tb_Consulta                           
                          WHERE DataConsulta = @data";

            var result = await Db.SqlConnection.QueryAsync<ConsultaDto>(query, new { data });

            return result;
        }

        public async Task<ConsultaDto> GetConsultasById(int id)
        {
            var query = @"SELECT C.Id
                    	  ,P.Nome
                          ,P.Sobrenome
                          ,PessoaId
                          ,Detalhes
                          ,DataConsulta
                          ,DataCriacao
                          ,Hora
                          ,MedicoId
                          ,M.Nome as Medico
                      FROM Tb_Consulta C
                      INNER JOIN Tb_Pessoa P ON P.Id = C.PessoaId
                      INNER JOIN Tb_Medico M ON M.Id = C.MedicoId                                             
                          WHERE C.Id = @id;

                        SELECT Id
                              ,ConsultaId
                              ,Comentario
                              ,Data
                          FROM Tb_Comentario
                          Where ConsultaId = @Id;";



            using (var multi = await Db.SqlConnection.QueryMultipleAsync(query, new { id }))
            {
                var Consulta = multi.Read<ConsultaDto>().FirstOrDefault();
                Consulta.Comentarios = multi.Read<ComentarioDto>().ToList();


                return Consulta;
            }
        }
    }
}
