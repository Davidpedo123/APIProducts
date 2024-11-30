using Microsoft.EntityFrameworkCore;



namespace APIproductos.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Venta> Ventas { get; set; }
        public DbSet<DetalleVenta> DetalleVentas { get; set; }
        public DbSet<Devolucion> Devoluciones { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Definir claves primarias
            modelBuilder.Entity<Categoria>().HasKey(c => c.CategoriaID);
            modelBuilder.Entity<Producto>().HasKey(p => p.ProductoID);
            modelBuilder.Entity<Venta>().HasKey(v => v.VentaID);
            modelBuilder.Entity<DetalleVenta>().HasKey(dv => dv.DetalleID);
            modelBuilder.Entity<Devolucion>().HasKey(d => d.DevolucionID);

            // Relaciones
            modelBuilder.Entity<Producto>()
                .HasOne(p => p.Categoria)
                .WithMany(c => c.Productos)
                .HasForeignKey(p => p.CategoriaID);

            modelBuilder.Entity<DetalleVenta>()
                .HasOne(dv => dv.Venta)
                .WithMany(v => v.DetalleVentas)
                .HasForeignKey(dv => dv.VentaID);

            modelBuilder.Entity<DetalleVenta>()
                .HasOne(dv => dv.Producto)
                .WithMany()
                .HasForeignKey(dv => dv.ProductoID);

            modelBuilder.Entity<Devolucion>()
                .HasOne(d => d.Venta)
                .WithMany(v => v.Devoluciones)
                .HasForeignKey(d => d.VentaID);

            modelBuilder.Entity<Devolucion>()
                .HasOne(d => d.Producto)
                .WithMany()
                .HasForeignKey(d => d.ProductoID);
        }
    }
}