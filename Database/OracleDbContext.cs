using Database.Mappings;
using Database.Models;
using Microsoft.EntityFrameworkCore;
namespace Database
{
    public class OracleDbContext : DbContext
    {
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<ConsumoEnergetico> ConsumosEnergeticos { get; set; }
        public DbSet<DicaEconomia> DicasEconomia { get; set; }

        public OracleDbContext(DbContextOptions<OracleDbContext> options) : base(options)
        {

        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UsuarioMapping());
            modelBuilder.ApplyConfiguration(new ConsumoEnergeticoMapping());
            modelBuilder.ApplyConfiguration(new DicaEconomiaMapping());

            base.OnModelCreating(modelBuilder);
        }
    }
}
