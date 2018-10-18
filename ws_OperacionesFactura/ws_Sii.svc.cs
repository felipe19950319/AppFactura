﻿using DTE_Maker;
using Newtonsoft.Json;
//using Newtonsoft.Json.Linq;
using SqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
//using System.Text;
//using System.Web;
using System.Web.Configuration;
using System.Xml;
using System.Xml.Linq;
//using System.Xml.Linq;




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
        public Response GetPreviewDTE(MakeDte.DTE dte)
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

                // return JsonConvert.SerializeObject(r,Newtonsoft.Json.Formatting.Indented);
                return r;
            }
            catch (Exception ex)
            {
                  r.code = Code.ERROR;
                  r.type = Type.text;
                  r.ObjectResponse = ex.ToString();
                  return r;
            }
        }
        [OperationContract, WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
        public Response SaveDocDte(MakeDte.DTE dte)
        {
            Response r = new Response();
            try
            {
             
                MySqlConnector mysql = new MySqlConnector();
                DataTable dt_documentodte = new DataTable();

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

                dt_documentodte = mysql.ExecQuery().ToDataTable();
                string IdDte = dt_documentodte.Rows[0]["IdDte"].ToString();

                DataTable dt_documentdte_detalle = new DataTable();
                foreach (var det in dte.documento.detalle)
                {             
                    var cdgItem = det.CdgItem;
                 
                    mysql.AddProcedure("sp_ins_documentdte_detalle");
                    mysql       
                          .AddParameter("IdDte", IdDte)
                          .AddParameter("NumeroLinea", det.NroLinDet)
                          .AddParameter("Exento", "0")
                          .AddParameter("IdDetalle",cdgItem[0].Id_Detalle)
                          .AddParameter("CantidadComprada", det.QtyItem)
                          .AddParameter("MontoTotalDetalle", det.MontoItem)
                          .AddParameter("DescuentoRecargo", "0")
                          .AddParameter("TipoDescuentoRecargo", "0")
                          .AddParameter("MontoUnitDetalle", det.PrcItem)
                          .AddParameter("HasIva",det.HasIva);
                    dt_documentdte_detalle = mysql.ExecQuery().ToDataTable();
                }
                //finalmente si guardamos el documento en la bd procedemos a generar el xml
                MakeDte m = new MakeDte();
                XmlDocument xml = new XmlDocument();
                xml = m.Serialize(dte);

                //insertamos el xml en la tabla
                mysql.AddProcedure("sp_ins_file");
                mysql
                      .AddParameter("File", xml.OuterXml)
                      .AddParameter("FileName",
                                                IdDte //nombre compuesto por id
                                             +dte.documento.encabezado.iddoc.TipoDTE.ToString()//tipo
                                             +RutWithOutDv(dte.documento.encabezado.emisor.RUTEmisor)//rutemisor
                                             +dte.documento.encabezado.iddoc.Folio.ToString()//folio
                                    )
                      .AddParameter("Type", ".xml");
               var IdFile= mysql.ExecQuery().ToDataTable().Rows[0]["IdFile"].ToString();
                //asociamos el archivo insertado al documento dte
                mysql.AddProcedure("sp_upd_DocumentDte_File");
                mysql
                      .AddParameter("idFile", IdFile)
                      .AddParameter("idDte", IdDte);
                mysql.ExecQuery();

                r.code = Code.OK;
                r.type = Type.text;
                r.ObjectResponse = "Se ha ingresado el documento Correctamente!";
                return r;



            }
            catch (Exception ex)
            {
                r.code = Code.ERROR;
                r.type = Type.text;
                r.ObjectResponse = ex.ToString();
                return r;
            }
          
        }

        [OperationContract, WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
        private Response SavePfx(structCertificadoDigital certificadoDigital) {

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
                            .AddParameter("Pwd",Utilities.Encryption(certificadoDigital.Password))
                            .AddParameter("Path", rutaGuardado)
                            .AddParameter("TypeFile", certificadoDigital.TypeFile);

                    DataTable dt = new DataTable();
                    dt = mysql.ExecQuery().ToDataTable();


              

                    if (dt.Rows[0]["InsertStatus"].ToString() == "OK")
                    {
                        Directory.CreateDirectory(RutaCertificado + certificadoDigital.RutEmpresa);
                        File.WriteAllBytes(rutaGuardado, Convert.FromBase64String(certificadoDigital.Base64));
                    }

                    r.code = Code.OK;
                    r.type = Type.text;
                    r.ObjectResponse = "Se ha ingresado el certificado correctamente!";

                }
                else
                {
                    r.code = Code.ERROR;
                    r.type = Type.text;
                    r.ObjectResponse= "La contraseña ingresada no es valida!";
                }
                return r;
            }
            catch (Exception ex)
            {
                r.code = Code.ERROR;
                r.type = Type.text;
                r.ObjectResponse = ex.ToString();
                return r;
            }
        }
        [OperationContract, WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
        private String SingDocSii(string RutEmpresa, string IdDocumento)
        {
            //obtenemos el CAF y el NUMERO de folio para proceder con el proceso

            return "";
        }

        [OperationContract, WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]//guardar y emitir
        private string SendDocSii(string RutEmpresa,string IdDocumento,string Ambiente)
        {
            //sp_sel_certificado_digital
            MySqlConnector mysql = new MySqlConnector();
            mysql.ConnectionString = WebConfigurationManager.ConnectionStrings["MySqlProvider"].ConnectionString;
            mysql.AddProcedure("sp_sel_certificado_digital");
            mysql.
                  AddParameter("rutEmpresa", RutEmpresa);            
            DataTable dt = new DataTable();
            dt = mysql.ExecQuery().ToDataTable();

            SiiUtilities siiUtil = new SiiUtilities();

            string PathCert = dt.Rows[0]["Path"].ToString();
            string PassCert =Utilities.Decryption( dt.Rows[0]["Password"].ToString());
            X509Certificate2 cert = new X509Certificate2(PathCert,PassCert);

            ConexionSII cn = new ConexionSII();
            string Token = string.Empty;
            //PASO 1 PEDIMOS EL TOKEN
            Token = cn.PidoSemillaToken(cert,Ambiente);
            //PASO 2 FIRMAMOS EL DOCUMENTO 

            //PASO 3 ENSOBRAMOS EL DTE EN EL SETDTE

            //PASO 4 ENVIAMOS

            if (!string.IsNullOrEmpty(Token))
            {
                EnvioSii envioSii = new EnvioSii();
                envioSii.Xml = XDocument.Parse("",LoadOptions.PreserveWhitespace);
                envioSii.RutEmisor = RutEmpresa;
                envioSii.RutEmpresa = RutEmpresa;
                envioSii.Token = Token;
                siiUtil.SendDoc(envioSii);
            }
            return "";
        }

        [OperationContract, WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
        private Response SaveFolio(structFolio folio)
        {
            Response r = new Response();
            try
            {
        
            MySqlConnector mysql = new MySqlConnector();
            mysql.ConnectionString = WebConfigurationManager.ConnectionStrings["MySqlProvider"].ConnectionString;

            mysql.AddProcedure("sp_ins_caf");
            mysql.
                     AddParameter("RUT_EMPRESA", RutWithOutDv(folio.RutEmpresa))
                    .AddParameter("FOLIO_DESDE", folio.FolioDesde)
                    .AddParameter("FOLIO_HASTA", folio.FolioHasta)
                    .AddParameter("ESTADO", "ACTIVO")
                    .AddParameter("CAF", folio.xml)
                    .AddParameter("TIPO_DOC_CAF", folio.TipoDocumento);

            DataTable dt = new DataTable();
            dt = mysql.ExecQuery().ToDataTable();

                if (dt.Rows[0]["TypeResult"].ToString() == "0")
                {
                    r.code = Code.ERROR;
                }
                else
                {
                    r.code = Code.OK;
                }
         
            r.type = Type.text;
            r.ObjectResponse = dt.Rows[0]["Result"].ToString();

            return r;
            }
            catch (Exception ex)
            {
                r.code = Code.ERROR;
                r.type = Type.text;
                r.ObjectResponse = ex.ToString();
                return r;
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
