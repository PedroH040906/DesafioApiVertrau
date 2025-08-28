// desafio.Data/ApplicationDbContext.cs
using Microsoft.EntityFrameworkCore;
using desafio.Entities;

namespace desafio.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Usuario> Usuarios { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseNpgsql(
                "Host=localhost;Port=5432;Database=usuario;Username=postgres;Password=postgres");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
    modelBuilder.Entity<Usuario>()
        .Property(u => u.Genero)
        .HasConversion<string>();  // salva como texto (Masculino, Feminino, Outro)
            base.OnModelCreating(modelBuilder);
        }
    }
}
