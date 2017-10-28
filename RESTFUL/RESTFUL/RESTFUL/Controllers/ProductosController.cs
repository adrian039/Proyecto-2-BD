using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RESTFUL.Controllers
{
    public class ProductosController : ApiController
    {
        [HttpGet]
        public IEnumerable<producto> getAll()
        {
            using (gspEntities entities = new gspEntities())
            {
                entities.Configuration.LazyLoadingEnabled = false;
                return entities.productos.ToList();
            }
        }
        [HttpGet]
        public producto getbyid(int id)
        {
            try
            {
                using (gspEntities entities = new gspEntities())
                {
                    entities.Configuration.LazyLoadingEnabled = false;
                    var entity = entities.productos.FirstOrDefault(e => e.ean == id);
                    if (entity == null)
                    {
                        return null;
                    }
                    else
                    {
                        return entity;
                    }

                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [HttpPost]
        public HttpResponseMessage Post([FromBody] producto producto)
        {
            try
            {
                using (gspEntities entities = new gspEntities())
                {

                    entities.Configuration.LazyLoadingEnabled = false;
                    entities.productos.Add(producto);
                    entities.SaveChanges();
                    var message = Request.CreateResponse(HttpStatusCode.Created, producto);
                    return message;
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
        [HttpPut]
        public HttpResponseMessage Put(int id, [FromBody]producto producto)
        {
            try
            {
                using (gspEntities entities = new gspEntities())
                {
                    entities.Configuration.LazyLoadingEnabled = false;
                    var entity = entities.productos.FirstOrDefault(e => e.ean == id);
                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "producto con ID: " + id.ToString() + ", no encontrado.");
                    }
                    else
                    {
                        entity.descripcion = producto.descripcion;
                        entity.idproveedor = producto.idproveedor;
                        entity.imagen = producto.imagen;
                        entity.nombre = producto.nombre;
                        entities.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK, entity);
                    }

                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
        [HttpDelete]
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                using (gspEntities entities = new gspEntities())
                {
                    entities.Configuration.LazyLoadingEnabled = false;
                    var entity = entities.productos.FirstOrDefault(e => e.ean == id);
                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "prodcuto con ID: " + id.ToString() + ", no encontrado.");
                    }
                    else
                    {
                        entities.productos.Remove(entity);
                        entities.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK, "producto Deleted");
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
