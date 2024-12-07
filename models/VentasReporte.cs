
public class VentasPorCategoria
{
    public int CategoriaID { get; set; }
    public string NombreCategoria { get; set; }
    public int CantidadVendida { get; set; }
    public decimal TotalGenerado { get; set; }
}


public class VentasPorProducto
{
    public int ProductoID { get; set; }
    public string NombreProducto { get; set; }
    public int CantidadVendida { get; set; }
    public decimal TotalGenerado { get; set; }
}
