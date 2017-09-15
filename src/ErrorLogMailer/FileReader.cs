using System;
using System.Collections.Generic;
using System.Linq; 
using System.IO;
using System.Xml; 
using System.Xml.Linq; 

namespace Dgg.Solid.ByExample.ErrorLogMailer
{
	class FileReader
	{
		private readonly IFormatReader _default;
		private readonly List<IFormatReader> _readers;
		public FileReader(IFormatReader defaultReader)
		{
			_default = defaultReader;
			_readers = new List<IFormatReader>();
		}

		public FileReader Register(IFormatReader reader)
		{
			if (reader != null) _readers.Add(reader);
			return this;
		}

		public FileReader RegisterRange(IEnumerable<IFormatReader> readers)
		{
			_readers.AddRange(readers ?? Enumerable.Empty<IFormatReader>());
			return this;
		}

		public string ReadBody(StreamReader file)
		{
			string messageBody = string.Empty;
			bool handled = false;
			
			string fileContents = file.ReadToEnd();
			foreach(IFormatReader reader in _readers)
			{
				handled = reader.CanHandle(fileContents);
				if (handled)
				{
					messageBody = reader.ReadBody(fileContents);
					break;
				}
			}
			
			if (!handled)
			{
				messageBody = _default.ReadBody(fileContents);
			}

			return messageBody;
		}
	}
}