using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Validation.Integration.WCF;
using WcfService_factura.clases;
using System.ServiceModel.Web;
using System.Data;
using System.IO;
using System.Runtime.InteropServices;

namespace WcfService_factura
{
       
    [ServiceContract] 
    public interface IServicioFactura
    {
        [OperationContract]
        DTE_DETALLE[] det(DataTable dt);
        [OperationContract]
        DTE_referencia[] refer(DataTable dt);
        [OperationContract]
        DTE_DscrGlobal[] desc_global(DataTable dt);
        [OperationContract]
        int redondear(decimal algo);
        [OperationContract]
        void generar_pdf(string rutadte, string plantilla_xslt, string ruta_doc_firmado);
        [OperationContract]
        void procedimientofirma(string SetDTE, string DTE, string rut_session, string nom_doc,string uri);
        [OperationContract]
        bool login(string rut, string password);
        [OperationContract]
        string get_empresa(string rut);
        [OperationContract]
        string get_empresaRUT(string rut_user);
        [OperationContract]
        bool ingresa_Caf(string ruta_xml_caf, string rut);
        [OperationContract]
        string upload_sii(string rutemisor, string rutempresa, string rutaxml);
        [OperationContract]
        bool parse_csv_to_xml(string empresas_csv, string ruta_save_xml);
        [OperationContract]
        bool xml_emp_to_db(string ruta_emp_contribuyente);
        [OperationContract]
        string get_token();
        [OperationContract]
        void genera_libro_v(string fecha_perdiodo, int id_empresa, string session);
        [OperationContract] 
        string get_setDTE(string session);
        [OperationContract]
        int get_num_folio(string rut_user, int tipo_doc);
        [OperationContract]
        string ObtieListaDetalle(int id_emp);
        [OperationContract]
        string ObtieneListaEmpresa(string rut_usuario);
        [OperationContract]
        string ObtieneListaClientes(int id_emp);
         [OperationContract]
        int administrador_folios(int nro_actual, string xml_respuesta, string rut_user, int tipo_doc,int id_doc);
        [OperationContract]
        bool Genera_dte_envia_(SetDTE setdte, [Optional] DTE_idDoc dte_idDoc,
             [Optional]  DTE_Emisor dte_emis, [Optional]DTE_Receptor dte_recep, [Optional]DTE_Transporte trans,
             [Optional]  DTE_Totales tot, [Optional] List<DTE_DETALLE> det, [Optional] List<DTE_SubTotInfo> sub, [Optional] List<DTE_DscrGlobal> dscrcg,
             [Optional]  List<DTE_referencia> refer, [Optional] List<DTE_comisiones> comi, string session_rut, string tipo_doc, string hora_envio);
        }

    }

