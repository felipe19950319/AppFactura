
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
using System.Web.Script.Serialization;
using System.Xml;

namespace WcfService_factura.clases
{
    public class Metodos
    {
        public string PruebaTimbreDD(string DD, string pk)
        {
            ASCIIEncoding ByteConverter = new ASCIIEncoding();
            byte[] bytesStrDD = ByteConverter.GetBytes(DD);
            byte[] HashValue = new SHA1CryptoServiceProvider().ComputeHash(bytesStrDD);
            RSACryptoServiceProvider rsa = crearRsaDesdePEM(pk);
            byte[] bytesSing = rsa.SignHash(HashValue, "SHA1");
            string FRMT1 = Convert.ToBase64String(bytesSing);
            return FRMT1;
        }
        public string obtieneLecturaXML(string xml_string, string NameItem)
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

        public string obtieneFRMA(string path)
        {

            string FRMA = obtieneLecturaXML(path, "FRMA");
            return FRMA;
        }

        public string obtienePrivateKeyFactura(string path)
        {

            string privateKey = obtieneLecturaXML(path, "RSASK");
            return privateKey;
        }

        public string obtienePublicKeyFactura(string path)
        {

            string publicKey = obtieneLecturaXML(path, "RSAPUBK");
            return publicKey;
        }


        static bool verbose = false;

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

        public void Comprobar_Firma(string uriXml)
        {
            const string XPATH_MODULUS = "sii:DTE/sig:Signature/sig:KeyInfo/sig:KeyValue/sig:RSAKeyValue/sig:Modulus";
            const string XPATH_EXPONENT = "siI:DTE/sig:Signature/sig:KeyInfo/sig:KeyValue/sig:RSAKeyValue/sig:Exponent";
            XmlDocument documento = new XmlDocument();
            documento.PreserveWhitespace = true;
            documento.Load(uriXml);
            XmlNamespaceManager ns = new XmlNamespaceManager(documento.NameTable);
            ns.AddNamespace("sii", documento.DocumentElement.NamespaceURI);
            ns.AddNamespace("sig", "http://www.w3.org/2000/09/xmldsig#");
            string Modulus = documento.SelectSingleNode(XPATH_MODULUS, ns).InnerText;
            string Exponent = documento.SelectSingleNode(XPATH_EXPONENT, ns).InnerText;
            string PublicKeyXml = string.Empty;
            PublicKeyXml += "<RSAKeyValue";
            PublicKeyXml += "<Modulus>{0}</Modulus>";
            PublicKeyXml += "<Exponent>{1}</Exponent>";
            PublicKeyXml += "</RSAKeyValue>";
            PublicKeyXml = string.Format(PublicKeyXml, Modulus, Exponent);
            RSACryptoServiceProvider publicKey = new RSACryptoServiceProvider();
            publicKey.FromXmlString(PublicKeyXml);
            SignedXml signedXml = new SignedXml(documento);
            XmlNodeList nodeList = documento.GetElementsByTagName("Signature");
            signedXml.LoadXml((XmlElement)nodeList[0]);
            bool esCorrecto = signedXml.CheckSignature(publicKey);

        }

        public void firmarDocumentoXml(ref XmlDocument xmldocument, X509Certificate2 certificado, string referenciaUri)
        {

            SignedXml signedXml = new SignedXml(xmldocument);
            signedXml.SigningKey = certificado.PrivateKey;
            Signature XMLSignature = signedXml.Signature;
            Reference reference = new Reference();
            reference.Uri = referenciaUri;
            XMLSignature.SignedInfo.AddReference(reference);
            KeyInfo keyInfo = new KeyInfo();
            keyInfo.AddClause(new RSAKeyValue((RSA)certificado.PrivateKey));

            keyInfo.AddClause(new KeyInfoX509Data(certificado));
            XMLSignature.KeyInfo = keyInfo;

            signedXml.ComputeSignature();
            XmlElement xmlDigitalSignature = signedXml.GetXml();

            xmldocument.DocumentElement.AppendChild(xmldocument.ImportNode(xmlDigitalSignature, true));


        }
        public string Dt_to_json(DataTable table)
        {
            JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
            List<Dictionary<string, object>> parentRow = new List<Dictionary<string, object>>();
            Dictionary<string, object> childRow;
            foreach (DataRow row in table.Rows)
            {
                childRow = new Dictionary<string, object>();
                foreach (DataColumn col in table.Columns)
                {
                    childRow.Add(col.ColumnName, row[col]);
                }
                parentRow.Add(childRow);
            }
            return jsSerializer.Serialize(parentRow);
        }

    }
}