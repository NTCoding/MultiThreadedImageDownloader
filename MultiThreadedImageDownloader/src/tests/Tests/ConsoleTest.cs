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

			var images = downloader.Download("http://www.struq.com");

			foreach (var item in images)
			{
				Console.WriteLine(item.URL);
			}
		}
	}
}