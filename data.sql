CREATE TABLE Categorias (
    CategoriaID INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(100) NOT NULL,
    Descripcion NVARCHAR(255),
    FechaCreacion DATETIME DEFAULT GETDATE()
);

CREATE TABLE Productos (
    ProductoID INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(150) NOT NULL,
    Descripcion NVARCHAR(255),
    Precio DECIMAL(10,2) NOT NULL,
    Stock INT DEFAULT 0,
    CategoriaID INT NOT NULL,
    FechaCreacion DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (CategoriaID) REFERENCES Categorias(CategoriaID)
);

CREATE TABLE Ventas (
    VentaID INT PRIMARY KEY IDENTITY(1,1),
    FechaVenta DATETIME DEFAULT GETDATE(),
    Total DECIMAL(10,2) NOT NULL,
    Cliente NVARCHAR(150)
);

CREATE TABLE DetalleVentas (
    DetalleID INT PRIMARY KEY IDENTITY(1,1),
    VentaID INT NOT NULL,
    ProductoID INT NOT NULL,
    Cantidad INT NOT NULL,
    PrecioUnitario DECIMAL(10,2) NOT NULL,
    Subtotal AS (Cantidad * PrecioUnitario) PERSISTED,
    FOREIGN KEY (VentaID) REFERENCES Ventas(VentaID),
    FOREIGN KEY (ProductoID) REFERENCES Productos(ProductoID)
);

CREATE TABLE Devoluciones (
    DevolucionID INT PRIMARY KEY IDENTITY(1,1),
    VentaID INT NOT NULL,
    ProductoID INT NOT NULL,
    Cantidad INT NOT NULL,
    Motivo NVARCHAR(255),
    FechaDevolucion DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (VentaID) REFERENCES Ventas(VentaID),
    FOREIGN KEY (ProductoID) REFERENCES Productos(ProductoID)
);

CREATE INDEX idx_Productos_Categoria ON Productos(CategoriaID);






--INSERTAR DATOS
--Categorias
INSERT INTO Categorias (Nombre, Descripcion)
VALUES ('Camisetas', 'Camisetas de diferentes tamaños y colores'),
       ('Pantalones', 'Pantalones de mezclilla y algodón'),
       ('Zapatos', 'Zapatos deportivos y casuales');

--Productos
INSERT INTO Productos (Nombre, Descripcion, Precio, Stock, CategoriaID)
VALUES ('Camiseta Roja', 'Camiseta de algodón rojo', 15.99, 100, 1),
       ('Pantalón Azul', 'Pantalón de mezclilla azul', 25.50, 50, 2),
       ('Zapatillas Deportivas', 'Zapatillas deportivas cómodas', 40.00, 200, 3);
--Ventas
INSERT INTO Ventas (FechaVenta, Total, Cliente)
VALUES (GETDATE(), 81.49, 'Juan Pérez');
--DetalleVentas
INSERT INTO DetalleVentas (VentaID, ProductoID, Cantidad, PrecioUnitario)
VALUES (1, 1, 2, 15.99),  -- 2 Camisetas rojas
       (1, 2, 1, 25.50);  -- 1 Pantalón azul
--Devoluciones
INSERT INTO Devoluciones (VentaID, ProductoID, Cantidad, Motivo)
VALUES (1, 1, 1, 'Tamaño incorrecto');


      


