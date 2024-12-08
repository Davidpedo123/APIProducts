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

        public DbSet<VentasPorCategoria> VentasPorCategoria { get; set; }
        public DbSet<VentasPorProducto> VentasPorProducto { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<Categoria>().HasKey(c => c.CategoriaID);
            modelBuilder.Entity<Producto>().HasKey(p => p.ProductoID);
            modelBuilder.Entity<Venta>().HasKey(v => v.VentaID);
            modelBuilder.Entity<DetalleVenta>().HasKey(dv => dv.DetalleID);
            modelBuilder.Entity<Devolucion>().HasKey(d => d.DevolucionID);

           

            modelBuilder.Entity<DetalleVenta>()
                .HasOne(dv => dv.Venta)
                .WithMany(v => v.DetalleVentas)
                .HasForeignKey(dv => dv.VentaID);

            modelBuilder.Entity<DetalleVenta>()
                .HasOne(dv => dv.Producto)
                .WithMany()
                .HasForeignKey(dv => dv.ProductoID);

            

            modelBuilder.Entity<VentasPorProducto>()
            .ToView("VentasPorProducto") 
            .HasNoKey(); 

            modelBuilder.Entity<VentasPorCategoria>()
                .ToView("VentasPorCategoria") 
                .HasNoKey(); 

        }
    }
}