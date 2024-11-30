public class DetalleVenta
{
    public int DetalleID { get; set; } // Clave primaria
    public int VentaID { get; set; } // Clave foránea
    public int ProductoID { get; set; } // Clave foránea
    public int Cantidad { get; set; }
    public decimal PrecioUnitario { get; set; }
    public decimal Subtotal { get; set; } // Esta es una columna calculada en SQL

    public Venta Venta { get; set; } // Relación con la tabla Venta
    public Producto Producto { get; set; } // Relación con la tabla Producto
}
