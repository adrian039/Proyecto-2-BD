using Npgsql;
using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RESTFUL.Controllers
{
    public class ReportesController : ApiController
    {
        JSONSerializer serial = new JSONSerializer();

        [HttpGet]
        public HttpResponseMessage topSalesByDate(string date1, string date2)
        {
            try
            {
                using (gspEntity entities= new gspEntity())
                {
                   NpgsqlConnection conn = new NpgsqlConnection(entities.Database.Connection.ConnectionString);
                    conn.Open();
                    using (NpgsqlCommand com = new NpgsqlCommand("BEGIN; SELECT * FROM topsalesbydate(:P0, :P1); COMMIT;", conn))
                    {
                        com.Parameters.Add(new NpgsqlParameter("P0", NpgsqlDbType.Date));
                        com.Parameters.Add(new NpgsqlParameter("P1", NpgsqlDbType.Date));
                        com.Prepare();
                        com.Parameters[0].Value = date1;
                        com.Parameters[1].Value = date2;
                        using (NpgsqlDataReader dr = com.ExecuteReader())
                        {
                            var result = serial.Serialize(dr);
                            conn.Close();
                            return Request.CreateResponse(HttpStatusCode.OK, result);
                        }

                    };
                  
                }

            }catch(Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }


        }
    }
}
