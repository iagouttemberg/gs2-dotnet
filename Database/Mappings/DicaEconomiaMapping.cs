using Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Database.Mappings;

public class DicaEconomiaMapping : IEntityTypeConfiguration<DicaEconomia>
{
    public void Configure(EntityTypeBuilder<DicaEconomia> builder)
    {
        builder.ToTable("DicaEconomia");

        builder.HasKey(d => d.Id);

        builder.Property(d => d.Titulo)
            .IsRequired();

        builder.Property(d => d.Descricao)
            .IsRequired();
        
        builder.Property(d => d.DataCriacao)
            .IsRequired();
    }
}