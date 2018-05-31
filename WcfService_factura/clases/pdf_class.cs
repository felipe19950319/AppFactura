using Fonet;
using Fonet.Render.Pdf;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml;
using System.Xml.Xsl;

namespace WcfService_factura.clases
{
    public class pdf_class
    {
        public string Generar_xsl_fo(string dte, string plantilla_xslt)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(dte);

            string XSLFO = null;
            XslCompiledTransform xslt = new XslCompiledTransform();
            xslt.Load(plantilla_xslt);//Aqui va el path de la plantilla
            using (System.IO.StringWriter tmp = new System.IO.StringWriter())
            {
                using (XmlTextWriter res = new XmlTextWriter(tmp))
                {
                    xslt.Transform(xmlDoc, null, res);
                    XSLFO = tmp.ToString();
                    xmlDoc.LoadXml(XSLFO);
                    xmlDoc.InnerXml.ToString();
                    // xmlDoc.Save("c:/test/test.fo");
                }
            }

            return XSLFO;

        }

        public void generar_pdf(XmlDocument fo, string rutaPDF)
        {
            FonetDriver driver = FonetDriver.Make();
            var options = new PdfRendererOptions();
            var font = new System.IO.FileInfo(@"c:/xml/arial.ttf");
            options.AddPrivateFont(font);
            options.FontType = FontType.Subset;
            driver.Options = options;
            ///poner ruta pdf 417 que el nombre tiene que ser igual al tiempo de firma ej 2016-12-13T192207          
            FileStream pdf_stream = null;
            pdf_stream = new FileStream(rutaPDF, FileMode.Create);
            driver.Render(fo, pdf_stream);
            //System.Diagnostics.Process.Start("c:/xml/hello.pdf");

        }
        public string pdf_417(string ruta_doc_firmado)
        {

            string texto;
            DataSet ds = new DataSet();
            ds.ReadXml(ruta_doc_firmado);
            string aux = ds.Tables["Documento"].Rows[0][1].ToString();
            aux = aux.Replace(":", "_");
            string aux1 = ds.Tables["DA"].Rows[0][1].ToString();
            string nombre_archivo = aux1 + " " + aux;

            string ruta_pdf417 = "c:/tyscom xml/pdf/pdf_417/" + nombre_archivo + ".bmp";
            BarcodePDF417 codigobarras = new BarcodePDF417();
            codigobarras.Options = BarcodePDF417.PDF417_USE_ASPECT_RATIO;
            codigobarras.ErrorLevel = 8;
            texto = "ETKT: 099843838838392122982|FLIGHT: KLM9999 CIA:KLMDUTCH |";
            texto += "FROM: MAD | TO: AMS | DATE: 2010-12-23| HOUR:11:00 | SEAT: 20D |";
            texto += "SMOKING: NO | PASS: APELLIDO1APELLIDO2/NOMBRE | ID: 00000000X***";
            codigobarras.SetText(texto);

            System.Drawing.Bitmap bm = new Bitmap(codigobarras.CreateDrawingImage(Color.Black, Color.White));
            bm.Save(ruta_pdf417);

            return ruta_pdf417;
        }

        public void make_pdf(string ruta_dte, string plantilla_xslt, string ruta_doc_firmado)
        {

            XmlDocument fo = new XmlDocument();
            string tratamiento_fo = Generar_xsl_fo(ruta_dte, plantilla_xslt);
            ///se debe hacer un tratamiento al string fo paracambiar la ruta de la imagen de el pdf 417 
            tratamiento_fo = tratamiento_fo.Replace("LOCALIZACION_PDF417", pdf_417(ruta_doc_firmado));
            string ruta_pdf = pdf_417(ruta_doc_firmado);
            ruta_pdf = ruta_pdf.Replace("/pdf/pdf_417/", "/pdf/");
            ruta_pdf = ruta_pdf.Replace("bmp", "pdf");
            fo.LoadXml(tratamiento_fo);
            generar_pdf(fo, ruta_pdf);


        }

    }
}