public class Devolucion
{
    public int DevolucionID { get; set; } 
    public int VentaID { get; set; } 
    public int ProductoID { get; set; } 
    public int Cantidad { get; set; }
    public string Motivo { get; set; }
    public DateTime FechaDevolucion { get; set; } = DateTime.UtcNow;

    
}
