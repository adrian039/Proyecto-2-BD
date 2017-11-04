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
            using (gspEntity entities = new gspEntity())
            {
                entities.Configuration.LazyLoadingEnabled = false;
                return entities.empleadoes.ToList();
            }
        }

        [HttpGet]
        public Models.EmployeeInfo getbyid([FromUri]int idemp)
        {
            try
            {
                using (gspEntity entities = new gspEntity())
                {
                    entities.Configuration.LazyLoadingEnabled = false;
                    var entity = entities.empleadoes.Join(entities.empleadosxsucursals,c => c.cedula, cm => cm.idempleado, (c, cm) => new Models.EmployeeInfo{ nombre = c.nombre, idsucursal = cm.idsucursal, cedula = c.cedula}).FirstOrDefault(e => e.cedula == idemp);
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
        public empleado getUserLoginInfo(int id)
        {
            try
            {
                using (gspEntity entities = new gspEntity())
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
                using (gspEntity entities = new gspEntity())
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

        [HttpPost]
        public Models.EmployeeInfo empleadoLogin([FromUri] string username, [FromUri] string pass)
        {
            try
            {
                using (gspEntity entities = new gspEntity())
                {

                    entities.Configuration.LazyLoadingEnabled = false;
                    var entity = entities.empleadoes.Join(
                        entities.empleadosxsucursals, 
                        c => c.cedula, cm => cm.idempleado, 
                        (c, cm) => new Models.EmployeeInfo {
                            cedula = c.cedula,
                            email = c.email,
                            username = c.username,
                            nombre = c.nombre,
                            papellido = c.papellido,
                            sapellido = c.sapellido,
                            idrol = cm.idrol,
                            idsucursal = cm.idsucursal }).FirstOrDefault(e => e.username == username);
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

        [HttpPut]
        public HttpResponseMessage Put(int id, [FromBody]empleado empleado)
        {
            try
            {
                using (gspEntity entities = new gspEntity())
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
                using (gspEntity entities = new gspEntity())
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
