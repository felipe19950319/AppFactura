using Controlador;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication_tyscom
{
    public partial class Site1 : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var s = new Session();
            string rut_user = s.get_sessionRUT();
            rut_user = rut_user.Replace(".", "");
            Label1.Text = rut_user;
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            Response.Redirect("Seleccion_Empresa.aspx");
        }

        protected void log_out_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Session.RemoveAll();
            Session.Abandon();
            Response.Redirect("Login.aspx");
        }
    }
}