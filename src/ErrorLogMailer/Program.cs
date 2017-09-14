using System; 
using System.IO; 

namespace Dgg.Solid.ByExample.ErrorLogMailer
{
	class Program
	{
		static void Main(string[] args)
		{
			string filePath = args[0]; 
 
			FileInfo file = new FileInfo(filePath);
			var sender = new EmailSender();
			string body = sender.ReadBody(file);
			sender.SendMail();
			 
			writeLine("EMAIL SENT"); 
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