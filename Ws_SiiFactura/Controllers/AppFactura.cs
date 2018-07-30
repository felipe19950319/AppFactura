using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Ws_SiiFactura.Controllers
{
    [RoutePrefix("WsSii_api/Methods")]
    public class AppFactura : ApiController
    {

        [Route("")]
        [HttpGet]
        public HttpResponseMessage Prueba()
        {
            return Request.CreateResponse(HttpStatusCode.OK, "hola");
        }
    }
}
