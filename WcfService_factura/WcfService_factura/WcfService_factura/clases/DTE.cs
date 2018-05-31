using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace WcfService_factura.clases
{
    [DataContract]
    public class DTE_idDoc:IDisposable
    {
        [DataMember]
        public string ID  { get; set; }
        [DataMember]
        public string TipoDTE { get; set; }
        [DataMember]
        public string Folio { get; set; }
        [DataMember]
        public string FchEmis { get; set; }
        [DataMember]
        public string IndNoRebaja { get; set; }
        [DataMember]
        public string TipoDespacho { get; set; }
        [DataMember]
        public string IndTraslado { get; set; }
        [DataMember]
        public string TpoImpresion { get; set; }
        [DataMember]
        public string IndServicio { get; set; }
        [DataMember]
        public string MntBruto { get; set; }
        [DataMember]
        public string FmaPago { get; set; }
        [DataMember]
        public string FmaPagExp { get; set; }
        [DataMember]
        public string FchCancel { get; set; }
        [DataMember]
        public string MntCancel { get; set; }
        [DataMember]
        public string SaldoInsol { get; set; }
        [DataMember]
        public string FchPago { get; set; }
        [DataMember]
        public string MntPago { get; set; }
        [DataMember]
        public string GlosaPagos { get; set; }
        [DataMember]
        public string PeriodoDesde { get; set; }
        [DataMember]
        public string PeriodoHasta { get; set; }
        [DataMember]
        public string MedioPago { get; set; }
        [DataMember]
        public string TpoCtaPago { get; set; }
        [DataMember]
        public string NumCtaPago { get; set; }
        [DataMember]
        public string BcoPago { get; set; }
        [DataMember]
        public string TermPagoCdg { get; set; }
        [DataMember]
        public string TermPagoGlosa { get; set; }
        [DataMember]
        public string TermPagoDias { get; set; }
        [DataMember]
        public string FchVenc { get; set; }

        public void Dispose()
        {
          //  throw new NotImplementedException();
        }
    }
    [DataContract]
    public class DTE_Emisor {
        [DataMember]
        public string RUTEmisor { get; set; }
        [DataMember]
        public string RznSoc { get; set; }
        [DataMember]
        public string GiroEmis { get; set; }
        [DataMember]
        public string Telefono { get; set; }
        [DataMember]
        public string CorreoEmisor { get; set; }
        [DataMember]
        public string Acteco { get; set; }
        [DataMember]
        public string CdgTraslado { get; set; }
        [DataMember]
        public string FolioAut { get; set; }
        [DataMember]
        public string FchAut { get; set; }
        [DataMember]
        public string Sucursal { get; set; }
        [DataMember]
        public string CdgSIISucur { get; set; }
        [DataMember]
        public string DirOrigen { get; set; }
        [DataMember]
        public string CmnaOrigen { get; set; }
        [DataMember]
        public string CiudadOrigen { get; set; }
        [DataMember]
        public string CdgVendedor { get; set; }
        [DataMember]
        public string IdAdicEmisor { get; set; }
    }
    [DataContract]
    public class DTE_Receptor
    {
        [DataMember]
        public string RUTRecep { get; set; }
        [DataMember]
        public string CdgIntRecep { get; set; }
        [DataMember]
        public string RznSocRecep { get; set; }
        [DataMember]
        public string NumId { get; set; }
        [DataMember]
        public string Nacionalidad { get; set; }
        [DataMember]
        public string GiroRecep { get; set; }
        [DataMember]
        public string Contacto { get; set; }
        [DataMember]
        public string CorreoRecep { get; set; }
        [DataMember]
        public string DirRecep { get; set; }
        [DataMember]
        public string CmnaRecep { get; set; }
        [DataMember]
        public string CiudadRecep { get; set; }
        [DataMember]
        public string DirPostal { get; set; }
        [DataMember]
        public string CmnaPostal { get; set; }
        [DataMember]
        public string CiudadPostal { get; set; }
    }
    [DataContract]
    public class DTE_Transporte {

        [DataMember]
        public string Patente { get; set; }
        [DataMember]
        public string RUTTrans { get; set; }
        [DataMember]
        public string RUTChofer { get; set; }
        [DataMember]
        public string NombreChofer { get; set; }
        [DataMember]
        public string DirDest { get; set; }
        [DataMember]
        public string CmnaDest { get; set; }
        [DataMember]
        public string CiudadDest { get; set; }
        //<aduana>
        public string CodModVenta { get; set; }
        [DataMember]
        public string CodClauVenta { get; set; }
        [DataMember]
        public string TotClauVenta { get; set; }
        [DataMember]
        public string CodViaTransp { get; set; }
        [DataMember]
        public string NombreTransp { get; set; }
        [DataMember]
        public string RUTCiaTransp { get; set; }
        [DataMember]
        public string NomCiaTransp { get; set; }
        [DataMember]
        public string IdAdicTransp { get; set; }
        [DataMember]
        public string Booking { get; set; }
        [DataMember]
        public string Operador { get; set; }
        [DataMember]
        public string CodPtoEmbarque { get; set; }
        [DataMember]
        public string IdAdicPtoEmb { get; set; }
        [DataMember]
        public string CodPtoDesemb { get; set; }
        [DataMember]
        public string IdAdicPtoDesemb { get; set; }
        [DataMember]
        public string Tara { get; set; }
        [DataMember]
        public string CodUnidMedTara { get; set; }
        [DataMember]
        public string PesoBruto { get; set; }
        [DataMember]
        public string CodUnidPesoBruto { get; set; }
        [DataMember]
        public string PesoNeto { get; set; }
        [DataMember]
        public string CodUnidPesoNeto { get; set; }
        [DataMember]
        public string TotItems { get; set; }
        [DataMember]
        public string TotBultos { get; set; }
        [DataMember]
        public string CodTpoBultos { get; set; }
        [DataMember]
        public string CantBultos { get; set; }
        [DataMember]
        public string Marcas { get; set; }
        [DataMember]
        public string IdContainer { get; set; }
        [DataMember]
        public string Sello { get; set; }
        [DataMember]
        public string EmisorSello { get; set; }
        [DataMember]
        public string MntFlete { get; set; }
        [DataMember]
        public string MntSeguro { get; set; }
        [DataMember]
        public string CodPaisRecep { get; set; }
        [DataMember]
        public string CodPaisDestin { get; set; }
    }

    [DataContract]
    public class DTE_Totales {
        [DataMember]
        public string MntNeto { get; set; }
        [DataMember]
        public string MntExe { get; set; }
        [DataMember]
        public string MntBase { get; set; }
        [DataMember]
        public string MntMargenCom { get; set; }
        [DataMember]
        public string TasaIVA { get; set; }
        [DataMember]
        public string IVA { get; set; }
        [DataMember]
        public string IVAProp { get; set; }
        [DataMember]
        public string IVATerc { get; set; }
        [DataMember]
        public string TipoImp { get; set; }
        [DataMember]
        public string TasaImp { get; set; }
        [DataMember]
        public string MontoImp { get; set; }
        [DataMember]
        public string IVANoRet { get; set; }
        [DataMember]
        public string CredEC { get; set; }
        [DataMember]
        public string GrntDep { get; set; }
        [DataMember]
        public string _ValComNeto { get; set; }
        [DataMember]
        public string _ValComExe { get; set; }
        [DataMember]
        public string _ValComIVA { get; set; }
        [DataMember]
        public string MntTotal { get; set; }
        [DataMember]
        public string MontoNF { get; set; }
        [DataMember]
        public string MontoPeriodo { get; set; }
        [DataMember]
        public string SaldoAnterior { get; set; }
        [DataMember]
        public string VlrPagar { get; set; }
    }

    [DataContract]
    public class DTE_DETALLE {
        [DataMember]
        public string NroLinDet { get; set; }
        [DataMember]
        public string TpoCodigo { get; set; }
        [DataMember]
        public string VlrCodigo { get; set; }
        [DataMember]
        public string IndExe { get; set; }
        [DataMember]
        public string IndAgente { get; set; }
        [DataMember]
        public string MntBaseFaena { get; set; }
        [DataMember]
        public string MntMargComer { get; set; }
        [DataMember]
        public string PrcConsFinal { get; set; }
        [DataMember]
        public string NmbItem { get; set; }
        [DataMember]
        public string DscItem { get; set; }
        [DataMember]
        public string QtyRef { get; set; }
        [DataMember]
        public string UnmdRef { get; set; }
        [DataMember]
        public string PrcRef { get; set; }
        [DataMember]
        public string QtyItem { get; set; }
        [DataMember]
        public string SubQty { get; set; }
        [DataMember]
        public string SubCod { get; set; }
        [DataMember]
        public string FchElabor { get; set; }
        [DataMember]
        public string FchVencim { get; set; }
        [DataMember]
        public string UnmdItem { get; set; }
        [DataMember]
        public string PrcItem { get; set; }
        [DataMember]
        public string PrcOtrMon { get; set; }
        [DataMember]
        public string Moneda { get; set; }
        [DataMember]
        public string FctConv { get; set; }
        [DataMember]
        public string DctoOtrMnda { get; set; }
        [DataMember]
        public string RecargoOtrMnda { get; set; }
        [DataMember]
        public string MontoItemOtrMnda { get; set; }
        [DataMember]
        public string DescuentoPct { get; set; }
        [DataMember]
        public string DescuentoMonto { get; set; }
        [DataMember]
        public string TipoDscto { get; set; }
        [DataMember]
        public string ValorDscto { get; set; }
        [DataMember]
        public string RecargoPct { get; set; }
        [DataMember]
        public string RecargoMonto { get; set; }
        [DataMember]
        public string TipoRecargo { get; set; }
        [DataMember]
        public string ValorRecargo { get; set; }
        [DataMember]
        public string CodImpAdic { get; set; }
        [DataMember]
        public string MontoItem { get; set; }

        public DTE_DETALLE(string NroLinDet, string TpoCodigo, string VlrCodigo, string IndExe, string IndAgente,
        string MntBaseFaena, string MntMargComer, string PrcConsFinal, string NmbItem, string DscItem, string QtyRef, string UnmdRef, string PrcRef,
        string QtyItem, string SubQty, string SubCod, string FchElabor, string FchVencim, string UnmdItem, string PrcItem, string PrcOtrMon, string Moneda,
        string FctConv, string DctoOtrMnda, string RecargoOtrMnda, string MontoItemOtrMnda, string DescuentoPct, string DescuentoMonto, string TipoDscto,
        string ValorDscto, string RecargoPct, string RecargoMonto, string TipoRecargo, string ValorRecargo, string CodImpAdic, string MontoItem)
        {
            this.NroLinDet = NroLinDet; this.TpoCodigo = TpoCodigo; this.VlrCodigo = VlrCodigo; this.IndExe = IndExe; this.IndAgente = IndAgente;
            this.MntBaseFaena = MntBaseFaena; this.MntMargComer = MntMargComer; this.PrcConsFinal = PrcConsFinal; this.NmbItem = NmbItem;
            this.DscItem = DscItem; this.QtyRef = QtyRef; this.UnmdRef = UnmdRef; this.PrcRef = PrcRef; this.QtyItem = QtyItem;
            this.SubQty = SubQty; this.SubCod = SubCod; this.FchElabor = FchElabor; this.FchVencim = FchVencim; this.UnmdItem = UnmdItem;
            this.PrcItem = PrcItem; this.PrcOtrMon = PrcOtrMon; this.Moneda = Moneda; this.FctConv = FctConv; this.DctoOtrMnda = DctoOtrMnda;
            this.RecargoOtrMnda = RecargoOtrMnda; this.MontoItemOtrMnda = MontoItemOtrMnda; this.DescuentoPct = DescuentoPct;
            this.DescuentoMonto = DescuentoMonto; this.TipoDscto = TipoDscto;
            this.ValorDscto = ValorDscto; this.RecargoPct = RecargoPct; this.RecargoMonto = RecargoMonto; this.TipoRecargo = TipoRecargo;
            this.ValorRecargo = ValorRecargo; this.CodImpAdic = CodImpAdic; this.MontoItem = MontoItem;
        }

        public DTE_DETALLE()
        {
        }
    }
    [DataContract]
    public class DTE_SubTotInfo {
        [DataMember]
        public  string NroSTI { get; set; }
        [DataMember]
        public  string GlosaSTI { get; set; }
        [DataMember]
        public  string OrdenSTI { get; set; }
        [DataMember]
        public string SubTotNetoSTI { get; set; }
        [DataMember]
        public string SubTotIVASTI { get; set; }
        [DataMember]
        public string SubTotAdicSTI { get; set; }
        [DataMember]
        public string SubTotExeSTI { get; set; }
        [DataMember]
        public string ValSubtotSTI { get; set; }
        [DataMember]
        public string LineasDeta { get; set; }

        public DTE_SubTotInfo(string NroSTI , string GlosaSTI ,
        string OrdenSTI , string SubTotNetoSTI , string SubTotIVASTI ,
        string SubTotAdicSTI , string SubTotExeSTI , string ValSubtotSTI ,
        string LineasDeta ) {

            this.NroSTI = NroSTI;
            this.GlosaSTI = GlosaSTI;
            this.OrdenSTI = OrdenSTI;
            this.SubTotNetoSTI = SubTotNetoSTI;
            this.SubTotIVASTI = SubTotIVASTI;
            this.SubTotAdicSTI = SubTotAdicSTI;
            this.SubTotExeSTI = SubTotExeSTI;
            this.ValSubtotSTI = ValSubtotSTI;
            this.LineasDeta = LineasDeta;
        }
        public DTE_SubTotInfo() { }
    }
    [DataContract]
    public class DTE_DscrGlobal {
        [DataMember]
        public  string NroLinDR { get; set; }
        [DataMember]
        public string TpoMov { get; set; }
        [DataMember]
        public string GlosaDR { get; set; }
        [DataMember]
        public string TpoValor { get; set; }
        [DataMember]
        public string ValorDR { get; set; }
        [DataMember]
        public string ValorDROtrMnda { get; set; }
        [DataMember]
        public string IndExeDR { get; set; }

        public DTE_DscrGlobal(string NroLinDR, string TpoMov, string GlosaDR,
            string TpoValor , string ValorDR , string ValorDROtrMnda ,string IndExeDR
            )
        {
            this.NroLinDR = NroLinDR;
            this.TpoMov = TpoMov;
            this.GlosaDR = GlosaDR;
            this.TpoValor = TpoValor;
            this.ValorDR = ValorDR;
            this.ValorDROtrMnda = ValorDROtrMnda;
            this.IndExeDR = IndExeDR;

        }
        public DTE_DscrGlobal() { }
    }
    [DataContract]
    public class DTE_referencia {
        [DataMember]
        public string NroLinRef { get; set; }
        [DataMember]
        public string TpoDocRef { get; set; }
        [DataMember]
        public string IndGlobal { get; set; }
        [DataMember]
        public string FolioRef { get; set; }
        [DataMember]
        public string RUTOtr { get; set; }
        [DataMember]
        public string FchRef { get; set; }
        [DataMember]
        public string CodRef { get; set; }
        [DataMember]
        public string RazonRef { get; set; }

        public DTE_referencia(string NroLinRef ,
        string TpoDocRef ,string IndGlobal , string FolioRef , string RUTOtr ,
        string FchRef ,  string CodRef , string RazonRef ) {

            this.NroLinRef = NroLinRef;
            this.TpoDocRef = TpoDocRef;
            this.IndGlobal = IndGlobal;
            this.FolioRef = FolioRef;
            this.RUTOtr = RUTOtr;
            this.FchRef = FchRef;
            this.CodRef = CodRef;
            this.RazonRef = RazonRef;

        }
        public DTE_referencia() { }
    }
    [DataContract]
    public class DTE_comisiones {
        [DataMember]
        public string NroLinCom { get; set; }
        [DataMember]
        public  string TipoMovim { get; set; }
        [DataMember]
        public string Glosa { get; set; }
        [DataMember]
        public string TasaComision { get; set; }
        [DataMember]
        public  string ValComNeto { get; set; }
        [DataMember]
        public  string ValComExe { get; set; }
        [DataMember]
        public  string ValComIVA { get; set; }

        public DTE_comisiones(string NroLinCom ,  string TipoMovim , string Glosa ,
        string TasaComision ,string ValComNeto , string ValComExe ,  string ValComIVA ) {

            this.NroLinCom = NroLinCom;
            this.TipoMovim = TipoMovim;
            this.Glosa = Glosa;
            this.TasaComision = TasaComision;
            this.ValComNeto = ValComNeto;
            this.ValComExe = ValComExe;
            this.ValComIVA = ValComIVA;
        }

        public DTE_comisiones() { }
    }




}