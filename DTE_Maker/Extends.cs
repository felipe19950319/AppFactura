using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace DTE_Maker
{
    public class Extends
    {

        public XmlDocument Serialize(object obj)
        {
            XmlSerializer serializer = new XmlSerializer(obj.GetType());
            XmlDocument xmlDoc = new XmlDocument();
            using (MemoryStream ms = new MemoryStream())
            {

                serializer.Serialize(ms, obj);
                ms.Position = 0;
                xmlDoc.Load(ms);
            }

            return xmlDoc;
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
