using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RESTFUL_API.Models
{
    public class ventasModel
    {
        public int idCliente { get; set; }
        public int idEmpleado { get; set; }
        public JArray productos { get; set; }
        public int idSucursal { get; set; }
        public int tipoPago { get; set; }
        public string fecha { get; set; }
    }
}