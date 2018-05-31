using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;
using System.Xml;
using System.Data;
using System.ComponentModel.DataAnnotations;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using WcfService_factura.clases;
using System.IO;
using System.ServiceModel.Activation;
using System.Web;
using LumenWorks.Framework.IO.Csv;
using Microsoft.VisualBasic.FileIO;
using System.Data.SqlClient;
using WcfService_factura.Cn_proxy_Upload;
using System.Collections;
using System.Xml.Linq;
using System.Runtime.InteropServices;
using System.Reflection;
using System.Web.UI.WebControls;


namespace WcfService_factura
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession)]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class ServicioFactura : IServicioFactura
    {
        //LISTAS 
        #region
        Listas l = new Listas();
        public DTE_DETALLE[] det(DataTable dt) {
            return l.det(dt);
        }

        public DTE_referencia[] refer(DataTable dt) {
            return l.refer(dt);
        }

        public DTE_DscrGlobal[] desc_global(DataTable dt){
            return l.desc_global(dt);
        }

        public Caratula[] caratula(DataTable dt) {
           return l.caratula(dt);      
        }

        public Totales_Periodo[] totales_periodo(DataTable dt){

            return l.totales_periodo(dt);
        }

        public Detalle_l_v[] det_libroCV(DataTable dt) {

           return l.det_libroCV(dt);
        }

        #endregion
        //db base de datos ! variable local 
        EntityFramework.Tyscom_FacturaEntities2 db = new EntityFramework.Tyscom_FacturaEntities2();

        public static DataTable Linq_to_dt(IEnumerable<dynamic> v)
        {
         
            var firstRecord = v.FirstOrDefault();
            if (firstRecord == null)
                return null;

            PropertyInfo[] infos = firstRecord.GetType().GetProperties();

            DataTable table = new DataTable();

            foreach (var info in infos)
            {
                Type propType = info.PropertyType;
                if (propType.IsGenericType
                    && propType.GetGenericTypeDefinition() == typeof(Nullable<>)) 
                {
                    table.Columns.Add(info.Name, Nullable.GetUnderlyingType(propType));
                }
                else
                {
                    table.Columns.Add(info.Name, info.PropertyType);
                }
            }
            DataRow row;
            foreach (var record in v)
            {
                row = table.NewRow();
                for (int i = 0; i < table.Columns.Count; i++)
                {
                    row[i] = infos[i].GetValue(record) != null ? infos[i].GetValue(record) : DBNull.Value;
                }
                table.Rows.Add(row);
            }
            table.AcceptChanges();
            return table;
        }

        public int redondear(decimal algo)
        {
            int redondear = 0;
            algo = Math.Round(algo, 1, MidpointRounding.AwayFromZero);
            algo = Math.Round(algo, 0, MidpointRounding.AwayFromZero);
            redondear = int.Parse(algo.ToString());
            return redondear;
        }

        public void genera_libro_v(string fecha_perdiodo,int id_empresa,string rut_empresa ) {

       
            DataSet ds = new DataSet();
           ds.ReadXml(XmlReader.Create(new StringReader(get_setDTE(rut_empresa))));

            using (Caratula c = new Caratula())
            {

                c.RutEmisorLibro = ds.Tables["get_setDTE"].Rows[0][0].ToString();
                c.RutEnvia = ds.Tables["get_setDTE"].Rows[0][1].ToString();
                c.PeriodoTributario = fecha_perdiodo;
                c.FchResol = ds.Tables["get_setDTE"].Rows[0][2].ToString();
                c.NroResol = "0";
                c.TipoOperacion = "EJEMPLO";
                c.TipoLibro = "EJEMPLO";
                c.TipoEnvio = "EJEMPLO";

                DataTable dt_tot = new DataTable();
                dt_tot.TableName = ("TotalesPeriodo");
                dt_tot.Columns.AddRange(new DataColumn[6] {
                    new DataColumn("TpoDoc"),
                    new DataColumn("TotDoc"),
                    new DataColumn("TotMntExe"),
                    new DataColumn("TotMntNeto"),
                    new DataColumn("TotMntIVA"),
                    new DataColumn("TotMntTotal")
               });
                var get_data_dte = from libro_c in db.DTE_EMISION
                                   where libro_c.FECHA_EMISION.Contains(fecha_perdiodo) && libro_c.ID_EMPESA == id_empresa
                                   select libro_c;

                DataTable dt = new DataTable();

                dt = Linq_to_dt(get_data_dte).Copy();
                int[] tpo_docs = { 29, 30, 32, 33, 34, 40, 43, 45, 46, 55, 56, 60, 61, 101, 110, 112 };
                //TOTALES PERIODO
                #region      
                foreach (int docs in tpo_docs)
                {
                    var tpo_doc = (from data in dt.AsEnumerable() where data.Field<int>("TIPO") == docs select data.Field<int>("TIPO")).FirstOrDefault();
                    var tot_doc = dt.AsEnumerable().Where(x => x.Field<int>("TIPO") == docs).Count();
                    var tot_mnt_exe = (from data in dt.AsEnumerable() where data.Field<int>("TIPO") == docs select data.Field<decimal>("MONTO_EXE")).Sum();
                    var tot_mnt_neto = (from data in dt.AsEnumerable() where data.Field<int>("TIPO") == docs select data.Field<decimal>("MONTO_NETO")).Sum();
                    var tot_mnt_iva = (from data in dt.AsEnumerable() where data.Field<int>("TIPO") == docs select data.Field<decimal>("MONTO_IVA")).Sum();
                    var tot_mnt_total = (from data in dt.AsEnumerable() where data.Field<int>("TIPO") == docs select data.Field<decimal>("MONTO_TOTAL")).Sum();

                    DataRow dr = dt_tot.NewRow();
                    dr["TpoDoc"] = tpo_doc;
                    dr["TotDoc"] = tot_doc;
                    dr["TotMntExe"] = redondear(tot_mnt_exe);
                    dr["TotMntNeto"] = redondear(tot_mnt_neto);
                    dr["TotMntIVA"] = redondear(tot_mnt_iva);
                    dr["TotMntTotal"] = redondear(tot_mnt_total);
                    dt_tot.Rows.Add(dr);
                }
                //iteracion para borra las filas que contengan datos en 0 
                for (int i = dt_tot.Rows.Count - 1; i >= 0; i--)
                {
                    DataRow dr = dt_tot.Rows[i];
                    if (dr["TpoDoc"].ToString() == "0" && dr["TotDoc"].ToString() == "0")
                    {
                        dr.Delete();
                    }
                }
                #endregion
                //TOTALES PERIODO 

                //DETALLE
                #region
                DataTable dt_det = new DataTable();
                dt_det.TableName = ("DETALLE");
                dt_det.Columns.AddRange(new DataColumn[9] {
                    new DataColumn("TpoDoc"),
                    new DataColumn("NroDoc"),
                    new DataColumn("FchDoc"),
                    new DataColumn("RUTDoc"),
                    new DataColumn("RznSoc"),
                    new DataColumn("MntExe"),
                    new DataColumn("MntNeto"),
                    new DataColumn("MntIVA"),
                    new DataColumn("MntTotal")
               });
                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    DataRow dr_det = dt_det.NewRow();

                    dr_det["TpoDoc"] = dt.Rows[j].Field<int>("TIPO").ToString();
                    dr_det["NroDoc"] = dt.Rows[j].Field<int>("NUM_FOLIO").ToString();

                    string fecha = dt.Rows[j].Field<string>("FECHA_EMISION").ToString();
                    fecha = fecha.Replace("_", ":").Replace("T", " ");
                    fecha = DateTime.Parse(fecha).ToString("yyyy-MM-dd");

                    dr_det["FchDoc"] = fecha;
                    dr_det["RUTDoc"] = "";
                    dr_det["RznSoc"] = "";
                    dr_det["MntExe"] = redondear(dt.Rows[j].Field<decimal>("MONTO_EXE")).ToString();
                    dr_det["MntNeto"] = redondear(dt.Rows[j].Field<decimal>("MONTO_NETO")).ToString();
                    dr_det["MntIVA"] = redondear(dt.Rows[j].Field<decimal>("MONTO_IVA")).ToString();
                    dr_det["MntTotal"] = redondear(dt.Rows[j].Field<decimal>("MONTO_TOTAL")).ToString();
                    dt_det.Rows.Add(dr_det);
                }





                #endregion

                ///generar libro de pruebas !!!!!!! ruta c:/nueva carpeta / doc.xml
                Libro_Compra_Venta(c, totales_periodo(dt_tot).ToList(), det_libroCV(dt_det).ToList());

                quitar_Tag("c:/Nueva Carpeta/doc.xml");

            }     
            //

        }

        public int get_num_folio(string rut_user, int tipo_doc)
        {
            int nro_actual = -1;
            string folio_desde = "";
            string folio_hasta = "";
            try
            {
                int id_empresa = Int32.Parse(get_empresa(rut_user));

                var get_caf = from CAF in db.CAF
                              where CAF.ID_EMPESA == id_empresa && CAF.ESTADO == "ACTIVO"
                              && CAF.TIPO_DOC_CAF == tipo_doc
                              select new
                              {
                                  CAF.ID_CAF,
                                  CAF.FOLIO_HASTA,
                                  CAF.FOLLIO_DESDE
                              };


                string _id_caf = get_caf.FirstOrDefault().ID_CAF.ToString();

                int id_caf = Int32.Parse(_id_caf);

                folio_hasta = get_caf.FirstOrDefault().FOLIO_HASTA.ToString();
                folio_desde = get_caf.FirstOrDefault().FOLLIO_DESDE.ToString();
                //
                var get__Tracking_caf = from TRACKING_CAF in db.TRACKING_CAF
                                        where TRACKING_CAF.ID_CAF == id_caf

                                        select new
                                        {
                                            TRACKING_CAF.NRO_ACTUAL,
                                            TRACKING_CAF.ESTADO,
                                            TRACKING_CAF.TRACKING_ESTADO_DTE
                                        };
                string num_actual = get__Tracking_caf.ToList().LastOrDefault().NRO_ACTUAL.ToString();
                string estado = get__Tracking_caf.ToList().LastOrDefault().ESTADO.ToString();
                string tracking_estado_dte = get__Tracking_caf.ToList().LastOrDefault().TRACKING_ESTADO_DTE.ToString();


                if (num_actual == string.Empty)
                {
                    num_actual = folio_desde;
                }
                int aux = Int32.Parse(num_actual);
                nro_actual = aux;
                nro_actual = (nro_actual + 1);
                if (estado == "7" || estado != "0" && tracking_estado_dte == "0")
                {

                    nro_actual = int.Parse(num_actual);

                }
                if (nro_actual == Int32.Parse(folio_hasta))
                {

                    nro_actual = -2;
                    if (nro_actual == -2)
                    {

                        var result = (from CAF in db.CAF
                                      where CAF.ID_EMPESA == id_empresa && CAF.ESTADO == "ACTIVO"
                           && CAF.TIPO_DOC_CAF == tipo_doc
                                      select CAF).SingleOrDefault();

                        result.ESTADO = "INACTIVO";

                        db.SaveChanges();
                    }
                }
                return nro_actual;
            }

            catch (NullReferenceException)
            {
                if (nro_actual == -1)
                {
                    nro_actual = Int32.Parse(folio_desde);
                }
                return nro_actual;
            }
            catch (FormatException)
            {

                nro_actual = -5;
                //-5 error 
                return nro_actual;
            }

        }

        public int administrador_folios(int nro_actual, string xml_respuesta, string rut_user, int tipo_doc,int id_dte)
        {
            int id_empresa = Int32.Parse(get_empresa(rut_user));

            var get_caf = from CAF in db.CAF
                          where CAF.ID_EMPESA == id_empresa && CAF.ESTADO == "ACTIVO"
                          && CAF.TIPO_DOC_CAF == tipo_doc
                          select new
                          {
                              CAF.ID_CAF,
                              CAF.FOLIO_HASTA,
                              CAF.FOLLIO_DESDE
                          };
            //
            int id_caf = get_caf.FirstOrDefault().ID_CAF;
            DataSet ds = new DataSet();
            ds.ReadXml(XmlReader.Create(new StringReader(xml_respuesta)));
            //captar respuesta de UPLOAD SII 
            string rut_envia = ds.Tables["RECEPCIONDTE"].Rows[0][0].ToString();
            string rut_empresa = ds.Tables["RECEPCIONDTE"].Rows[0][1].ToString();
            string archivo_enviado = ds.Tables["RECEPCIONDTE"].Rows[0][2].ToString();
            string tiempo_envio = ds.Tables["RECEPCIONDTE"].Rows[0][3].ToString();
            string status = ds.Tables["RECEPCIONDTE"].Rows[0][4].ToString();
            string track_id = ds.Tables["RECEPCIONDTE"].Rows[0][5].ToString();
            //
      
         
            var tracking_caf = new EntityFramework.TRACKING_CAF();

            tracking_caf.ID_CAF = id_caf;
            tracking_caf.ID_DTE = id_dte;
            tracking_caf.NRO_ACTUAL = nro_actual;
            tracking_caf.FECHA = DateTime.Parse(tiempo_envio).ToString();
            tracking_caf.ESTADO = status;
            tracking_caf.TRACKING_ESTADO_DTE = track_id;
            db.TRACKING_CAF.Add(tracking_caf);
            db.SaveChanges();

            return nro_actual;
        }

        public string get_token()
        {
            var cn = new Cn_Metodos();
            string token = cn.Recupera_Token();
            return token;
        }

        public string get_caf(string rut_user, int tipo_doc)
        {

            int id_empresa = Int32.Parse(get_empresa(rut_user));

            var get_caf = from CAF in db.CAF
                          where CAF.ID_EMPESA == id_empresa && CAF.ESTADO == "ACTIVO"
                          && CAF.TIPO_DOC_CAF == tipo_doc
                          select new
                          {
                              CAF.CAF1,
                              CAF.TRACKING_CAF,
                              CAF.ESTADO
                          };


            string caf_data = get_caf.FirstOrDefault().CAF1;


            return caf_data;
        }

        //metodo que carga datos de contribuyentes para actualizar la bd y tener datos como mail rut empresa etc
        //este metodo demora aprox 3:30 en actualizar la bd !
        //en produccion recordar cambiar el localhost! o actualizar el metodo a entity framework
        //este metodo se podria ocupar con un demonio "bot" encargado de ir abuscar el excel de contribuyentes y cargarlo a la bd utilizando este metodo!
        public bool xml_emp_to_db(string ruta_emp_contribuyente)
        {
            bool procedimiento = false;

            string connetionString = null;
            SqlConnection connection;
            SqlCommand command;
            SqlDataAdapter adpter = new SqlDataAdapter();
            DataSet ds = new DataSet();
            string sql = null;
            string rut_cont = null;
            string rzn_cont = null;
            string numero_resol = null;
            string fecha_resol = null;
            string mail = null;
            string url = null;
            connetionString = "Data Source=localhost;Initial Catalog=Tyscom_Factura;Integrated Security=true;";
            connection = new SqlConnection(connetionString);
            ds.ReadXml(ruta_emp_contribuyente);
            int i = 0;
            connection.Open();
            for (i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
            {
                rut_cont = ds.Tables["Empresa"].Rows[i].ItemArray[0].ToString();
                rzn_cont = ds.Tables["Empresa"].Rows[i].ItemArray[1].ToString();
                numero_resol = ds.Tables["Empresa"].Rows[i].ItemArray[2].ToString();
                fecha_resol = ds.Tables["Empresa"].Rows[i].ItemArray[3].ToString();
                mail = ds.Tables["Empresa"].Rows[i].ItemArray[4].ToString();
                url = ds.Tables["Empresa"].Rows[i].ItemArray[5].ToString();
                sql = ("insert into CONTRIBUYENTE_SII (RUT_CONTRIBUYENTE,RZN_CONTRIBUYENTE,FECHA_RESOLUCION,EMAIL_CONTRIBUYENTE,PAGINA_WEB_CONTRIBUYENTE) values (@RUT_CONTRIBUYENTE,@RZN_CONTRIBUYENTE,@FECHA_RESOLUCION,@EMAIL_CONTRIBUYENTE,@PAGINA_WEB_CONTRIBUYENTE)");
                command = new SqlCommand(sql, connection);
                command.Parameters.Add("@RUT_CONTRIBUYENTE", rut_cont);
                command.Parameters.Add("@RZN_CONTRIBUYENTE", rzn_cont);
                command.Parameters.Add("@FECHA_RESOLUCION", fecha_resol);
                command.Parameters.Add("@EMAIL_CONTRIBUYENTE", mail);
                command.Parameters.Add("@PAGINA_WEB_CONTRIBUYENTE", url);

                adpter.InsertCommand = command;
                if (adpter.InsertCommand.ExecuteNonQuery() == 1)
                {
                    procedimiento = true;
                }
                else
                {

                    procedimiento = false;
                }
            }

            return procedimiento;
            connection.Close();
        }

        public bool parse_csv_to_xml(string empresas_csv, string ruta_save_xml)
        {
            bool procedimiento;
            if (String.IsNullOrEmpty(empresas_csv) || String.IsNullOrEmpty(ruta_save_xml))
            {
                procedimiento = false;
            }
            else
            {
                var parse = new csv_to_xml();
                parse.csv_xml(empresas_csv, ruta_save_xml);
                procedimiento = true;
            }
            return procedimiento;
        }

        //METODOENCARGADO DE TRAER LOS DATOS DE SET DTE et;set;   
        public string get_setDTE(string rut_emp)
        {
          

            var emp = new EntityFramework.EMPRESA();

            var data =
    (from empresas in db.EMPRESA
     where empresas.RUT_EMPRESA == rut_emp
     select new
     {
         rut_empresa = empresas.RUT_EMPRESA,
         rzn_soc = empresas.RAZON_SOCIAL,

         fch_rez = empresas.FECHA_RESOLUCION,

         comuna = empresas.COMUNA,
         ciudad = empresas.CIUDAD,
         codigo_sii = empresas.CODIGO_SII_SUCUR,
         giro = empresas.GIRO,
         direccion = empresas.DIRECCION

     });
            string result = string.Join(".", data);

            int position0 = result.IndexOf("rut_empresa =") + "rut_empresa =".Length;
            int position1 = result.LastIndexOf(", rzn_soc =");
            string rut_empresa = result.Substring(position0, position1 - position0).Trim();

            position0 = result.IndexOf(", rzn_soc =") + ", rzn_soc =".Length;
            position1 = result.LastIndexOf(", fch_rez =");
            string rzn_soc = result.Substring(position0, position1 - position0).Trim();

            position0 = result.IndexOf(", fch_rez =") + ", fch_rez =".Length;
            position1 = result.LastIndexOf(", comuna =");
            string fch_rez = result.Substring(position0, position1 - position0).Trim();

            position0 = result.IndexOf(", comuna =") + ", comuna =".Length;
            position1 = result.LastIndexOf(", ciudad =");
            string comuna = result.Substring(position0, position1 - position0).Trim();

            position0 = result.IndexOf(", ciudad =") + ", ciudad =".Length;
            position1 = result.LastIndexOf(", codigo_sii =");
            string ciudad = result.Substring(position0, position1 - position0).Trim();

            position0 = result.IndexOf(", codigo_sii =") + ", codigo_sii =".Length;
            position1 = result.LastIndexOf(", giro =");
            string codigo_sii = result.Substring(position0, position1 - position0).Trim();

            position0 = result.IndexOf(", giro =") + ", giro =".Length;
            position1 = result.LastIndexOf(", direccion =");
            string giro = result.Substring(position0, position1 - position0).Trim();

            position0 = result.IndexOf(", direccion =") + ", direccion =".Length;
            position1 = result.LastIndexOf("}");
            string Direccion = result.Substring(position0, position1 - position0).Trim();

            string xml = "<?xml version=\"1.0\" encoding=\"ISO-8859-1\"?>" + "<get_setDTE><rut_empresa>" + rut_empresa + "</rut_empresa><rzn_soc>" + rzn_soc + "</rzn_soc><fch_rez>"
                + fch_rez + "</fch_rez><comuna>" + comuna + "</comuna><ciudad>" + ciudad + "</ciudad><codigo_sii>" + codigo_sii + "</codigo_sii><giro>" + giro + "</giro><direccion>" + Direccion + "</direccion></get_setDTE>";

            return xml;
            //return data.ToArray();         
        }

        public string upload_sii(string rutemisor, string rutempresa, string rutaxml)
        {
            // bool procedimiento;
            var cn = new Cn_proxy_Upload.Cn_Metodos();
            var e = new EnvioSii();
            string respuesta = e.enviarDoc(cn.Recupera_Token(), rutemisor, rutempresa, rutaxml);
            if (String.IsNullOrEmpty(respuesta))
            {

            }
            else
            {

            }
            return respuesta;
        }

        public int guarda_dte_emis(int id_emp, int tipo, int id_cliente
            , string fecha_emis, string estado_recep,string ruta_env_sii,
            string ruta_env_clie, decimal monto_neto,
            decimal monto_iva,
            decimal monto_exe,
            decimal monto_total
            )
        {
         

            DataSet ds = new DataSet();
            ds.ReadXml(ruta_env_sii);
      

           int num_folio=Int32.Parse( ds.Tables["IdDoc"].Rows[0][1].ToString());
          

            var dte_emis = new EntityFramework.DTE_EMISION();
            dte_emis.ID_EMPESA = id_emp;
            dte_emis.TIPO = tipo;
            dte_emis.ID_CLIENTE = id_cliente;
    
            dte_emis.FECHA_EMISION = fecha_emis;
            dte_emis.NUM_FOLIO = num_folio;
            dte_emis.ESTADO_RECEP = estado_recep;
            dte_emis.MONTO_EXE = monto_exe;
            dte_emis.MONTO_NETO = monto_neto;
            dte_emis.MONTO_IVA = monto_iva;
            dte_emis.MONTO_TOTAL = monto_total;
            dte_emis.RUTA_XML_ENV_SII = ruta_env_sii;
            dte_emis.RUTA_XML_ENV_CLIENTE = ruta_env_clie;

            db.DTE_EMISION.Add(dte_emis);
            db.SaveChanges();

            var get_id_doc = from doc_emision in db.DTE_EMISION
                             where doc_emision.ID_EMPESA == id_emp &&
doc_emision.TIPO == tipo && doc_emision.ID_CLIENTE == id_cliente && doc_emision.FECHA_EMISION == fecha_emis
                             select new
                             {
                                 doc_emision.ID_DTE
                             };
            string aux = get_id_doc.FirstOrDefault().ID_DTE.ToString();
            if (aux == string.Empty) {
                aux = "0";
            }

            int id_dte =Int32.Parse( aux);

            return id_dte;
        }

        //metodo que trabaja en conjunto con el cliente asignandole el string correspondiente a la sesion (get_empresa!)!
        public string get_empresaRUT(string rut_user)
        {
            try
            {
                int id_empresa = Int32.Parse(get_empresa(rut_user));


                var get_rut_emp = from EMPRESA in db.EMPRESA
                                  where EMPRESA.ID_EMPESA == id_empresa
                                  select new
                                  {
                                      EMPRESA.RUT_EMPRESA
                                  };
                string resultado = get_rut_emp.FirstOrDefault().RUT_EMPRESA.ToString();





                return resultado;
            }
            catch (NullReferenceException ex)
            {
                return ex.ToString();
            }
        }

        public string get_empresa(string rut)
        {
            try
            {          
                var consulta = from emp_usu in db.EMP_USU
                               join usu_ in db.USUARIO
                               on emp_usu.ID_USU equals usu_.ID_USU
                               where usu_.RUT == rut
                               select new {
                                   emp_usu.ID_EMP
                               };


                string algo = consulta.FirstOrDefault().ID_EMP.ToString();

                if (string.IsNullOrEmpty(algo))
                {
                    algo= "No se encontraron datos!";
                }
                return algo;
            }
            catch (NullReferenceException ex)
            {
                return ex.ToString();
            }


        }

        //ingresa caf rut es referido al rut de la sesion de el cliente ....
        public bool ingresa_Caf(string caf_data, string rut)
        {
            bool procedimiento = false;

            XmlDocument xml = new XmlDocument();
            xml.LoadXml(caf_data);

            string caf_text = xml.InnerXml.ToString();

            DataSet ds = new DataSet();
            ds.ReadXml((XmlReader.Create(new StringReader(caf_text))));

            //
            if (caf_data == "" || rut == "")
            {
                procedimiento = false;
            }
            /*    var _CAF = from CAF in consultas.CAF
                               where
                                 CAF.ID_EMPESA == id_emp &&
                                 CAF.CAF1==caf_text
                               select new
                               {

                                  CAF.CAF1
                               };
                string resultado = _CAF.FirstOrDefault().CAF1.ToString();

                if (resultado==caf_text){
                    procedimiento = false;
                }*/
            else
            {

                var caf = new EntityFramework.CAF();
                int id_emp = int.Parse(get_empresa(rut));

                // caf.ID_CAF = null;
                caf.ID_EMPESA = Int32.Parse(get_empresa(rut));
                caf.FOLLIO_DESDE = Int32.Parse(ds.Tables["RNG"].Rows[0][0].ToString());
                caf.FOLIO_HASTA = Int32.Parse(ds.Tables["RNG"].Rows[0][1].ToString());
                caf.FECHA_CARGA = DateTime.Now.ToLocalTime().ToString();
                caf.ESTADO = "ACTIVO";
                caf.CAF1 = caf_data;
                caf.TIPO_DOC_CAF = Int32.Parse(ds.Tables["DA"].Rows[0][2].ToString());

                db.CAF.Add(caf);
                db.SaveChanges();
                //
                procedimiento = true;
            }

            return procedimiento;

        }

        //metodo encargado de quitar las etiquetas vacias o unicas en el xml ! 
        public bool Limpia_tag_xml(string ruta_xml)
        {

            bool procedimiento = false;
            if (ruta_xml == "")
            {
                procedimiento = false;
            }
            else
            {
                var document = XDocument.Load(ruta_xml, LoadOptions.PreserveWhitespace);
                document.Descendants()
                        .Where(e => e.IsEmpty || String.IsNullOrWhiteSpace(e.Value))
                        .Remove();
                string xml = (document.ToString(SaveOptions.DisableFormatting));

                XmlDocument doc = new XmlDocument();
                doc.LoadXml(xml);
                doc.Save(ruta_xml);

                procedimiento = true;
            }
            return procedimiento;
        }

        public bool login(string rut, string password)
        {
            bool login;
            var User = db.USUARIO.FirstOrDefault(u => u.RUT == rut
                     && u.PASSWORD == password);
            if (User != null)    //Usuario Encontrado!
            {
                //Codigo...
                login = true;
            }
            else    //Usuario No encontrado
            {
                login = false;

            }
            return login;
        }

        public void valida_rut(string rut)
        {
            var validarut = new clases.Valida_Rut();

            if (validarut.ValidaRut(rut) == false)
            {
                throw new FaultException("'" + rut + "'NO ES VALIDO COMO RUT !");
            }

        }

        public void Libro_Compra_Venta(Caratula caratula, List<Totales_Periodo> totales_periodo, List<Detalle_l_v> det_l_v)
        {


            XmlTextWriter doc = new XmlTextWriter("c:/Nueva carpeta/doc.xml", Encoding.GetEncoding("iso-8859-1"));
            doc.Formatting = Formatting.Indented;
            doc.Indentation = 0;
            doc.WriteStartDocument();
            doc.WriteStartElement("LibroCompraVenta");
            doc.WriteStartAttribute("xmlns");
            doc.WriteValue("http://www.sii.cl/SiiDte");
            doc.WriteEndAttribute();

            doc.WriteStartAttribute("xmlns:xsi");
            doc.WriteValue("http://www.w3.org/2001/XMLSchema-instance");
            doc.WriteEndAttribute();

            doc.WriteStartAttribute("xsi:schemaLocation");
            doc.WriteValue("http://www.sii.cl/SiiDte LibroCV_v10.xsd");
            doc.WriteEndAttribute();

            doc.WriteStartAttribute("version");
            doc.WriteValue("1.0");
            doc.WriteEndAttribute();
            //<enviolibro>
            doc.WriteStartElement("EnvioLibro");
            doc.WriteStartAttribute("ID");
            doc.WriteValue("SetDoc");
            doc.WriteEndAttribute();

            string RutEmisorLibro = caratula.RutEmisorLibro;
            string RutEnvia = caratula.RutEnvia;
            string PeriodoTributario = caratula.PeriodoTributario;
            string FchResol = caratula.FchResol;
            string NroResol = caratula.NroResol;
            string TipoOperacion = caratula.TipoOperacion;
            string TipoLibro = caratula.TipoLibro;
            string TipoEnvio = caratula.TipoEnvio;
            string NroSegmento = caratula.NroSegmento;
            string FolioNotificacion = caratula.FolioNotificacion;
            string CodAutRec = caratula.CodAutRec;
            //<caratula>
            doc.WriteStartElement("Caratula");

            doc.WriteStartElement("RutEmisorLibro");
            doc.WriteValue(RutEmisorLibro);
            doc.WriteEndElement();

            doc.WriteStartElement("RutEnvia");
            doc.WriteValue(RutEnvia);
            doc.WriteEndElement();

            doc.WriteStartElement("PeriodoTributario");
            doc.WriteValue(PeriodoTributario);
            doc.WriteEndElement();

            doc.WriteStartElement("FchResol");
            doc.WriteValue(FchResol);
            doc.WriteEndElement();

            doc.WriteStartElement("NroResol");
            doc.WriteValue(NroResol);
            doc.WriteEndElement();

            doc.WriteStartElement("TipoOperacion");
            doc.WriteValue(TipoOperacion);
            doc.WriteEndElement();

            doc.WriteStartElement("TipoLibro");
            doc.WriteValue(TipoLibro);
            doc.WriteEndElement();

            doc.WriteStartElement("TipoEnvio");
            doc.WriteValue(TipoEnvio);
            doc.WriteEndElement();

            doc.WriteStartElement("NroSegmento");
            doc.WriteValue(NroSegmento);
            doc.WriteEndElement();

            doc.WriteStartElement("FolioNotificacion");
            doc.WriteValue(FolioNotificacion);
            doc.WriteEndElement();

            doc.WriteStartElement("CodAutRec");
            doc.WriteValue(CodAutRec);
            doc.WriteEndElement();

            doc.WriteEndElement();
            //</caratula>

            //<resumen_periodo>
            doc.WriteStartElement("ResumenPeriodo");



            //   </TotalesPeriodo>
            #region
            string TpoDoc = "";
            string TpoImp = "";
            string TotDoc = "";
            string TotAnulado = "";
            string TotOpExe = "";
            string TotMntExe = "";
            string TotMntNeto = "";
            string TotOpIVARec = "";
            string TotMntIVA = "";
            string TotOpActivoFijo = "";
            string TotMntActivoFijo = "";
            string TotMntIVAActivoFijo = "";
            string CodIVANoRec = "";
            string TotOpIVANoRec = "";
            string TotMntIVANoRec = "";
            string TotOpIVAUsoComun = "";
            string TotIVAUsoComun = "";
            string FctProp = "";
            string TotCredIVAUsoComun = "";
            string TotIVAFueraPlazo = "";
            string TotIVAPropio = "";
            string TotIVATerceros = "";
            string TotLey18211 = "";
            string CodImp = "";
            string TotMntImp = "";
            string TotImpSinCredito = "";
            string TotOpIVARetTotal = "";
            string TotIVARetTotal = "";
            string TotOpIVARetParcial = "";
            string TotIVARetParcial = "";
            string TotCredEC = "";
            string TotDepEnvase = "";
            string TotValComNeto = "";
            string TotValComExe = "";
            string TotValComIVA = "";
            string TotMntTotal = "";
            string TotOpIVANoRetenido = "";
            string TotIVANoRetenido = "";
            string TotMntNoFact = "";
            string TotMntPeriodo = "";
            string TotPsjNac = "";
            string TotPsjInt = "";
            string TotTabPuros = "";
            string TotTabCigarrillos = "";
            string TotTabElaborado = "";
            string TotImpVehiculo = "";
            foreach (Totales_Periodo _totales_periodo in totales_periodo)
            {

                TpoDoc = _totales_periodo.TpoDoc;
                TpoImp = _totales_periodo.TpoImp;
                TotDoc = _totales_periodo.TotDoc;
                TotAnulado = _totales_periodo.TotAnulado;
                TotOpExe = _totales_periodo.TotOpExe;
                TotMntExe = _totales_periodo.TotMntExe;
                TotMntNeto = _totales_periodo.TotMntNeto;
                TotOpIVARec = _totales_periodo.TotOpIVARec;
                TotMntIVA = _totales_periodo.TotMntIVA;
                TotOpActivoFijo = _totales_periodo.TotOpActivoFijo;
                TotMntActivoFijo = _totales_periodo.TotMntActivoFijo;
                TotMntIVAActivoFijo = _totales_periodo.TotMntIVAActivoFijo;
                CodIVANoRec = _totales_periodo.CodIVANoRec;
                TotOpIVANoRec = _totales_periodo.TotOpIVARec;
                TotMntIVANoRec = _totales_periodo.TotMntIVANoRec;
                TotOpIVAUsoComun = _totales_periodo.TotOpIVAUsoComun;
                TotIVAUsoComun = _totales_periodo.TotIVAUsoComun;
                FctProp = _totales_periodo.FctProp;
                TotCredIVAUsoComun = _totales_periodo.TotCredIVAUsoComun;
                TotIVAFueraPlazo = _totales_periodo.TotIVAFueraPlazo;
                TotIVAPropio = _totales_periodo.TotIVAPropio;
                TotIVATerceros = _totales_periodo.TotIVATerceros;
                TotLey18211 = _totales_periodo.TotLey18211;
                CodImp = _totales_periodo.CodImp;
                TotMntImp = _totales_periodo.TotMntImp;
                TotImpSinCredito = _totales_periodo.TotImpSinCredito;
                TotOpIVARetTotal = _totales_periodo.TotOpIVARetTotal;
                TotIVARetTotal = _totales_periodo.TotIVARetTotal;
                TotOpIVARetParcial = _totales_periodo.TotOpIVARetParcial;
                TotIVARetParcial = _totales_periodo.TotIVARetParcial;
                TotCredEC = _totales_periodo.TotCredEC;
                TotDepEnvase = _totales_periodo.TotDepEnvase;
                TotValComNeto = _totales_periodo.TotValComNeto;
                TotValComExe = _totales_periodo.TotValComExe;
                TotValComIVA = _totales_periodo.TotValComIVA;
                TotMntTotal = _totales_periodo.TotMntTotal;
                TotOpIVANoRetenido = _totales_periodo.TotOpIVANoRetenido;
                TotIVANoRetenido = _totales_periodo.TotIVANoRetenido;
                TotMntNoFact = _totales_periodo.TotMntNoFact;
                TotMntPeriodo = _totales_periodo.TotMntPeriodo;
                TotPsjNac = _totales_periodo.TotPsjNac;
                TotPsjInt = _totales_periodo.TotPsjInt;
                TotTabPuros = _totales_periodo.TotTabPuros;
                TotTabCigarrillos = _totales_periodo.TotTabCigarrillos;
                TotTabElaborado = _totales_periodo.TotTabElaborado;
                TotImpVehiculo = _totales_periodo.TotImpVehiculo;

                doc.WriteStartElement("TotalesPeriodo");

                doc.WriteStartElement("TpoDoc");
                doc.WriteValue(TpoDoc);
                doc.WriteEndElement();

                doc.WriteStartElement("TpoImp");
                doc.WriteValue(TpoImp);
                doc.WriteEndElement();

                doc.WriteStartElement("TotDoc");
                doc.WriteValue(TotDoc);
                doc.WriteEndElement();

                doc.WriteStartElement("TotAnulado");
                doc.WriteValue(TotAnulado);
                doc.WriteEndElement();

                doc.WriteStartElement("TotOpExe");
                doc.WriteValue(TotOpExe);
                doc.WriteEndElement();

                doc.WriteStartElement("TotMntExe");
                doc.WriteValue(TotMntExe);
                doc.WriteEndElement();

                doc.WriteStartElement("TotMntNeto");
                doc.WriteValue(TotMntNeto);
                doc.WriteEndElement();

                doc.WriteStartElement("TotOpIVARec");
                doc.WriteValue(TotOpIVANoRec);
                doc.WriteEndElement();

                doc.WriteStartElement("TotMntIVA");
                doc.WriteValue(TotMntIVA);
                doc.WriteEndElement();

                doc.WriteStartElement("TotOpActivoFijo");
                doc.WriteValue(TotOpActivoFijo);
                doc.WriteEndElement();

                doc.WriteStartElement("TotMntActivoFijo");
                doc.WriteValue(TotMntActivoFijo);
                doc.WriteEndElement();

                doc.WriteStartElement("TotMntIVAActivoFijo");
                doc.WriteValue(TotMntIVAActivoFijo);
                doc.WriteEndElement();

                doc.WriteStartElement("TotIVANoRec");

                doc.WriteStartElement("CodIVANoRec");
                doc.WriteValue(CodIVANoRec);
                doc.WriteEndElement();
                doc.WriteStartElement("TotOpIVANoRec");
                doc.WriteValue(TotOpIVANoRec);
                doc.WriteEndElement();
                doc.WriteStartElement("TotMntIVANoRec");
                doc.WriteValue(TotMntIVANoRec);
                doc.WriteEndElement();

                doc.WriteEndElement();

                doc.WriteStartElement("TotOpIVAUsoComun");
                doc.WriteValue(TotOpIVAUsoComun);
                doc.WriteEndElement();

                doc.WriteStartElement("TotIVAUsoComun");
                doc.WriteValue(TotIVAUsoComun);
                doc.WriteEndElement();

                doc.WriteStartElement("FctProp");
                doc.WriteValue(FctProp);
                doc.WriteEndElement();

                doc.WriteStartElement("TotCredIVAUsoComun");
                doc.WriteValue(TotCredIVAUsoComun);
                doc.WriteEndElement();

                doc.WriteStartElement("TotIVAFueraPlazo");
                doc.WriteValue(TotIVAFueraPlazo);
                doc.WriteEndElement();

                doc.WriteStartElement("TotIVAPropio");
                doc.WriteValue(TotIVAPropio);
                doc.WriteEndElement();

                doc.WriteStartElement("TotIVATerceros");
                doc.WriteValue(TotIVATerceros);
                doc.WriteEndElement();

                doc.WriteStartElement("TotLey18211");
                doc.WriteValue(TotLey18211);
                doc.WriteEndElement();

                doc.WriteStartElement("TotOtrosImp");

                doc.WriteStartElement("CodImp");
                doc.WriteValue(CodImp);
                doc.WriteEndElement();
                doc.WriteStartElement("TotMntImp");
                doc.WriteValue(TotMntImp);
                doc.WriteEndElement();

                doc.WriteEndElement();

                doc.WriteStartElement("TotImpSinCredito");
                doc.WriteValue(TotImpSinCredito);
                doc.WriteEndElement();

                doc.WriteStartElement("TotOpIVARetTotal");
                doc.WriteValue(TotOpIVARetTotal);
                doc.WriteEndElement();

                doc.WriteStartElement("TotIVARetTotal");
                doc.WriteValue(TotIVARetTotal);
                doc.WriteEndElement();

                doc.WriteStartElement("TotOpIVARetParcial");
                doc.WriteValue(TotOpIVARetParcial);
                doc.WriteEndElement();

                doc.WriteStartElement("TotIVARetParcial");
                doc.WriteValue(TotIVARetParcial);
                doc.WriteEndElement();

                doc.WriteStartElement("TotCredEC");
                doc.WriteValue(TotCredEC);
                doc.WriteEndElement();

                doc.WriteStartElement("TotDepEnvase");
                doc.WriteValue(TotDepEnvase);
                doc.WriteEndElement();

                doc.WriteStartElement("TotLiquidaciones");

                doc.WriteStartElement("TotValComNeto");
                doc.WriteValue(TotValComNeto);
                doc.WriteEndElement();
                doc.WriteStartElement("TotValComExe");
                doc.WriteValue(TotValComExe);
                doc.WriteEndElement();
                doc.WriteStartElement("TotValComIVA");
                doc.WriteValue(TotValComIVA);
                doc.WriteEndElement();

                doc.WriteEndElement();

                doc.WriteStartElement("TotMntTotal");
                doc.WriteValue(TotMntTotal);
                doc.WriteEndElement();

                doc.WriteStartElement("TotOpIVANoRetenido");
                doc.WriteValue(TotOpIVANoRetenido);
                doc.WriteEndElement();

                doc.WriteStartElement("TotIVANoRetenido");
                doc.WriteValue(TotIVANoRetenido);
                doc.WriteEndElement();

                doc.WriteStartElement("TotMntNoFact");
                doc.WriteValue(TotMntNoFact);
                doc.WriteEndElement();

                doc.WriteStartElement("TotMntPeriodo");
                doc.WriteValue(TotMntPeriodo);
                doc.WriteEndElement();

                doc.WriteStartElement("TotPsjNac");
                doc.WriteValue(TotPsjNac);
                doc.WriteEndElement();

                doc.WriteStartElement("TotPsjInt");
                doc.WriteValue(TotPsjInt);
                doc.WriteEndElement();

                doc.WriteStartElement("TotTabPuros");
                doc.WriteValue(TotTabPuros);
                doc.WriteEndElement();

                doc.WriteStartElement("TotTabCigarrillos");
                doc.WriteValue(TotTabCigarrillos);
                doc.WriteEndElement();

                doc.WriteStartElement("TotTabElaborado");
                doc.WriteValue(TotTabElaborado);
                doc.WriteEndElement();

                doc.WriteStartElement("TotImpVehiculo");
                doc.WriteValue(TotImpVehiculo);
                doc.WriteEndElement();

                doc.WriteEndElement();
                //</TotalesSegmento>
            }
            #endregion

            doc.WriteEndElement();
            //</resumen_segmento>


            //    < detalle >
            string TpoDoc_ = "";
            string Emisor = "";
            string IndFactCompra = "";
            string NroDoc = "";
            string Anulado = "";
            string Operacion = "";
            string TpoImp_ = "";
            string TasaImp_ = "";
            string NumInt = "";
            string IndServicio = "";
            string IndSinCosto = "";
            string FchDoc = "";
            string CdgSIISucur = "";
            string RUTDoc = "";
            string RznSoc = "";
            string NumId = "";
            string Nacionalidad = "";
            string TpoDocRef = "";
            string FolioDocRef = "";
            string MntExe = "";
            string MntNeto = "";
            string MntIVA = "";
            string MntActivoFijo = "";
            string MntIVAActivoFijo = "";
            string CodIVANoRec_ = "";
            string MntIVANoRec = "";
            string IVAUsoComun = "";
            string IVAFueraPlazo = "";
            string IVAPropio = "";
            string IVATerceros = "";
            string Ley18211 = "";
            string CodImp_ = "";
            string MntImp = "";
            string MntSinCred = "";
            string IVARetTotal = "";
            string IVARetParcial = "";
            string CredEC = "";
            string DepEnvase = "";
            string RutEmisor = "";
            string ValComNeto = "";
            string ValComExe = "";
            string ValComIVA = "";
            string MntTotal = "";
            string IVANoRetenido = "";
            string MntNoFact = "";
            string MntPeriodo = "";
            string PsjNac = "";
            string PsjInt = "";
            string TabPuros = "";
            string TabCigarrillos = "";
            string TabElaborado = "";
            string ImpVehiculo = "";
            string TmstFirma = "";

            //<detalle>
            #region
            foreach (Detalle_l_v detalle_l_v in det_l_v)
            {
                TpoDoc_ = detalle_l_v.TpoDoc_;
                Emisor = detalle_l_v.Emisor;
                IndFactCompra = detalle_l_v.IndFactCompra;
                NroDoc = detalle_l_v.NroDoc;
                Anulado = detalle_l_v.Anulado;
                Operacion = detalle_l_v.Operacion;
                TpoImp_ = detalle_l_v.TpoImp_;
                TasaImp_ = detalle_l_v.TasaImp_;
                NumInt = detalle_l_v.NumInt;
                IndServicio = detalle_l_v.IndServicio;
                IndSinCosto = detalle_l_v.IndSinCosto;
                FchDoc = detalle_l_v.FchDoc;
                CdgSIISucur = detalle_l_v.CdgSIISucur;
                RUTDoc = detalle_l_v.RUTDoc;
                RznSoc = detalle_l_v.RznSoc;
                NumId = detalle_l_v.NumId;
                Nacionalidad = detalle_l_v.Nacionalidad;
                TpoDocRef = detalle_l_v.TpoDocRef;
                FolioDocRef = detalle_l_v.FolioDocRef;
                MntExe = detalle_l_v.MntExe;
                MntNeto = detalle_l_v.MntNeto;
                MntIVA = detalle_l_v.MntIVA;
                MntActivoFijo = detalle_l_v.MntActivoFijo;
                MntIVAActivoFijo = detalle_l_v.MntIVAActivoFijo;
                CodIVANoRec_ = detalle_l_v.CodIVANoRec_;
                MntIVANoRec = detalle_l_v.MntIVANoRec;
                IVAUsoComun = detalle_l_v.IVAUsoComun;
                IVAFueraPlazo = detalle_l_v.IVAFueraPlazo;
                IVAPropio = detalle_l_v.IVAPropio;
                IVATerceros = detalle_l_v.IVATerceros;
                Ley18211 = detalle_l_v.Ley18211;
                CodImp_ = detalle_l_v.CodImp_;
                MntImp = detalle_l_v.MntImp;
                MntSinCred = detalle_l_v.MntSinCred;
                IVARetTotal = detalle_l_v.IVARetTotal;
                IVARetParcial = detalle_l_v.IVARetParcial;
                CredEC = detalle_l_v.CredEC;
                DepEnvase = detalle_l_v.DepEnvase;
                RutEmisor = detalle_l_v.RutEmisor;
                ValComNeto = detalle_l_v.ValComNeto;
                ValComExe = detalle_l_v.ValComExe;
                ValComIVA = detalle_l_v.ValComIVA;
                MntTotal = detalle_l_v.MntTotal;
                IVANoRetenido = detalle_l_v.IVANoRetenido;
                MntNoFact = detalle_l_v.MntNoFact;
                MntPeriodo = detalle_l_v.MntPeriodo;
                PsjNac = detalle_l_v.PsjNac;
                PsjInt = detalle_l_v.PsjInt;
                TabPuros = detalle_l_v.TabPuros;
                TabCigarrillos = detalle_l_v.TabCigarrillos;
                TabElaborado = detalle_l_v.TabElaborado;
                ImpVehiculo = detalle_l_v.ImpVehiculo;
                TmstFirma = detalle_l_v.TmstFirma;

                doc.WriteStartElement("Detalle");

                doc.WriteStartElement("TpoDoc");
                doc.WriteValue(TpoDoc_);
                doc.WriteEndElement();

                doc.WriteStartElement("Emisor");
                doc.WriteValue(Emisor);
                doc.WriteEndElement();

                doc.WriteStartElement("IndFactCompra");
                doc.WriteValue(IndFactCompra);
                doc.WriteEndElement();

                doc.WriteStartElement("NroDoc");
                doc.WriteValue(NroDoc);
                doc.WriteEndElement();

                doc.WriteStartElement("Anulado");
                doc.WriteValue(Anulado);
                doc.WriteEndElement();

                doc.WriteStartElement("Operacion");
                doc.WriteValue(Operacion);
                doc.WriteEndElement();

                doc.WriteStartElement("TpoImp");
                doc.WriteValue(TpoImp_);
                doc.WriteEndElement();

                doc.WriteStartElement("TasaImp");
                doc.WriteValue(TasaImp_);
                doc.WriteEndElement();

                doc.WriteStartElement("NumInt");
                doc.WriteValue(NumInt);
                doc.WriteEndElement();

                doc.WriteStartElement("IndServicio");
                doc.WriteValue(IndServicio);
                doc.WriteEndElement();

                doc.WriteStartElement("IndSinCosto");
                doc.WriteValue(IndSinCosto);
                doc.WriteEndElement();

                doc.WriteStartElement("FchDoc");
                doc.WriteValue(FchDoc);
                doc.WriteEndElement();

                doc.WriteStartElement("CdgSIISucur");
                doc.WriteValue(CdgSIISucur);
                doc.WriteEndElement();

                doc.WriteStartElement("RUTDoc");
                doc.WriteValue(RUTDoc);
                doc.WriteEndElement();

                doc.WriteStartElement("RznSoc");
                doc.WriteValue(RznSoc);
                doc.WriteEndElement();

                //<extranjero>
                doc.WriteStartElement("Extranjero");

                doc.WriteStartElement("NumId");
                doc.WriteValue(NumId);
                doc.WriteEndElement();

                doc.WriteStartElement("Nacionalidad");
                doc.WriteValue(Nacionalidad);
                doc.WriteEndElement();

                doc.WriteEndElement();
                //</extranjero>

                doc.WriteStartElement("TpoDocRef");
                doc.WriteValue(TpoDocRef);
                doc.WriteEndElement();

                doc.WriteStartElement("FolioDocRef");
                doc.WriteValue(FolioDocRef);
                doc.WriteEndElement();

                doc.WriteStartElement("MntExe");
                doc.WriteValue(MntExe);
                doc.WriteEndElement();

                doc.WriteStartElement("MntNeto");
                doc.WriteValue(MntNeto);
                doc.WriteEndElement();

                doc.WriteStartElement("MntIVA");
                doc.WriteValue(MntIVA);
                doc.WriteEndElement();

                doc.WriteStartElement("MntActivoFijo");
                doc.WriteValue(MntActivoFijo);
                doc.WriteEndElement();

                doc.WriteStartElement("MntIVAActivoFijo");
                doc.WriteValue(MntIVAActivoFijo);
                doc.WriteEndElement();

                //<ivanorec>
                doc.WriteStartElement("IVANoRec");

                doc.WriteStartElement("CodIVANoRec");
                doc.WriteValue(CodIVANoRec_);
                doc.WriteEndElement();

                doc.WriteStartElement("MntIVANoRec");
                doc.WriteValue(MntIVANoRec);
                doc.WriteEndElement();

                doc.WriteEndElement();
                //</ivanorec>

                doc.WriteStartElement("IVAUsoComun");
                doc.WriteValue(IVAUsoComun);
                doc.WriteEndElement();

                doc.WriteStartElement("IVAFueraPlazo");
                doc.WriteValue(IVAFueraPlazo);
                doc.WriteEndElement();

                doc.WriteStartElement("IVAPropio");
                doc.WriteValue(IVAPropio);
                doc.WriteEndElement();

                doc.WriteStartElement("IVATerceros");
                doc.WriteValue(IVATerceros);
                doc.WriteEndElement();

                doc.WriteStartElement("Ley18211");
                doc.WriteValue(Ley18211);
                doc.WriteEndElement();

                doc.WriteStartElement("OtrosImp");

                doc.WriteStartElement("CodImp");
                doc.WriteValue(CodImp_);
                doc.WriteEndElement();
                doc.WriteStartElement("TasaImp");
                doc.WriteValue(TasaImp_);
                doc.WriteEndElement();
                doc.WriteStartElement("MntImp");
                doc.WriteValue(MntImp);
                doc.WriteEndElement();

                doc.WriteEndElement();

                doc.WriteStartElement("MntSinCred");
                doc.WriteValue(MntSinCred);
                doc.WriteEndElement();

                doc.WriteStartElement("IVARetTotal");
                doc.WriteValue(IVARetTotal);
                doc.WriteEndElement();

                doc.WriteStartElement("IVARetParcial");
                doc.WriteValue(IVARetParcial);
                doc.WriteEndElement();

                doc.WriteStartElement("CredEC");
                doc.WriteValue(CredEC);
                doc.WriteEndElement();

                doc.WriteStartElement("DepEnvase");
                doc.WriteValue(DepEnvase);
                doc.WriteEndElement();

                //<liquidaciones>
                doc.WriteStartElement("Liquidaciones");

                doc.WriteStartElement("RutEmisor");
                doc.WriteValue(RutEmisor);
                doc.WriteEndElement();

                doc.WriteStartElement("ValComNeto");
                doc.WriteValue(ValComNeto);
                doc.WriteEndElement();

                doc.WriteStartElement("ValComExe");
                doc.WriteValue(ValComExe);
                doc.WriteEndElement();

                doc.WriteStartElement("ValComIVA");
                doc.WriteValue(ValComIVA);
                doc.WriteEndElement();

                doc.WriteEndElement();
                //</liquidaciones>

                doc.WriteStartElement("MntTotal");
                doc.WriteValue(MntTotal);
                doc.WriteEndElement();

                doc.WriteStartElement("IVANoRetenido");
                doc.WriteValue(IVANoRetenido);
                doc.WriteEndElement();

                doc.WriteStartElement("MntNoFact");
                doc.WriteValue(MntNoFact);
                doc.WriteEndElement();

                doc.WriteStartElement("MntPeriodo");
                doc.WriteValue(MntPeriodo);
                doc.WriteEndElement();

                doc.WriteStartElement("PsjNac");
                doc.WriteValue(PsjNac);
                doc.WriteEndElement();

                doc.WriteStartElement("PsjInt");
                doc.WriteValue(PsjInt);
                doc.WriteEndElement();

                doc.WriteStartElement("TabPuros");
                doc.WriteValue(TabPuros);
                doc.WriteEndElement();

                doc.WriteStartElement("TabCigarrillos");
                doc.WriteValue(TabCigarrillos);
                doc.WriteEndElement();

                doc.WriteStartElement("TabElaborado");
                doc.WriteValue(TabElaborado);
                doc.WriteEndElement();

                doc.WriteStartElement("ImpVehiculo");
                doc.WriteValue(ImpVehiculo);
                doc.WriteEndElement();

                doc.WriteEndElement();
            }
            #endregion
            //</detalle>

            doc.WriteStartElement("TmstFirma");
            doc.WriteValue(TmstFirma);
            doc.WriteEndElement();

            //////////
            doc.WriteEndDocument();
            // fin documento
            doc.Flush();
            doc.Close();


        }

        public string GenerarSetDTE(clases.SetDTE setdte)
        {

            string RutEmisor = setdte.RutEmisor;
            string RutEnvia = setdte.RutEnvia;
            string receptor = setdte.receptor;
            string FchResol = setdte.FchResol;
            string NroResol = setdte.NroResol;
            string TmstFirmmaEnv = setdte.TmstFirmmaEnv;
            string TpoDTE = setdte.TpoDTE;
            string NroDTE = setdte.NroDTE;

            string result = "";

            using (StringWriter str = new StringWriter())
            using (XmlTextWriter doc = new XmlTextWriter(str))
            {

                doc.Formatting = Formatting.Indented;
                doc.Indentation = 0;
                doc.WriteStartDocument();
                doc.WriteStartElement("EnvioDTE");
                doc.WriteStartAttribute("xmlns");
                doc.WriteValue("http://www.sii.cl/SiiDte");
                doc.WriteEndAttribute();

                doc.WriteStartAttribute("xmlns:xsi");
                doc.WriteValue("http://www.w3.org/2001/XMLSchema-instance");
                doc.WriteEndAttribute();

                doc.WriteStartAttribute("xsi:schemaLocation");
                doc.WriteValue("http://www.sii.cl/SiiDte EnvioDTE_v10.xsd");
                doc.WriteEndAttribute();

                doc.WriteStartAttribute("version");
                doc.WriteValue("1.0");
                doc.WriteEndAttribute();
                //Fin Cabecera
                doc.WriteStartElement("SetDTE");
                doc.WriteStartAttribute("ID");
                doc.WriteValue("SetDoc");
                doc.WriteEndAttribute();

                doc.WriteStartElement("Caratula");
                doc.WriteStartAttribute("version");
                doc.WriteValue("1.0");
                doc.WriteEndAttribute();

                doc.WriteStartElement("RutEmisor");
                doc.WriteValue(RutEmisor);
                doc.WriteEndElement();

                doc.WriteStartElement("RutEnvia");
                doc.WriteValue(RutEnvia);
                doc.WriteEndElement();

                doc.WriteStartElement("RutReceptor");
                doc.WriteValue(receptor);
                doc.WriteEndElement();

                doc.WriteStartElement("FchResol");
                doc.WriteValue(FchResol);
                doc.WriteEndElement();

                doc.WriteStartElement("NroResol");
                doc.WriteValue(NroResol);
                doc.WriteEndElement();

                doc.WriteStartElement("TmstFirmaEnv");
                doc.WriteValue(TmstFirmmaEnv);
                doc.WriteEndElement();

                doc.WriteStartElement("SubTotDTE");

                doc.WriteStartElement("TpoDTE");
                doc.WriteValue(TpoDTE);
                doc.WriteEndElement();

                doc.WriteStartElement("NroDTE");
                doc.WriteValue(NroDTE);
                doc.WriteEndElement();
                doc.WriteEndElement();
                doc.WriteEndElement();
               
                doc.WriteEndDocument();
                // fin documento
                doc.Flush();
                doc.Close();
                result = str.ToString();
                result = result.Replace("utf-16", "iso-8859-1");
            
            

        }
            return result;
        }

        public void quitar_Tag(string ruta_dte)
        {
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.Load(ruta_dte);
            string xml = xmldoc.InnerXml.ToString();
            XElement doc = XElement.Parse(xml);
            doc.Descendants().Where(e => string.IsNullOrEmpty(e.Value)).Remove();
            string algo = doc.ToString();
            xmldoc.LoadXml(algo);
            xmldoc.Save(ruta_dte);

        }

        //metodo referente al proceso de envio y generar el dte 
        public bool Genera_dte_envia_(SetDTE setdte, [Optional] DTE_idDoc dte_idDoc,
            [Optional]  DTE_Emisor dte_emis, [Optional]DTE_Receptor dte_recep, [Optional]DTE_Transporte trans,
            [Optional]  DTE_Totales tot, [Optional] List<DTE_DETALLE> det, [Optional] List<DTE_SubTotInfo> sub, [Optional] List<DTE_DscrGlobal> dscrcg,
            [Optional]  List<DTE_referencia> refer, [Optional] List<DTE_comisiones> comi, string session_rut,string tipo_doc, string hora_envio) {
         bool procedimiento = false;
      
            int id_empresa = Int32.Parse(get_empresa(session_rut));
            var data =
            (from archivos in db.RUTA_GUARDADO_EMPRESA
             where archivos.ID_EMPRESA == id_empresa
             select new
             {
                 ruta_dte_clie=archivos.RUTA_DTE_CLIE,
                 rut_doc_firm_clie=archivos.RUTA_ENVIO_CLIENTE,
                 ruta_dte_sii = archivos.RUTA_DTE_SII,
                 rut_doc_firm_sii = archivos.RUTA_ENVIO_SII
             });
            //asigna folio y nombre a documento 
            int Folio = get_num_folio(session_rut, Int32.Parse(tipo_doc));
            string uri = ("DOC" + Folio.ToString() + "" + hora_envio.Replace(":", ""));
            string ID = uri;
            ID = ID.Replace("-", "");
            dte_idDoc.ID = ID;
            //
            string ruta_dte_clie = data.FirstOrDefault().ruta_dte_clie.ToString();
            string ruta_doc_firm_clie = data.FirstOrDefault().rut_doc_firm_clie.ToString();
            string ruta_dte_sii = data.FirstOrDefault().ruta_dte_sii.ToString();  
            string ruta_doc_firm_sii = data.FirstOrDefault().rut_doc_firm_sii.ToString();

            string nom_dte_clie = ruta_dte_clie + "DTE_DOC_CLIE" + get_empresaRUT(session_rut) + "_TPO_" + tipo_doc + "_H_" + hora_envio + ".xml";
           // string nom_doc_firm_clie = ruta_doc_firm_clie + "DOC_FIRM_CLIE" + get_empresaRUT(session_rut) + "_TPO_" + tipo_doc + "_H_" + hora_envio + ".xml";
            string nom_dte_sii = ruta_dte_sii + "DTE_DOC_EMP" + get_empresaRUT(session_rut) + "_TPO_" + tipo_doc + "_H_" + hora_envio + ".xml";  
            string nom_doc_firm_sii = ruta_doc_firm_sii + "DOC_FIRMENVSII" + get_empresaRUT(session_rut) + "_TPO_" + tipo_doc + "_H_" + hora_envio + ".xml";

            //genera archivos 
            //archivos cliente 
            //string set_dte_clie = GenerarSetDTE(setdte);
            GenerarDTE(dte_idDoc, dte_emis, dte_recep, trans, tot, det, sub, dscrcg, refer, comi, nom_dte_clie, session_rut);

            quitar_Tag(nom_dte_clie);
            //archivos sii
            setdte.receptor = "60803000-K";
            string set_dte_sii = GenerarSetDTE(setdte);
            dte_recep.RUTRecep = "60803000-K";
            GenerarDTE(dte_idDoc, dte_emis, dte_recep, trans, tot, det, sub, dscrcg, refer, comi, nom_dte_sii, session_rut);
            quitar_Tag(nom_dte_sii);
            //
            //datos set_dte
            string xml_get = get_setDTE(get_empresaRUT(session_rut));
            DataSet ds = new DataSet();
            ds.ReadXml(XmlReader.Create(new StringReader(xml_get)));
            string rutemis_env = ds.Tables["get_setDTE"].Rows[0][0].ToString().Replace("-", "");
            //firma de el documento para posterior envio al sii
            procedimientofirma(set_dte_sii, nom_dte_sii, session_rut, nom_doc_firm_sii, uri);
            //GUARDA DTE EMISION DE EL CLIENTE Y EL SII

            //EL ID DE EL CLIENTE ESTA PUESTO EN 0 CON UN CLIENTE FICTICIO Y DE PRUEBA !
            string fecha_envio = hora_envio;
            fecha_envio = fecha_envio.Replace("_", ":");
            if (tot.MntNeto == string.Empty) {
                tot.MntNeto ="0"; 
            }
            if (tot.IVA == string.Empty) {
                tot.IVA = "0";
            }

            decimal monto_neto =decimal.Parse( tot.MntNeto);
            decimal monto_iva =decimal.Parse( tot.IVA);
            decimal monto_exe =decimal.Parse( tot.MntExe);
            decimal monto_total =decimal.Parse( tot.MntTotal);
            int id_doc=  guarda_dte_emis(id_empresa,Int32.Parse( tipo_doc),0,hora_envio,"",nom_dte_sii,nom_dte_clie,monto_neto,monto_iva,monto_exe,monto_total);

            //envio de el documento al sii
            string respuesta = upload_sii(rutemis_env, rutemis_env, nom_doc_firm_sii);
            //guarda en la db el folio 
            administrador_folios(Folio, respuesta, session_rut, Int32.Parse(tipo_doc),id_doc);
            //
            return procedimiento;
        }


        //metodo para generar nombre de archivo dte y set dte!
     /*   public string Genera_dte_Archivo(SetDTE setdte, [Optional] DTE_idDoc dte_idDoc,
            [Optional]  DTE_Emisor dte_emis, [Optional]DTE_Receptor dte_recep, [Optional]DTE_Transporte trans,
            [Optional]  DTE_Totales tot, [Optional] List<DTE_DETALLE> det, [Optional] List<DTE_SubTotInfo> sub, [Optional] List<DTE_DscrGlobal> dscrcg,
            [Optional]  List<DTE_referencia> refer, [Optional] List<DTE_comisiones> comi,string session_rut,string tipo_doc,string hora_envio) {
    
            int id_empresa=  Int32.Parse(  get_empresa(session_rut));
            
            var data =
            (from archivos in db.RUTA_GUARDADO_EMPRESA
               where archivos.ID_EMPRESA == id_empresa
                select new
                {
                ruta_dte=archivos.RUTA_DTE,
                ruta_setdte=archivos.RUTA_SETDTE,
                rut_doc_firm_sii=archivos.RUTA_ENVIO_SII
                  });
            string ruta_dte = data.FirstOrDefault().ruta_dte.ToString();
            string ruta_setdte = data.FirstOrDefault().ruta_setdte.ToString();
            string ruta_doc_firm_sii = data.FirstOrDefault().rut_doc_firm_sii.ToString();

            string nombre_dte =ruta_dte+ "/DTE_DOC_EMP"+get_empresaRUT(session_rut) + "_TPO_"+ tipo_doc + "_H_"+hora_envio +".xml";
            string nombre_setdte= ruta_setdte + "/SETDTE_DOC_EMP" + get_empresaRUT(session_rut) + "_TPO_" + tipo_doc + "_H_" + hora_envio + ".xml";
            string nombre_doc_firm_sii = ruta_doc_firm_sii + "/DOC_FIRM_EMP" + get_empresaRUT(session_rut) + "_TPO_" + tipo_doc + "_H_" + hora_envio + ".xml";

   string set_dte=GenerarSetDTE(setdte);
     GenerarDTE(dte_idDoc, dte_emis, dte_recep, trans, tot, det, sub,dscrcg, refer, comi,nombre_dte,session_rut);
            quitar_Tag(nombre_dte);

           // Limpia_tag_xml(nombre_dte);


            string rutas= "<?xml version=\"1.0\" encoding=\"utf-16\"?><RUTAS><DTE>" + nombre_dte+"</DTE><SETDTE>"+nombre_setdte+ "</SETDTE><NOM_DOC_FIRM>"+nombre_doc_firm_sii+"</NOM_DOC_FIRM></RUTAS>";

            return rutas;
        }*/

        public void GenerarDTE(
          DTE_idDoc dte_idDoc,
            DTE_Emisor dte_emis,
          [Optional] DTE_Receptor dte_recep,
          [Optional] DTE_Transporte trans,
          [Optional] DTE_Totales tot,
          [Optional] List<DTE_DETALLE> det,
          [Optional] List<DTE_SubTotInfo> sub,
          [Optional] List<DTE_DscrGlobal> dscrcg,
          [Optional] List<DTE_referencia> refer,
          [Optional] List<DTE_comisiones> comi,
            string ruta,string rut_user)
        {

        
                //<iddoc>
                string ID = dte_idDoc.ID;
                string TipoDTE = dte_idDoc.TipoDTE;
                string Folio = dte_idDoc.Folio;
                string FchEmis = dte_idDoc.FchEmis;
                string IndNoRebaja = dte_idDoc.IndNoRebaja;
                string TipoDespacho = dte_idDoc.TipoDespacho;
                string IndTraslado = dte_idDoc.IndTraslado;
                string TpoImpresion = dte_idDoc.TpoImpresion;
                string IndServicio = dte_idDoc.IndServicio;
                string MntBruto = dte_idDoc.MntBruto;
                string FmaPago = dte_idDoc.FmaPago;
                string FmaPagExp = dte_idDoc.FmaPagExp;
                string FchCancel = dte_idDoc.FchCancel;
                string MntCancel = dte_idDoc.MntCancel;
                string SaldoInsol = dte_idDoc.SaldoInsol;
                string FchPago = dte_idDoc.FchPago;
                string MntPago = dte_idDoc.MntPago;
                string GlosaPagos = dte_idDoc.GlosaPagos;
                string PeriodoDesde = dte_idDoc.PeriodoDesde;
                string PeriodoHasta = dte_idDoc.PeriodoHasta;
                string MedioPago = dte_idDoc.MedioPago;
                string TpoCtaPago = dte_idDoc.TpoCtaPago;
                string NumCtaPago = dte_idDoc.NumCtaPago;
                string BcoPago = dte_idDoc.BcoPago;
                string TermPagoCdg = dte_idDoc.TermPagoCdg;
                string TermPagoGlosa = dte_idDoc.TermPagoGlosa;
                string TermPagoDias = dte_idDoc.TermPagoDias;
                string FchVenc = dte_idDoc.FchVenc;
                //</iddoc>
                //<emisor>
                string RUTEmisor = dte_emis.RUTEmisor;
                string RznSoc = dte_emis.RznSoc;
                string GiroEmis = dte_emis.GiroEmis;
                string Telefono = dte_emis.Telefono;
                string CorreoEmisor = dte_emis.CorreoEmisor;
                string Acteco = dte_emis.Acteco;
                string CdgTraslado = dte_emis.CdgTraslado;
                string FolioAut = dte_emis.FolioAut;
                string FchAut = dte_emis.FchAut;
                string Sucursal = dte_emis.Sucursal;
                string CdgSIISucur = dte_emis.CdgSIISucur;
                string DirOrigen = dte_emis.DirOrigen;
                string CmnaOrigen = dte_emis.CmnaOrigen;
                string CiudadOrigen = dte_emis.CiudadOrigen;
                string CdgVendedor = dte_emis.CdgVendedor;
                string IdAdicEmisor = dte_emis.IdAdicEmisor;
                //</emisor>
                string RUTMandante = "";
                //<receptor>
                string RUTRecep = dte_recep.RUTRecep;
                string CdgIntRecep = dte_recep.CdgIntRecep;
                string RznSocRecep = dte_recep.RznSocRecep;
                string NumId = dte_recep.NumId;
                string Nacionalidad = dte_recep.Nacionalidad;
                string GiroRecep = dte_recep.GiroRecep;
                string Contacto = dte_recep.Contacto;
                string CorreoRecep = dte_recep.CorreoRecep;
                string DirRecep = dte_recep.DirRecep;
                string CmnaRecep = dte_recep.CmnaRecep;
                string CiudadRecep = dte_recep.CiudadRecep;
                string DirPostal = dte_recep.DirPostal;
                string CmnaPostal = dte_recep.CmnaPostal;
                string CiudadPostal = dte_recep.CiudadPostal;
                //<receptor>
                string RUTSolicita = "";
                //<transporte>
                string Patente = trans.Patente;
                string RUTTrans = trans.RUTTrans;
                string RUTChofer = trans.RUTChofer;
                string NombreChofer = trans.NombreChofer;
                string DirDest = trans.DirDest;
                string CmnaDest = trans.CmnaDest;
                string CiudadDest = trans.CiudadDest;
                //<aduana>
                string CodModVenta = trans.CodModVenta;
                string CodClauVenta = trans.CodClauVenta;
                string TotClauVenta = trans.TotClauVenta;
                string CodViaTransp = trans.CodViaTransp;
                string NombreTransp = trans.NombreTransp;
                string RUTCiaTransp = trans.RUTCiaTransp;
                string NomCiaTransp = trans.NomCiaTransp;
                string IdAdicTransp = trans.IdAdicTransp;
                string Booking = trans.Booking;
                string Operador = trans.Operador;
                string CodPtoEmbarque = trans.CodPtoEmbarque;
                string IdAdicPtoEmb = trans.IdAdicPtoEmb;
                string CodPtoDesemb = trans.CodPtoDesemb;
                string IdAdicPtoDesemb = trans.IdAdicPtoDesemb;
                string Tara = trans.Tara;
                string CodUnidMedTara = trans.CodUnidMedTara;
                string PesoBruto = trans.PesoBruto;
                string CodUnidPesoBruto = trans.CodUnidPesoBruto;
                string PesoNeto = trans.PesoNeto;
                string CodUnidPesoNeto = trans.CodUnidPesoNeto;
                string TotItems = trans.TotItems;
                string TotBultos = trans.TotBultos;
                string CodTpoBultos = trans.CodTpoBultos;
                string CantBultos = trans.CantBultos;
                string Marcas = trans.Marcas;
                string IdContainer = trans.IdContainer;
                string Sello = trans.Sello;
                string EmisorSello = trans.EmisorSello;
                string MntFlete = trans.MntFlete;
                string MntSeguro = trans.MntSeguro;
                string CodPaisRecep = trans.CodPaisRecep;
                string CodPaisDestin = trans.CodPaisDestin;
                //</aduana>
                //</transporte>

                //<totales>
                string MntNeto = tot.MntNeto;
                string MntExe = tot.MntExe;
                string MntBase = tot.MntBase;
                string MntMargenCom = tot.MntMargenCom;
                string TasaIVA = tot.TasaIVA;
                string IVA = tot.IVA;
                string IVAProp = tot.IVAProp;
                string IVATerc = tot.IVATerc;
                string TipoImp = tot.TipoImp;
                string TasaImp = tot.TasaImp;
                string MontoImp = tot.MontoImp;
                string IVANoRet = tot.IVANoRet;
                string CredEC = tot.CredEC;
                string GrntDep = tot.GrntDep;
                string _ValComNeto = tot._ValComNeto;
                string _ValComExe = tot._ValComExe;
                string _ValComIVA = tot._ValComIVA;
                string MntTotal = tot.MntTotal;
                string MontoNF = tot.MontoNF;
                string MontoPeriodo = tot.MontoPeriodo;
                string SaldoAnterior = tot.SaldoAnterior;
                string VlrPagar = tot.VlrPagar;
                //</totales>

                //<detalle>
                string NroLinDet = "";
                string TpoCodigo = "";
                string VlrCodigo = "";
                string IndExe = "";
                string IndAgente = "";
                string MntBaseFaena = "";
                string MntMargComer = "";
                string PrcConsFinal = "";
                string NmbItem = "";
                string DscItem = "";
                string QtyRef = "";
                string UnmdRef = "";
                string PrcRef = "";
                string QtyItem = "";
                string SubQty = "";
                string SubCod = "";
                string FchElabor = "";
                string FchVencim = "";
                string UnmdItem = "";
                string PrcItem = "";
                string PrcOtrMon = "";
                string Moneda = "";
                string FctConv = "";
                string DctoOtrMnda = "";
                string RecargoOtrMnda = "";
                string MontoItemOtrMnda = "";
                string DescuentoPct = "";
                string DescuentoMonto = "";
                string TipoDscto = "";
                string ValorDscto = "";
                string RecargoPct = "";
                string RecargoMonto = "";
                string TipoRecargo = "";
                string ValorRecargo = "";
                string CodImpAdic = "";
                string MontoItem = "";
                // </detalle>
                //<subtotinfo>
                string NroSTI = "";
                string GlosaSTI = "";
                string OrdenSTI = "";
                string SubTotNetoSTI = "";
                string SubTotIVASTI = "";
                string SubTotAdicSTI = "";
                string SubTotExeSTI = "";
                string ValSubtotSTI = "";
                string LineasDeta = "";
                //</subtotinfo>
                //<dscrcgglobal>
                string NroLinDR = "";
                string TpoMov = "";
                string GlosaDR = "";
                string TpoValor = "";
                string ValorDR = "";
                string ValorDROtrMnda = "";
                string IndExeDR = "";
                //</dscrcgglobal>
                //<referencia>
                string NroLinRef = "";
                string TpoDocRef = "";
                string IndGlobal = "";
                string FolioRef = "";
                string RUTOtr = "";
                string FchRef = "";
                string CodRef = "";
                string RazonRef = "";
                //</referencia>
                //<comiciones>
                string NroLinCom = "";
                string TipoMovim = "";
                string Glosa = "";
                string TasaComision = "";
                string ValComNeto = "";
                string ValComExe = "";
                string ValComIVA = "";
                //</comiciones>




                XmlTextWriter doc = new XmlTextWriter(ruta, Encoding.GetEncoding("iso-8859-1"));
                doc.Formatting = Formatting.Indented;
                doc.Indentation = 0;
                doc.WriteStartDocument();

                doc.WriteStartElement("DTE");
                doc.WriteStartAttribute("version");
                doc.WriteValue("1.0");
                doc.WriteEndAttribute();

                doc.WriteStartElement("Documento");

                doc.WriteStartAttribute("ID");
                doc.WriteValue(ID);
                doc.WriteEndAttribute();

                //encabezado
                #region
                doc.WriteStartElement("Encabezado");
                doc.WriteStartElement("IdDoc");

                doc.WriteStartElement("TipoDTE");
                doc.WriteValue(TipoDTE);
                doc.WriteEndElement();

                doc.WriteStartElement("Folio");
                doc.WriteValue(Folio);
                doc.WriteEndElement();

                doc.WriteStartElement("FchEmis");
                doc.WriteValue(FchEmis);
                doc.WriteEndElement();

                doc.WriteStartElement("IndNoRebaja");
                doc.WriteValue(IndNoRebaja);
                doc.WriteEndElement();

                doc.WriteStartElement("TipoDespacho");
                doc.WriteValue(TipoDespacho);
                doc.WriteEndElement();

                doc.WriteStartElement("IndTraslado");
                doc.WriteValue(IndTraslado);
                doc.WriteEndElement();

                doc.WriteStartElement("TpoImpresion");
                doc.WriteValue(TpoImpresion);
                doc.WriteEndElement();

                doc.WriteStartElement("IndServicio");
                doc.WriteValue(IndServicio);
                doc.WriteEndElement();

                doc.WriteStartElement("MntBruto");
                doc.WriteValue(MntBruto);
                doc.WriteEndElement();

                doc.WriteStartElement("FmaPago");
                doc.WriteValue(FmaPago);
                doc.WriteEndElement();

                doc.WriteStartElement("FmaPagExp");
                doc.WriteValue(FmaPagExp);
                doc.WriteEndElement();

                doc.WriteStartElement("FchCancel");
                doc.WriteValue(FchCancel);
                doc.WriteEndElement();

                doc.WriteStartElement("MntCancel");
                doc.WriteValue(MntCancel);
                doc.WriteEndElement();

                doc.WriteStartElement("SaldoInsol");
                doc.WriteValue(SaldoInsol);
                doc.WriteEndElement();

                doc.WriteStartElement("MntPagos");

                doc.WriteStartElement("FchPago");
                doc.WriteValue(FchPago);
                doc.WriteEndElement();

                doc.WriteStartElement("MntPago");
                doc.WriteValue(MntPago);
                doc.WriteEndElement();

                doc.WriteStartElement("GlosaPagos");
                doc.WriteValue(GlosaPagos);
                doc.WriteEndElement();

                doc.WriteEndElement();
                //fin mntpagos

                doc.WriteStartElement("PeriodoDesde");
                doc.WriteValue(PeriodoDesde);
                doc.WriteEndElement();

                doc.WriteStartElement("PeriodoHasta");
                doc.WriteValue(PeriodoHasta);
                doc.WriteEndElement();

                doc.WriteStartElement("MedioPago");
                doc.WriteValue(MedioPago);
                doc.WriteEndElement();

                doc.WriteStartElement("TpoCtaPago");
                doc.WriteValue(TpoCtaPago);
                doc.WriteEndElement();

                doc.WriteStartElement("NumCtaPago");
                doc.WriteValue(NumCtaPago);
                doc.WriteEndElement();

                doc.WriteStartElement("BcoPago");
                doc.WriteValue(BcoPago);
                doc.WriteEndElement();

                doc.WriteStartElement("TermPagoCdg");
                doc.WriteValue(TermPagoCdg);
                doc.WriteEndElement();

                doc.WriteStartElement("TermPagoGlosa");
                doc.WriteValue(TermPagoGlosa);
                doc.WriteEndElement();

                doc.WriteStartElement("TermPagoDias");
                doc.WriteValue(TermPagoDias);
                doc.WriteEndElement();

                doc.WriteStartElement("FchVenc");
                doc.WriteValue(FchVenc);
                doc.WriteEndElement();

                doc.WriteEndElement();
                //fin Id Doc
                doc.WriteStartElement("Emisor");

                doc.WriteStartElement("RUTEmisor");
                doc.WriteValue(RUTEmisor);
                doc.WriteEndElement();

                doc.WriteStartElement("RznSoc");
                doc.WriteValue(RznSoc);
                doc.WriteEndElement();

                doc.WriteStartElement("GiroEmis");
                doc.WriteValue(GiroEmis);
                doc.WriteEndElement();

                doc.WriteStartElement("Telefono");
                doc.WriteValue(Telefono);
                doc.WriteEndElement();

                doc.WriteStartElement("CorreoEmisor");
                doc.WriteValue(CorreoEmisor);
                doc.WriteEndElement();

                doc.WriteStartElement("Acteco");
                doc.WriteValue(Acteco);
                doc.WriteEndElement();

                doc.WriteStartElement("GuiaExport");

                doc.WriteStartElement("CdgTraslado");
                doc.WriteValue(CdgTraslado);
                doc.WriteEndElement();

                doc.WriteStartElement("FolioAut");
                doc.WriteValue(FolioAut);
                doc.WriteEndElement();

                doc.WriteStartElement("FchAut");
                doc.WriteValue(FchAut);
                doc.WriteEndElement();

                doc.WriteEndElement();
                //fin guiaexport

                doc.WriteStartElement("Sucursal");
                doc.WriteValue(Sucursal);
                doc.WriteEndElement();

                doc.WriteStartElement("CdgSIISucur");
                doc.WriteValue(CdgSIISucur);
                doc.WriteEndElement();

                doc.WriteStartElement("DirOrigen");
                doc.WriteValue(DirOrigen);
                doc.WriteEndElement();

                doc.WriteStartElement("CmnaOrigen");
                doc.WriteValue(CmnaOrigen);
                doc.WriteEndElement();

                doc.WriteStartElement("CiudadOrigen");
                doc.WriteValue(CiudadOrigen);
                doc.WriteEndElement();

                doc.WriteStartElement("CdgVendedor");
                doc.WriteValue(CdgVendedor);
                doc.WriteEndElement();

                doc.WriteStartElement("IdAdicEmisor");
                doc.WriteValue(IdAdicEmisor);
                doc.WriteEndElement();

                doc.WriteEndElement();
                //fin de Emisor
                doc.WriteStartElement("RUTMandante");
                doc.WriteValue(RUTMandante);
                doc.WriteEndElement();
                //empieza receptor
                doc.WriteStartElement("Receptor");

                doc.WriteStartElement("RUTRecep");
                doc.WriteValue(RUTRecep);
                doc.WriteEndElement();

                doc.WriteStartElement("CdgIntRecep");
                doc.WriteValue(CdgIntRecep);
                doc.WriteEndElement();

                doc.WriteStartElement("RznSocRecep");
                doc.WriteValue(RznSocRecep);
                doc.WriteEndElement();

                doc.WriteStartElement("Extranjero");

                doc.WriteStartElement("NumId");
                doc.WriteValue(NumId);
                doc.WriteEndElement();

                doc.WriteStartElement("Nacionalidad");
                doc.WriteValue(Nacionalidad);
                doc.WriteEndElement();

                doc.WriteEndElement();
                //fin extranjero

                doc.WriteStartElement("GiroRecep");
                doc.WriteValue(GiroRecep);
                doc.WriteEndElement();

                doc.WriteStartElement("Contacto");
                doc.WriteValue(Contacto);
                doc.WriteEndElement();

                doc.WriteStartElement("CorreoRecep");
                doc.WriteValue(CorreoRecep);
                doc.WriteEndElement();

                doc.WriteStartElement("DirRecep");
                doc.WriteValue(DirRecep);
                doc.WriteEndElement();

                doc.WriteStartElement("CmnaRecep");
                doc.WriteValue(CmnaRecep);
                doc.WriteEndElement();

                doc.WriteStartElement("CiudadRecep");
                doc.WriteValue(CiudadRecep);
                doc.WriteEndElement();

                doc.WriteStartElement("DirPostal");
                doc.WriteValue(DirPostal);
                doc.WriteEndElement();

                doc.WriteStartElement("CmnaPostal");
                doc.WriteValue(CmnaPostal);
                doc.WriteEndElement();

                doc.WriteStartElement("CiudadPostal");
                doc.WriteValue(CiudadPostal);
                doc.WriteEndElement();

                doc.WriteEndElement();
                //fin de Receptor

                doc.WriteStartElement("RUTSolicita");
                doc.WriteValue(RUTSolicita);
                doc.WriteEndElement();

                doc.WriteStartElement("Transporte");

                doc.WriteStartElement("Patente");
                doc.WriteValue(Patente);
                doc.WriteEndElement();

                doc.WriteStartElement("RUTTrans");
                doc.WriteValue(RUTTrans);
                doc.WriteEndElement();

                doc.WriteStartElement("Chofer");

                doc.WriteStartElement("RUTChofer");
                doc.WriteValue(RUTChofer);
                doc.WriteEndElement();

                doc.WriteStartElement("NombreChofer");
                doc.WriteValue(NombreChofer);
                doc.WriteEndElement();

                doc.WriteEndElement();
                //fin chofer

                doc.WriteStartElement("DirDest");
                doc.WriteValue(DirDest);
                doc.WriteEndElement();

                doc.WriteStartElement("CmnaDest");
                doc.WriteValue(CmnaDest);
                doc.WriteEndElement();

                doc.WriteStartElement("CiudadDest");
                doc.WriteValue(CiudadDest);
                doc.WriteEndElement();

                doc.WriteStartElement("Aduana");

                doc.WriteStartElement("CodModVenta");
                doc.WriteValue(CodModVenta);
                doc.WriteEndElement();

                doc.WriteStartElement("CodClauVenta");
                doc.WriteValue(CodClauVenta);
                doc.WriteEndElement();

                doc.WriteStartElement("TotClauVenta");
                doc.WriteValue(TotClauVenta);
                doc.WriteEndElement();

                doc.WriteStartElement("CodViaTransp");
                doc.WriteValue(CodViaTransp);
                doc.WriteEndElement();

                doc.WriteStartElement("NombreTransp");
                doc.WriteValue(NombreTransp);
                doc.WriteEndElement();

                doc.WriteStartElement("RUTCiaTransp");
                doc.WriteValue(RUTCiaTransp);
                doc.WriteEndElement();

                doc.WriteStartElement("NomCiaTransp");
                doc.WriteValue(NomCiaTransp);
                doc.WriteEndElement();

                doc.WriteStartElement("IdAdicTransp");
                doc.WriteValue(IdAdicTransp);
                doc.WriteEndElement();

                doc.WriteStartElement("Booking");
                doc.WriteValue(Booking);
                doc.WriteEndElement();

                doc.WriteStartElement("Operador");
                doc.WriteValue(Operador);
                doc.WriteEndElement();

                doc.WriteStartElement("CodPtoEmbarque");
                doc.WriteValue(CodPtoEmbarque);
                doc.WriteEndElement();

                doc.WriteStartElement("IdAdicPtoEmb");
                doc.WriteValue(IdAdicPtoEmb);
                doc.WriteEndElement();

                doc.WriteStartElement("CodPtoDesemb");
                doc.WriteValue(CodPtoDesemb);
                doc.WriteEndElement();

                doc.WriteStartElement("IdAdicPtoDesemb");
                doc.WriteValue(IdAdicPtoDesemb);
                doc.WriteEndElement();

                doc.WriteStartElement("Tara");
                doc.WriteValue(Tara);
                doc.WriteEndElement();

                doc.WriteStartElement("CodUnidMedTara");
                doc.WriteValue(CodUnidMedTara);
                doc.WriteEndElement();

                doc.WriteStartElement("PesoBruto");
                doc.WriteValue(PesoBruto);
                doc.WriteEndElement();

                doc.WriteStartElement("CodUnidPesoBruto");
                doc.WriteValue(CodUnidPesoBruto);
                doc.WriteEndElement();

                doc.WriteStartElement("PesoNeto");
                doc.WriteValue(PesoNeto);
                doc.WriteEndElement();

                doc.WriteStartElement("CodUnidPesoNeto");
                doc.WriteValue(CodUnidPesoNeto);
                doc.WriteEndElement();

                doc.WriteStartElement("TotItems");
                doc.WriteValue(TotItems);
                doc.WriteEndElement();

                doc.WriteStartElement("TotBultos");
                doc.WriteValue(TotBultos);
                doc.WriteEndElement();

                doc.WriteStartElement("TipoBultos");

                doc.WriteStartElement("CodTpoBultos");
                doc.WriteValue(CodTpoBultos);
                doc.WriteEndElement();

                doc.WriteStartElement("CantBultos");
                doc.WriteValue(CantBultos);
                doc.WriteEndElement();

                doc.WriteStartElement("Marcas");
                doc.WriteValue(Marcas);
                doc.WriteEndElement();

                doc.WriteStartElement("IdContainer");
                doc.WriteValue(IdContainer);
                doc.WriteEndElement();

                doc.WriteStartElement("Sello");
                doc.WriteValue(Sello);
                doc.WriteEndElement();

                doc.WriteStartElement("EmisorSello");
                doc.WriteValue(EmisorSello);
                doc.WriteEndElement();

                doc.WriteEndElement();
                //fin tipo bultos

                doc.WriteStartElement("MntFlete");
                doc.WriteValue(MntFlete);
                doc.WriteEndElement();

                doc.WriteStartElement("MntSeguro");
                doc.WriteValue(MntSeguro);
                doc.WriteEndElement();

                doc.WriteStartElement("CodPaisRecep");
                doc.WriteValue(CodPaisRecep);
                doc.WriteEndElement();

                doc.WriteStartElement("CodPaisDestin");
                doc.WriteValue(CodPaisDestin);
                doc.WriteEndElement();

                doc.WriteEndElement();
                //fin aduana

                doc.WriteEndElement();
                //FIN TRANSPORTE

                //empieza totales
                #region
                doc.WriteStartElement("Totales");


                doc.WriteStartElement("MntNeto");
                doc.WriteValue(MntNeto);
                doc.WriteEndElement();

                doc.WriteStartElement("MntExe");
                doc.WriteValue(MntExe);
                doc.WriteEndElement();

                doc.WriteStartElement("MntBase");
                doc.WriteValue(MntBase);
                doc.WriteEndElement();

                doc.WriteStartElement("MntMargenCom");
                doc.WriteValue(MntMargenCom);
                doc.WriteEndElement();

                doc.WriteStartElement("TasaIVA");
                doc.WriteValue(TasaIVA);
                doc.WriteEndElement();

                doc.WriteStartElement("IVA");
                doc.WriteValue(IVA);
                doc.WriteEndElement();

                doc.WriteStartElement("IVAProp");
                doc.WriteValue(IVAProp);
                doc.WriteEndElement();

                doc.WriteStartElement("IVATerc");
                doc.WriteValue(IVATerc);
                doc.WriteEndElement();


                doc.WriteStartElement("ImptoReten");

                doc.WriteStartElement("TipoImp");
                doc.WriteValue(TipoImp);
                doc.WriteEndElement();

                doc.WriteStartElement("TasaImp");
                doc.WriteValue(TasaImp);
                doc.WriteEndElement();

                doc.WriteStartElement("MontoImp");
                doc.WriteValue(MontoImp);
                doc.WriteEndElement();

                doc.WriteEndElement();
                //fin imptoreten

                doc.WriteStartElement("IVANoRet");
                doc.WriteValue(IVANoRet);
                doc.WriteEndElement();

                doc.WriteStartElement("CredEC");
                doc.WriteValue(CredEC);
                doc.WriteEndElement();

                doc.WriteStartElement("GrntDep");
                doc.WriteValue(GrntDep);
                doc.WriteEndElement();

                doc.WriteStartElement("Comisiones");

                doc.WriteStartElement("ValComNeto");
                doc.WriteValue(_ValComNeto);
                doc.WriteEndElement();

                doc.WriteStartElement("ValComExe");
                doc.WriteValue(_ValComExe);
                doc.WriteEndElement();

                doc.WriteStartElement("ValComIVA");
                doc.WriteValue(_ValComIVA);
                doc.WriteEndElement();

                doc.WriteEndElement();
                //fin comiciones

                doc.WriteStartElement("MntTotal");
                doc.WriteValue(MntTotal);
                doc.WriteEndElement();

                doc.WriteStartElement("MontoNF");
                doc.WriteValue(MontoNF);
                doc.WriteEndElement();

                doc.WriteStartElement("MontoPeriodo");
                doc.WriteValue(MontoPeriodo);
                doc.WriteEndElement();

                doc.WriteStartElement("SaldoAnterior");
                doc.WriteValue(SaldoAnterior);
                doc.WriteEndElement();

                doc.WriteStartElement("VlrPagar");
                doc.WriteValue(VlrPagar);
                doc.WriteEndElement();

                doc.WriteEndElement();
                #endregion
                //fin de Totales

                doc.WriteEndElement();

                #endregion
                //fin de Encabezado

                //EMPIESA DETALLE
                #region

                int i = 1;
                foreach (DTE_DETALLE _det in det)
                {
                    i++;
                    NroLinDet = (Int32.Parse(_det.NroLinDet) + 1).ToString();
                    TpoCodigo = _det.TpoCodigo.ToString();
                    VlrCodigo = _det.VlrCodigo.ToString();
                    IndExe = _det.IndExe.ToString();
                    IndAgente = _det.IndAgente.ToString();
                    MntBaseFaena = _det.MntBaseFaena.ToString();
                    MntMargComer = _det.MntMargComer.ToString();
                    PrcConsFinal = _det.PrcConsFinal.ToString();
                    NmbItem = _det.NmbItem.ToString();
                    DscItem = _det.DscItem.ToString();
                    QtyRef = _det.QtyRef.ToString();
                    UnmdRef = _det.UnmdRef.ToString();
                    PrcRef = _det.PrcRef.ToString();
                    QtyItem = _det.QtyItem.ToString();
                    SubQty = _det.SubQty.ToString();
                    SubCod = _det.SubCod.ToString();
                    FchElabor = _det.FchElabor.ToString();
                    FchVencim = _det.FchVencim.ToString();
                    UnmdItem = _det.UnmdItem.ToString();
                    PrcItem = _det.PrcItem.ToString();
                    PrcOtrMon = _det.PrcOtrMon.ToString();
                    Moneda = _det.Moneda.ToString();
                    FctConv = _det.FctConv.ToString();
                    DctoOtrMnda = _det.DctoOtrMnda.ToString();
                    RecargoOtrMnda = _det.RecargoOtrMnda.ToString();
                    MontoItemOtrMnda = _det.MontoItemOtrMnda.ToString();
                    DescuentoPct = _det.DescuentoPct.ToString();
                    DescuentoMonto = _det.DescuentoMonto.ToString();
                    TipoDscto = _det.TipoDscto.ToString();
                    ValorDscto = _det.ValorDscto.ToString();
                    RecargoPct = _det.RecargoPct.ToString();
                    RecargoMonto = _det.RecargoMonto.ToString();
                    TipoRecargo = _det.TipoRecargo.ToString();
                    ValorRecargo = _det.ValorRecargo.ToString();
                    CodImpAdic = _det.CodImpAdic.ToString();
                    MontoItem = _det.MontoItem.ToString();


                    doc.WriteStartElement("Detalle");

                    doc.WriteStartElement("NroLinDet");
                    doc.WriteValue(NroLinDet);
                    doc.WriteEndElement();

                    doc.WriteStartElement("CdgItem");

                    doc.WriteStartElement("TpoCodigo");
                    doc.WriteValue(TpoCodigo);
                    doc.WriteEndElement();

                    doc.WriteStartElement("VlrCodigo");
                    doc.WriteValue(VlrCodigo);
                    doc.WriteEndElement();

                    doc.WriteEndElement();
                    //fin cdgITEM

                    doc.WriteStartElement("IndExe");
                    doc.WriteValue(IndExe);
                    doc.WriteEndElement();

                    doc.WriteStartElement("Retenedor");

                    doc.WriteStartElement("IndAgente");
                    doc.WriteValue(IndAgente);
                    doc.WriteEndElement();

                    doc.WriteStartElement("MntBaseFaena");
                    doc.WriteValue(MntBaseFaena);
                    doc.WriteEndElement();

                    doc.WriteStartElement("MntMargComer");
                    doc.WriteValue(MntMargComer);
                    doc.WriteEndElement();

                    doc.WriteStartElement("PrcConsFinal");
                    doc.WriteValue(PrcConsFinal);
                    doc.WriteEndElement();

                    doc.WriteEndElement();
                    //fin retenedor

                    doc.WriteStartElement("NmbItem");
                    doc.WriteValue(NmbItem);
                    doc.WriteEndElement();

                    doc.WriteStartElement("DscItem");
                    doc.WriteValue(DscItem);
                    doc.WriteEndElement();

                    doc.WriteStartElement("QtyRef");
                    doc.WriteValue(QtyRef);
                    doc.WriteEndElement();

                    doc.WriteStartElement("UnmdRef");
                    doc.WriteValue(UnmdRef);
                    doc.WriteEndElement();

                    doc.WriteStartElement("PrcRef");
                    doc.WriteValue(PrcRef);
                    doc.WriteEndElement();

                    doc.WriteStartElement("QtyItem");
                    doc.WriteValue(QtyItem);
                    doc.WriteEndElement();

                    doc.WriteStartElement("Subcantidad");

                    doc.WriteStartElement("SubQty");
                    doc.WriteValue(SubQty);
                    doc.WriteEndElement();

                    doc.WriteStartElement("SubCod");
                    doc.WriteValue(SubCod);
                    doc.WriteEndElement();

                    doc.WriteEndElement();
                    //fin subcantidad

                    doc.WriteStartElement("FchElabor");
                    doc.WriteValue(FchElabor);
                    doc.WriteEndElement();

                    doc.WriteStartElement("FchVencim");
                    doc.WriteValue(FchVencim);
                    doc.WriteEndElement();

                    doc.WriteStartElement("UnmdItem");
                    doc.WriteValue(UnmdItem);
                    doc.WriteEndElement();

                    doc.WriteStartElement("PrcItem");
                    doc.WriteValue(PrcItem);
                    doc.WriteEndElement();

                    doc.WriteStartElement("OtrMnda");

                    doc.WriteStartElement("PrcOtrMon");
                    doc.WriteValue(PrcOtrMon);
                    doc.WriteEndElement();

                    doc.WriteStartElement("Moneda");
                    doc.WriteValue(Moneda);
                    doc.WriteEndElement();

                    doc.WriteStartElement("FctConv");
                    doc.WriteValue(FctConv);
                    doc.WriteEndElement();

                    doc.WriteStartElement("DctoOtrMnda");
                    doc.WriteValue(DctoOtrMnda);
                    doc.WriteEndElement();

                    doc.WriteStartElement("RecargoOtrMnda");
                    doc.WriteValue(RecargoOtrMnda);
                    doc.WriteEndElement();

                    doc.WriteStartElement("MontoItemOtrMnda");
                    doc.WriteValue(MontoItemOtrMnda);
                    doc.WriteEndElement();

                    doc.WriteEndElement();
                    //fin otrmnda

                    doc.WriteStartElement("DescuentoPct");
                    doc.WriteValue(DescuentoPct);
                    doc.WriteEndElement();

                    doc.WriteStartElement("DescuentoMonto");
                    doc.WriteValue(DescuentoMonto);
                    doc.WriteEndElement();

                    doc.WriteStartElement("SubDscto");

                    doc.WriteStartElement("TipoDscto");
                    doc.WriteValue(TipoDscto);
                    doc.WriteEndElement();

                    doc.WriteStartElement("ValorDscto");
                    doc.WriteValue(ValorDscto);
                    doc.WriteEndElement();

                    doc.WriteEndElement();
                    //fin subdscto

                    doc.WriteStartElement("RecargoPct");
                    doc.WriteValue(RecargoPct);
                    doc.WriteEndElement();

                    doc.WriteStartElement("RecargoMonto");
                    doc.WriteValue(RecargoMonto);
                    doc.WriteEndElement();

                    doc.WriteStartElement("SubRecargo");

                    doc.WriteStartElement("TipoRecargo");
                    doc.WriteValue(TipoRecargo);
                    doc.WriteEndElement();

                    doc.WriteStartElement("ValorRecargo");
                    doc.WriteValue(ValorRecargo);
                    doc.WriteEndElement();

                    doc.WriteEndElement();
                    //fin subrecargo

                    doc.WriteStartElement("CodImpAdic");
                    doc.WriteValue(CodImpAdic);
                    doc.WriteEndElement();

                    doc.WriteStartElement("MontoItem");
                    doc.WriteValue(MontoItem);
                    doc.WriteEndElement();
                    doc.WriteEndElement();
                }

                #endregion
                //fin Detalle

                //subtotinfo
                #region
                int j = 1;
                foreach (DTE_SubTotInfo subtot in sub)
                {
                    j++;
                    NroSTI = (Int32.Parse(subtot.NroSTI) + 1).ToString();
                    GlosaSTI = subtot.GlosaSTI.ToString();
                    OrdenSTI = subtot.OrdenSTI.ToString();
                    SubTotNetoSTI = subtot.SubTotNetoSTI.ToString();
                    SubTotIVASTI = subtot.SubTotIVASTI.ToString();
                    SubTotAdicSTI = subtot.SubTotAdicSTI.ToString();
                    SubTotExeSTI = subtot.SubTotExeSTI.ToString();
                    ValSubtotSTI = subtot.ValSubtotSTI.ToString();
                    LineasDeta = subtot.LineasDeta.ToString();

                    doc.WriteStartElement("SubTotInfo");

                    doc.WriteStartElement("NroSTI");
                    doc.WriteValue(NroSTI);
                    doc.WriteEndElement();

                    doc.WriteStartElement("GlosaSTI");
                    doc.WriteValue(GlosaSTI);
                    doc.WriteEndElement();

                    doc.WriteStartElement("OrdenSTI");
                    doc.WriteValue(OrdenSTI);
                    doc.WriteEndElement();

                    doc.WriteStartElement("SubTotNetoSTI");
                    doc.WriteValue(SubTotNetoSTI);
                    doc.WriteEndElement();

                    doc.WriteStartElement("SubTotIVASTI");
                    doc.WriteValue(SubTotIVASTI);
                    doc.WriteEndElement();

                    doc.WriteStartElement("SubTotAdicSTI");
                    doc.WriteValue(SubTotAdicSTI);
                    doc.WriteEndElement();

                    doc.WriteStartElement("SubTotExeSTI");
                    doc.WriteValue(SubTotExeSTI);
                    doc.WriteEndElement();

                    doc.WriteStartElement("ValSubtotSTI");
                    doc.WriteValue(ValSubtotSTI);
                    doc.WriteEndElement();

                    doc.WriteStartElement("LineasDeta");
                    doc.WriteValue(LineasDeta);
                    doc.WriteEndElement();

                    doc.WriteEndElement();
                }
                #endregion
                //fin subtotInfo


                // DSCRCGGLOBAL
                #region
                foreach (DTE_DscrGlobal _dscrcg in dscrcg)
                {
                    NroLinDR = Int32.Parse((_dscrcg.NroLinDR) + 1).ToString();
                    TpoMov = _dscrcg.TpoMov.ToString();
                    GlosaDR = _dscrcg.GlosaDR.ToString();
                    TpoValor = _dscrcg.TpoValor.ToString();
                    ValorDR = _dscrcg.ValorDR.ToString();
                    ValorDROtrMnda = _dscrcg.ValorDROtrMnda.ToString();
                    IndExeDR = _dscrcg.IndExeDR.ToString();

                    doc.WriteStartElement("DscRcgGlobal");

                    doc.WriteStartElement("NroLinDR");
                    doc.WriteValue(NroLinDR);
                    doc.WriteEndElement();

                    doc.WriteStartElement("TpoMov");
                    doc.WriteValue(TpoMov);
                    doc.WriteEndElement();

                    doc.WriteStartElement("GlosaDR");
                    doc.WriteValue(GlosaDR);
                    doc.WriteEndElement();

                    doc.WriteStartElement("TpoValor");
                    doc.WriteValue(TpoValor);
                    doc.WriteEndElement();

                    doc.WriteStartElement("ValorDR");
                    doc.WriteValue(ValorDR);
                    doc.WriteEndElement();

                    doc.WriteStartElement("ValorDROtrMnda");
                    doc.WriteValue(ValorDROtrMnda);
                    doc.WriteEndElement();

                    doc.WriteStartElement("IndExeDR");
                    doc.WriteValue(IndExeDR);
                    doc.WriteEndElement();

                    doc.WriteEndElement();
                }
                #endregion
                //FIN DSCRCGGLOBAL

                //referencia
                #region

                foreach (DTE_referencia _refer in refer)
                {
                    NroLinRef = Int32.Parse((_refer.NroLinRef) + 1).ToString();
                    TpoDocRef = _refer.TpoDocRef.ToString();
                    IndGlobal = _refer.IndGlobal.ToString();
                    FolioRef = _refer.FolioRef.ToString();
                    RUTOtr = _refer.RUTOtr.ToString();
                    FchRef = _refer.FchRef.ToString();
                    CodRef = _refer.CodRef.ToString();
                    RazonRef = _refer.RazonRef.ToString();

                    doc.WriteStartElement("Referencia");

                    doc.WriteStartElement("NroLinRef");
                    doc.WriteValue(NroLinRef);
                    doc.WriteEndElement();

                    doc.WriteStartElement("TpoDocRef");
                    doc.WriteValue(TpoDocRef);
                    doc.WriteEndElement();

                    doc.WriteStartElement("IndGlobal");
                    doc.WriteValue(IndGlobal);
                    doc.WriteEndElement();

                    doc.WriteStartElement("FolioRef");
                    doc.WriteValue(FolioRef);
                    doc.WriteEndElement();

                    doc.WriteStartElement("RUTOtr");
                    doc.WriteValue(RUTOtr);
                    doc.WriteEndElement();

                    doc.WriteStartElement("FchRef");
                    doc.WriteValue(FchRef);
                    doc.WriteEndElement();

                    doc.WriteStartElement("CodRef");
                    doc.WriteValue(CodRef);
                    doc.WriteEndElement();

                    doc.WriteStartElement("RazonRef");
                    doc.WriteValue(RazonRef);
                    doc.WriteEndElement();

                    doc.WriteEndElement();
                }
                #endregion
                //FIN REFERENCIA

                //comisiones
                #region

                foreach (DTE_comisiones _comi in comi)
                {
                    NroLinCom = Int32.Parse((_comi.NroLinCom) + 1).ToString();
                    TipoMovim = _comi.TipoMovim.ToString();
                    Glosa = _comi.Glosa.ToString();
                    TasaComision = _comi.TasaComision.ToString();
                    ValComNeto = _comi.ValComNeto.ToString();
                    ValComExe = _comi.ValComExe.ToString();
                    ValComIVA = _comi.ValComIVA.ToString();

                    doc.WriteStartElement("Comisiones");

                    doc.WriteStartElement("NroLinCom");
                    doc.WriteValue(NroLinCom);
                    doc.WriteEndElement();

                    doc.WriteStartElement("TipoMovim");
                    doc.WriteValue(TipoMovim);
                    doc.WriteEndElement();

                    doc.WriteStartElement("Glosa");
                    doc.WriteValue(Glosa);
                    doc.WriteEndElement();

                    doc.WriteStartElement("TasaComision");
                    doc.WriteValue(TasaComision);
                    doc.WriteEndElement();

                    doc.WriteStartElement("ValComNeto");
                    doc.WriteValue(ValComNeto);
                    doc.WriteEndElement();

                    doc.WriteStartElement("ValComExe");
                    doc.WriteValue(ValComExe);
                    doc.WriteEndElement();

                    doc.WriteStartElement("ValComIVA");
                    doc.WriteValue(ValComIVA);
                    doc.WriteEndElement();

                    doc.WriteEndElement();
                }
                #endregion
                //fin Comisiones

                doc.WriteStartElement("TED");
                doc.WriteStartAttribute("version");
                doc.WriteValue("1.0");
                doc.WriteEndAttribute();
                // Lectura de datos desde Folios Autorizados
                //------------------------------------------
                clases.Metodos m = new clases.Metodos();
            string Path = get_caf(rut_user,int.Parse(TipoDTE));
                string RE = m.obtieneLecturaXML(Path, "RE");
                string RS = m.obtieneLecturaXML(Path, "RS");
                string TD = m.obtieneLecturaXML(Path, "TD");
                string D = m.obtieneLecturaXML(Path, "D");
                string H = m.obtieneLecturaXML(Path, "H");
                string FA = m.obtieneLecturaXML(Path, "FA");
                string M = m.obtieneLecturaXML(Path, "M");
                string E = m.obtieneLecturaXML(Path, "E");
                string IDK = m.obtieneLecturaXML(Path, "IDK");
                ///////////lectura de frma desde el caf
                DataSet ds = new DataSet();
                string CAF = Path;
                ds.ReadXml((XmlReader.Create(new StringReader(CAF))));
                string FRMA = ds.Tables["FRMA"].Rows[0][1].ToString();
                string FRMT = m.obtieneLecturaXML(Path, string.Format("FRMT algoritmo={0}SHA1withRSA{0} ", (char)34));
                string RSASK = m.obtienePrivateKeyFactura(Path);
                string RSAPUBK = m.obtienePublicKeyFactura(Path);
                doc.WriteStartElement("DD");

                doc.WriteStartElement("RE");
                doc.WriteValue(RE);
                doc.WriteEndElement();

                doc.WriteStartElement("TD");
                doc.WriteValue(TD);
                doc.WriteEndElement();

                doc.WriteStartElement("F");
                doc.WriteValue(Folio);
                doc.WriteEndElement();

                doc.WriteStartElement("FE");
                doc.WriteValue(FchEmis);
                doc.WriteEndElement();

                doc.WriteStartElement("RR");
                doc.WriteValue(RUTRecep);
                doc.WriteEndElement();

                doc.WriteStartElement("RSR");
                doc.WriteValue(RznSocRecep);
                doc.WriteEndElement();

                doc.WriteStartElement("MNT");
                doc.WriteValue(MntTotal);
                doc.WriteEndElement();
                //  nombrePrimerItem

                doc.WriteStartElement("IT1");
                doc.WriteValue(NmbItem);
                doc.WriteEndElement();
                //Fin de datos precargados
                doc.WriteStartElement("CAF");
                doc.WriteStartAttribute("version");
                doc.WriteValue("1.0");
                doc.WriteEndAttribute();

                doc.WriteStartElement("DA");
                doc.WriteStartElement("RE");
                doc.WriteValue(RUTEmisor);
                doc.WriteEndElement();

                doc.WriteStartElement("RS");
                doc.WriteValue(RS);
                doc.WriteEndElement();

                doc.WriteStartElement("TD");
                doc.WriteValue(TD);
                doc.WriteEndElement();

                doc.WriteStartElement("RNG");
                doc.WriteStartElement("D");
                doc.WriteValue(D);
                doc.WriteEndElement();

                doc.WriteStartElement("H");
                doc.WriteValue(H);
                doc.WriteEndElement();
                doc.WriteEndElement();
                //fin RNG
                doc.WriteStartElement("FA");
                doc.WriteValue(FA);
                doc.WriteEndElement();

                doc.WriteStartElement("RSAPK");
                doc.WriteStartElement("M");
                doc.WriteValue(M);
                doc.WriteEndElement();

                doc.WriteStartElement("E");
                doc.WriteValue(E);
                doc.WriteEndElement();
                doc.WriteEndElement();
                //fin RSAPK
                doc.WriteStartElement("IDK");
                doc.WriteValue(IDK);
                doc.WriteEndElement();
                doc.WriteEndElement();
                //fin DA
                doc.WriteStartElement("FRMA");
                doc.WriteStartAttribute("algoritmo");
                doc.WriteValue("SHA1withRSA");
                doc.WriteEndAttribute();
                doc.WriteValue(FRMA);
                doc.WriteEndElement();
                doc.WriteEndElement();
                //fin CAF
                string TSTED = DateTime.Now.ToString("yyy/MM/dd" + "T" + "HH:mm:ss").Replace('/', '-');
                doc.WriteStartElement("TSTED");
                doc.WriteValue(TSTED);
                doc.WriteEndElement();

                doc.WriteEndElement();
                //fin Documento
                ///////////////////////GENERAR FRMT
                string AUXFRMT = "<DD><RE>" + RE + "</RE><TD>" + TD + "</TD><F>" + Folio + "</F><FE>" +
                FchEmis + "</FE><RR>" + RUTRecep + "</RR><RSR>" + RznSocRecep + "</RSR><MNT>" + MntTotal + "</MNT><IT1>" + NmbItem + "</IT1><CAF version=" + "\"1.0\"" + "><DA><RE>" +
                RE + "</RE><RS>" + RS + "</RS><TD>" + TD + "</TD><RNG><D>" + D + "</D><H>" + H + "</H></RNG><FA>" + FA + "</FA><RSAPK><M>" + M + "</M><E>" +
                E + "</E></RSAPK><IDK>" + IDK + "</IDK></DA><FRMA algoritmo=" + "\"SHA1withRSA\"" + ">" + FRMA + "</FRMA></CAF><TSTED>" + TSTED + "</TSTED></DD>";
                string PP = ds.Tables["AUTORIZACION"].Rows[0][1].ToString();
                m.PruebaTimbreDD(AUXFRMT, PP);
                string CONVERSIONFRMT = Hash_String_SHA1(AUXFRMT);
                /////////////////////////////////
                doc.WriteStartElement("FRMT");
                doc.WriteStartAttribute("algoritmo");
                doc.WriteValue("SHA1withRSA");
                doc.WriteEndAttribute();
                doc.WriteValue(m.PruebaTimbreDD(AUXFRMT, PP));
                doc.WriteEndElement();
                doc.WriteEndElement();
                string TmstFirma = DateTime.Now.ToString("yyy/MM/dd" + "T" + "HH:mm:ss").Replace('/', '-');
                doc.WriteStartElement("TmstFirma");
                doc.WriteValue(TmstFirma);
                doc.WriteEndElement();
                doc.WriteEndDocument();
                // fin documentO
                doc.Flush();
                doc.Close();
            }
            
        public static string Hash_String_SHA1(string strDatos)
        {
            string functionReturnValue = null;
            byte[] Datos = new byte[(strDatos.Length) + 1];
            byte[] Resultado = null;
            functionReturnValue = "";
            try
            {
                SHA1CryptoServiceProvider sha = new SHA1CryptoServiceProvider();
                //Esta es una implementación de la clase abstracta SHA1.
                Resultado = sha.ComputeHash(Datos);
                functionReturnValue = System.Convert.ToBase64String(Resultado);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            return functionReturnValue;
        }

        public void InsertarDTE(string rutaSetDTE, string rutaDTE, string rut_session, string nom_doc, string uri)
        {
            uri = uri.Replace("-", "");
            clases.Metodos m = new clases.Metodos();
            //cargo dte y lo paso a string ...
            XmlDocument DTE = new XmlDocument();
            DTE.PreserveWhitespace = true;
            DTE.Load(rutaDTE);
            DTE.Save(rutaDTE);
            DTE.Load(rutaDTE);
            DTE.DocumentElement.RemoveAttribute("xml");
            //lo firmo 
            string a = @"C:\tyscom xml\certificados\fabian.pfx";
            string c = "xmay3187";
            X509Certificate2 cert = new X509Certificate2(a, c);
            m.firmarDocumentoXml(ref DTE, cert, "#" + uri);
            string aux1 = DTE.InnerXml;
            Regex.Replace(aux1, @">(\s+)<|><", ">\r\n<");
            //quito encabezado de xml para realizar una insercion correcta...
            //  aux1 = aux1.Substring("<?xml version=\"1.0\" encoding=\"ISO-8859-1\"?>".Length);
            Regex.Replace(aux1, @">(\s+)<|><", ">\r\n<");
            //Console.Write(aux.IndexOf("</SetDTE>").ToString());//busca el numero de set dte para insertar la cadena

            //cargo set dte y lo paso a string ...
            XmlDocument SetDTE = new XmlDocument();
            SetDTE.PreserveWhitespace = true;
            SetDTE.Load(rutaSetDTE);
            SetDTE.Save(rutaSetDTE);
            SetDTE.Load(rutaSetDTE);
            string aux = SetDTE.OuterXml.ToString();
            Regex.Replace(aux, @">(\s+)<|><", ">\r\n<");

            //Console.Write(Int32.Parse( aux.IndexOf("</SetDTE>").ToString())); //busco posicion de setdte
            string insercion = aux.Insert(Int32.Parse(aux.IndexOf("</SetDTE>").ToString()), aux1);
            Regex.Replace(insercion, @">(\s+)<|><", ">\r\n<");
            // Console.WriteLine(insercion);
            DTE.LoadXml(insercion);
            m.firmarDocumentoXml(ref DTE, cert, "#" + uri);
            DTE.Save(nom_doc);
        }

        public string get_direct_doc_firmado(string session_rut) {
  
            int id_empresa = Int32.Parse(get_empresa(session_rut));

            var data =
            (from archivos in db.RUTA_GUARDADO_EMPRESA
             where archivos.ID_EMPRESA == id_empresa
             select new
             {
                 ruta_doc_firmado_sii = archivos.RUTA_ENVIO_SII
                
             });
            string ruta_doc_firmado_sii = data.FirstOrDefault().ruta_doc_firmado_sii.ToString();

                return ruta_doc_firmado_sii;
        }

        public void procedimientofirma(string SetDTE, string DTE, string rut_session, string nom_doc,string uri)
        {
            //genero set dte y lo guardo
            // GenerarSetDTE();
            string path = @"c:\TEMP\";
            if (!Directory.Exists(path))
            {
                DirectoryInfo di = Directory.CreateDirectory(path);
                di.Attributes = FileAttributes.Directory | FileAttributes.Hidden;
            }
            string time = DateTime.Now.ToString();
            time = time.Replace(":", "");
            time = time.Replace("/", "");
         
            string ruta = @"c:\TEMP\" + "set_dte" + time + ".xml";

            XmlDocument doc = new XmlDocument();
            //   doc.PreserveWhitespace = true;
            doc.LoadXml(SetDTE);
            doc.Save(ruta);
            doc.Load(ruta);
            string xml = doc.InnerXml;
            xml = xml.Replace("iso-", "ISO-");
            doc.LoadXml(xml);
            doc.Save(ruta);
            //genero  dte y lo guardo..
            // GenerarDTE();
            XmlDocument doc1 = new XmlDocument();
            doc1.Load(DTE);
            string xml1 = doc1.InnerXml;
            xml1 = xml1.Replace("iso-", "ISO-");
            doc1.LoadXml(xml1);
            doc1.Save(DTE);
            //EJECUTO METODO PARA INSERTAR DTE EN SETDTE Y FIRMAR EN MEMORIA
            InsertarDTE(ruta, DTE, rut_session, nom_doc, uri);


        }

        public void generar_pdf(string rutadte,string plantilla_xslt,string ruta_doc_firmado) {
           

            clases.pdf_class pdf = new clases.pdf_class();          
            pdf.make_pdf(rutadte, plantilla_xslt, ruta_doc_firmado);
        }

        public int obtiene_idUsuario(string rut_usuario) {
            int id=-1;

            var id_usu = from x in db.USUARIO
                         where x.RUT == rut_usuario
                         select new
                         {
                             x.ID_USU
                         };
            id = id_usu.FirstOrDefault().ID_USU;

            return id;
        }
        public string ObtieListaDetalle(int id_emp) {
            var ListaProd = from x in db.DETALLE_PROD where x.ID_EMPRESA == (id_emp) select x;

            DataTable dt = new DataTable();

            dt = Linq_to_dt(ListaProd).Copy();
            dt = replace_data_tostring(dt).Copy();


            Metodos m = new Metodos();
            string _ListaProd = m.Dt_to_json(dt);
            return _ListaProd;

        }

        public string ObtieneListaClientes(int id_emp) {

            var ListaClie = from x in db.CLIENTE
                            where x.ID_EMPRESA==(id_emp)
                            select new
                            {
                                x.ID_CLIENTE,
                                x.RUT_CLIENTE,
                                x.RZN_SOC_CLIE,
                                x.CIUDAD,
                                x.COMUNA,
                                x.GIRO_CLIENTE,
                                x.DIRECCION_CLIENTE,
                                x.TELEFONO_CLIENTE,
                                x.EMAIL_CLIENTE,
                                x.ESTADO_CLIENTE,
                                x.FECHA_CREACION
                            };

            DataTable dt = new DataTable();

            dt = Linq_to_dt(ListaClie).Copy();
         dt=replace_data_tostring(dt).Copy();
            

            Metodos m = new Metodos();
            string ListaClientes = m.Dt_to_json(dt);
            return ListaClientes;
        }

        public string ObtieneListaEmpresa(string rut_usuario) {

          int id_usu= obtiene_idUsuario(rut_usuario);
         
            var ListaEmp = from x in db.EMPRESA
                           join y in db.EMP_USU
                           on x.ID_EMPESA equals y.ID_EMP
                           where y.ID_USU.Equals(id_usu)
                           select new {
                               x.ID_EMPESA,
                               x.RUT_EMPRESA,
                               x.RAZON_SOCIAL,
                               x.DIRECCION,
                               x.COMUNA,
                               x.CIUDAD,
                               x.FECHA_RESOLUCION,
                               x.CODIGO_SII_SUCUR,
                               x.GIRO,
                               x.NUM_RESOL
                           };
            DataTable dt = new DataTable();
               dt= Linq_to_dt(ListaEmp).Copy();
            Metodos m = new Metodos();
            string Lista_emp = m.Dt_to_json(dt);


            return Lista_emp;

        }
        public DataTable replace_data_tostring(DataTable dt) {
            DataTable dtClone = dt.Clone(); //just copy structure, no data
            for (int i = 0; i < dtClone.Columns.Count; i++)
            {
                if (dtClone.Columns[i].DataType != typeof(string))
                    dtClone.Columns[i].DataType = typeof(string);
            }

            foreach (DataRow dr in dt.Rows)
            {
                dtClone.ImportRow(dr);
            }
            return dtClone;
        }
    }
}
