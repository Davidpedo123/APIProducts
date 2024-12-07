using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIproductos.Models;

namespace APIproductos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProductosController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/productos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Producto>>> GetProductos()
        {
            var productos = await _context.Productos.ToListAsync();
            return Ok(productos);
        }

        // GET: api/productos/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Producto>> GetProductoPorID(int id)
        {
            var producto = await _context.Productos.FindAsync(id);

            if (producto == null)
            {
                return NotFound($"No se encontró un producto con ID {id}.");
            }

            return Ok(producto);
        }

        // POST: api/productos
        [HttpPost]
        public async Task<IActionResult> CrearProducto([FromBody] Producto producto)
        {
            var categoria = await _context.Categorias.FindAsync(producto.CategoriaID);
            if (categoria == null)
            {
                return BadRequest("La categoría no existe.");
            }

            _context.Productos.Add(producto);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProductoPorID), new { id = producto.ProductoID }, producto);
        }

        // PUT: api/productos/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProducto(int id, [FromBody] Producto producto)
        {
            var productoExistente = await _context.Productos.FindAsync(id);
            if (productoExistente == null)
            {
                return NotFound();
            }

            productoExistente.Nombre = producto.Nombre;
            productoExistente.Descripcion = producto.Descripcion;
            productoExistente.Precio = producto.Precio;
            productoExistente.Stock = producto.Stock;
            productoExistente.CategoriaID = producto.CategoriaID;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/productos/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProducto(int id)
        {
            var producto = await _context.Productos.FindAsync(id);
            
            if (producto == null)
            {
                return NotFound();
            }

            _context.Productos.Remove(producto);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        [HttpGet("ventas")]
        public async Task<ActionResult<IEnumerable<Venta>>> GetVentas()
        {
            var ventas = await _context.Ventas
                .Select(v => new Venta
                {
                    VentaID = v.VentaID,
                    FechaVenta = v.FechaVenta,
                    Total = v.Total,
                    Cliente = v.Cliente
                })
                .ToListAsync();

            return Ok(ventas);
        }
    
        // GET: api/productos/reportes
        [HttpGet("ventas-por-categoria")]
        public async Task<ActionResult<IEnumerable<VentasPorCategoria>>> GetVentasPorCategoria()
        {
            var ventasPorCategoria = await _context.VentasPorCategoria.ToListAsync();
            return Ok(ventasPorCategoria);
        }

        [HttpGet("ventas-por-producto")]
        public async Task<ActionResult<IEnumerable<VentasPorProducto>>> GetVentasPorProducto()
        {
            var ventasPorProducto = await _context.VentasPorProducto.ToListAsync();
            return Ok(ventasPorProducto);
        }
        [HttpGet("Devoluciones/{ventaId}")]
        public async Task<ActionResult<IEnumerable<Devolucion>>> ObtenerDevolucionesPorVenta(int ventaId)
        {
            // Consultar devoluciones asociadas con la Venta
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

        if (devoluciones == null || !devoluciones.Any())
        {
            return NotFound($"No se encontraron devoluciones para la venta con ID {ventaId}");
        }

        return Ok(devoluciones);

        }
        [HttpPost("Devolucion")]
        public async Task<ActionResult<Devolucion>> CrearDevolucion(Devolucion devolucion)
        {
            _context.Devoluciones.Add(devolucion);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(ObtenerDevolucionesPorVenta), new { ventaId = devolucion.VentaID }, devolucion);
        }
        [HttpPut("Devoluciones/{id}")]
        public async Task<IActionResult> ActualizarDevolucion(int id, Devolucion devolucion)
        {
            if (id != devolucion.DevolucionID)
            {
                return BadRequest();
            }

            _context.Entry(devolucion).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }






        // Método auxiliar para verificar si el producto existe
        private bool ProductoExists(int id)
        {
            return _context.Productos.Any(e => e.ProductoID == id);
        }
    }
}
