using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RESTFUL_API.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RESTFUL_API.Controllers
{
    public class EstadosPedidoController : ApiController
    {
        JSONSerializer serial = new JSONSerializer();
        string DatabaseConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["GasStationPharmacyDB"].ConnectionString;


        [HttpPost]
        public HttpResponseMessage updateEstadoPedido(pedidosModel EstadoPedido)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(DatabaseConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("UPDATE PEDIDOS SET Estado=@estado WHERE idPedido=@id", conn);
                    cmd.Parameters.AddWithValue("@id", EstadoPedido.idPedido);
                    cmd.Parameters.AddWithValue("@estado", EstadoPedido.Estado);
                    cmd.Connection = conn;
                    conn.Open();
                    cmd.ExecuteReader();
                    var message = Request.CreateResponse(HttpStatusCode.Created, EstadoPedido);
                    if (EstadoPedido.Estado == 3)
                    {
                        return regVentaToApi(EstadoPedido.idPedido, EstadoPedido.sucursalRecojo.Value, EstadoPedido.idCliente.Value);
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, EstadoPedido);
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
                            return Request.CreateResponse(HttpStatusCode.OK, result.Replace("\"", ""));
                        }

                    }


                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
    }
}
