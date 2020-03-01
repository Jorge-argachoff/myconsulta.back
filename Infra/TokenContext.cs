using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infra
{
    public class TokenContext:IdentityDbContext
    {

        public TokenContext(DbContextOptions<TokenContext> options):base(options)
        {

        }


    }
}