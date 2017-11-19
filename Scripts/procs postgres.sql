CREATE OR REPLACE FUNCTION REGVENTA (
    idCliente int,
    idEmpleado int,
    productos text,
    idSuc int,
    tipoPago int,
    fech timestamp,
    star time,
    endd time
                                    )
RETURNS TEXT AS $state$
    DECLARE p2 text;
    DECLARE tam int;
    DECLARE cont int;
    DECLARE idVen int; 
    DECLARE cant int;
    DECLARE prod int;
BEGIN
	cont:=0;
	tam:=json_array_length(CAST(productos AS json));
    INSERT INTO venta(idcliente, tpago, idsucursal, fecha, starts, ends) VALUES (idCliente, tipoPago, idSuc, fech, star, endd) RETURNING venta.idventa into idVen;
    RAISE NOTICE idVen;
    INSERT INTO ventasxempleado(idventa, idempleado) VALUES (idVen, idEmpleado);
	WHILE cont<tam LOOP
        p2:=productos::json->cont;
        cant:=p2::json->'cantidad';
        prod:=p2::json->'ean';
        INSERT INTO detalleventa(idventa, idproducto, cantidad) VALUES (idVenta, prod, cant);
        UPDATE productosxsucursal SET cantidad=cantidad-cant WHERE productosxsucursal.idsucursal=idSuc AND  productosxsucursal.idproducto=prod;
        cont:=cont+1;
    END LOOP;
    RETURN 'true';
END;
$state$ LANGUAGE plpgsql;




CREATE OR REPLACE FUNCTION TOPSALESBYDATE (
    ifecha DATE,
    ffecha DATE
                                    )
RETURNS TABLE(ean integer, nombre varchar(50), cantidad bigint) AS $$
BEGIN
	RETURN QUERY SELECT detalleventa.idproducto AS ean, productos.nombre, SUM(detalleventa.cantidad) AS cantidad FROM productos INNER JOIN
    detalleventa ON (productos.ean=detalleventa.idproducto) INNER JOIN venta ON (detalleventa.idventa=venta.idventa) WHERE (venta.fecha>=ifecha) 
    AND (venta.fecha<=ffecha) GROUP BY detalleventa.idproducto , productos.nombre ORDER BY cantidad  DESC LIMIT 20;
END;
$$ LANGUAGE plpgsql;




CREATE OR REPLACE FUNCTION TOPSUCURSALSALES (
    suc INTEGER
                                    )
RETURNS TABLE(ean integer, nombre varchar(50), cantidad bigint) AS $$
BEGIN
	RETURN QUERY SELECT detalleventa.idproducto AS ean, productos.nombre, SUM(detalleventa.cantidad) AS cantidad FROM productos 
    INNER JOIN detalleventa ON (productos.ean=detalleventa.idproducto) INNER JOIN venta ON (detalleventa.idventa=venta.idventa) 
    WHERE (venta.idsucursal=suc) GROUP BY detalleventa.idproducto , productos.nombre ORDER BY cantidad  DESC LIMIT 20;
END;
$$ LANGUAGE plpgsql;




CREATE OR REPLACE FUNCTION TOPEMPLEADOSALES (
    idemp INTEGER
                                    )
RETURNS TABLE(ean integer, nombre varchar(50), cantidad bigint) AS $$
BEGIN
	RETURN QUERY SELECT detalleventa.idproducto AS ean, productos.nombre, SUM(detalleventa.cantidad) AS cantidad FROM productos 
	INNER JOIN detalleventa ON (productos.ean=detalleventa.idproducto) INNER JOIN venta ON (detalleventa.idventa=venta.idventa) 
	INNER JOIN ventasxempleado ON (ventasxempleado.idventa=venta.idventa) WHERE (ventasxempleado.idempleado=idemp) GROUP BY 
    detalleventa.idproducto , productos.nombre ORDER BY cantidad  DESC LIMIT 20;
END;
$$ LANGUAGE plpgsql;




CREATE OR REPLACE FUNCTION LOWQUANTITY()
RETURNS TABLE(idsucursal integer, sucursal varchar(50), ean integer, nombre varchar(50), cantidad integer) AS $$
BEGIN
    RETURN QUERY SELECT productosxsucursal.idsucursal, sucursales.nombre AS sucursal, productos.ean, productos.nombre, 
    productosxsucursal.cantidad FROM productosxsucursal INNER JOIN sucursales ON (sucursales.idsucursal=productosxsucursal.idsucursal) 
    INNER JOIN productos ON (productosxsucursal.idproducto=productos.ean) WHERE (productosxsucursal.cantidad<=5) 
    ORDER BY cantidad ASC;
END;
$$ LANGUAGE plpgsql;



CREATE OR REPLACE FUNCTION PROMTIMEEMPLEADO(
    fech DATE
)
RETURNS TABLE(cedula integer, nombre varchar(50), papellido varchar(60), sapellido varchar(60), tiempopromedio interval) AS $$
BEGIN
    RETURN QUERY SELECT empleado.cedula, empleado.nombre, empleado.papellido, empleado.sapellido, sum(venta.ends-venta.starts)/count(venta.ends)
    AS tiempoPromedio FROM empleado INNER JOIN ventasxempleado ON (ventasxempleado.idempleado=empleado.cedula) INNER JOIN venta ON 
    (venta.idventa=ventasxempleado.idventa) WHERE (venta.fecha=fech) GROUP BY empleado.cedula;
END;
$$ LANGUAGE plpgsql;







