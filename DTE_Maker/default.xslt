<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="2.0" xmlns:fn="http://www.w3.org/2005/02/xpath-functions" xmlns:ms="urn:schemas-microsoft-com:xslt" xmlns:tt="samples-and-documentation" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
xmlns:sii="http://www.sii.cl/SiiDte"
xmlns:local="http://custodium.com/local"
xmlns:func="http://exslt.org/functions"
xmlns:str="http://exslt.org/strings"
xmlns:exsl="http://exslt.org/common"
extension-element-prefixes=" sii local func str exsl"
xmlns:fo="http://www.w3.org/1999/XSL/Format">
  <xsl:decimal-format  name="formato" decimal-separator="," grouping-separator="."  />
  <xsl:decimal-format name="peso" decimal-separator="," grouping-separator="."/>
  <xsl:decimal-format name="dolar" decimal-separator="." grouping-separator=","/>
  <xsl:param name="archivogif" select="'default value'"/>
  <xsl:output encoding="UTF-8" indent="yes" method="xml"/>
  <xsl:variable name="TipoTras" select="/DTE/Documento/Encabezado/IdDoc/IndTraslado"/>
  <xsl:variable name="FechaEmis" select="/DTE/Documento/Encabezado/IdDoc/FchEmis"/>
  <xsl:variable name="FmaPago" select="/DTE/Documento/Encabezado/IdDoc/FmaPago" />
  <xsl:variable name="RutEmis" select="/DTE/Documento/Encabezado/Emisor/RUTEmisor"/>
  <xsl:variable name="RutRecep" select="/DTE/Documento/Encabezado/Receptor/RUTRecep"/>
  <!-- <xsl:variable name="TipoRef" select="/DTE/Documento/Referencia/TpoDocRef"/> -->
  <xsl:variable name="TipoDoc" select="/DTE/Documento/Encabezado/IdDoc/TipoDTE" />
  <xsl:variable name="firma" select="/DTE/Documento/TmstFirma"/>
  <!-- <xsl:variable name="acuserecibo" select="/DTE/Documento/TED/DD/TSTED"/>
  <xsl:variable name="UrlEsquema" select="/DTE/Documento/TED/DD/CAF/DA/RSAPK/M"/>-->
  <xsl:variable name="TpoMoneda" select="/DTE/Documento/Encabezado/OtraMoneda/TpoMoneda"/>
  <xsl:variable name="LargoTabla" select="7.2"/>
  <xsl:variable name="Contador" select="count(/DTE/Documento/Detalle)"/>
  <xsl:variable name="LargoFinal" select="$LargoTabla - (0.7 * $Contador)"/>
  <xsl:variable name="LargoTablaRef" select="3.6"/>
  <xsl:variable name="ContadorRef" select="count(/DTE/Documento/Referencia)"/>
  <xsl:variable name="LargoFinalRef" select="$LargoTablaRef - (0.4 * $ContadorRef)"/>
  <xsl:variable name="NroResol" select="/DTE/Documento/Encabezado/IdDoc/NroResol"/>
  <xsl:variable name="FchResol" select="ms:format-date(/DTE/Documento/Encabezado/IdDoc/FchResol, 'dd-MM-yyyy')"/>
  <xsl:variable name="ListaDocValidos" select="'-30-32-33-34-35-38-39-40-41-43-45-46-50-52-55-56-60-61-103-110-111-112-801-802-803-804-805-806-807-811-812-813-814-815-'"/>
  <xsl:variable name="LogoEmpresa" select="/DTE/Documento/Encabezado/Emisor/Logo1"/>

  <xsl:variable name="Forma-Pago" select="/DTE/Documento/Encabezado/IdDoc/TermPagoCdg" />
  <xsl:variable name="TipoDoc1" select="/DTE/Documento/Referencia/TpoDocRef" />
  <xsl:variable name="DireccionRegional" select="/DTE/Documento/Encabezado/Emisor/OficinaSII" />
  <!-- <xsl:variable name="TipoDoc1" select="concat(format-number(number(substring(DTE/Documento/Referencia/TpoDocRef,1,string-length(DTE/Documento/Referencia/TpoDocRef)-2)),'#.###.###.###','formato'),'-',substring(DTE/Documento/Referencia/TpoDocRef,string-length(DTE/Documento/Referencia/TpoDocRef),1))"/> -->

  <ms:script implements-prefix="tt" language="JScript">function toLower(str) { return str.toLowerCase();}</ms:script>
  <xsl:template match="/DTE">
    <fo:root xmlns:fo="http://www.w3.org/1999/XSL/Format">
      <fo:layout-master-set>
        <fo:simple-page-master margin-bottom="0cm" margin-left="0.5cm" margin-right="0cm" margin-top="0cm" master-name="DteIC" page-width="21cm">
          <fo:region-body margin="0.2in" margin-bottom="0cm" margin-top="0cm" region-name="xsl-region-body"/>
          <fo:region-before extent="1.5cm"/>
          <fo:region-after extent="1.5cm"/>
        </fo:simple-page-master>
      </fo:layout-master-set>
      <fo:page-sequence master-reference="DteIC">
        <fo:flow flow-name="xsl-region-body" font-family="Arial" font-size="6pt" font-weight="bold">
          <fo:table table-layout="fixed" width="100%">
            <fo:table-column column-width="1.5cm"/>
            <fo:table-body>
              <fo:table-row text-align="center">
                <fo:table-cell>
                  <fo:block font-size="3pt" >&#160;</fo:block>
                </fo:table-cell>
              </fo:table-row>
            </fo:table-body>
          </fo:table>
          <!--cabecera doc-->
          <fo:table>
            <fo:table-column column-width="0.5cm"/>
            <fo:table-column column-width="10.5cm"/>
            <fo:table-column column-width="2.0cm"/>
            <fo:table-column column-width="6.0cm"/>
            <fo:table-body>
              <fo:table-row>
                <fo:table-cell>
                  <fo:block font-size="5pt" space-before="3mm">&#160;</fo:block>
                </fo:table-cell>
                <!--ENCABEZADO ..................................-->
                <fo:table-cell>
                  <fo:block font-size="10pt" >
                    <xsl:if test="/DTE/Documento/Encabezado/Emisor/Logo1">
                      <fo:block>
                        <fo:external-graphic width="4cm" src="{$LogoEmpresa}"/>
                      </fo:block>
                    </xsl:if>
                    <fo:block font-size="12pt" space-before="3mm" font-weight="bold">

                      <xsl:value-of select="Documento/Encabezado/Emisor/RznSoc"/>
                    </fo:block>
                    <fo:block font-size="5pt">&#160;</fo:block>
                    <fo:block font-size="9pt" font-weight="bold">
                      <xsl:value-of select="Documento/Encabezado/Emisor/GiroEmis"/>
                    </fo:block>
                    <fo:block font-size="7.5pt">
                      <xsl:value-of select="Documento/Encabezado/Emisor/DirOrigen"/> -
                      <xsl:value-of select="Documento/Encabezado/Emisor/CmnaOrigen"/>
                    </fo:block>
                    <fo:block font-size="7.5pt">
                      Ciudad:
                      <xsl:value-of select="Documento/Encabezado/Emisor/CiudadOrigen"/>
                    </fo:block>
                    <fo:block font-size="7.5pt">
                      Teléfono:
                      <xsl:value-of select="Documento/Encabezado/Emisor/Telefono"/>
                    </fo:block>
                    <fo:block font-size="7.5pt">
                      eMail:
                      <xsl:value-of select="Documento/Encabezado/Emisor/CorreoEmisor"/>
                    </fo:block>
                    <!--fo:external-graphic src="'url('http://pruebasaerosan1203.acepta.com/styles/img/96885450K.jpg')'" content-width="11.0cm" content-height="100%" scaling="uniforme"/-->

                  </fo:block>
                </fo:table-cell>
                <fo:table-cell>

                </fo:table-cell>
                <!-- CUADRO SII ............................. -->
                <fo:table-cell>

                  <fo:block border-style="solid" border-width="0.9mm" border-color="red" space-before="3mm">
                    <fo:block font-size="4pt">&#160;</fo:block>
                    <fo:block font-size="2pt">&#160;</fo:block>
                    <fo:block font-weight="bold" color="red" text-align="center" font-size="12pt">
                      R.U.T.:
                      <xsl:call-template name="Rutformat">
                        <xsl:with-param name="input" select="Documento/Encabezado/Emisor/RUTEmisor"/>
                      </xsl:call-template>
                    </fo:block>
                    <fo:block font-size="9pt">&#160;</fo:block>
                    <fo:block font-weight="bold" color="red" text-align="center" font-size="12pt">
                      <xsl:choose>
                        <xsl:when test="$TipoDoc=30">FACTURA </xsl:when>
                        <xsl:when test="$TipoDoc=32">FACTURA VENTA DE BIENES Y SERVICIOS</xsl:when>
                        <xsl:when test="$TipoDoc=33">FACTURA ELECTRÓNICA</xsl:when>
                        <xsl:when test="$TipoDoc=34">FACTURA EXENTA ELECTRÓNICA</xsl:when>
                        <xsl:when test="$TipoDoc=35">BOLETA</xsl:when>
                        <xsl:when test="$TipoDoc=38">BOLETA EXENTA</xsl:when>
                        <xsl:when test="$TipoDoc=39">BOLETA ELECTRÓNICA</xsl:when>
                        <xsl:when test="$TipoDoc=40">LIQUIDACIÓN ELECTRÓNICA</xsl:when>
                        <xsl:when test="$TipoDoc=41">BOLETA EXENTA ELECTRÓNICA</xsl:when>
                        <xsl:when test="$TipoDoc=43">LIQUIDACIÓN FACTURA ELECTRÓNICA</xsl:when>
                        <xsl:when test="$TipoDoc=45">FACTURA DE COMPRA</xsl:when>
                        <xsl:when test="$TipoDoc=46">FACTURA DE COMPRA ELECTRÓNICA</xsl:when>
                        <xsl:when test="$TipoDoc=50">GUÍA DE DESPACHO</xsl:when>
                        <xsl:when test="$TipoDoc=52">GUÍA DE DESPACHO ELECTRÓNICA</xsl:when>
                        <xsl:when test="$TipoDoc=55">NOTA DE DÉBITO</xsl:when>
                        <xsl:when test="$TipoDoc=56">NOTA DE DÉBITO ELECTRÓNICA</xsl:when>
                        <xsl:when test="$TipoDoc=60">NOTA DE CRÉDITO</xsl:when>
                        <xsl:when test="$TipoDoc=61">NOTA DE CRÉDITO ELECTRÓNICA</xsl:when>
                        <xsl:when test="$TipoDoc=103">LIQUIDACIÓN</xsl:when>
                        <xsl:when test="$TipoDoc=110">FACTURA DE EXPORTACIÓN ELECTRÓNICA</xsl:when>
                        <xsl:when test="$TipoDoc=111">NOTA DE DÉBITO EXPORTACIÓN ELECTRÓNICA</xsl:when>
                        <xsl:when test="$TipoDoc=112">NOTA DE CRÉDITO EXPORTACIÓN ELECTRÓNICA</xsl:when>
                        <xsl:when test="$TipoDoc=801">ORDEN DE COMPRA</xsl:when>
                        <xsl:when test="$TipoDoc=802">NOTA DE PEDIDO</xsl:when>
                        <xsl:when test="$TipoDoc=803">CONTRATO</xsl:when>
                        <xsl:when test="$TipoDoc=804">RESOLUCIÓN</xsl:when>
                        <xsl:when test="$TipoDoc=805">PROCESO CHILE COMPRA</xsl:when>
                        <xsl:when test="$TipoDoc=806">FICHA CHILE CMPRA</xsl:when>
                        <xsl:when test="$TipoDoc=807">DUS</xsl:when>
                        <xsl:when test="$TipoDoc=811">CARTA PORTE</xsl:when>
                        <xsl:when test="$TipoDoc=812">RESOLUCIÓN SNA</xsl:when>
                        <xsl:when test="$TipoDoc=813">PASAPORTE</xsl:when>
                        <xsl:when test="$TipoDoc=814">CERTIFICADO DEPÓSITO BOLSA</xsl:when>
                        <xsl:when test="$TipoDoc=815">VALE PRENDA BOLSA</xsl:when>
                        <xsl:when test="$TipoDoc=I01">Hola probando</xsl:when>
                        <xsl:otherwise>
                          <xsl:value-of select="TipoDTE"/>
                        </xsl:otherwise>
                      </xsl:choose>
                    </fo:block>
                    <fo:block font-size="9pt">&#160;</fo:block>
                    <fo:block font-weight="bold" color="red" text-align="center" font-size="12pt">
                      N°:<xsl:value-of select="Documento/Encabezado/IdDoc/Folio"/>
                    </fo:block>
                    <fo:block font-size="2pt">&#160;</fo:block>
                  </fo:block>
                  <fo:block font-size="6pt">&#160;</fo:block>
                  <xsl:choose>
                    <xsl:when test="/DTE/Documento/Encabezado/Emisor/OficinaSII">
                      <fo:block font-size="9pt" color="red" text-align="center" font-weight="bold">
                        S.I.I <xsl:value-of select="$DireccionRegional"/>
                      </fo:block>
                    </xsl:when>
                    <xsl:otherwise>
                      <fo:block font-size="9pt" color="red" text-align="center" font-weight="bold">
                        S.I.I - NO ENCONTRADO
                      </fo:block>
                    </xsl:otherwise>
                  </xsl:choose>
                </fo:table-cell>
              </fo:table-row>
            </fo:table-body >
          </fo:table >
          <!--fin cabecera doc-->
          <fo:block space-after="4pt"></fo:block>
          <!--cabecera emisor-->
          <fo:table border-style="solid" border-width="0.3mm" space-after="2mm">
            <fo:table-column column-width="1.9cm"/>
            <fo:table-column column-width="0.3cm"/>
            <fo:table-column column-width="9.5cm"/>
            <fo:table-column column-width="2.2cm"/>
            <fo:table-column column-width="0.3cm"/>
            <fo:table-column column-width="4.8cm"/>
            <fo:table-body>
              <fo:table-row>
                <fo:table-cell>
                  <fo:block font-size="1pt">&#160;</fo:block>
                  <fo:block font-size="7pt">&#160;Razón Social</fo:block>
                  <fo:block font-size="7pt">&#160;R.U.T</fo:block>
                  <fo:block font-size="7pt">&#160;Giro</fo:block>
                  <fo:block font-size="7pt">&#160;Dirección</fo:block>
                  <fo:block font-size="7pt">&#160;Comuna </fo:block>
                  <fo:block font-size="7pt">&#160;Ciudad </fo:block>
                </fo:table-cell>
                <fo:table-cell>
                  <fo:block font-size="1pt">&#160;</fo:block>
                  <fo:block font-size="7pt">:</fo:block>
                  <fo:block font-size="7pt">:</fo:block>
                  <fo:block font-size="7pt">:</fo:block>
                  <fo:block font-size="7pt">:</fo:block>
                  <fo:block font-size="7pt">:</fo:block>
                  <fo:block font-size="7pt">:</fo:block>
                </fo:table-cell>
                <fo:table-cell>
                  <fo:block font-size="1pt">&#160;</fo:block>
                  <fo:block font-size="7pt">
                    <xsl:value-of select="Documento/Encabezado/Receptor/RznSocRecep"/>
                  </fo:block>
                  <fo:block font-size="7pt">
                    <xsl:call-template name="Rutformat">
                      <xsl:with-param name="input" select="Documento/Encabezado/Receptor/RUTRecep"/>
                    </xsl:call-template>
                  </fo:block>
                  <fo:block font-size="7pt">
                    <xsl:value-of select="Documento/Encabezado/Receptor/GiroRecep"/>
                  </fo:block>
                  <fo:block font-size="7pt">
                    <xsl:value-of select="Documento/Encabezado/Receptor/DirRecep"/>
                  </fo:block>
                  <fo:block font-size="7pt">
                    <xsl:value-of select="Documento/Encabezado/Receptor/CmnaRecep"/>
                  </fo:block>
                  <fo:block font-size="7pt">
                    <xsl:value-of select="Documento/Encabezado/Receptor/CiudadRecep"/>
                  </fo:block>
                </fo:table-cell>
                <fo:table-cell>
                  <fo:block font-size="1pt">&#160;</fo:block>
                  <fo:block font-size="7pt">Fecha</fo:block>
                  <fo:block font-size="7pt">Vence</fo:block>
                  <fo:block font-size="7pt">Pago</fo:block>
                  <fo:block font-size="7pt">&#160;</fo:block>
                  <fo:block font-size="7pt">&#160;</fo:block>
                  <fo:block font-size="7pt">&#160;</fo:block>
                </fo:table-cell>
                <fo:table-cell>
                  <fo:block font-size="1pt">&#160;</fo:block>
                  <fo:block font-size="7pt">:</fo:block>
                  <fo:block font-size="7pt">:</fo:block>
                  <fo:block font-size="7pt">:</fo:block>
                  <fo:block font-size="7pt">&#160;</fo:block>
                  <fo:block font-size="7pt">&#160;</fo:block>
                  <fo:block font-size="7pt">&#160;</fo:block>
                </fo:table-cell>
                <fo:table-cell>
                  <fo:block font-size="1pt">&#160;</fo:block>
                  <fo:block font-size="7pt">
                    <xsl:call-template name="format-fecha-all">
                      <xsl:with-param name="input" select="Documento/Encabezado/IdDoc/FchEmis"/>
                      <xsl:with-param name="nombre" select="'nombre'"/>
                      <xsl:with-param name="separador" select="' de '"/>
                    </xsl:call-template>
                  </fo:block>
                  <fo:block font-size="7pt">
                    <xsl:if test="Documento/Encabezado/IdDoc/FchVenc">
                      <xsl:call-template name="format-fecha-all">
                        <xsl:with-param name="input" select="Documento/Encabezado/IdDoc/FchVenc"/>
                        <xsl:with-param name="nombre" select="'nombre'"/>
                        <xsl:with-param name="separador" select="' de '"/>
                      </xsl:call-template>
                    </xsl:if>&#160;
                  </fo:block>
                  <fo:block font-size="7pt">
                    <xsl:choose>
                      <xsl:when test="$Forma-Pago=1">Contado </xsl:when>
                      <xsl:when test="$Forma-Pago=2">Cr&#233;dito</xsl:when>
                      <xsl:when test="$Forma-Pago=3">Sin Costo (entrega gratuita)</xsl:when>
                      <xsl:otherwise>
                        <xsl:value-of select="TermPagoCdg"/>
                      </xsl:otherwise>
                    </xsl:choose>
                  </fo:block>
                  <fo:block font-size="7pt">&#160;</fo:block>
                  <fo:block font-size="7pt">&#160;</fo:block>
                  <fo:block font-size="7pt">&#160;</fo:block>
                </fo:table-cell>
              </fo:table-row>
            </fo:table-body>
          </fo:table>
          <!--fin cabecera emisor-->
          <!--receptor-->
          <!--fin receptor-->
          <!--REFERENCIAS-->
          <!--TABLA DE DETALLE-->
          <fo:table border-style="solid" border-width="0.3mm" border-height="0.3mm" space-after="2mm" height="350px" border-bottom-style="solid">
            <fo:table-column column-width="2.0cm"/>
            <fo:table-column column-width="1.0cm"/>
            <fo:table-column column-width="9.0cm"/>
            <fo:table-column column-width="0.4cm"/>
            <fo:table-column column-width="2.2cm"/>
            <fo:table-column column-width="2.2cm"/>
            <fo:table-column column-width="2.2cm"/>
            <fo:table-body>
              <!-- ETIQUETAS DETALLE............................. -->
              <fo:table-row font-size="7pt" text-align="center" font-weight="bold">
                <fo:table-cell border-bottom-style="solid" border-height="50mm" border-width="0.3mm">
                  <fo:block font-size="2pt">&#160;</fo:block>
                  <fo:block>Código</fo:block>
                </fo:table-cell>
                <fo:table-cell border-bottom-style="solid" border-height="50mm" border-width="0.3mm">
                  <fo:block font-size="2pt">&#160;</fo:block>
                  <fo:block>Cant.</fo:block>
                </fo:table-cell>
                <fo:table-cell border-bottom-style="solid"  border-width="0.3mm">
                  <fo:block font-size="2pt">&#160;</fo:block>
                  <fo:block>Descripci&#243;n</fo:block>
                </fo:table-cell>
                <fo:table-cell border-bottom-style="solid" border-width="0.3mm">
                  <fo:block font-size="2pt">&#160;</fo:block>
                  <fo:block>E</fo:block>
                </fo:table-cell>
                <fo:table-cell border-bottom-style="solid" border-width="0.3mm">
                  <fo:block font-size="2pt">&#160;</fo:block>
                  <fo:block>Precio</fo:block>
                </fo:table-cell>
                <fo:table-cell border-bottom-style="solid" border-width="0.3mm">
                  <fo:block font-size="2pt">&#160;&#160;&#160;</fo:block>
                  <fo:block>Desc/Recargo</fo:block>
                </fo:table-cell>
                <fo:table-cell border-bottom-style="solid" border-width="0.3mm">
                  <fo:block font-size="2pt">&#160;&#160;&#160;</fo:block>
                  <fo:block>Total</fo:block>
                </fo:table-cell>
              </fo:table-row>
              <xsl:for-each select="Documento/Detalle">
                <fo:table-row>
                  <!--CANTIDAD -->
                  <fo:table-cell font-size="7pt"  border-width="0.3mm" padding-right="0.9mm">
                    <fo:block text-align="right" padding-right="0.9mm">
                      <xsl:value-of select="CdgItem[1]/VlrCodigo"/>
                    </fo:block>
                  </fo:table-cell>
                  <fo:table-cell font-size="7pt"  border-width="0.3mm" padding-right="0.9mm">
                    <fo:block text-align="right" padding-right="0.9mm">
                      &#160;&#160;&#160;<xsl:value-of select="format-number(QtyItem, '.##0,####', 'peso')"/>&#160;
                    </fo:block>
                  </fo:table-cell>
                  <!--DESCRIPCION PRODUCTO -->
                  <fo:table-cell border-width="0.3mm">
                    <xsl:if test="NmbItem">
                      <fo:block text-align="left" font-size="8pt">
                        <xsl:value-of select="NmbItem"/>
                      </fo:block>
                    </xsl:if>
                    <xsl:choose>
                      <xsl:when test="DscItem">
                        <fo:block text-align="left" font-size="7pt">
                          &#160;&#160;
                          <xsl:call-template name="divide_en_lineas">
                            <xsl:with-param name="val" select="DscItem"/>
                            <xsl:with-param name="c1" select="'^'"/>
                          </xsl:call-template>
                        </fo:block>
                        <xsl:for-each select="Subcantidad">
                          <fo:block text-align="left" font-size="6pt">
                            &#160;
                            &#160;&#160;&#160;&#160;&#160;&#160;<xsl:value-of select="SubCod"/> &#160;&#160;|&#160;&#160;Cant: <xsl:value-of select="SubQty"/>
                          </fo:block>
                        </xsl:for-each>
                      </xsl:when>
                      <xsl:otherwise>&#160;</xsl:otherwise>
                    </xsl:choose>
                  </fo:table-cell>
                  <!--EXENTO-->
                  <fo:table-cell border-width="0.3mm">
                    <xsl:if test="IndExe">
                      <fo:block text-align="left">
                        &#160;
                        <xsl:value-of select="IndExe"/>
                      </fo:block>
                    </xsl:if>
                  </fo:table-cell>



                  <!--PRECIO-->
                  <fo:table-cell border-width="0.3mm">
                    <fo:block text-align="right">
                      <xsl:value-of select="format-number(PrcItem,'###.###','formato')"/>&#160;
                    </fo:block>
                  </fo:table-cell>
                  <!--Desc/RECARGO-->
                  <!-- <fo:table-cell border-right-style="solid" border-width="0.3mm">							
											<fo:block text-align="right">
											<xsl:choose>
												<xsl:when test="DescuentoMonto[.!='']">
													<xsl:call-template name="NaN">
														<xsl:with-param name="number" select="format-number(DescuentoMonto, '.##0,####', 'peso')"/>
													</xsl:call-template>&#160;	
												</xsl:when>
												<xsl:otherwise>&#160;</xsl:otherwise>
											</xsl:choose>	
											</fo:block>								
									</fo:table-cell> -->
                  <fo:table-cell border-width="0.3mm">
                    <fo:block text-align="right">
                      <xsl:choose>
                        <xsl:when test="DescuentoMonto">
                          -<xsl:call-template name="formatea-number">
                            <xsl:with-param name="val" select="DescuentoMonto"/>
                            <xsl:with-param name="format-string" select="'.##0'"/>
                            <xsl:with-param name="peso" select="'peso'"/>
                          </xsl:call-template>

                          <xsl:value-of select="format-number(DescuentoMonto,'###.###', 'formato')"/>
                          (-<xsl:value-of select="format-number(DescuentoPct, '##,##', 'formato')"/>%)

                        </xsl:when>
                        <xsl:when test="RecargoMonto">
                          <xsl:call-template name="formatea-number">
                            <xsl:with-param name="val" select="RecargoMonto"/>
                            <xsl:with-param name="format-string" select="'.##0'"/>
                            <xsl:with-param name="peso" select="'peso'"/>
                          </xsl:call-template>
                          -<xsl:value-of select="format-number(RecargoMonto, '###.###', 'formato')"/>
                          (<xsl:value-of select="format-number(RecargoPct, '##,##', 'formato')"/>%)
                        </xsl:when>
                      </xsl:choose>&#160;&#160;
                    </fo:block>
                  </fo:table-cell>
                  <!--TOTAL LINEA (NETO)-->
                  <fo:table-cell border-width="0.3mm">
                    <fo:block text-align="right">
                      <xsl:value-of select="format-number(MontoItem,'###.###','formato')"/>&#160;
                    </fo:block>
                  </fo:table-cell>
                </fo:table-row>
              </xsl:for-each>


            </fo:table-body>
          </fo:table>
          <!--FIN TABLA DE DETALLE-->

          <!--  <fo:block space-after="4pt" > </fo:block> -->
          <!-- TOTALES............................. -->
          <fo:table>
            <fo:table-column column-width="13.5cm"/>
            <fo:table-column column-width="0.2cm"/>
            <fo:table-column column-width="5.3cm"/>
            <fo:table-body>
              <fo:table-row>

                <fo:table-cell>
                  <fo:block>
                    <fo:table border-style="solid" border-width="0.3mm" space-after="2mm">
                      <fo:table-column column-width="4.3cm"/>
                      <fo:table-column column-width="2.8cm"/>
                      <fo:table-column column-width="2.3cm"/>
                      <fo:table-column column-width="4.1cm"/>

                      <fo:table-body>
                        <fo:table-row font-size="7pt" text-align="center" font-weight="bold" height="0.3cm">
                          <fo:table-cell>
                            <fo:block border-bottom-style="solid" border-right-style="solid" border-width="0.3mm">Tipo Doc. Ref.</fo:block>
                          </fo:table-cell>
                          <fo:table-cell>
                            <fo:block border-bottom-style="solid" border-right-style="solid" border-width="0.3mm">Folio Ref.</fo:block>
                          </fo:table-cell>
                          <fo:table-cell>
                            <fo:block border-bottom-style="solid" border-right-style="solid" border-width="0.3mm">Fecha Ref.</fo:block>
                          </fo:table-cell>
                          <fo:table-cell>
                            <fo:block border-bottom-style="solid" border-right-style="solid" border-width="0.3mm">Raz&#243;n Ref.</fo:block>
                          </fo:table-cell>
                        </fo:table-row>
                        <fo:table-row font-size="6pt">
                          <fo:table-cell>
                            <!-- <fo:block border-right-style="solid" text-align="center" space-before="0.2mm" border-height="1.6mm" border-width="0.3mm">
										<xsl:choose>
											<xsl:when test="$TipoDoc1=30">FACTURA </xsl:when>
											<xsl:when test="$TipoDoc1=32">FACTURA VENTA DE BIENES Y SERVICIOS</xsl:when>
											<xsl:when test="$TipoDoc1=33">FACTURA ELECTRÓNICA</xsl:when>
											<xsl:when test="$TipoDoc1=34">FACTURA EXENTA ELECTRÓNICA</xsl:when>
											<xsl:when test="$TipoDoc1=35">BOLETA</xsl:when>
											<xsl:when test="$TipoDoc1=38">BOLETA EXENTA</xsl:when>
											<xsl:when test="$TipoDoc1=39">BOLETA ELECTRÓNICA</xsl:when>
											<xsl:when test="$TipoDoc1=40">LIQUIDACIÓN ELECTRÓNICA</xsl:when>
											<xsl:when test="$TipoDoc1=41">BOLETA EXENTA ELECTRÓNICA</xsl:when>
											<xsl:when test="$TipoDoc1=43">LIQUIDACIÓN FACTURA ELECTRÓNICA</xsl:when>
											<xsl:when test="$TipoDoc1=45">FACTURA DE COMPRA</xsl:when>
											<xsl:when test="$TipoDoc1=46">FACTURA DE COMPRA ELECTRÓNICA</xsl:when>
											<xsl:when test="$TipoDoc1=50">GUÍA DE DESPACHO</xsl:when>
											<xsl:when test="$TipoDoc1=52">GUÍA DE DESPACHO ELECTRÓNICA</xsl:when>
											<xsl:when test="$TipoDoc1=55">NOTA DE DÉBITO</xsl:when>
											<xsl:when test="$TipoDoc1=56">NOTA DE DÉBITO ELECTRÓNICA</xsl:when>
											<xsl:when test="$TipoDoc1=60">NOTA DE CRÉDITO</xsl:when>
											<xsl:when test="$TipoDoc1=61">NOTA DE CRÉDITO ELECTRÓNICA</xsl:when>
											<xsl:when test="$TipoDoc1=103">LIQUIDACIÓN</xsl:when>
											<xsl:when test="$TipoDoc1=110">FACTURA DE EXPORTACIÓN ELECTRÓNICA</xsl:when>
											<xsl:when test="$TipoDoc1=111">NOTA DE DÉBITO EXPORTACIÓN ELECTRÓNICA</xsl:when>
											<xsl:when test="$TipoDoc1=112">NOTA DE CRÉDITO EXPORTACIÓN ELECTRÓNICA</xsl:when>
											<xsl:when test="$TipoDoc1=801">ORDEN DE COMPRA</xsl:when>
											<xsl:when test="$TipoDoc1=802">NOTA DE PEDIDO</xsl:when>
											<xsl:when test="$TipoDoc1=803">CONTRATO</xsl:when>
											<xsl:when test="$TipoDoc1=804">RESOLUCIÓN</xsl:when>
											<xsl:when test="$TipoDoc1=805">PROCESO CHILE COMPRA</xsl:when>
											<xsl:when test="$TipoDoc1=806">FICHA CHILE CMPRA</xsl:when>
											<xsl:when test="$TipoDoc1=807">DUS</xsl:when>
											<xsl:when test="$TipoDoc1=811">CARTA PORTE</xsl:when>
											<xsl:when test="$TipoDoc1=812">RESOLUCIÓN SNA</xsl:when>
											<xsl:when test="$TipoDoc1=813">PASAPORTE</xsl:when>
											<xsl:when test="$TipoDoc1=814">CERTIFICADO DEPÓSITO BOLSA</xsl:when>
											<xsl:when test="$TipoDoc1=815">VALE PRENDA BOLSA</xsl:when>
											<xsl:when test="$TipoDoc1=01">INTERNO</xsl:when>
												<xsl:otherwise>
													<xsl:value-of select="TpoDocRef"/>
												</xsl:otherwise>
										</xsl:choose>
									</fo:block> -->
                            <xsl:for-each select="Documento/Referencia">
                              <xsl:choose>
                                <xsl:when test="TpoDocRef[.!='I99']">
                                  <fo:block border-right-style="solid" text-align="center" space-before="0.2mm" border-height="1.6mm" border-width="0.3mm">
                                    <xsl:call-template name="TpoDoc">
                                      <xsl:with-param name="TipoDoc1" select="TpoDocRef"/>
                                    </xsl:call-template>
                                  </fo:block>
                                </xsl:when>
                              </xsl:choose>
                            </xsl:for-each>
                          </fo:table-cell>
                          <fo:table-cell>
                            <xsl:for-each select="Documento/Referencia">
                              <xsl:choose>
                                <xsl:when test="TpoDocRef[.!='I99']">
                                  <fo:block border-right-style="solid" space-before="0.2mm" text-align="center" border-width="0.3mm">
                                    <!-- <xsl:value-of select="concat(formatea-number(number(substring(Documento/Referencia/FolioRef,1,string-length(Documento/Referencia/FolioRef)-2)),'#.###.###.###','formato'),'-',substring(Documento/Referencia/FolioRef,string-length(Documento/Referencia/FolioRef),1))"/>&#160; -->
                                    <xsl:value-of select="FolioRef"/>&#160;
                                    <!-- <xsl:value-of select="concat(substring(Documento/Referencia/FolioRef,1,string-length(Documento/Referencia/FolioRef)-2),'ABCDEFGHIJKLMNOPQRSTUV','formato'))"/>&#160; -->
                                  </fo:block>
                                </xsl:when>
                              </xsl:choose>
                            </xsl:for-each>
                          </fo:table-cell>
                          <fo:table-cell>
                            <xsl:for-each select="Documento/Referencia">
                              <xsl:choose>
                                <xsl:when test="TpoDocRef[.!='I99']">
                                  <fo:block border-right-style="solid" text-align="center" space-before="0.2mm" border-width="0.3mm">
                                    &#160;
                                    <xsl:call-template name="format-fecha-all">
                                      <xsl:with-param name="input" select="FchRef"/>
                                      <xsl:with-param name="separador" select="'-'"/>
                                    </xsl:call-template>
                                  </fo:block>
                                </xsl:when>
                              </xsl:choose>
                            </xsl:for-each>
                          </fo:table-cell>
                          <fo:table-cell>
                            <xsl:for-each select="Documento/Referencia">
                              <xsl:choose>
                                <xsl:when test="TpoDocRef[.!='I99']">
                                  <fo:block border-right-style="solid" text-align="center" space-before="0.2mm" border-width="0.3mm">
                                    <xsl:value-of select="RazonRef"/>&#160;
                                  </fo:block>
                                </xsl:when>
                              </xsl:choose>
                            </xsl:for-each>
                          </fo:table-cell>
                        </fo:table-row>
                      </fo:table-body>
                    </fo:table>
                  </fo:block>
                  <fo:block>
                    <!-- OBSERVACION............................. -->
                    <fo:table border-style="solid" border-width="0.3mm" space-after="2mm">
                      <!-- Columnas con transporte: valida si existe el campo............................. -->
                      <xsl:choose>
                        <xsl:when test="Documento/Encabezado/Transporte">
                          <fo:table-column column-width="6.75cm"/>
                          <fo:table-column column-width="6.75cm"/>
                        </xsl:when>
                        <!-- De no existir, genera solo 1 con el dato de Observacion............................. -->
                        <xsl:otherwise>
                          <fo:table-column column-width="13.5cm"/>
                        </xsl:otherwise>
                      </xsl:choose>
                      <fo:table-body>
                        <fo:table-row height="1.5cm">
                          <!-- OBSERVACION............................. -->
                          <fo:table-cell border-top-style="solid" border-right-style="solid" border-width="0.3mm">
                            <fo:block font-size="6pt" margin-left="0.1cm">
                              OBSERVACIONES:
                            </fo:block>

                            <fo:block border-right-style="solid" text-align="center" space-before="0.2mm" border-height="1.6mm" border-width="0.3mm">
                              <xsl:choose>
                                <xsl:when test="Documento/Referencia/TpoDocRef[.='I99']">
                                  <fo:block font-size="7pt" text-align="left" margin-left="0.1cm">
                                    <xsl:value-of select="Documento/Referencia/RazonRef"/>&#160;&#160;&#160;
                                  </fo:block>
                                </xsl:when>
                              </xsl:choose>
                              <!-- <xsl:choose>
											<xsl:when test="$TipoDoc1=30">FACTURA </xsl:when>
											<xsl:when test="$TipoDoc1=32">FACTURA VENTA DE BIENES Y SERVICIOS</xsl:when>
											<xsl:when test="$TipoDoc1=33">FACTURA ELECTRÓNICA</xsl:when>
											<xsl:when test="$TipoDoc1=34">FACTURA EXENTA ELECTRÓNICA</xsl:when>
											<xsl:when test="$TipoDoc1=35">BOLETA</xsl:when>
											<xsl:when test="$TipoDoc1=38">BOLETA EXENTA</xsl:when>
											<xsl:when test="$TipoDoc1=39">BOLETA ELECTRÓNICA</xsl:when>
											<xsl:when test="$TipoDoc1=40">LIQUIDACIÓN ELECTRÓNICA</xsl:when>
											<xsl:when test="$TipoDoc1=41">BOLETA EXENTA ELECTRÓNICA</xsl:when>
											<xsl:when test="$TipoDoc1=43">LIQUIDACIÓN FACTURA ELECTRÓNICA</xsl:when>
											<xsl:when test="$TipoDoc1=45">FACTURA DE COMPRA</xsl:when>
											<xsl:when test="$TipoDoc1=46">FACTURA DE COMPRA ELECTRÓNICA</xsl:when>
											<xsl:when test="$TipoDoc1=50">GUÍA DE DESPACHO</xsl:when>
											<xsl:when test="$TipoDoc1=52">GUÍA DE DESPACHO ELECTRÓNICA</xsl:when>
											<xsl:when test="$TipoDoc1=55">NOTA DE DÉBITO</xsl:when>
											<xsl:when test="$TipoDoc1=56">NOTA DE DÉBITO ELECTRÓNICA</xsl:when>
											<xsl:when test="$TipoDoc1=60">NOTA DE CRÉDITO</xsl:when>
											<xsl:when test="$TipoDoc1=61">NOTA DE CRÉDITO ELECTRÓNICA</xsl:when>
											<xsl:when test="$TipoDoc1=103">LIQUIDACIÓN</xsl:when>
											<xsl:when test="$TipoDoc1=110">FACTURA DE EXPORTACIÓN ELECTRÓNICA</xsl:when>
											<xsl:when test="$TipoDoc1=111">NOTA DE DÉBITO EXPORTACIÓN ELECTRÓNICA</xsl:when>
											<xsl:when test="$TipoDoc1=112">NOTA DE CRÉDITO EXPORTACIÓN ELECTRÓNICA</xsl:when>
											<xsl:when test="$TipoDoc1=801">ORDEN DE COMPRA</xsl:when>
											<xsl:when test="$TipoDoc1=802">NOTA DE PEDIDO</xsl:when>
											<xsl:when test="$TipoDoc1=803">CONTRATO</xsl:when>
											<xsl:when test="$TipoDoc1=804">RESOLUCIÓN</xsl:when>
											<xsl:when test="$TipoDoc1=805">PROCESO CHILE COMPRA</xsl:when>
											<xsl:when test="$TipoDoc1=806">FICHA CHILE CMPRA</xsl:when>
											<xsl:when test="$TipoDoc1=807">DUS</xsl:when>
											<xsl:when test="$TipoDoc1=811">CARTA PORTE</xsl:when>
											<xsl:when test="$TipoDoc1=812">RESOLUCIÓN SNA</xsl:when>
											<xsl:when test="$TipoDoc1=813">PASAPORTE</xsl:when>
											<xsl:when test="$TipoDoc1=814">CERTIFICADO DEPÓSITO BOLSA</xsl:when>
											<xsl:when test="$TipoDoc1=815">VALE PRENDA BOLSA</xsl:when>
											<xsl:when test="$TipoDoc1=I99"><xsl:value-of select="Documento/Referencia/RazonRef"/></xsl:when>
												<xsl:otherwise>
													[.!='']<xsl:value-of select="TpoDocRef"/>
												</xsl:otherwise> -->
                              <!--</xsl:choose> -->
                            </fo:block>

                            <!-- <fo:block>
															<xsl:call-template name="divide_en_lineas">
																<xsl:with-param name="val" select="DatosAdjuntos[NombreDA='Observacion']/ValorDA"/>
																<xsl:with-param name="c1" select="'^'"/>
															</xsl:call-template>
													</fo:block>	
													<fo:block>	
														<xsl:call-template name="divide_en_lineas">
															<xsl:with-param name="val" select="DatosAdjuntos[NombreDA='Observacion']/ValorDA"/>
															<xsl:with-param name="c1" select="'^'"/>
														</xsl:call-template>
													</fo:block>	 -->


                          </fo:table-cell>
                          <!-- TRANSPORTE: valida si existe el campo............................. -->
                          <xsl:choose>
                            <xsl:when test="Documento/Encabezado/Transporte">
                              <fo:table-cell border-top-style="solid" border-right-style="solid" border-width="0.3mm">
                                <fo:block font-size="6pt" space-after="0.3mm">
                                  &#160;INDICADOR DE TRASLADO:&#160;&#160;
                                  <xsl:call-template name="Traslado">
                                    <xsl:with-param name="CodTraslado" select="Documento/Encabezado/IdDoc/IndTraslado"/>
                                  </xsl:call-template>
                                </fo:block>
                                <xsl:choose>
                                  <xsl:when test="Documento/Encabezado/Transporte/Patente">
                                    <fo:block font-size="6pt" space-after="0.3mm">
                                      &#160;PATENTE:&#160;&#160;<xsl:value-of select='Documento/Encabezado/Transporte/Patente'/>
                                    </fo:block>
                                  </xsl:when>
                                  <xsl:otherwise>&#160;</xsl:otherwise>
                                </xsl:choose>
                                <fo:block font-size="6pt" space-after="0.3mm">
                                  &#160;DIRECCION DEST.:&#160;&#160;<xsl:value-of select='Documento/Encabezado/Transporte/DirDest'/>
                                </fo:block>
                                <xsl:choose>
                                  <xsl:when test="Documento/Encabezado/Transporte/RUTTrans">
                                    <fo:block font-size="6pt" space-after="0.3mm">
                                      &#160;R.U.T. TRANS.:&#160;&#160;
                                      <xsl:call-template name="Rutformat">
                                        <xsl:with-param name="input" select="Documento/Encabezado/Transporte/RUTTrans"/>
                                      </xsl:call-template>
                                    </fo:block>
                                  </xsl:when>
                                  <xsl:otherwise>&#160;</xsl:otherwise>
                                </xsl:choose>
                                <fo:block font-size="6pt" space-after="0.3mm">
                                  &#160;COMUNA DEST.:&#160;&#160;<xsl:value-of select='Documento/Encabezado/Transporte/CmnaDest'/>
                                </fo:block>
                              </fo:table-cell>
                            </xsl:when>
                          </xsl:choose>
                        </fo:table-row>
                      </fo:table-body>
                    </fo:table>
                  </fo:block>
                  <fo:block>
                    <fo:table text-align="left">
                      <fo:table-column column-width="5.9cm"/>
                      <fo:table-column column-width="0.2cm"/>
                      <fo:table-column column-width="5.4cm"/>
                      <fo:table-body>
                        <fo:table-row>
                          <fo:table-cell text-align="center">
                            <!--  <fo:block font-size="7pt" space-before="2mm">
                              <fo:external-graphic width="150pt" src="PDF417"/>
                            </fo:block>-->
                            <!--  <fo:block font-size="6px" text-align="center">Timbre Electronico SII</fo:block>-->
                            <!-- <fo:block font-size="6px" text-align="center">
                              Res
                              <xsl:value-of select="Documento/Encabezado/IdDoc/NroResol"/>de
                              <xsl:value-of select="ms:format-date(Documento/Encabezado/IdDoc/FchResol, 'yyyy')"/>- Verifique este documento en www.sii.cl
                            </fo:block>-->
                          </fo:table-cell>
                          <fo:table-cell>
                            <fo:block>&#160;</fo:block>
                          </fo:table-cell>
                          <fo:table-cell>
                            <fo:block font-size="2pt">&#160;</fo:block>
                            <fo:block font-size="5pt" space-before="2mm">
                              <fo:table border-style="solid" border-width="0.3mm">
                                <fo:table-column column-width="1.5cm"/>
                                <fo:table-column column-width="0.2cm"/>
                                <fo:table-column column-width="2.0cm"/>
                                <fo:table-column column-width="1.5cm"/>
                                <fo:table-column column-width="2.0cm"/>
                                <fo:table-column column-width="0.2cm"/>
                                <fo:table-body>
                                  <fo:table-row>
                                    <fo:table-cell>
                                      <fo:block font-size="8pt">&#160;Nombre</fo:block>
                                    </fo:table-cell>
                                    <fo:table-cell>
                                      <fo:block font-size="8pt">:</fo:block>
                                    </fo:table-cell>
                                    <fo:table-cell border-bottom-style="solid" border-width="0.3mm">
                                      <fo:block>&#160;</fo:block>
                                    </fo:table-cell>
                                    <fo:table-cell border-bottom-style="solid" border-width="0.3mm">
                                      <fo:block>&#160;</fo:block>
                                    </fo:table-cell>
                                    <fo:table-cell border-bottom-style="solid" border-width="0.3mm">
                                      <fo:block>&#160;</fo:block>
                                    </fo:table-cell>
                                    <fo:table-cell>
                                      <fo:block font-size="8pt">&#160;</fo:block>
                                    </fo:table-cell>
                                  </fo:table-row>
                                  <fo:table-row>
                                    <fo:table-cell>
                                      <fo:block font-size="8pt">&#160;R.U.T</fo:block>
                                    </fo:table-cell>
                                    <fo:table-cell>
                                      <fo:block font-size="8pt">:</fo:block>
                                    </fo:table-cell>
                                    <fo:table-cell border-bottom-style="solid" border-width="0.3mm">
                                      <fo:block>&#160;</fo:block>
                                    </fo:table-cell>
                                    <fo:table-cell>
                                      <fo:block font-size="8pt">&#160;&#160;Fecha :&#160;&#160;&#160;&#160;</fo:block>
                                    </fo:table-cell>
                                    <fo:table-cell border-bottom-style="solid" border-width="0.3mm">
                                      <fo:block>&#160;</fo:block>
                                    </fo:table-cell>
                                    <fo:table-cell>
                                      <fo:block font-size="8pt">&#160;</fo:block>
                                    </fo:table-cell>
                                  </fo:table-row>
                                  <fo:table-row>
                                    <fo:table-cell>
                                      <fo:block font-size="8pt">&#160;Recinto</fo:block>
                                    </fo:table-cell>
                                    <fo:table-cell>
                                      <fo:block font-size="8pt">:</fo:block>
                                    </fo:table-cell>
                                    <fo:table-cell border-bottom-style="solid" border-width="0.3mm">
                                      <fo:block>&#160;</fo:block>
                                    </fo:table-cell>
                                    <fo:table-cell border-bottom-style="solid" border-width="0.3mm">
                                      <fo:block>&#160;</fo:block>
                                    </fo:table-cell>
                                    <fo:table-cell border-bottom-style="solid" border-width="0.3mm">
                                      <fo:block>&#160;</fo:block>
                                    </fo:table-cell>
                                    <fo:table-cell>
                                      <fo:block font-size="8pt">&#160;</fo:block>
                                    </fo:table-cell>
                                  </fo:table-row>
                                  <fo:table-row>
                                    <fo:table-cell>
                                      <fo:block font-size="8pt">&#160;Firma</fo:block>
                                    </fo:table-cell>
                                    <fo:table-cell>
                                      <fo:block font-size="8pt">:</fo:block>
                                    </fo:table-cell>
                                    <fo:table-cell border-bottom-style="solid" border-width="0.3mm">
                                      <fo:block>&#160;</fo:block>
                                    </fo:table-cell>
                                    <fo:table-cell border-bottom-style="solid" border-width="0.3mm">
                                      <fo:block>&#160;</fo:block>
                                    </fo:table-cell>
                                    <fo:table-cell border-bottom-style="solid" border-width="0.3mm">
                                      <fo:block>&#160;</fo:block>
                                    </fo:table-cell>
                                    <fo:table-cell>
                                      <fo:block font-size="8pt">&#160;</fo:block>
                                    </fo:table-cell>
                                  </fo:table-row>
                                  <fo:table-row>
                                    <fo:table-cell number-columns-spanned="6">
                                      <fo:block font-size="4pt">&#160;</fo:block>
                                    </fo:table-cell>
                                  </fo:table-row>
                                  <fo:table-row>
                                    <fo:table-cell number-columns-spanned="6">
                                      <fo:block font-size="5pt">&#160;EL ACUSE DE RECIBO QUE SE DECLARA EN ESTE ACTO, DE ACUERDO A LO DISPUESTO EN LA LETRA b) &#160;DEL Art.4&#176;, Y LA LETRA c) DEL Art.5&#176; DE LA LEY&#160;19.983, ACREDITA QUE LA ENTREGA DE MERCADERIAS O &#160;SERVICIOS(S) PRESTADO(S) HA(N) SIDO RECIBIDO(S).</fo:block>
                                    </fo:table-cell>
                                  </fo:table-row>
                                  <fo:table-row>
                                    <fo:table-cell number-columns-spanned="6">
                                      <fo:block font-size="1pt">&#160;</fo:block>
                                    </fo:table-cell>
                                  </fo:table-row>
                                </fo:table-body>
                              </fo:table>
                            </fo:block>
                          </fo:table-cell>
                        </fo:table-row>
                      </fo:table-body>
                    </fo:table>
                  </fo:block>
                </fo:table-cell>
                <fo:table-cell>
                  <fo:block font-size="3pt">&#160;</fo:block>
                </fo:table-cell>
                <!-- 							<fo:table-cell>
								<fo:block>
									
								</fo:block>
							</fo:table-cell> -->
                <fo:table-cell>
                  <fo:block>
                    <!-- TOTALES............................. -->
                    <fo:table border-style="solid" border-width="0.3mm">
                      <fo:table-column column-width="2.65cm"/>
                      <fo:table-column column-width="2.65cm"/>
                      <fo:table-body>
                        <!-- MONTO NETO -->
                        <xsl:choose>
                          <xsl:when test="Documento/Encabezado/Totales/MntNeto[.!='']">
                            <fo:table-row display-align="center" height="5mm">
                              <fo:table-cell>
                                <fo:block font-size="7pt">&#160;Monto Neto</fo:block>
                              </fo:table-cell>
                              <fo:table-cell>
                                <fo:block font-size="7pt" text-align="right">
                                  <xsl:value-of select="format-number(Documento/Encabezado/Totales/MntNeto, '###.##0', 'peso')"/>&#160;
                                </fo:block>
                              </fo:table-cell>
                            </fo:table-row>
                          </xsl:when>
                        </xsl:choose>
                        <!-- DESCUENTOS GLOBALES  -->
                        <xsl:for-each select="Documento/DscRcgGlobal">
                          <xsl:choose>
                            <xsl:when test="TpoMov[.='D']">
                              <fo:table-row height="4mm" display-align="center">
                                <fo:table-cell>
                                  <fo:block font-size="7pt">
                                    &#160;Descuento&#160;<xsl:value-of select='TpoValor'/>
                                  </fo:block>
                                </fo:table-cell>
                                <fo:table-cell>
                                  <fo:block font-size="7pt" text-align="right">
                                    <xsl:value-of select="format-number(ValorDR, '###.##0', 'peso')"/>&#160;
                                  </fo:block>
                                </fo:table-cell>
                              </fo:table-row>
                            </xsl:when>
                            <xsl:when test="TpoMov[.='R']">
                              <fo:table-row height="5mm" display-align="center">
                                <fo:table-cell>
                                  <fo:block font-size="7pt">&#160;Recargo</fo:block>
                                </fo:table-cell>
                                <fo:table-cell>
                                  <fo:block font-size="7pt" text-align="right">
                                    <xsl:value-of select="format-number(ValorDR, '###.##0', 'peso')"/>&#160;
                                  </fo:block>
                                </fo:table-cell>
                              </fo:table-row>
                            </xsl:when>
                          </xsl:choose>
                        </xsl:for-each>
                        <!-- MONTO EXENTO -->
                        <xsl:choose>
                          <xsl:when test="Documento/Encabezado/Totales/MntExe[.!='']">
                            <fo:table-row height="5mm" display-align="center">
                              <fo:table-cell>
                                <fo:block font-size="7pt">&#160;Monto Exento</fo:block>
                              </fo:table-cell>
                              <fo:table-cell>
                                <fo:block font-size="7pt" text-align="right">
                                  <xsl:value-of select="format-number(Documento/Encabezado/Totales/MntExe, '###.##0', 'peso')"/>&#160;
                                </fo:block>
                              </fo:table-cell>
                            </fo:table-row>
                          </xsl:when>
                        </xsl:choose>
                        <!-- MONTO IVA -->
                        <xsl:choose>
                          <xsl:when test="Documento/Encabezado/Totales/IVA[.!='']">
                            <fo:table-row height="5mm" display-align="center">
                              <fo:table-cell>
                                <fo:block font-size="7pt">&#160;Monto I.V.A. 19%</fo:block>
                              </fo:table-cell>
                              <fo:table-cell>
                                <fo:block font-size="7pt" text-align="right">
                                  <xsl:value-of select="format-number(Documento/Encabezado/Totales/IVA, '###.##0', 'peso')"/>&#160;
                                </fo:block>
                              </fo:table-cell>
                            </fo:table-row>
                          </xsl:when>
                        </xsl:choose>
                        <!-- MONTO CEC -->
                        <xsl:choose>
                          <xsl:when test="Documento/Encabezado/Totales/CredEC[.!='']">
                            <fo:table-row height="5mm" display-align="center">
                              <fo:table-cell>
                                <fo:block font-size="7pt">&#160;CEC</fo:block>
                              </fo:table-cell>
                              <fo:table-cell>
                                <fo:block font-size="7pt" text-align="right">
                                  <xsl:value-of select="format-number(Documento/Encabezado/Totales/CredEC, '###.##0', 'peso')"/>&#160;
                                </fo:block>
                              </fo:table-cell>
                            </fo:table-row>
                          </xsl:when>
                        </xsl:choose>
                        <!-- OTROS IMPUESTOS -->
                        <xsl:for-each select="Documento/Encabezado/Totales/ImptoReten">
                          <xsl:choose>
                            <xsl:when test="TipoImp[.!='']">
                              <fo:table-row height="4mm" display-align="center">
                                <fo:table-cell>
                                  <fo:block font-size="7pt">
                                    &#160;Impuesto&#160;<xsl:value-of select='TipoImp'/>
                                  </fo:block>
                                </fo:table-cell>
                                <fo:table-cell>
                                  <fo:block font-size="7pt" text-align="right">
                                    <xsl:value-of select="format-number(MontoImp, '###.##0', 'peso')"/>&#160;
                                  </fo:block>
                                </fo:table-cell>
                              </fo:table-row>
                            </xsl:when>
                          </xsl:choose>
                        </xsl:for-each>
                        <!-- MONTO TOTAL -->
                        <fo:table-row height="5mm" display-align="center">
                          <fo:table-cell>
                            <fo:block font-size="7pt">&#160;Monto Total</fo:block>
                          </fo:table-cell>
                          <fo:table-cell>
                            <fo:block font-size="7pt" text-align="right">
                              <xsl:value-of select="format-number(Documento/Encabezado/Totales/MntTotal, '###.##0', 'peso')"/>&#160;
                            </fo:block>
                          </fo:table-cell>
                        </fo:table-row>
                        <xsl:choose>
                          <xsl:when test="Exportaciones/Encabezado/Totales/SaldoAnterior[.!='']">
                            <!-- SALDO ANTERIOR -->
                            <fo:table-row height="5mm" display-align="center">
                              <fo:table-cell>
                                <fo:block font-size="7pt">&#160;Saldo Anterior</fo:block>
                              </fo:table-cell>
                              <fo:table-cell>
                                <fo:block font-size="7pt" text-align="right">
                                  <xsl:value-of select="format-number(Documento/Encabezado/Totales/SaldoAnterior, '###.##0', 'peso')"/>&#160;
                                </fo:block>
                              </fo:table-cell>
                            </fo:table-row>
                          </xsl:when>
                        </xsl:choose>
                        <xsl:choose>
                          <xsl:when test="Exportaciones/Encabezado/Totales/VlrPagar[.!='']">
                            <!-- VALOR A PAGAR -->
                            <fo:table-row height="5mm" display-align="center">
                              <fo:table-cell>
                                <fo:block font-size="7pt">&#160;Total a pagar</fo:block>
                              </fo:table-cell>
                              <fo:table-cell>
                                <fo:block font-size="7pt" text-align="right">
                                  <xsl:value-of select="format-number(Documento/Encabezado/Totales/VlrPagar, '###.##0', 'peso')"/>&#160;
                                </fo:block>
                              </fo:table-cell>
                            </fo:table-row>
                            <!-- MONTO NF -->
                          </xsl:when>
                        </xsl:choose>
                        <xsl:choose>
                          <xsl:when test="Documento/Encabezado/Totales/MontoNF[.!='']">
                            <fo:table-row height="5mm" display-align="center">
                              <fo:table-cell>
                                <fo:block font-size="7pt">&#160;Monto no Facturable</fo:block>
                              </fo:table-cell>
                              <fo:table-cell>
                                <fo:block font-size="7pt" text-align="right">
                                  <xsl:value-of select="format-number(Documento/Encabezado/Totales/MontoNF, '###.##0', 'formato')"/>&#160;
                                </fo:block>
                              </fo:table-cell>
                            </fo:table-row>
                          </xsl:when>
                        </xsl:choose>
                      </fo:table-body>
                    </fo:table>
                  </fo:block>
                </fo:table-cell>
              </fo:table-row>
            </fo:table-body>
          </fo:table>
          <xsl:if test="Documento/Encabezado/IdDoc/Cedible">
            <xsl:choose>
              <xsl:when test="$TipoDoc = 33 or $TipoDoc = 34">
                <fo:block font-size="10pt" text-align="right" margin-right="2cm" color="red">CEDIBLE</fo:block>
              </xsl:when>
              <xsl:when test="$TipoDoc = 52">
                <fo:block font-size="10pt" text-align="right" margin-right="2cm" color="red">CEDIBLE CON SU FACTURA</fo:block>
              </xsl:when>
            </xsl:choose>
          </xsl:if>
        </fo:flow>
      </fo:page-sequence>
    </fo:root>
  </xsl:template>
  <!-- format-fecha-all -->
  <xsl:template name="format-fecha-all">
    <xsl:param name="input" select="''"/>
    <xsl:param name="nombre" select="''"/>
    <xsl:param name="separador" select="'-'"/>
    <xsl:variable name="year" select="substring($input, 1, 4)"/>
    <xsl:variable name="mes">
      <xsl:call-template name="dos-digitos">
        <xsl:with-param name="mes-id" select="substring($input, 6, 2)"/>
        <xsl:with-param name="id" select="'mes'"/>
      </xsl:call-template>
    </xsl:variable>
    <xsl:variable name="day">
      <xsl:call-template name="dos-digitos">
        <xsl:with-param name="mes-id" select="substring($input, 9, 2)"/>
        <xsl:with-param name="id" select="'dia'"/>
      </xsl:call-template>
    </xsl:variable>
    <xsl:choose>
      <xsl:when test="$nombre = 'nombre'">
        <xsl:variable name="nom">
          <xsl:call-template name="nombre-mes">
            <xsl:with-param name="tip" select="$mes"/>
          </xsl:call-template>
        </xsl:variable>
        <xsl:value-of select="concat($day, $separador, $nom, $separador, $year)"/>
      </xsl:when>
      <xsl:when test="$nombre = 'corto'">
        <xsl:variable name="nomb">
          <xsl:call-template name="nom-mes">
            <xsl:with-param name="tip" select="$mes"/>
          </xsl:call-template>
        </xsl:variable>
        <xsl:value-of select="concat($day, $separador, $nomb, $separador, $year)"/>
      </xsl:when>
      <xsl:when test="$nombre = 'nombremayus'">
        <xsl:variable name="nomM">
          <xsl:call-template name="nombre-mes-mayus">
            <xsl:with-param name="tip" select="$mes"/>
          </xsl:call-template>
        </xsl:variable>
        <xsl:value-of select="concat($day, $separador, $nomM, $separador, $year)"/>
      </xsl:when>
      <xsl:when test="$nombre = 'mayuscorto'">
        <xsl:variable name="nomMC">
          <xsl:call-template name="nom-mes-mayus">
            <xsl:with-param name="tip" select="$mes"/>
          </xsl:call-template>
        </xsl:variable>
        <xsl:value-of select="concat($day, $separador, $nomMC, $separador, $year)"/>
      </xsl:when>
      <xsl:otherwise>
        <xsl:value-of select="concat($day, $separador, $mes, $separador, $year)"/>
      </xsl:otherwise>
    </xsl:choose>
  </xsl:template>
  <!-- dos-digitos-->
  <xsl:template name="dos-digitos">
    <xsl:param name="mes-id" select="0"/>
    <xsl:param name="id" select="'mes'"/>
    <xsl:choose>
      <xsl:when test="$id ='mes'">
        <!--Para mes-->
        <xsl:choose>
          <xsl:when test="number($mes-id) &gt;= 1 and number($mes-id) &lt;= 12">
            <xsl:value-of select="format-number(number($mes-id), '00')"/>
          </xsl:when>
          <xsl:otherwise>
            <xsl:value-of select="ERR"/>
          </xsl:otherwise>
        </xsl:choose>
      </xsl:when>
      <xsl:otherwise>
        <!--Para dia-->
        <xsl:choose>
          <xsl:when test="$id = 'dia'">
            <xsl:value-of select="format-number(number($mes-id), '00')"/>
          </xsl:when>
          <xsl:otherwise>
            <xsl:value-of select="ERR"/>
          </xsl:otherwise>
        </xsl:choose>
      </xsl:otherwise>
    </xsl:choose>
  </xsl:template>
  <!-- nombre-mes -->
  <xsl:template name="nombre-mes">
    <!-- Esta funcion recibe un nÃºmero y devuelve el nombre del mes que
	     	  le corresponde Se utiliza para pcl principalmente.
	     -->
    <xsl:param name="tip" />
    <xsl:variable name="mes">
      <TipoMes tip='01'>Enero</TipoMes>
      <TipoMes tip='02'>Febrero</TipoMes>
      <TipoMes tip='03'>Marzo</TipoMes>
      <TipoMes tip='04'>Abril</TipoMes>
      <TipoMes tip='05'>Mayo</TipoMes>
      <TipoMes tip='06'>Junio</TipoMes>
      <TipoMes tip='07'>Julio</TipoMes>
      <TipoMes tip='08'>Agosto</TipoMes>
      <TipoMes tip='09'>Septiembre</TipoMes>
      <TipoMes tip='10'>Octubre</TipoMes>
      <TipoMes tip='11'>Noviembre</TipoMes>
      <TipoMes tip='12'>Diciembre</TipoMes>
    </xsl:variable>
    <xsl:variable name="resultado">
      <xsl:for-each select="exsl:node-set($mes)/TipoMes">
        <xsl:if test="@tip = $tip">
          <xsl:value-of select="." />
        </xsl:if>
      </xsl:for-each>
    </xsl:variable>
    <xsl:choose>
      <xsl:when test="$resultado">
        <xsl:value-of select="$resultado" />
      </xsl:when>
      <xsl:otherwise>
        <xsl:value-of select="$tip" />
      </xsl:otherwise>
    </xsl:choose>
  </xsl:template>

  <xsl:template name="nom-mes">
    <!-- Esta funcion recibe 02 y devuelve Feb-->
    <xsl:param name="tip"/>
    <xsl:variable name="mes">
      <TipoMes tip='01'>Ene</TipoMes>
      <TipoMes tip='02'>Feb</TipoMes>
      <TipoMes tip='03'>Mar</TipoMes>
      <TipoMes tip='04'>Abr</TipoMes>
      <TipoMes tip='05'>May</TipoMes>
      <TipoMes tip='06'>Jun</TipoMes>
      <TipoMes tip='07'>Jul</TipoMes>
      <TipoMes tip='08'>Ago</TipoMes>
      <TipoMes tip='09'>Sep</TipoMes>
      <TipoMes tip='10'>Oct</TipoMes>
      <TipoMes tip='11'>Nov</TipoMes>
      <TipoMes tip='12'>Dic</TipoMes>
    </xsl:variable>
    <xsl:variable name="resultado">
      <xsl:for-each select="exsl:node-set($mes)/TipoMes">
        <xsl:if test="@tip = $tip">
          <xsl:value-of select="."/>
        </xsl:if>
      </xsl:for-each>
    </xsl:variable>
    <xsl:choose>
      <xsl:when test="$resultado">
        <xsl:value-of select="$resultado"/>
      </xsl:when>
      <xsl:otherwise>
        <xsl:value-of select="$tip"/>
      </xsl:otherwise>
    </xsl:choose>
  </xsl:template>

  <xsl:template name="nombre-mes-mayus">
    <!-- Esta funcion recibe un nÃºmero y devuelve el nombre del mes que
	     	  le corresponde Se utiliza para pcl principalmente.     -->
    <xsl:param name="tip"/>
    <xsl:variable name="mes">
      <TipoMes tip='01'>ENERO</TipoMes>
      <TipoMes tip='02'>FEBRERO</TipoMes>
      <TipoMes tip='03'>MARZO</TipoMes>
      <TipoMes tip='04'>ABRIL</TipoMes>
      <TipoMes tip='05'>MAYO</TipoMes>
      <TipoMes tip='06'>JUNIO</TipoMes>
      <TipoMes tip='07'>JULIO</TipoMes>
      <TipoMes tip='08'>AGOSTO</TipoMes>
      <TipoMes tip='09'>SEPTIEMBRE</TipoMes>
      <TipoMes tip='10'>OCTUBRE</TipoMes>
      <TipoMes tip='11'>NOVIEMBRE</TipoMes>
      <TipoMes tip='12'>DICIEMBRE</TipoMes>
    </xsl:variable>
    <xsl:variable name="resultado">
      <xsl:for-each select="exsl:node-set($mes)/TipoMes">
        <xsl:if test="@tip = $tip">
          <xsl:value-of select="."/>
        </xsl:if>
      </xsl:for-each>
    </xsl:variable>
    <xsl:choose>
      <xsl:when test="$resultado">
        <xsl:value-of select="$resultado"/>
      </xsl:when>
      <xsl:otherwise>
        <xsl:value-of select="$tip"/>
      </xsl:otherwise>
    </xsl:choose>
  </xsl:template>

  <xsl:template name="nom-mes-mayus">
    <!-- Esta funcion recibe 02 y devuelve FEB-->
    <xsl:param name="tip"/>
    <xsl:variable name="mes">
      <TipoMes tip='01'>- ENE</TipoMes>
      <TipoMes tip='02'>- FEB</TipoMes>
      <TipoMes tip='03'>- MAR</TipoMes>
      <TipoMes tip='04'>- ABR</TipoMes>
      <TipoMes tip='05'>- MAY</TipoMes>
      <TipoMes tip='06'>- JUN</TipoMes>
      <TipoMes tip='07'>- JUL</TipoMes>
      <TipoMes tip='08'>- AGO</TipoMes>
      <TipoMes tip='09'>- SEP</TipoMes>
      <TipoMes tip='10'>- OCT</TipoMes>
      <TipoMes tip='11'>- NOV</TipoMes>
      <TipoMes tip='12'>- DIC</TipoMes>
    </xsl:variable>
    <xsl:variable name="resultado">
      <xsl:for-each select="exsl:node-set($mes)/TipoMes">
        <xsl:if test="@tip = $tip">
          <xsl:value-of select="."/>
        </xsl:if>
      </xsl:for-each>
    </xsl:variable>
    <xsl:choose>
      <xsl:when test="$resultado">
        <xsl:value-of select="$resultado"/>
      </xsl:when>
      <xsl:otherwise>
        <xsl:value-of select="$tip"/>
      </xsl:otherwise>
    </xsl:choose>
  </xsl:template>

  <xsl:template name="Forma-Pago">
    <xsl:param name="Forma-Pago"/>
    <xsl:variable name="respDTE">
      <codigor Forma-Pago='1'>Contado</codigor>
      <codigor Forma-Pago='2'>Cr&#233;dito</codigor>
      <codigor Forma-Pago='3'>Sin Costo (entrega gratuita)</codigor>
    </xsl:variable>
    <xsl:variable name="respuesta">
      <xsl:for-each select="exsl:node-set($respDTE)/codigor">
        <xsl:if test="@Forma-Pago = $Forma-Pago">
          <xsl:value-of select="."/>
        </xsl:if>
      </xsl:for-each>
    </xsl:variable>
    <xsl:value-of select="$respuesta"/>
  </xsl:template>

  <xsl:template name="NaN">
    <xsl:param name="number"/>
    <xsl:choose>
      <xsl:when test="string($number) != 'NaN'">
        <xsl:value-of select="$number"/>
      </xsl:when>
      <xsl:otherwise>
        <xsl:value-of select="' '"/>
      </xsl:otherwise>
    </xsl:choose>
  </xsl:template>

  <xsl:template name="divide_en_lineas">
    <!-- template para separar en líneas Cristian Acuna, modificada por EPS para que tome salto de linea del final-->
    <xsl:param name="val"/>
    <xsl:param name="c1"/>
    <xsl:choose>
      <xsl:when test="string-length(substring-after($val, $c1)) = 0">
        <xsl:choose>
          <xsl:when test="contains($val, $c1)">
            <xsl:value-of select="substring-before($val, $c1)"/>
          </xsl:when>
          <xsl:otherwise>
            <xsl:value-of select="$val"/>
          </xsl:otherwise>
        </xsl:choose>
      </xsl:when>
      <xsl:otherwise>
        <xsl:value-of select="substring-before($val, $c1)"/>
        <fo:block font-size="6pt">
          &#160;
          <xsl:call-template name="divide_en_lineas">
            <xsl:with-param name="val">
              &#160;<xsl:value-of select="substring-after($val, $c1)"/>
            </xsl:with-param>
            <xsl:with-param name="c1">
              <xsl:value-of select="$c1"/>
            </xsl:with-param>
          </xsl:call-template>
        </fo:block>
      </xsl:otherwise>
    </xsl:choose>
  </xsl:template>

  <xsl:template name="formatea-number">
    <xsl:param name="val"/>
    <xsl:param name="format-string" select="'.##0'"/>
    <xsl:param name="locale" select="'peso'"/>
    <xsl:variable name="result" select="format-number($val, $format-string, $locale)"/>
  </xsl:template>

  <xsl:template name="letrasMayu">
    <xsl:param name="val"/>
    <xsl:param name="format-string" select="'###'"/>
    <xsl:param name="locale" select="'mayus'"/>
    <xsl:variable name="result" select="format-mayus($val, $format-string, $locale)"/>
  </xsl:template>

  <xsl:template name="TpoDoc">
    <xsl:param name="TipoDoc1"/>
    <xsl:variable name="respDTE">
      <codigor TipoDoc1='32'>FACTURA VENTA DE BIENES Y SERVICIOS</codigor>
      <codigor TipoDoc1='33'>FACTURA ELECTRÓNICA</codigor>
      <codigor TipoDoc1='34'>FACTURA EXENTA ELECTRÓNICA</codigor>
      <codigor TipoDoc1='35'>BOLETA</codigor>
      <codigor TipoDoc1='38'>BOLETA EXENTA</codigor>
      <codigor TipoDoc1='39'>BOLETA ELECTRÓNICA</codigor>
      <codigor TipoDoc1='40'>LIQUIDACIÓN ELECTRÓNICA</codigor>
      <codigor TipoDoc1='41'>BOLETA EXENTA ELECTRÓNICA</codigor>
      <codigor TipoDoc1='43'>LIQUIDACIÓN FACTURA ELECTRÓNICA</codigor>
      <codigor TipoDoc1='45'>FACTURA DE COMPRA</codigor>
      <codigor TipoDoc1='46'>FACTURA DE COMPRA ELECTRÓNICA</codigor>
      <codigor TipoDoc1='50'>GUÍA DE DESPACHO</codigor>
      <codigor TipoDoc1='52'>GUÍA DE DESPACHO ELECTRÓNICA</codigor>
      <codigor TipoDoc1='55'>NOTA DE DÉBITO</codigor>
      <codigor TipoDoc1='56'>NOTA DE DÉBITO ELECTRÓNICA</codigor>
      <codigor TipoDoc1='60'>NOTA DE CRÉDITO</codigor>
      <codigor TipoDoc1='61'>NOTA DE CRÉDITO ELECTRÓNICA</codigor>
      <codigor TipoDoc1='103'>LIQUIDACIÓN</codigor>
      <codigor TipoDoc1='110'>FACTURA DE EXPORTACIÓN ELECTRÓNICA</codigor>
      <codigor TipoDoc1='111'>NOTA DE DÉBITO EXPORTACIÓN ELECTRÓNICA</codigor>
      <codigor TipoDoc1='112'>NOTA DE CRÉDITO EXPORTACIÓN ELECTRÓNICA</codigor>
      <codigor TipoDoc1='801'>ORDEN DE COMPRA</codigor>
      <codigor TipoDoc1='802'>NOTA DE PEDIDO</codigor>
      <codigor TipoDoc1='803'>CONTRATO</codigor>
      <codigor TipoDoc1='804'>RESOLUCIÓN</codigor>
      <codigor TipoDoc1='805'>PROCESO CHILE COMPRA</codigor>
      <codigor TipoDoc1='806'>FICHA CHILE CMPRA</codigor>
      <codigor TipoDoc1='807'>DUS</codigor>
      <codigor TipoDoc1='811'>CARTA PORTE</codigor>
      <codigor TipoDoc1='812'>RESOLUCIÓN SNA</codigor>
      <codigor TipoDoc1='813'>PASAPORTE</codigor>
      <codigor TipoDoc1='814'>CERTIFICADO DEPÓSITO BOLSA</codigor>
      <codigor TipoDoc1='815'>VALE PRENDA BOLSA</codigor>
      <codigor TipoDoc1='I99'>INTERNO</codigor>
    </xsl:variable>
    <xsl:variable name="respuesta">
      <xsl:for-each select="exsl:node-set($respDTE)/codigor">
        <xsl:if test="@TipoDoc1 = $TipoDoc1">
          <xsl:value-of select="."/>
        </xsl:if>
      </xsl:for-each>
    </xsl:variable>
    <xsl:value-of select="$respuesta"/>
  </xsl:template>

  <xsl:template name="Traslado">
    <xsl:param name="CodTraslado"/>
    <xsl:variable name="respDTE">
      <codigor CodTraslado='1'>Constituye venta</codigor>
      <codigor CodTraslado='2'>Venta por efectuar</codigor>
      <codigor CodTraslado='3'>Consignaciones</codigor>
      <codigor CodTraslado='4'>Entrega gratuita</codigor>
      <codigor CodTraslado='5'>Traslado interno</codigor>
      <codigor CodTraslado='6'>Otro traslado no venta</codigor>
      <codigor CodTraslado='7'>Guia de devolucion</codigor>
    </xsl:variable>
    <xsl:variable name="respuesta">
      <xsl:for-each select="exsl:node-set($respDTE)/codigor">
        <xsl:if test="@CodTraslado = $CodTraslado">
          <xsl:value-of select="."/>
        </xsl:if>
      </xsl:for-each>
    </xsl:variable>
    <xsl:value-of select="$respuesta"/>
  </xsl:template>

  <!-- <xsl:template name="formatea-number">
		  <xsl:param name="val"/>
		  <xsl:param name="format-string" select="'.##0'"/>
		  <xsl:param name="locale" select="'peso'"/>
		  <xsl:variable name="result" select="format-number($val, $format-string, $locale)"/>
	 </xsl:template> -->

  <xsl:template name="Rutformat">
    <xsl:param name="input"/>
    <xsl:variable name="rut" select="substring-before($input, '-')"/>
    <xsl:variable name="last" select="substring($rut,string-length($rut)-2,3)"/>
    <xsl:variable name="middle" select="substring($rut,string-length($rut)-5,3)"/>
    <xsl:variable name="first">
      <xsl:choose>
        <xsl:when test="string-length($rut)=7">
          <xsl:value-of select="substring($rut,1,1)"/>
        </xsl:when>
        <xsl:when test="string-length($rut)=8">
          <xsl:value-of select="substring($rut,1,2)"/>
        </xsl:when>
        <xsl:when test="string-length($rut)=9">
          <xsl:value-of select="substring($rut,1,3)"/>
        </xsl:when>
      </xsl:choose>
    </xsl:variable>
    <xsl:variable name="dv" select="substring-after($input, '-')"/>
    <xsl:value-of select="concat($first,'.',$middle,'.',$last, '-', $dv)"/>
  </xsl:template>

</xsl:stylesheet>