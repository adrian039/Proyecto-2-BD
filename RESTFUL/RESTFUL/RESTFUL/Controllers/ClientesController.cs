using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RESTFUL.Controllers
{
    public class ClientesController : ApiController
    {
        [HttpGet]
        public IEnumerable<cliente> getAll()
        {
            using (gspEntity entities = new gspEntity())
            {
                entities.Configuration.LazyLoadingEnabled = false;
                return entities.clientes.ToList().Where(e=>e.estado!=0);
            }
        }
        [HttpGet]
        public cliente getbyid(int id)
        {
            try
            {
                using (gspEntity entities = new gspEntity())
                {
                    entities.Configuration.LazyLoadingEnabled = false;
                    var entity = entities.clientes.FirstOrDefault(e => e.cedula == id);
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
        public cliente verifCliente(int cedula)
        {
            try
            {
                using (gspEntity entities = new gspEntity())
                {
                    entities.Configuration.LazyLoadingEnabled = false;
                    var resp = entities.clientes.FirstOrDefault(e => e.cedula == cedula);
                    if (resp != null)
                    {
                        return  resp;
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

        [HttpPost]
        public HttpResponseMessage Post([FromBody] cliente cliente)
        {
            try
            {
                using (gspEntity entities = new gspEntity())
                {
                    
                    entities.Configuration.LazyLoadingEnabled = false;
                    entities.clientes.Add(cliente);
                    entities.SaveChanges();
                    var message = Request.CreateResponse(HttpStatusCode.Created, cliente);
                    return message;
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
        [HttpPut]
        public HttpResponseMessage Put(int id, [FromBody]cliente user)
        {
            try
            {
                using (gspEntity entities = new gspEntity())
                {
                    entities.Configuration.LazyLoadingEnabled = false;
                    var entity = entities.clientes.FirstOrDefault(e => e.cedula == id);
                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Empleado con Cedula: " + id.ToString() + ", no encontrado.");
                    }
                    else
                    {
                        entity.nombre = user.nombre;
                        entity.cedula = user.cedula;
                        entity.papellido = user.papellido;
                        entity.sapellido = user.sapellido;
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
                    var entity = entities.clientes.FirstOrDefault(e => e.cedula == id);
                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Empleado con Cedula: " + id.ToString() + ", no encontrado.");
                    }
                    else
                    {
                        entity.estado = 0;
                        entities.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK, "Client Deleted");
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
