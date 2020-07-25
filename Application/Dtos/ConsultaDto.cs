using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Dtos
{
    public class ConsultaDto
    {       
        public int Id { get; set; }

        public int PessoaId { get; set; }

        public DateTime DataConsulta { get; set; }

        public DateTime DataCriacao { get; set; }        

        public string Detalhes { get; set; }

        public string Nome { get; set; }

        public string SobreNome { get; set; }
        public string Hora { get; set; }

        public int MedicoId { get; set; }

        public string Medico { get; set; }
        public IEnumerable<ComentarioDto> Comentarios { get; set; }

        public PessoaDto Pessoa { get; set; }

        public string Status { get; set; }

    }
}
