using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RESTFUL.Models
{
    public class regVentaModel
    {
        public int idCliente { get; set; }
        public int idEmpleado { get; set; }
       public JArray productos { get; set; }
       public int idSucursal {get; set;}
       public int tipoPago { get; set; }
        public System.DateTime fecha { get; set; }
        public System.TimeSpan starts { get; set; }
        public System.TimeSpan ends { get; set; }
    }
}