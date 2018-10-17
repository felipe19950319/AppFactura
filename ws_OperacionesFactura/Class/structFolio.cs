using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ws_OperacionesFactura
{
    public class structFolio
    {
        public string NameFile { get; set; }
        public string Type { get; set; }
        public int TipoDocumento { get; set; }
        public string RutEmpresa { get; set; }
        public int FolioDesde { get; set; }
        public int FolioHasta { get; set; }
        public string xml { get; set; }

        /*
                   ObjFolio.name = name;
                    ObjFolio.type = type;
                    ObjFolio.TipoDoc = TipoDoc;
                    ObjFolio.RutEmpresa = RutEmpresa;
                    ObjFolio.FolioDesde = FolioDesde;
                    ObjFolio.FolioHasta = FolioHasta;
                    ObjFolio.xml = Folio_xml;
         */
    }
}
