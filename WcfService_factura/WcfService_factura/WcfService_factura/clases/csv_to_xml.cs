using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace WcfService_factura.clases
{
    public class csv_to_xml
    {

        public bool csv_xml(string ruta_empresas_csv,string ruta_save_xml)
        {
            bool procedimiento;
            if (String.IsNullOrEmpty(ruta_save_xml) || String.IsNullOrEmpty(ruta_save_xml)) {
                procedimiento = false;
            }
            else {
                var lines = File.ReadAllLines(ruta_empresas_csv);
                var xml = new XElement("Empresas",
                  lines.Select(line => new XElement("Empresa",
                      line.Split(';')
                          .Select((column, index) => new XElement("Column" + index, column)))));

                xml.Save(ruta_save_xml);

                procedimiento = true;
            }
            return procedimiento;
        }

    }
}