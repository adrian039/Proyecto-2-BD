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
        "Port=5432; Database=gasstationpharmacy; User Id=rsolano10@postgreserver-20170401; Password=BasesDatosak7;";

        [HttpGet]
        public string getAll()
        {
            using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
            {
                /*// Connect to the database
                conn.Open();

                // Read rows
                NpgsqlCommand selectCommand = new NpgsqlCommand("SELECT * FROM MyTable", conn);
                NpgsqlDataReader results = selectCommand.ExecuteReader();

                // Enumerate over the rows
                while (results.Read())
                {
                    Console.WriteLine("Column 0: {0} Column 1: {1}", results[0], results[1]);
                   
                }*/
                return "ok";
            }
        }
    }
}
