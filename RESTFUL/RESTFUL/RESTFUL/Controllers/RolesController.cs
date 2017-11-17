using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RESTFUL.Controllers
{
    public class RolesController : ApiController
    {
        [HttpGet]
        public IEnumerable<role> getAll()
        {

            using (gspEntity entities = new gspEntity())
            {
                entities.Configuration.LazyLoadingEnabled = false;
                return entities.roles.ToList().Where(e=>e.estado!=0);
            }
        }

       /* [HttpGet]
        public HttpResponseMessage getRolbySucursal([FromUri]int suc)
        {
            using (gspEntity entitie=new )
            {

            }
        }
        */
        [HttpPost]
        public HttpResponseMessage regRol([FromBody] role rol)
        {
            try
            {
                using(gspEntity entities=new gspEntity())
                {
                    entities.Configuration.LazyLoadingEnabled = false;
                    entities.roles.Add(rol);
                    entities.SaveChanges();
                    var message = Request.CreateResponse(HttpStatusCode.Created, rol);
                    return message;
                }

            }catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpPut]
        public HttpResponseMessage updateRol([FromBody] role rol)
        {
            try
            {
                using (gspEntity entities=new gspEntity())
                {
                    entities.Configuration.LazyLoadingEnabled = false;
                    var entity = entities.roles.FirstOrDefault(e => e.idrol == rol.idrol);
                    if (entity == null)
                    {
                        return Request.CreateResponse(HttpStatusCode.Conflict, "No existe ese rol");
                    }
                    else
                    {
                        entity.nombre = rol.nombre;
                        entity.descripcion = rol.descripcion;
                        entities.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK, rol);
                    }

                }

            }catch(Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpDelete]
        public HttpResponseMessage deleteRol(int idRol)
        {
            try
            {
                using (gspEntity entities= new gspEntity())
                {
                    entities.Configuration.LazyLoadingEnabled = false;
                    var entity = entities.roles.FirstOrDefault(e => e.idrol == idRol);
                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Rol con ID: " + idRol.ToString() + ", no encontrado.");
                    }
                    else
                    {
                        entity.estado = 0;
                        entities.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK, "Rol Deleted");
                    }
                }
            }catch(Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
    }
}
