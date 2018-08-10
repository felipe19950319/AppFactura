using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;

namespace SqlConnector
{
    public class Common
    {
        public Dictionary<string, dynamic> Parameters = new Dictionary<string, dynamic>();
        public string Procedure = string.Empty;
        public string ConnectionString = string.Empty;
        public DataSet ds = new DataSet();

        private string FormatNumber(string Number)
        {
            Number = Number.Replace(",", ".");
            return Number;
        }
        public string CheckTypeVar(dynamic Value)
        {
            string _x = string.Empty;
            if (Value is int)
            {
                _x = Convert.ToString(Value);
            }

            if (Value is decimal)
            {
                _x = Convert.ToString(Value);
                _x = FormatNumber(_x);
            }

            if (Value is float)
            {
                _x = Convert.ToString(Value);
                _x = FormatNumber(_x);
            }
            if (Value is string)
            {
                _x = Convert.ToString(Value);
            }
            if (Value is double)
            {
                _x = Convert.ToString(Value);
               _x = FormatNumber(_x);
            }
            if (Value is char)
            {
                _x = Convert.ToString(Value);
            }
            if (Value is DateTime)
            {
                _x = Convert.ToString(Value);
            }

            return _x;
        }

        public void AddProcedure(string Procedure)
        {
            this.Procedure = Procedure;
        }

        public Common AddParameter(string Parameter, dynamic Value)
        {
            Parameters.Add(Parameter, CheckTypeVar(Value));
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
                    t[0].Replace("\"", ""),
                    t[1].Replace("\"", "").Substring(1)
                   );                 
            }

        }
    }




}
