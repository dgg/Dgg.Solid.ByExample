using System.IO;
using Microsoft.Data.Sqlite;
using System.Data;

namespace Dgg.Solid.ByExample.ErrorLogMailer
{
	public class DatabaseReader : IMessageInfoRetriever
	{
		private readonly string _connectionString;
		public DatabaseReader(FileInfo connectionFile)
		{
			_connectionString = connectionFile.ReadText();
		}

		public string GetMessageBody()
		{
			string body = null;
			using (var connection = new SqliteConnection(_connectionString))
			{
				connection.Open();

				using (var command = connection.CreateCommand())
				{
					command.CommandText = "SELECT [Message] FROM Log ORDER BY ROWID ASC Limit 1";
					command.CommandType = CommandType.Text;

					body = command.ExecuteScalar() as string;
				}
				connection.Close();
			}	
			return body;
		}
	}
}