using Infra.Mappers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Infra.Context
{
    public class ApplicationContext: DbContext
    {

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder model)
        {


            model.ApplyConfiguration(new PessoaMapper());
            model.ApplyConfiguration(new ConsultaMapper());
            model.ApplyConfiguration(new ComentarioMapper());
            model.ApplyConfiguration(new MedicoMapper());
            model.ApplyConfiguration(new HorarioMapper());
            model.ApplyConfiguration(new EspecialidadeMapper());
            


        }


    }

    public class AppContextFactory : IDesignTimeDbContextFactory<ApplicationContext>
    {
        public ApplicationContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();
            optionsBuilder.UseSqlServer(@"Data Source=.\SQLEXPRESS; Initial Catalog=myConsulta; Trusted_Connection=True; MultipleActiveResultSets=true");

            return new ApplicationContext(optionsBuilder.Options);
        }
    }
}