INSERT INTO cliente(cedula,nombre,papellido,sapellido, estado) VALUES (100020354,'Marvin','Fonseca','Fernandez',1);
INSERT INTO cliente(cedula,nombre,papellido,sapellido, estado) VALUES (113459876,'Manuel','Calderon','Arango',1);
INSERT INTO cliente(cedula,nombre,papellido,sapellido, estado) VALUES (309990888,'Valeria','Artavia','Meneces',1);
INSERT INTO cliente(cedula,nombre,papellido,sapellido, estado) VALUES (234789465,'Felix','Meneces','Garzona',1);
INSERT INTO cliente(cedula,nombre,papellido,sapellido, estado) VALUES (203940576,'Alejandra','Rivas','Vega',1);

INSERT INTO empleado (cedula, email, nombre, papellido, sapellido, password, username, estado) VALUES (1, 'rsolano1996@gmail.com','Rodolfo','Solano','Asenjo','123', 'rsolano',1 );
INSERT INTO empleado (cedula, email, nombre, papellido, sapellido, password, username, estado) VALUES (2,'marcfg29@gmail.com','Marco','Fernandez','Apellido2','123', 'mfernandez',1);
INSERT INTO empleado (cedula, email, nombre, papellido, sapellido, password, username, estado) VALUES (3,'blabla@gmail.com','Gafeth','Rodriguez','Sanchez','123', 'ghernandez',1);
INSERT INTO empleado (cedula, email, nombre, papellido, sapellido, password, username, estado) VALUES (4,'adriansanchez2015101969@gmail.com','Adrian','Sanchez','Anderson','pato123', 'asanchez',1);
INSERT INTO empleado (cedula, email, nombre, papellido, sapellido, password, username, estado) VALUES (5,'afelipe.vargas.r@gmail.com','Andres','Vargas','Rivera','123', 'avargas', 1);

INSERT INTO sucursales ( nombre, direccion, estado, imagen) VALUES ('Centro Escazu', 'Bomba Escazu',1,'');
INSERT INTO sucursales ( nombre, direccion, estado, imagen) VALUES ('Centro de Desamparados', 'Bomba Alajuela',1,'');
INSERT INTO sucursales ( nombre, direccion, estado, imagen) VALUES ('Phischel Mall', 'Phischel Mall San Pedro',1,'');
INSERT INTO sucursales ( nombre, direccion, estado, imagen) VALUES ('Phischel Central', 'Phischel San Jose', 1,'');

INSERT INTO roles (nombre, descripcion, estado) VALUES ('Administrador','Admin de la farmacia', 1);
INSERT INTO roles (nombre, descripcion, estado) VALUES ('Cajero','Maneja diner en la farmacia', 1);
INSERT INTO roles (nombre, descripcion, estado) VALUES ('Farmaceutico','Vendedores especializados en medicina', 1);
INSERT INTO roles (nombre, descripcion, estado) VALUES ('Ingeniero','Administra de la app', 1);
INSERT INTO roles (nombre, descripcion, estado) VALUES ('Doctor','Medico general', 1);

INSERT INTO empleadosxsucursal (idsucursal, idempleado, idrol) VALUES (1, 1, 1);
INSERT INTO empleadosxsucursal (idsucursal, idempleado, idrol) VALUES (2, 2, 2);
INSERT INTO empleadosxsucursal (idsucursal, idempleado, idrol) VALUES (3, 3, 3);
INSERT INTO empleadosxsucursal (idsucursal, idempleado, idrol) VALUES (4, 4, 1);
INSERT INTO empleadosxsucursal (idsucursal, idempleado, idrol) VALUES (1, 5, 2);
INSERT INTO empleadosxsucursal (idsucursal, idempleado, idrol) VALUES (2, 1, 3);
INSERT INTO empleadosxsucursal (idsucursal, idempleado, idrol) VALUES (3, 2, 1);
INSERT INTO empleadosxsucursal (idsucursal, idempleado, idrol) VALUES (4, 3, 2);
INSERT INTO empleadosxsucursal (idsucursal, idempleado, idrol) VALUES (1, 4, 3);
INSERT INTO empleadosxsucursal (idsucursal, idempleado, idrol) VALUES (2, 5, 1);

