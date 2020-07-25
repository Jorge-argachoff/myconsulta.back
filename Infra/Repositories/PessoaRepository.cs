using Application.Dtos;
using Dapper;
using Infra.Dapper;
using Infra.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Repositories
{
    public class PessoaRepository:IPessoaRepository
    {
        public ConsultaDb Db { get; set; }
        public PessoaRepository(ConsultaDb db = null)
        {
            Db = db;
        }
        public Task<IEnumerable<PessoaDto>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<PessoaDto> GetPessoaByCpf(string cpf)
        {
            var query = @"SELECT Id
                              ,Nome
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
                          FROM Tb_Pessoa
                          WHERE CPF = @cpf";

            var result = await Db.SqlConnection.QueryFirstOrDefaultAsync<PessoaDto>(query, new { cpf });

            return result;
        }
        public async Task CreatePessoa(PessoaDto pessoa)
        {
            var args = new{};
            
            var query = @"SELECT Id
                              ,Nome
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
                          FROM Tb_Pessoa
                          WHERE CPF = @cpf";

            await Db.SqlConnection.QueryFirstOrDefaultAsync<PessoaDto>(query, args);

            
        }

        public async Task<PessoaDto> GetPessoaByIdConsulta(int idConsulta)
        {
            var query = @"SELECT P.Id
                              ,Nome
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
                          FROM Tb_Pessoa P
						  join dbo.Tb_Consulta C on C.PessoaId = P.Id
						  Where C.Id = @idConsulta";

            var result = await Db.SqlConnection.QueryFirstOrDefaultAsync<PessoaDto>(query, new { idConsulta });

            return result;
        }
    }
}
