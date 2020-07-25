namespace Infra.Entities
{
    public class Medico
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        public bool Ativo { get; set; }

        public int PessoaId { get; set; }

        public int EspecialidadeId { get; set; }
    }
}