CREATE OR REPLACE FUNCTION process_raid() RETURNS TRIGGER AS $raid$
    BEGIN
    	IF (TG_TABLE_NAME = 'cliente') THEN
            IF (TG_OP = 'UPDATE') THEN
                PERFORM dblink_exec('raid_server','UPDATE cliente SET nombre='|| '''' || NEW.nombre || '''' ||',papellido='|| '''' || NEW.papellido || '''' ||',sapellido='|| '''' || NEW.sapellido || '''' ||',estado='|| NEW.estado ||' WHERE cedula='|| OLD.cedula ||';');
                RETURN NEW;
            ELSIF (TG_OP = 'INSERT') THEN
                PERFORM dblink_exec('raid_server','INSERT INTO cliente VALUES ('|| NEW.cedula ||','|| '''' || NEW.nombre || '''' ||','|| '''' || NEW.papellido || '''' ||','|| '''' || NEW.sapellido || '''' ||','|| NEW.estado ||');');
                RETURN NEW;
            END IF;
        ELSEIF (TG_TABLE_NAME = 'empleado') THEN
        	IF (TG_OP = 'UPDATE') THEN
                PERFORM dblink_exec('raid_server','UPDATE empleado SET nombre='|| '''' || NEW.nombre || '''' ||',papellido='|| '''' || NEW.papellido || '''' ||',sapellido='|| '''' || NEW.sapellido || '''' ||',username='|| '''' || NEW.username || '''' || ',password='|| '''' || NEW.password || '''' || ',email='|| '''' || NEW.email || '''' || ',estado='|| NEW.estado ||' WHERE cedula='|| OLD.cedula ||';');
                RETURN NEW;
            ELSIF (TG_OP = 'INSERT') THEN
                PERFORM dblink_exec('raid_server','INSERT INTO empleado VALUES ('|| NEW.cedula ||','|| '''' || NEW.nombre || '''' ||','|| '''' || NEW.papellido || '''' ||','|| '''' || NEW.sapellido || '''' ||','|| '''' || NEW.username || '''' || ','|| '''' || NEW.password || '''' || ','|| '''' || NEW.email || '''' || ','|| NEW.estado ||');');
                RETURN NEW;
            END IF;
        ELSEIF (TG_TABLE_NAME = 'caja') THEN
            IF (TG_OP = 'INSERT') THEN
                PERFORM dblink_exec('raid_server','INSERT INTO caja VALUES ('|| NEW.idcaja ||','|| NEW.idsucursal ||','|| '''' || NEW.fecha || '''' ||','|| NEW.idempleado ||','|| NEW.efectivo || ','|| NEW.tipo ||');');
                RETURN NEW;
            END IF;
        ELSEIF (TG_TABLE_NAME = 'cajasxsucursal') THEN
            IF (TG_OP = 'INSERT') THEN
                PERFORM dblink_exec('raid_server','INSERT INTO cajasxsucursal VALUES ('|| NEW.idcaja ||','|| NEW.idsucursal ||');');
                RETURN NEW;
            END IF;
        ELSEIF (TG_TABLE_NAME = 'venta') THEN
            IF (TG_OP = 'INSERT') THEN
                PERFORM dblink_exec('raid_server','INSERT INTO venta (idventa,idcliente,tpago,idsucursal,fecha) VALUES ('|| NEW.idventa || ','|| NEW.idcliente ||','|| NEW.tpago ||','|| NEW.idsucursal ||','|| '''' || NEW.fecha || '''' ||');');
                RETURN NEW;
            END IF;
        ELSEIF (TG_TABLE_NAME = 'detalleventa') THEN
            IF (TG_OP = 'INSERT') THEN
                PERFORM dblink_exec('raid_server','INSERT INTO detalleventa VALUES ('|| NEW.idventa ||','|| NEW.idproducto ||','|| NEW.cantidad ||');');
                RETURN NEW;
            END IF;
        ELSEIF (TG_TABLE_NAME = 'empleadosxsucursal') THEN
            IF (TG_OP = 'INSERT') THEN
                PERFORM dblink_exec('raid_server','INSERT INTO empleadosxsucursal VALUES ('|| NEW.idempleado ||','|| NEW.idsucursal ||','|| NEW.idrol ||');');
                RETURN NEW;
            END IF;
        ELSEIF (TG_TABLE_NAME = 'empresa') THEN
            IF (TG_OP = 'INSERT') THEN
                PERFORM dblink_exec('raid_server','INSERT INTO empresa VALUES ('|| NEW.idempresa ||','|| '''' || NEW.nombre || '''' ||');');
                RETURN NEW;
            END IF;
        ELSEIF (TG_TABLE_NAME = 'productos') THEN
            IF (TG_OP = 'INSERT') THEN
                PERFORM dblink_exec('raid_server','INSERT INTO productos VALUES ('|| NEW.ean ||','|| NEW.idproveedor ||','|| '''' || NEW.nombre || '''' ||','|| '''' || NEW.imagen || '''' ||','|| '''' || NEW.descripcion || '''' ||','|| NEW.estado||');');
                RETURN NEW;
            ELSEIF (TG_OP = 'UPDATE') THEN
            	PERFORM dblink_exec('raid_server','UPDATE productos SET idproveedor='|| NEW.idproveedor ||',nombre='|| '''' || NEW.nombre || '''' ||',imagen='|| '''' || NEW.imagen || '''' ||',descripcion='|| '''' || NEW.descripcion || '''' ||',estado='|| NEW.estado||' WHERE ean='|| OLD.ean ||';');
                RETURN NEW;
            END IF;
        ELSEIF (TG_TABLE_NAME = 'productosxsucursal') THEN
            IF (TG_OP = 'INSERT') THEN
                PERFORM dblink_exec('raid_server','INSERT INTO productosxsucursal VALUES ('|| NEW.idsucursal ||','|| NEW.idproducto ||','|| NEW.cantidad ||','|| NEW.precio ||');');
                RETURN NEW;
            ELSEIF (TG_OP = 'UPDATE') THEN
            	PERFORM dblink_exec('raid_server','UPDATE productosxsucursal SET idsucursal='|| NEW.idsucursal ||',idproducto='|| NEW.idproducto ||',cantidad='|| NEW.cantidad ||',precio='|| NEW.precio ||' WHERE idsucursal='|| OLD.idsucursal ||' AND idproducto='|| OLD.idproducto ||';');
                RETURN NEW;
            END IF;
        ELSEIF (TG_TABLE_NAME = 'proveedores') THEN
            IF (TG_OP = 'INSERT') THEN
                PERFORM dblink_exec('raid_server','INSERT INTO proveedores VALUES ('|| NEW.idproveedor ||','|| '''' || NEW.nombre || '''' ||','|| NEW.telefono ||','|| NEW.estado ||');');
                RETURN NEW;
            ELSEIF (TG_OP = 'UPDATE') THEN
            	PERFORM dblink_exec('raid_server','UPDATE proveedores SET idproveedor='|| NEW.idproveedor ||',nombre='|| '''' || NEW.nombre || '''' ||',telefono='|| NEW.telefono ||',estado='|| NEW.estado ||' WHERE idproveedor='|| OLD.idproveedor ||';');
                RETURN NEW;
            END IF;
        ELSEIF (TG_TABLE_NAME = 'roles') THEN
            IF (TG_OP = 'INSERT') THEN
                PERFORM dblink_exec('raid_server','INSERT INTO roles VALUES ('|| NEW.idrol ||','|| '''' || NEW.nombre || '''' ||','|| '''' || NEW.descripcion || '''' ||','|| NEW.estado ||');');
                RETURN NEW;
            ELSEIF (TG_OP = 'UPDATE') THEN
            	PERFORM dblink_exec('raid_server','UPDATE roles SET idrol='|| NEW.idrol ||',nombre='|| '''' || NEW.nombre || '''' ||',descripcion='|| '''' || NEW.descripcion || '''' ||',estado='|| NEW.estado ||' WHERE idrol='|| OLD.idrol ||';');
                RETURN NEW;
            END IF;
        ELSEIF (TG_TABLE_NAME = 'sucursales') THEN
            IF (TG_OP = 'INSERT') THEN
                PERFORM dblink_exec('raid_server','INSERT INTO sucursales VALUES ('|| NEW.idsucursal ||','|| NEW.idempresa ||','|| '''' || NEW.nombre || '''' ||','|| '''' || NEW.direccion || '''' ||','|| '''' ||NEW.imagen || '''' ||',' || NEW.estado ||');');
                RETURN NEW;
            ELSEIF (TG_OP = 'UPDATE') THEN
            	PERFORM dblink_exec('raid_server','UPDATE sucursales SET idempresa='|| NEW.idempresa ||',nombre='|| '''' || NEW.nombre || '''' ||',direccion='|| '''' || NEW.direccion || '''' ||',imagen='|| '''' || NEW.imagen|| '''' ||',estado='|| NEW.estado ||' WHERE idsucursal='|| OLD.idsucursal ||';');
                RETURN NEW;
            END IF;
        ELSEIF (TG_TABLE_NAME = 'tipopago') THEN
            IF (TG_OP = 'INSERT') THEN
                PERFORM dblink_exec('raid_server','INSERT INTO tipopago VALUES ('|| NEW.idtipo ||','|| '''' || NEW.nombre || '''' ||','|| '''' || NEW.descripcion || '''' ||');');
                RETURN NEW;
            END IF;
        ELSEIF (TG_TABLE_NAME = 'ventasxempleado') THEN
            IF (TG_OP = 'INSERT') THEN
                PERFORM dblink_exec('raid_server','INSERT INTO ventasxempleado VALUES ('|| NEW.idventa ||','|| NEW.idempleado ||');');
                RETURN NEW;
            END IF;
        ELSEIF (TG_TABLE_NAME = 'ventasxsucursal') THEN
            IF (TG_OP = 'INSERT') THEN
                PERFORM dblink_exec('raid_server','INSERT INTO ventasxsucursal VALUES ('|| NEW.idsucursal ||','|| NEW.idventa ||');');
                RETURN NEW;
            END IF;
        END IF;
        RETURN NULL; -- result is ignored since this is an AFTER trigger
    END;
$raid$ LANGUAGE plpgsql;

/*CREATE TRIGGER raid
BEFORE INSERT OR UPDATE OR DELETE ON cliente
    FOR EACH ROW EXECUTE PROCEDURE process_raid();
CREATE TRIGGER raid_empleado
BEFORE INSERT OR UPDATE OR DELETE ON empleado
    FOR EACH ROW EXECUTE PROCEDURE process_raid(); 
CREATE TRIGGER raid_caja
BEFORE INSERT OR UPDATE OR DELETE ON caja
    FOR EACH ROW EXECUTE PROCEDURE process_raid();
CREATE TRIGGER raid_cajasxsucursal
BEFORE INSERT OR UPDATE OR DELETE ON cajasxsucursal
    FOR EACH ROW EXECUTE PROCEDURE process_raid();
CREATE TRIGGER raid_detalleventa
BEFORE INSERT OR UPDATE OR DELETE ON detalleventa
    FOR EACH ROW EXECUTE PROCEDURE process_raid();
CREATE TRIGGER raid_venta
BEFORE INSERT OR UPDATE OR DELETE ON venta
    FOR EACH ROW EXECUTE PROCEDURE process_raid();
CREATE TRIGGER raid_empleadosxsucursal
BEFORE INSERT OR UPDATE OR DELETE ON empleadosxsucursal
    FOR EACH ROW EXECUTE PROCEDURE process_raid();
CREATE TRIGGER raid_empresa
BEFORE INSERT OR UPDATE OR DELETE ON empresa
    FOR EACH ROW EXECUTE PROCEDURE process_raid();
CREATE TRIGGER raid_productos
BEFORE INSERT OR UPDATE OR DELETE ON productos
    FOR EACH ROW EXECUTE PROCEDURE process_raid();
CREATE TRIGGER raid_productosxsucursal
BEFORE INSERT OR UPDATE OR DELETE ON productosxsucursal
    FOR EACH ROW EXECUTE PROCEDURE process_raid();
CREATE TRIGGER raid_proveedores
BEFORE INSERT OR UPDATE OR DELETE ON proveedores
    FOR EACH ROW EXECUTE PROCEDURE process_raid();
CREATE TRIGGER raid_roles
BEFORE INSERT OR UPDATE OR DELETE ON roles
    FOR EACH ROW EXECUTE PROCEDURE process_raid();
CREATE TRIGGER raid_sucursales
BEFORE INSERT OR UPDATE OR DELETE ON sucursales
    FOR EACH ROW EXECUTE PROCEDURE process_raid();
CREATE TRIGGER raid_tipopago
BEFORE INSERT OR UPDATE OR DELETE ON tipopago
    FOR EACH ROW EXECUTE PROCEDURE process_raid();
CREATE TRIGGER raid_ventasxempleado
BEFORE INSERT OR UPDATE OR DELETE ON ventasxempleado
    FOR EACH ROW EXECUTE PROCEDURE process_raid();
CREATE TRIGGER raid_ventasxsucursal
BEFORE INSERT OR UPDATE OR DELETE ON ventasxsucursal
    FOR EACH ROW EXECUTE PROCEDURE process_raid();
*/



