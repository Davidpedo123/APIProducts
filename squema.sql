-- DROP SCHEMA dbo;

CREATE SCHEMA dbo;
-- master.dbo.Categorias definition

-- Drop table

-- DROP TABLE master.dbo.Categorias;

CREATE TABLE master.dbo.Categorias (
	CategoriaID int IDENTITY(1,1) NOT NULL,
	Nombre nvarchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	Descripcion nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	FechaCreacion datetime DEFAULT getdate() NULL,
	CONSTRAINT PK__Categori__F353C1C55049653B PRIMARY KEY (CategoriaID)
);


-- master.dbo.MSreplication_options definition

-- Drop table

-- DROP TABLE master.dbo.MSreplication_options;

CREATE TABLE master.dbo.MSreplication_options (
	optname sysname COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	value bit NOT NULL,
	major_version int NOT NULL,
	minor_version int NOT NULL,
	revision int NOT NULL,
	install_failures int NOT NULL
);


-- master.dbo.Ventas definition

-- Drop table

-- DROP TABLE master.dbo.Ventas;

CREATE TABLE master.dbo.Ventas (
	VentaID int IDENTITY(1,1) NOT NULL,
	FechaVenta datetime DEFAULT getdate() NULL,
	Total decimal(10,2) NOT NULL,
	Cliente nvarchar(150) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	CONSTRAINT PK__Ventas__5B41514C83729983 PRIMARY KEY (VentaID)
);


-- master.dbo.spt_fallback_db definition

-- Drop table

-- DROP TABLE master.dbo.spt_fallback_db;

CREATE TABLE master.dbo.spt_fallback_db (
	xserver_name varchar(30) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	xdttm_ins datetime NOT NULL,
	xdttm_last_ins_upd datetime NOT NULL,
	xfallback_dbid smallint NULL,
	name varchar(30) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	dbid smallint NOT NULL,
	status smallint NOT NULL,
	version smallint NOT NULL
);


-- master.dbo.spt_fallback_dev definition

-- Drop table

-- DROP TABLE master.dbo.spt_fallback_dev;

CREATE TABLE master.dbo.spt_fallback_dev (
	xserver_name varchar(30) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	xdttm_ins datetime NOT NULL,
	xdttm_last_ins_upd datetime NOT NULL,
	xfallback_low int NULL,
	xfallback_drive char(2) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	low int NOT NULL,
	high int NOT NULL,
	status smallint NOT NULL,
	name varchar(30) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	phyname varchar(127) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL
);


-- master.dbo.spt_fallback_usg definition

-- Drop table

-- DROP TABLE master.dbo.spt_fallback_usg;

CREATE TABLE master.dbo.spt_fallback_usg (
	xserver_name varchar(30) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	xdttm_ins datetime NOT NULL,
	xdttm_last_ins_upd datetime NOT NULL,
	xfallback_vstart int NULL,
	dbid smallint NOT NULL,
	segmap int NOT NULL,
	lstart int NOT NULL,
	sizepg int NOT NULL,
	vstart int NOT NULL
);


-- master.dbo.spt_monitor definition

-- Drop table

-- DROP TABLE master.dbo.spt_monitor;

CREATE TABLE master.dbo.spt_monitor (
	lastrun datetime NOT NULL,
	cpu_busy int NOT NULL,
	io_busy int NOT NULL,
	idle int NOT NULL,
	pack_received int NOT NULL,
	pack_sent int NOT NULL,
	connections int NOT NULL,
	pack_errors int NOT NULL,
	total_read int NOT NULL,
	total_write int NOT NULL,
	total_errors int NOT NULL
);


-- master.dbo.Productos definition

-- Drop table

-- DROP TABLE master.dbo.Productos;

CREATE TABLE master.dbo.Productos (
	ProductoID int IDENTITY(1,1) NOT NULL,
	Nombre nvarchar(150) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	Descripcion nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	Precio decimal(10,2) NOT NULL,
	Stock int DEFAULT 0 NULL,
	CategoriaID int NOT NULL,
	FechaCreacion datetime DEFAULT getdate() NULL,
	CONSTRAINT PK__Producto__A430AE83CD00C492 PRIMARY KEY (ProductoID),
	CONSTRAINT FK__Productos__Categ__2A6B46EF FOREIGN KEY (CategoriaID) REFERENCES master.dbo.Categorias(CategoriaID)
);
 CREATE NONCLUSTERED INDEX idx_Productos_Categoria ON dbo.Productos (  CategoriaID ASC  )  
	 WITH (  PAD_INDEX = OFF ,FILLFACTOR = 100  ,SORT_IN_TEMPDB = OFF , IGNORE_DUP_KEY = OFF , STATISTICS_NORECOMPUTE = OFF , ONLINE = OFF , ALLOW_ROW_LOCKS = ON , ALLOW_PAGE_LOCKS = ON  )
	 ON [PRIMARY ] ;


-- master.dbo.DetalleVentas definition

-- Drop table

-- DROP TABLE master.dbo.DetalleVentas;

CREATE TABLE master.dbo.DetalleVentas (
	DetalleID int IDENTITY(1,1) NOT NULL,
	VentaID int NOT NULL,
	ProductoID int NOT NULL,
	Cantidad int NOT NULL,
	PrecioUnitario decimal(10,2) NOT NULL,
	Subtotal AS ([Cantidad]*[PrecioUnitario]) PERSISTED,
	CONSTRAINT PK__DetalleV__6E19D6FAB88FA468 PRIMARY KEY (DetalleID),
	CONSTRAINT FK__DetalleVe__Produ__3118447E FOREIGN KEY (ProductoID) REFERENCES master.dbo.Productos(ProductoID),
	CONSTRAINT FK__DetalleVe__Venta__30242045 FOREIGN KEY (VentaID) REFERENCES master.dbo.Ventas(VentaID)
);


-- master.dbo.Devoluciones definition

-- Drop table

-- DROP TABLE master.dbo.Devoluciones;

CREATE TABLE master.dbo.Devoluciones (
	DevolucionID int IDENTITY(1,1) NOT NULL,
	VentaID int NOT NULL,
	ProductoID int NOT NULL,
	Cantidad int NOT NULL,
	Motivo nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	FechaDevolucion datetime DEFAULT getdate() NULL,
	CONSTRAINT PK__Devoluci__28E7B0E71A463D19 PRIMARY KEY (DevolucionID),
	CONSTRAINT FK__Devolucio__Produ__35DCF99B FOREIGN KEY (ProductoID) REFERENCES master.dbo.Productos(ProductoID),
	CONSTRAINT FK__Devolucio__Venta__34E8D562 FOREIGN KEY (VentaID) REFERENCES master.dbo.Ventas(VentaID)
);


-- dbo.spt_values source

create view spt_values as
select name collate database_default as name,
	number,
	type collate database_default as type,
	low, high, status
from sys.spt_values;