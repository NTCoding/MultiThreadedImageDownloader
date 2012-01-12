using System;
using System.Net;

namespace ImageDownloader
{
	public class HtmlRetriever : IHtmlRetriever
	{
		private WebClient client;

		public string GetHtml(string url)
		{
			return Client.DownloadString(url);
		}

		protected WebClient Client
		{
			get { return client ?? (client = new WebClient()); }
		}
	}
}