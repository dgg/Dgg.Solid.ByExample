using System; 
using System.IO; 
using MailKit.Net.Smtp; 
using MimeKit;

namespace Dgg.Solid.ByExample.ErrorLogMailer
{
    class Program
    {
        static void Main(string[] args)
        {
            string filePath = args[0]; 
 
            FileInfo file = new FileInfo(filePath); 
            string body; 
            using (StreamReader rdr = file.OpenText()) 
            { 
                body = rdr.ReadToEnd(); 
            } 
 
            MailboxAddress from = new MailboxAddress("Me", "dgg@solid-by-example.com"),
                to = new MailboxAddress("cto@solid-by-example.com");
            using (SmtpClient client = new SmtpClient()) 
            { 
                var emailMessage = new MimeMessage 
                { 
                    From = { from }, 
                    To = { to }, 
                    Subject = "Error log", 
                    Body = new TextPart("plain") { Text = body } 
                }; 
 
                /*  
                * WE WOULD BE AUTHENTICATING and SENDING FOR REAL 
                */ 
                client.ServerCertificateValidationCallback = (s, c, h, e) => true; 
                //client.Connect("smtp-mail.solid-by-example.com", 587, false); 
                //client.Authenticate("dgg@solid-by-example.com", "P4zzw0rd"); 
                //client.Send(emailMessage); 
                client.Disconnect(true); 
            } 
            writeLine("EMAIL SENT"); 
            writeLine("sender:", from.ToString());
            writeLine("recipient:", to.ToString());
            writeLine("body:", body); 
        } 
 
        private static void writeLine(string label, string text = null, ConsoleColor textColor = ConsoleColor.White) 
        { 
            ConsoleColor current = Console.ForegroundColor; 
            Console.ForegroundColor = textColor; 
            Console.WriteLine(label); 
            Console.ForegroundColor = current; 
            if (!string.IsNullOrWhiteSpace(text)) 
            { 
                Console.Write("\t"); 
                Console.WriteLine(text); 
            } 
        }
    }
}