CREATE TABLE CAJA(
idCaja INT NOT NULL,
idSucursal INT NOT NULL,
Fecha timestamp,
idEmpleado INT,
Efectivo INT,
Tipo INT, 
PRIMARY KEY (idCaja, Fecha, idSucursal, Tipo)
)

CREATE TABLE EMPLEADO(
Cedula INT NOT NULL,
Nombre VARCHAR(50),
pApellido VARCHAR (60),
sApellido VARCHAR (60),
Username VARCHAR (50),
Password VARCHAR(100),
Email VARCHAR (60),
Estado INT,
PRIMARY KEY (Cedula)
)

CREATE TABLE SUCURSALES(
	idSucursal SERIAL not null,
  Nombre VARCHAR(50),
  Direccion VARCHAR(150),
  Imagen TEXT,
  Estado INT,
  
  PRIMARY KEY(idSucursal)
)

CREATE TABLE CLIENTE(
Cedula INT NOT NULL,
Nombre VARCHAR(50),
pApellido VARCHAR (60),
sApellido VARCHAR (60),
Estado INT, 
  
PRIMARY KEY (Cedula)
)

CREATE TABLE TIPOPAGO(
idTipo SERIAL NOT NULL,
  Nombre VARCHAR(50),
  Descripcion VARCHAR(150),
  
  PRIMARY KEY (idTipo)
)

CREATE TABLE ROLES(
idRol SERIAL NOT NULL,
Nombre VARCHAR(50),
Descripcion VARCHAR(150),
Estado INT,

PRIMARY KEY (idRol)
)

CREATE TABLE PROVEEDORES(
	idProveedor INT not null,
  Nombre VARCHAR(50),
  Telefono INT,
  Estado INT,
  
  PRIMARY KEY (idProveedor)
)

CREATE TABLE VENTA(
	idVenta SERIAL not null,
  idCliente INT not null,
  tPago INT not null,
  idSucursal INT not null,
  fecha DATE,
  
  PRIMARY KEY (idVenta)
)

CREATE TABLE PRODUCTOS(
	EAN INT not null,
  idProveedor INT not null,
  Nombre VARCHAR(50),
  Imagen TEXT,
  Descripcion VARCHAR(100),
  Estado INT,
  
  PRIMARY KEY (EAN)
)
CREATE TABLE PRODUCTOSXSUCURSAL(
	idSucursal INT not null,
  idProducto INT not null,
  Cantidad INT,
  Precio INT,
  
  PRIMARY KEY (idSucursal, idProducto)
)


CREATE TABLE VENTASXEMPLEADO(
	idVenta INT not null,
  idEmpleado INT not null,
  
  PRIMARY KEY(idVenta, idEmpleado)
)

CREATE TABLE EMPLEADOSXSUCURSAL(
	idEmpleado INT not null,
  idSucursal INT not null,
  idRol INT not null,
  
  PRIMARY KEY(idEmpleado, idSucursal)
)

CREATE TABLE VENTASXSUCURSAL(
  idSucursal INT not null,
  idVenta INT not null,
  
  PRIMARY KEY (idSucursal,idVenta)
)

CREATE TABLE CAJASXSUCURSAL(
	idCaja int,
    idSucursal int,
    
    PRIMARY KEY (idCaja, idSucursal)
)

CREATE TABLE DETALLEVENTA(
  idVenta INT,
  idProducto INT,
  Cantidad INT,
  PRIMARY KEY (idVenta, idProducto)
)

CREATE TABLE EMPRESA(
	idempresa SERIAL not null,
    Nombre VARCHAR(50),
    
    PRIMARY KEY (idempresa)
)

CREATE TABLE SUCURSALXEMPRESA(
	idempresa int not null,
    idsucursal int not null,
    
    PRIMARY KEY (idempresa,idsucursal)
)

ALTER TABLE SUCURSALXEMPRESA
ADD CONSTRAINT FK_SUCURSAL_SUCURSALXEMPRESA FOREIGN KEY (idsucursal) REFERENCES SUCURSALES(idSucursal)
ALTER TABLE SUCURSALXEMPRESA
ADD CONSTRAINT FK_SUCURSAL_EMPRESA FOREIGN KEY (idempresa) REFERENCES EMPRESA(idempresa)

