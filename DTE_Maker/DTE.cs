using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DTE_Maker
{
    [XmlRoot("DTE"), Serializable]
    public class DTE
    {
     
    }

    public class Documento
    {

    }


    /*
        public class DTE_idDoc : IDisposable
        {         
            public string ID { get; set; }
            
            public string TipoDTE { get; set; }
            
            public string Folio { get; set; }
            
            public string FchEmis { get; set; }
            
            public string IndNoRebaja { get; set; }
            
            public string TipoDespacho { get; set; }
            
            public string IndTraslado { get; set; }
            
            public string TpoImpresion { get; set; }
            
            public string IndServicio { get; set; }
            
            public string MntBruto { get; set; }
            
            public string FmaPago { get; set; }
            
            public string FmaPagExp { get; set; }
            
            public string FchCancel { get; set; }
            
            public string MntCancel { get; set; }
            
            public string SaldoInsol { get; set; }
            
            public string FchPago { get; set; }
            
            public string MntPago { get; set; }
            
            public string GlosaPagos { get; set; }
            
            public string PeriodoDesde { get; set; }
            
            public string PeriodoHasta { get; set; }
            
            public string MedioPago { get; set; }
            
            public string TpoCtaPago { get; set; }
            
            public string NumCtaPago { get; set; }
            
            public string BcoPago { get; set; }
            
            public string TermPagoCdg { get; set; }
            
            public string TermPagoGlosa { get; set; }
            
            public string TermPagoDias { get; set; }
            
            public string FchVenc { get; set; }

            public void Dispose()
            {
                //  throw new NotImplementedException();
            }
        }

        public class DTE_Emisor
        {
            
            public string RUTEmisor { get; set; }
            
            public string RznSoc { get; set; }
            
            public string GiroEmis { get; set; }
            
            public string Telefono { get; set; }
            
            public string CorreoEmisor { get; set; }
            
            public string Acteco { get; set; }
            
            public string CdgTraslado { get; set; }
            
            public string FolioAut { get; set; }
            
            public string FchAut { get; set; }
            
            public string Sucursal { get; set; }
            
            public string CdgSIISucur { get; set; }
            
            public string DirOrigen { get; set; }
            
            public string CmnaOrigen { get; set; }
            
            public string CiudadOrigen { get; set; }
            
            public string CdgVendedor { get; set; }
            
            public string IdAdicEmisor { get; set; }
        }

        public class DTE_Receptor
        {
            
            public string RUTRecep { get; set; }
            
            public string CdgIntRecep { get; set; }
            
            public string RznSocRecep { get; set; }
            
            public string NumId { get; set; }
            
            public string Nacionalidad { get; set; }
            
            public string GiroRecep { get; set; }
            
            public string Contacto { get; set; }
            
            public string CorreoRecep { get; set; }
            
            public string DirRecep { get; set; }
            
            public string CmnaRecep { get; set; }
            
            public string CiudadRecep { get; set; }
            
            public string DirPostal { get; set; }
            
            public string CmnaPostal { get; set; }
            
            public string CiudadPostal { get; set; }
        }

        public class DTE_Transporte
        {

            
            public string Patente { get; set; }
            
            public string RUTTrans { get; set; }
            
            public string RUTChofer { get; set; }
            
            public string NombreChofer { get; set; }
            
            public string DirDest { get; set; }
            
            public string CmnaDest { get; set; }
            
            public string CiudadDest { get; set; }
            //<aduana>
            public string CodModVenta { get; set; }
            
            public string CodClauVenta { get; set; }
            
            public string TotClauVenta { get; set; }
            
            public string CodViaTransp { get; set; }
            
            public string NombreTransp { get; set; }
            
            public string RUTCiaTransp { get; set; }
            
            public string NomCiaTransp { get; set; }
            
            public string IdAdicTransp { get; set; }
            
            public string Booking { get; set; }
            
            public string Operador { get; set; }
            
            public string CodPtoEmbarque { get; set; }
            
            public string IdAdicPtoEmb { get; set; }
            
            public string CodPtoDesemb { get; set; }
            
            public string IdAdicPtoDesemb { get; set; }
            
            public string Tara { get; set; }
            
            public string CodUnidMedTara { get; set; }
            
            public string PesoBruto { get; set; }
            
            public string CodUnidPesoBruto { get; set; }
            
            public string PesoNeto { get; set; }
            
            public string CodUnidPesoNeto { get; set; }
            
            public string TotItems { get; set; }
            
            public string TotBultos { get; set; }
            
            public string CodTpoBultos { get; set; }
            
            public string CantBultos { get; set; }
            
            public string Marcas { get; set; }
            
            public string IdContainer { get; set; }
            
            public string Sello { get; set; }
            
            public string EmisorSello { get; set; }
            
            public string MntFlete { get; set; }
            
            public string MntSeguro { get; set; }
            
            public string CodPaisRecep { get; set; }
            
            public string CodPaisDestin { get; set; }
        }

        public class DTE_Totales
        {
            
            public string MntNeto { get; set; }
            
            public string MntExe { get; set; }
            
            public string MntBase { get; set; }
            
            public string MntMargenCom { get; set; }
            
            public string TasaIVA { get; set; }
            
            public string IVA { get; set; }
            
            public string IVAProp { get; set; }
            
            public string IVATerc { get; set; }
            
            public string TipoImp { get; set; }
            
            public string TasaImp { get; set; }
            
            public string MontoImp { get; set; }
            
            public string IVANoRet { get; set; }
            
            public string CredEC { get; set; }
            
            public string GrntDep { get; set; }
            
            public string _ValComNeto { get; set; }
            
            public string _ValComExe { get; set; }
            
            public string _ValComIVA { get; set; }
            
            public string MntTotal { get; set; }
            
            public string MontoNF { get; set; }
            
            public string MontoPeriodo { get; set; }
            
            public string SaldoAnterior { get; set; }
            
            public string VlrPagar { get; set; }
        }

        public class DTE_Detalle
        {
            
            public string NroLinDet { get; set; }
            
            public string TpoCodigo { get; set; }
            
            public string VlrCodigo { get; set; }
            
            public string IndExe { get; set; }
            
            public string IndAgente { get; set; }
            
            public string MntBaseFaena { get; set; }
            
            public string MntMargComer { get; set; }
            
            public string PrcConsFinal { get; set; }
            
            public string NmbItem { get; set; }
            
            public string DscItem { get; set; }
            
            public string QtyRef { get; set; }
            
            public string UnmdRef { get; set; }
            
            public string PrcRef { get; set; }
            
            public string QtyItem { get; set; }
            
            public string SubQty { get; set; }
            
            public string SubCod { get; set; }
            
            public string FchElabor { get; set; }
            
            public string FchVencim { get; set; }
            
            public string UnmdItem { get; set; }
            
            public string PrcItem { get; set; }
            
            public string PrcOtrMon { get; set; }
            
            public string Moneda { get; set; }
            
            public string FctConv { get; set; }
            
            public string DctoOtrMnda { get; set; }
            
            public string RecargoOtrMnda { get; set; }
            
            public string MontoItemOtrMnda { get; set; }
            
            public string DescuentoPct { get; set; }
            
            public string DescuentoMonto { get; set; }
            
            public string TipoDscto { get; set; }
            
            public string ValorDscto { get; set; }
            
            public string RecargoPct { get; set; }
            
            public string RecargoMonto { get; set; }
            
            public string TipoRecargo { get; set; }
            
            public string ValorRecargo { get; set; }
            
            public string CodImpAdic { get; set; }
            
            public string MontoItem { get; set; }

            public DTE_Detalle(string NroLinDet, string TpoCodigo, string VlrCodigo, string IndExe, string IndAgente,
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

            public DTE_Detalle()
            {
            }
        }

        public class DTE_SubTotInfo
        {
            
            public string NroSTI { get; set; }
            
            public string GlosaSTI { get; set; }
            
            public string OrdenSTI { get; set; }
            
            public string SubTotNetoSTI { get; set; }
            
            public string SubTotIVASTI { get; set; }
            
            public string SubTotAdicSTI { get; set; }
            
            public string SubTotExeSTI { get; set; }
            
            public string ValSubtotSTI { get; set; }
            
            public string LineasDeta { get; set; }

            public DTE_SubTotInfo(string NroSTI, string GlosaSTI,
            string OrdenSTI, string SubTotNetoSTI, string SubTotIVASTI,
            string SubTotAdicSTI, string SubTotExeSTI, string ValSubtotSTI,
            string LineasDeta)
            {

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

        public class DTE_DscrGlobal
        {
            
            public string NroLinDR { get; set; }
            
            public string TpoMov { get; set; }
            
            public string GlosaDR { get; set; }
            
            public string TpoValor { get; set; }
            
            public string ValorDR { get; set; }
            
            public string ValorDROtrMnda { get; set; }
            
            public string IndExeDR { get; set; }

            public DTE_DscrGlobal(string NroLinDR, string TpoMov, string GlosaDR,
                string TpoValor, string ValorDR, string ValorDROtrMnda, string IndExeDR
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

        public class DTE_referencia
        {
            
            public string NroLinRef { get; set; }
            
            public string TpoDocRef { get; set; }
            
            public string IndGlobal { get; set; }
            
            public string FolioRef { get; set; }
            
            public string RUTOtr { get; set; }
            
            public string FchRef { get; set; }
            
            public string CodRef { get; set; }
            
            public string RazonRef { get; set; }

            public DTE_referencia(string NroLinRef,
            string TpoDocRef, string IndGlobal, string FolioRef, string RUTOtr,
            string FchRef, string CodRef, string RazonRef)
            {

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

        public class DTE_comisiones
        {
            
            public string NroLinCom { get; set; }
            
            public string TipoMovim { get; set; }
            
            public string Glosa { get; set; }
            
            public string TasaComision { get; set; }
            
            public string ValComNeto { get; set; }
            
            public string ValComExe { get; set; }
            
            public string ValComIVA { get; set; }

            public DTE_comisiones(string NroLinCom, string TipoMovim, string Glosa,
            string TasaComision, string ValComNeto, string ValComExe, string ValComIVA)
            {

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

     */
}
