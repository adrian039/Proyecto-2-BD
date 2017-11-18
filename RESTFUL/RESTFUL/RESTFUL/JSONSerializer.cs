﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Diagnostics;
using Npgsql;

namespace RESTFUL
{
    public class JSONSerializer
    {
        public IEnumerable<Dictionary<string, object>> Serialize(NpgsqlDataReader reader)
        {
            var results = new List<Dictionary<string, object>>();
            var cols = new List<string>();
            for (var i = 0; i < reader.FieldCount; i++)
                cols.Add(reader.GetName(i));

            while (reader.Read())
            {

                results.Add(SerializeRow(cols, reader));
            }
            return results;
        }
        public Dictionary<string, object> singleserialize(NpgsqlDataReader reader)
        {
            var results = new List<Dictionary<string, object>>();
            var cols = new List<string>();
            for (var i = 0; i < reader.FieldCount; i++)
            {
                cols.Add(reader.GetName(i));
            }
            reader.Read();
            return SerializeRow(cols, reader);
        }
        private Dictionary<string, object> SerializeRow(IEnumerable<string> cols,
                                                        NpgsqlDataReader reader)
        {
            var result = new Dictionary<string, object>();
            foreach (var col in cols)
            {
                result.Add(col, reader[col]);
            }
            return result;
        }
    }
}