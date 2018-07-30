using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace DTE_Maker
{
    public class Extends
    {

        public XDocument Serialize(Object obj)
        {
            XDocument doc = new XDocument();
            using (var writer = doc.CreateWriter())
            {
                // write xml into the writer
                var serializer = new DataContractSerializer(obj.GetType());
                serializer.WriteObject(writer, obj);
            }
            return doc;

        }
        public XmlDocument ToXmlDocument( XDocument xDocument)
        {
            var xmlDocument = new XmlDocument();
            using (var xmlReader = xDocument.CreateReader())
            {
                xmlDocument.Load(xmlReader);
            }
            return xmlDocument;
        }
    }
}
