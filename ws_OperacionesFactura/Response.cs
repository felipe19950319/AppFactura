using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//FELIPE PASACHE 2018 -08-01
namespace ws_OperacionesFactura
{
    public enum Code
    {
        OK=200,
        ERROR=500        
    }

    public enum Type
    {
        json=0,//(se debe parsear en el cliente)
        base64=1,//se procesa directamente
        text=2//se interpreta
    }
    public class Response
    {
        public Code code;   
        public Type type;
        public string ObjectResponse;
        public Exception ex;//si tenemos una excepcion
    }

  
}