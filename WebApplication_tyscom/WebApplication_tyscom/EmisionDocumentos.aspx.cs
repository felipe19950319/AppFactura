using Controlador;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using Newtonsoft.Json;

namespace WebApplication_tyscom
{
    public partial class EmisionDocumentos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session s = new Session();
            if (s.get_tpo_doc() == null) {
                Response.Redirect("Seleccion_Docs.apsx");
            }
            if ( string.IsNullOrEmpty(s.get_nombre_emp()))
            {
                Response.Redirect("Seleccion_Empresa.aspx");
            }
            if (string.IsNullOrEmpty( s.get_id_emp()) )
            {
                Response.Redirect("Seleccion_Empresa.aspx");
            }


            if (!this.IsPostBack)
            {    //<detalle> datatable 
                #region
                DataTable dt = new DataTable();
                dt.TableName = ("DETALLES");
                dt.Columns.AddRange(new DataColumn[37] {
                    new DataColumn("NroLinDet"),
                    new DataColumn("TpoCodigo"),
                    new DataColumn("VlrCodigo"),
                    new DataColumn("IndExe"),
                    new DataColumn("IndAgente"),
                    new DataColumn("MntBaseFaena"),
                    new DataColumn("MntMargComer"),
                    new DataColumn("PrcConsFinal"),
                    new DataColumn("NmbItem"),
                    new DataColumn("DscItem"),
                    new DataColumn("QtyRef"),
                    new DataColumn("UnmdRef"),
                    new DataColumn("PrcRef"),
                    new DataColumn("QtyItem"),
                    new DataColumn("SubQty"),
                    new DataColumn("SubCod"),
                    new DataColumn("FchElabor"),
                    new DataColumn("FchVencim"),
                    new DataColumn("UnmdItem"),
                    new DataColumn("PrcItem"),
                    new DataColumn("PrcOtrMon"),
                    new DataColumn("Moneda"),
                    new DataColumn("FctConv"),
                    new DataColumn("DctoOtrMnda"),
                    new DataColumn("RecargoOtrMnda"),
                    new DataColumn("MontoItemOtrMnda"),
                    new DataColumn("DescuentoPct"),
                    new DataColumn("DescuentoMonto"),
                    new DataColumn("TipoDscto"),
                    new DataColumn("ValorDscto"),
                    new DataColumn("RecargoPct"),
                    new DataColumn("RecargoMonto"),
                    new DataColumn("TipoRecargo"),
                    new DataColumn("ValorRecargo"),
                    new DataColumn("CodImpAdic"),
                    new DataColumn("MontoItem"),
                    new DataColumn("AUX")
                });
                ViewState["DETALLES"] = dt;
                GridView1.DataSource = (DataTable)ViewState["DETALLES"];
                GridView1.DataBind();
                #endregion
                //</detalle>

                //<referencias> datatable
                #region
                DataTable dt_ref = new DataTable();
                dt_ref.TableName = ("REFERENCIAS");
                dt_ref.Columns.AddRange(new DataColumn[8] {
                    new DataColumn("NroLinRef"),
                    new DataColumn("TpoDocRef"),
                    new DataColumn("IndGlobal"),
                    new DataColumn("FolioRef"),
                    new DataColumn("RUTOtr"),
                    new DataColumn("FchRef"),
                    new DataColumn("CodRef"),
                    new DataColumn("RazonRef")
               });
                ViewState["REFERENCIAS"] = dt_ref;
                Grid_Referencia.DataSource = (DataTable)ViewState["REFERENCIAS"];
                Grid_Referencia.DataBind();
                #endregion
                //</referencias>

                //<desc_global>
                #region
                DataTable dt_desc_global = new DataTable();
                dt_desc_global.TableName = ("DESCUENTO_GLOBAL");
                dt_desc_global.Columns.AddRange(new DataColumn[7] {
                    new DataColumn("NroLinDR"),
                    new DataColumn("TpoMov"),
                    new DataColumn("GlosaDR"),
                    new DataColumn("TpoValor"),
                    new DataColumn("ValorDR"),
                    new DataColumn("ValorDROtrMnda"),
                    new DataColumn("IndExeDR")
               });
                ViewState["DESCUENTO_GLOBAL"] = dt_desc_global;
                #endregion
                //</desc_global >
                txt_sub_uni.Attributes.Add("readonly", "readonly");
                txt_desc_rec.Text = "0";
                txt_cantidad.Text = "0";          
            }
            //
       
