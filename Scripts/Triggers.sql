CREATE OR REPLACE FUNCTION process_raid() RETURNS TRIGGER AS $raid$
    BEGIN
    	IF (TG_TABLE_NAME = 'cliente') THEN
            IF (TG_OP = 'DELETE') THEN
                RETURN OLD;
            ELSIF (TG_OP = 'UPDATE') THEN
                RETURN NEW;
            ELSIF (TG_OP = 'INSERT') THEN
                PERFORM dblink_exec('raid_server','INSERT INTO cliente VALUES ('|| NEW.cedula ||','|| '''' || NEW.nombre || '''' ||','|| '''' || NEW.papellido || '''' ||','|| '''' || NEW.sapellido || '''' ||','|| NEW.estado ||');');
                RETURN NEW;
            END IF;
        END IF;
        RETURN NULL; -- result is ignored since this is an AFTER trigger
    END;
$raid$ LANGUAGE plpgsql;

CREATE TRIGGER raid
BEFORE INSERT OR UPDATE OR DELETE ON cliente
    FOR EACH ROW EXECUTE PROCEDURE process_raid();