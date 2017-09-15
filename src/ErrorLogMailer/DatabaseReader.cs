using System.IO;
using Microsoft.Data.Sqlite;
using System.Data;

namespace Dgg.Solid.ByExample.ErrorLogMailer
{
	public class DatabaseReader
	{
		public string ReadBody(FileInfo connectionFile)
		{
			string body = null;
			string connectionString = connectionFile.ReadText();
			using (var connection = new SqliteConnection(connectionString))
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