INSERT INTO proveedores (idproveedor, nombre, telefono, estado) VALUES (0,'Bayern', 22550890, 1);
INSERT INTO proveedores (idproveedor, nombre, telefono, estado) VALUES (1,'Playboy', 22733665, 1);
INSERT INTO proveedores (idproveedor, nombre, telefono, estado) VALUES (2,'Distribuidor X', 22360911, 1);
INSERT INTO proveedores (idproveedor, nombre, telefono, estado) VALUES (3,'GSK', 22795413, 1);

INSERT INTO productos(ean, idproveedor, nombre, descripcion, estado, imagen) VALUES (0,0,'talerdin','medicamento',1,'');
INSERT INTO productos(ean, idproveedor, nombre, descripcion, estado, imagen) VALUES (1,0,'antifludes','antogripal y dolor',1,'');
INSERT INTO productos(ean, idproveedor, nombre, descripcion, estado, imagen) VALUES (2,0,'tabcin','antogripal',1,'');
INSERT INTO productos(ean, idproveedor, nombre, descripcion, estado, imagen) VALUES (3,0,'gex','antogripal',1,'');
INSERT INTO productos(ean, idproveedor, nombre, descripcion, estado, imagen) VALUES (4,1,'condones','N/A',1,'');
INSERT INTO productos(ean, idproveedor, nombre, descripcion, estado, imagen) VALUES (5,2,'bendas','para heridas',1,'');
INSERT INTO productos(ean, idproveedor, nombre, descripcion, estado, imagen) VALUES (6,3,'suero x','para vomito y diarrea',1,'');
INSERT INTO productos(ean, idproveedor, nombre, descripcion, estado, imagen) VALUES (7,3,'Ibuprofeno','para el dolor',1,'');
INSERT INTO productos(ean, idproveedor, nombre, descripcion, estado, imagen) VALUES (8,1,'cigarros','N/A',1,'');
INSERT INTO productos(ean, idproveedor, nombre, descripcion, estado, imagen) VALUES (9,2,'chicles','N/A',1,'');
INSERT INTO productos(ean, idproveedor, nombre, descripcion, estado, imagen) VALUES (10,0,'cataflan','para el dolor',1,'');

INSERT INTO productosxsucursal (idsucursal,idproducto, cantidad, precio) VALUES (1,1,10,500);
INSERT INTO productosxsucursal (idsucursal,idproducto, cantidad, precio) VALUES (1,2,10,1200);
INSERT INTO productosxsucursal (idsucursal,idproducto, cantidad, precio) VALUES (1,3,10,250);
INSERT INTO productosxsucursal (idsucursal,idproducto, cantidad, precio) VALUES (1,4,10,2200);
INSERT INTO productosxsucursal (idsucursal,idproducto, cantidad, precio) VALUES (1,5,10,100);
INSERT INTO productosxsucursal (idsucursal,idproducto, cantidad, precio) VALUES (1,6,10,400);
INSERT INTO productosxsucursal (idsucursal,idproducto, cantidad, precio) VALUES (1,7,10,120);
INSERT INTO productosxsucursal (idsucursal,idproducto, cantidad, precio) VALUES (1,8,10,875);
INSERT INTO productosxsucursal (idsucursal,idproducto, cantidad, precio) VALUES (1,9,10,525);
INSERT INTO productosxsucursal (idsucursal,idproducto, cantidad, precio) VALUES (1,10,10,330);
INSERT INTO productosxsucursal (idsucursal,idproducto, cantidad, precio) VALUES (2,0,10,1200);

INSERT INTO tipopago (nombre, descripcion) VALUES ('Tarjeta', 'pago mediante tarjeta de credito o debito');
INSERT INTO tipopago (nombre, descripcion) VALUES ('Efectivo', 'pago mediante dinero en efectivo');

INSERT INTO cajasxsucursal (idcaja, idsucursal) VALUES (1,1);
INSERT INTO cajasxsucursal (idcaja, idsucursal) VALUES (1,2);

INSERT INTO caja (idcaja, fecha, idsucursal, idempleado, efectivo, tipo) VALUES (1,'2017-03-12T00:00:00',1, 1,54500,0);
INSERT INTO caja (idcaja, fecha, idsucursal, idempleado, efectivo, tipo) VALUES (2,'2017-03-12T00:00:00',1, 1,104000,1);


