using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace WcfService_factura.clases
{
    [DataContract]
  public  class SetDTE
    {

        [DataMember]
        public string RutEmisor { get; set; }

       
        [DataMember]
        public string RutEnvia { get; set; }

    
        [DataMember]
        public string receptor { get; set; }

       
        [DataMember]
        public string FchResol { get; set; }

       
        [DataMember]
        public string NroResol { get; set; }

      
        [DataMember]
        public string TmstFirmmaEnv { get; set; }

       
        [DataMember]
        public string TpoDTE { get; set; }

     
        [DataMember]
        public string NroDTE { get; set; }
        /// 
    }

   

}