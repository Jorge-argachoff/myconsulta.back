using Infra.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Mappers
{
    public class PessoaMapper: IEntityTypeConfiguration<Pessoa>
    {

        public void Configure(EntityTypeBuilder<Pessoa> builder)
        {

            builder.ToTable("Tb_Pessoa");

            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            // base.Configure(builder);
        }


    }
}