ALTER TABLE PRODUCTOS
ADD CONSTRAINT FK_PRODUCTOS_PROVEEDORES FOREIGN KEY (idProveedor) REFERENCES PROVEEDORES(idProveedor)

ALTER TABLE VENTASXSUCURSAL
ADD CONSTRAINT FK_VENTASXSUCURSAL_SUCURSALES FOREIGN KEY (idSucursal) REFERENCES SUCURSALES(idSucursal)
ALTER TABLE VENTASXSUCURSAL
ADD CONSTRAINT FK_VENTASXSUCURSAL_VENTAS FOREIGN KEY (idVenta) REFERENCES VENTA(idVenta)

ALTER TABLE VENTA
ADD CONSTRAINT FK_VENTA_SUCURSAL FOREIGN KEY (idSucursal) REFERENCES SUCURSALES(idSucursal)
ALTER TABLE VENTA
ADD CONSTRAINT FK_VENTA_TIPOPAGO FOREIGN KEY (tPago) REFERENCES TIPOPAGO(idTipo)
ALTER TABLE VENTA
ADD CONSTRAINT FK_VENTA_CLIENTES FOREIGN KEY (idCliente) REFERENCES CLIENTE(Cedula)

ALTER TABLE CAJA
ADD CONSTRAINT FK_CAJA_EMPLEADOS FOREIGN KEY (idEmpleado) REFERENCES EMPLEADO(Cedula)
ALTER TABLE CAJA
ADD CONSTRAINT FK_CAJA_CAJASXSUCURSAL FOREIGN KEY (idCaja,idSucursal) REFERENCES CAJASXSUCURSAL(idCaja,idSucursal)

ALTER TABLE PRODUCTOSXSUCURSAL
ADD CONSTRAINT FK_IDSUCURSAL_SUCURSAL FOREIGN KEY (idSucursal) REFERENCES SUCURSALES(idSucursal)
ALTER TABLE PRODUCTOSXSUCURSAL
ADD CONSTRAINT FK_IDPRODUCTOS_SUCURSAL FOREIGN KEY (idProducto) REFERENCES PRODUCTOS(EAN)

ALTER TABLE VENTASXEMPLEADO
ADD CONSTRAINT FK_IDVENTA_EMPLEADO FOREIGN KEY (idVenta) REFERENCES VENTA(idVenta)
ALTER TABLE VENTASXEMPLEADO
ADD CONSTRAINT FK_IDEMPLEADO_EMPLEADO FOREIGN KEY (idEmpleado) REFERENCES EMPLEADO(Cedula)

ALTER TABLE EMPLEADOSXSUCURSAL
ADD CONSTRAINT FK_IDEMPLEADO_SUCURSAL FOREIGN KEY (idEmpleado) REFERENCES EMPLEADO(Cedula)
ALTER TABLE EMPLEADOSXSUCURSAL
ADD CONSTRAINT FK_IDSUCURSAL_SUCURSALES FOREIGN KEY (idSucursal) REFERENCES SUCURSALES(idSucursal)
ALTER TABLE EMPLEADOSXSUCURSAL
ADD CONSTRAINT FK_EMPLEADOSXSUCURSAL_ROLES FOREIGN KEY (idRol) REFERENCES ROLES(idRol)

ALTER TABLE CAJASXSUCURSAL
ADD CONSTRAINT FK_CAJASXSUCURSAL_SUCURSAL FOREIGN KEY (idSucursal) REFERENCES sucursales(idsucursal)

ALTER TABLE DETALLEVENTA
ADD CONSTRAINT FK_DETALLEVENTA_VENTA FOREIGN KEY (idVenta) REFERENCES venta(idVenta)

ALTER TABLE DETALLEVENTA
ADD CONSTRAINT FK_DETALLEVENTA_PRODUCTOS FOREIGN KEY (idProducto) REFERENCES productos(ean)