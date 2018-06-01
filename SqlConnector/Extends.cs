using Newtonsoft.Json;
using System.Data;
using System.IO;
using System.Xml.Linq;

namespace SqlConnector
{
   public static class Extends
    {
        public static DataTable ToDataTable(this Common ms)
        {
            return ms.ds.Tables[0];
        }

        public static XDocument ToXml(this Common ms)
        {
            string result = string.Empty;
            using (MemoryStream ms_ = new MemoryStream())
            {
                ms.ds.Tables[0].WriteXml(ms_, System.Data.XmlWriteMode.IgnoreSchema);
                result = System.Text.Encoding.Default.GetString(ms_.ToArray());
            }

            XDocument xdoc = XDocument.Parse(result);
            return xdoc;
        }

        public static string ToJson(this Common ms)
        {
            return JsonConvert.SerializeObject(ms.ds.Tables[0], Formatting.Indented);
        }
    }
}
