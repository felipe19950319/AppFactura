using Proxys.Certificacion;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;
using System.Web;
using System.Xml;
using WcfService_factura.clases;

namespace WcfService_factura.Cn_proxy_Upload
{
    public class Cn_Metodos
    {
        //
        static X509Certificate2 GetCertFromFile(string certPath, string certPassword)
        {
            X509Certificate2 cert = new X509Certificate2();
            cert.Import(certPath, certPassword, X509KeyStorageFlags.PersistKeySet);

            return cert;
        }
        public static void CallHttps()
        {
            Console.WriteLine("Enter full path to certificate file (.pfx)");
            string certFile = Console.ReadLine();
            Console.WriteLine("Enter password for the certificate");
            string certPassword = Console.ReadLine();
            Console.WriteLine();


            string url = "https://maullin.sii.cl/cvc_cgi/dte/ce_empresas_dwnld";
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            req.ClientCertificates.Add(GetCertFromFile(certFile, certPassword));
            HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
            using (var readStream = new StreamReader(resp.GetResponseStream()))
            {
                Console.WriteLine(readStream.ReadToEnd());
            }

            Console.ReadLine();
        }

        public string Recupera_Token()
        {
            //string rutaseed = "c:/tyscom xml/seed/Seed.xml";
            CrSeedService maullin = new CrSeedService();
            string respuesta = maullin.getSeed();
            ///obtener semilla
            ///obtenertoken y firmar!
            XmlDocument xmld = new XmlDocument();
            xmld.LoadXml(respuesta);
           string seed =xmld.InnerXml.ToString();

            DataSet ds = new DataSet();
            ds.ReadXml(XmlReader.Create(new StringReader(seed)));
            string codeSeed = ds.Tables["RESP_BODY"].Rows[0][0].ToString();
            string body = string.Format("<gettoken><item><Semilla>" + codeSeed + "</Semilla></item></gettoken>");
            xmld.LoadXml(body);
           seed= xmld.InnerXml.ToString();
            /////
            //datos estaticos de prueba !
            string a = @"C:\tyscom xml\certificados\fabian.pfx";
            string c = "xmay3187";
            X509Certificate2 cert = new X509Certificate2(a, c);
            Metodos m = new Metodos();

            string algo = firmarDocumentoSemilla(seed, cert);
            xmld.LoadXml(algo);
        //    xmld.Save(rutaseed);

            string signedSeed = algo;
            ////>2CWDHDQ4MFB9Q
            //// Luego asigne el valor al metodo GetToken()
            GetTokenFromSeedService gt = new Proxys.Certificacion.GetTokenFromSeedService();
            string valorRespuesta = gt.getToken(signedSeed);

            //Console.WriteLine(algo);
            Console.WriteLine(valorRespuesta);

            XmlDocument token = new XmlDocument();
            //retorna token     
            token.LoadXml(valorRespuesta);

            valorRespuesta = token.GetElementsByTagName("TOKEN")[0].OuterXml;
            valorRespuesta = valorRespuesta.Replace("<TOKEN>", string.Empty);
            valorRespuesta = valorRespuesta.Replace("</TOKEN>", string.Empty);
            return valorRespuesta;

        }
        public static string firmarDocumentoSemilla(string documento, X509Certificate2 certificado)
        {

            ////
            //// Cree un nuevo documento xml y defina sus caracteristicas
            XmlDocument doc = new XmlDocument();
            doc.PreserveWhitespace = false;
            doc.LoadXml(documento);
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

            return doc.InnerXml;




        }
    }
}