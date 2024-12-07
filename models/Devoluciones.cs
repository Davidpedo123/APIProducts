public class Devolucion
{
    public int DevolucionID { get; set; } // Clave primaria
    public int VentaID { get; set; } // Clave foránea
    public int ProductoID { get; set; } // Clave foránea
    public int Cantidad { get; set; }
    public string Motivo { get; set; }
    public DateTime FechaDevolucion { get; set; } = DateTime.UtcNow;

    
}
