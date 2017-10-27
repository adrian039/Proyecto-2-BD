using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RESTFUL.Controllers
{
    public class EmpleadosController : ApiController
    {
        [HttpGet]
        public IEnumerable<empleado> getAll()
        {
            using (gspEntities entities = new gspEntities())
            {
                entities.Configuration.LazyLoadingEnabled = false;
                return entities.empleadoes.ToList();
            }
        }
        [HttpGet]
        public empleado getbyid(int id)
        {
            try
            {
                using (gspEntities entities = new gspEntities())
                {
                    entities.Configuration.LazyLoadingEnabled = false;
                    var entity = entities.empleadoes.FirstOrDefault(e => e.cedula == id);
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
        public HttpResponseMessage Post([FromBody] empleado empleado)
        {
            try
            {
                using (gspEntities entities = new gspEntities())
                {

                    entities.Configuration.LazyLoadingEnabled = false;
                    entities.empleadoes.Add(empleado);
                    entities.SaveChanges();
                    var message = Request.CreateResponse(HttpStatusCode.Created, empleado);
                    return message;
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
        [HttpPut]
        public HttpResponseMessage Put(int id, [FromBody]empleado empleado)
        {
            try
            {
                using (gspEntities entities = new gspEntities())
                {
                    entities.Configuration.LazyLoadingEnabled = false;
                    var entity = entities.empleadoes.FirstOrDefault(e => e.cedula == id);
                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Empleado con Cedula: " + id.ToString() + ", no encontrado.");
                    }
                    else
                    {
                        entity.nombre = empleado.nombre;
                        entity.cedula = empleado.cedula;
                        entity.papellido = empleado.papellido;
                        entity.sapellido = empleado.sapellido;
                        entity.username = empleado.username;
                        entity.password = empleado.password;
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
                    var entity = entities.empleadoes.FirstOrDefault(e => e.cedula == id);
                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Empleado con Cedula: " + id.ToString() + ", no encontrado.");
                    }
                    else
                    {
                        entities.empleadoes.Remove(entity);
                        entities.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK, "Empleado Deleted");
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
