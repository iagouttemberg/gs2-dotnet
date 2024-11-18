using Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Database.Mappings;

public class UsuarioMapping : IEntityTypeConfiguration<Usuario>
{
    public void Configure(EntityTypeBuilder<Usuario> builder)
    {
        builder.ToTable("Usuario");

        builder.HasKey(u => u.Id);

        builder.Property(u => u.FirebaseId)
            .IsRequired();

        builder.Property(u => u.Nome)
            .IsRequired();
        
        builder.HasIndex(u => u.Email)
            .IsUnique();

        builder.Property(u => u.DataCadastro)
            .IsRequired();
        
        builder.HasMany(u => u.ConsumosEnergeticos)
            .WithOne(c => c.Usuario)
            .HasForeignKey(c => c.UsuarioId);
    }
}