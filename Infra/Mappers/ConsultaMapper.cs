using Infra.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Mappers
{
    public class ConsultaMapper : IEntityTypeConfiguration<Consulta>
    {

        public void Configure(EntityTypeBuilder<Consulta> builder)
        {

            builder.ToTable("Tb_Consulta");

            builder.Property(x => x.Id).ValueGeneratedOnAdd();

        }


    }
}