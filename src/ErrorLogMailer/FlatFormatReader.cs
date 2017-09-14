namespace Dgg.Solid.ByExample.ErrorLogMailer
{
	class FlatFormatReader : IFormatReader
	{
		public bool CanHandle(string fileContent)
		{
			return true;
		}

		public string ReadBody(string fileContent)
		{
			return fileContent;
		}
	}
}