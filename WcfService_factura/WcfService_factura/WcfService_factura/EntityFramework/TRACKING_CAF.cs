//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WcfService_factura.EntityFramework
{
    using System;
    using System.Collections.Generic;
    
    public partial class TRACKING_CAF
    {
        public int ID_TRACKING { get; set; }
        public Nullable<int> ID_CAF { get; set; }
        public Nullable<int> ID_DTE { get; set; }
        public Nullable<int> NRO_ACTUAL { get; set; }
        public string FECHA { get; set; }
        public string ESTADO { get; set; }
        public string TRACKING_ESTADO_DTE { get; set; }
    
        public virtual CAF CAF { get; set; }
        public virtual DTE_EMISION DTE_EMISION { get; set; }
    }
}
