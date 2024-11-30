public class Venta
{
    public int VentaID { get; set; } // Clave primaria
    public DateTime FechaVenta { get; set; }
    public decimal Total { get; set; }
    public string Cliente { get; set; }
    
    public ICollection<DetalleVenta> DetalleVentas { get; set; } // Relación 1 a muchos
    public ICollection<Devolucion> Devoluciones { get; set; } // Relación 1 a muchos
}
