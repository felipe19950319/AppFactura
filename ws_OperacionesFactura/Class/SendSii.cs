using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Security;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Web;
using System.Xml;
using System.Xml.Linq;
//using ws_OperacionesFactura.Certificacion;

public class EnvioSii
{
    public string Token { get; set; }
    public string RutEmisor { get; set; }
    public string RutEmpresa { get; set; }
    public XDocument Xml { get; set; }
    public string NameXml { get; set; }

        public EnvioSii() { }
}

public class SiiUtilities
{
    public string SendDoc(EnvioSii envioSii)
    {
        //string rutEmisor = "766551424";
        //string rutEmpresa = "766551424";
        StringBuilder secuencia = new StringBuilder();

        //  string nombreArchivo = @"C:\tyscom xml\xml\documentos\DTE_FIRMADO.xml";
        string pRutEmisor = envioSii.RutEmisor.Substring(0, (envioSii.RutEmisor.Length - 1));
        string pDigEmisor = envioSii.RutEmisor.Substring(envioSii.RutEmisor.Length - 1);
        string pRutEmpresa = envioSii.RutEmpresa.Substring(0, (envioSii.RutEmpresa.Length - 1));
        string pDigEmpresa = envioSii.RutEmpresa.Substring(envioSii.RutEmpresa.Length - 1);

        ////
        //// Cree el header del request a enviar al SII
        //// segun la información solicitada del SII
        secuencia.Append("--7d23e2a11301c4\r\n");
        secuencia.Append("Content-Disposition: form-data; name=\"rutSender\"\r\n");
        secuencia.Append("\r\n");
        secuencia.Append(pRutEmisor + "\r\n");
        secuencia.Append("--7d23e2a11301c4\r\n");
        secuencia.Append("Content-Disposition: form-data; name=\"dvSender\"\r\n");
        secuencia.Append("\r\n");
        secuencia.Append(pDigEmisor + "\r\n");
        secuencia.Append("--7d23e2a11301c4\r\n");
        secuencia.Append("Content-Disposition: form-data; name=\"rutCompany\"\r\n");
        secuencia.Append("\r\n");
        secuencia.Append(pRutEmpresa + "\r\n");
        secuencia.Append("--7d23e2a11301c4\r\n");
        secuencia.Append("Content-Disposition: form-data; name=\"dvCompany\"\r\n");
        secuencia.Append("\r\n");
        secuencia.Append(pDigEmpresa + "\r\n");
        secuencia.Append("--7d23e2a11301c4\r\n");
        secuencia.Append("Content-Disposition: form-data; name=\"archivo\"; filename=\"" + envioSii.NameXml + "\"\r\n");
        secuencia.Append("Content-Type: text/xml\r\n");
        secuencia.Append("\r\n");
        ////
        //// Lea el documento xml que se va a enviar al SII
        XDocument xdocument = envioSii.Xml;// XDocument.Load(envioSii.RutaXml, LoadOptions.PreserveWhitespace);
        ////
        //// Cargue el documento en el objeto secuencia
        secuencia.Append("<?xml version=\"1.0\" encoding=\"ISO-8859-1\"?>");
        secuencia.Append(xdocument.ToString(SaveOptions.DisableFormatting));
        secuencia.Append("\r\n\r\n--7d23e2a11301c4--");
        //
        // Aqui se configura el request que hace la solicitud al SII
        #region CONFIGURACION DE REQUEST
        ////
        //// Defina que ambiente utilizar.
        //// Certific "https://maullin.sii.cl/cgi_dte/UPL/DTEUpload";
        string pUrl = "https://maullin.sii.cl/cgi_dte/UPL/DTEUpload";
        ////
        //// Cree los parametros del header.
        //// Token debe ser el valor asignado por el SII
        string pMethod = "POST";
        string pAccept = "image/gif, image/x-xbitmap, image/jpeg, image/pjpeg,application/vnd.ms-powerpoint, application/ms-excel,application/msword, */*";
        string pReferer = "www.tyscom.cl";
        string pToken = "TOKEN={0}";
        ////
        //// Cree un nuevo request para iniciar el proceso
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(pUrl);
        request.Method = pMethod;
        request.Accept = pAccept;
        request.Referer = pReferer;

        ////
        //// Agregar el content-type
        request.ContentType = "multipart/form-data: boundary=7d23e2a11301c4";
        request.ContentLength = secuencia.Length;

        ////
        //// Defina manualmente los headers del request
        request.Headers.Add("Accept-Language", "es-cl");
        request.Headers.Add("Accept-Encoding", "gzip, deflate");
        request.Headers.Add("Cache-Control", "no-cache");
        request.Headers.Add("Cookie", string.Format(pToken, envioSii.Token));

        ////
        //// Defina el user agent
        request.UserAgent = "Mozilla/4.0 (compatible; PROG 1.0; Windows NT 5.0; YComp 5.0.2.4)";
        request.KeepAlive = true;
        request.Timeout = 20000;

        #endregion
        #region ESCRIBE LA DATA NECESARIA
        ////
        //// Recupere el streamwriter para escribir la secuencia
        try
        {
            using (StreamWriter sw = new StreamWriter(request.GetRequestStream(), Encoding.GetEncoding("ISO-8859-1")))
            {
                sw.Write(secuencia.ToString());
            }

        }
        catch (WebException ex)
        {
            string ss = ex.Message;
        }
        catch (ProtocolViolationException ex)
        {
            string ss = ex.Message;
        }
        catch (ObjectDisposedException ex)
        {
            string ss = ex.Message;
        }
        catch (InvalidOperationException ex)
        {
            string ss = ex.Message;
        }
        catch (NotSupportedException ex)
        {
            string ss = ex.Message;
        }
        catch (Exception ex)
        {
            ////
            //// Error en el metodo
            //// Error del formato del envio
            string ss = ex.Message;

        }
        #endregion


        #region ENVIA Y SOLICITA RESPUESTA

        //// Defina donde depositar el resultado
        string respuestaSii = string.Empty;

        ////
        //// Recupere la respuesta del sii
        using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
        {
            using (StreamReader sr = new StreamReader(response.GetResponseStream()))
            {
                respuestaSii = sr.ReadToEnd().Trim();
                Console.WriteLine(respuestaSii);
            }

        }

        ////
        //// Hay respuesta?
        if (string.IsNullOrEmpty(respuestaSii))
            throw new ArgumentNullException("Respuesta del SII es null");


        ////
        //// Interprete la respuesta del SII.
        //// respuestaSii contiene la respuesta del SII acerca del envio en formato XML
        return respuestaSii;


        #endregion
    }

    

}



