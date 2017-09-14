using System;
using System.IO;
using MailKit.Net.Smtp; 
using MimeKit;

namespace Dgg.Solid.ByExample.ErrorLogMailer
{
	public class EmailSender
	{
		private string _body;
		public string ReadBody(FileInfo file)
		{
			using (StreamReader rdr = file.OpenText()) 
			{
				_body = rdr.ReadToEnd();
			}
			return _body;
		}

		public void SendMail()
		{
			using (SmtpClient client = new SmtpClient()) 
			{
				var emailMessage = new MimeMessage 
				{ 
					From = { new MailboxAddress("Me", "dgg@solid-by-example.com") }, 
					To = { new MailboxAddress("cto@solid-by-example.com") }, 
					Subject = "Error log", 
					Body = new TextPart("plain") { Text = _body } 
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