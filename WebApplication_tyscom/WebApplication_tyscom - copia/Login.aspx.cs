using Controlador;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication_tyscom
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            var s = new Session();
            //
            var consultas = new ServFact.ServicioFacturaClient();

            if (consultas.login(txt_rut.Text, txt_pass.Text) == true)
            {
                s.rut = txt_rut.Text;
                Session["RUT_SESSION"] = s;

                s.JSON_ListaEmpresas = consultas.ObtieneListaEmpresa(s.rut);
                Session["JSON_ListaEmpresas"] = s;

                Response.Redirect("Seleccion_Empresa.aspx");
            }
            else
            {
                Response.Write("Usuario Incorrecto!");

            }
        }
    }
}