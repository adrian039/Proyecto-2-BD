using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using RESTFUL.Models;

namespace RESTFUL.Controllers
{
    public class ProductosSucursalController : ApiController
    {
        

       [HttpPost]
        public producto hayDisponible([FromBody]productosxsucursal prod)
        {
            try
            {
                using (gspEntity entities= new gspEntity())
                {
                    entities.Configuration.LazyLoadingEnabled = false;
                    var resp = entities.productosxsucursals.FirstOrDefault(e => (e.idproducto == prod.idproducto) && (e.idsucursal == prod.idsucursal)
                     && (e.cantidad >= prod.cantidad));
                    if (resp!=null)
                    {
                        var product = entities.productos.FirstOrDefault(e => e.ean == prod.idproducto);
                        return  product;
                    }
                    else
                    {
                        return null;
                    }
                }

            }catch (Exception ex)
            {
                return null;
            }
        }

    }
}
