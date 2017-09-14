using System;
using System.Linq; 
using System.IO;
using System.Xml; 
using System.Xml.Linq; 

namespace Dgg.Solid.ByExample.ErrorLogMailer
{
	class FormatReader
	{
		public string ReadBody(StreamReader file)
		{
			string body;
			string fileContent = file.ReadToEnd();
	  		try 
	  		{
				XDocument doc = XDocument.Parse(fileContent);
				body = doc 
		  			.Descendants("email") 
		  			.Descendants("body") 
		  			.Single().Value; 
	  		} 
	  		catch (XmlException) 
	  		{
				body = fileContent;
	  		} 
	  		return body; 
		}
	}
}