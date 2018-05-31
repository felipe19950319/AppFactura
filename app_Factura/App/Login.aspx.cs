using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SqlConnector;
using System.Web.Configuration;
using System.Data;

namespace app_Factura.App
{
    public partial class Login : System.Web.UI.Page
    {
        //cadena de conexion
        string cnString = WebConfigurationManager.ConnectionStrings["MySqlProvider"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Login_Click(object sender, EventArgs e)
        {       
            string _rut = string.Empty;
            string _pass = string.Empty;     
            _rut = rut.Value;
            _pass = inputPassword.Value;

            DataTable dt = new DataTable();
            MySqlConnector mysql = new MySqlConnector();
            mysql.ConnectionString = cnString;

            mysql.AddProcedure("Login");
            mysql
                .AddParameter("Rut",_rut)
                .AddParameter("Contrasenia",_pass);
          
           dt = mysql.ExecQuery().ToDataTable();

            if (dt.Rows[0][0].ToString() == "FALSE")
                Response.Redirect("Login.aspx");
            else
                Response.Redirect("MenuPrincipal.aspx");
        }
    }
}