using Application.Dtos;
using Infra.Dapper;
using Infra.Interfaces;
using Dapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Application.Enums;

namespace Infra.Repositories
{
    public class ConsultaRepository : IConsultaRepository
    {
        public ConsultaDb Db { get; set; }
        public ConsultaRepository(ConsultaDb db = null)
        {
            Db = db;
        }

        public async Task<IEnumerable<ConsultaDto>> GetNextConsultas()
        {

            var parameters = new { DataDeHoje = DateTime.Now };

            var query = @"SELECT C.Id
                    	  ,P.Nome
                          ,C.PessoaId                          
                          ,Detalhes
                          ,DataConsulta
                          ,C.DataCriacao
                          ,Hora
                          ,MedicoId
                          ,M.Nome as Medico
                          ,C.Status
                      FROM Tb_Consulta C
                      INNER JOIN Tb_Pessoa P ON P.Id = C.PessoaId
                      INNER JOIN Tb_Medico M ON M.Id = C.MedicoId
                      WHERE DataConsulta >= @DataDeHoje
                      AND C.Status = 'Ativa'                                                                                                                                 
                      Order by DataConsulta,Hora";

            var result = await Db.SqlConnection.QueryAsync<ConsultaDto>(query, parameters);

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

            var result = await Db.SqlConnection.QueryAsync<ConsultaDto>(query, new { cpf });

            return result;
        }

        public async Task CreateConsulta(ConsultaDto consulta)
        {
            var parameters = new
            {
                PessoaId = consulta.PessoaId,
                Detalhes = consulta.Detalhes,
                DataConsulta = consulta.DataConsulta,
                DataCriacao = DateTime.Now,
                Hora = consulta.Hora,
                MedicoId = consulta.MedicoId,
                Status = StatusConsultaEnum.Ativa.ToString()   
            };
            var query = @"INSERT INTO Tb_Consulta
                               ([PessoaId]                               
                               ,[Detalhes]
                               ,[DataConsulta]
                               ,[DataCriacao]
                               ,[Hora]                               
                               ,[MedicoId]
                               ,[Status])
                         VALUES
                               (@PessoaId,@Detalhes,@DataConsulta,@DataCriacao,@Hora,@MedicoId,@Status)";

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

                        
                        Select 
						  PE.Id,Nome
						 ,Sobrenome
						 ,Endereco
						 ,Numero
						 ,Bairro
						 ,Complemento
						 ,CEP
						 ,CPF
						 ,RG
						 ,Telefone
						 ,DataNascimento
		            From Tb_Pessoa PE
						  inner join Tb_Consulta CO ON CO.PessoaId = PE.id
						  where CO.Id = @Id";



            using (var multi = await Db.SqlConnection.QueryMultipleAsync(query, new { id }))
            {
                var Consulta = await multi.ReadFirstOrDefaultAsync<ConsultaDto>();
                Consulta.Pessoa = await multi.ReadFirstOrDefaultAsync<PessoaDto>();
                Consulta.Comentarios = await GetComentsByPersonId(Consulta.PessoaId);

                return Consulta;
            }
        }

        public async Task<IEnumerable<ComentarioDto>> GetComentsByPersonId(int id)
        {
            var query = @" SELECT Id
                                  ,ConsultaId
                                  ,Comentario
                                  ,Data
		            	          ,PessoaId
                                  ,EscritoPor
                             FROM Tb_Comentario						  
		              WHERE PessoaId = @id
		              ORDER BY Data DESC";

            var result = await Db.SqlConnection.QueryAsync<ComentarioDto>(query, new { id });

            return result;
        }

        public async Task CreateComentario(ComentarioDto comentario)
        {
            var parameters = new
            {
                ConsultaId = comentario.ConsultaId,
                Comentario = comentario.Comentario,
                Data = DateTime.Now,
                PessoaId = comentario.PessoaId,
                EscritoPor = comentario.EscritoPor

            };
            var query = @"INSERT INTO Tb_Comentario
                                        ([ConsultaId]
                                        ,[Comentario]
                                        ,[Data]
                                        ,[PessoaId]
                                        ,[EscritoPor])
                                    VALUES
                                        (@ConsultaId
                                        ,@Comentario
                                        ,@Data
                                        ,@PessoaId
                                        ,@EscritoPor)";

            await Db.SqlConnection.ExecuteAsync(query, parameters);
        }

        public async Task ChangeStatusConsulta(int idConsulta, StatusConsultaEnum status)
        {
            var parameters = new
            {
                Id = idConsulta,
                Status = status.ToString()
            };
            var query = @"UPDATE Tb_Consulta
                            SET Status = @Status
                            WHERE Id = @Id";

            await Db.SqlConnection.ExecuteAsync(query, parameters);

        }
    }
}
