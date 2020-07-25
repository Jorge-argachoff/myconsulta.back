using Infra.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Mappers
{
    public class EspecialidadeMapper: IEntityTypeConfiguration<Especialidade>
    {

        public void Configure(EntityTypeBuilder<Especialidade> builder)
        {

            builder.ToTable("Tb_Especialidade");

            builder.Property(x => x.Id).ValueGeneratedOnAdd();

        }


    }
}