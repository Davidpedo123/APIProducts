using APIproductos.Models;
using APIproductos.Services;
using Microsoft.AspNetCore.Mvc;

namespace APIproductos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DevolucionesController : ControllerBase
    {
        private readonly DevolucionesService _devolucionesService;

        public DevolucionesController(DevolucionesService devolucionesService)
        {
            _devolucionesService = devolucionesService;
        }

        [HttpGet("{ventaId}")]
        public async Task<IActionResult> ObtenerDevolucionesPorVenta(int ventaId)
        {
            var devoluciones = await _devolucionesService.ObtenerDevolucionesPorVentaAsync(ventaId);

            if (devoluciones == null || !devoluciones.Any())
                return NotFound($"No se encontraron devoluciones para la venta con ID {ventaId}");

            return Ok(devoluciones);
        }

        [HttpPost]
        public async Task<IActionResult> CrearDevolucion([FromBody] Devolucion devolucion)
        {
            var nuevaDevolucion = await _devolucionesService.CrearDevolucionAsync(devolucion);
            return CreatedAtAction(nameof(ObtenerDevolucionesPorVenta), new { ventaId = nuevaDevolucion.VentaID }, nuevaDevolucion);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarDevolucion(int id, [FromBody] Devolucion devolucion)
        {
            var resultado = await _devolucionesService.ActualizarDevolucionAsync(id, devolucion);

            if (!resultado)
                return BadRequest("El ID de la devolución no coincide o no se puede actualizar");

            return NoContent();
        }
    }
}
