using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace app_Factura.App
{
    public partial class General : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static string GetSessionVariable(string SessionVarName)
        {
            var x = string.Empty;

            try
            {
                x = HttpContext.Current.Session[SessionVarName].ToString();
            }
            catch (Exception ex)
            {
                x = ex.InnerException.ToString();
            }

            return x;
        }

    }
}