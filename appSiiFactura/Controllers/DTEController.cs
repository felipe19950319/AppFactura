using DTE_Maker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Configuration;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Xml.Linq;
using static MakeDte;

namespace appSiiFactura.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/DTE")]
    public class DTEController : ApiController
    {
        [Route("GetPreviewDTE")]
        [HttpPost]
        public IHttpActionResult GetPreviewDTE(DTE dte)
        {
            string xsltPath = WebConfigurationManager.AppSettings["xslt_dte"];
            string temp = WebConfigurationManager.AppSettings["temp"];
            temp = temp + Guid.NewGuid() + ".pdf";
            MakeDte m = new MakeDte();
            XDocument x = new XDocument();
            x = m.Serialize(dte);

            pdfSII pdf = new pdfSII();
            
            pdf.MakeXsl(m.ToXmlDocument(x), xsltPath);
            pdf.MakePdf(temp);

            return Ok("lol");
        }
    }
}
