using System;
using Infra.Entities.Base;

namespace Infra.Entities
{
    public class Consulta:EntityBase
    {
        public DateTime DataConsulta { get; set; }    
        public string Detalhes { get; set; }
        public string Nome { get; set; }
        public string SobreNome { get; set; }
        public string Hora { get; set; }
        public int PessoaId { get; set; }
        public int MedicoId { get; set; }       

        public string Status { get; set; }
    }
}