using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Dtos
{
    public class MedicoDto
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        public bool Ativo { get; set; }
    }
}
