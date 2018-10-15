using SqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace app_Factura.App
{
    public partial class ListaDocumentos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod]
        public static string sp_sel_Documentos(string RutEmpresa)
        {

            MySqlConnector mysql = new MySqlConnector();
            mysql.ConnectionString = HttpContext.Current.Session["cnString"].ToString();
            mysql.AddProcedure("sp_sel_Documentos");
            mysql
                .AddParameter("RutEmpresa", RutEmpresa);


            return mysql.ExecQuery().ToJson();
        }
    }
}