using Microsoft.EntityFrameworkCore;

namespace Soporte_Técnico_ISP.Models
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options){ }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Caso> Casos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Caso>()
                .HasOne(c => c.Usuario)        // un Caso tiene un Usuario
                .WithMany(u => u.Casos)        // un Usuario tiene muchos Casos
                .HasForeignKey(c => c.UsuarioId)
                .OnDelete(DeleteBehavior.Cascade);
        }

    }
    
}
