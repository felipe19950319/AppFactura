using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication_tyscom
{
    public partial class Seleccion_Docs : System.Web.UI.Page
    {
        Controlador.Session s = new Controlador.Session();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (s.get_nombre_emp() == null) {
                Response.Redirect("Selecccion_Empresa.aspx");
            }
        }
    
        protected void link_33_Click(object sender, EventArgs e)
        {
           
            s._tpo_doc = "33";
            Session["tipo_doc"] = s;
            if (s.get_nombre_emp() == null)
            {
                Response.Redirect("Selecccion_Empresa.aspx");
            }
            Response.Redirect("EmisionDocumentos.aspx");
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
      
            s._tpo_doc = "34";
            Session["tipo_doc"] = s;
            Response.Redirect("EmisionDocumentos.aspx");
        }

        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            s._tpo_doc = "56";
            Session["tipo_doc"] = s;
            Response.Redirect("EmisionDocumentos.aspx");
        }

        protected void LinkButton3_Click(object sender, EventArgs e)
        {
            s._tpo_doc = "61";
            Session["tipo_doc"] = s;
            Response.Redirect("EmisionDocumentos.aspx");
        }
    }
}