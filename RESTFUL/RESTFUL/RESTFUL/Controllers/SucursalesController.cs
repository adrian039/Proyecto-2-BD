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
            using (gspEntity entities = new gspEntity())
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
                using (gspEntity entities = new gspEntity())
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
        [HttpGet]
        public IEnumerable<cajasxsucursal> getCajas([FromUri]int idSucursal)
        {
            try
            {
                using (gspEntity entities = new gspEntity())
                {
                    entities.Configuration.LazyLoadingEnabled = false;
                    var entity = entities.cajasxsucursals.Where(e => e.idsucursal == idSucursal);
                    if (entity == null)
                    {
                        return null;
                    }
                    else
                    {
                        return entity.ToList();
                    }

                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        [HttpGet]
        public IEnumerable<sucursale> getSucursalesxEmpresa([FromUri]int idEmpresa)
        {
            try
            {
                using (gspEntity entities = new gspEntity())
                {
                    entities.Configuration.LazyLoadingEnabled = false;
                    var entity = entities.sucursales.Where(e => e.idempresa == idEmpresa);
                    if (entity == null)
                    {
                        return null;
                    }
                    else
                    {
                        return entity.ToList();
                    }

                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        [HttpGet]
        public IEnumerable<Models.Roles> getRoles([FromUri]int idSucxRol)
        {
            try
            {
                using (gspEntity entities = new gspEntity())
                {
                    entities.Configuration.LazyLoadingEnabled = false;
                    var entity = entities.empleadosxsucursals.Join(
                         entities.roles,
                         c => c.idrol, cm => cm.idrol,
                         (c, cm) => new Models.Roles
                         {
                             idrol = c.idrol,
                             nombre = cm.nombre,
                             descripcion = cm.descripcion,
                             idsucursal = c.idsucursal
                         }).Where(e => e.idsucursal == idSucxRol);
                    if (entity == null)
                    {
                        return null;
                    }
                    else
                    {
                        return entity.ToList();
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
                using (gspEntity entities = new gspEntity())
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
                using (gspEntity entities = new gspEntity())
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
                using (gspEntity entities = new gspEntity())
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
