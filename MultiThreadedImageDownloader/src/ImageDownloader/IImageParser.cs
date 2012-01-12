using System.Collections.Generic;

namespace ImageDownloader
{
	public interface IImageParser
	{
		IEnumerable<string> Parse(string html);
	}
}