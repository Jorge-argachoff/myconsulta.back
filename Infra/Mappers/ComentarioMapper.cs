using Infra.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Mappers
{
    public class ComentarioMapper: IEntityTypeConfiguration<Comentario>
    {

        public void Configure(EntityTypeBuilder<Comentario> builder)
        {

            builder.ToTable("Tb_Comentario");

            builder.Property(x => x.Id).ValueGeneratedOnAdd();

        }


    }
}