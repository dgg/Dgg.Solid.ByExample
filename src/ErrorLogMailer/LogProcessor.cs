using System;
using Microsoft.Data.Sqlite;
using System.Data;

namespace Dgg.Solid.ByExample.ErrorLogMailer
{
	public class LogProcessor
	{
		private readonly IEmailSender _sender;
		private readonly IMessageInfoRetriever _messageRetriever;
		public LogProcessor(IEmailSender sender, IMessageInfoRetriever messageRetriever)
		{
			_sender = sender;
			_messageRetriever = messageRetriever;
		}
		public string Process()
		{
			string body = _messageRetriever.GetMessageBody();
			_sender.SendMail(body);
			return body;
		}
	}
}