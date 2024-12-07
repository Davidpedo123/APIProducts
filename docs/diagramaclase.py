from graphviz import Digraph

# Crear el grafo
dot = Digraph()

# Configurar la forma de los nodos
dot.attr(node='shape', shape='square')  # Esto hace que los nodos sean cuadrados

# Agregar los nodos y las relaciones
dot.node('A', 'Venta\n+int VentaID\n+DateTime FechaVenta\n+decimal Total\n+string Cliente\n+DetalleVentas\n+Devoluciones')
dot.node('B', 'DetalleVenta\n+int DetalleID\n+int VentaID\n+int ProductoID\n+int Cantidad\n+decimal PrecioUnitario\n+decimal Subtotal')
dot.node('C', 'Devolucion\n+int DevolucionID\n+int VentaID\n+int ProductoID\n+int Cantidad\n+string Motivo\n+DateTime FechaDevolucion')
dot.node('D', 'Producto\n+int ProductoID\n+string Nombre\n+string Descripcion\n+decimal Precio\n+int Stock\n+int CategoriaID\n+DateTime FechaCreacion\n+GetProductos()\n+GetProductoPorID()\n+GetProductosPorCategoria()\n+CrearProducto()\n+DeleteProducto()')
dot.node('E', 'Categoria\n+int CategoriaID\n+string Nombre\n+string Descripcion\n+DateTime FechaCreacion\n+Productos')

# Agregar las relaciones
dot.edge('A', 'B', label='Contiene')
dot.edge('A', 'C', label='Contiene')
dot.edge('B', 'D', label='Incluye')
dot.edge('C', 'D', label='Incluye')
dot.edge('D', 'E', label='Tiene')

# Renderizar y mostrar el diagrama
dot.render('diagrama_clases2', format='png', cleanup=True)
