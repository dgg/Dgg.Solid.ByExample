using System; 
using System.IO;
using static System.Console;

namespace Dgg.Solid.ByExample.ErrorLogMailer
{
	class Program
	{
		static void Main(string[] args)
		{
			string filePath = args[0]; 

			string body = null;
			try
			{
				var sender = new EmailSender();
				body = sender.ReadBody(new FileInfo(filePath));
				sender.SendMail();
			}
			catch (System.Exception ex)
			{
				Error.WriteLine(ex.ToString());
				Environment.Exit(-1); 
			}		
			 
			writeLine("EMAIL SENT"); 
			writeLine("body:", body); 
		} 
 
		private static void writeLine(string label, string text = null, ConsoleColor textColor = ConsoleColor.White) 
		{ 
			ConsoleColor current = ForegroundColor; 
			ForegroundColor = textColor; 
			WriteLine(label); 
			ForegroundColor = current; 
			if (!string.IsNullOrWhiteSpace(text)) 
			{
				Write("\t"); 
				WriteLine(text); 
			} 
		}
	}
}