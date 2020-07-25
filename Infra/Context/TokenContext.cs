using Infra.Entities;
using Infra.Mappers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Infra.Context
{
    public class TokenContext : IdentityDbContext
    {

        public TokenContext(DbContextOptions<TokenContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


            base.OnModelCreating(modelBuilder);

        }


    }

    public class AppFactory : IDesignTimeDbContextFactory<TokenContext>
    {
        public TokenContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<TokenContext>();
            optionsBuilder.UseMySql(@"host=localhost; Port=3306; Database=AspNetIdentity;username=root;password=123456;");

            return new TokenContext(optionsBuilder.Options);
        }
    }
}