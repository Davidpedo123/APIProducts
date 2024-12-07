using APIproductos.Models;
using APIproductos.Services;
using Microsoft.AspNetCore.Mvc;

namespace APIproductos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VentasController : ControllerBase
    {
        private readonly VentasService _ventasService;

        public VentasController(VentasService ventasService)
        {
            _ventasService = ventasService;
        }

        // GET: api/ventas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Venta>>> GetVentas()
        {
            var ventas = await _ventasService.ObtenerVentasAsync();
            return Ok(ventas);
        }

        // GET: api/ventas/por-categoria
        [HttpGet("por-categoria")]
        public async Task<ActionResult<IEnumerable<VentasPorCategoria>>> GetVentasPorCategoria()
        {
            var ventasPorCategoria = await _ventasService.ObtenerVentasPorCategoriaAsync();
            return Ok(ventasPorCategoria);
        }

        // GET: api/ventas/por-producto
        [HttpGet("por-producto")]
        public async Task<ActionResult<IEnumerable<VentasPorProducto>>> GetVentasPorProducto()
        {
            var ventasPorProducto = await _ventasService.ObtenerVentasPorProductoAsync();
            return Ok(ventasPorProducto);
        }
    }
}
