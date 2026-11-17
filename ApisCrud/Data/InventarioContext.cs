
using ApisCrud.Models;
using Microsoft.EntityFrameworkCore;

namespace ApisCrud.Data
{
    public class InventarioContext : DbContext
    {
        public InventarioContext(DbContextOptions<InventarioContext> options)
            : base(options)
        {
        }

        public DbSet<Producto> Productos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Resena> Resenas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Llaves primarias
            modelBuilder.Entity<Producto>()
                .HasKey(p => p.IdProducto);

            modelBuilder.Entity<Usuario>()
                .HasKey(u => u.IdUsuario);

            modelBuilder.Entity<Resena>()
                .HasKey(r => r.IdResena);

            // Relaciones Producto - Reseñas (1:N)
            modelBuilder.Entity<Resena>()
                .HasOne<Producto>()
                .WithMany()
                .HasForeignKey(r => r.IdProducto)
                .OnDelete(DeleteBehavior.Restrict);

            // Relaciones Usuario - Reseñas (1:N)
            modelBuilder.Entity<Resena>()
                .HasOne<Usuario>()
                .WithMany()
                .HasForeignKey(r => r.IdUsuario)
                .OnDelete(DeleteBehavior.Restrict);

            // Precisión del precio
            modelBuilder.Entity<Producto>()
                .Property(p => p.PrecioUnitario)
                .HasPrecision(18, 2);

            // Valores predeterminados para borrado lógico
            modelBuilder.Entity<Producto>()
                .Property(p => p.Estatus)
                .HasDefaultValue(true);

            modelBuilder.Entity<Usuario>()
                .Property(u => u.Estatus)
                .HasDefaultValue(true);

            modelBuilder.Entity<Resena>()
                .Property(r => r.Estatus)
                .HasDefaultValue(true);
        }
    }
}
