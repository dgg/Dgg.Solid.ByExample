using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;


namespace Dgg.Solid.ByExample.ErrorLogMailer
{
	public class FileReader : IMessageInfoRetriever
	{
		private readonly IFormatReader _default;
		private readonly List<IFormatReader> _readers;
		private readonly FileInfo _file;
		public FileReader(FileInfo file, IFormatReader defaultReader)
		{
			_file = file;
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

		public string GetMessageBody()
		{
			string fileContents = _file.ReadText();
			string messageBody = parseBody(fileContents);
			return messageBody;
		}

		private string parseBody(string fileContents)
		{
			bool handled = false;
			string body= null;
			foreach(IFormatReader reader in _readers)
			{
				handled = tryRead(reader, fileContents, out body);
				if (handled) break;
			}

			if (!handled)
			{
				body = _default.ReadBody(fileContents);
			}
			
			return body;
		}

		private bool tryRead(IFormatReader reader, string fileContents, out string body)
		{
			bool handled = false;
			body = null;

			handled = reader.CanHandle(fileContents);
			if (handled)
			{
				body = reader.ReadBody(fileContents);
			}
			return handled;
		}
	}
}