            var serv = new ServFact.ServicioFacturaClient();
            TextArea_clientes.Value = serv.ObtieneListaClientes(Int32.Parse( s.get_id_emp()));
            TextArea_detalles.Value = serv.ObtieListaDetalle(Int32.Parse( s.get_id_emp()));

                   
            string tipodoc = s.get_tpo_doc();
            List_Tipo_Doc.SelectedValue = tipodoc;


            if (List_Tipo_Doc.SelectedValue.ToString() == "34")
            {
                List_iva.SelectedIndex = List_iva.Items.IndexOf(List_iva.Items.FindByText("NO"));
                List_iva.Enabled = false;
            }

            //
            int id_emp =Int32.Parse( s.get_id_emp());
            string json = s.get_ListaEmpresas();
            DataTable dt_list_emp = (DataTable)JsonConvert.DeserializeObject(json, (typeof(DataTable)));
            var empresa_a_usar = from x in dt_list_emp.AsEnumerable()
                                 where x.Field<Int64>("ID_EMPESA") == id_emp
                                 select new
                                 {
                                     rut_emis = x.Field<string>("RUT_EMPRESA"),
                                     rzn_emis= x.Field<string>("RAZON_SOCIAL"),
                                     fecha= x.Field<string>("FECHA_RESOLUCION"),
                                     giro= x.Field<string>("GIRO"),
                                     direccion= x.Field<string>("DIRECCION"),
                                     comuna= x.Field<string>("COMUNA"),
                                     ciudad= x.Field<string>("CIUDAD")
                                 };
         


            string rut_session = s.get_sessionRUT();
            string xml_get = serv.get_setDTE(serv.get_empresaRUT(rut_session));

