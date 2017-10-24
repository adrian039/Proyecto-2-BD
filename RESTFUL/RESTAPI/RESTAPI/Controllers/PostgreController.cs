using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Npgsql;
using System.Data.SqlClient;

namespace RESTAPI.Controllers
{
    public class PostgreController : ApiController
    {
        string connectionString = "Server=postgreserver-20170401.postgres.database.azure.com; " +
        "Port=5432; SSL Mode=Require;Trust Server Certificate=true; Database=gasstationpharmacy; User Id=rsolano10@postgreserver-20170401; Password=BasesDatosak7;";
        JSONSerializer serialize = new JSONSerializer();

        [HttpGet]
        public IEnumerable<Dictionary<string, object>> getAll()
        {
            using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
            {
                // Connect to the database
                conn.Open();

                // Read rows
                NpgsqlCommand selectCommand = new NpgsqlCommand("SELECT * FROM cliente", conn);
                NpgsqlDataReader results = selectCommand.ExecuteReader();

                return serialize.Serialize(results);
                
            }
        }
    }
}
