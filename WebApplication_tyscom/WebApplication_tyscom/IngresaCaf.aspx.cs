using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

namespace WebApplication_tyscom
{
    public partial class IngresaCaf : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        string caf_load = "";
        public void UploadDocument(object sender, EventArgs e)
        {
            try
            {
                if (fuDocument.HasFile)
                {
                    string nombre_archivo = fuDocument.FileName.ToString();
                    MemoryStream m = new MemoryStream(fuDocument.FileBytes);

                    XmlDocument xml = new XmlDocument();
                    xml.Load(m);

                    caf_load = xml.InnerXml.ToString();
                    DataSet ds = new DataSet();
                    ds.ReadXml(XmlReader.Create(new StringReader(caf_load)));
                    Controlador.Session s = new Controlador.Session();
                    s._caf = caf_load;
                    Session["CAF"] = s;
                    string comprobacion = ds.Tables["AUTORIZACION"].Rows[0][1].ToString();

                    comprobacion.IndexOf("-----BEGIN RSA PRIVATE KEY-----");

                    txt_rut_emp.Text = ds.Tables["DA"].Rows[0][0].ToString();
                    txt_nom_emp.Text = ds.Tables["DA"].Rows[0][1].ToString();
                    txt_tipo_doc.Text = ds.Tables["DA"].Rows[0][2].ToString();
                    txt_folio_desde.Text = ds.Tables["RNG"].Rows[0][0].ToString();
                    txt_folio_hasta.Text = ds.Tables["RNG"].Rows[0][1].ToString();
                    txt_fecha_aut.Text = ds.Tables["DA"].Rows[0][4].ToString();

                    Label2.Text = nombre_archivo;
                    Label2.Visible = true;
                    //

                    //           
                }

            }
            catch (NullReferenceException null_ex)
            {

                Label1.Visible = true;
            }
        }

        protected void LinkButton5_Click(object sender, EventArgs e)
        {
            var serv = new ServFact.ServicioFacturaClient();
            Controlador.Session s = new Controlador.Session();
            string rut = s.get_sessionRUT();
            string caf = s.get_caf();
            serv.ingresa_Caf(caf, rut);

        }
    }
}