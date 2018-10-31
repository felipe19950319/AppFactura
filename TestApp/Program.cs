using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;

namespace TestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // string xml = File.ReadAllText("C:\\Nueva carpeta\\file.xml");
            XElement x = XElement.Load("C:\\Nueva carpeta\\file.xml");
            x = stripNS(x);
            var e =x.Descendants("Receptor");
            string Folio = string.Empty;
            var query = (from dte in e select new {
                dte.Element("RznSocRecep").Value
            }).ToList();
            // string Folio = GetNodeValue(xml, "Folio");
            // XDocument xelem = XDocument.Load("C:\\Nueva carpeta\\file.xml");
            // var x = xelem.XPathSelectElement("//EnvioDTE//SetDTE//DTE");

        }
       public static XElement stripNS(XElement root)
        {
            return new XElement(
                root.Name.LocalName,
                root.HasElements ?
                    root.Elements().Select(el => stripNS(el)) :
                    (object)root.Value
            );
        }
        public static string GetNodeValue(string xml_string, string NameItem)
        {
            XmlTextReader reader = new XmlTextReader(new StringReader(xml_string));
            string semilla = "";
            string nombreNodo = "";
            reader.WhitespaceHandling = WhitespaceHandling.None;
            while ((reader.Read()))
            {
                switch (reader.NodeType)
                {
                    case XmlNodeType.Element:
                        nombreNodo = reader.Name;
                        if (reader.HasAttributes)
                        {
                            while (reader.MoveToNextAttribute())
                            {
                                nombreNodo = reader.Name;
                            }
                        }
                        break;
                    case XmlNodeType.Text:
                        if (nombreNodo == NameItem)
                        {
                            semilla = reader.Value;
                        }
                        break;
                }
            }
            return semilla;
        }
    }
}
