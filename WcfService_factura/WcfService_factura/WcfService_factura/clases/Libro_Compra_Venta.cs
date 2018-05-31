using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WcfService_factura.clases
{
    public class Caratula:IDisposable
    {

        public string RutEmisorLibro { get; set; }
        public string RutEnvia { get; set; }
        public string PeriodoTributario { get; set; }
        public string FchResol { get; set; }
        public string NroResol { get; set; }
        public string TipoOperacion { get; set; }
        public string TipoLibro { get; set; }
        public string TipoEnvio { get; set; }
        public string NroSegmento { get; set; }
        public string FolioNotificacion { get; set; }
        public string CodAutRec { get; set; }

        public void Dispose()
        {
          //  throw new NotImplementedException();
        }
    }

    public class Totales_Periodo
    {

        public string TpoDoc { get; set; }
        public string TpoImp { get; set; }
        public string TotDoc { get; set; }
        public string TotAnulado { get; set; }
        public string TotOpExe { get; set; }
        public string TotMntExe { get; set; }
        public string TotMntNeto { get; set; }
        public string TotOpIVARec { get; set; }
        public string TotMntIVA { get; set; }
        public string TotOpActivoFijo { get; set; }
        public string TotMntActivoFijo { get; set; }
        public string TotMntIVAActivoFijo { get; set; }
        public string CodIVANoRec { get; set; }
        public string TotOpIVANoRec { get; set; }
        public string TotMntIVANoRec { get; set; }
        public string TotOpIVAUsoComun { get; set; }
        public string TotIVAUsoComun { get; set; }
        public string FctProp { get; set; }
        public string TotCredIVAUsoComun { get; set; }
        public string TotIVAFueraPlazo { get; set; }
        public string TotIVAPropio { get; set; }
        public string TotIVATerceros { get; set; }
        public string TotLey18211 { get; set; }
        public string CodImp { get; set; }
        public string TotMntImp { get; set; }
        public string TotImpSinCredito { get; set; }
        public string TotOpIVARetTotal { get; set; }
        public string TotIVARetTotal { get; set; }
        public string TotOpIVARetParcial { get; set; }
        public string TotIVARetParcial { get; set; }
        public string TotCredEC { get; set; }
        public string TotDepEnvase { get; set; }
        public string TotValComNeto { get; set; }
        public string TotValComExe { get; set; }
        public string TotValComIVA { get; set; }
        public string TotMntTotal { get; set; }
        public string TotOpIVANoRetenido { get; set; }
        public string TotIVANoRetenido { get; set; }
        public string TotMntNoFact { get; set; }
        public string TotMntPeriodo { get; set; }
        public string TotPsjNac { get; set; }
        public string TotPsjInt { get; set; }
        public string TotTabPuros { get; set; }
        public string TotTabCigarrillos { get; set; }
        public string TotTabElaborado { get; set; }
        public string TotImpVehiculo { get; set; }

        public Totales_Periodo(
        string TpoDoc, string TpoImp, string TotDoc, string TotAnulado, string TotOpExe, string TotMntExe, string TotMntNeto, string TotOpIVARec,
        string TotMntIVA, string TotOpActivoFijo, string TotMntActivoFijo, string TotMntIVAActivoFijo, string CodIVANoRec,
        string TotOpIVANoRec, string TotMntIVANoRec, string TotOpIVAUsoComun,
        string TotIVAUsoComun, string FctProp, string TotCredIVAUsoComun, string TotIVAFueraPlazo, string TotIVAPropio, string TotIVATerceros,
        string TotLey18211, string CodImp, string TotMntImp, string TotImpSinCredito, string TotOpIVARetTotal, string TotIVARetTotal, string TotOpIVARetParcial,
        string TotIVARetParcial, string TotCredEC, string TotDepEnvase, string TotValComNeto, string TotValComExe, string TotValComIVA, string TotMntTotal
            , string TotOpIVANoRetenido, string TotIVANoRetenido, string TotMntNoFact, string TotMntPeriodo,
        string TotPsjNac, string TotPsjInt, string TotTabPuros, string TotTabCigarrillos, string TotTabElaborado, string TotImpVehiculo
            )
        {
            this.TpoDoc = TpoDoc; this.TpoImp = TpoImp; this.TotDoc = TotDoc; this.TotAnulado = TotAnulado;
            this.TotOpExe = TotOpExe; this.TotMntExe = TotMntExe; this.TotMntNeto = TotMntNeto; this.TotOpIVARec = TotOpIVARec;
            this.TotMntIVA = TotMntIVA; this.TotOpActivoFijo = TotOpActivoFijo; this.TotMntActivoFijo = TotMntActivoFijo;
            this.TotMntIVAActivoFijo = TotMntIVAActivoFijo; this.CodIVANoRec = CodIVANoRec;
            this.TotOpIVANoRec = TotOpIVANoRec; this.TotMntIVANoRec = TotMntIVANoRec; this.TotOpIVAUsoComun = TotOpIVAUsoComun;
            this.TotIVAUsoComun = TotIVAUsoComun; this.FctProp = FctProp; this.TotCredIVAUsoComun = TotCredIVAUsoComun; this.TotIVAFueraPlazo = TotIVAFueraPlazo;
            this.TotIVAPropio = TotIVAPropio; this.TotIVATerceros = TotIVATerceros; this.TotLey18211 = TotLey18211; this.CodImp = CodImp; this.TotMntImp = TotMntImp;
            this.TotImpSinCredito = TotImpSinCredito; this.TotOpIVARetTotal = TotOpIVARetTotal; this.TotIVARetTotal = TotIVARetTotal; this.TotOpIVARetParcial = TotOpIVARetParcial;
            this.TotIVARetParcial = TotIVARetParcial; this.TotCredEC = TotCredEC; this.TotDepEnvase = TotDepEnvase; this.TotValComNeto = TotValComNeto; this.TotValComExe = TotValComExe;
            this.TotValComIVA = TotValComIVA; this.TotMntTotal = TotMntTotal; this.TotOpIVANoRetenido = TotOpIVANoRetenido; this.TotIVANoRetenido = TotIVANoRetenido;
            this.TotMntNoFact = TotMntNoFact; this.TotMntPeriodo = TotMntPeriodo; this.TotPsjNac = TotPsjNac; this.TotPsjInt = TotPsjInt; this.TotTabPuros = TotTabPuros;
            this.TotTabCigarrillos = TotTabCigarrillos; this.TotTabElaborado = TotTabElaborado; this.TotImpVehiculo = TotImpVehiculo;
        }
        public Totales_Periodo() { }
    }
    public class Detalle_l_v {
       public string TpoDoc_ { get; set; }
        public string Emisor { get; set; }
        public string IndFactCompra { get; set; }
        public string NroDoc { get; set; }
        public string Anulado { get; set; }
        public string Operacion { get; set; }
        public string TpoImp_ { get; set; }
        public string TasaImp_ { get; set; }
        public string NumInt { get; set; }
        public string IndServicio { get; set; }
        public string IndSinCosto { get; set; }
        public string FchDoc { get; set; }
        public string CdgSIISucur { get; set; }
        public string RUTDoc { get; set; }
        public string RznSoc { get; set; }
        public string NumId { get; set; }
        public string Nacionalidad { get; set; }
        public string TpoDocRef { get; set; }
        public string FolioDocRef { get; set; }
        public string MntExe { get; set; }
        public string MntNeto { get; set; }
        public string MntIVA { get; set; }
        public string MntActivoFijo { get; set; }
        public string MntIVAActivoFijo { get; set; }
        public string CodIVANoRec_ { get; set; }
        public string MntIVANoRec { get; set; }
        public string IVAUsoComun { get; set; }
        public string IVAFueraPlazo { get; set; }
        public string IVAPropio { get; set; }
        public string IVATerceros { get; set; }
        public string Ley18211 { get; set; }
        public string CodImp_ { get; set; }
        public string MntImp { get; set; }
        public string MntSinCred { get; set; }
        public string IVARetTotal { get; set; }
        public string IVARetParcial { get; set; }
        public string CredEC { get; set; }
        public string DepEnvase { get; set; }
        public string RutEmisor { get; set; }
        public string ValComNeto { get; set; }
        public string ValComExe { get; set; }
        public string ValComIVA { get; set; }
        public string MntTotal { get; set; }
        public string IVANoRetenido { get; set; }
        public string MntNoFact { get; set; }
        public string MntPeriodo { get; set; }
        public string PsjNac { get; set; }
        public string PsjInt { get; set; }
        public string TabPuros { get; set; }
        public string TabCigarrillos { get; set; }
        public string TabElaborado { get; set; }
        public string ImpVehiculo { get; set; }
        public string TmstFirma { get; set; }

        public Detalle_l_v(
            string TpoDoc_ , string Emisor , string IndFactCompra ,string NroDoc , string Anulado ,
        string Operacion ,string TpoImp_ , string TasaImp_ , string NumInt ,string IndServicio , string IndSinCosto ,
        string FchDoc , string CdgSIISucur ,string RUTDoc , string RznSoc , string NumId , string Nacionalidad ,
        string TpoDocRef ,string FolioDocRef , string MntExe ,string MntNeto , string MntIVA , string MntActivoFijo ,
        string MntIVAActivoFijo ,string CodIVANoRec_ , string MntIVANoRec , string IVAUsoComun , string IVAFueraPlazo ,string IVAPropio ,
        string IVATerceros ,string Ley18211 ,string CodImp_ , string MntImp ,string MntSinCred ,string IVARetTotal ,
        string IVARetParcial ,string CredEC ,string DepEnvase ,string RutEmisor ,string ValComNeto , string ValComExe ,
        string ValComIVA ,string MntTotal,string IVANoRetenido ,string MntNoFact ,string MntPeriodo ,string PsjNac ,
        string PsjInt,string TabPuros,string TabCigarrillos,string TabElaborado,string ImpVehiculo,string TmstFirma 
            ) {
            this.TpoDoc_ = TpoDoc_;this.Emisor = Emisor;this.IndFactCompra = IndFactCompra;this.NroDoc = NroDoc;this.Anulado = Anulado;
            this.Operacion = Operacion;this.TpoImp_ = TpoImp_;this.TasaImp_ = TasaImp_;this.NumInt = NumInt;this.IndServicio = IndServicio;
            this.IndSinCosto = IndSinCosto;this.FchDoc = FchDoc;this.CdgSIISucur = CdgSIISucur;this.RUTDoc = RUTDoc;this.RznSoc = RznSoc;
            this.NumId = NumId;this.Nacionalidad = Nacionalidad;this.TpoDocRef = TpoDocRef;this.FolioDocRef = FolioDocRef;this.MntExe = MntExe;
            this.MntNeto = MntNeto;this.MntIVA = MntIVA;this.MntActivoFijo = MntActivoFijo;this.MntIVAActivoFijo = MntIVAActivoFijo;this.CodIVANoRec_ = CodIVANoRec_;
            this.MntIVANoRec = MntIVANoRec;this.IVAUsoComun = IVAUsoComun;this.IVAFueraPlazo = IVAFueraPlazo;this.IVAPropio = IVAPropio;this.IVATerceros = IVATerceros;
            this.Ley18211 = Ley18211;this.CodImp_ = CodImp_;this.MntImp = MntImp;this.MntSinCred = MntSinCred;this.IVARetTotal = IVARetTotal;this.IVARetParcial = IVARetParcial;
            this.CredEC = CredEC;this.DepEnvase = DepEnvase;this.RutEmisor = RutEmisor;this.ValComNeto = ValComNeto;this.ValComExe = ValComExe;
            this.ValComIVA = ValComIVA;this.MntTotal = MntTotal;this.IVANoRetenido = IVANoRetenido;this.MntNoFact = MntNoFact;this.MntPeriodo = MntPeriodo;
            this.PsjNac = PsjNac;this.PsjInt = PsjInt;this.TabPuros = TabPuros;this.TabCigarrillos = TabCigarrillos;this.TabElaborado = TabElaborado;
            this.ImpVehiculo = ImpVehiculo;this.TmstFirma = TmstFirma;

        }
        public Detalle_l_v() { }

    }
}