using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using static DTE_Maker.DTE;

namespace DTE_Maker
{
    public class Maker
    {
        /*private XDocument dteComplete = new XDocument();
        private XElement setDte;
        private XElement dte;*/
        private string SetDTE = string.Empty;
        private string DTE = string.Empty;


        public Maker GeneraPreview()
        {
            Maker m = new Maker();
            return m;
        }

        public Maker FirmarDocumento()
        {
            Maker m = new Maker();
            return m;
        }

       //metodos primitivos v1 heredados de otra version
        public Maker GenerarSetDTE(SetDTE setdte)
        {
            Maker m = new Maker();
            string RutEmisor = setdte.RutEmisor;
            string RutEnvia = setdte.RutEnvia;
            string receptor = setdte.receptor;
            string FchResol = setdte.FchResol;
            string NroResol = setdte.NroResol;
            string TmstFirmmaEnv = setdte.TmstFirmmaEnv;
            string TpoDTE = setdte.TpoDTE;
            string NroDTE = setdte.NroDTE;

            string result = string.Empty;

            using (StringWriter str = new StringWriter())
            using (XmlTextWriter doc = new XmlTextWriter(str))
            {
                doc.Formatting = Formatting.Indented;
                doc.Indentation = 0;
                doc.WriteStartDocument();
                doc.WriteStartElement("EnvioDTE");
                doc.WriteStartAttribute("xmlns");
                doc.WriteValue("http://www.sii.cl/SiiDte");
                doc.WriteEndAttribute();

                doc.WriteStartAttribute("xmlns:xsi");
                doc.WriteValue("http://www.w3.org/2001/XMLSchema-instance");
                doc.WriteEndAttribute();

                doc.WriteStartAttribute("xsi:schemaLocation");
                doc.WriteValue("http://www.sii.cl/SiiDte EnvioDTE_v10.xsd");
                doc.WriteEndAttribute();

                doc.WriteStartAttribute("version");
                doc.WriteValue("1.0");
                doc.WriteEndAttribute();
                //Fin Cabecera
                doc.WriteStartElement("SetDTE");
                doc.WriteStartAttribute("ID");
                doc.WriteValue("SetDoc");
                doc.WriteEndAttribute();

                doc.WriteStartElement("Caratula");
                doc.WriteStartAttribute("version");
                doc.WriteValue("1.0");
                doc.WriteEndAttribute();

                doc.WriteStartElement("RutEmisor");
                doc.WriteValue(RutEmisor);
                doc.WriteEndElement();

                doc.WriteStartElement("RutEnvia");
                doc.WriteValue(RutEnvia);
                doc.WriteEndElement();

                doc.WriteStartElement("RutReceptor");
                doc.WriteValue(receptor);
                doc.WriteEndElement();

                doc.WriteStartElement("FchResol");
                doc.WriteValue(FchResol);
                doc.WriteEndElement();

                doc.WriteStartElement("NroResol");
                doc.WriteValue(NroResol);
                doc.WriteEndElement();

                doc.WriteStartElement("TmstFirmaEnv");
                doc.WriteValue(TmstFirmmaEnv);
                doc.WriteEndElement();

                doc.WriteStartElement("SubTotDTE");

                doc.WriteStartElement("TpoDTE");
                doc.WriteValue(TpoDTE);
                doc.WriteEndElement();

                doc.WriteStartElement("NroDTE");
                doc.WriteValue(NroDTE);
                doc.WriteEndElement();
                doc.WriteEndElement();
                doc.WriteEndElement();

                doc.WriteEndDocument();
                // fin documento
                doc.Flush();
                doc.Close();
                result = str.ToString();
                result = result.Replace("utf-16", "ISO-8859-1");



            }

            m.SetDTE = result;
            return m;
          //  return Maker;
        }

