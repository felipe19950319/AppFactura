using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SqlConnector;

namespace Transaction_ws
{
    public class Transaction
    {
        public string CnString= string.Empty;
        public string Login()
        {
            MySqlConnector mysql = new MySqlConnector();
            mysql.getCnString(CnString);

            //mysql.AddProcedure()
           
            return "";
        }

    }
}
