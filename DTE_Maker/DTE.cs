using DTE_Maker;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

//felipe pasache 2018 07 23 a partir de entidades se crea el xml en base a la serializacion 
public class MakeDte : Extends
{
    [XmlRoot("DTE"), Serializable]
    public class DTE
    {
        [XmlAttribute("version")]
        public string version = "1.0";
        [XmlIgnore]
        public string TipoOperacion;
        [XmlElement("Documento")]
        public Documento documento = new Documento();
    }
    public class Documento
    {
        [XmlAttribute("ID")]
        public string ID { get; set; }

        [XmlElement("Encabezado")]
        public Encabezado encabezado = new Encabezado();
        [XmlElement("Detalle")]
        public List<Detalle> detalle = new List<Detalle>();
        [XmlElement("Referencia")]
        public List<Referencia> referencia = new List<Referencia>();
    }
    public class Encabezado
    {
        [XmlElement("IdDoc")]
        public IdDoc iddoc = new IdDoc();
        [XmlElement("Emisor")]
        public Emisor emisor = new Emisor();
        [XmlElement("Receptor")]
        public Receptor receptor = new Receptor();
        [XmlElement("Totales")]
        public Totales totales = new Totales();
    }
    public class IdDoc
    {
        [XmlElement("TipoDTE")]
        public int TipoDTE { get; set; }
        [XmlElement("Folio")]
        public int Folio { get; set; }
        [XmlElement("FchEmis")]
        public string FchEmis { get; set; }
        [XmlElement("FmaPago")]
        public int FmaPago { get; set; }
    }
    public class Emisor
    {
        [XmlElement("RUTEmisor")]
        public string RUTEmisor { get; set; }
        [XmlElement("RznSoc")]
        public string RznSoc { get; set; }
        [XmlElement("GiroEmis")]
        public string GiroEmis { get; set; }
        [XmlElement("Acteco")]
        public string Acteco { get; set; }
        [XmlElement("DirOrigen")]
        public string DirOrigen { get; set; }
        [XmlElement("CmnaOrigen")]
        public string CmnaOrigen { get; set; }
        [XmlElement("CiudadOrigen")]
        public string CiudadOrigen { get; set; }
    }
    public class Receptor
    {
        [XmlElement("RUTRecep")]
        public string RUTRecep { get; set; }
        [XmlElement("RznSocRecep")]
        public string RznSocRecep { get; set; }
        [XmlElement("GiroRecep")]
        public string GiroRecep { get; set; }
        [XmlElement("DirRecep")]
        public string DirRecep { get; set; }
        [XmlElement("CmnaRecep")]
        public string CmnaRecep { get; set; }
        [XmlElement("CiudadRecep")]
        public string CiudadRecep { get; set; }
    }
    public class Totales
    {
        [XmlElement("MntNeto")]
        public decimal MntNeto { get; set; }
        [XmlElement("MntExe")]
        public decimal MntExe { get; set; }
        [XmlElement("TasaIVA")]
        public decimal TasaIVA { get; set; }
        [XmlElement("IVA")]
        public decimal IVA { get; set; }
        [XmlElement("MntTotal")]
        public decimal MntTotal { get; set; }
    }
    public class Detalle
    {
        [XmlElement("NroLinDet")]
        public int NroLinDet { get; set; }
        [XmlElement("CdgItem")]
        public List<CdgItem> cdgItem = new List<CdgItem>();
        [XmlElement("NmbItem")]
        public string NmbItem { get; set; }
        [XmlElement("DscItem")]
        public string DscItem { get; set; }
        [XmlElement("QtyItem")]
        public int QtyItem { get; set; }
        [XmlElement("PrcItem")]
        public decimal PrcItem { get; set; }
        [XmlElement("MontoItem")]
        public decimal MontoItem { get; set; }
    }

    public class CdgItem
    {
        [XmlElement("TpoCodigo")]
        public string TpoCodigo { get; set; }
        [XmlElement("VlrCodigo")]
        public string VlrCodigo { get; set; }
    }
    public class Referencia
    {
        [XmlElement("NroLinRef")]
        public int NroLinRef { get; set; }

        [XmlElement("TpoDocRef")]
        public int TpoDocRef { get; set; }

        [XmlElement("FolioRef")]
        public int FolioRef { get; set; }

        [XmlElement("FchRef")]
        public int FchRef { get; set; }

        [XmlElement("CodRef")]
        public int CodRef { get; set; }

        [XmlElement("RazonRef")]
        public int RazonRef { get; set; }
    }

   
    public class Ids
    {
        public int IdEmpresa { get; set; }
        public int IdEmisor { get; set; }
        public int IdReceptor { get; set; }
    }

}


