using Dapper;
using Domain.Dtos;
using Domain.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    public class ConfiguracaoServices:IConfiguracaoRepositorio
    {
        public ConsultaDb Db { get; set; }
        public ConfiguracaoServices(ConsultaDb db = null)
        {
            Db = db;
        }

        #region[[SELECT]]
        public async Task<IEnumerable<HorarioDto>> GetAllHours()
        {
            var query = @"SELECT Id,
                    	  Horario
                      FROM Tb_Horario                                            
                      Order by Horario";

            var result = await Db.SqlConnection.QueryAsync<HorarioDto>(query);            

            return result.GroupBy(x => x.Horario).Select(y => y.First()); 
        }
        public async Task<IEnumerable<NomeConsultaDto>> GetAllNomes()
        {
            var query = @"SELECT Id,
                    	  Nome
                      FROM Tb_NomeConsulta                                            
                      Order by Nome";

            var result = await Db.SqlConnection.QueryAsync<NomeConsultaDto>(query);

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
            
            var query = @"INSERT INTO Tb_Horario(Horario)
                            VALUES(@horario)";

            await Db.SqlConnection.ExecuteAsync(query,new { horario = horario.Horario });
            
        }         

        public async Task CreateNome(NomeConsultaDto nome)
        {
            var query = @"INSERT INTO Tb_NomeConsulta(Nome)
                            VALUES(@Nome)";

            await Db.SqlConnection.ExecuteAsync(query, new {  nome.Nome });
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
            var query = @"DELETE FROM Tb_NomeConsulta
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
