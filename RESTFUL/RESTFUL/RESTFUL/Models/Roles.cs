using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RESTFUL.Models
{
    public class Roles
    {
        public int idrol { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public int idsucursal { get; set; }
    }
}