using System.Linq; 
using System.Xml; 
using System.Xml.Linq; 

namespace Dgg.Solid.ByExample.ErrorLogMailer
{
	class XmlFormatReader : IFormatReader
	{
	private XDocument _doc;
	public bool CanHandle(string fileContent)
	{
		bool canHandle;
		try 
	  	{
			_doc = XDocument.Parse(fileContent);
			canHandle = true;
	  	} 
	  	catch (XmlException) 
	  	{
			canHandle = false;
	  	}
		return canHandle;
	}

		public string ReadBody(string fileContent)
		{
			string body = _doc 
		  			.Descendants("email") 
		  			.Descendants("body") 
		  			.Single().Value; 
			return body;
		}	
	}	
}