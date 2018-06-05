using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace app_Factura.App
{
    public partial class Site1 : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //variables de sesion
           /* HttpContext.Current.Session["Razon_social"] = "";
            HttpContext.Current.Session["_UserRut"] = "";
            HttpContext.Current.Session["Id_emp"] = "";
            HttpContext.Current.Session["cnString"] = "";*/



            if (!string.IsNullOrEmpty(HttpContext.Current.Session["_UserRut"].ToString()))
            {
                lblUserRut.Text = HttpContext.Current.Session["_UserRut"].ToString();
            }

            if (HttpContext.Current.Session["Razon_social"]!=null)
            {
                NombreEmpresa.Text = HttpContext.Current.Session["Razon_social"].ToString();
            }
            else
            {
                NombreEmpresa.Text = "";
            }
        }
    }
}