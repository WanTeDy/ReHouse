using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Net.Mail;
using ReHouse.Utils;
using System.IO;
using static RazorEngine.Razor;

namespace ReHouse.Utils.Helpers
{
    public class SmtpEmailSender
    {        
        public SmtpEmailSender(string smtp, string from, string password, int port = 25)
        {
            SmtpServer = smtp;
            Sender = from;
            Password = password;
            Port = port;
        }

        public string SmtpServer { get; set; }
        public string Sender { get; set; }
        public string Password { get; set; }
        public int Port { get; set; }

        public static String FormatUrl(String view)
        {
            return ConstV.ServerLocalPath + @"\MailTemplate" + @"\" + view + ".cshtml";
        }

        public static String GetHtmlRazor(Object model, String url)
        {
            var fileText = File.ReadAllText(url);

            //var result = Engine.Razor.RunCompile(fileText, "templateKey", null, new { Name = "World" });
            //return null;
            return Parse(fileText, model);
        }

        public void Send(string destination, string subject, string body)
        {
            // адрес и порт smtp-сервера, с которого мы и будем отправлять письмо
            SmtpClient client = new SmtpClient(SmtpServer, Port);

            //client.DeliveryMethod = SmtpDeliveryMethod.Network;
            //client.UseDefaultCredentials = true;                   
            client.Credentials = new System.Net.NetworkCredential(Sender, Password);
            //client.EnableSsl = true;
            // создаем письмо: message.Destination - адрес получателя
            var mail = new MailMessage(Sender, destination);
            mail.Subject = subject;
            mail.Body = body;// HttpUtility.HtmlEncode(body);
            mail.IsBodyHtml = true;
            mail.To.Add(Sender);

            client.Send(mail);
        }
    }
}