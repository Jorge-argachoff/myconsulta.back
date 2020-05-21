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
    }
}
