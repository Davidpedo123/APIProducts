using APIproductos.Models;
using APIproductos.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace APIproductos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductosController : ControllerBase
    {
        private readonly ServicesProduct _productService;
        private readonly VentasService _ventasService;

        public ProductosController(ServicesProduct productService, VentasService ventasService)
        {
            _productService = productService;
            _ventasService = ventasService;
        }

        [HttpGet]
        public async Task<IActionResult> GetProductos()
        {
            var productos = await _productService.GetProductosAsync();
            return Ok(productos);
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductoPorID(int id)
        {
            var producto = await _productService.GetProductoPorIDAsync(id);
            if (producto == null)
                return NotFound($"No se encontró un producto con ID {id}.");
            return Ok(producto);
        }
         
        [HttpGet("categoria/{categoriaId}")]
        public async Task<IActionResult> GetProductosPorCategoria(int categoriaId)
        {
            var productos = await _productService.GetProductosPorCategoriaAsync(categoriaId);

            if (productos == null || !productos.Any())
            {
                return NotFound($"No se encontraron productos para la categoría con ID {categoriaId}.");
            }

            return Ok(productos);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProducto(int id, [FromBody] Producto producto)
        {
            try
            {
                
                var productoActualizado = await _productService.UpdateProductoAsync(id, producto);
                return Ok(productoActualizado);  
            }
            catch (Exception ex)
            {
                
                return NotFound(ex.Message);  
            }
        }
        

        [HttpPost]
        public async Task<IActionResult> CrearProducto([FromBody] Producto producto)
        {
            var productoCreado = await _productService.CrearProductoAsync(producto);
            return CreatedAtAction(nameof(GetProductoPorID), new { id = productoCreado.ProductoID }, productoCreado);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProducto(int id)
        {
            var producto = await _productService.GetProductoPorIDAsync(id);
            if (producto == null)
                return NotFound();

            await _productService.DeleteProductoAsync(producto);
            return NoContent();
        }
        
    }
}
