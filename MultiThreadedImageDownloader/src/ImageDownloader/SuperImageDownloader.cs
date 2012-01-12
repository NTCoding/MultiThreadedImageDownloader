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
			this.htmlRetriever = htmlRetriever;
			this.parser  = parser;
			this.imageRetriever = imageRetriever;
		}

		public IEnumerable<DownloadedImageDTO> Download(string url)
		{
			var html = htmlRetriever.GetHtml(url);

			var urls = parser.Parse(html, url);

			// fetch the images

			// convert the images))
			return Enumerable.Empty<DownloadedImageDTO>();
		}
	}
}