           /* DataSet ds = new DataSet();
            ds.ReadXml(XmlReader.Create(new StringReader(xml_get)));
            */
            txt_rut_emisor.Text = empresa_a_usar.FirstOrDefault().rut_emis;
            txt_rut_envia.Text = rut_session;
            txt_rzn_emis.Text = empresa_a_usar.FirstOrDefault().rzn_emis;
            txt_fecha.Text = empresa_a_usar.FirstOrDefault().fecha;
            txt_giro_emisor.Text = empresa_a_usar.FirstOrDefault().giro;
            txt_direccion_emisor.Text = empresa_a_usar.FirstOrDefault().direccion;
            txt_comuna_emisor.Text = empresa_a_usar.FirstOrDefault().comuna;
            txt_ciudad_emisor.Text = empresa_a_usar.FirstOrDefault().ciudad;
        }


        private void add_desc_global()
        {


        }
        public int redondear(double algo)
        {
            int redondear = 0;
            algo = Math.Round(algo, 1, MidpointRounding.AwayFromZero);
            algo = Math.Round(algo, 0, MidpointRounding.AwayFromZero);
            redondear = int.Parse(algo.ToString());
            return redondear;
        }
        protected void LinkButton5_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)ViewState["DETALLES"];
            string total = (Int32.Parse(txt_precio_u.Text.Trim()) * Int32.Parse(txt_cantidad.Text.Trim())).ToString();

            DataRow dr = dt.NewRow();

            string dscto = "";
            string recargo = "";
            string dscto_monto = "";
            string recargo_monto = "";
            string precio = txt_precio_u.Text.ToString();

            if (list_dcto_recargo.SelectedItem.Text.ToString() == "+%")
            {
                recargo = txt_desc_rec.Text.ToString();
                recargo_monto = ((double.Parse(precio) * double.Parse(recargo)) / 100).ToString();
            }

            if (list_dcto_recargo.SelectedItem.Text.ToString() == "-%")
            {
                dscto = txt_desc_rec.Text.ToString();
                dscto_monto = ((double.Parse(precio) * double.Parse(dscto)) / 100).ToString();
            }

            dr["NroLinDet"] = dt.Rows.Count;
            dr["TpoCodigo"] = "INT1";
            dr["VlrCodigo"] = txt_codigo.Text.Trim();
            dr["IndExe"] = "";
            dr["IndAgente"] = "";
            dr["MntBaseFaena"] = "";
            dr["MntMargComer"] = "";
            dr["PrcConsFinal"] = "";
            dr["NmbItem"] = txt_nombre.Text.Trim();
            dr["DscItem"] = txt_descripcion.Text.Trim();
            dr["QtyRef"] = "";
            dr["UnmdRef"] = "";
            dr["PrcRef"] = "";
            dr["QtyItem"] = txt_cantidad.Text.Trim();
            dr["SubQty"] = "";
            dr["SubCod"] = "";
            dr["FchElabor"] = "";
            dr["FchVencim"] = "";
            dr["UnmdItem"] = list_u_medida.Text;
            dr["PrcItem"] = txt_precio_u.Text;
            dr["PrcOtrMon"] = "";
            dr["Moneda"] = "";
            dr["FctConv"] = "";
            dr["DctoOtrMnda"] = "";
            dr["RecargoOtrMnda"] = "";
            dr["MontoItemOtrMnda"] = "";
            if (dscto == "0")
            {
                dscto = "";
            }
            if (dscto_monto == "0")
            {
                dscto_monto = "";
            }
            if (recargo == "0")
            {
                recargo = "";
            }
            if (recargo_monto == "0") { recargo_monto = ""; }

            dr["DescuentoPct"] = dscto;
            dr["DescuentoMonto"] = dscto_monto;
            dr["TipoDscto"] = "";
            dr["ValorDscto"] = "";
            dr["RecargoPct"] = recargo;
            dr["RecargoMonto"] = recargo_monto;
            dr["TipoRecargo"] = "";
            dr["ValorRecargo"] = "";
            dr["CodImpAdic"] = "";
            dr["MontoItem"] = txt_sub_uni.Text.ToString();
            dr["AUX"] = List_iva.SelectedItem.Text.ToString();

            dt.Rows.Add(dr);

            ViewState["DETALLES"] = dt;
            GridView1.DataSource = (DataTable)ViewState["DETALLES"];
            GridView1.DataBind();

            dt = (DataTable)ViewState["DETALLES"];



            double neto = 0;
            double exento = 0;

            if (List_iva.SelectedItem.Text.ToString() == "SI")
            {
                _txt_tasa_iva.Text = "19";

                if (txt_mnt_neto.Text.ToString() == string.Empty)
                {
                    txt_mnt_neto.Text = "0";
                }
                if (_txt_excento.Text.ToString() == string.Empty)
                {
                    _txt_excento.Text = "0";
                }


                neto = double.Parse(dr["MontoItem"].ToString()) + double.Parse(txt_mnt_neto.Text);
                txt_mnt_neto.Text = neto.ToString();


                txt_total.Text = redondear((double.Parse(txt_mnt_neto.Text.ToString()) * 1.19) + double.Parse(_txt_excento.Text.ToString())).ToString();
                txt_iva.Text = redondear((((double.Parse(txt_mnt_neto.Text.ToString()) * 19) / 100))).ToString();

            }

            if (List_iva.SelectedItem.Text.ToString() == "NO")
            {
                if (txt_mnt_neto.Text.ToString() == string.Empty)
                {
                    txt_mnt_neto.Text = "0";
                }
                if (_txt_excento.Text.ToString() == string.Empty)
                {
                    _txt_excento.Text = "0";
                }
                exento = double.Parse(dr["MontoItem"].ToString()) + double.Parse(_txt_excento.Text.ToString());
                _txt_excento.Text = exento.ToString();

                txt_total.Text = ((double.Parse(txt_mnt_neto.Text.ToString()) * 1.19) + double.Parse(_txt_excento.Text.ToString())).ToString();
                txt_iva.Text = (((double.Parse(txt_mnt_neto.Text.ToString()) * 19) / 100).ToString());

            }

            GridView1.DataBind();
        }

        public void generar_documento()
        {

            var serv = new ServFact.ServicioFacturaClient();
            var setdte = new ServFact.SetDTE();


            string tipo_doc = List_Tipo_Doc.SelectedValue.ToString();
            string time_firma = DateTime.Now.ToString("yyy/MM/dd" + "T" + "HH:mm:ss").Replace('/', '-');
            string fecha_emis = DateTime.Now.ToString("yyy/MM/dd").Replace('/', '-');

            Session s = new Session();
            string rut_session = s.get_sessionRUT();
            string xml_get = serv.get_setDTE(serv.get_empresaRUT(rut_session));

            DataSet ds = new DataSet();
            ds.ReadXml(XmlReader.Create(new StringReader(xml_get)));
            setdte.RutEmisor = ds.Tables["get_setDTE"].Rows[0][0].ToString();
            setdte.RutEnvia = rut_session;
            setdte.receptor = txt_rut_recep.Text.ToString();
            setdte.FchResol = ds.Tables["get_setDTE"].Rows[0][2].ToString();
            setdte.NroResol = "0";
            setdte.TmstFirmmaEnv = time_firma;
            setdte.TpoDTE = tipo_doc;
            setdte.NroDTE = "1";

            //
            DataTable dt = (DataTable)ViewState["DETALLES"];
            int i = -1;
            foreach (DataRow row in dt.Rows)
            {
                i++;
                row["NroLinDet"] = i;
            }
            GridView1.DataSource = dt;
            GridView1.DataBind();
            //
            DataTable dt_ref = (DataTable)ViewState["REFERENCIAS"];

            int Folio = serv.get_num_folio(rut_session, Int32.Parse(tipo_doc));

            var dte_id = new ServFact.DTE_idDoc();
            var dte_emis = new ServFact.DTE_Emisor();
            var dte_recep = new ServFact.DTE_Receptor();
            var dte_trans = new ServFact.DTE_Transporte();
            var dte_total = new ServFact.DTE_Totales();

            /* string uri = ("DOC" + Folio.ToString() + "" + time_firma.Replace(":", ""));
             string ID = uri;
             ID = ID.Replace("-", "");
             dte_id.ID = ID;*/
            dte_id.TipoDTE = tipo_doc;
            dte_id.Folio = Folio.ToString();
            dte_id.FchEmis = fecha_emis;
            dte_emis.RUTEmisor = ds.Tables["get_setDTE"].Rows[0][0].ToString();
            dte_emis.RznSoc = ds.Tables["get_setDTE"].Rows[0][1].ToString();
            dte_emis.GiroEmis = txt_giro_emisor.Text.ToString();
            dte_emis.Acteco = "726000";
            dte_emis.CdgSIISucur = "12345";
            dte_emis.DirOrigen = ds.Tables["get_setDTE"].Rows[0][2].ToString();
            dte_emis.CmnaOrigen = ds.Tables["get_setDTE"].Rows[0][3].ToString();
            dte_emis.CiudadOrigen = ds.Tables["get_setDTE"].Rows[0][4].ToString();
            ///EMISOR
            ///RECEPTOR
            dte_recep.RUTRecep = txt_rut_recep.Text.ToString();
            dte_recep.RznSocRecep = txt_rzn_recep.Text.ToString();
            dte_recep.GiroRecep = txt_giro_recep.Text.ToString();
            dte_recep.DirRecep = txt_dir_recep.Text.ToString();
            dte_recep.CmnaRecep = txt_comuna_recep.Text.ToString();
            dte_recep.CiudadRecep = txt_ciudad_recep.Text.ToString();
            ///RECEPTOR
            ///TOTALES
            ///
            string mnt_neto = txt_mnt_neto.Text.ToString();
            string mntexe = _txt_excento.Text.ToString();
            string tasaiva = _txt_tasa_iva.Text.ToString();
            string iva = txt_iva.Text.ToString();
            if (List_Tipo_Doc.SelectedValue.ToString() == "34")
            {
                mnt_neto = "";
                tasaiva = "";
                iva = "";
            }
            dte_total.MntNeto = mnt_neto;
            dte_total.MntExe = mntexe;
            dte_total.TasaIVA = tasaiva;
            dte_total.IVA = iva;
            dte_total.MntTotal = txt_total.Text.ToString();
            ///TOTALEs
            string tiempo = time_firma.Replace(":", "_");


            ///cuando no se ocupen arrays se pasan al parametro inicializado en 0
           ServFact.DTE_SubTotInfo[] sub = new ServFact.DTE_SubTotInfo[0];
            DataTable dt_desc_rec = (DataTable)ViewState["DESCUENTO_GLOBAL"];
            // svFact1.DTE_referencia[] refer = new svFact1.DTE_referencia[0];
          ServFact.DTE_comisiones[] comi = new ServFact.DTE_comisiones[0];

            Listas l = new Listas();



            serv.Genera_dte_envia_(
                setdte,
                dte_id,
                dte_emis,
                dte_recep,
                dte_trans,
                dte_total,
                serv.det(dt),
                sub,
                l.desc_global(dt_desc_rec),
                l.refer(dt_ref),
                comi,
                rut_session,
                tipo_doc,
                tiempo
                );
            /*   DataSet ds1 = new DataSet();
               ds1.ReadXml(XmlReader.Create(new StringReader(xml_nombres)));
               string nom_dte = ds1.Tables["RUTAS"].Rows[0][0].ToString();
               string nom_set_dte = ds1.Tables["RUTAS"].Rows[0][1].ToString();
               string nom_doc_firm = ds1.Tables["RUTAS"].Rows[0][2].ToString();

               serv.procedimientofirma(nom_set_dte, nom_dte, rut_session, nom_doc_firm, uri);

               string rutemis_env = ds.Tables["get_setDTE"].Rows[0][0].ToString().Replace("-", "");
               string respuesta = serv.upload_sii(rutemis_env, rutemis_env, nom_doc_firm);
               serv.administrador_folios(Folio, respuesta, rut_session, Int32.Parse(tipo_doc));*/
        }


        protected void LinkButton_ref_Click(object sender, EventArgs e)
        {
            if (
             string.IsNullOrEmpty(txt_folio_ref.Text) ||
             string.IsNullOrEmpty(txt_fecha_ref.Text)
             )
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Por Favor Complete Todos los Campos de Referencia');", true);
            }
            else
            {
                DataTable dt_ref = (DataTable)ViewState["REFERENCIAS"];
                DataRow dr = dt_ref.NewRow();
                dr["NroLinRef"] = dt_ref.Rows.Count;
                dr["TpoDocRef"] = list_documentos.SelectedValue.ToString();

                dr["IndGlobal"] = "";
                dr["FolioRef"] = txt_folio_ref.Text.Trim();
                dr["RUTOtr"] = "";
                dr["FchRef"] = txt_fecha_ref.Text.Trim();
                string prubea = ListCodRef.SelectedValue.ToString();
                dr["CodRef"] = ListCodRef.SelectedValue.ToString();
                dr["RazonRef"] = ListCodRef.SelectedItem.ToString();

                dt_ref.Rows.Add(dr);

                ViewState["REFERENCIAS"] = dt_ref;
                Grid_Referencia.DataSource = (DataTable)ViewState["REFERENCIAS"];
                Grid_Referencia.DataBind();

                dt_ref = (DataTable)ViewState["REFERENCIAS"];
            }
            Grid_Referencia.DataBind();
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            DataTable dt = (DataTable)ViewState["DETALLES"];
            dt.Rows.RemoveAt(e.RowIndex);
            int i = -1;
            foreach (DataRow row in dt.Rows)
            {
                i++;
                row["NroLinDet"] = i;
            }
            GridView1.DataSource = dt;
            GridView1.DataBind();
            ViewState["DETALLES"] = dt;
            GridView1.DataSource = (DataTable)ViewState["DETALLES"];
            GridView1.DataBind();
        }

        protected void Grid_Referencia_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

            DataTable dt_ref = (DataTable)ViewState["REFERENCIAS"];
            dt_ref.Rows.RemoveAt(e.RowIndex);
            int i = -1;
            foreach (DataRow row in dt_ref.Rows)
            {
                i++;
                row["NroLinRef"] = i;
            }
            GridView1.DataSource = dt_ref;
            GridView1.DataBind();
            Grid_Referencia.DataSource = (DataTable)ViewState["REFERENCIAS"];
            Grid_Referencia.DataBind();
        }

        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            generar_documento();
        }

        protected void desc_recargo_Click(object sender, EventArgs e)
        {
            int exento = 0;
            int afecto = 0;

            DataTable dt_detalles = (DataTable)ViewState["DETALLES"];
            dt_detalles.DataSet.Clear();

            afecto = dt_detalles.AsEnumerable().Where(x => x.Field<string>("AUX") == "SI").Count();
            exento = dt_detalles.AsEnumerable().Where(x => x.Field<string>("AUX") == "NO").Count();


            DataTable dt_desc_rec = (DataTable)ViewState["DESCUENTO_GLOBAL"];
            DataRow dr;
            /* dr["NroLinDR"] = "";
             dr["TpoMov"] = "";
             dr["GlosaDR"] = "";
             dr["TpoValor"] = "";
             dr["ValorDR"] = "";
             dr["ValorDROtrMnda"] = "";
             dr["IndExeDR"] = "";
             */
            if (afecto > 0)
            {
                dr = dt_desc_rec.NewRow();

                dr["NroLinDR"] = dt_desc_rec.Rows.Count.ToString();
                dr["TpoMov"] = "D";
                dr["TpoValor"] = (list_desc_rec_glob.SelectedItem.ToString()).Replace("+", "").Replace("-", "");
                dr["ValorDR"] = txt_desc_rec_glob.Text.ToString();
                dt_desc_rec.Rows.Add(dr);
            }

            if (exento > 0)
            {
                dr = dt_desc_rec.NewRow();
                dr["NroLinDR"] = dt_desc_rec.Rows.Count.ToString();
                dr["TpoMov"] = "D";
                dr["TpoValor"] = list_desc_rec_glob.SelectedItem.ToString();
                dr["ValorDR"] = txt_desc_rec_glob.Text.ToString();
                dr["IndExeDR"] = "1";
                dt_desc_rec.Rows.Add(dr);
            }
        }
    }
}