public class Venta
{
    public int VentaID { get; set; } 
    public DateTime FechaVenta { get; set; }
    public decimal Total { get; set; }
    public string Cliente { get; set; }
    
    public ICollection<DetalleVenta> DetalleVentas { get; set; } 
    public ICollection<Devolucion> Devoluciones { get; set; } 
}
