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
    }
}
