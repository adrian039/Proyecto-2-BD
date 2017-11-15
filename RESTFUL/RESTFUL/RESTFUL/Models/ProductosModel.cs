using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RESTFUL.Models
{
    public class ProductosModel
    {
        public int ean { get; set; }
        public int idproveedor { get; set; }
        public int estado { get; set; }
        public string nombre { get; set; }
        public string imagen { get; set; }
        public string descripcion { get; set; }
        public int Sucursal { get; set; }
    }
}