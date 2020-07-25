using Microsoft.AspNetCore.Identity;

namespace Infra.Entities
{
    public class AplicationUser:IdentityUser
    {
        public int PessoaId { get; set; }
        
    }
}