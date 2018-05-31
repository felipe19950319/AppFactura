using Limilabs.Client.IMAP;
using Limilabs.Mail;
using Limilabs.Mail.MIME;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace WcfService_factura.clases
{
    public class Mail
    {
        public void get_data_message()
        {
           
            try
            {
                string resultado;
                // C#          
                using (Imap imap = new Imap())
                {
                    //puerto 110 

                    imap.Connect("imap.gmail.com", 993, true);   // conneccion a host con el puerto 
                    imap.UseBestLogin("re.sii.tyscom@gmail.com", "Facturacion2016");
                    imap.SelectInbox();

                    List<long> uids = imap.Search(Flag.Unseen);

                    foreach (long uid in uids)
                    {
                        IMail email = new MailBuilder().CreateFromEml(imap.GetMessageByUID(uid));

                        Console.WriteLine(email.Subject);
                        // guardar elementos en el disco
                        foreach (MimeData mime in email.Attachments)
                        {
                            mime.Save("c:/tyscom xml/respuestas_sii/" + mime.SafeFileName);
                        }
                    }
                    imap.Close();
                }

               
             

            }
            catch (Exception ex)
            {

                

            }
          
        }
        public void email_send()
        {
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
            mail.From = new MailAddress("re.sii.tyscom@gmail.com");
            mail.To.Add("fabianfmiralles@gmail.com");
            mail.Subject = "Documento DTE ";
            mail.Body = "se adjunta el documento dte";

            System.Net.Mail.Attachment attachment;
            ////////trabajar con query desde la bd la direccion del doc xml esto solo es prueba de envio :(
            attachment = new System.Net.Mail.Attachment("c:/tyscom xml/xml/documentos/DTE_FIRMADO.xml");
            mail.Attachments.Add(attachment);

            SmtpServer.Port = 587;
            //"re.sii.tyscom@gmail.com"
            SmtpServer.Credentials = new System.Net.NetworkCredential("re.sii.tyscom@gmail.com", "Facturacion2016");
            SmtpServer.EnableSsl = true;

            SmtpServer.Send(mail);

        }


    }
}