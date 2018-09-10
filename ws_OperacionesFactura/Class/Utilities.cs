using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Web;

namespace ws_OperacionesFactura.Class
{
    public class Utilities
    {
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
    }
}