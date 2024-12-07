using APIproductos.Models;
using Microsoft.EntityFrameworkCore;

namespace APIproductos.Services
{
    public class DevolucionesService
    {
        private readonly AppDbContext _context;

        public DevolucionesService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<object>> ObtenerDevolucionesPorVentaAsync(int ventaId)
        {
            var devoluciones = await _context.Devoluciones
                .Where(d => d.VentaID == ventaId)
                .Select(d => new
                {
                    d.DevolucionID,
                    d.VentaID,
                    d.ProductoID,
                    d.Cantidad,
                    d.Motivo,
                    d.FechaDevolucion
                })
                .ToListAsync();

            return devoluciones;
        }

        public async Task<Devolucion> CrearDevolucionAsync(Devolucion devolucion)
        {
            _context.Devoluciones.Add(devolucion);
            await _context.SaveChangesAsync();
            return devolucion;
        }

        public async Task<bool> ActualizarDevolucionAsync(int id, Devolucion devolucion)
        {
            if (id != devolucion.DevolucionID)
                return false;

            _context.Entry(devolucion).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
