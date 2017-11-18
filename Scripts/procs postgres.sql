CREATE OR REPLACE FUNCTION REGVENTA (
    idCliente int,
    idEmpleado int,
    productos text,
    idSuc int,
    tipoPago int,
    fech timestamp
                                    )
RETURNS TEXT AS $state$
    DECLARE p2 text;
    DECLARE tam int;
    DECLARE cont int;
    DECLARE idVenta int; 
    DECLARE cant int;
    DECLARE prod int;
BEGIN
	cont:=0;
	tam:=json_array_length(CAST(productos AS json));
    INSERT INTO venta(idcliente, tpago, idsucursal, fecha) VALUES (idCliente, tipoPago, idSuc, fech) RETURNING venta.idventa into idVenta;
    INSERT INTO ventasxempleado(idventa, idempleado) VALUES (idVenta, idEmpleado);
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
	RETURN QUERY SELECT detalleventa.idproducto AS ean, productos.nombre, SUM(detalleventa.cantidad)/2 AS cantidad FROM detalleventa INNER JOIN
    productos ON (productos.ean=detalleventa.idproducto) INNER JOIN venta ON (venta.idventa IS NOT NULL) WHERE (venta.fecha>=ifecha) 
    AND (venta.fecha<ffecha) GROUP BY detalleventa.idproducto , productos.nombre ORDER BY cantidad  DESC LIMIT 20;
END;
$$ LANGUAGE plpgsql;



