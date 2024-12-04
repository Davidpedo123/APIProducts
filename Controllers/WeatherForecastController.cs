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
            // Realiza un SELECT simple a la tabla Productos
            var productos = await _context.Productos.ToListAsync();
            return Ok(productos); // Devuelve los productos en formato JSON
        }

        // GET: api/productos/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Producto>> GetProductoPorID(int id)
        {
            // Busca el producto por ID
            var producto = await _context.Productos.FindAsync(id);

            if (producto == null)
            {
                return NotFound($"No se encontró un producto con ID {id}.");
            }

            return Ok(producto);
        }

        // POST: api/productos
        [HttpPost]
        public async Task<ActionResult<Producto>> PostProducto([FromBody] Producto producto)
        {
            // Validar el objeto recibido
            if (producto == null)
            {
                return BadRequest("El producto no puede ser nulo.");
            }

            if (string.IsNullOrEmpty(producto.Nombre))
            {
                return BadRequest("El nombre del producto es obligatorio.");
            }

            try
            {
                // Agregar el producto al contexto
                _context.Productos.Add(producto);

                // Guardar los cambios en la base de datos
                await _context.SaveChangesAsync();

                // Devolver el producto creado con el código de estado 201 (Created)
                return CreatedAtAction(nameof(GetProductoPorID), new { id = producto.ProductoID }, producto);
            }
            catch (Exception ex)
            {
                // Manejo de errores
                return StatusCode(500, $"Error al guardar el producto: {ex.Message}");
            }
        }
    }
}
