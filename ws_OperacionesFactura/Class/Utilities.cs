using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Web;
using System.Xml;
using System.Xml.Linq;

namespace ws_OperacionesFactura
{
    public class Utilities
    {

        public static string Encryption(string strText)
        {
            var publicKey = "<RSAKeyValue><Modulus>21wEnTU+mcD2w0Lfo1Gv4rtcSWsQJQTNa6gio05AOkV/Er9w3Y13Ddo5wGtjJ19402S71HUeN0vbKILLJdRSES5MHSdJPSVrOqdrll/vLXxDxWs/U0UT1c8u6k/Ogx9hTtZxYwoeYqdhDblof3E75d9n2F0Zvf6iTb4cI7j6fMs=</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";

            var testData = Encoding.UTF8.GetBytes(strText);

            using (var rsa = new RSACryptoServiceProvider(1024))
            {
                try
                {
                    // client encrypting data with public key issued by server                    
                    rsa.FromXmlString(publicKey.ToString());

                    var encryptedData = rsa.Encrypt(testData, true);

                    var base64Encrypted = Convert.ToBase64String(encryptedData);

                    return base64Encrypted;
                }
                finally
                {
                    rsa.PersistKeyInCsp = false;
                }
            }
        }
        public static string Decryption(string strText)
        {
            var privateKey = "<RSAKeyValue><Modulus>21wEnTU+mcD2w0Lfo1Gv4rtcSWsQJQTNa6gio05AOkV/Er9w3Y13Ddo5wGtjJ19402S71HUeN0vbKILLJdRSES5MHSdJPSVrOqdrll/vLXxDxWs/U0UT1c8u6k/Ogx9hTtZxYwoeYqdhDblof3E75d9n2F0Zvf6iTb4cI7j6fMs=</Modulus><Exponent>AQAB</Exponent><P>/aULPE6jd5IkwtWXmReyMUhmI/nfwfkQSyl7tsg2PKdpcxk4mpPZUdEQhHQLvE84w2DhTyYkPHCtq/mMKE3MHw==</P><Q>3WV46X9Arg2l9cxb67KVlNVXyCqc/w+LWt/tbhLJvV2xCF/0rWKPsBJ9MC6cquaqNPxWWEav8RAVbmmGrJt51Q==</Q><DP>8TuZFgBMpBoQcGUoS2goB4st6aVq1FcG0hVgHhUI0GMAfYFNPmbDV3cY2IBt8Oj/uYJYhyhlaj5YTqmGTYbATQ==</DP><DQ>FIoVbZQgrAUYIHWVEYi/187zFd7eMct/Yi7kGBImJStMATrluDAspGkStCWe4zwDDmdam1XzfKnBUzz3AYxrAQ==</DQ><InverseQ>QPU3Tmt8nznSgYZ+5jUo9E0SfjiTu435ihANiHqqjasaUNvOHKumqzuBZ8NRtkUhS6dsOEb8A2ODvy7KswUxyA==</InverseQ><D>cgoRoAUpSVfHMdYXW9nA3dfX75dIamZnwPtFHq80ttagbIe4ToYYCcyUz5NElhiNQSESgS5uCgNWqWXt5PnPu4XmCXx6utco1UVH8HGLahzbAnSy6Cj3iUIQ7Gj+9gQ7PkC434HTtHazmxVgIR5l56ZjoQ8yGNCPZnsdYEmhJWk=</D></RSAKeyValue>";

            var testData = Encoding.UTF8.GetBytes(strText);

            using (var rsa = new RSACryptoServiceProvider(1024))
            {
                try
                {
                    var base64Encrypted = strText;

                    // server decrypting data with private key                    
                    rsa.FromXmlString(privateKey);

                    var resultBytes = Convert.FromBase64String(base64Encrypted);
                    var decryptedBytes = rsa.Decrypt(resultBytes, true);
                    var decryptedData = Encoding.UTF8.GetString(decryptedBytes);
                    return decryptedData.ToString();
                }
                finally
                {
                    rsa.PersistKeyInCsp = false;
                }
            }
        }
        public static bool VerifyPassword(byte[] Certificate, string password)
        {
            try
            {
                var certificate = new X509Certificate2(Certificate, password);
            }
            catch (CryptographicException ex)
            {
                if ((ex.HResult & 0xFFFF) == 0x56)
                {
                    return false;
                };

                throw;
            }

            return true;
        }
        static bool verbose = false;
        public string GetNodeValue(string xml_string, string NameItem)
        {
            XmlTextReader reader = new XmlTextReader(new StringReader(xml_string));
            string semilla = "";
            string nombreNodo = "";
            reader.WhitespaceHandling = WhitespaceHandling.None;
            while ((reader.Read()))
            {
                switch (reader.NodeType)
                {
                    case XmlNodeType.Element:
                        nombreNodo = reader.Name;
                        if (reader.HasAttributes)
                        {
                            while (reader.MoveToNextAttribute())
                            {
                                nombreNodo = reader.Name;
                            }
                        }
                        break;
                    case XmlNodeType.Text:
                        if (nombreNodo == NameItem)
                        {
                            semilla = reader.Value;
                        }
                        break;
                }
            }
            return semilla;
        }
        public string CreateFRMT(string DD, string pk)
        {
            ASCIIEncoding ByteConverter = new ASCIIEncoding();
            byte[] bytesStrDD = ByteConverter.GetBytes(DD);
            byte[] HashValue = new SHA1CryptoServiceProvider().ComputeHash(bytesStrDD);
            RSACryptoServiceProvider rsa = crearRsaDesdePEM(pk);
            byte[] bytesSing = rsa.SignHash(HashValue, "SHA1");
            string FRMT1 = Convert.ToBase64String(bytesSing);
            return FRMT1;
        }
        public static RSACryptoServiceProvider DecodeRSAPrivateKey(byte[] privkey)
        {
            byte[] MODULUS, E, D, P, Q, DP, DQ, IQ;


            // --------- Set up stream to decode the asn.1 encoded RSA private key ------
            MemoryStream mem = new MemoryStream(privkey);
            BinaryReader binr = new BinaryReader(mem);  //wrap Memory Stream with BinaryReader for easy reading
            byte bt = 0;
            ushort twobytes = 0;
            int elems = 0;
            try
            {
                twobytes = binr.ReadUInt16();
                if (twobytes == 0x8130) //data read as little endian order (actual data order for Sequence is 30 81)
                    binr.ReadByte();    //advance 1 byte
                else if (twobytes == 0x8230)
                    binr.ReadInt16();   //advance 2 bytes
                else
                    return null;


                twobytes = binr.ReadUInt16();
                if (twobytes != 0x0102) //version number
                    return null;
                bt = binr.ReadByte();
                if (bt != 0x00)
                    return null;




                //------ all private key components are Integer sequences ----
                elems = GetIntegerSize(binr);
                MODULUS = binr.ReadBytes(elems);


                elems = GetIntegerSize(binr);
                E = binr.ReadBytes(elems);


                elems = GetIntegerSize(binr);
                D = binr.ReadBytes(elems);


                elems = GetIntegerSize(binr);
                P = binr.ReadBytes(elems);


                elems = GetIntegerSize(binr);
                Q = binr.ReadBytes(elems);


                elems = GetIntegerSize(binr);
                DP = binr.ReadBytes(elems);


                elems = GetIntegerSize(binr);
                DQ = binr.ReadBytes(elems);


                elems = GetIntegerSize(binr);
                IQ = binr.ReadBytes(elems);


                System.Console.WriteLine("showing components ..");
                if (verbose)
                {
                    showBytes("\nModulus", MODULUS);
                    showBytes("\nExponent", E);
                    showBytes("\nD", D);
                    showBytes("\nP", P);
                    showBytes("\nQ", Q);
                    showBytes("\nDP", DP);
                    showBytes("\nDQ", DQ);
                    showBytes("\nIQ", IQ);
                }


                // ------- create RSACryptoServiceProvider instance and initialize with public key -----
                CspParameters CspParameters = new CspParameters();
                CspParameters.Flags = CspProviderFlags.UseMachineKeyStore;
                RSACryptoServiceProvider RSA = new RSACryptoServiceProvider(1024, CspParameters);
                RSAParameters RSAparams = new RSAParameters();
                RSAparams.Modulus = MODULUS;
                RSAparams.Exponent = E;
                RSAparams.D = D;
                RSAparams.P = P;
                RSAparams.Q = Q;
                RSAparams.DP = DP;
                RSAparams.DQ = DQ;
                RSAparams.InverseQ = IQ;
                RSA.ImportParameters(RSAparams);
                return RSA;
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                binr.Close();
            }
        }
        private static int GetIntegerSize(BinaryReader binr)
        {
            byte bt = 0;
            byte lowbyte = 0x00;
            byte highbyte = 0x00;
            int count = 0;
            bt = binr.ReadByte();
            if (bt != 0x02)      //expect integer
                return 0;
            bt = binr.ReadByte();
            if (bt == 0x81)
                count = binr.ReadByte();    // data size in next byte
            else
                if (bt == 0x82)
            {
                highbyte = binr.ReadByte();    // data size in next 2 bytes
                lowbyte = binr.ReadByte();
                byte[] modint = { lowbyte, highbyte, 0x00, 0x00 };
                count = BitConverter.ToInt32(modint, 0);
            }
            else
            {
                count = bt;      // we already have the data size
            }
            while (binr.ReadByte() == 0x00)
            {    //remove high order zeros in data
                count -= 1;
            }
            binr.BaseStream.Seek(-1, SeekOrigin.Current);        //last ReadByte wasn't a removed zero, so back up a byte
            return count;
        }
        private static void showBytes(String info, byte[] data)
        {
            System.Console.WriteLine("{0} [{1} bytes]", info, data.Length);
            for (int i = 1; i <= data.Length; i++)
            {
                System.Console.Write("{0:X2} ", data[i - 1]);
                if (i % 16 == 0)
                    System.Console.WriteLine();
            }
            System.Console.WriteLine("\n\n");
        }
        public static RSACryptoServiceProvider crearRsaDesdePEM(string base64)
        {
            ////
            //// Extraiga de la cadena los header y footer
            base64 = base64.Replace("-----BEGIN RSA PRIVATE KEY-----", string.Empty);
            base64 = base64.Replace("-----END RSA PRIVATE KEY-----", string.Empty);
            //// el resultado que se encuentra en base 64 cambielo a
            //// resultado string
            byte[] arrPK = Convert.FromBase64String(base64);
            //// obtenga el Rsa object a partir de
            return DecodeRSAPrivateKey(arrPK);
        }
        public XmlDocument SignDocXml(XmlDocument xmldocument, X509Certificate2 certificado, string Uri)
        {
            SignedXml signedXml = new SignedXml(xmldocument);
            signedXml.SigningKey = certificado.PrivateKey;
            Signature XMLSignature = signedXml.Signature;
            Reference reference = new Reference();
            reference.Uri = Uri;
            XMLSignature.SignedInfo.AddReference(reference);
            KeyInfo keyInfo = new KeyInfo();
            keyInfo.AddClause(new RSAKeyValue((RSA)certificado.PrivateKey));
            keyInfo.AddClause(new KeyInfoX509Data(certificado));
            XMLSignature.KeyInfo = keyInfo;
            signedXml.ComputeSignature();
            XmlElement xmlDigitalSignature = signedXml.GetXml();
            xmldocument.DocumentElement.AppendChild(xmldocument.ImportNode(xmlDigitalSignature, true));
            return xmldocument;
        }
        public XmlDocument GenerateDte_withCaf(string xmlDte,string xmlCaf, X509Certificate2 cert)
        {
            //modificar en este punto para reemplazar caracteres raros !
            xmlDte = xmlDte.Replace(@"xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema""", "");
            XElement element = XElement.Parse(xmlDte);
                 
            string RE = element.DescendantsAndSelf("DTE").Descendants("RUTEmisor").First().Value; 
            string TD = element.DescendantsAndSelf("DTE").Descendants("TipoDTE").First().Value; 
            string F =  element.DescendantsAndSelf("DTE").Descendants("Folio").First().Value; 
            string FE = element.DescendantsAndSelf("DTE").Descendants("FchEmis").First().Value; 
            string RR = element.DescendantsAndSelf("DTE").Descendants("RUTRecep").First().Value; 
            string RSR =element.DescendantsAndSelf("DTE").Descendants("RznSocRecep").First().Value;  
            string MNT =element.DescendantsAndSelf("DTE").Descendants("MntTotal").First().Value;  
            string IT1 =element.DescendantsAndSelf("DTE").Descendants("NmbItem").First().Value;
            string TSTED = DateTime.Now.ToString("yyy/MM/dd" + "T" + "HH:mm:ss").Replace('/', '-');
            XElement caf = XElement.Parse(xmlCaf);
            // caf.Descendants("CAF");
    
            XElement xelem = new XElement("TED",
                new XAttribute("version","1.0"),
                new XElement("DD",
                new XElement("RE",RE),
                new XElement("TD",TD),
                new XElement("F",F),
                new XElement("FE",FE),
                new XElement("RR",RR),
                new XElement("RSR",RSR),
                new XElement("MNT",MNT),
                new XElement("IT1",IT1),
                caf.Descendants("CAF"),
                new XElement("TSTED",TSTED)
                )             
                );
            string RSASK = caf.DescendantsAndSelf("AUTORIZACION").Descendants("RSASK").First().Value;
            string ResultRSA = CreateFRMT(xelem.Descendants("DD").ToString(), RSASK);

            xelem.Add(
                new XElement("FRMT",
                new XAttribute("algoritmo", "SHA1withRSA"),
                ResultRSA
                )
                );

            //agregamos el nodo del timbre al dte
            element.Element("Documento").Add(
                xelem,
                new XElement("TmstFirma",TSTED));

           // element.Element("Documento").FirstAttribute.Value = "DF602";
           string Uri ="#"+ element.Element("Documento").FirstAttribute.Value;

            XmlDocument xmldoc = new XmlDocument();
            xmldoc.LoadXml(element.ToString());

            // SignDocXml(xmldoc,cert,Uri);
            // element.Save(@"C:\prueba\dte.xml");
            XNamespace xmlns = "http://www.sii.cl/SiiDte";
            XNamespace xsi = "http://www.w3.org/2001/XMLSchema-instance";
            XNamespace schemaLocation = "http://www.sii.cl/SiiDte EnvioDTE_v10.xsd";
          
            XElement SetDTE = 
                new XElement(xmlns+"EnvioDTE",
                                       // new XAttribute("xmlns", xmlns), 
                                        new XAttribute(XNamespace.Xmlns+"xsi", xsi), 
                                        new XAttribute(xsi+ "schemaLocation", schemaLocation),
                                        new XAttribute("version", "1.0"),
                new XElement("SetDTE", 
                                        new XAttribute("ID", "SetDoc"),
                                        new XElement("Caratula", 
                                        new XAttribute("version","1.0"),
                                                                new XElement("RutEmisor", RE),
                                                                new XElement("RutEnvia", RE),
                                                                new XElement("RutReceptor", RR),
                                                                new XElement("FchResol", "2016-09-13"),
                                                                new XElement("NroResol", "0"),
                                                                new XElement("TmstFirmaEnv",TSTED),
                                                                new XElement("SubTotDTE",
                                                                new XElement("TpoDTE", TD),
                                                                new XElement("NroDTE", "1")
                )
                ), 
              XElement.Parse(SignDocXml(xmldoc, cert, Uri).InnerXml) //DTE
                )
                );

            //dte con el setdte yejecutamos la segunda firma
            XDocument documento = new XDocument(new XDeclaration("1.0", "ISO-8859-1", ""),SetDTE);

            XmlDocument dte = new XmlDocument();
            dte.PreserveWhitespace = true;
            dte.LoadXml("<?xml version=\"1.0\" encoding=\"ISO-8859-1\"?>" + documento.ToString().Replace(" xmlns=\"\"",""));
            return SignDocXml(dte, cert, "#SetDoc"); ;
        }
    }
}