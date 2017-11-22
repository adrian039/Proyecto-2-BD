using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using RESTFUL_API.Models;
using System.Data.SqlClient;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Text;

namespace RESTFUL_API.Controllers
{
    public class PedidosController : ApiController
    {
        private static readonly HttpClient client = new HttpClient();
        JSONSerializer serial = new JSONSerializer();
        string DatabaseConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["GasStationPharmacyDB"].ConnectionString;

        [HttpGet]
        public IEnumerable<Dictionary<string, object>> getAll()
        {
            using (SqlConnection conn = new SqlConnection(DatabaseConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT idPedido,sucursalRecojo,idCliente,horaRecojo,Telefono,Imagen,Estado FROM PEDIDOS", conn);
                cmd.Connection = conn;
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    var r = serial.Serialize(reader);
                    conn.Close();
                    return r;
                }

            }
        }

        [HttpGet]
        public HttpResponseMessage getPedidosbyidPedido(int idPedido)
        {
            using (SqlConnection conn = new SqlConnection(DatabaseConnectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("SELECT idPedido,sucursalRecojo,idCliente,horaRecojo,Telefono,Imagen,Estado FROM PEDIDOS WHERE idPedido=@id", conn);
                    cmd.Parameters.AddWithValue("@id", idPedido);
                    cmd.Connection = conn;
                    conn.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        var r = serial.singleserialize(reader);
                        var message = Request.CreateResponse(HttpStatusCode.Accepted, r);
                        conn.Close();
                        return message;
                    }
                }catch(Exception ex)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, ex);
                }

            }
        }

        [HttpGet]
        public HttpResponseMessage getTotalPedidos(int idEmpresa)
        {

            using (SqlConnection conn = new SqlConnection(DatabaseConnectionString))
            {

                SqlCommand cmd = new SqlCommand("EXEC GETCOUNTPEDIDOS @id", conn);
                cmd.Parameters.AddWithValue("@id", idEmpresa);
                cmd.Connection = conn;
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    var r = serial.singleserialize(reader);
                    conn.Close();
                    return Request.CreateResponse(HttpStatusCode.Created, r);
                }
            }

        }

        [HttpPost]
        public HttpResponseMessage regPedido([FromBody] pedidosModel pedido)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(DatabaseConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("INSERT INTO PEDIDOS(sucursalRecojo,idCliente,horaRecojo,Telefono,Imagen,Estado) OUTPUT INSERTED.idPedido VALUES (@sucursal,@cliente,@hora,@telefono,@imagen,@estado)", conn);
                    cmd.Parameters.AddWithValue("@sucursal", pedido.sucursalRecojo);
                    cmd.Parameters.AddWithValue("@cliente", pedido.idCliente);
                    cmd.Parameters.AddWithValue("@hora", pedido.horaRecojo);
                    cmd.Parameters.AddWithValue("@telefono", pedido.Telefono);
                    cmd.Parameters.AddWithValue("@imagen", pedido.Imagen);
                    cmd.Parameters.AddWithValue("@estado", pedido.Estado);
                    cmd.Connection = conn;
                    conn.Open();
                    var message = Request.CreateResponse(HttpStatusCode.Created, serial.singleserialize(cmd.ExecuteReader()));
                    conn.Close();
                    return message;
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
        [HttpDelete]
        public HttpResponseMessage deletePedido([FromUri] int id)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(DatabaseConnectionString))
                {
                    // SqlCommand cmd = new SqlCommand("UPDATE  PEDIDOS SET  Estado=5 WHERE idPedido=@id", conn);
                    SqlCommand cmd = new SqlCommand("EXEC DELETEPEDIDO @id", conn);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Connection = conn;
                    conn.Open();
                    cmd.ExecuteReader();
                    var message = Request.CreateResponse(HttpStatusCode.Created, id);
                    return message;
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpGet]
        public IEnumerable<Dictionary<string, object>> getPedidosbyId([FromUri] int id)
        {
            using (SqlConnection conn = new SqlConnection(DatabaseConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT idPedido,sucursalRecojo,idCliente,horaRecojo,Telefono,Imagen,Estado FROM PEDIDOS WHERE idCliente=@id AND Estado!=5", conn);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Connection = conn;
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    var r = serial.Serialize(reader);
                    conn.Close();
                    return r;
                }

            }
        }
        [HttpGet]
        public IEnumerable<Dictionary<string, object>> getPedidosbyIdSucursal([FromUri] int idSucursal)
        {
            using (SqlConnection conn = new SqlConnection(DatabaseConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT idPedido,sucursalRecojo,idCliente,horaRecojo,Telefono,Imagen,Estado FROM PEDIDOS WHERE sucursalRecojo=@id AND Estado!=5", conn);
                cmd.Parameters.AddWithValue("@id", idSucursal);
                cmd.Connection = conn;
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    var r = serial.Serialize(reader);
                    conn.Close();
                    return r;
                }

            }
        }

        [HttpGet]
        public IEnumerable<Dictionary<string, object>> getInfoPedidosbyIdSucursal([FromUri] int idSuc)
        {
            using (SqlConnection conn = new SqlConnection(DatabaseConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT CLIENTE.Penalizacion AS penalizacionCliente, PEDIDOS.idPedido, PEDIDOS.sucursalRecojo, PEDIDOS.idCliente, PEDIDOS.horaRecojo,"+
                " PEDIDOS.Telefono, PEDIDOS.Imagen, PEDIDOS.Estado FROM PEDIDOS INNER JOIN CLIENTE ON CLIENTE.Cedula = PEDIDOS.idCliente WHERE PEDIDOS.sucursalRecojo = @id", conn);
                cmd.Parameters.AddWithValue("@id", idSuc);
                cmd.Connection = conn;
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    var r = serial.Serialize(reader);
                    conn.Close();
                    return r;
                }

            }
        }

        [HttpPut]
        public HttpResponseMessage updatePedido([FromBody]pedidosModel pedido)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(DatabaseConnectionString))
                {
                     SqlCommand cmd = new SqlCommand("UPDATE PEDIDOS SET sucursalRecojo=@sucursal, idCliente=@cliente, horaRecojo=@hora, Telefono=@telefono, Imagen=@imagen, Estado=@estado WHERE idPedido=@id", conn);
                     cmd.Parameters.AddWithValue("@id", pedido.idPedido);
                     cmd.Parameters.AddWithValue("@sucursal", pedido.sucursalRecojo);
                     cmd.Parameters.AddWithValue("@cliente", pedido.idCliente);
                     cmd.Parameters.AddWithValue("@hora", pedido.horaRecojo);
                     cmd.Parameters.AddWithValue("@telefono", pedido.Telefono);
                     cmd.Parameters.AddWithValue("@imagen", pedido.Imagen);
                     cmd.Parameters.AddWithValue("@estado", pedido.Estado);
                     cmd.Connection = conn;
                     conn.Open();
                     cmd.ExecuteReader();
                     var message = Request.CreateResponse(HttpStatusCode.Created, pedido);
                    if (pedido.Estado == 3)
                    {
                        return regVentaToApi(pedido.idPedido, pedido.sucursalRecojo.Value, pedido.idCliente.Value);
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, pedido);
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        private HttpResponseMessage regVentaToApi(int venta, int suc, int cliente)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(DatabaseConnectionString))
                {
                    SqlCommand cmd1 = new SqlCommand("SELECT idProducto AS ean, Cantidad AS cantidad FROM DETALLEPEDIDO WHERE idPedido=@id");
                    cmd1.Parameters.AddWithValue("@id", venta);
                    cmd1.Connection = conn;
                    conn.Open();
                    using (var reader = cmd1.ExecuteReader())
                    {
                       var r = serial.Serialize(reader);
                       string JSONresult;
                        JSONresult = JsonConvert.SerializeObject(r);
                       string jsonText = JSONresult.Replace("\"", "");
                        string sqlFormattedDate = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.fff");
                        JObject ven = new JObject();
                        ven.Add("idCliente", cliente);
                        ven.Add("idEmpleado", 0);
                        ven.Add("idSucursal", suc);
                        ven.Add("productos", JArray.Parse(JSONresult));
                        ven.Add("tipoPago", 1);
                        ven.Add("fecha", sqlFormattedDate);
                        ven.Add("starts", "00:00:00");
                        ven.Add("ends", "00:00:00");
                        ven.Add("idcaja", 0);
                        var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://gsprest.azurewebsites.net/api/Ventas/");
                        httpWebRequest.ContentType = "application/json";
                        httpWebRequest.Method = "POST";

                        using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                        {
                            streamWriter.Write(ven);
                            streamWriter.Flush();
                            streamWriter.Close();
                        }

                        var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                       using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                        {
                            var result = streamReader.ReadToEnd();
                            return Request.CreateResponse(HttpStatusCode.OK, result.Replace("\"",""));
                        }

                    }

                     
                }
            }catch(Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        
    }

}