public class ConexionSII
{
    private string RespuestaSemilla, RespuestaToken, Semi_Firmada, Firmada;
    private XmlDocument SemillaXml = new XmlDocument();
    private XmlDocument TokenXml = new XmlDocument();
    private XmlDocument SemillaFirmada = new XmlDocument();
    private XmlNode NodoSemilla, NodoEstadoSemilla, NodoToken, NodoEstadoToken, NodoGlosaToken;
    private ws_OperacionesFactura.Certificacion.CrSeedService pidosemillaCerti = new ws_OperacionesFactura.Certificacion.CrSeedService();
    private ws_OperacionesFactura.Certificacion.GetTokenFromSeedService pidotokenCerti = new ws_OperacionesFactura.Certificacion.GetTokenFromSeedService();
    private ws_OperacionesFactura.Produccion.CrSeedService pidosemillaProd = new ws_OperacionesFactura.Produccion.CrSeedService();
    private ws_OperacionesFactura.Produccion.GetTokenFromSeedService pidotokenProd = new ws_OperacionesFactura.Produccion.GetTokenFromSeedService();

    public string PidoSemillaToken(X509Certificate2 cert,string Ambiente)
    {
        try
        {
            if (Ambiente == "CERTI")
            {
                RespuestaSemilla = pidosemillaCerti.getSeed();
            }
            if (Ambiente == "PROD")
            {
                RespuestaSemilla = pidosemillaProd.getSeed();
            }

            SemillaXml.LoadXml(RespuestaSemilla);
            NodoSemilla = SemillaXml.SelectSingleNode("//SEMILLA");
            NodoEstadoSemilla = SemillaXml.SelectSingleNode("//ESTADO");
            // saco valor semilla del xml
            var Semilla = NodoSemilla.ChildNodes.Item(0).InnerText;
            // saco valor estado semilla del xml
            var EstadoSemilla = NodoEstadoSemilla.ChildNodes.Item(0).InnerText;
            // FirmarSemilla
            Semi_Firmada = FirmarSemilla(Semilla, cert);   // voy a funcion firmarsemilla con dos parametros, valor semilla y nombre de certificado
            // region Recuperar TOKEN
            // Suponiendo que el objeto XmlDocument ( XMLDOM ) contenga la semilla firmada, esta debería ser la forma de recuperar
            // el valor string.
            SemillaFirmada.LoadXml(Semi_Firmada);    // convierto el string de semilla firmada en un documento xml
            Firmada = SemillaFirmada.OuterXml;            // extraigo solo los valores string para pasar al metodo getToken
            if (Ambiente == "CERTI")
            {
                RespuestaToken = pidotokenCerti.getToken(Firmada);
            }
            if (Ambiente == "PROD")
            {
                RespuestaToken = pidotokenProd.getToken(Firmada);
            }

            TokenXml.LoadXml(RespuestaToken);
            NodoToken = TokenXml.SelectSingleNode("//TOKEN");
            NodoEstadoToken = TokenXml.SelectSingleNode("//ESTADO");
            NodoGlosaToken = TokenXml.SelectSingleNode("//GLOSA");
            // saco valor token del xml
            var Token = NodoToken.ChildNodes.Item(0).InnerText;
            // saco valor estado token del xml
            var EstadoToken = NodoEstadoToken.ChildNodes.Item(0).InnerText;
            // saco valor glosa token del xml
            var GlosaToken = NodoGlosaToken.ChildNodes.Item(0).InnerText;

       
            return Token;
        }
        catch (Exception ex)
        {
            return null;
        }// Exit Function
    }

