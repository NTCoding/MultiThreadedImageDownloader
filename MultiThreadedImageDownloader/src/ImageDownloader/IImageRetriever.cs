using System.Collections.Generic;

namespace ImageDownloader
{
	public interface IImageRetriever
	{
		IEnumerable<DownloadedImageDTO> RetrieveFor(IEnumerable<string> testSrcs);
	}
}