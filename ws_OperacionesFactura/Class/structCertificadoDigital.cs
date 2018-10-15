using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ws_OperacionesFactura
{
    public class structCertificadoDigital
    {
        public string RutEmpresa { get; set; }
        public string Password { get; set; }
        public string Path { get; set; }
        public string TypeFile { get; set; }
        public string Base64 { get; set; }  
    }
}