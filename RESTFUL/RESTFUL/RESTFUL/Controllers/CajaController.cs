using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RESTFUL.Controllers
{
    public class CajaController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage Post([FromBody] caja caja)
        {
            try
            {
                using (gspEntity entities = new gspEntity())
                {

                    entities.Configuration.LazyLoadingEnabled = false;
                    entities.cajas.Add(caja);
                    entities.SaveChanges();
                    var message = Request.CreateResponse(HttpStatusCode.Created, caja);
                    return message;
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
    }
}
