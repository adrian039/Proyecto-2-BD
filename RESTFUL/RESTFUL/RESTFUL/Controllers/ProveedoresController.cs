using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RESTFUL.Controllers
{
    public class ProveedoresController : ApiController
    {
        [HttpGet]
        public IEnumerable<proveedore> getAll()
        {
            using (gspEntity entities = new gspEntity())
            {
                entities.Configuration.LazyLoadingEnabled = false;
                return entities.proveedores.ToList();
            }
        }
        [HttpGet]
        public proveedore getbyid(int id)
        {
            try
            {
                using (gspEntity entities = new gspEntity())
                {
                    entities.Configuration.LazyLoadingEnabled = false;
                    var entity = entities.proveedores.FirstOrDefault(e => e.idproveedor == id);
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
        public HttpResponseMessage Post([FromBody] proveedore proveedor)
        {
            try
            {
                using (gspEntity entities = new gspEntity())
                {

                    entities.Configuration.LazyLoadingEnabled = false;
                    entities.proveedores.Add(proveedor);
                    entities.SaveChanges();
                    var message = Request.CreateResponse(HttpStatusCode.Created, proveedor);
                    return message;
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
        [HttpPut]
        public HttpResponseMessage Put(int id, [FromBody]proveedore user)
        {
            try
            {
                using (gspEntity entities = new gspEntity())
                {
                    entities.Configuration.LazyLoadingEnabled = false;
                    var entity = entities.proveedores.FirstOrDefault(e => e.idproveedor == id);
                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "proveedor con ID: " + id.ToString() + ", no encontrado.");
                    }
                    else
                    {
                        entity.nombre = user.nombre;
                        entity.idproveedor = user.idproveedor;
                        entity.telefono = user.telefono;
                        entity.productos = user.productos;
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
                using (gspEntity entities = new gspEntity())
                {
                    entities.Configuration.LazyLoadingEnabled = false;
                    var entity = entities.proveedores.FirstOrDefault(e => e.idproveedor == id);
                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Proveedor con ID: " + id.ToString() + ", no encontrado.");
                    }
                    else
                    {
                        entity.estado = 0;
                        entities.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK, "Proveedor Deleted");
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
