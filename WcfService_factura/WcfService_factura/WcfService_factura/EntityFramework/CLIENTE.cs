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
    
    public partial class CLIENTE
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CLIENTE()
        {
            this.DTE_EMISION = new HashSet<DTE_EMISION>();
        }
    
        public int ID_CLIENTE { get; set; }
        public Nullable<int> ID_EMPRESA { get; set; }
        public string RUT_CLIENTE { get; set; }
        public string RZN_SOC_CLIE { get; set; }
        public string CIUDAD { get; set; }
        public string COMUNA { get; set; }
        public string GIRO_CLIENTE { get; set; }
        public string DIRECCION_CLIENTE { get; set; }
        public string TELEFONO_CLIENTE { get; set; }
        public string EMAIL_CLIENTE { get; set; }
        public string ESTADO_CLIENTE { get; set; }
        public string FECHA_CREACION { get; set; }
        public string ESTADO { get; set; }
    
        public virtual EMPRESA EMPRESA { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DTE_EMISION> DTE_EMISION { get; set; }
    }
}