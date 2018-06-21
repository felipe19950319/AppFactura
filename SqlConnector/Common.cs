using Newtonsoft.Json;
using System.Collections.Generic;
using System.Data;

namespace SqlConnector
{
    public class Common
    {
        public Dictionary<string, string> Parameters = new Dictionary<string, string>();
        public string Procedure = string.Empty;
        public string ConnectionString = string.Empty;
        public DataSet ds = new DataSet();

        public void AddProcedure(string Procedure)
        {
            this.Procedure = Procedure;
        }

        public Common AddParameter(string Parameter,string Value)
        {
            Parameters.Add(Parameter, Value);
            return this;
        }

        public string getCnString(string CnString)
        {
            return CnString;
         
        }

        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        public static string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }
        public void ParametersFromJson(string jsonArray)
        {
           string jsonData= Base64Decode(jsonArray);

            dynamic jsonObj = JsonConvert.DeserializeObject(jsonData);
            foreach (object o in jsonObj)
            {
                var c = o.ToString();
                if (c.Contains("{") || c.Contains("}"))
                {
                   c= c.Replace("{","").Replace("}","");
                }
               string[] t =  c.Split(new char[] { ':' }, 2);

               Parameters.Add(
                   t[0].Replace("\"",""),
                   t[1].Replace("\"", "")
                   );                 
            }

        }
    }




}