    // Firmo la semilla para poder validarla en el SII
    public string FirmarSemilla(string seed, X509Certificate2 cert)
    {
        string resultado = null;
        string body;

        // Firmo la semilla.
        try
        {
            body = "";  // blanqueo para asegurar el string desde cero
            // saco los ceros iniciales a la semilla
            body = String.Format("<getToken><item><Semilla>{0}</Semilla></item></getToken>", double.Parse(seed).ToString());
            // esta linea no esta en el codigo de Marcelo Rojas hecha en c# sin ella no funciona la autentificación con vb.net
            //body = "<?xml version=\"1.0\"?><getToken><item><Semilla>" + body + "</Semilla></item></getToken>";
            resultado = FirmarDocumentoSemilla(body, cert);
            return resultado;
        }
        catch (Exception ex)
        {
            
            resultado = null;
            return resultado;
        }
    }

    // Firmar el documento xml semilla
    public string FirmarDocumentoSemilla(string documento, X509Certificate2 certificado)
    {     
        IntPtr pCertContext = IntPtr.Zero;  
        XmlDocument doc = null;
        SignedXml signedXml = null/* TODO Change to default(_) if this is not a reference type */;
        Reference reference = null/* TODO Change to default(_) if this is not a reference type */;   
        XmlDsigEnvelopedSignatureTransform env = null/* TODO Change to default(_) if this is not a reference type */;
        KeyInfo keyInfo = null/* TODO Change to default(_) if this is not a reference type */;
        XmlElement xmlDigitalSignature = null;

        Signature XMLSignature;

        try
        {
            // Creo un nuevo documento xml y defino sus caracteristicas
            doc = new XmlDocument();
            doc.PreserveWhitespace = false;
            // MsgBox(documento)
            doc.LoadXml(documento);

            // Creo el objeto XMLSignature.
            signedXml = new SignedXml(doc);

            // Agrego la clave privada al objeto xmlSignature.
            signedXml.SigningKey = certificado.PrivateKey;

            // Obtengo el objeto signature desde el objeto SignedXml.
            XMLSignature = signedXml.Signature;

            // Creo una referencia al documento que va a firmarse  'si la referencia es "" se firmara todo el documento
            reference = new Reference();
            reference.Uri = "";

            // Add an enveloped transformation to the reference.
            env = new XmlDsigEnvelopedSignatureTransform();
            reference.AddTransform(env);

            // Agrego el objeto referenciado al objeto firma.
            signedXml.AddReference(reference);

            // Agregue RSAKeyValue KeyInfo  ( requerido para el SII ).
            keyInfo = new KeyInfo();
            keyInfo.AddClause(new RSAKeyValue((RSA)certificado.PrivateKey));

            // Agregar información del certificado x509
            keyInfo.AddClause(new KeyInfoX509Data(certificado));

            // Agregar KeyInfo al objeto Signature 
            XMLSignature.KeyInfo = keyInfo;

            // Creo la firma
            signedXml.ComputeSignature();

            // Recupero la representacion xml de la firma
            xmlDigitalSignature = signedXml.GetXml();

            // Agrego la representacion xml de la firma al documento xml
            doc.DocumentElement.AppendChild(doc.ImportNode(xmlDigitalSignature, true));

            // Limpio el documento xml de la declaracion xml ( Opcional, pero para nuestro proceso es valido  )
            if (doc.FirstChild is XmlDeclaration)
                doc.RemoveChild(doc.FirstChild);
            return doc.InnerXml;
        }
        catch (Exception e)
        {

            return null;
        }
    }
}



