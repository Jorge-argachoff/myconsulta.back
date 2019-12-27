using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Dtos
{
    public class ConsultaDto
    {       
        public int Id { get; set; }

        public int PessoaId { get; set; }

        public DateTime DataConsulta { get; set; }

        public DateTime DataAgendamento { get; set; }

        public string Descricao { get; set; }

        public string Detalhes { get; set; }
    }
}
