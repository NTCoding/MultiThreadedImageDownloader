using System;
using System.Collections.Generic;

namespace ImageDownloader
{
	public class ImageRetriever : IImageRetriever
	{
		public IEnumerable<DownloadedImageDTO> RetrieveFor(IEnumerable<string> testSrcs)
		{
			throw new NotImplementedException();
		}
	}
}