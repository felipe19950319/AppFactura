using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SqlConnector;
using DTE_Maker;
using System.Xml.Linq;
using Newtonsoft.Json;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            //generamos el dte 
            MakeDte.DTE dte = new MakeDte.DTE();

            dte.documento.ID = "DOC3238";
            dte.documento.encabezado.iddoc.TipoDTE = 1;
            dte.documento.encabezado.iddoc.Folio = 1;
            dte.documento.encabezado.iddoc.FchEmis = "2017-05-08";
            dte.documento.encabezado.iddoc.FmaPago = 1;

            dte.documento.encabezado.emisor.RUTEmisor = "19047321-k";
            dte.documento.encabezado.emisor.RznSoc = "adsddf";

            dte.documento.encabezado.receptor.RUTRecep = "190473121-k";

            dte.documento.encabezado.totales.MntNeto = 9999;

            MakeDte.Detalle det = new MakeDte.Detalle();
            det.MontoItem = 99;

            dte.detalle.Add(det);
            dte.detalle.Add(det);

            var x = JsonConvert.SerializeObject(dte);
           /* MakeDte m = new MakeDte();
            XDocument xdoc = new XDocument();
            xdoc= m.Serialize(dte);*/

          

        }
    }
}
