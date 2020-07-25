using Application.Dtos;
using Dapper;
using Infra.Dapper;
using Infra.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Repositories
{
    public class ConfiguracaoRepository:IConfiguracaoRepository
    {
        public ConsultaDb Db { get; set; }
        public ConfiguracaoRepository(ConsultaDb db = null)
        {
            Db = db;
        }

        #region[[SELECT]]
        public async Task<IEnumerable<HorarioDto>> GetAllHours()
        {
            var query = @"SELECT Id,
                    	  Hora
                      FROM Tb_Horario                                            
                      Order by Hora";

            var result = await Db.SqlConnection.QueryAsync<HorarioDto>(query);

            return result.GroupBy(x => x.Hora).Select(y => y.First());
        }
        public async Task<IEnumerable<EspecialidadeDto>> GetAllNomes()
        {
            var query = @"SELECT Id,
                    	  Nome
                      FROM Tb_Especialidade                                            
                      Order by Nome";

            var result = await Db.SqlConnection.QueryAsync<EspecialidadeDto>(query);

            return result;
        }

        public async Task<IEnumerable<MedicoDto>> GetAllMedicos()
        {
            var query = @"SELECT Id,
                    	  Nome,
                          Ativo
                      FROM Tb_Medico
                       WHERE Ativo = 1
                      Order by Nome";

            var result = await Db.SqlConnection.QueryAsync<MedicoDto>(query);

            return result;
        }

        #endregion

        #region[[INSERT]]

        public async Task CreateHorario(HorarioDto horario)
        {

            var query = @"INSERT INTO Tb_Horario(Hora,DataCriacao)
                            VALUES(@Hora,@DataCriacao)";

            await Db.SqlConnection.ExecuteAsync(query, new { Hora = horario.Hora, DataCriacao = DateTime.Now });

        }

        public async Task CreateNome(EspecialidadeDto nome)
        {
            var query = @"INSERT INTO Tb_Especialidade(Nome)
                            VALUES(@Nome)";

            await Db.SqlConnection.ExecuteAsync(query, new { nome.Nome });
        }
        public async Task CreateMedico(MedicoDto nome)
        {
            var query = @"INSERT INTO Tb_Medico(Nome,Ativo)
                            VALUES(@Nome,1)";

            await Db.SqlConnection.ExecuteAsync(query, new { nome.Nome });
        }
        #endregion

        #region[[DELETE]]

        public async Task deleteNome(int id)
        {
            var query = @"DELETE FROM Tb_Especialidade
                            WHERE Id = @id";

            await Db.SqlConnection.ExecuteAsync(query, new { id });
        }
        public async Task deleteHorario(int id)
        {
            var query = @"DELETE FROM Tb_Horario
                            WHERE Id = @id";

            await Db.SqlConnection.ExecuteAsync(query, new { id });
        }
        public async Task InactiveMedico(int id)
        {
            var parameters = new
            {
                Id = id,
                InativadoEm = DateTime.Now
            };

            var query = @"UPDATE Tb_Medico
                            SET Ativo = 0,InativadoEm = @InativadoEm
                            WHERE Id = @Id";

            await Db.SqlConnection.ExecuteAsync(query, parameters);
        }

        #endregion
    }
}
