using Fonet;
using Fonet.Render.Pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Xsl;

namespace DTE_Maker
{
   public class pdfSII
    {
        /* MakePdfSII.MakePdfSII m = new MakePdfSII.MakePdfSII();
          XmlDocument dte = new XmlDocument();
          dte.Load("C:\\Nueva carpeta\\asfalcura.xml");
          dte.Save("C:\\Nueva carpeta\\asfalcura.xml");
          dte.Load("C:\\Nueva carpeta\\asfalcura.xml");
          m.MakeXsl(dte, "C:\\Nueva carpeta\\ESTANDAR.xslt");
          m.MakePdf("C:\\Nueva carpeta\\asfalcura.pdf", "C:\\Nueva carpeta\\arial.ttf");*/

        private XmlDocument DTE = new XmlDocument();
        //public XmlDocument xmlDTE = new XmlDocument(); 

        public void MakeXsl(XmlDocument xmlDTE, string PathXslt)
        {
            XslCompiledTransform xslt = new XslCompiledTransform();

            xslt.Load(PathXslt);//Aqui va el path de la plantilla
            using (System.IO.StringWriter tmp = new System.IO.StringWriter())
            {
                using (XmlTextWriter res = new XmlTextWriter(tmp))
                {
                    xslt.Transform(xmlDTE, null, res);

                    xmlDTE.LoadXml(tmp.ToString());
                }
            }

            DTE = xmlDTE;
        }


        public void MakePdf(string rutaPDF, string RutaFuente)
        {
            FonetDriver driver = FonetDriver.Make();

            var options = new PdfRendererOptions();

            var font = new System.IO.FileInfo(RutaFuente);

            options.AddPrivateFont(font);

            options.FontType = FontType.Subset;

            driver.Options = options;

            FileStream PdfStream = null;

            PdfStream = new FileStream(rutaPDF, FileMode.Create);
            driver.Render(DTE, PdfStream);
        }
    }
}


