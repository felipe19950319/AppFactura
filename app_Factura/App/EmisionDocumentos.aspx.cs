using SqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using DTE_Maker;
using System.Xml.Linq;

namespace app_Factura.App
{
    public partial class EmisionDocumentos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod]
        public static string sel_unidadmedida() {
            MySqlConnector mysql = new MySqlConnector();
            mysql.ConnectionString = HttpContext.Current.Session["cnString"].ToString();
            mysql.AddProcedure("sp_sel_unidadmedida");

            return mysql.ExecQuery().ToJson();
        }

        [WebMethod]
        public static string GetComuna(string json) {
            //sp_sel_comuna
            MySqlConnector mysql = new MySqlConnector();
            mysql.ConnectionString = HttpContext.Current.Session["cnString"].ToString();
            mysql.AddProcedure("sp_sel_comuna");
            mysql.ParametersFromJson(json);

            return mysql.ExecQuery().ToJson();
        }

        [WebMethod]
        public static string GetRegion(string json)
        {
            //sp_sel_comuna
            MySqlConnector mysql = new MySqlConnector();
            mysql.ConnectionString = HttpContext.Current.Session["cnString"].ToString();
            mysql.AddProcedure("sp_sel_region");
            mysql.ParametersFromJson(json);

            return mysql.ExecQuery().ToJson();
        }

        [WebMethod]
        public static string GetEmpresa(string json)
        {
            //sp_sel_comuna

            MySqlConnector mysql = new MySqlConnector();
            mysql.ConnectionString = HttpContext.Current.Session["cnString"].ToString();
            mysql.AddProcedure("sp_sel_empresa");
            mysql.ParametersFromJson(json);


            return mysql.ExecQuery().ToJson();
        }

        [WebMethod]
        public static string GetGiro(string json)
        {
            //sp_sel_comuna

            MySqlConnector mysql = new MySqlConnector();
            mysql.ConnectionString = HttpContext.Current.Session["cnString"].ToString();
            mysql.AddProcedure("sp_sel_giro");
            mysql.ParametersFromJson(json);


            return mysql.ExecQuery().ToJson();
        }


        [WebMethod]
        public static string GetDetalleProducto(string RegInicio,string RegFin)
        {

            var Id_emp = HttpContext.Current.Session["Id_emp"].ToString();
            MySqlConnector mysql = new MySqlConnector();
            mysql.ConnectionString = HttpContext.Current.Session["cnString"].ToString();
            mysql.AddProcedure("sp_sel_detalle_producto");
            mysql
                .AddParameter("Id_Empresa", Id_emp)
                .AddParameter("Inicio", RegInicio)
                .AddParameter("Fin",RegFin);

            return mysql.ExecQuery().ToJson();
        }

        [WebMethod]
        public static string InsertProducto(string json)
        {

            if (!string.IsNullOrEmpty(json))
            {
                MySqlConnector mysql = new MySqlConnector();
                mysql.ConnectionString = HttpContext.Current.Session["cnString"].ToString();
                mysql.AddProcedure("sp_ins_detalle_producto");

                mysql.ParametersFromJson(json);

                return mysql.ExecQuery().ToJson();
            }
            else {
                return "ERROR";
            }     
        }

        [WebMethod]
        public static string InsertReceptor(string json)
        {

            if (!string.IsNullOrEmpty(json))
            {
                MySqlConnector mysql = new MySqlConnector();
                mysql.ConnectionString = HttpContext.Current.Session["cnString"].ToString();
                mysql.AddProcedure("sp_ins_receptor");

                mysql.ParametersFromJson(json);

                return mysql.ExecQuery().ToJson();
            }
            else {
                return "ERROR";
            }
        }

        [WebMethod]
        public static string GetReceptor(string json)
        {
            if (!string.IsNullOrEmpty(json))
            {
                MySqlConnector mysql = new MySqlConnector();
                mysql.ConnectionString = HttpContext.Current.Session["cnString"].ToString();
                mysql.AddProcedure("sp_sel_receptor");

                mysql.ParametersFromJson(json);

                return mysql.ExecQuery().ToJson();
            }
            else {
                return "ERROR";
            }
        }
        [WebMethod]
        public static string CreateXML_DTE(MakeDte.DTE dte/*,MakeDte.Detalle detalle*/)
        {
            MakeDte m = new MakeDte();
            XDocument x = new XDocument();
            x = m.Serialize(dte);

            pdfSII pdf = new pdfSII();
            pdf.MakeXsl(m.ToXmlDocument(x), @"C:\Nueva carpeta (2)\default.xslt");
            pdf.MakePdf(@"C:\Nueva carpeta (2)\hola.pdf");

            return "";
        }


    }
}