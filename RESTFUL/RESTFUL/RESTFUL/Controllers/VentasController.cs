using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using RESTFUL.Models;
using System.Data.SqlClient;
using Npgsql;
using NpgsqlTypes;
using Newtonsoft.Json.Linq;

namespace RESTFUL.Controllers
{
    public class VentasController : ApiController
    {
        [HttpGet]
        public IEnumerable<venta> getVentas([FromUri] System.DateTime fecha)
        {
            try
            {
                using (var entities = new gspEntity())
                {
                    entities.Configuration.LazyLoadingEnabled = false;
                    return entities.ventas.Where(x => x.fecha == fecha).ToList();
                }

            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [HttpPost]
        public string regVenta([FromBody]regVentaModel venta)
        {
            string message="null";
            try
            {
                using (gspEntity entities = new gspEntity())
                {/*
                    NpgsqlConnection conn = new NpgsqlConnection(entities.Database.Connection.ConnectionString);
                    conn.Open();
                    using (NpgsqlCommand com = new NpgsqlCommand("BEGIN; SELECT * FROM regventa(:P0, :P1, :P2, :P3, :P4, :P5); COMMIT;", conn))
                    {
                        com.Parameters.Add(new NpgsqlParameter("P0", NpgsqlDbType.Integer));
                        com.Parameters.Add(new NpgsqlParameter("P1", NpgsqlDbType.Integer));
                        com.Parameters.Add(new NpgsqlParameter("P2", NpgsqlDbType.Text));
                        com.Parameters.Add(new NpgsqlParameter("P3", NpgsqlDbType.Integer));
                        com.Parameters.Add(new NpgsqlParameter("P4", NpgsqlDbType.Integer));
                        com.Parameters.Add(new NpgsqlParameter("P5", NpgsqlDbType.Timestamp));
                        com.Prepare();
                        com.Parameters[0].Value = venta.idCliente;
                        com.Parameters[1].Value = venta.idEmpleado;
                        com.Parameters[2].Value = venta.productos.ToString();
                        com.Parameters[3].Value = venta.idSucursal;
                        com.Parameters[4].Value = venta.tipoPago;
                        com.Parameters[5].Value = venta.fecha;
                        using (NpgsqlDataReader dr = com.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                message = dr.GetString(0);
                            }
                        }
                        
                    };
                    conn.Close();
                    return message;*/
                 //return entities.regventa(venta.idCliente, venta.idCliente, venta.productos.ToString(), venta.idSucursal, venta.tipoPago, venta.fecha).ToString();
                    return "hola";
                }
            }
            catch (Exception ex)
            {
                return message;
            }
        }
    }
}
