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
            using (gspEntities entities = new gspEntities())
            {
                entities.Configuration.LazyLoadingEnabled = false;
                return entities.clientes.ToList();
            }
        }

        [HttpPost]
        public HttpResponseMessage Post([FromBody] cliente cliente)
        {
            try
            {
                using (gspEntities entities = new gspEntities())
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

        public HttpResponseMessage Put(int id, [FromBody]cliente user)
        {
            try
            {
                using (gspEntities entities = new gspEntities())
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
    }
}
