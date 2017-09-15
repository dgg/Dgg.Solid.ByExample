using System;
using System.IO;

namespace Dgg.Solid.ByExample.ErrorLogMailer
{
	static class FileExtensions
	{
		public static string ReadText(this FileInfo file)
		{
			if (file == null) throw new ArgumentNullException(nameof(file));
    		if (!file.Exists) throw new FileNotFoundException("File does not exist", file.FullName);

			string fileContents;
			using( var sr = file.OpenText())
			{
				fileContents = sr.ReadToEnd();
			}
			return fileContents;
		}
	}
}