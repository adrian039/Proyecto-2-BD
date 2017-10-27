using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RESTFUL.Controllers
{
    public class SucursalesController : ApiController
    {
        [HttpGet]
        public IEnumerable<sucursale> getAll()
        {
            using (gspEntities entities = new gspEntities())
            {
                entities.Configuration.LazyLoadingEnabled = false;
                return entities.sucursales.ToList();
            }
        }
        [HttpGet]
        public sucursale getbyid(int id)
        {
            try
            {
                using (gspEntities entities = new gspEntities())
                {
                    entities.Configuration.LazyLoadingEnabled = false;
                    var entity = entities.sucursales.FirstOrDefault(e => e.idsucursal == id);
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
        public HttpResponseMessage Post([FromBody] sucursale sucursal)
        {
            try
            {
                using (gspEntities entities = new gspEntities())
                {

                    entities.Configuration.LazyLoadingEnabled = false;
                    entities.sucursales.Add(sucursal);
                    entities.SaveChanges();
                    var message = Request.CreateResponse(HttpStatusCode.Created, sucursal);
                    return message;
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
        [HttpPut]
        public HttpResponseMessage Put(int id, [FromBody]sucursale sucursal)
        {
            try
            {
                using (gspEntities entities = new gspEntities())
                {
                    entities.Configuration.LazyLoadingEnabled = false;
                    var entity = entities.sucursales.FirstOrDefault(e => e.idsucursal == id);
                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "sucursal con ID: " + id.ToString() + ", no encontrado.");
                    }
                    else
                    {
                        entity.nombre = sucursal.nombre;
                        entity.direccion = sucursal.direccion;
                        entity.imagen = sucursal.imagen;
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
                    var entity = entities.sucursales.FirstOrDefault(e => e.idsucursal == id);
                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "sucursal con ID: " + id.ToString() + ", no encontrado.");
                    }
                    else
                    {
                        entities.sucursales.Remove(entity);
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
