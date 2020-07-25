using Infra.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Mappers
{
    public class MedicoMapper: IEntityTypeConfiguration<Medico>
    {

        public void Configure(EntityTypeBuilder<Medico> builder)
        {

            builder.ToTable("Tb_Medico");

            builder.Property(x => x.Id).ValueGeneratedOnAdd();

        }


    }
}