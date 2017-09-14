 using System;
 using System.Data.Common;

namespace Dgg.Solid.ByExample.ErrorLogMailer
{
	public class DatabaseConnectionReader : IFormatReader
	{
		public bool CanHandle(string fileContents)
		{
			bool canHandle = false;
			var builder = new DbConnectionStringBuilder();
			try
			{
				builder.ConnectionString = fileContents;
				canHandle = true;
			}
			catch (ArgumentException) { }
			return canHandle;
		}

		public string ReadBody(string fileContents)
		{
			throw new NotImplementedException("Need to read from the database! Not from this interface!");
		}		
	}
}