using Microsoft.AspNetCore.Identity;

namespace Infra.Entities
{
    public class ApplicationUser:IdentityUser
    {
        public int PessoaId { get; set; }
        
    }
}