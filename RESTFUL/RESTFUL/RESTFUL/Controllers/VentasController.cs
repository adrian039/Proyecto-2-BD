﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using RESTFUL.Models;
using System.Data.SqlClient;
using Npgsql;
using NpgsqlTypes;

namespace RESTFUL.Controllers
{
    public class VentasController : ApiController
    {
        JSONSerializer serial = new JSONSerializer();
        [HttpGet]
        public IEnumerable<venta> getVentas([FromUri] System.DateTime fecha, int idsuc, int idcaja)
        {
            try
            {
                using (var entities = new gspEntity())
                {
                    entities.Configuration.LazyLoadingEnabled = false;
                    return entities.ventas.Where(x => (x.fecha == fecha) && (x.idsucursal==idsuc) && (x.idcaja==idcaja)).ToList();
                }

            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [HttpGet]
        public HttpResponseMessage getSucursalSales(int idsuc)
        {
            try
            {
                using (gspEntity entities = new gspEntity())
                {
                    NpgsqlConnection conn = new NpgsqlConnection(entities.Database.Connection.ConnectionString);
                    conn.Open();
                    using (NpgsqlCommand com = new NpgsqlCommand("BEGIN; SELECT * FROM getsucursalsales(:P0); COMMIT;", conn))
                    {
                        com.Parameters.Add(new NpgsqlParameter("P0", NpgsqlDbType.Integer));
                        com.Prepare();
                        com.Parameters[0].Value = idsuc;
                        using (NpgsqlDataReader dr = com.ExecuteReader())
                        {
                            var result = serial.Serialize(dr);
                            conn.Close();
                            return Request.CreateResponse(HttpStatusCode.OK, result);
                        }

                    };

                }

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpPost]
        public string regVenta([FromBody]regVentaModel venta)
        {
            string message="null";
            try
            {
                using (gspEntity entities = new gspEntity())
                {
                    NpgsqlConnection conn = new NpgsqlConnection(entities.Database.Connection.ConnectionString);
                    conn.Open();
                    using (NpgsqlCommand com = new NpgsqlCommand("BEGIN; SELECT * FROM regventa(:P0, :P1, :P2, :P3, :P4, :P5, :P6, :P7, :P8); COMMIT;", conn))
                    {
                        com.Parameters.Add(new NpgsqlParameter("P0", NpgsqlDbType.Integer));
                        com.Parameters.Add(new NpgsqlParameter("P1", NpgsqlDbType.Integer));
                        com.Parameters.Add(new NpgsqlParameter("P2", NpgsqlDbType.Text));
                        com.Parameters.Add(new NpgsqlParameter("P3", NpgsqlDbType.Integer));
                        com.Parameters.Add(new NpgsqlParameter("P4", NpgsqlDbType.Integer));
                        com.Parameters.Add(new NpgsqlParameter("P5", NpgsqlDbType.Timestamp));
                        com.Parameters.Add(new NpgsqlParameter("P6", NpgsqlDbType.Time));
                        com.Parameters.Add(new NpgsqlParameter("P7", NpgsqlDbType.Time));
                        com.Parameters.Add(new NpgsqlParameter("P8", NpgsqlDbType.Integer));
                        com.Prepare();
                        com.Parameters[0].Value = venta.idCliente;
                        com.Parameters[1].Value = venta.idEmpleado;
                        com.Parameters[2].Value = venta.productos.ToString();
                        com.Parameters[3].Value = venta.idSucursal;
                        com.Parameters[4].Value = venta.tipoPago;
                        com.Parameters[5].Value = venta.fecha;
                        com.Parameters[6].Value = venta.starts;
                        com.Parameters[7].Value = venta.ends;
                        com.Parameters[8].Value = venta.idcaja;
                        using (NpgsqlDataReader dr = com.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                message = dr.GetString(0);
                            }
                        }
                        
                    };
                    conn.Close();
                    return message;
                }
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }
    }
}
