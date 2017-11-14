CREATE OR REPLACE FUNCTION REGVENTA (
    idCliente int,
    idEmpleado int,
    productos text,
    idSuc int,
    tipoPago int,
    fech date
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