        //metodos primitivos v1 heredados de otra version
        public Maker GenerarDTE(
                    DTE_idDoc dte_idDoc,
                    DTE_Emisor dte_emis,
                    DTE_Receptor dte_recep,
                    DTE_Transporte trans,
                    DTE_Totales tot,
                    List<DTE_Detalle> det,
                    List<DTE_SubTotInfo> sub,
                    List<DTE_DscrGlobal> dscrcg,
                    List<DTE_referencia> refer,
                    List<DTE_comisiones> comi
                    )
        {
            Maker m = new Maker();
            //<iddoc>
            string ID = dte_idDoc.ID;
            string TipoDTE = dte_idDoc.TipoDTE;
            string Folio = dte_idDoc.Folio;
            string FchEmis = dte_idDoc.FchEmis;
            string IndNoRebaja = dte_idDoc.IndNoRebaja;
            string TipoDespacho = dte_idDoc.TipoDespacho;
            string IndTraslado = dte_idDoc.IndTraslado;
            string TpoImpresion = dte_idDoc.TpoImpresion;
            string IndServicio = dte_idDoc.IndServicio;
            string MntBruto = dte_idDoc.MntBruto;
            string FmaPago = dte_idDoc.FmaPago;
            string FmaPagExp = dte_idDoc.FmaPagExp;
            string FchCancel = dte_idDoc.FchCancel;
            string MntCancel = dte_idDoc.MntCancel;
            string SaldoInsol = dte_idDoc.SaldoInsol;
            string FchPago = dte_idDoc.FchPago;
            string MntPago = dte_idDoc.MntPago;
            string GlosaPagos = dte_idDoc.GlosaPagos;
            string PeriodoDesde = dte_idDoc.PeriodoDesde;
            string PeriodoHasta = dte_idDoc.PeriodoHasta;
            string MedioPago = dte_idDoc.MedioPago;
            string TpoCtaPago = dte_idDoc.TpoCtaPago;
            string NumCtaPago = dte_idDoc.NumCtaPago;
            string BcoPago = dte_idDoc.BcoPago;
            string TermPagoCdg = dte_idDoc.TermPagoCdg;
            string TermPagoGlosa = dte_idDoc.TermPagoGlosa;
            string TermPagoDias = dte_idDoc.TermPagoDias;
            string FchVenc = dte_idDoc.FchVenc;
            //</iddoc>
            //<emisor>
            string RUTEmisor = dte_emis.RUTEmisor;
            string RznSoc = dte_emis.RznSoc;
            string GiroEmis = dte_emis.GiroEmis;
            string Telefono = dte_emis.Telefono;
            string CorreoEmisor = dte_emis.CorreoEmisor;
            string Acteco = dte_emis.Acteco;
            string CdgTraslado = dte_emis.CdgTraslado;
            string FolioAut = dte_emis.FolioAut;
            string FchAut = dte_emis.FchAut;
            string Sucursal = dte_emis.Sucursal;
            string CdgSIISucur = dte_emis.CdgSIISucur;
            string DirOrigen = dte_emis.DirOrigen;
            string CmnaOrigen = dte_emis.CmnaOrigen;
            string CiudadOrigen = dte_emis.CiudadOrigen;
            string CdgVendedor = dte_emis.CdgVendedor;
            string IdAdicEmisor = dte_emis.IdAdicEmisor;
            //</emisor>
            string RUTMandante = string.Empty;
            //<receptor>
            string RUTRecep = dte_recep.RUTRecep;
            string CdgIntRecep = dte_recep.CdgIntRecep;
            string RznSocRecep = dte_recep.RznSocRecep;
            string NumId = dte_recep.NumId;
            string Nacionalidad = dte_recep.Nacionalidad;
            string GiroRecep = dte_recep.GiroRecep;
            string Contacto = dte_recep.Contacto;
            string CorreoRecep = dte_recep.CorreoRecep;
            string DirRecep = dte_recep.DirRecep;
            string CmnaRecep = dte_recep.CmnaRecep;
            string CiudadRecep = dte_recep.CiudadRecep;
            string DirPostal = dte_recep.DirPostal;
            string CmnaPostal = dte_recep.CmnaPostal;
            string CiudadPostal = dte_recep.CiudadPostal;
            //<receptor>
            string RUTSolicita = string.Empty;
            //<transporte>
            string Patente = trans.Patente;
            string RUTTrans = trans.RUTTrans;
            string RUTChofer = trans.RUTChofer;
            string NombreChofer = trans.NombreChofer;
            string DirDest = trans.DirDest;
            string CmnaDest = trans.CmnaDest;
            string CiudadDest = trans.CiudadDest;
            //<aduana>
            string CodModVenta = trans.CodModVenta;
            string CodClauVenta = trans.CodClauVenta;
            string TotClauVenta = trans.TotClauVenta;
            string CodViaTransp = trans.CodViaTransp;
            string NombreTransp = trans.NombreTransp;
            string RUTCiaTransp = trans.RUTCiaTransp;
            string NomCiaTransp = trans.NomCiaTransp;
            string IdAdicTransp = trans.IdAdicTransp;
            string Booking = trans.Booking;
            string Operador = trans.Operador;
            string CodPtoEmbarque = trans.CodPtoEmbarque;
            string IdAdicPtoEmb = trans.IdAdicPtoEmb;
            string CodPtoDesemb = trans.CodPtoDesemb;
            string IdAdicPtoDesemb = trans.IdAdicPtoDesemb;
            string Tara = trans.Tara;
            string CodUnidMedTara = trans.CodUnidMedTara;
            string PesoBruto = trans.PesoBruto;
            string CodUnidPesoBruto = trans.CodUnidPesoBruto;
            string PesoNeto = trans.PesoNeto;
            string CodUnidPesoNeto = trans.CodUnidPesoNeto;
            string TotItems = trans.TotItems;
            string TotBultos = trans.TotBultos;
            string CodTpoBultos = trans.CodTpoBultos;
            string CantBultos = trans.CantBultos;
            string Marcas = trans.Marcas;
            string IdContainer = trans.IdContainer;
            string Sello = trans.Sello;
            string EmisorSello = trans.EmisorSello;
            string MntFlete = trans.MntFlete;
            string MntSeguro = trans.MntSeguro;
            string CodPaisRecep = trans.CodPaisRecep;
            string CodPaisDestin = trans.CodPaisDestin;
            //</aduana>
            //</transporte>

            //<totales>
            string MntNeto = tot.MntNeto;
            string MntExe = tot.MntExe;
            string MntBase = tot.MntBase;
            string MntMargenCom = tot.MntMargenCom;
            string TasaIVA = tot.TasaIVA;
            string IVA = tot.IVA;
            string IVAProp = tot.IVAProp;
            string IVATerc = tot.IVATerc;
            string TipoImp = tot.TipoImp;
            string TasaImp = tot.TasaImp;
            string MontoImp = tot.MontoImp;
            string IVANoRet = tot.IVANoRet;
            string CredEC = tot.CredEC;
            string GrntDep = tot.GrntDep;
            string _ValComNeto = tot._ValComNeto;
            string _ValComExe = tot._ValComExe;
            string _ValComIVA = tot._ValComIVA;
            string MntTotal = tot.MntTotal;
            string MontoNF = tot.MontoNF;
            string MontoPeriodo = tot.MontoPeriodo;
            string SaldoAnterior = tot.SaldoAnterior;
            string VlrPagar = tot.VlrPagar;
            //</totales>

            //<detalle>
            string NroLinDet = string.Empty;
            string TpoCodigo = string.Empty;
            string VlrCodigo = string.Empty;
            string IndExe = string.Empty;
            string IndAgente = string.Empty;
            string MntBaseFaena = string.Empty;
            string MntMargComer = string.Empty;
            string PrcConsFinal = string.Empty;
            string NmbItem = string.Empty;
            string DscItem = string.Empty;
            string QtyRef = string.Empty;
            string UnmdRef = string.Empty;
            string PrcRef = string.Empty;
            string QtyItem = string.Empty;
            string SubQty = string.Empty;
            string SubCod = string.Empty;
            string FchElabor = string.Empty;
            string FchVencim = string.Empty;
            string UnmdItem = string.Empty;
            string PrcItem = string.Empty;
            string PrcOtrMon = string.Empty;
            string Moneda = string.Empty;
            string FctConv = string.Empty;
            string DctoOtrMnda = string.Empty;
            string RecargoOtrMnda = string.Empty;
            string MontoItemOtrMnda = string.Empty;
            string DescuentoPct = string.Empty;
            string DescuentoMonto = string.Empty;
            string TipoDscto = string.Empty;
            string ValorDscto = string.Empty;
            string RecargoPct = string.Empty;
            string RecargoMonto = string.Empty;
            string TipoRecargo = string.Empty;
            string ValorRecargo = string.Empty;
            string CodImpAdic = string.Empty;
            string MontoItem = string.Empty;
            // </detalle>
            //<subtotinfo>
            string NroSTI = string.Empty;
            string GlosaSTI = string.Empty;
            string OrdenSTI = string.Empty;
            string SubTotNetoSTI = string.Empty;
            string SubTotIVASTI = string.Empty;
            string SubTotAdicSTI = string.Empty;
            string SubTotExeSTI = string.Empty;
            string ValSubtotSTI = string.Empty;
            string LineasDeta = string.Empty;
            //</subtotinfo>
            //<dscrcgglobal>
            string NroLinDR = string.Empty;
            string TpoMov = string.Empty;
            string GlosaDR = string.Empty;
            string TpoValor = string.Empty;
            string ValorDR = string.Empty;
            string ValorDROtrMnda = string.Empty;
            string IndExeDR = string.Empty;
            //</dscrcgglobal>
            //<referencia>
            string NroLinRef = string.Empty;
            string TpoDocRef = string.Empty;
            string IndGlobal = string.Empty;
            string FolioRef = string.Empty;
            string RUTOtr = string.Empty;
            string FchRef = string.Empty;
            string CodRef = string.Empty;
            string RazonRef = string.Empty;
            //</referencia>
            //<comiciones>
            string NroLinCom = string.Empty;
            string TipoMovim = string.Empty;
            string Glosa = string.Empty;
            string TasaComision = string.Empty;
            string ValComNeto = string.Empty;
            string ValComExe = string.Empty;
            string ValComIVA = string.Empty;
            //</comiciones>



            string result = string.Empty;

            using (StringWriter str = new StringWriter())
            using (XmlTextWriter doc = new XmlTextWriter(str))
            {
                //XmlTextWriter doc = new XmlTextWriter(ruta, Encoding.GetEncoding("iso-8859-1"));
                doc.Formatting = Formatting.Indented;
                doc.Indentation = 0;
                doc.WriteStartDocument();

                doc.WriteStartElement("DTE");
                doc.WriteStartAttribute("version");
                doc.WriteValue("1.0");
                doc.WriteEndAttribute();

                doc.WriteStartElement("Documento");

                doc.WriteStartAttribute("ID");
                doc.WriteValue(ID);
                doc.WriteEndAttribute();

                //encabezado
                #region
                doc.WriteStartElement("Encabezado");
                doc.WriteStartElement("IdDoc");

                doc.WriteStartElement("TipoDTE");
                doc.WriteValue(TipoDTE);
                doc.WriteEndElement();

                doc.WriteStartElement("Folio");
                doc.WriteValue(Folio);
                doc.WriteEndElement();

                doc.WriteStartElement("FchEmis");
                doc.WriteValue(FchEmis);
                doc.WriteEndElement();

                doc.WriteStartElement("IndNoRebaja");
                doc.WriteValue(IndNoRebaja);
                doc.WriteEndElement();

                doc.WriteStartElement("TipoDespacho");
                doc.WriteValue(TipoDespacho);
                doc.WriteEndElement();

                doc.WriteStartElement("IndTraslado");
                doc.WriteValue(IndTraslado);
                doc.WriteEndElement();

                doc.WriteStartElement("TpoImpresion");
                doc.WriteValue(TpoImpresion);
                doc.WriteEndElement();

                doc.WriteStartElement("IndServicio");
                doc.WriteValue(IndServicio);
                doc.WriteEndElement();

                doc.WriteStartElement("MntBruto");
                doc.WriteValue(MntBruto);
                doc.WriteEndElement();

                doc.WriteStartElement("FmaPago");
                doc.WriteValue(FmaPago);
                doc.WriteEndElement();

                doc.WriteStartElement("FmaPagExp");
                doc.WriteValue(FmaPagExp);
                doc.WriteEndElement();

                doc.WriteStartElement("FchCancel");
                doc.WriteValue(FchCancel);
                doc.WriteEndElement();

                doc.WriteStartElement("MntCancel");
                doc.WriteValue(MntCancel);
                doc.WriteEndElement();

                doc.WriteStartElement("SaldoInsol");
                doc.WriteValue(SaldoInsol);
                doc.WriteEndElement();

                doc.WriteStartElement("MntPagos");

                doc.WriteStartElement("FchPago");
                doc.WriteValue(FchPago);
                doc.WriteEndElement();

                doc.WriteStartElement("MntPago");
                doc.WriteValue(MntPago);
                doc.WriteEndElement();

                doc.WriteStartElement("GlosaPagos");
                doc.WriteValue(GlosaPagos);
                doc.WriteEndElement();

                doc.WriteEndElement();
                //fin mntpagos

                doc.WriteStartElement("PeriodoDesde");
                doc.WriteValue(PeriodoDesde);
                doc.WriteEndElement();

                doc.WriteStartElement("PeriodoHasta");
                doc.WriteValue(PeriodoHasta);
                doc.WriteEndElement();

                doc.WriteStartElement("MedioPago");
                doc.WriteValue(MedioPago);
                doc.WriteEndElement();

                doc.WriteStartElement("TpoCtaPago");
                doc.WriteValue(TpoCtaPago);
                doc.WriteEndElement();

                doc.WriteStartElement("NumCtaPago");
                doc.WriteValue(NumCtaPago);
                doc.WriteEndElement();

                doc.WriteStartElement("BcoPago");
                doc.WriteValue(BcoPago);
                doc.WriteEndElement();

                doc.WriteStartElement("TermPagoCdg");
                doc.WriteValue(TermPagoCdg);
                doc.WriteEndElement();

                doc.WriteStartElement("TermPagoGlosa");
                doc.WriteValue(TermPagoGlosa);
                doc.WriteEndElement();

                doc.WriteStartElement("TermPagoDias");
                doc.WriteValue(TermPagoDias);
                doc.WriteEndElement();

                doc.WriteStartElement("FchVenc");
                doc.WriteValue(FchVenc);
                doc.WriteEndElement();

                doc.WriteEndElement();
                //fin Id Doc
                doc.WriteStartElement("Emisor");

                doc.WriteStartElement("RUTEmisor");
                doc.WriteValue(RUTEmisor);
                doc.WriteEndElement();

                doc.WriteStartElement("RznSoc");
                doc.WriteValue(RznSoc);
                doc.WriteEndElement();

                doc.WriteStartElement("GiroEmis");
                doc.WriteValue(GiroEmis);
                doc.WriteEndElement();

                doc.WriteStartElement("Telefono");
                doc.WriteValue(Telefono);
                doc.WriteEndElement();

                doc.WriteStartElement("CorreoEmisor");
                doc.WriteValue(CorreoEmisor);
                doc.WriteEndElement();

                doc.WriteStartElement("Acteco");
                doc.WriteValue(Acteco);
                doc.WriteEndElement();

                doc.WriteStartElement("GuiaExport");

                doc.WriteStartElement("CdgTraslado");
                doc.WriteValue(CdgTraslado);
                doc.WriteEndElement();

                doc.WriteStartElement("FolioAut");
                doc.WriteValue(FolioAut);
                doc.WriteEndElement();

                doc.WriteStartElement("FchAut");
                doc.WriteValue(FchAut);
                doc.WriteEndElement();

                doc.WriteEndElement();
                //fin guiaexport

                doc.WriteStartElement("Sucursal");
                doc.WriteValue(Sucursal);
                doc.WriteEndElement();

                doc.WriteStartElement("CdgSIISucur");
                doc.WriteValue(CdgSIISucur);
                doc.WriteEndElement();

                doc.WriteStartElement("DirOrigen");
                doc.WriteValue(DirOrigen);
                doc.WriteEndElement();

                doc.WriteStartElement("CmnaOrigen");
                doc.WriteValue(CmnaOrigen);
                doc.WriteEndElement();

                doc.WriteStartElement("CiudadOrigen");
                doc.WriteValue(CiudadOrigen);
                doc.WriteEndElement();

                doc.WriteStartElement("CdgVendedor");
                doc.WriteValue(CdgVendedor);
                doc.WriteEndElement();

                doc.WriteStartElement("IdAdicEmisor");
                doc.WriteValue(IdAdicEmisor);
                doc.WriteEndElement();

                doc.WriteEndElement();
                //fin de Emisor
                doc.WriteStartElement("RUTMandante");
                doc.WriteValue(RUTMandante);
                doc.WriteEndElement();
                //empieza receptor
                doc.WriteStartElement("Receptor");

                doc.WriteStartElement("RUTRecep");
                doc.WriteValue(RUTRecep);
                doc.WriteEndElement();

                doc.WriteStartElement("CdgIntRecep");
                doc.WriteValue(CdgIntRecep);
                doc.WriteEndElement();

                doc.WriteStartElement("RznSocRecep");
                doc.WriteValue(RznSocRecep);
                doc.WriteEndElement();

                doc.WriteStartElement("Extranjero");

                doc.WriteStartElement("NumId");
                doc.WriteValue(NumId);
                doc.WriteEndElement();

                doc.WriteStartElement("Nacionalidad");
                doc.WriteValue(Nacionalidad);
                doc.WriteEndElement();

                doc.WriteEndElement();
                //fin extranjero

                doc.WriteStartElement("GiroRecep");
                doc.WriteValue(GiroRecep);
                doc.WriteEndElement();

                doc.WriteStartElement("Contacto");
                doc.WriteValue(Contacto);
                doc.WriteEndElement();

                doc.WriteStartElement("CorreoRecep");
                doc.WriteValue(CorreoRecep);
                doc.WriteEndElement();

                doc.WriteStartElement("DirRecep");
                doc.WriteValue(DirRecep);
                doc.WriteEndElement();

                doc.WriteStartElement("CmnaRecep");
                doc.WriteValue(CmnaRecep);
                doc.WriteEndElement();

                doc.WriteStartElement("CiudadRecep");
                doc.WriteValue(CiudadRecep);
                doc.WriteEndElement();

                doc.WriteStartElement("DirPostal");
                doc.WriteValue(DirPostal);
                doc.WriteEndElement();

                doc.WriteStartElement("CmnaPostal");
                doc.WriteValue(CmnaPostal);
                doc.WriteEndElement();

                doc.WriteStartElement("CiudadPostal");
                doc.WriteValue(CiudadPostal);
                doc.WriteEndElement();

                doc.WriteEndElement();
                //fin de Receptor

                doc.WriteStartElement("RUTSolicita");
                doc.WriteValue(RUTSolicita);
                doc.WriteEndElement();

                doc.WriteStartElement("Transporte");

                doc.WriteStartElement("Patente");
                doc.WriteValue(Patente);
                doc.WriteEndElement();

                doc.WriteStartElement("RUTTrans");
                doc.WriteValue(RUTTrans);
                doc.WriteEndElement();

                doc.WriteStartElement("Chofer");

                doc.WriteStartElement("RUTChofer");
                doc.WriteValue(RUTChofer);
                doc.WriteEndElement();

                doc.WriteStartElement("NombreChofer");
                doc.WriteValue(NombreChofer);
                doc.WriteEndElement();

                doc.WriteEndElement();
                //fin chofer

                doc.WriteStartElement("DirDest");
                doc.WriteValue(DirDest);
                doc.WriteEndElement();

                doc.WriteStartElement("CmnaDest");
                doc.WriteValue(CmnaDest);
                doc.WriteEndElement();

                doc.WriteStartElement("CiudadDest");
                doc.WriteValue(CiudadDest);
                doc.WriteEndElement();

                doc.WriteStartElement("Aduana");

                doc.WriteStartElement("CodModVenta");
                doc.WriteValue(CodModVenta);
                doc.WriteEndElement();

                doc.WriteStartElement("CodClauVenta");
                doc.WriteValue(CodClauVenta);
                doc.WriteEndElement();

                doc.WriteStartElement("TotClauVenta");
                doc.WriteValue(TotClauVenta);
                doc.WriteEndElement();

                doc.WriteStartElement("CodViaTransp");
                doc.WriteValue(CodViaTransp);
                doc.WriteEndElement();

                doc.WriteStartElement("NombreTransp");
                doc.WriteValue(NombreTransp);
                doc.WriteEndElement();

                doc.WriteStartElement("RUTCiaTransp");
                doc.WriteValue(RUTCiaTransp);
                doc.WriteEndElement();

                doc.WriteStartElement("NomCiaTransp");
                doc.WriteValue(NomCiaTransp);
                doc.WriteEndElement();

                doc.WriteStartElement("IdAdicTransp");
                doc.WriteValue(IdAdicTransp);
                doc.WriteEndElement();

                doc.WriteStartElement("Booking");
                doc.WriteValue(Booking);
                doc.WriteEndElement();

                doc.WriteStartElement("Operador");
                doc.WriteValue(Operador);
                doc.WriteEndElement();

                doc.WriteStartElement("CodPtoEmbarque");
                doc.WriteValue(CodPtoEmbarque);
                doc.WriteEndElement();

                doc.WriteStartElement("IdAdicPtoEmb");
                doc.WriteValue(IdAdicPtoEmb);
                doc.WriteEndElement();

                doc.WriteStartElement("CodPtoDesemb");
                doc.WriteValue(CodPtoDesemb);
                doc.WriteEndElement();

                doc.WriteStartElement("IdAdicPtoDesemb");
                doc.WriteValue(IdAdicPtoDesemb);
                doc.WriteEndElement();

                doc.WriteStartElement("Tara");
                doc.WriteValue(Tara);
                doc.WriteEndElement();

                doc.WriteStartElement("CodUnidMedTara");
                doc.WriteValue(CodUnidMedTara);
                doc.WriteEndElement();

                doc.WriteStartElement("PesoBruto");
                doc.WriteValue(PesoBruto);
                doc.WriteEndElement();

                doc.WriteStartElement("CodUnidPesoBruto");
                doc.WriteValue(CodUnidPesoBruto);
                doc.WriteEndElement();

                doc.WriteStartElement("PesoNeto");
                doc.WriteValue(PesoNeto);
                doc.WriteEndElement();

                doc.WriteStartElement("CodUnidPesoNeto");
                doc.WriteValue(CodUnidPesoNeto);
                doc.WriteEndElement();

                doc.WriteStartElement("TotItems");
                doc.WriteValue(TotItems);
                doc.WriteEndElement();

                doc.WriteStartElement("TotBultos");
                doc.WriteValue(TotBultos);
                doc.WriteEndElement();

                doc.WriteStartElement("TipoBultos");

                doc.WriteStartElement("CodTpoBultos");
                doc.WriteValue(CodTpoBultos);
                doc.WriteEndElement();

                doc.WriteStartElement("CantBultos");
                doc.WriteValue(CantBultos);
                doc.WriteEndElement();

                doc.WriteStartElement("Marcas");
                doc.WriteValue(Marcas);
                doc.WriteEndElement();

                doc.WriteStartElement("IdContainer");
                doc.WriteValue(IdContainer);
                doc.WriteEndElement();

                doc.WriteStartElement("Sello");
                doc.WriteValue(Sello);
                doc.WriteEndElement();

                doc.WriteStartElement("EmisorSello");
                doc.WriteValue(EmisorSello);
                doc.WriteEndElement();

                doc.WriteEndElement();
                //fin tipo bultos

                doc.WriteStartElement("MntFlete");
                doc.WriteValue(MntFlete);
                doc.WriteEndElement();

                doc.WriteStartElement("MntSeguro");
                doc.WriteValue(MntSeguro);
                doc.WriteEndElement();

                doc.WriteStartElement("CodPaisRecep");
                doc.WriteValue(CodPaisRecep);
                doc.WriteEndElement();

                doc.WriteStartElement("CodPaisDestin");
                doc.WriteValue(CodPaisDestin);
                doc.WriteEndElement();

                doc.WriteEndElement();
                //fin aduana

                doc.WriteEndElement();
                //FIN TRANSPORTE

                //empieza totales
                #region
                doc.WriteStartElement("Totales");


                doc.WriteStartElement("MntNeto");
                doc.WriteValue(MntNeto);
                doc.WriteEndElement();

                doc.WriteStartElement("MntExe");
                doc.WriteValue(MntExe);
                doc.WriteEndElement();

                doc.WriteStartElement("MntBase");
                doc.WriteValue(MntBase);
                doc.WriteEndElement();

                doc.WriteStartElement("MntMargenCom");
                doc.WriteValue(MntMargenCom);
                doc.WriteEndElement();

                doc.WriteStartElement("TasaIVA");
                doc.WriteValue(TasaIVA);
                doc.WriteEndElement();

                doc.WriteStartElement("IVA");
                doc.WriteValue(IVA);
                doc.WriteEndElement();

                doc.WriteStartElement("IVAProp");
                doc.WriteValue(IVAProp);
                doc.WriteEndElement();

                doc.WriteStartElement("IVATerc");
                doc.WriteValue(IVATerc);
                doc.WriteEndElement();


                doc.WriteStartElement("ImptoReten");

                doc.WriteStartElement("TipoImp");
                doc.WriteValue(TipoImp);
                doc.WriteEndElement();

                doc.WriteStartElement("TasaImp");
                doc.WriteValue(TasaImp);
                doc.WriteEndElement();

                doc.WriteStartElement("MontoImp");
                doc.WriteValue(MontoImp);
                doc.WriteEndElement();

                doc.WriteEndElement();
                //fin imptoreten

                doc.WriteStartElement("IVANoRet");
                doc.WriteValue(IVANoRet);
                doc.WriteEndElement();

                doc.WriteStartElement("CredEC");
                doc.WriteValue(CredEC);
                doc.WriteEndElement();

                doc.WriteStartElement("GrntDep");
                doc.WriteValue(GrntDep);
                doc.WriteEndElement();

                doc.WriteStartElement("Comisiones");

                doc.WriteStartElement("ValComNeto");
                doc.WriteValue(_ValComNeto);
                doc.WriteEndElement();

                doc.WriteStartElement("ValComExe");
                doc.WriteValue(_ValComExe);
                doc.WriteEndElement();

                doc.WriteStartElement("ValComIVA");
                doc.WriteValue(_ValComIVA);
                doc.WriteEndElement();

                doc.WriteEndElement();
                //fin comiciones

                doc.WriteStartElement("MntTotal");
                doc.WriteValue(MntTotal);
                doc.WriteEndElement();

                doc.WriteStartElement("MontoNF");
                doc.WriteValue(MontoNF);
                doc.WriteEndElement();

                doc.WriteStartElement("MontoPeriodo");
                doc.WriteValue(MontoPeriodo);
                doc.WriteEndElement();

                doc.WriteStartElement("SaldoAnterior");
                doc.WriteValue(SaldoAnterior);
                doc.WriteEndElement();

                doc.WriteStartElement("VlrPagar");
                doc.WriteValue(VlrPagar);
                doc.WriteEndElement();

                doc.WriteEndElement();
                #endregion
                //fin de Totales

                doc.WriteEndElement();

                #endregion
                //fin de Encabezado

                //EMPIESA DETALLE
                #region

                int i = 1;
                foreach (DTE_Detalle _det in det)
                {
                    i++;
                    NroLinDet = (Int32.Parse(_det.NroLinDet) + 1).ToString();
                    TpoCodigo = _det.TpoCodigo.ToString();
                    VlrCodigo = _det.VlrCodigo.ToString();
                    IndExe = _det.IndExe.ToString();
                    IndAgente = _det.IndAgente.ToString();
                    MntBaseFaena = _det.MntBaseFaena.ToString();
                    MntMargComer = _det.MntMargComer.ToString();
                    PrcConsFinal = _det.PrcConsFinal.ToString();
                    NmbItem = _det.NmbItem.ToString();
                    DscItem = _det.DscItem.ToString();
                    QtyRef = _det.QtyRef.ToString();
                    UnmdRef = _det.UnmdRef.ToString();
                    PrcRef = _det.PrcRef.ToString();
                    QtyItem = _det.QtyItem.ToString();
                    SubQty = _det.SubQty.ToString();
                    SubCod = _det.SubCod.ToString();
                    FchElabor = _det.FchElabor.ToString();
                    FchVencim = _det.FchVencim.ToString();
                    UnmdItem = _det.UnmdItem.ToString();
                    PrcItem = _det.PrcItem.ToString();
                    PrcOtrMon = _det.PrcOtrMon.ToString();
                    Moneda = _det.Moneda.ToString();
                    FctConv = _det.FctConv.ToString();
                    DctoOtrMnda = _det.DctoOtrMnda.ToString();
                    RecargoOtrMnda = _det.RecargoOtrMnda.ToString();
                    MontoItemOtrMnda = _det.MontoItemOtrMnda.ToString();
                    DescuentoPct = _det.DescuentoPct.ToString();
                    DescuentoMonto = _det.DescuentoMonto.ToString();
                    TipoDscto = _det.TipoDscto.ToString();
                    ValorDscto = _det.ValorDscto.ToString();
                    RecargoPct = _det.RecargoPct.ToString();
                    RecargoMonto = _det.RecargoMonto.ToString();
                    TipoRecargo = _det.TipoRecargo.ToString();
                    ValorRecargo = _det.ValorRecargo.ToString();
                    CodImpAdic = _det.CodImpAdic.ToString();
                    MontoItem = _det.MontoItem.ToString();


                    doc.WriteStartElement("Detalle");

                    doc.WriteStartElement("NroLinDet");
                    doc.WriteValue(NroLinDet);
                    doc.WriteEndElement();

                    doc.WriteStartElement("CdgItem");

                    doc.WriteStartElement("TpoCodigo");
                    doc.WriteValue(TpoCodigo);
                    doc.WriteEndElement();

                    doc.WriteStartElement("VlrCodigo");
                    doc.WriteValue(VlrCodigo);
                    doc.WriteEndElement();

                    doc.WriteEndElement();
                    //fin cdgITEM

                    doc.WriteStartElement("IndExe");
                    doc.WriteValue(IndExe);
                    doc.WriteEndElement();

                    doc.WriteStartElement("Retenedor");

                    doc.WriteStartElement("IndAgente");
                    doc.WriteValue(IndAgente);
                    doc.WriteEndElement();

                    doc.WriteStartElement("MntBaseFaena");
                    doc.WriteValue(MntBaseFaena);
                    doc.WriteEndElement();

                    doc.WriteStartElement("MntMargComer");
                    doc.WriteValue(MntMargComer);
                    doc.WriteEndElement();

                    doc.WriteStartElement("PrcConsFinal");
                    doc.WriteValue(PrcConsFinal);
                    doc.WriteEndElement();

                    doc.WriteEndElement();
                    //fin retenedor

                    doc.WriteStartElement("NmbItem");
                    doc.WriteValue(NmbItem);
                    doc.WriteEndElement();

                    doc.WriteStartElement("DscItem");
                    doc.WriteValue(DscItem);
                    doc.WriteEndElement();

                    doc.WriteStartElement("QtyRef");
                    doc.WriteValue(QtyRef);
                    doc.WriteEndElement();

                    doc.WriteStartElement("UnmdRef");
                    doc.WriteValue(UnmdRef);
                    doc.WriteEndElement();

                    doc.WriteStartElement("PrcRef");
                    doc.WriteValue(PrcRef);
                    doc.WriteEndElement();

                    doc.WriteStartElement("QtyItem");
                    doc.WriteValue(QtyItem);
                    doc.WriteEndElement();

                    doc.WriteStartElement("Subcantidad");

                    doc.WriteStartElement("SubQty");
                    doc.WriteValue(SubQty);
                    doc.WriteEndElement();

                    doc.WriteStartElement("SubCod");
                    doc.WriteValue(SubCod);
                    doc.WriteEndElement();

                    doc.WriteEndElement();
                    //fin subcantidad

                    doc.WriteStartElement("FchElabor");
                    doc.WriteValue(FchElabor);
                    doc.WriteEndElement();

                    doc.WriteStartElement("FchVencim");
                    doc.WriteValue(FchVencim);
                    doc.WriteEndElement();

                    doc.WriteStartElement("UnmdItem");
                    doc.WriteValue(UnmdItem);
                    doc.WriteEndElement();

                    doc.WriteStartElement("PrcItem");
                    doc.WriteValue(PrcItem);
                    doc.WriteEndElement();

                    doc.WriteStartElement("OtrMnda");

                    doc.WriteStartElement("PrcOtrMon");
                    doc.WriteValue(PrcOtrMon);
                    doc.WriteEndElement();

                    doc.WriteStartElement("Moneda");
                    doc.WriteValue(Moneda);
                    doc.WriteEndElement();

                    doc.WriteStartElement("FctConv");
                    doc.WriteValue(FctConv);
                    doc.WriteEndElement();

                    doc.WriteStartElement("DctoOtrMnda");
                    doc.WriteValue(DctoOtrMnda);
                    doc.WriteEndElement();

                    doc.WriteStartElement("RecargoOtrMnda");
                    doc.WriteValue(RecargoOtrMnda);
                    doc.WriteEndElement();

                    doc.WriteStartElement("MontoItemOtrMnda");
                    doc.WriteValue(MontoItemOtrMnda);
                    doc.WriteEndElement();

                    doc.WriteEndElement();
                    //fin otrmnda

                    doc.WriteStartElement("DescuentoPct");
                    doc.WriteValue(DescuentoPct);
                    doc.WriteEndElement();

                    doc.WriteStartElement("DescuentoMonto");
                    doc.WriteValue(DescuentoMonto);
                    doc.WriteEndElement();

                    doc.WriteStartElement("SubDscto");

                    doc.WriteStartElement("TipoDscto");
                    doc.WriteValue(TipoDscto);
                    doc.WriteEndElement();

                    doc.WriteStartElement("ValorDscto");
                    doc.WriteValue(ValorDscto);
                    doc.WriteEndElement();

                    doc.WriteEndElement();
                    //fin subdscto

                    doc.WriteStartElement("RecargoPct");
                    doc.WriteValue(RecargoPct);
                    doc.WriteEndElement();

                    doc.WriteStartElement("RecargoMonto");
                    doc.WriteValue(RecargoMonto);
                    doc.WriteEndElement();

                    doc.WriteStartElement("SubRecargo");

                    doc.WriteStartElement("TipoRecargo");
                    doc.WriteValue(TipoRecargo);
                    doc.WriteEndElement();

                    doc.WriteStartElement("ValorRecargo");
                    doc.WriteValue(ValorRecargo);
                    doc.WriteEndElement();

                    doc.WriteEndElement();
                    //fin subrecargo

                    doc.WriteStartElement("CodImpAdic");
                    doc.WriteValue(CodImpAdic);
                    doc.WriteEndElement();

                    doc.WriteStartElement("MontoItem");
                    doc.WriteValue(MontoItem);
                    doc.WriteEndElement();
                    doc.WriteEndElement();
                }

                #endregion
                //fin Detalle

                //subtotinfo
                #region
                int j = 1;
                foreach (DTE_SubTotInfo subtot in sub)
                {
                    j++;
                    NroSTI = (Int32.Parse(subtot.NroSTI) + 1).ToString();
                    GlosaSTI = subtot.GlosaSTI.ToString();
                    OrdenSTI = subtot.OrdenSTI.ToString();
                    SubTotNetoSTI = subtot.SubTotNetoSTI.ToString();
                    SubTotIVASTI = subtot.SubTotIVASTI.ToString();
                    SubTotAdicSTI = subtot.SubTotAdicSTI.ToString();
                    SubTotExeSTI = subtot.SubTotExeSTI.ToString();
                    ValSubtotSTI = subtot.ValSubtotSTI.ToString();
                    LineasDeta = subtot.LineasDeta.ToString();

                    doc.WriteStartElement("SubTotInfo");

                    doc.WriteStartElement("NroSTI");
                    doc.WriteValue(NroSTI);
                    doc.WriteEndElement();

                    doc.WriteStartElement("GlosaSTI");
                    doc.WriteValue(GlosaSTI);
                    doc.WriteEndElement();

                    doc.WriteStartElement("OrdenSTI");
                    doc.WriteValue(OrdenSTI);
                    doc.WriteEndElement();

                    doc.WriteStartElement("SubTotNetoSTI");
                    doc.WriteValue(SubTotNetoSTI);
                    doc.WriteEndElement();

                    doc.WriteStartElement("SubTotIVASTI");
                    doc.WriteValue(SubTotIVASTI);
                    doc.WriteEndElement();

                    doc.WriteStartElement("SubTotAdicSTI");
                    doc.WriteValue(SubTotAdicSTI);
                    doc.WriteEndElement();

                    doc.WriteStartElement("SubTotExeSTI");
                    doc.WriteValue(SubTotExeSTI);
                    doc.WriteEndElement();

                    doc.WriteStartElement("ValSubtotSTI");
                    doc.WriteValue(ValSubtotSTI);
                    doc.WriteEndElement();

                    doc.WriteStartElement("LineasDeta");
                    doc.WriteValue(LineasDeta);
                    doc.WriteEndElement();

                    doc.WriteEndElement();
                }
                #endregion
                //fin subtotInfo


                // DSCRCGGLOBAL
                #region
                foreach (DTE_DscrGlobal _dscrcg in dscrcg)
                {
                    NroLinDR = Int32.Parse((_dscrcg.NroLinDR) + 1).ToString();
                    TpoMov = _dscrcg.TpoMov.ToString();
                    GlosaDR = _dscrcg.GlosaDR.ToString();
                    TpoValor = _dscrcg.TpoValor.ToString();
                    ValorDR = _dscrcg.ValorDR.ToString();
                    ValorDROtrMnda = _dscrcg.ValorDROtrMnda.ToString();
                    IndExeDR = _dscrcg.IndExeDR.ToString();

                    doc.WriteStartElement("DscRcgGlobal");

                    doc.WriteStartElement("NroLinDR");
                    doc.WriteValue(NroLinDR);
                    doc.WriteEndElement();

                    doc.WriteStartElement("TpoMov");
                    doc.WriteValue(TpoMov);
                    doc.WriteEndElement();

                    doc.WriteStartElement("GlosaDR");
                    doc.WriteValue(GlosaDR);
                    doc.WriteEndElement();

                    doc.WriteStartElement("TpoValor");
                    doc.WriteValue(TpoValor);
                    doc.WriteEndElement();

                    doc.WriteStartElement("ValorDR");
                    doc.WriteValue(ValorDR);
                    doc.WriteEndElement();

                    doc.WriteStartElement("ValorDROtrMnda");
                    doc.WriteValue(ValorDROtrMnda);
                    doc.WriteEndElement();

                    doc.WriteStartElement("IndExeDR");
                    doc.WriteValue(IndExeDR);
                    doc.WriteEndElement();

                    doc.WriteEndElement();
                }
                #endregion
                //FIN DSCRCGGLOBAL

                //referencia
                #region

                foreach (DTE_referencia _refer in refer)
                {
                    NroLinRef = Int32.Parse((_refer.NroLinRef) + 1).ToString();
                    TpoDocRef = _refer.TpoDocRef.ToString();
                    IndGlobal = _refer.IndGlobal.ToString();
                    FolioRef = _refer.FolioRef.ToString();
                    RUTOtr = _refer.RUTOtr.ToString();
                    FchRef = _refer.FchRef.ToString();
                    CodRef = _refer.CodRef.ToString();
                    RazonRef = _refer.RazonRef.ToString();

                    doc.WriteStartElement("Referencia");

                    doc.WriteStartElement("NroLinRef");
                    doc.WriteValue(NroLinRef);
                    doc.WriteEndElement();

                    doc.WriteStartElement("TpoDocRef");
                    doc.WriteValue(TpoDocRef);
                    doc.WriteEndElement();

                    doc.WriteStartElement("IndGlobal");
                    doc.WriteValue(IndGlobal);
                    doc.WriteEndElement();

                    doc.WriteStartElement("FolioRef");
                    doc.WriteValue(FolioRef);
                    doc.WriteEndElement();

                    doc.WriteStartElement("RUTOtr");
                    doc.WriteValue(RUTOtr);
                    doc.WriteEndElement();

                    doc.WriteStartElement("FchRef");
                    doc.WriteValue(FchRef);
                    doc.WriteEndElement();

                    doc.WriteStartElement("CodRef");
                    doc.WriteValue(CodRef);
                    doc.WriteEndElement();

                    doc.WriteStartElement("RazonRef");
                    doc.WriteValue(RazonRef);
                    doc.WriteEndElement();

                    doc.WriteEndElement();
                }
                #endregion
                //FIN REFERENCIA

                //comisiones
                #region

                foreach (DTE_comisiones _comi in comi)
                {
                    NroLinCom = Int32.Parse((_comi.NroLinCom) + 1).ToString();
                    TipoMovim = _comi.TipoMovim.ToString();
                    Glosa = _comi.Glosa.ToString();
                    TasaComision = _comi.TasaComision.ToString();
                    ValComNeto = _comi.ValComNeto.ToString();
                    ValComExe = _comi.ValComExe.ToString();
                    ValComIVA = _comi.ValComIVA.ToString();

                    doc.WriteStartElement("Comisiones");

                    doc.WriteStartElement("NroLinCom");
                    doc.WriteValue(NroLinCom);
                    doc.WriteEndElement();

                    doc.WriteStartElement("TipoMovim");
                    doc.WriteValue(TipoMovim);
                    doc.WriteEndElement();

                    doc.WriteStartElement("Glosa");
                    doc.WriteValue(Glosa);
                    doc.WriteEndElement();

                    doc.WriteStartElement("TasaComision");
                    doc.WriteValue(TasaComision);
                    doc.WriteEndElement();

                    doc.WriteStartElement("ValComNeto");
                    doc.WriteValue(ValComNeto);
                    doc.WriteEndElement();

                    doc.WriteStartElement("ValComExe");
                    doc.WriteValue(ValComExe);
                    doc.WriteEndElement();

                    doc.WriteStartElement("ValComIVA");
                    doc.WriteValue(ValComIVA);
                    doc.WriteEndElement();

                    doc.WriteEndElement();
                }
                #endregion
                //fin Comisiones
                /*
                doc.WriteStartElement("TED");
                doc.WriteStartAttribute("version");
                doc.WriteValue("1.0");
                doc.WriteEndAttribute();
                // Lectura de datos desde Folios Autorizados
                //------------------------------------------
                clases.Metodos m = new clases.Metodos();
                string Path = get_caf(rut_user, int.Parse(TipoDTE));
                string RE = m.obtieneLecturaXML(Path, "RE");
                string RS = m.obtieneLecturaXML(Path, "RS");
                string TD = m.obtieneLecturaXML(Path, "TD");
                string D = m.obtieneLecturaXML(Path, "D");
                string H = m.obtieneLecturaXML(Path, "H");
                string FA = m.obtieneLecturaXML(Path, "FA");
                string M = m.obtieneLecturaXML(Path, "M");
                string E = m.obtieneLecturaXML(Path, "E");
                string IDK = m.obtieneLecturaXML(Path, "IDK");
                ///////////lectura de frma desde el caf
                DataSet ds = new DataSet();
                string CAF = Path;
                ds.ReadXml((XmlReader.Create(new StringReader(CAF))));
                string FRMA = ds.Tables["FRMA"].Rows[0][1].ToString();
                string FRMT = m.obtieneLecturaXML(Path, string.Format("FRMT algoritmo={0}SHA1withRSA{0} ", (char)34));
                string RSASK = m.obtienePrivateKeyFactura(Path);
                string RSAPUBK = m.obtienePublicKeyFactura(Path);
                doc.WriteStartElement("DD");

                doc.WriteStartElement("RE");
                doc.WriteValue(RE);
                doc.WriteEndElement();

                doc.WriteStartElement("TD");
                doc.WriteValue(TD);
                doc.WriteEndElement();

                doc.WriteStartElement("F");
                doc.WriteValue(Folio);
                doc.WriteEndElement();

                doc.WriteStartElement("FE");
                doc.WriteValue(FchEmis);
                doc.WriteEndElement();

                doc.WriteStartElement("RR");
                doc.WriteValue(RUTRecep);
                doc.WriteEndElement();

                doc.WriteStartElement("RSR");
                doc.WriteValue(RznSocRecep);
                doc.WriteEndElement();

                doc.WriteStartElement("MNT");
                doc.WriteValue(MntTotal);
                doc.WriteEndElement();
                //  nombrePrimerItem

                doc.WriteStartElement("IT1");
                doc.WriteValue(NmbItem);
                doc.WriteEndElement();
                //Fin de datos precargados
                doc.WriteStartElement("CAF");
                doc.WriteStartAttribute("version");
                doc.WriteValue("1.0");
                doc.WriteEndAttribute();

                doc.WriteStartElement("DA");
                doc.WriteStartElement("RE");
                doc.WriteValue(RUTEmisor);
                doc.WriteEndElement();

                doc.WriteStartElement("RS");
                doc.WriteValue(RS);
                doc.WriteEndElement();

                doc.WriteStartElement("TD");
                doc.WriteValue(TD);
                doc.WriteEndElement();

                doc.WriteStartElement("RNG");
                doc.WriteStartElement("D");
                doc.WriteValue(D);
                doc.WriteEndElement();

                doc.WriteStartElement("H");
                doc.WriteValue(H);
                doc.WriteEndElement();
                doc.WriteEndElement();
                //fin RNG
                doc.WriteStartElement("FA");
                doc.WriteValue(FA);
                doc.WriteEndElement();

                doc.WriteStartElement("RSAPK");
                doc.WriteStartElement("M");
                doc.WriteValue(M);
                doc.WriteEndElement();

                doc.WriteStartElement("E");
                doc.WriteValue(E);
                doc.WriteEndElement();
                doc.WriteEndElement();
                //fin RSAPK
                doc.WriteStartElement("IDK");
                doc.WriteValue(IDK);
                doc.WriteEndElement();
                doc.WriteEndElement();
                //fin DA
                doc.WriteStartElement("FRMA");
                doc.WriteStartAttribute("algoritmo");
                doc.WriteValue("SHA1withRSA");
                doc.WriteEndAttribute();
                doc.WriteValue(FRMA);
                doc.WriteEndElement();
                doc.WriteEndElement();
                //fin CAF
                string TSTED = DateTime.Now.ToString("yyy/MM/dd" + "T" + "HH:mm:ss").Replace('/', '-');
                doc.WriteStartElement("TSTED");
                doc.WriteValue(TSTED);
                doc.WriteEndElement();

                doc.WriteEndElement();
                //fin Documento
                ///////////////////////GENERAR FRMT
                string AUXFRMT = "<DD><RE>" + RE + "</RE><TD>" + TD + "</TD><F>" + Folio + "</F><FE>" +
                FchEmis + "</FE><RR>" + RUTRecep + "</RR><RSR>" + RznSocRecep + "</RSR><MNT>" + MntTotal + "</MNT><IT1>" + NmbItem + "</IT1><CAF version=" + "\"1.0\string.Empty + "><DA><RE>" +
                RE + "</RE><RS>" + RS + "</RS><TD>" + TD + "</TD><RNG><D>" + D + "</D><H>" + H + "</H></RNG><FA>" + FA + "</FA><RSAPK><M>" + M + "</M><E>" +
                E + "</E></RSAPK><IDK>" + IDK + "</IDK></DA><FRMA algoritmo=" + "\"SHA1withRSA\string.Empty + ">" + FRMA + "</FRMA></CAF><TSTED>" + TSTED + "</TSTED></DD>";
                string PP = ds.Tables["AUTORIZACION"].Rows[0][1].ToString();
                m.PruebaTimbreDD(AUXFRMT, PP);
                string CONVERSIONFRMT = Hash_String_SHA1(AUXFRMT);
                /////////////////////////////////
                doc.WriteStartElement("FRMT");
                doc.WriteStartAttribute("algoritmo");
                doc.WriteValue("SHA1withRSA");
                doc.WriteEndAttribute();
                doc.WriteValue(m.PruebaTimbreDD(AUXFRMT, PP));
                doc.WriteEndElement();
                doc.WriteEndElement();
                string TmstFirma = DateTime.Now.ToString("yyy/MM/dd" + "T" + "HH:mm:ss").Replace('/', '-');
                doc.WriteStartElement("TmstFirma");
                doc.WriteValue(TmstFirma);*/
                doc.WriteEndElement();
                doc.WriteEndDocument();
                // fin documentO
                doc.Flush();
                doc.Close();

                result = str.ToString();
                result = result.Replace("utf-16", "ISO-8859-1");
            }
            m.DTE = result;
            return m;
        }

        private string HashStringSHA1(string strDatos)
        {
            string functionReturnValue = string.Empty;
            byte[] Datos = new byte[(strDatos.Length) + 1];
            byte[] Result = null;
            SHA1CryptoServiceProvider sha = new SHA1CryptoServiceProvider();
            Result = sha.ComputeHash(Datos);
            functionReturnValue = Convert.ToBase64String(Result);
            return functionReturnValue;
        }
    }
}
