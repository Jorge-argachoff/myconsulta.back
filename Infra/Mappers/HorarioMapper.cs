using Infra.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Mappers
{
    public class HorarioMapper: IEntityTypeConfiguration<Horario>
    {

        public void Configure(EntityTypeBuilder<Horario> builder)
        {

            builder.ToTable("Tb_Horario");

            builder.Property(x => x.Id).ValueGeneratedOnAdd();

        }


    }
}