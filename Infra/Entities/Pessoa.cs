using System;
using Infra.Entities.Base;

namespace Infra.Entities
{
    public class Pessoa:EntityBase
    {
        public string Nome { get; set; }

        public string Sobrenome { get; set; }

        public string Endereco { get; set; }

        public int Numero { get; set; }

        public string Bairro { get; set; }

        public string CPF { get; set; }

        public string CEP { get; set; }

        public string RG { get; set; }

        public string Telefone { get; set; }

        public DateTime DataNascimento { get; set; }

        public string Complemento { get; set; }
        
    }
}