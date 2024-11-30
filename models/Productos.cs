public class Producto
{
    public int ProductoID { get; set; } // Clave primaria
    public string Nombre { get; set; }
    public string Descripcion { get; set; }
    public decimal Precio { get; set; }
    public int Stock { get; set; }
    public int CategoriaID { get; set; } // Clave foránea
    public DateTime FechaCreacion { get; set; }

    public Categoria Categoria { get; set; } // Relación con la tabla Categoria
}
