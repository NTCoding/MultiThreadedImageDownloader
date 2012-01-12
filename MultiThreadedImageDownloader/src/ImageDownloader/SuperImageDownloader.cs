using System;
using System.Collections.Generic;
using System.Linq;

namespace ImageDownloader
{
	public class SuperImageDownloader
	{
		private readonly IHtmlRetriever htmlRetriever;
		private readonly IImageParser parser;

		public SuperImageDownloader(IHtmlRetriever htmlRetriever, IImageParser parser)
		{
			this.htmlRetriever = htmlRetriever;
			this.parser  = parser;
		}

		public IEnumerable<DownloadedImageDTO> Download(string url)
		{
			// get the resource
			var html = htmlRetriever.GetHtml(url);

			var urls = parser.Parse(html);

			// fetch the images

			// convert the images))
			return Enumerable.Empty<DownloadedImageDTO>();
		}
	}
}