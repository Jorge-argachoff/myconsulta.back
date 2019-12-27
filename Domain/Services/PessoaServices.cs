using Dapper;
using Domain.Dtos;
using Domain.Repositorios;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    public class PessoaServices : IPessoaRepositorio
    {

        public ConsultaDb Db { get; set; }
        public PessoaServices(ConsultaDb db = null)
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
