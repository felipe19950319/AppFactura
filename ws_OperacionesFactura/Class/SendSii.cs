using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Web;
using System.Xml;
using System.Xml.Linq;
using ws_OperacionesFactura.Class;

public class EnvioSii
 {
        public string Token { get; set; }
        public string RutEmisor { get; set; }
        public string RutEmpresa { get; set; }
        public string RutaXml { get; set; }

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
        secuencia.Append("Content-Disposition: form-data; name=\"archivo\"; filename=\"" + envioSii.RutaXml + "\"\r\n");
        secuencia.Append("Content-Type: text/xml\r\n");
        secuencia.Append("\r\n");
        ////
        //// Lea el documento xml que se va a enviar al SII
        XDocument xdocument = XDocument.Load(envioSii.RutaXml, LoadOptions.PreserveWhitespace);
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

    public string GetToken(string PathCertificate, string PwdCetificate)
    {
        //obtenemos token 
        CrSeedService SeedService = new CrSeedService();
        string Response = SeedService.getSeed();
        XmlDocument xdoc = new XmlDocument();
        xdoc.LoadXml(Response);

        X509Certificate2 cert = new X509Certificate2(PathCertificate, PwdCetificate);

        string SignedSeed = SignSeed(xdoc, cert).InnerXml;
        ////>2CWDHDQ4MFB9Q
        //// Luego asigne el valor al metodo GetToken()
        GetTokenFromSeedService gt = new GetTokenFromSeedService();
        string SignSeedResponse = gt.getToken(SignedSeed);

        return "";

    }

    public static XmlDocument SignSeed(XmlDocument doc, X509Certificate2 certificado)
    {
        doc.PreserveWhitespace = false;
        SignedXml signedXml = new SignedXml(doc);
        signedXml.SigningKey = certificado.PrivateKey;
        Signature XMLSignature = signedXml.Signature;
        Reference reference = new Reference("");
        XmlDsigEnvelopedSignatureTransform env = new XmlDsigEnvelopedSignatureTransform();
        reference.AddTransform(env);
        XMLSignature.SignedInfo.AddReference(reference);
        KeyInfo keyInfo = new KeyInfo();
        keyInfo.AddClause(new RSAKeyValue((RSA)certificado.PrivateKey));
        keyInfo.AddClause(new KeyInfoX509Data(certificado));
        XMLSignature.KeyInfo = keyInfo;
        signedXml.ComputeSignature();
        XmlElement xmlDigitalSignature = signedXml.GetXml();
        doc.DocumentElement.AppendChild(doc.ImportNode(xmlDigitalSignature, true));

        if (doc.FirstChild is XmlDeclaration)
        {
            doc.RemoveChild(doc.FirstChild);
        }

        return doc;
    }
}


