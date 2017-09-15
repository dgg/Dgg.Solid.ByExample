using System;
using System.IO;
using MailKit.Net.Smtp; 
using MimeKit;

namespace Dgg.Solid.ByExample.ErrorLogMailer
{
	public class EmailSender
	{
		private readonly FileReader _fileReader;
		private readonly DatabaseReader _dbReader;
		public EmailSender()
		{
			_fileReader = new FileReader(new FlatFormatReader());
			_dbReader = new DatabaseReader();
		}

		private string _body;
		public string ReadBody(FileInfo file, params IFormatReader[] readers)
		{
			if (file == null) throw new ArgumentNullException(nameof(file));
    		if (!file.Exists) throw new FileNotFoundException("File containing the email body does not exist", file.FullName);

			using (StreamReader rdr = file.OpenText()) 
			{
				_body = _fileReader
					.RegisterRange(readers)
					.ReadBody(rdr);
			}
			return _body;
		}

		public string ReadBodyFromDb(FileInfo connectionFile)
		{
			_body = _dbReader.ReadBody(connectionFile);
			return _body;
		}

		public void SendMail()
		{
			if (_body == null) throw new InvalidOperationException("Please load the body before sending the email.");

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
			// reset the body after sending
			_body = null;
		}
	}    
}