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
    
    public partial class EMPRESA
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public EMPRESA()
        {
            this.CAF = new HashSet<CAF>();
            this.CERTIFICADO_DIGITAL = new HashSet<CERTIFICADO_DIGITAL>();
            this.CLIENTE = new HashSet<CLIENTE>();
            this.DETALLE_PROD = new HashSet<DETALLE_PROD>();
            this.DTE_EMISION = new HashSet<DTE_EMISION>();
            this.DTE_RECEPCION = new HashSet<DTE_RECEPCION>();
            this.EMP_USU = new HashSet<EMP_USU>();
            this.LIBRO_COMPRA = new HashSet<LIBRO_COMPRA>();
            this.LIBRO_VENTA = new HashSet<LIBRO_VENTA>();
            this.RUTA_GUARDADO_EMPRESA = new HashSet<RUTA_GUARDADO_EMPRESA>();
        }
    
        public int ID_EMPESA { get; set; }
        public string RUT_EMPRESA { get; set; }
        public string RAZON_SOCIAL { get; set; }
        public string DIRECCION { get; set; }
        public string COMUNA { get; set; }
        public string CIUDAD { get; set; }
        public string FECHA_RESOLUCION { get; set; }
        public string CODIGO_SII_SUCUR { get; set; }
        public string GIRO { get; set; }
        public Nullable<int> NUM_RESOL { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CAF> CAF { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CERTIFICADO_DIGITAL> CERTIFICADO_DIGITAL { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CLIENTE> CLIENTE { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DETALLE_PROD> DETALLE_PROD { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DTE_EMISION> DTE_EMISION { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DTE_RECEPCION> DTE_RECEPCION { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EMP_USU> EMP_USU { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LIBRO_COMPRA> LIBRO_COMPRA { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LIBRO_VENTA> LIBRO_VENTA { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RUTA_GUARDADO_EMPRESA> RUTA_GUARDADO_EMPRESA { get; set; }
    }
}
