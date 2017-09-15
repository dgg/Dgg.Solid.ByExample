using System; 
using System.IO;
using static System.Console;

using Microsoft.Data.Sqlite;
using System.Data;

namespace Dgg.Solid.ByExample.ErrorLogMailer
{
	class Program
	{
		static void Main(string[] args)
		{
			// poor man's command line parsing
			string filePath = args[0];
			bool useDb = args.Length > 1 && 
				args[1].Equals("-db", StringComparison.OrdinalIgnoreCase);

			string body = null;
			try
			{
				var sender = new EmailSender();
				var file = new FileInfo(filePath);
				if (useDb)
				{
					// init data (not needed in real-world scenario)
					var keepAliveConnection = initDb(File.ReadAllText(filePath));
					body = sender.ReadBodyFromDb(file);
					keepAliveConnection.Close();
					keepAliveConnection.Dispose();
				}
				else
				{
					body = sender.ReadBody(file, new XmlFormatReader());
				}
				sender.SendMail();
			}
			catch (System.Exception ex)
			{
				writeLine("ERROR", ex.ToString(), ConsoleColor.Red);
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

		private static IDbConnection initDb(string connectionString)
		{
			var connection = new SqliteConnection(connectionString);
			connection.Open();
			using (var command = connection.CreateCommand())
			{
				command.CommandType = CommandType.Text;

				command.CommandText = @"
CREATE TABLE `Log` (
	`Id`	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
	`Application`	TEXT NOT NULL,
	`Logged`	TEXT NOT NULL,
	`Level`	TEXT NOT NULL,
	`Message`	TEXT NOT NULL,
	`UserName`	TEXT,
	`ServerName`	TEXT,
	`Port`	TEXT,
	`Url`	TEXT,
	`Https`	INTEGER,
	`ServerAddress`	TEXT,
	`RemoteAddress`	TEXT,
	`Logger`	TEXT,
	`Callsite`	TEXT,
	`Exception`	TEXT
);";
				command.ExecuteNonQuery();
				command.CommandText = @"
INSERT INTO `Log`(
	`Application`,
	`Logged`,
	`Level`,
	`Message`
) 
VALUES (
	'RandomApp',
	'2017-09-15 10:28:33',
	'Error',
	'wow! from a db'
);";
			command.ExecuteNonQuery();
		}
		return connection;
		}
	}
}