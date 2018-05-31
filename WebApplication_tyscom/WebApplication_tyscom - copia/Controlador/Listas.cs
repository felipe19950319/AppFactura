using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

    public class Listas
    {
    
        public WebApplication_tyscom.ServFact.DTE_DETALLE[] det(DataTable dt)
        {

            var array = (from rw in dt.AsEnumerable()
                         select new WebApplication_tyscom.ServFact.DTE_DETALLE()
                         {

                             NroLinDet = (rw["NroLinDet"]).ToString(),
                             TpoCodigo = "INT1",
                             VlrCodigo = (rw["VlrCodigo"]).ToString(),
                             IndExe = (rw["IndExe"]).ToString(),
                             IndAgente = (rw["IndAgente"]).ToString(),
                             MntBaseFaena = (rw["MntBaseFaena"]).ToString(),
                             MntMargComer = (rw["MntMargComer"]).ToString(),
                             PrcConsFinal = (rw["PrcConsFinal"]).ToString(),
                             NmbItem = (rw["NmbItem"]).ToString(),
                             DscItem = (rw["DscItem"]).ToString(),
                             QtyRef = (rw["QtyRef"]).ToString(),
                             UnmdRef = (rw["UnmdRef"]).ToString(),
                             PrcRef = (rw["PrcRef"]).ToString(),
                             QtyItem = (rw["QtyItem"]).ToString(),
                             SubQty = (rw["SubQty"]).ToString(),
                             SubCod = (rw["SubCod"]).ToString(),
                             FchElabor = (rw["FchElabor"]).ToString(),
                             FchVencim = (rw["FchVencim"]).ToString(),
                             UnmdItem = (rw["UnmdItem"]).ToString(),
                             PrcItem = (rw["PrcItem"]).ToString(),
                             PrcOtrMon = (rw["PrcOtrMon"]).ToString(),
                             Moneda = (rw["Moneda"]).ToString(),
                             FctConv = (rw["FctConv"]).ToString(),
                             DctoOtrMnda = (rw["DctoOtrMnda"]).ToString(),
                             RecargoOtrMnda = (rw["RecargoOtrMnda"]).ToString(),
                             MontoItemOtrMnda = (rw["MontoItemOtrMnda"]).ToString(),
                             DescuentoPct = (rw["DescuentoPct"]).ToString(),
                             DescuentoMonto = (rw["DescuentoMonto"]).ToString(),
                             TipoDscto = (rw["TipoDscto"]).ToString(),
                             ValorDscto = (rw["ValorDscto"]).ToString(),
                             RecargoPct = (rw["RecargoPct"]).ToString(),
                             RecargoMonto = (rw["RecargoMonto"]).ToString(),
                             TipoRecargo = (rw["TipoRecargo"]).ToString(),
                             ValorRecargo = (rw["ValorRecargo"]).ToString(),
                             CodImpAdic = (rw["CodImpAdic"]).ToString(),
                             MontoItem = (rw["MontoItem"]).ToString()

                         }).ToArray();

            return array;
        }


    public WebApplication_tyscom.ServFact.DTE_referencia[] refer(DataTable dt)
    {

        var array = (from rw in dt.AsEnumerable()
                     select new WebApplication_tyscom.ServFact.DTE_referencia()
                     {
                         NroLinRef = (rw["NroLinRef"]).ToString(),
                         TpoDocRef= (rw["TpoDocRef"]).ToString(),
                         IndGlobal = (rw["IndGlobal"]).ToString(),
                         FolioRef = (rw["FolioRef"]).ToString(),
                         RUTOtr = (rw["RUTOtr"]).ToString(),
                         FchRef = (rw["FchRef"]).ToString(),
                         CodRef = (rw["CodRef"]).ToString(),
                         RazonRef = (rw["RazonRef"]).ToString()

                     }).ToArray();

        return array;
    }

    public WebApplication_tyscom.ServFact.DTE_DscrGlobal[] desc_global(DataTable dt)
    {

        var array = (from rw in dt.AsEnumerable()
                     select new WebApplication_tyscom.ServFact.DTE_DscrGlobal()
                     {
                         NroLinDR = (rw["NroLinDR"]).ToString(),
                         TpoMov = (rw["TpoMov"]).ToString(),
                         GlosaDR = (rw["GlosaDR"]).ToString(),
                         TpoValor = (rw["TpoValor"]).ToString(),
                         ValorDR = (rw["ValorDR"]).ToString(),
                         ValorDROtrMnda = (rw["ValorDROtrMnda"]).ToString(),
                         IndExeDR = (rw["IndExeDR"]).ToString(),
                     }).ToArray();

        return array;
    }
}
