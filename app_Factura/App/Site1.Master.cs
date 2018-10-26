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
            if (!string.IsNullOrEmpty(GetSessionVariable("_UserRut")))
            {
                lblUserRut.Text = GetSessionVariable("_UserRut");
                SetHiddenFields();
            }
            else
            {
                Response.Redirect("Login.aspx");
            }

            if (!string.IsNullOrEmpty( GetSessionVariable("Razon_social")))
            {
                var Ambiente = GetSessionVariable("Ambiente");
                if (Ambiente == "CERT") {
                    NombreEmpresa.Text = GetSessionVariable("Razon_social") + " (Certificación) " ;
                }
                if (Ambiente == "PROD")
                {
                    NombreEmpresa.Text = GetSessionVariable("Razon_social") + " (Producción) ";
                }

            }

        }

        public void SetHiddenFields()
        {
            _SES_IdEmpresa.Value = GetSessionVariable("Id_emp");
            _SES_RutUser.Value= GetSessionVariable("_UserRut");
            _SES_RutEmpresa.Value = GetSessionVariable("RutEmpresa");
            _SES_Ambiente.Value = GetSessionVariable("Ambiente");
        }

        public string GetSessionVariable(string SessionVarName)
        {
            var x = string.Empty;

            try
            {
                x = HttpContext.Current.Session[SessionVarName].ToString();
            }
            catch (Exception ex)
            {
                x = "";
            }

            return x;
        }

    }
}