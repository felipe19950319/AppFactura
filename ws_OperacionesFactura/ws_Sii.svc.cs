using DTE_Maker;
using Newtonsoft.Json;
//using Newtonsoft.Json.Linq;
using SqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
//using System.Text;
//using System.Web;
using System.Web.Configuration;
using System.Xml;
//using System.Xml.Linq;
using ws_OperacionesFactura.Class;

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
            Response r = new Response();
            try
            {
             
                MySqlConnector mysql = new MySqlConnector();
                mysql.ConnectionString = WebConfigurationManager.ConnectionStrings["MySqlProvider"].ConnectionString;
                mysql.AddProcedure("sp_ins_documentodte");
                mysql
                    .AddParameter("RutEmpresa", RutWithOutDv(dte.documento.encabezado.emisor.RUTEmisor))
                    .AddParameter("RutEmisor", RutWithOutDv(dte.documento.encabezado.emisor.RUTEmisor))
                    .AddParameter("RutReceptor", RutWithOutDv(dte.documento.encabezado.receptor.RUTRecep))
                    .AddParameter("TipoDte", dte.documento.encabezado.iddoc.TipoDTE)
                    .AddParameter("Folio", dte.documento.encabezado.iddoc.Folio)
                    .AddParameter("FechaEmision", dte.documento.encabezado.iddoc.FchEmis)
                    .AddParameter("MontoNeto",  dte.documento.encabezado.totales.MntNeto)
                    .AddParameter("MontoExento",  dte.documento.encabezado.totales.MntExe)
                    .AddParameter("MontoIva",  dte.documento.encabezado.totales.IVA)
                    .AddParameter("TasaIva", dte.documento.encabezado.totales.TasaIVA)
                    .AddParameter("MontoTotal",  dte.documento.encabezado.totales.MntTotal)
                    .AddParameter("TipoOperacion", dte.TipoOperacion);


                return mysql.ExecQuery().ToJson();
            }
            catch (Exception ex)
            {
                return "";
            }
          
        }

        [OperationContract, WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
        private string SavePfx(structCertificadoDigital certificadoDigital) {

            Response r = new Response();
            try
            {
                //paso 1 verificamos que el certificado y la password sean validos
                byte[] Cert = System.Convert.FromBase64String(certificadoDigital.Base64);
                bool _PASSWORD_OK = Utilities.VerifyPassword(Cert,certificadoDigital.Password);

                //paso 2 si la password del certificado es ok seguimos con el proceso
                if (_PASSWORD_OK == true)
                {
                    string RutaCertificado = WebConfigurationManager.AppSettings["Certificados"];
                    //RutaCertificado = RutaCertificado + certificadoDigital.RutEmpresa+".pfx";
                    //creamos la carpeta de la empresa utilizando el rut de la empresa como nombre             
                    String rutaGuardado = RutaCertificado + certificadoDigital.RutEmpresa + "\\" + certificadoDigital.RutEmpresa + ".pfx";

                    MySqlConnector mysql = new MySqlConnector();
                    mysql.ConnectionString = WebConfigurationManager.ConnectionStrings["MySqlProvider"].ConnectionString;
                    mysql.AddProcedure("sp_ins_certificado_digital");
                    mysql.
                             AddParameter("rutEmpresa", certificadoDigital.RutEmpresa)
                            .AddParameter("Pwd", certificadoDigital.Password)
                            .AddParameter("Path", rutaGuardado)
                            .AddParameter("TypeFile", certificadoDigital.TypeFile);

                    DataTable dt = new DataTable();
                    dt = mysql.ExecQuery().ToDataTable();


                    r.code = Code.OK;
                    r.type = Type.json;
                    r.ObjectResponse = JsonConvert.SerializeObject(dt, Newtonsoft.Json.Formatting.Indented);

                    if (dt.Rows[0]["InsertStatus"].ToString() == "OK")
                    {
                        Directory.CreateDirectory(RutaCertificado + certificadoDigital.RutEmpresa);
                        File.WriteAllBytes(rutaGuardado, Convert.FromBase64String(certificadoDigital.Base64));
                    }



                }
                else
                {
                    r.code = Code.ERROR;
                    r.type = Type.json;
                    r.ObjectResponse= "[\r\n  {\r\n    \"Result\": 2,\r\n    \"InsertStatus\": \"La contraseña  informada no es valida\"\r\n  }\r\n]";
                }
                return JsonConvert.SerializeObject(r, Newtonsoft.Json.Formatting.Indented);
            }
            catch (Exception ex)
            {
                r.code = Code.ERROR;
                r.type = Type.json;
                r.ex = ex;
                return JsonConvert.SerializeObject(r, Newtonsoft.Json.Formatting.Indented);
            }
        }

        private string RutWithOutDv(string rut)
        {
            rut = rut.Replace("-", "");
            rut = rut.Substring(0, rut.Length - 1);
            return rut;
        }
      
    }
}
