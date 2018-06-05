using SqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace app_Factura.App
{
    public partial class SeleccionEmpresa : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string _UserRut = HttpContext.Current.Session["_UserRut"].ToString();

            DataTable dt = new DataTable();
            MySqlConnector mysql = new MySqlConnector();
            mysql.ConnectionString = HttpContext.Current.Session["cnString"].ToString();

            mysql.AddProcedure("GetEmpresaByRut");
            mysql
                .AddParameter("_UserRut", _UserRut);

            dt = mysql.ExecQuery().ToDataTable();

            DropEmpresa.DataTextField = "RAZON_SOCIAL";
            DropEmpresa.DataValueField = "ID_EMP";

            DropEmpresa.DataSource = dt;
            DropEmpresa.DataBind();
        }


        [WebMethod]
        public static void ajax_SeleccionEmpresa(string Id_emp, string Razon_social)
        {
            HttpContext.Current.Session["Id_emp"] = Id_emp;
            HttpContext.Current.Session["Razon_social"] = Razon_social;     
            //menu estatico 
        
        }
    }
}