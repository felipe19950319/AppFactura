using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ws_OperacionesFactura.Class
{
    public enum TypeFile {
        Ruta=0,
        Fisico=1
    }

    public struct TipoArchivo {
        public const string CertificadoDigital = "CERTDIG";
        public const string DTE = "DTE";
        public const string PDF_DTE = "PDF_DTE";
        public const string FOLIO = "FOLIO";
    }
    public class File_
    {
        public TypeFile fisico;
        public string Path;
        public string _Type;//extension
        public int RutEmpresa;//sin dv
        public string tipoArchivo;
        public string FisicalFile;//pensado para almacenar archivos en bd menores a 200kb base64 o xml
        public string FileName;

    }
}