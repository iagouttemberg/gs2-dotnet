using Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Database.Mappings;

public class ConsumoEnergeticoMapping : IEntityTypeConfiguration<ConsumoEnergetico>
{
    public void Configure(EntityTypeBuilder<ConsumoEnergetico> builder)
    {
        builder.ToTable("ConsumoEnergetico");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Mes)
            .IsRequired();

        builder.Property(c => c.Ano)
            .IsRequired();
        
        builder.Property(c => c.ConsumoKWh)
            .IsRequired();

        builder.HasOne(c => c.Usuario)
            .WithMany(u => u.ConsumosEnergeticos)
            .HasForeignKey(c => c.UsuarioId);
    }
}