public class Categoria
{
    public int CategoriaID { get; set; } // Clave primaria
    public string Nombre { get; set; }
    public string Descripcion { get; set; }
    public DateTime FechaCreacion { get; set; }
    
    public ICollection<Producto> Productos { get; set; } // Relación 1 a muchos
}
