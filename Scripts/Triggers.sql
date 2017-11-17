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
                PERFORM dblink_exec('raid_server','INSERT INTO venta VALUES ('|| NEW.idcliente ||','|| NEW.tpago ||','|| NEW.idsucursal ||','|| '''' || NEW.fecha || '''' ||');');
                RETURN NEW;
            END IF;
        ELSEIF (TG_TABLE_NAME = 'detalleventa') THEN
            IF (TG_OP = 'INSERT') THEN
                PERFORM dblink_exec('raid_server','INSERT INTO detalleventa VALUES ('|| NEW.idventa ||','|| NEW.idproducto ||','|| NEW.cantidad ||');');
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
*/


