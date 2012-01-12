using System;
using System.Collections.Generic;
using System.Linq;

namespace ImageDownloader
{
	public class SuperImageDownloader
	{
		private readonly IHtmlRetriever htmlRetriever;
		private readonly IImageParser parser;
		private readonly IImageRetriever imageRetriever;

		public SuperImageDownloader(IHtmlRetriever htmlRetriever, IImageParser parser, IImageRetriever imageRetriever)
		{
			this.htmlRetriever  = htmlRetriever;
			this.imageRetriever = imageRetriever;
			this.parser  = parser;
		}

		public IEnumerable<DownloadedImageDTO> Download(string url)
		{
			var html = htmlRetriever.GetHtml(url);

			var srcs = parser.Parse(html, url);

			imageRetriever.RetrieveFor(srcs);

			return Enumerable.Empty<DownloadedImageDTO>();
		}
	}
}