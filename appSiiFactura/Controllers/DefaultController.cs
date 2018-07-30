using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using Newtonsoft.Json;

namespace appSiiFactura.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/Default")]
    public class DefaultController : ApiController
    {
        [Route("Prueba")]
        [HttpPost]
        public IHttpActionResult Prueba()
        {
            var customersFake = new string[] { "customer-1", "customer-2", "customer-3" };
            return Ok("");
        }
        [Route("Prueba1")]
        [HttpPost]
        public IHttpActionResult Prueba1()
        {
            var customersFake = new string[] { "customer-2", "customer-3", "customer-4" };
            return Ok("");
        }
    }

}
