using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Controlador
{
    public class Session
    {
        public string emp { get; set; }
        //nombre empresa en uso !
        public string get_nombre_emp()
        {
            string empresa = "";
            try
            {
                Session s = (Session)HttpContext.Current.Session["Empresa"];
             
                if (s.emp == string.Empty)
                {
                    s.emp = "";
                }
                else
                {
                    empresa = s.emp;

                }
                return empresa;
            }
            catch (NullReferenceException) {
                return empresa;
       
            }
        }
        public string ID_emp { get; set; }
        public string get_id_emp()
        {
            Session s = (Session)HttpContext.Current.Session["id_emp"];
            string id = "";
            
            try
            {
                id = s.ID_emp;
                return id;
            }
            catch (NullReferenceException)
            {
                return id;
                
            }
        }
        public string _tpo_doc { get; set; }
        public string get_tpo_doc()
        {
            Session s = (Session)HttpContext.Current.Session["tipo_doc"];
            string tipo_doc = s._tpo_doc;
            return tipo_doc;
        }

        public string _caf { get; set; }
        public string get_caf() {
            Session s = (Session)HttpContext.Current.Session["CAF"];
            string caf_data = s._caf;
            return caf_data;
        }

        public string rut { get; set; }
        public string get_sessionRUT() {
            Session s = (Session)HttpContext.Current.Session["RUT_SESSION"];
            string session = s.rut;
            return session;
        }

        public string JSON_ListaEmpresas { get; set; }
        public string get_ListaEmpresas()
        {
            Session s = (Session)HttpContext.Current.Session["JSON_ListaEmpresas"];
            string lista_empresas = s.JSON_ListaEmpresas;
            return lista_empresas;
        }
    }
    
}
