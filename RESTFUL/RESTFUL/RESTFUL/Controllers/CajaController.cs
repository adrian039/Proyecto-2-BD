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
        [HttpGet]
        public IEnumerable<caja> getAll()
        {
            using (gspEntity entities = new gspEntity())
            {
                entities.Configuration.LazyLoadingEnabled = false;
                return entities.cajas.ToList();
            }
        }
        [HttpPost]
        public bool Post([FromBody] caja cajalog)
        {
            try
            {
                using (gspEntity entities = new gspEntity())
                {

                    entities.Configuration.LazyLoadingEnabled = false;
                    entities.cajas.Add(cajalog);
                    entities.SaveChanges();
                    var message = Request.CreateResponse(HttpStatusCode.Created, cajalog);
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
