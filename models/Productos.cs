public class Producto
{
    public int ProductoID { get; set; } // Clave primaria
    public string Nombre { get; set; }
    public string Descripcion { get; set; }
    public decimal Precio { get; set; }
    public int Stock { get; set; }
    
    public int CategoriaID { get; set; } // Solo el ID de la categoría, no la categoría completa

    public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;

    // Aquí solo la referencia al ID, no toda la entidad Categoria
      // Puede ser omitido en la creación o solo usado para las respuestas (lectura)
}
