using APIproductos.Models;
using Microsoft.EntityFrameworkCore;

namespace APIproductos.Services
{
    public class VentasService
    {
        private readonly AppDbContext _context;

        public VentasService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Venta>> ObtenerVentasAsync()
        {
            return await _context.Ventas.ToListAsync();
        }

        public async Task<IEnumerable<VentasPorCategoria>> ObtenerVentasPorCategoriaAsync()
        {
            return await _context.VentasPorCategoria.ToListAsync();
        }

        public async Task<IEnumerable<VentasPorProducto>> ObtenerVentasPorProductoAsync()
        {
            return await _context.VentasPorProducto.ToListAsync();
        }
    }
}
