using System;
using ImageDownloader;
using NUnit.Framework;

namespace Tests
{
	[TestFixture]
	public class ConsoleTest
	{
		[Test]
		public void DownloadImages()
		{
			var downloader = new SuperImageDownloader(new HtmlRetriever(), new ImageParser(), new ImageRetriever());

			var result = downloader.Download("http://www.bbc.co.uk");

			foreach (var item in result)
			{
				Console.WriteLine(item.URL);
			}
		}
	}
}