using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Dtos
{
    public class ComentarioDto
    {
        public int Id { get; set; }

        public int ConsultaId { get; set; }

        public string Comentario { get; set; }

        public DateTime Data { get; set; }
    }
}
