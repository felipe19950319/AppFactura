using DTE_Maker;
using Newtonsoft.Json;
using SqlConnector;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;
//using System.Web;
using System.Web.Configuration;
using System.Xml;
using System.Xml.Linq;

namespace ws_OperacionesFactura
{
    [ServiceContract(Namespace = "")]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class ws_Sii
    {
        // Para usar HTTP GET, agregue el atributo [WebGet]. (El valor predeterminado de ResponseFormat es WebMessageFormat.Json)
        // Para crear una operación que devuelva XML,
        //     agregue [WebGet(ResponseFormat=WebMessageFormat.Xml)]
        //     e incluya la siguiente línea en el cuerpo de la operación:
        //         WebOperationContext.Current.OutgoingResponse.ContentType = "text/xml";
        [OperationContract,WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
        public string DoWork(string prueba)
        {
            // Agregue aquí la implementación de la operación
            return prueba;
        }

        [OperationContract, WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
        public string GetPreviewDTE(MakeDte.DTE dte)
        {
            Response r = new Response();
            try
            {
                string xsltPath = WebConfigurationManager.AppSettings["xslt_dte"];
                string temp = WebConfigurationManager.AppSettings["temp"];
                temp = temp + Guid.NewGuid() + ".pdf";
                MakeDte m = new MakeDte();
                XmlDocument xml = new XmlDocument();
                xml = m.Serialize(dte);

                pdfSII pdf = new pdfSII();

                pdf.MakeXsl(xml, xsltPath);
                pdf.MakePdf(temp);

                Byte[] bytes = File.ReadAllBytes(temp);
                String file = Convert.ToBase64String(bytes);
                File.Delete(temp);

              
                r.code = Code.OK;
                r.type = Type.base64;
                r.ObjectResponse = file;

               return JsonConvert.SerializeObject(r,Newtonsoft.Json.Formatting.Indented);
               
            }
            catch (Exception ex)
            {
                r.code = Code.ERROR;
                r.type = Type.json;
                r.ex = ex;
                return JsonConvert.SerializeObject(r, Newtonsoft.Json.Formatting.Indented);
            }
        }
        [OperationContract, WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
        public string SaveDocDte(MakeDte.DTE dte)
        {
            try
            {
             
                MySqlConnector mysql = new MySqlConnector();
                mysql.ConnectionString = WebConfigurationManager.ConnectionStrings["MySqlProvider"].ConnectionString;
                mysql.AddProcedure("sp_ins_documentodte");
                mysql
                    .AddParameter("RutEmpresa", RutWithOutDv(dte.documento.encabezado.emisor.RUTEmisor))
                    .AddParameter("RutEmisor", RutWithOutDv(dte.documento.encabezado.emisor.RUTEmisor))
                    .AddParameter("RutReceptor", RutWithOutDv(dte.documento.encabezado.receptor.RUTRecep))
                    .AddParameter("TipoDte", dte.documento.encabezado.iddoc.TipoDTE.ToString())
                    .AddParameter("Folio", dte.documento.encabezado.iddoc.Folio.ToString())
                    .AddParameter("FechaEmision", dte.documento.encabezado.iddoc.FchEmis)
                    .AddParameter("MontoNeto", FormatNumberMySql( dte.documento.encabezado.totales.MntNeto.ToString()))
                    .AddParameter("MontoExento", FormatNumberMySql( dte.documento.encabezado.totales.MntExe.ToString()))
                    .AddParameter("MontoIva", FormatNumberMySql( dte.documento.encabezado.totales.IVA.ToString()))
                    .AddParameter("TasaIva", FormatNumberMySql( dte.documento.encabezado.totales.TasaIVA.ToString()))
                    .AddParameter("MontoTotal", FormatNumberMySql( dte.documento.encabezado.totales.MntTotal.ToString()));


                return mysql.ExecQuery().ToJson();
            }
            catch (Exception ex)
            {
                return "";
            }
          
        }
        private string FormatNumberMySql(string Number){
            Number = Number.Replace(",", ".");
            return Number;
        }
        private string RutWithOutDv(string rut)
        {
            rut = rut.Replace("-", "");
            rut = rut.Substring(0, rut.Length - 1);
            return rut;
        }
      
    }
}
