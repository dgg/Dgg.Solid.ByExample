using System;
using System.IO;
using MailKit.Net.Smtp; 
using MimeKit;

namespace Dgg.Solid.ByExample.ErrorLogMailer
{
	public class EmailSender
	{
		public void SendMail(string body)
		{
			if (body == null) throw new ArgumentNullException(nameof(body));

			using (SmtpClient client = new SmtpClient()) 
			{
				var emailMessage = new MimeMessage 
				{ 
					From = { new MailboxAddress("Me", "dgg@solid-by-example.com") }, 
					To = { new MailboxAddress("cto@solid-by-example.com") }, 
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
		}
	}    
}