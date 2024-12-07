using APIproductos.Models;
using Microsoft.EntityFrameworkCore;

namespace APIproductos.Services
{
    public class ServicesProduct
    {
        private readonly AppDbContext _context;

        public ServicesProduct(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Producto>> GetProductosAsync()
        {
            return await _context.Productos.ToListAsync();
        }

        public async Task<Producto> GetProductoPorIDAsync(int id)
        {
            return await _context.Productos.FindAsync(id);
        }

        public async Task<Producto> CrearProductoAsync(Producto producto)
        {
            _context.Productos.Add(producto);
            await _context.SaveChangesAsync();
            return producto;
        }

        public async Task<bool> ProductoExistsAsync(int id)
        {
            return await _context.Productos.AnyAsync(e => e.ProductoID == id);
        }

        public async Task<Producto> UpdateProductoAsync(Producto producto)
        {
            _context.Entry(producto).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return producto;
        }

        public async Task DeleteProductoAsync(Producto producto)
        {
            _context.Productos.Remove(producto);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Producto>> GetProductosPorCategoriaAsync(int categoriaId)
        {
            return await _context.Productos
                .Where(p => p.CategoriaID == categoriaId)
                .ToListAsync();
        }
    }
}
