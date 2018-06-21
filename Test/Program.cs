using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SqlConnector;


namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            SqlConnector.Common c = new Common();
           
           



            MySqlConnector mysql = new MySqlConnector();
            mysql.ConnectionString = "SERVER=localhost;PORT=3309;DATABASE=tyscom_factura;UID=root;PASSWORD=19047321k;SslMode=none;";
            mysql.AddProcedure("sp_ins_detalle_producto");
             mysql.ParametersFromJson("eyJwX0lEX0VNUFJFU0EiOiIxIiwicF9OT01CUkUiOiJHQUxMRVRBMSIsInBfQ09ESUdPIjoiUE85SzkiLCJwX1NUT0NLIjoiMSIsInBfREVTQ1JJUENJT05fUFJPRFVDVE8iOiJPUE8iLCJwX1ZBTE9SX1VOSVRBUklPIjoiOTkiLCJwX0ZFQ0hBX0NSRUFDSU9OIjoiMjAxOC0wNi0yMCIsInBfREVTQ1VFTlRPX1BDVCI6IjAiLCJwX0VTVEFETyI6IkNTVE9DSyIsInBfSWRVbmlkYWRNZWRpZGEiOiIyIn0=");

        var x=     mysql.ExecQuery().ToJson();
        }
    }
}
