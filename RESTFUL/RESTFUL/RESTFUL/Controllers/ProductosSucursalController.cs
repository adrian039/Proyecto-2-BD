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
                using (gspEntity entities = new gspEntity())
                {
                    entities.Configuration.LazyLoadingEnabled = false;
                    var resp = entities.productosxsucursals.FirstOrDefault(e => (e.idproducto == prod.idproducto) && (e.idsucursal == prod.idsucursal)
                     && (e.cantidad >= prod.cantidad));
                    if (resp != null)
                    {
                        var product = entities.productos.FirstOrDefault(e => e.ean == prod.idproducto);
                        return product;
                    }
                    else
                    {
                        return null;
                    }
                }

            } catch (Exception ex)
            {
                return null;
            }
        }

        [HttpGet]
        public IEnumerable<ProductosModel> getProductos([FromUri] int idSucursal)
        {
            try
            {
                using (gspEntity entities = new gspEntity())
                {
                    entities.Configuration.LazyLoadingEnabled = false;
                    var entity = entities.productosxsucursals.Join(entities.productos, c => c.idproducto, cm => cm.ean, (c, cm) => new ProductosModel
                    {
                        ean = cm.ean,
                        idproveedor = cm.idproveedor,
                        nombre = cm.nombre,
                        imagen = cm.imagen,
                        descripcion = cm.descripcion,
                        Sucursal = c.idsucursal
                    }).Where(e => e.Sucursal == idSucursal);
                    if (entity != null)
                    {
                        return entity.ToList();
                    }
                    else
                    {
                        return null;
                    }
                }

            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [HttpDelete]
        public HttpResponseMessage deleteProduct(int prod, int suc)
        {
            try
            {
                using (gspEntity entities = new gspEntity())
                {
                    entities.Configuration.LazyLoadingEnabled = false;
                    var entity = entities.productosxsucursals.FirstOrDefault(e => e.idproducto == prod && e.idsucursal==suc);
                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Error eliminando producto");
                    }
                    else
                    {
                        entity.estado = 0;
                        entities.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK, "Producto Deleted");
                    }

                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

    }
}
