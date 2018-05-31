using Controlador;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication_tyscom
{
    public partial class Seleccion_Empresa : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session s = new Session();
            string json = s.get_ListaEmpresas();
            DataTable dt = (DataTable)JsonConvert.DeserializeObject(json, (typeof(DataTable)));
                                                              
            List_Emp.DataSource = dt;
            List_Emp.DataValueField = dt.Columns["ID_EMPESA"].ToString();
            List_Emp.DataTextField = dt.Columns["RAZON_SOCIAL"].ToString();
            List_Emp.DataBind();
        }

        protected void link_emp_Click(object sender, EventArgs e)
        {
            string id_emp = Text1.Value;
            Session s = new Session();
            s.ID_emp = id_emp;
            Session["id_emp"] = s;

            string nom_emp = Text2.Value;
            s.emp = nom_emp;
            Session["Empresa"] = s;

            Response.Redirect("Seleccion_Docs.aspx");

        }
    }
}