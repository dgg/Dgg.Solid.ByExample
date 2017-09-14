namespace Dgg.Solid.ByExample.ErrorLogMailer
{
	public interface IFormatReader
	{
		bool CanHandle(string fileContent);
		string ReadBody(string fileContent);
	}
}