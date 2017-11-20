using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RESTFUL.Models
{
    public class EmployeeInfo
    {
        public int cedula { get; set; }
        public string email { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string nombre { get; set; }
        public string papellido { get; set; }
        public string sapellido { get; set; }
        public int idrol { get; set; }
        public int idsucursal { get; set; }
        public int estado { get; set; }

    }
}